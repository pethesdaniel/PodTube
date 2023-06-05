using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PodTube.Attributes;

using Microsoft.AspNetCore.Authorization;
using PodTube.BLL.Services;
using PodTube.Shared.Models.DTO;
using Microsoft.AspNetCore.Identity;
using PodTube.DataAccess.Entities;

namespace PodTube.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserApiController : ControllerBase
    {
        private UserService _userService;
        private UserManager<User> _userManager;
        public UserApiController(UserService userService, UserManager<User> userManager) : base() {
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Get information of a User
        /// </summary>
        /// <param name="userId">ID of User to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Video not found</response>
        /// <response code="405">Validation exception</response>
        [HttpGet("{userId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetUserById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute][Required]long userId)
        {
            var result = await _userService.GetUserById(userId);

            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get information of logged in user
        /// </summary>
        /// <param name="userId">ID of User to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Video not found</response>
        /// <response code="405">Validation exception</response>
        [Authorize]
        [HttpGet("me")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetUserSelf")]
        [SwaggerResponse(statusCode: 200, type: typeof(UserDto), description: "Successful operation")]
        public async Task<ActionResult<UserDto>> GetUserSelf() {
            var user = await _userManager.GetUserAsync(User);
            return await GetUserById(user?.Id ?? 0);
        }
    }
}
