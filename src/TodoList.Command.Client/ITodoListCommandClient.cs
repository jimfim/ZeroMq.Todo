using TodoList.Commands;

namespace TodoList.Client.Command
{
    public interface ITodoListCommandClient
    {
        void CreateTodoList(CreateTodoListCommand command);
    }
}