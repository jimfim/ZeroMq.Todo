using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using TodoList.Queries;

namespace TodoList.Query.Client
{
    public class TodoListQueryClient : ITodoListQueryClient
    {
        private readonly RequestSocket _sender;
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public TodoListQueryClient(IConfiguration configuration)
        {
            Console.WriteLine($"constructing TodoListQueryClient");
            var connectionString = configuration["query-proxy"];
            Console.WriteLine($"Connecting to {connectionString}");
            _sender = new RequestSocket(connectionString);
        }

        public async Task<GetTodoListResponse> GetAsync(Guid request)
        {
            var payload = JsonConvert.SerializeObject(new GetTodoListRequest
            {
                Id = request
            }, _settings);

            _sender.SendFrame(payload);
            //_sender.re
            var response = _sender.ReceiveFrameString();
            return JsonConvert.DeserializeObject<GetTodoListResponse>(response);
        }
    }
}