using System;
using MediatR;

namespace TodoList.Queries
{
    public class GetTodoListRequest : IRequest<GetTodoListResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetTodoListResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }
    }
}