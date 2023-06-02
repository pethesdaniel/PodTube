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
        ///// Deletes a video
        ///// </summary>
        ///// <param name="videoId">ID of video to return</param>
        ///// <response code="200">Successful operation</response>
        ///// <response code="404">Video not found</response>
        ///// <response code="405">Validation exception</response>
        //[HttpDelete]
        //[Route("/video/{videoId}")]
        //[ValidateModelState]
        //[SwaggerOperation(OperationId = "VideoVideoIdDelete")]
        //public virtual IActionResult VideoVideoIdDelete([FromRoute][Required]long? videoId)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200);

        //    //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(404);

        //    //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(405);

        //    throw new NotImplementedException();
        //}

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

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "PostUploadVideo")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<VideoDto>> PostUploadVideo([FromForm][Required] string metadata, [FromForm][Required] List<IFormFile> files) {
            var metadataDto = JsonSerializer.Deserialize<VideoRequestBody>(metadata);
            var success = _videoService.UploadVideo(metadataDto!, files);
            if (!success) {
                return StatusCode(400);
            }
            return StatusCode(200);
        }
    }
}
