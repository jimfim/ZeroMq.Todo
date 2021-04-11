using MediatR;

namespace TodoList.Commands
{

    public class CreateTodoListCommand : IRequest
    {
        public string Name { get; set; }
    }
}