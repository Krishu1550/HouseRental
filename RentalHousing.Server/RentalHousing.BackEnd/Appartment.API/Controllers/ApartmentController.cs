using Appartment.Application.DTOs;
using Appartment.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Appartment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment([FromBody] CreateApartmentDto dto)
        {
            var apartment = await _apartmentService.AddApartmentAsync(dto);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.Id }, apartment);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetApartmentById(int id)
        {
            var apartment = await _apartmentService.GetByIdAsync(id);
            if (apartment == null)
                return NotFound();
            return Ok(apartment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            var apartments = await _apartmentService.GetAllAsync();
            return Ok(apartments);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetApartmentsByOwner(string ownerId)
        {
            var apartments = await _apartmentService.GetByOwnerIdAsync(ownerId);
            return Ok(apartments);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchApartments([FromQuery] string? city, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? bedrooms)
        {
            var apartments = await _apartmentService.SearchAsync(city ?? "", minPrice, maxPrice, bedrooms);
            return Ok(apartments);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] UpdateApartmentDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            await _apartmentService.UpdateApartmentAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            await _apartmentService.DeleteApartmentAsync(id);
            return NoContent();
        }
    }
}
