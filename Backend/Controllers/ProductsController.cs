using FreakyFashion.DTOs;
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
        public async Task<ActionResult<List<ProductsDTO>>> GetProducts()
        {
            var products = await service.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDTO>> GetProductById(int id)
        {
            var product = await service.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("slug={slug}")]
        public async Task<ActionResult<List<ProductsDTO>>> GetProductBySlug(string slug)
        {
            var product = await service.GetProductBySlug(slug);

            if (product == null)
                return NotFound($"Product with slug '{slug}' not found.");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductsDTO>> CreateProduct(CreateProductDTO productDTO)
        {
            var createdProduct = await service.CreateProduct(productDTO);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = createdProduct.Id },
                createdProduct
                );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await service.DeleteProduct(id);

            if (result == false)
                return NotFound();

            return NoContent();
        }
    }
}
