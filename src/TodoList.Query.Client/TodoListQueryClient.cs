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
            _sender = new RequestSocket(configuration["todo-query-proxy"]);
        }

        public async Task<GetTodoListResponse> GetAsync(Guid request)
        {
            var payload = JsonConvert.SerializeObject(new GetTodoListRequest
            {
                Id = request
            }, _settings);

            _sender.SendFrame(payload);
            var response = _sender.ReceiveFrameString();
            return JsonConvert.DeserializeObject<GetTodoListResponse>(response);
        }
    }
}