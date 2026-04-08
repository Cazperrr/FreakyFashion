using System.ComponentModel.DataAnnotations;

namespace FreakyFashion.Models
{
    public class Products
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? UrlSlug { get; set; }
        public int CategoryId { get; set; }
    }
}
