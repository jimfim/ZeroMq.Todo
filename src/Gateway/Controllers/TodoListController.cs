using System.Collections.Generic;
using Gateway.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TodoList.Client.Command;
using TodoList.Commands;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListCommandClient _todoListCommandClient;

        public TodoListController(ITodoListCommandClient todoListCommandClient)
        {
            _todoListCommandClient = todoListCommandClient;
        }

        [HttpGet]
        public IEnumerable<TodoListViewModel> Get()
        {
            return new List<TodoListViewModel>
            {
                new TodoListViewModel
                {
                    Name = "Hello World"
                }
            };
        }

        [HttpPost]
        public void Create([FromBody] CreateTodoListCommand command)
        {
            _todoListCommandClient.CreateTodoList(command);
        }
    }
}