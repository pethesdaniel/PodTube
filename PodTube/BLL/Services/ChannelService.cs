using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace PodTube.BLL.Services
{
    public class ChannelService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public ChannelService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ChannelDto? GetChannelById(long id) {
            return dbContext.Channels
                .ProjectTo<ChannelDto>(mapper.ConfigurationProvider)
                .FirstOrDefault(c => c.Id == id);
        }

        public VideoPagedListDto GetPagedVideosByChannelId(long id, int page, int limit) {
            var channel = dbContext.Channels.Include(channel => channel.Videos).ThenInclude(v=>v.Thumbnail).FirstOrDefault(channel => channel.Id == id);
            var videoDtos = mapper.Map<List<VideoDto>>(channel.Videos);
            if (channel == null) {
                return mapper.Map<VideoPagedListDto>(new List<VideoDto>().ToPagedList());
            }
            var result = mapper.Map<VideoPagedListDto>(videoDtos.ToPagedList(video => video.Id, page, limit));
            return result;
        }

        public ChannelPagedListDto GetChannelsPaged(int page, int limit) {
            IPagedList<ChannelDto> pagedChannels = dbContext.Channels
                .ProjectTo<ChannelDto>(mapper.ConfigurationProvider)
                .ToPagedList(channel => channel.Id, page, limit);
            return mapper.Map<ChannelPagedListDto>(pagedChannels);
        }
    }
}
