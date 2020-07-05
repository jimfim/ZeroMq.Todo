using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

        public CommandListener(IMediator mediator)
        {
            _mediator = mediator;
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
            var receiver = new PullSocket(">tcp://127.0.0.1:1234");
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