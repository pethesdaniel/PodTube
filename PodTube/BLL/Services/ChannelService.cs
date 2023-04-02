using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace PodTube.BLL.Services {
    public class ChannelService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public ChannelService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ChannelWithOwnerDto? GetChannelById(long id) {
            return dbContext.Channels
                .Include(c => c.Picture)
                .Where(c => c.Id == id)
                .ProjectTo<ChannelWithOwnerDto>(mapper.ConfigurationProvider)
                .FirstOrDefault() 
                ?? null;
        }

        public ChannelWithVideoDto? GetFullChannelById(long id) {
            return dbContext.Channels
                .Include(c => c.Videos)
                .Include(c => c.Picture)
                .Include(c => c.Owner)
                .Where(c => c.Id == id)
                .ProjectTo<ChannelWithVideoDto>(mapper.ConfigurationProvider)
                .FirstOrDefault()
                ?? null;
        }

        public PagedListDto<ChannelDto> GetChannelsPaged(int page, int limit) {
            IPagedList<ChannelDto> pagedChannels = dbContext.Channels.ProjectTo<ChannelDto>(mapper.ConfigurationProvider).ToPagedList(channel => channel.Id, page, limit);
            return mapper.Map<PagedListDto<ChannelDto>>(pagedChannels);
        }
    }
}
