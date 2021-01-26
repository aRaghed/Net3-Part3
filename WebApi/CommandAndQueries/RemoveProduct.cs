using System.ComponentModel.DataAnnotations;

namespace WebApi.CommandAndQueries
{
    public record RemoveProduct : ICommand<string>
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}