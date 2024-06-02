using Microsoft.AspNetCore.Mvc;
using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Application.Helpers;
using WakeCommerceProject.Application.Interfaces;
using WakeCommerceProject.Application.Mappings;

namespace WakeCommerceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryObject query)
        {
            // Retrieve products from the repository
            var products = await _productRepository.GetAllAsync(query);

            // Convert each product to its corresponding DTO
            var productDTO = products.Select(s => s.ToProductDTO());

            return Ok(productDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                // If the product is not found, return a 404 Not Found response
                return NotFound();
            }

            return Ok(product.ToProductDTO());
        }



        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequestDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return a BadRequest response
                return BadRequest(ModelState);
            }
                
            // Convert the productDTO to a product model
            var productModel = productDTO.ToProductFromCreateDTO();

            // Create the product asynchronously in the repository
            await _productRepository.CreateAsync(productModel);
            
            // Return a 201 Created response with the newly created product's DTO
            return CreatedAtAction(nameof(GetByIdAsync), new { id = productModel.Id }, productModel.ToProductDTO());
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateProductRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return a BadRequest response
                return BadRequest(ModelState);
            }
            
            // Update the product asynchronously in the repository
            var productModel = await _productRepository.UpdateAsync(id, updateDTO);

            if (productModel == null)
            {
                // If the product is not found, return a 404 Not Found response
                return NotFound();
            }

            return Ok(productModel.ToProductDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            
            var productModel = await _productRepository.DeleteAsync(id);

            if (productModel == null)
            {
                // If the product is not found, return a 404 Not Found response
                return NotFound();
            }

            // Return a 204 No Content status (indicating successful deletion)
            return NoContent();
        }
    }
}
