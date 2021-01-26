using System.Collections.Generic;

using WebApi.BusinessLogic;

namespace WebApi.CommandAndQueries
{
    public record ListTodaysOrders : IQuery<List<Order>>
    {
        public int CustomerID { get; set; }
    }
}