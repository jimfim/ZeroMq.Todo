using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

namespace Server
{
    public class CommandListener : BackgroundService
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public CommandListener(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("Listening....");
            var threadCount = 4;
            var tasks = new List<Task>();
            for (var i = 1; i < threadCount; i++)
            {
                tasks.Add(StartListening(cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        private async Task StartListening(CancellationToken cancellationToken)
        {
            var receiver = new PullSocket(_configuration["Command_Proxy"]);
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var command = await Task.Factory.StartNew(() =>
                {
                    var frameString = receiver.ReceiveFrameString();
                    return JsonConvert.DeserializeObject(frameString, _jsonSerializerSettings);
                }, cancellationToken);
                await _mediator.Send(command, cancellationToken);
            }
        }
    }
}