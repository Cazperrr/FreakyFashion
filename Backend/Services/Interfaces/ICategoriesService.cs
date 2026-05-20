using FreakyFashion.DTOs;

namespace FreakyFashion.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoriesDTO>> GetCategories();
        Task<CategoriesDTO?> GetCategoryById(int id);
        Task<CategoriesDTO?> GetCategoryBySlug(string slug);
        Task<CategoriesDTO> CreateCategory(CreateCategoryDTO category);
        Task<bool> DeleteCategory(int id);
    }
}
