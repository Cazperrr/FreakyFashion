using FreakyFashion.Data;
using FreakyFashion.DTOs;
using FreakyFashion.Models;
using FreakyFashion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashion.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AppDbContext _context;
        private readonly ISlugService _slugService;
        public ProductsService(AppDbContext context, ISlugService slugService)
        {
            _context = context;
            _slugService = slugService;
        }

        public async Task<ProductsDTO> CreateProduct(CreateProductDTO productDTO)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDTO.CategoryId);

            if (!categoryExists)
                return null;

            var product = new Products
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Image = productDTO.Image,
                UrlSlug = await GenerateUniqueProductSlug(productDTO.Name),
                CategoryId = productDTO.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductsDTO(
                Id: product.Id,
                Name: product.Name,
                Description: product.Description,
                Price: product.Price,
                Image: product.Image,
                UrlSlug: product.UrlSlug
            );
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductsDTO?> GetProductById(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return new ProductsDTO(
                Id: product.Id,
                Name: product.Name,
                Description: product.Description,
                Price: product.Price,
                Image: product.Image,
                UrlSlug: product.UrlSlug
            );
        }

        public async Task<ProductsDTO> GetProductBySlug(string slug)
        {
            var response = await _context.Products.FirstOrDefaultAsync(p => p.UrlSlug == slug.ToLower());

            if (response == null)
                return null;

            return new ProductsDTO(
                Id: response.Id,
                Name: response.Name,
                Description: response.Description,
                Price: response.Price,
                Image: response.Image,
                UrlSlug: response.UrlSlug);
        }

        public async Task<List<ProductsDTO>> GetProducts()
        {
            var response = await _context.Products.Select(p => new ProductsDTO(
                Id: p.Id,
                Name: p.Name,
                Description: p.Description,
                Price: p.Price,
                Image: p.Image,
                UrlSlug: p.UrlSlug
            )).ToListAsync();

            return await Task.FromResult(response);
        }

        public async Task<string> GenerateUniqueProductSlug(string name)
        {
            string baseSlug = _slugService.GenerateSlug(name);
            string uniqueSlug = baseSlug;
            int counter = 1;

            while (await _context.Products.AnyAsync(p => p.UrlSlug == uniqueSlug))
            {
                uniqueSlug = $"{baseSlug}-{counter}";
                counter++;
            }
            return uniqueSlug;
        }
    }
}
