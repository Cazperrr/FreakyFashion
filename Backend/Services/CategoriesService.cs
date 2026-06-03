using Azure;
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

        public async Task<List<CategoriesDTO>> GetCategories()
        {
            var response = await _context.Categories
                .Select(c => new CategoriesDTO(
                Id: c.Id,
                Name: c.Name,
                Image: c.Image,
                UrlSlug: c.UrlSlug,
                Products: c.Products.Select(p => new ProductsDTO(
                    Id: p.Id,
                    Name: p.Name,
                    Description: p.Description,
                    Price: p.Price,
                    Image: p.Image,
                    UrlSlug: p.UrlSlug
                    )).ToList()
            )).ToListAsync();

            return response;
        }

        public async Task<CategoriesDTO?> GetCategoryById(int id)
        {
            var response = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (response == null)
                return null;

            return new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products.Select(p => new ProductsDTO(
                    Id: p.Id,
                    Name: p.Name,
                    Description: p.Description,
                    Price: p.Price,
                    Image: p.Image,
                    UrlSlug: p.UrlSlug
                    )).ToList()
            );
        }
        public async Task<CategoriesDTO> GetCategoryBySlug(string slug)
        {
            var response = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UrlSlug == slug.ToLower());

            if (response == null)
                return null;

            return new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products.Select(p => new ProductsDTO(
                    Id: p.Id,
                    Name: p.Name,
                    Description: p.Description,
                    Price: p.Price,
                    Image: p.Image,
                    UrlSlug: p.UrlSlug
                    )).ToList()
            );
        }

        public async Task<CategoriesDTO> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            var category = new Categories
            {
                Name = categoryDTO.Name,
                Image = categoryDTO.Image,
                UrlSlug = await GenerateUniqueCategorySlug(categoryDTO.Name)
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoriesDTO(
                Id: category.Id,
                Name: category.Name,
                Image: category.Image,
                UrlSlug: category.UrlSlug,
                Products: []
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
