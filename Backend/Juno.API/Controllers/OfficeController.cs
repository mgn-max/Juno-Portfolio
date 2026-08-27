using Juno.API.Mapper.OfficeMapper;
using Juno.API.Models.OfficeRequests;
using Juno.Application.DTOs.OfficeDtos;
using Juno.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Juno.API.Controllers
{
    [ApiController]
    [Route("api/offices")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<ActionResult<OfficeDetailsDto>> CreateOffice([FromBody] CreateOfficeRequest request)
        {
            var currentUserId = GetCurrentUserId();

            var result = await _officeService.CreateOffice(
                currentUserId,
                request.ToDto()
            );

            return CreatedAtAction(nameof(GetOfficeDetailsById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeDto>> GetOfficeById(Guid id)
        {
            var result = await _officeService.GetOfficeById(id);
            return Ok(result);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<OfficeDetailsDto>> GetOfficeDetailsById(Guid id)
        {
            var result = await _officeService.GetOfficeDetailsById(id);
            return Ok(result);
        }
        [HttpPut("{id}/basic-info")]
        public async Task<ActionResult> UpdateBasicInfo(Guid id, [FromBody] UpdateOfficeBasicInfoRequest request)
        {
            await _officeService.UpdateBasicInfo(id, request.ToDto());
            return NoContent();
        }

        [HttpPatch("{id}/address")]
        public async Task<ActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressRequest request)
        {
            await _officeService.UpdateAddress(id, request.ToDto());
            return NoContent();
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return Guid.Parse(userIdClaim);
        }
    }
}
