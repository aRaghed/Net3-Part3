
using WebApi.BusinessLogic;

namespace WebApi.CommandAndQueries
{
    public record GetOrder : IQuery<Order>
    {
        public int OrderId { get; set; }
    }
}