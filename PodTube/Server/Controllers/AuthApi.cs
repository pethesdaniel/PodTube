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
        private UserManager<User> _userManager;
        private PodTubeDbContext _context;
        private TokenService _tokenService;

        public AuthApi(UserManager<User> userManager, PodTubeDbContext context, TokenService tokenService) {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequestBody request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _userManager.CreateAsync(
                new User { UserName = request.Username, Email = request.Email },
                request.Password
            );
            if (result.Succeeded) {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }
            foreach (var error in result.Errors) {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestBody request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (managedUser == null) {
                return BadRequest("Bad credentials");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid) {
                return BadRequest("Bad credentials");
            }
            var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (userInDb is null)
                return Unauthorized();
            var accessToken = _tokenService.CreateToken(userInDb);
            await _context.SaveChangesAsync();
            return Ok(new AuthResponseDTO {
                Id = userInDb.Id,
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken,
            });
        }
    }
}
