using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoList.Commands;

namespace TodoList.App.CommandHandler.Handlers
{
    public class CreateTodoListCommandHandler : AsyncRequestHandler<CreateTodoListCommand>
    {
        protected override async Task Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{request.Name}");
        }
    }
}