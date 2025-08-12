using Appartment.Application.DTOs;
using Appartment.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appartment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestApartmentController : ControllerBase
    {
        private readonly IRequestApartmentService _requestService;

        public RequestApartmentController(IRequestApartmentService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestApartmentDto dto)
        {
            var result = await _requestService.CreateRequestAsync(dto);
            if (!result)
                return BadRequest("Failed to create request");

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound();

            return Ok(request);
        }

        [HttpGet("apartment/{apartmentId:int}")]
        public async Task<IActionResult> GetRequestsByApartment(int apartmentId)
        {
            var requests = await _requestService.GetRequestsByApartmentIdAsync(apartmentId);
            return Ok(requests);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRequestsByUser(string userId)
        {
            var requests = await _requestService.GetRequestsByUserIdAsync(userId);
            return Ok(requests);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] UpdateRequestApartmentDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            var result = await _requestService.UpdateRequestAsync(dto);
            if (!result)
                return BadRequest("Failed to update request");

            return NoContent();
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateRequestStatus(int id, [FromQuery] string status)
        {
            var result = await _requestService.UpdateStatusAsync(id, status);
            if (!result)
                return BadRequest("Invalid status or update failed");

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _requestService.DeleteRequestAsync(id);
            if (!result)
                return BadRequest("Failed to delete request");

            return NoContent();
        }

    }
}
