using System.Threading.Tasks;
using TodoList.Commands;

namespace TodoList.Client.Command
{
    public interface ITodoListCommandClient
    {
        Task CreateTodoListAsync(CreateTodoListCommand command);
    }
}