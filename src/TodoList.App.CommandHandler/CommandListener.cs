using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;
using TodoList.Commands;

namespace TodoList.App.CommandHandler
{
    public class CommandListener : BackgroundService
    {
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
            for (var i = 1; i < threadCount; i++) tasks.Add(StartListening(cancellationToken));
            await Task.WhenAll(tasks);
        }

        private async Task StartListening(CancellationToken cancellationToken)
        {
            var receiver = new PullSocket(">tcp://127.0.0.1:11234");
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var message = receiver.ReceiveMultipartMessage();
                var command = await Common.MessageExtensions.Deserialize(message, cancellationToken);
                await _mediator.Send(command, cancellationToken);
            }
        }
    }
}