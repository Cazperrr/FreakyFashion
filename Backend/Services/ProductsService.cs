using FreakyFashion.Models;
using FreakyFashion.Services.Interfaces;

namespace FreakyFashion.Services
{
    public class ProductsService : IProductsService
    {
        static List<Products> products = new List<Products>
        {
            new Products { Id = 1, Name = "T-Shirt", Description = "A comfortable t-shirt", Price = 19.99m, Image = "tshirt.jpg", UrlSlug = "t-shirt", CategoryId = 1 },
            new Products { Id = 2, Name = "Jeans", Description = "Stylish jeans", Price = 49.99m, Image = "jeans.jpg", UrlSlug = "jeans", CategoryId = 2 },
            new Products { Id = 3, Name = "Sneakers", Description = "Trendy sneakers", Price = 89.99m, Image = "sneakers.jpg", UrlSlug = "sneakers", CategoryId = 3 },
            new Products { Id = 4, Name = "Hoodie", Description = "A warm Hoodie", Price = 89.99m, Image = "hoodie.jpg", UrlSlug = "hoodie", CategoryId = 4 }
        };

        public Task<Products> CreateProduct(Products product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Products?> GetProductById(int id)
        {
            var result = products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(result);
        }

        public Task<Products?> GetProductBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Products>> GetProducts()
            => await Task.FromResult(products);
    }
}
