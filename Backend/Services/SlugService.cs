using FreakyFashion.Services.Interfaces;
using System.Text.RegularExpressions;

namespace FreakyFashion.Services
{
    public class SlugService : ISlugService
    {
        public string GenerateSlug(string input)
        {
            string slug = input.ToLower().Trim();

            slug = slug
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("ö", "o");

            // Remove special characters
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Remove spaces and replace with hyphens
            slug = Regex.Replace(slug, @"\s+", "-");

            // Remove multiple hyphens
            slug = Regex.Replace(slug, @"-+", "-");

            return slug;
        }
    }
}
