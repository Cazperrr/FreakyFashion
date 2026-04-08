using System.Runtime.CompilerServices;

namespace FreakyFashion.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Image { get; set; }
        public string? UrlSlug { get; set; }
        public List<Products>? Products { get; set; }
    }
}
