using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

namespace Response
{
    public class QueryListener : BackgroundService
    {
        private readonly IMediator _mediator;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public QueryListener(IMediator mediator)
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
            var server = new ResponseSocket(">tcp://localhost:5557");
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var command = await Task.Factory.StartNew(() =>
                {
                    var frameString = server.ReceiveFrameString();
                    return JsonConvert.DeserializeObject(frameString, _jsonSerializerSettings);
                }, cancellationToken);

                var response = await _mediator.Send(command, cancellationToken);
                server.SendFrame(JsonConvert.SerializeObject(response));
            }
        }
    }
}