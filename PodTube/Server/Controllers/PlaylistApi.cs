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
    [Authorize]
    [ApiController]
    [Route("api/playlist")]
    public class PlaylistApiController : ControllerBase
    {
        private PlaylistService _playlistService;
        private UserService _userService;

        public PlaylistApiController(PlaylistService playlistService, UserService userService) {
            _playlistService = playlistService;
            _userService = userService;
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
        public async Task<ActionResult<PlaylistPagedListDto>> GetPlaylistsPaged([FromQuery][Required]int page, [FromQuery][Required]int limit)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var ownerId = await _userService.GetAuthorizedUserId(User);

            var result = await _playlistService.GetAllPlaylistsForUser(ownerId, page, limit);
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

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                var result = await _playlistService.AuthorizeAndGetPlaylistById(ownerId, playlistId);
                if (result == null) {
                    return StatusCode(404);
                }
                return Ok(result);
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
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

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                var result = await _playlistService.AuthorizeAndGetVideosByPlaylistId(ownerId, playlistId);
                if (result == null) {
                    return StatusCode(404);
                }
                return Ok(result);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
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
        public async Task<ActionResult<PlaylistDto>> PostCreatePlaylist([FromBody][Required]PlaylistRequestBody body)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                var result = await _playlistService.CreateNewPlaylist(body, ownerId);
                if (result == 0) {
                    return StatusCode(500);
                }
                return Ok(await _playlistService.GetPlaylistById(result));
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a playlist and all of it&#x27;s videos
        /// </summary>
        /// <param name="playlistId">ID of playlist to delete</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Unsuccessful operation</response>
        [Authorize]
        [HttpDelete("{playlistId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "DeletePlaylistById")]
        public async Task<ActionResult> DeletePlaylistById([FromRoute][Required] long playlistId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                await _playlistService.AuthorizeAndDeletePlaylistById(ownerId, playlistId);
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

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                await _playlistService.AuthorizeAndAddVideoToPlaylistByIds(ownerId, playlistId, videoId);
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

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                await _playlistService.AuthorizeAndReorderVideoById(ownerId, playlistId, videoId, index);
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

            var ownerId = await _userService.GetAuthorizedUserId(User);

            try {
                await _playlistService.AuthorizeAndRemoveVideoFromPlaylistByIds(ownerId, playlistId, videoId);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
            return StatusCode(200);
        }
    }
}
