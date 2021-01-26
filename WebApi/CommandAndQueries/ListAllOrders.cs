using System.Collections.Generic;

using WebApi.BusinessLogic;

namespace WebApi.CommandAndQueries
{
    public record ListAllOrders : IQuery<List<Order>>
    {
    }
}