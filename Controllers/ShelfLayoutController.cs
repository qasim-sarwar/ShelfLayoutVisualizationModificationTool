using Microsoft.AspNetCore.Mvc;
using TexCode.Entities;
using TexCode.Services;
using TexCode.ViewModels;
using TexCode.Authorization;

namespace TexCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShelfLayoutController : BaseController
    {
        private IShelfLayoutService _shelfLayoutService;
        public ShelfLayoutController(IShelfLayoutService shelfLayoutService)
        {
            _shelfLayoutService = shelfLayoutService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllShelves()
        {
            var cabinets = _shelfLayoutService.GetCabinets();
            return Ok(cabinets);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetShelfById(int id)
        {
            var cabinets = _shelfLayoutService.GetCabinetById(id);
            return Ok(cabinets);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateShelf(int id, Cabinet model)
        {
            _shelfLayoutService.UpdateCabinet(id, model);
            return Ok(new { message = "Shelf updated successfully" });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteShelf(int id)
        {
            _shelfLayoutService.DeleteCabinet(id);
            return Ok(new { message = "Shelf deleted successfully" });
        }

        [Authorization.AllowAnonymous]
        [HttpPost("createNew")]
        public IActionResult CreateShelf(Cabinet model)
        {
            _shelfLayoutService.CreateCabinet(model);
            return Ok(new { message = "New Shelf Created Successful" });
        }
    }
}