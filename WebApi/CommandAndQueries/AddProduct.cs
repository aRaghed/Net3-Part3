using System.ComponentModel.DataAnnotations;

namespace WebApi.CommandAndQueries
{
    public record AddProduct : ICommand<string>
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}