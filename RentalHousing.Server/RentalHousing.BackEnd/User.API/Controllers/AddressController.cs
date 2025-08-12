using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOs;
using User.Application.Interface;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;

        }


        [HttpGet("GetAddressByUserId/{userId}")]
        public async Task<ActionResult<List<AddressDto>>> GetAddressByUserId([FromRoute] Guid userId)
        {
            var result = await _addressService.GetAddressByUserId(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("GetAddressById/{addressId}")]
        public async Task<ActionResult<AddressDto>> GetAddressById([FromRoute] Guid addressId)
        {
            var result = await _addressService.GetAddressById(addressId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllAddresses")]
        public async Task<ActionResult<List<AddressDto>>> GetAllAddresses()
        {
            var result = await _addressService.GetAllAddresses();
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("CreateAddress/{userId}")]
        public async Task<ActionResult<AddressDto>> CreateAddress([FromBody] AddressDto addressDto, [FromRoute] Guid userId)
        {
            if (addressDto == null)
            {
                return BadRequest("Address data is null");
            }
            await _addressService.CreateAddressAsync(addressDto, userId);
            return CreatedAtAction(nameof(GetAddressById), new { addressId = addressDto.UserId }, addressDto);
        }

        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest("Address data is null");
            }
            var updatedAddress = await _addressService.UpdateAddress(addressDto);
            if (updatedAddress == null)
            {
                return NotFound();
            }
            return Ok(updatedAddress);
        }

        [HttpDelete("DeleteAddressById/{addressId}/{userId}")]
        public async Task<IActionResult> DeleteAddressById([FromRoute] Guid addressId, [FromRoute] Guid userId)
        {
            if (addressId == Guid.Empty || userId == Guid.Empty)
            {
                return BadRequest("Invalid address or user ID");
            }
            await _addressService.DeleteAddressById(addressId, userId);
            return NoContent();
        }

    }
}
