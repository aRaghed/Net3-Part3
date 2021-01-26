using System.ComponentModel.DataAnnotations;

namespace WebApi.CommandAndQueries
{
    public record CreateOrder : ICommand<string>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}