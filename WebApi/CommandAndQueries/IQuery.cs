using MediatR;

namespace WebApi.CommandAndQueries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}