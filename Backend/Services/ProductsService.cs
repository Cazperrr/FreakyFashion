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
        public ProductsService(AppDbContext context)
        {
            _context = context;
        }

        //static List<Products> products = new List<Products>
        //{
        //    new Products { Id = 1, Name = "T-Shirt", Description = "A comfortable t-shirt", Price = 19.99m, Image = "tshirt.jpg", UrlSlug = "t-shirt", CategoryId = 1 },
        //    new Products { Id = 2, Name = "Jeans", Description = "Stylish jeans", Price = 49.99m, Image = "jeans.jpg", UrlSlug = "jeans", CategoryId = 2 },
        //    new Products { Id = 3, Name = "Sneakers", Description = "Trendy sneakers", Price = 89.99m, Image = "sneakers.jpg", UrlSlug = "sneakers", CategoryId = 3 },
        //    new Products { Id = 4, Name = "Hoodie", Description = "A warm Hoodie", Price = 89.99m, Image = "hoodie.jpg", UrlSlug = "hoodie", CategoryId = 4 }
        //};

        public async Task<ProductsDTO> CreateProduct(CreateProductDTO productDTO)
        {

            var product = new Products
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Image = productDTO.Image,
                UrlSlug = productDTO.UrlSlug,
                CategoryId = productDTO.CategoryId
                // TODO: Generate UrlSlug from the product name and ensure it's unique
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
            // TODO: Handle case sensitivity and ensure that the slug is unique for each product

            var response = await _context.Products.FirstOrDefaultAsync(p => p.UrlSlug.Equals(slug, StringComparison.OrdinalIgnoreCase));

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
    }
}
