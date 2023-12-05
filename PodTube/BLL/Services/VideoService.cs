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

        public async Task<VideoDto?> GetVideoById(long id) {
            var video = await dbContext.Videos
                .Include(v => v.Thumbnail)
                .Include(v => v.Frames)
                .ThenInclude(f => f.File)
                .Include(v => v.Audios)
                .ThenInclude(a => a.File)
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();
            return mapper.Map<VideoDto>(video);
        }

        public bool UploadVideo(VideoUploadRequestBody metadata) {
            var video = mapper.Map<Video>(metadata);

            try {
                dbContext.Add(video);
                dbContext.SaveChanges();
            } catch (Exception _) {
                return false;
            }
            return true;
        }
    }
}
