using FreakyFashion.DTOs;
using FreakyFashion.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoriesService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoriesDTO>>> GetCategories()
        {
            var categories = await service.GetCategories();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesDTO>> GetCategoryById(int id)
        {
            var category = await service.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("slug={slug}")]
        public async Task<ActionResult<CategoriesDTO>> GetCategoryBySlug(string slug)
        {
            var category = await service.GetCategoryBySlug(slug);

            if (category == null)
                return NotFound($"Category with slug '{slug}' not found.");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriesDTO>> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            var createdCategory = await service.CreateCategory(categoryDTO);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = createdCategory.Id },
                createdCategory
                );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await service.DeleteCategory(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
