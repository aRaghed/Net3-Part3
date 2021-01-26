using System;
using System.Collections.Generic;

namespace WebApi.BusinessLogic
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<OrderLine> OrderLines { get; set; }

        public Decimal OrderTotal { get; set; }
    }
}