using MediatR;

namespace WebApi.CommandAndQueries
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}