/*
 * PodTube - OpenAPI 3.0
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PodTube.Attributes;

using Microsoft.AspNetCore.Authorization;
using PodTube.Shared.Models;
using PodTube.BLL.Services;

namespace PodTube.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class PlaylistApiController : ControllerBase
    { 
        private PlaylistService PlaylistService { get; set; }

        public PlaylistApiController(PlaylistService playlistService) {
            this.PlaylistService = playlistService;
        }

        /// <summary>
        /// Get a paged list of all playlists
        /// </summary>
        /// <param name="page">Number of the currentpage</param>
        /// <param name="limit">Number of playlists on a page</param>
        /// <response code="200">Successful operation</response>
        /// <response code="405">Validation exception</response>
        [HttpGet]
        [Route("/playlist")]
        [ValidateModelState]
        [SwaggerOperation("PlaylistGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PagedListDto<PlaylistDto>), description: "Successful operation")]
        public virtual IActionResult PlaylistGet([FromQuery][Required()]int page, [FromQuery][Required()]int limit)
        {
            var result =  PlaylistService.GetAllPlaylists(page, limit);
            return new ObjectResult(result.ToJson());
        }

        /*/// <summary>
        /// Deletes a playlist and all of it&#x27;s videos
        /// </summary>
        /// <param name="playlistId">ID of playlist to delete</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Video not found</response>
        /// <response code="405">Validation exception</response>
        [HttpDelete]
        [Route("/playlist/{playlistId}")]
        [ValidateModelState]
        [SwaggerOperation("PlaylistPlaylistIdDelete")]
        public virtual IActionResult PlaylistPlaylistIdDelete([FromRoute][Required]long? playlistId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(405);

            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Get metadata of a playlist
        /// </summary>
        /// <param name="playlistId">ID of playlist to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Playlist not found</response>
        /// <response code="405">Validation exception</response>
        [HttpGet]
        [Route("/playlist/{playlistId}")]
        [ValidateModelState]
        [SwaggerOperation("PlaylistPlaylistIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlaylistDto), description: "Successful operation")]
        public virtual IActionResult PlaylistPlaylistIdGet([FromRoute][Required]long playlistId)
        {
            var result = PlaylistService.GetPlaylistById(playlistId);
            if(result == null) {
                StatusCode(404);
            }
            return new ObjectResult(result!.ToJson());
        }

        /// <summary>
        /// Get videos uploaded to a playlist
        /// </summary>
        /// <param name="playlistId">ID of playlist to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Playlist not found</response>
        /// <response code="405">Validation exception</response>
        [HttpGet]
        [Route("/playlist/{playlistId}/videos")]
        [ValidateModelState]
        [SwaggerOperation("PlaylistPlaylistIdVideosGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PagedListDto<VideoDto>), description: "Successful operation")]
        public virtual IActionResult PlaylistPlaylistIdVideosGet([FromRoute][Required]long playlistId, [FromQuery][Required] int page, [FromQuery][Required] int limit)
        {
            var result = PlaylistService.GetPagedVideosByPlaylistId(playlistId, page, limit);
            if (result == null) {
                StatusCode(404);
            }
            return new ObjectResult(result!.ToJson());
        }

        /*/// <summary>
        /// Create a new playlist
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("/playlist")]
        [ValidateModelState]
        [SwaggerOperation("PlaylistPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(PlaylistInfoWithOwner), description: "Successful operation")]
        public virtual IActionResult PlaylistPost([FromBody]PlaylistInfo body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PlaylistInfoWithOwner));

            //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(405);
            string exampleJson = null;
            exampleJson = "\"\"";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<PlaylistInfoWithOwner>(exampleJson)
                        : default(PlaylistInfoWithOwner);            //TODO: Change the data returned
            return new ObjectResult(example);
        }*/
    }
}
