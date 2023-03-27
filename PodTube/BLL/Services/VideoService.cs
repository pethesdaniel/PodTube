using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PodTube.BLL.Converters;

namespace PodTube.BLL.Services {
    public class VideoService {
        private PodTubeDbContext dbContext;
        public VideoService(PodTubeDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<VideoInfo> GetAllVideos() {
            var videos = dbContext.Video.Select(v => new VideoInfo {
                Id = v.Id,
                Name = v.Name,
                Description = v.Description ?? string.Empty,
                Cover = v.Thumbnail.ResourceURI ?? string.Empty,
            });
            return videos.ToList();
        }

        public FullVideoInfo? GetVideoById(long id) {
            return dbContext.Video
                .Include(v => v.Thumbnail)
                .Include(v=>v.Frames)
                .ThenInclude(f => f.File)
                .Include(v=>v.Sound)
                .ThenInclude(s => s.File)
                .Where(v => v.Id == id)
                .FirstOrDefault()?.ToFullVideoInfoDto() ?? null;
        }
    }
}
