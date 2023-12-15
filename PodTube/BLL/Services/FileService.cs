using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PodTube.BLL.DomainObject;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Services {
    public class FileService {

        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public FileService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<FileUploadResponseDTO?> UploadFile(IFormFile file, long userId) {
            var wwwrootDir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");

            if (!Directory.Exists(wwwrootDir)) {
                Directory.CreateDirectory(wwwrootDir);
            }

            var fileCreated = "";
            try {
                var fileName = Guid.NewGuid();
                var split = file.FileName.Split(".");
                var fileExtension = split.LastOrDefault("");
                var relativePath = Path.Combine("uploads", fileName.ToString() + (fileExtension != "" ? "." + fileExtension : ""));
                var absolutePath = Path.Combine(wwwrootDir, relativePath);
                using (var fileStream = new FileStream(absolutePath, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
                fileCreated = absolutePath;
                var fileDb = mapper.Map<DataAccess.Entities.File>(new FileCreate {
                    Path = relativePath,
                    UserId = userId,
                    MimeType = MimeTypes.GetMimeType(relativePath),
                    UserFriendlyName = split.FirstOrDefault("")
                });
                dbContext.Add(fileDb);
                await dbContext.SaveChangesAsync();

                return new FileUploadResponseDTO {
                    Url = relativePath,
                    MimeType = fileDb.MimeType,
                    FileId = fileDb.Id,
                    UserFriendlyName = fileDb.UserFriendlyName
                };
            } catch (Exception _) {
                if (!fileCreated.IsNullOrEmpty()) {
                    File.Delete(fileCreated);
                }
                return null;
            }
        }

        public async Task<bool> DeleteFile(long fileId, long userId) {
            var file = dbContext.Files.Include(file => file.Audios)
                .Include(file => file.Frames)
                .Include(file => file.ChannelThumbnails)
                .Include(file => file.PlaylistThumbnails)
                .Include(file => file.ProfilePictures)
                .Include(file => file.VideoThumbnails)
                .FirstOrDefault(file => file.Id == fileId && file.OwnerId == userId);
            if(file == null) {
                throw new ArgumentException("No such file id for user");
            }
            if (file.IsUsed()) {
                throw new ArgumentException("File is in use. Delete resource first.");
            }
            var wwwrootDir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            var absolutePath = Path.Combine(wwwrootDir, file.ResourceURI);
            try {
                dbContext.Files.Remove(file);
                await dbContext.SaveChangesAsync();
                File.Delete(absolutePath);
            } catch(Exception e) {
                return false;
            }
            return true;
        }

        public async Task<List<FileUploadResponseDTO>> GetFilesForUser(long userId) {
            var files = await dbContext.Files
                .Include(file => file.Audios)
                .Include(file => file.Frames)
                .Include(file => file.ChannelThumbnails)
                .Include(file => file.PlaylistThumbnails)
                .Include(file => file.ProfilePictures)
                .Include(file => file.VideoThumbnails)
                .Where(file => file.OwnerId == userId).ToListAsync();
            return mapper.Map<List<FileUploadResponseDTO>>(files);
        }
    }
}
