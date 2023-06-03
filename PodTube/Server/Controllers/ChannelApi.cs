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
using PodTube.Shared.Models.RequestBody;
using Microsoft.AspNetCore.Identity;
using PodTube.DataAccess.Entities;

namespace PodTube.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/channel")]
    public class ChannelApiController : ControllerBase {
        private ChannelService _channelService;
        private UserService _userService;
        public ChannelApiController(ChannelService channelService, UserService userService) : base() {
            _channelService = channelService;
            _userService = userService;
        }

        /// <summary>
        /// Get metadata of a channel
        /// </summary>
        /// <param name="channelId">ID of channel to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Channel not found</response>
        [HttpGet("{channelId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetChannelById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<ChannelDto>> GetChannelById([FromRoute][Required] long channelId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                var result = await _channelService.GetChannelById(channelId);

                if(result == null) {
                    return NotFound();
                }

                return Ok(result);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// Get videos uploaded to a channel
        /// </summary>
        /// <param name="channelId">ID of channel to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation exception</response>
        /// <response code="404">Channel not found</response>
        [HttpGet("{channelId}/videos")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetChannelVideos")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<VideoPagedListDto>> GetChannelVideos([FromRoute][Required] long channelId, [FromQuery][Required] int page, [FromQuery][Required] int limit) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                var result = await _channelService.GetPagedVideosByChannelId(channelId, page, limit);

                if (result == null) {
                    return NotFound();
                }

                return Ok(result);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a paged list of all channels
        /// </summary>
        /// <param name="page">Number of the currentpage</param>
        /// <param name="limit">Number of channels on a page</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation exception</response>
        [HttpGet]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetChannelsPaged")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<ChannelPagedListDto>> GetChannelsPaged([FromQuery][Required] int page, [FromQuery][Required] int limit) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                var result = await _channelService.GetChannelsPaged(page, limit);
                return Ok(result);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "CreateChannelPost")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<ChannelDto>> CreateChannelPost([Required][FromBody] ChannelRequestBody channelRequest) {
            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                return Ok(await _channelService.CreateChannel(channelRequest, ownerId));
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
