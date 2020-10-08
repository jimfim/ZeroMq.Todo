using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoList.Query.Models;

namespace Response.Handlers
{
    public class GetToDoListHandler : IRequestHandler<GetTodoListRequest, GetTodoListResponse>
    {
        public async Task<GetTodoListResponse> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetTodoListResponse()
            {
                Id = request.Id,
                Message = $"Some Random Stuff - {Guid.NewGuid()}"
            });
        }
    }
}