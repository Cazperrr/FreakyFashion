using FreakyFashion.DTOs;

namespace FreakyFashion.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductsDTO>> GetProducts();
        Task<ProductsDTO?> GetProductById(int id);
        Task<List<ProductsDTO>> GetProductBySlug(string slug);
        Task<ProductsDTO> CreateProduct(CreateProductDTO product);
        Task<bool> DeleteProduct(int id);
    }
}
