using FreakyFashion.Models;

namespace FreakyFashion.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<Products>> GetProducts();
        Task<Products?> GetProductById(int id);
        Task<Products?> GetProductBySlug(string slug);
        Task<Products> CreateProduct(Products product);
        Task<bool> DeleteProduct(int id);
    }
}
