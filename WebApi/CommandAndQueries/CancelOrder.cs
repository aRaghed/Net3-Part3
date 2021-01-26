using System.ComponentModel.DataAnnotations;

namespace WebApi.CommandAndQueries
{
    public record CancelOrder : ICommand<string>
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string CancelledBy { get; set; }
    }
}