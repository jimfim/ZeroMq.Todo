using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoList.Commands;

namespace Server.Handlers
{
    public class CreateTodoListCommandHandler : AsyncRequestHandler<CreateTodoListCommand>
    {
        protected override async Task Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var thread = Thread.CurrentThread.ManagedThreadId;
            await Console.Out.WriteLineAsync($"{thread} : {request.Name}");
        }
    }
}