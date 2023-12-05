using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using PodTube.Attributes;

using Microsoft.AspNetCore.Authorization;
using PodTube.BLL.Services;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using System.Text.Json;

namespace PodTube.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/video")]
    public class VideoApiController : ControllerBase
    {
        private VideoService _videoService;
        public VideoApiController(VideoService videoService) : base() {
            _videoService = videoService;
        }

        ///// <summary>
        ///// Get information and content of a video
        ///// </summary>
        ///// <param name="videoId">ID of video to return</param>
        ///// <response code="200">Successful operation</response>
        ///// <response code="400">Invalid ID supplied</response>
        ///// <response code="404">Video not found</response>
        ///// <response code="405">Validation exception</response>
        [HttpGet("{videoId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetVideoById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<VideoDto>> GetVideoById([FromRoute][Required]long videoId)
        {
            var result = await _videoService.GetVideoById(videoId);

            if(result == null) {
                return StatusCode(404);
            }

            return new ObjectResult(result);
        }


        /// <summary>
        /// Upload a video
        /// </summary>
        /// <param name="metadata"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [Authorize]
        [HttpPost("upload")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "PostUploadVideo")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult> PostUploadVideo([FromBody][Required] VideoUploadRequestBody metadata) {
            var success = _videoService.UploadVideo(metadata);
            if (!success) {
                return StatusCode(400);
            }
            return StatusCode(200);
        }

        /// <summary>
        /// Create a new playlist
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "PostCreatePlaylist")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<PlaylistDto>> PostCreatePlaylist([FromBody][Required] VideoUploadRequestBody body) {
            return StatusCode(200);
        }
    }
}
