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

namespace PodTube.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/channel")]
    public class ChannelApiController : ControllerBase {
        private ChannelService _channelService;
        public ChannelApiController(ChannelService channelService) : base() {
            _channelService = channelService;
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

                return new ObjectResult(result);
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

                return new ObjectResult(result);
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
                return new ObjectResult(result);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        ///// <summary>
        ///// Create a new channel
        ///// </summary>
        ///// <param name="body"></param>
        ///// <response code="200">Successful operation</response>
        ///// <response code="405">Invalid input</response>
        //[HttpPost]
        //[Route("/channel")]
        //[ValidateModelState]
        //[SwaggerOperation(OperationId = "ChannelPost")]
        //[SwaggerResponse(statusCode: 200, type: typeof(ChannelDto), description: "Successful operation")]
        //public virtual IActionResult ChannelPost([FromBody]ChannelRequestBody body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(ChannelInfoWithOwner));

        //    //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(405);
        //    string exampleJson = null;
        //    exampleJson = "\"\"";
            
        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<ChannelInfoWithOwner>(exampleJson)
        //                : default(ChannelInfoWithOwner);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}
    }
}
