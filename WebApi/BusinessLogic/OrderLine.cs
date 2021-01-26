using System;

namespace WebApi.BusinessLogic
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Decimal Price { get; set; }
    }
}