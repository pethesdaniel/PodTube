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
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;

namespace PodTube.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/playlist")]
    public class PlaylistApiController : ControllerBase
    {
        private PlaylistService _playlistService;

        public PlaylistApiController(PlaylistService playlistService) {
            _playlistService = playlistService;
        }

        /// <summary>
        /// Get a paged list of all playlists
        /// </summary>
        /// <param name="page">Number of the currentpage</param>
        /// <param name="limit">Number of playlists on a page</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Validation exception</response>
        [HttpGet]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetPlaylistsPaged")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<PlaylistPagedListDto>> GetPlaylistsPaged([FromQuery][Required()]int page, [FromQuery][Required()]int limit)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var result = await _playlistService.GetAllPlaylists(page, limit);
            return new ObjectResult(result);
        }

        /// <summary>
        /// Get metadata of a playlist
        /// </summary>
        /// <param name="playlistId">ID of playlist to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Playlist not found</response>
        /// <response code="405">Validation exception</response>
        [HttpGet("{playlistId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetPlaylistById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<PlaylistDto>> GetPlaylistById([FromRoute][Required]long playlistId)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var result = await _playlistService.GetPlaylistById(playlistId);
            if(result == null) {
                return StatusCode(404);
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Get videos uploaded to a playlist
        /// </summary>
        /// <param name="playlistId">ID of playlist to return</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Playlist not found</response>
        [HttpGet("{playlistId}/videos")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetPlaylistVideos")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<VideoDto[]>> GetPlaylistVideos([FromRoute][Required]long playlistId)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var result = await _playlistService.GetVideosByPlaylistId(playlistId);
            if (result == null) {
                return StatusCode(404);
            }

            return new ObjectResult(result);
        }
        //TODO: ADD USER AUTH
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
        public async Task<ActionResult<PlaylistDto>> PostCreatePlaylist([FromBody]PlaylistRequestBody body)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var result = await _playlistService.CreateNewPlaylist(body);

            return new ObjectResult(await _playlistService.GetPlaylistById(result));
        }

        /// <summary>
        /// Deletes a playlist and all of it&#x27;s videos
        /// </summary>
        /// <param name="playlistId">ID of playlist to delete</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Unsuccessful operation</response>
        [HttpDelete("{playlistId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "DeletePlaylistById")]
        public async Task<ActionResult> DeletePlaylistById([FromRoute][Required] long playlistId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                await _playlistService.DeletePlaylistById(playlistId);
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }

        /// <summary>
        /// Add video to playlist
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost("{playlistId}/videos")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "PostAddVideoToPlaylistById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult> PostAddVideoToPlaylistById([FromRoute][Required] long playlistId, [FromQuery][Required] long videoId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                await _playlistService.AddVideoToPlaylistByIds(playlistId, videoId);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }

        /// <summary>
        /// Add video to playlist
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpPost("{playlistId}/reorder/{videoId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "PostReorderVideoById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult> PostReorderVideoById([FromRoute][Required] long playlistId, [FromRoute][Required] long videoId, [FromQuery][Required] long index) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                await _playlistService.ReorderVideoById(playlistId, videoId, index);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }

        /// <summary>
        /// Remove video from playlist
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid input</response>
        [HttpDelete("{playlistId}/videos")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "DeleteRemoveVideoFromPlaylistById")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult> DeleteRemoveVideoFromPlaylistById([FromRoute][Required] long playlistId, [FromQuery][Required] long videoId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                await _playlistService.RemoveVideoFromPlaylistByIds(playlistId, videoId);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }
    }
}
