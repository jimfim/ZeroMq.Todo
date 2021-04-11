using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Client.Command;
using TodoList.Commands;
using TodoList.Queries;
using TodoList.Query.Client;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListCommandClient _todoListCommandClient;
        private readonly ITodoListQueryClient _todoListQueryClient;

        public TodoListController(ITodoListCommandClient todoListCommandClient, ITodoListQueryClient todoListQueryClient)
        {
            Console.WriteLine($"Constructing");
            _todoListCommandClient = todoListCommandClient;
            _todoListQueryClient = todoListQueryClient;
        }

        [HttpGet("{id}")]
        public async Task<GetTodoListResponse> Get(Guid id)
        {
            Console.WriteLine($"Get {id}");
            var response = await _todoListQueryClient.GetAsync(id);
            return response;
        }

        [HttpPost]
        public void Create([FromBody] CreateTodoListCommand command)
        {
            _todoListCommandClient.CreateTodoList(command);
        }
    }
}