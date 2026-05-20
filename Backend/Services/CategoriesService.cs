using FreakyFashion.DTOs;
using FreakyFashion.Models;
using FreakyFashion.Services.Interfaces;

namespace FreakyFashion.Services
{
    public class CategoriesService : ICategoriesService
    {
        static List<Categories> categories = new List<Categories>
        {
            new Categories { Id = 1, Name = "T-Shirts", Image = "tshirt.jpg", UrlSlug = "t-shirts" },
            new Categories { Id = 2, Name = "Pants", Image = "pants.jpg", UrlSlug = "pants" },
            new Categories { Id = 3, Name = "Shoes", Image = "shoes.jpg", UrlSlug = "shoes" },
            new Categories { Id = 4, Name = "Hoodies", Image = "hoodies.jpg", UrlSlug = "hoodies" }
        };

        public async Task<List<CategoriesDTO>> GetCategories()
        {
            var response = categories.Select(x => new CategoriesDTO(
                Id: x.Id,
                Name: x.Name,
                Image: x.Image,
                UrlSlug: x.UrlSlug,
                Products: x.Products
                )).ToList();

            return await Task.FromResult(response);
        }

        public async Task<CategoriesDTO?> GetCategoryById(int id)
        {
            var response = categories.FirstOrDefault(x => x.Id == id);

            if (response == null)
                return null;

            var categoryDto = new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products
            );

            return await Task.FromResult(categoryDto);
        }
        public async Task<CategoriesDTO> GetCategoryBySlug(string slug)
        {
            // TODO: Handle case sensitivity and ensure that the slug is unique for each product
            var response = categories.FirstOrDefault(c => c.UrlSlug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            if (response == null)
                return null;

            var categoryDto = new CategoriesDTO(
                Id: response.Id,
                Name: response.Name,
                Image: response.Image,
                UrlSlug: response.UrlSlug,
                Products: response.Products
            );

            return await Task.FromResult(categoryDto);
        }

        public async Task<CategoriesDTO> CreateCategory(CreateCategoryDTO category)
        {
            // TODO: Change for database implementation
            var newCategory = new Categories
            {
                Id = categories.Count + 1,
                Name = category.Name,
                Image = category.Image
            };

            categories.Add(newCategory);

            var categoryDto = new CategoriesDTO(
                Id: newCategory.Id,
                Name: newCategory.Name,
                Image: newCategory.Image,
                UrlSlug: newCategory.UrlSlug,
                Products: newCategory.Products
            );

            return await Task.FromResult(categoryDto);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return false;

            categories.Remove(category);
            return await Task.FromResult(true);
        }



    }
}
