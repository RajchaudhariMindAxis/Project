using CompanyPlatform.Core.DTOs.CRM;
using CompanyPlatform.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPlatform.API.Controllers.CRM
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAccessoryPriceController : ControllerBase
    {
        private readonly IProductAccessoryPriceService _service;

        public ProductAccessoryPriceController(
            IProductAccessoryPriceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Product accessory price not found."
                });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ProductAccessoryPriceDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProductAccessoryPriceDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);

                if (!updated)
                    return NotFound(new
                    {
                        message = "Product accessory price not found."
                    });

                return Ok(new
                {
                    message = "Product accessory price updated successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new
                {
                    message = "Product accessory price not found."
                });

            return Ok(new
            {
                message = "Product accessory price deleted successfully."
            });
        }
    }
}