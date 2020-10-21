using System;
using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using TodoList.Commands;

namespace TodoList.Client.Command
{
    public class TodoListCommandClient : ITodoListCommandClient
    {
        private readonly IConfiguration _configuration;
        private readonly PushSocket _sender;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
            {TypeNameHandling = TypeNameHandling.All};

        public TodoListCommandClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = configuration["command-proxy"];
            Console.WriteLine($"Connecting to {connectionString}");
            _sender = new PushSocket(connectionString);
        }

        public void CreateTodoList(CreateTodoListCommand command)
        {
            var payload = JsonConvert.SerializeObject(command, _settings);
            _sender.SendFrame(payload);
        }
    }
}