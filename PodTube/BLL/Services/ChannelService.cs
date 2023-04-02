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

        public PagedChannelList GetChannelsPaged(long page, long limit) {
            var startId = (page - 1) * limit;
            var endId = page * limit;
            var channels = dbContext.Channels.Where(c => c.Id > startId && c.Id <= endId).ProjectTo<ChannelInfo>(mapper.ConfigurationProvider);
            return new PagedChannelList {
                Channels = channels.ToList(),
                Limit = limit,
                Page = page,
                Total = (long)Math.Ceiling(dbContext.Channels.Count() / ((decimal)limit))
            };
        }
    }
}
