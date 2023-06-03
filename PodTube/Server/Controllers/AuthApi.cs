using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodTube.BLL.Services;
using PodTube.DataAccess.Contexts;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;

namespace PodTube.Server.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthApi : ControllerBase {


        private UserService _userService;

        public AuthApi(UserService userService) {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequestBody request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request.Username, request.Email, request.Password);
            if (result != null && result.Succeeded) {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }
            if(result != null) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestBody request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var accessToken = await _userService.Authorize(request.Email, request.Password);
                return Ok(new AuthResponseDTO {
                    Email = request.Email,
                    Token = accessToken,
                });
            } catch (ArgumentException) {
                return BadRequest("Bad credentials");
            }
        }
    }
}
