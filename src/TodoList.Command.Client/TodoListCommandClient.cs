using System;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using TodoList.Commands;

namespace TodoList.Client.Command
{
    public class TodoListCommandClient : ITodoListCommandClient
    {
        private readonly PushSocket _sender;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
            {TypeNameHandling = TypeNameHandling.All};

        public TodoListCommandClient()
        {
            _sender = new PushSocket(">tcp://127.0.0.1:5678");
        }

        public void CreateTodoList(CreateTodoListCommand command)
        {
            var payload = JsonConvert.SerializeObject(command, _settings);
            _sender.SendFrame(payload);
        }
    }
}