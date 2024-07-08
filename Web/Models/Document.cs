using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Document
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public List<string> Tags { get; set; } = null!;

        [Required]
        public object Data { get; set; } = null!;
    }
}
