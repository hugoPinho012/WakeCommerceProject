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

            var products = await _productRepository.GetAllAsync(query);
            var productDTO = products.Select(s => s.ToProductDTO());

            return Ok(productDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var stock = await _productRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToProductDTO());
        }



        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequestDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = productDTO.ToProductFromCreateDTO();
            await _productRepository.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = productModel.Id }, productModel.ToProductDTO());
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateProductRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepository.UpdateAsync(id, updateDTO);

            if (productModel == null)
            {
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
                return NotFound();
            }

            return NoContent();
        }
    }
}
