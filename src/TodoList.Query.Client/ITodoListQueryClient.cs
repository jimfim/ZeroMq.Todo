using System;
using System.Threading.Tasks;
using TodoList.Queries;

namespace TodoList.Query.Client
{
    public interface ITodoListQueryClient
    {
       Task<GetTodoListResponse> GetAsync(Guid request);
    }
}