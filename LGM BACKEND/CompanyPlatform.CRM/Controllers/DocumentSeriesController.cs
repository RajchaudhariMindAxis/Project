using CompanyPlatform.CRM.DTO.DocumentSeries;
using CompanyPlatform.CRM.Services.DocumentSeries;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPlatform.CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentSeriesController : ControllerBase
    {
        private readonly IDocumentSeriesService _service;

        public DocumentSeriesController(
            IDocumentSeriesService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Document series not found."
                });
            }

            return Ok(result);
        }

        // GENERATE CODE
        [HttpGet("generate-code")]
        public async Task<IActionResult> GenerateCode()
        {
            var code = await _service.GenerateCodeAsync();

            return Ok(new { code });
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] DocumentSeriesDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.DocumentSeriesId },
                    result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] DocumentSeriesDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        message = "Document series not found."
                    });
                }

                return Ok(new
                {
                    message = "Document series updated successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Document series not found."
                });
            }

            return Ok(new
            {
                message = "Document series soft-deleted successfully."
            });
        }
    }
}