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
using System.Data.Common;

namespace PodTube.BLL.Services
{
    public class VideoService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;

        private static string WWWROOT = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");

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

        public bool UploadVideoMetadata(VideoRequestBody metadata) {
            var video = mapper.Map<Video>(metadata);
            try {
                dbContext.Add(video);
                dbContext.SaveChanges();
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public bool UploadVideoAudio(long videoId, IFormFile file) {
            var video = dbContext.Videos.FirstOrDefault(video => video.Id == videoId);
            if(video == null) {
                throw new ArgumentException("Invalid videoId!");
            }
            var dbFile = UploadFile(file);
            video.Audio = dbFile;
            dbContext.SaveChanges();
            return true;
        }

        private DataAccess.Entities.File UploadFile(IFormFile file) {
            var relativePath = Path.Combine("uploads", file.FileName);
            var absolutePath = Path.Combine(WWWROOT, relativePath);
            try {
                using (var fileStream = new FileStream(absolutePath, FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                var dbFile = mapper.Map<DataAccess.Entities.File>(relativePath);
                dbContext.Add(dbFile);
                dbContext.SaveChanges();
                return dbFile;
            } catch(DbException dbException) {
                System.IO.File.Delete(absolutePath);
                throw dbException;
            }
        }
    }
}
