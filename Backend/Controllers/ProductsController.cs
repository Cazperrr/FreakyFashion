using FreakyFashion.Models;
using FreakyFashion.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
            => Ok(await service.GetProducts());

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            var product = await service.GetProductById(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }
    }
}
