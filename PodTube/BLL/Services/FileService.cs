using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PodTube.BLL.DomainObject;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using System;
using System.Collections.Generic;
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
                var fileExtension = file.FileName.Split(".").LastOrDefault("");
                var relativePath = Path.Combine("uploads", fileName.ToString() + (fileExtension != "" ? "." + fileExtension : ""));
                var absolutePath = Path.Combine(wwwrootDir, relativePath);
                using (var fileStream = new FileStream(absolutePath, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
                fileCreated = absolutePath;
                var fileDb = mapper.Map<DataAccess.Entities.File>(new FileCreate {
                    Path = relativePath,
                    UserId = userId
                });
                dbContext.Add(fileDb);
                await dbContext.SaveChangesAsync();

                return new FileUploadResponseDTO {
                    Url = relativePath,
                    MimeType = MimeTypes.GetMimeType(relativePath),
                    FileId = fileDb.Id
                };
            } catch (Exception _) {
                if (!fileCreated.IsNullOrEmpty()) {
                    File.Delete(fileCreated);
                }
                return null;
            }
        }

        public async Task<bool> DeleteFile(long fileId, long userId) {
            var file = dbContext.Files.FirstOrDefault(file => file.Id == fileId && file.OwnerId == userId);
            if(file == null) {
                throw new ArgumentException("No such file id for user");
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
    }
}
