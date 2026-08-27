using Juno.API.Models.OfficeMembershipRequests;
using Juno.Application.DTOs.OfficeMembershipDtos;
using Juno.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juno.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/office-memberships")]
    public class OfficeMembershipController : ControllerBase
    {
        private readonly IOfficeMembershipService _officeMembershipService;

        public OfficeMembershipController(IOfficeMembershipService officeMembershipService)
        {
            _officeMembershipService = officeMembershipService;
        }

        [HttpPost]
        public async Task<ActionResult<OfficeMembershipDetailsDto>> CreateMembership([FromBody] CreateMembershipRequest request)
        {
            var currentUserId = GetCurrentUserId();

            var requesterMembership = await _officeMembershipService
                .GetMembershipByUserAndOffice(currentUserId, request.OfficeId);

            var membership = await _officeMembershipService.CreateMembership(
                request.UserId,
                request.OfficeId,
                request.Profile,
                requesterMembership.Profile
            );

            return CreatedAtAction(nameof(GetMembershipDetails), new { id = membership.Id }, membership);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeMembershipDto>> GetMembership(Guid id)
        {
            var membership = await _officeMembershipService.GetMembershipById(id);
            return Ok(membership);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<OfficeMembershipDetailsDto>> GetMembershipDetails(Guid id)
        {
            var membership = await _officeMembershipService.GetMembershipDetailsById(id);
            return Ok(membership);
        }

        [HttpGet("users/{userId}/offices/{officeId}")]
        public async Task<ActionResult<OfficeMembershipDto>> GetMembershipByUserAndOffice(Guid userId, Guid officeId)
        {
            var membership = await _officeMembershipService.GetMembershipByUserAndOffice(userId, officeId);
            return Ok(membership);
        }

        [HttpGet("offices/{officeId}")]
        public async Task<ActionResult<List<OfficeMembershipDto>>> GetMembershipsByOffice(Guid officeId)
        {
            var memberships = await _officeMembershipService.GetMembershipsByOfficeId(officeId);
            return Ok(memberships);
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<List<OfficeMembershipDto>>> GetMembershipsByUser(Guid userId)
        {
            var memberships = await _officeMembershipService.GetMembershipsByUserId(userId);
            return Ok(memberships);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateMembership(Guid id, [FromBody] UpdateMembershipRequest request)
        {
            var currentUserId = GetCurrentUserId();

            var targetMembership = await _officeMembershipService.GetMembershipDetailsById(id);

            var requesterMembership = await _officeMembershipService
                .GetMembershipByUserAndOffice(currentUserId, targetMembership.OfficeId);

            await _officeMembershipService.UpdateMembership(
                id,
                request.Profile,
                request.Status,
                requesterMembership.Profile
            );

            return NoContent();
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return Guid.Parse(userIdClaim);
        }
    }
}