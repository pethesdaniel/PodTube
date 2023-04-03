using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PodTube.BLL.Services {
    public class VideoService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public VideoService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<VideoDto> GetAllVideos() {
            return dbContext.Video.ProjectTo<VideoDto>(mapper.ConfigurationProvider).ToList();
        }

        public VideoDto? GetVideoById(long id) {
            var video = dbContext.Video
                .Include(v => v.Thumbnail)
                .Include(v => v.Frames)
                .ThenInclude(f => f.File)
                .Include(v => v.Sound)
                .ThenInclude(s => s.File)
                .Where(v => v.Id == id)
                .FirstOrDefault();
            return mapper.Map<VideoDto>(video);
        }
    }
}
