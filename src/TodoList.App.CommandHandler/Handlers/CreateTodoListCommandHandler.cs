using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoList.Commands;

namespace Server.Handlers
{
    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand>
    {
        public async Task<Unit> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var thread = Thread.CurrentThread.ManagedThreadId;
            await Console.Out.WriteLineAsync($"{thread} : {request.Name}");
            return Unit.Value;
        }
    }
}