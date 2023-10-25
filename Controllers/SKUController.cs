using Microsoft.AspNetCore.Mvc;
using TexCode.Authorization;
using TexCode.Entities;
using TexCode.Services;

namespace TexCode.Controllers
{
    [ApiController]
    [Route("api/skus")]
    public class SKUController : BaseController
    {
        private readonly SKUService _skuService;

        public SKUController(SKUService skuService)
        {
            _skuService = skuService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateSKU(SKU sku)
        {
            try
            {
                await _skuService.CreateSKUAsync(sku);
                return Ok("SKU created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("janCode")]
        public async Task<IActionResult> GetSKU(string janCode)
        {
            try
            {
                var sku = await _skuService.GetSKUByJanCodeAsync(janCode);
                if (sku == null)
                {
                    return NotFound("SKU not found");
                }
                return Ok(sku);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllSKUs()
        {
            try
            {
                var skus = await _skuService.GetAllSKUsAsync();
                return Ok(skus);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("janCode")]
        public async Task<IActionResult> UpdateSKU(string janCode, SKU updatedSKU)
        {
            try
            {
                updatedSKU.JanCode = janCode;
                await _skuService.UpdateSKUAsync(updatedSKU);
                return Ok("SKU updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("janCode")]
        public async Task<IActionResult> DeleteSKU(string janCode)
        {
            try
            {
                await _skuService.DeleteSKUAsync(janCode);
                return Ok("SKU deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
