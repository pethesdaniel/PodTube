using PodTube.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using Microsoft.AspNetCore.Http;
using PodTube.DataAccess.Entities;
using Microsoft.AspNetCore;
using System.Text.Json;

namespace PodTube.BLL.Services
{
    public class VideoService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public VideoService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<VideoDto> GetAllVideos() {
            return dbContext.Videos.ProjectTo<VideoDto>(mapper.ConfigurationProvider).ToList();
        }

        public VideoDto? GetVideoById(long id) {
            var video = dbContext.Videos
                .Include(v => v.Thumbnail)
                .Include(v => v.Frames)
                .ThenInclude(f => f.File)
                .Include(v => v.Audio)
                .Where(v => v.Id == id)
                .FirstOrDefault();
            return mapper.Map<VideoDto>(video);
        }

        public bool UploadVideo(VideoRequestBody metadata, List<IFormFile> files) {
            if (
                (
                files.Count == metadata.Frames.Count + 1 
                && files.All(
                    file => metadata.Frames.Any(
                        frameMetadata => frameMetadata.File.Contains(file.FileName) 
                        || metadata.AudioFilename.Contains(file.FileName)
                        )
                    )
                ) == false) {
                return false;
            }

            metadata.Frames.ForEach(f => f.File = Path.Combine("uploads", f.File));
            metadata.AudioFilename = Path.Combine("uploads", metadata.AudioFilename);
            var video = mapper.Map<Video>(metadata);

            var wwwrootDir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");

            if (!Directory.Exists(wwwrootDir)) {
                Directory.CreateDirectory(wwwrootDir);
            }

            var filesCreated = new List<string>();
            try {
                foreach (var file in files) {
                    var relativePath = Path.Combine("uploads", file.FileName);
                    var absolutePath = Path.Combine(wwwrootDir, relativePath);
                    using (var fileStream = new FileStream(absolutePath, FileMode.Create)) {
                        file.CopyTo(fileStream);
                    }
                    filesCreated.Add(relativePath);
                }
                dbContext.Add(video);
                dbContext.AddRange(mapper.Map<List<DataAccess.Entities.File>>(filesCreated));
                dbContext.SaveChanges();
            } catch (Exception e) {
                foreach(var file in filesCreated) {
                    System.IO.File.Delete(file);
                }
                return false;
            }
            return true;
        }
    }
}
