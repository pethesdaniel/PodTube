using AutoMapper;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Mapper
{
    public class DbToDtoProfile : Profile {
        public DbToDtoProfile() {
            CreateMap<DataAccess.Entities.File, string>().ConvertUsing(file => file.ResourceURI);

            /*
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description ?? string.Empty,
                Cover = channel.Picture?.ResourceURI ?? string.Empty,
                Owner = channel?.Owner?.ToUserInfoDto() ?? null
             */
            CreateMap<Channel, ChannelDto>().ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(channel => channel.Thumbnail.ResourceURI));

            /*
                Url = frame.File.ResourceURI,
                TimestampStart = frame.TimeStampStart,
                TimestampEnd = frame.TimeStampEnd
             */
            CreateMap<Frame, FrameDto>().ForMember(dest => dest.Url, opt => opt.MapFrom(frame => frame.File.ResourceURI));

            /*
                Id = user.Id,
                Username = user.Name,
                ProfilePic = user.ProfilePicture?.ResourceURI ?? string.Empty
             */
            CreateMap<User, UserDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(user => user.UserName))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(user => user.ProfilePicture.ResourceURI));

            /*
                Id = video.Id,
                Name = video.Name,
                Description = video.Description ?? string.Empty,
                Cover = video.Thumbnail?.ResourceURI ?? string.Empty,
             */
            CreateMap<Video, VideoDto>().ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(video => video.Thumbnail.ResourceURI));

            CreateMap<Audio, string>().ConvertUsing(audio => audio.File.ResourceURI);

            CreateMap<Playlist, PlaylistDto>().ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(playlist => playlist.Thumbnail.ResourceURI));
            CreateMap<Playlist, PlaylistBasicDto>();

        }
    }
}
