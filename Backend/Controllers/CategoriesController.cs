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
    }
}
