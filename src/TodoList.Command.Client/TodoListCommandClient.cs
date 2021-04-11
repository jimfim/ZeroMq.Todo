using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using TodoList.Commands;
using TodoList.Common;

namespace TodoList.Client.Command
{
    public class TodoListCommandClient : ITodoListCommandClient
    {
        private readonly PushSocket _sender;

        public TodoListCommandClient()
        {
            _sender = new PushSocket(">tcp://127.0.0.1:5678");
        }

        public Task CreateTodoListAsync(CreateTodoListCommand command)
        {
            var message = MessageExtensions.PrepMessage(command);
            _sender.SendMultipartMessage(message);
            return Task.CompletedTask;
        }
    }
}