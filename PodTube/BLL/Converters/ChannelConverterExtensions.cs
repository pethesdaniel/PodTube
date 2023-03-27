using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Converters {
    public static class ChannelConverterExtensions {
        public static Shared.Models.ChannelInfo ToChannelInfoDto(this DataAccess.Entities.Channel channel) {
            return new Shared.Models.ChannelInfo {
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description ?? string.Empty,
                Cover = channel.Picture?.ResourceURI ?? string.Empty
            };
        }

        public static Shared.Models.ChannelInfoWithOwner ToChannelInfoWithOwnerDto(this DataAccess.Entities.Channel channel) {
            return new Shared.Models.ChannelInfoWithOwner {
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description ?? string.Empty,
                Cover = channel.Picture?.ResourceURI ?? string.Empty,
                Owner = channel?.Owner?.ToUserInfoDto() ?? null
            };
        }

        public static Shared.Models.FullChannelInfo ToFullChannelInfoDto(this DataAccess.Entities.Channel channel) {
            return new Shared.Models.FullChannelInfo {
                ChannelInfo = channel.ToChannelInfoWithOwnerDto(),
                Videos = channel.Videos.Select(v => v.ToVideoInfoDto()).ToList()
            };
        }
    }
}
