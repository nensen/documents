namespace Core.Models
{
    public class Document
    {
        public string Id { get; set; } = null!;

        public List<string> Tags { get; set; } = null!;

        public string Data { get; set; } = null!;
    }
}