using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodTube.Attributes;
using PodTube.BLL.Services;
using PodTube.DataAccess.Contexts;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PodTube.Server.Controllers {

    [ApiController]
    [Route("api/file")]
    public class FileApi : ControllerBase {
        private FileService _fileService;
        private UserService _userService;

        public FileApi(FileService fileService, UserService userService) {
            this._fileService = fileService;
            this._userService = userService;
        }

        [Authorize]
        [HttpPost]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "UploadFile")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult<FileUploadResponseDTO>> PostUploadFile([Required] IFormFile file) {
            var user = await _userService.GetAuthorizedUserId(User);
            if (user == 0) {
                return StatusCode(401);
            }
            try {
                var success = await _fileService.UploadFile(file, user);
                if (success == null) {
                    return StatusCode(400);
                }
                return new ActionResult<FileUploadResponseDTO>(success);
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{fileId}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "DeleteFile")]
        [SwaggerResponse(statusCode: 200, description: "Successful operation")]
        public async Task<ActionResult> DeleteFile([FromRoute][Required] long fileId) {
            var user = await _userService.GetAuthorizedUserId(User);
            if (user == 0) {
                return StatusCode(401);
            }
            try {
                var success = await _fileService.DeleteFile(fileId, user);
                if (!success) {
                    return StatusCode(400);
                }
                return StatusCode(200);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
