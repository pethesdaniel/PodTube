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

        public ChannelInfoWithOwner? GetChannelById(long id) {
            return dbContext.Channels
                .Include(c => c.Picture)
                .Where(c => c.Id == id)
                .ProjectTo<ChannelInfoWithOwner>(mapper.ConfigurationProvider)
                .FirstOrDefault() 
                ?? null;
        }

        public FullChannelInfo? GetFullChannelById(long id) {
            return dbContext.Channels
                .Include(c => c.Videos)
                .Include(c => c.Picture)
                .Include(c => c.Owner)
                .Where(c => c.Id == id)
                .ProjectTo<FullChannelInfo>(mapper.ConfigurationProvider)
                .FirstOrDefault()
                ?? null;
        }

        public PagedChannelList GetChannelsPaged(int page, int limit) {
            var pagedChannels = dbContext.Channels.ToPagedList(channel => channel.Id, page, limit);
            return mapper.Map<PagedChannelList>(pagedChannels);
        }
    }
}
