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
            await Console.Out.WriteLineAsync($"{request.Name}");
            return Unit.Value;
        }
    }
}