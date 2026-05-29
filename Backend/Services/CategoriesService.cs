using FreakyFashion.Data;
using FreakyFashion.DTOs;
using FreakyFashion.Models;
using FreakyFashion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashion.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _context;
        private readonly ISlugService _slugService;

        public CategoriesService(AppDbContext context, ISlugService slugService)
        {
            _context = context;
            _slugService = slugService;
        }

        //static List<Categories> categories = new List<Categories>
        //{
        //    new Categories { Id = 1, Name = "T-Shirts", Image = "tshirt.jpg", UrlSlug = "t-shirts" },
        //    new Categories { Id = 2, Name = "Pants", Image = "pants.jpg", UrlSlug = "pants" },
        //    new Categories { Id = 3, Name = "Shoes", Image = "shoes.jpg", UrlSlug = "shoes" },
        //    new Categories { Id = 4, Name = "Hoodies", Image = "hoodies.jpg", UrlSlug = "hoodies" }
        //};

        public async Task<List<CategoriesDTO>> GetCategories()
        {
            var response = await _context.Categories.Select(c => new CategoriesDTO(
                Id: c.Id,
                Name: c.Name,
                Image: c.Image,
                UrlSlug: c.UrlSlug,
                Products: c.Products
            )).ToListAsync();

            return await Task.FromResult(response);
        }

        public async Task<CategoriesDTO?> GetCategoryById(int id)
        {
            var response = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (response == null)
                return null;

            return new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products
            );
        }
        public async Task<CategoriesDTO> GetCategoryBySlug(string slug)
        {
            // TODO: Handle case sensitivity and ensure that the slug is unique for each product
            var response = await _context.Categories.FirstOrDefaultAsync(c => c.UrlSlug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            if (response == null)
                return null;

            return new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products
            );
        }

        public async Task<CategoriesDTO> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            var category = new Categories
            {
                Name = categoryDTO.Name,
                Image = categoryDTO.Image
                // TODO: Generate UrlSlug from Name, e.g., by replacing spaces with hyphens and converting to lowercase
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoriesDTO(
                Id: category.Id,
                Name: category.Name,
                Image: category.Image,
                UrlSlug: category.UrlSlug,
                Products: category.Products
            );
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GenerateUniqueCategorySlug(string name)
        {
            string baseSlug = _slugService.GenerateSlug(name);
            string slug = baseSlug;
            int counter = 1;

            while (await _context.Categories.AnyAsync(c => c.UrlSlug == slug))
            {
                slug = $"{baseSlug}-{counter}";
                counter++;
            }

            return slug;
        }

    }
}
