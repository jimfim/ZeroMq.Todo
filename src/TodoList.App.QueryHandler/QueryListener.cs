using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;

namespace Response
{
    public class QueryListener : BackgroundService
    {
        private readonly IMediator _mediator;

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

                var command = await Task.Factory.StartNew(async () =>
                {
                    var frameString = server.ReceiveFrameBytes();
                    return await JsonSerializer.DeserializeAsync<IRequest>(new MemoryStream(frameString),null, cancellationToken);
                }, cancellationToken);

                var response = await _mediator.Send(command, cancellationToken);
                server.SendFrame(JsonSerializer.SerializeToUtf8Bytes(response));
            }
        }
    }
}