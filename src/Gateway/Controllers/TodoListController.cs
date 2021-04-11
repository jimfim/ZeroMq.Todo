using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Client.Command;
using TodoList.Commands;
using TodoList.Queries;
using TodoList.Query.Client;

namespace Gateway.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListCommandClient _todoListCommandClient;
        private readonly ITodoListQueryClient _todoListQueryClient;
        public record CreateTodoListRequest(string name);

        public TodoListController(ITodoListCommandClient todoListCommandClient,ITodoListQueryClient todoListQueryClient)
        {
            _todoListCommandClient = todoListCommandClient;
            _todoListQueryClient = todoListQueryClient;
        }

        [HttpGet("{id}")]
        public async Task<GetTodoListResponse> Get(Guid id)
        {
            var response = await _todoListQueryClient.GetAsync(id);
            return response;
        }

        [HttpPost]
        public void Create([FromBody] CreateTodoListRequest request)
        {
            _todoListCommandClient.CreateTodoListAsync(new CreateTodoListCommand()
            {
                Name = request.name
            });
        }
    }
}