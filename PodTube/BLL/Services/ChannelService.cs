using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
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

        public async Task<ChannelDto?> GetChannelById(long id) {
            var channel = await dbContext.Channels
                .ProjectTo<ChannelDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);
            return channel;
        }

        public async Task<VideoPagedListDto?> GetPagedVideosByChannelId(long id, int page, int limit) {
            if (page < 0 || limit < 0) {
                throw new ArgumentException("One or more paging parameters are invalid");
            }
            var channel = await dbContext.Channels.Include(channel => channel.Videos).ThenInclude(v=>v.Thumbnail).FirstOrDefaultAsync(channel => channel.Id == id);
            if (channel == null) {
                return null;
            }

            var videoDtos = mapper.Map<List<VideoDto>>(channel.Videos);
            var result = mapper.Map<VideoPagedListDto>(videoDtos.ToPagedList(video => video.Id, page, limit));
            return result;
        }

        public async Task<ChannelPagedListDto> GetChannelsPaged(int page, int limit) {
            IPagedList<ChannelDto> pagedChannels = await dbContext.Channels
                .ProjectTo<ChannelDto>(mapper.ConfigurationProvider)
                .ToPagedListAsync(page, limit);
            return mapper.Map<ChannelPagedListDto>(pagedChannels);
        }

        public async Task<ChannelDto> CreateChannel(ChannelRequestBody channelRequest, long ownerId) {
            if(dbContext.Users.Count(user=> user.Id == ownerId) == 0) {
                throw new ArgumentException("Invalid ownerId!");
            }

            if(channelRequest.Name.Length == 0) {
                throw new ArgumentException("Invalid channel name!");
            }

            var channel = new DataAccess.Entities.Channel {
                Name = channelRequest.Name,
                Description = channelRequest.Description,
                OwnerId = ownerId
            };

            dbContext.Channels.Add(channel);

            await dbContext.SaveChangesAsync();

            return mapper.Map<ChannelDto>(channel);
        }
    }
}
