using Juno.API.Models.UserRequests;
using Juno.Application.DTOs.UserDtos;
using Juno.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Juno.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDetailsDto>> Me()
        {
            var user = await _userService.GetUserDetailsById(GetLoggedUserId());
            return Ok(user);
        }

        [HttpPatch("me")]
        public async Task<ActionResult> UpdateInfo( [FromBody] UpdateInfoRequest request)
        {
            await _userService.Update(GetLoggedUserId(), request.Name,request.Login,request.Email);
            return NoContent();
        }

        [HttpPatch("me/password")]
        public async Task<ActionResult> UpdatePassword( [FromBody] UpdatePasswordRequest request)
        {
            await _userService.UpdatePasswords(GetLoggedUserId(), request.NewPassword);
            return NoContent();
        }

        [HttpPatch("me/photo")]
        public async Task<ActionResult> UpdatePhotoUrl([FromBody] UpdatePhotoUrlRequest request)
        {
            await _userService.UpdatePhotoUrl(GetLoggedUserId(), request.PhotoUrl);
            return NoContent();
        }


        private Guid GetLoggedUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return Guid.Parse(claim.Value);
        }
    }
}
