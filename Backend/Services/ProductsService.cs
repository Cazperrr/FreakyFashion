using FreakyFashion.DTOs;
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

        public Task<ProductsDTO> CreateProduct(CreateProductDTO product)
        {
            var newProduct = new Products
            {
                Id = products.Count + 1,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                UrlSlug = product.UrlSlug,
                CategoryId = product.CategoryId
            };

            products.Add(newProduct);

            var response = new ProductsDTO(
                Id: newProduct.Id,
                Name: newProduct.Name,
                Description: newProduct.Description,
                Price: newProduct.Price,
                Image: newProduct.Image,
                UrlSlug: newProduct.UrlSlug
            );

            return Task.FromResult(response);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            products.Remove(product);

            return true;
        }

        public async Task<ProductsDTO?> GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return null;

            var productDTO = new ProductsDTO(
                Id: product.Id,
                Name: product.Name,
                Description: product.Description,
                Price: product.Price,
                Image: product.Image,
                UrlSlug: product.UrlSlug
            );

            return await Task.FromResult(productDTO);
        }

        public async Task<List<ProductsDTO>> GetProductBySlug(string slug)
        {
            var result = products
                .Where(p => p.UrlSlug == slug)
                .Select(result => new ProductsDTO(
                    Id: result.Id,
                    Name: result.Name,
                    Description: result.Description,
                    Price: result.Price,
                    Image: result.Image,
                    UrlSlug: result.UrlSlug
                )).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<ProductsDTO>> GetProducts()
        {
            var response = products.Select(x => new ProductsDTO(

                Id: x.Id,
                Name: x.Name,
                Description: x.Description,
                Price: x.Price,
                Image: x.Image,
                UrlSlug: x.UrlSlug
            )).ToList();

            return await Task.FromResult(response);
        }
    }
}
