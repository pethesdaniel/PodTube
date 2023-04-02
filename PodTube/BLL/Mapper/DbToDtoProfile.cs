using AutoMapper;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Mapper {
    public class DbToDtoProfile : Profile {
        public DbToDtoProfile() {
            /*
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description ?? string.Empty,
                Cover = channel.Picture?.ResourceURI ?? string.Empty
             */
            CreateMap<Channel, ChannelDto>().ForMember(dest => dest.Cover, opt => opt.MapFrom(channel => channel.Picture.ResourceURI));
            /*
                Id = channel.Id,
                Name = channel.Name,
                Description = channel.Description ?? string.Empty,
                Cover = channel.Picture?.ResourceURI ?? string.Empty,
                Owner = channel?.Owner?.ToUserInfoDto() ?? null
             */
            CreateMap<Channel, ChannelWithOwnerDto>().ForMember(dest => dest.Cover, opt => opt.MapFrom(channel => channel.Picture.ResourceURI));
            /*
             ChannelInfo = channel.ToChannelInfoWithOwnerDto(),
             Videos = channel.Videos.Select(v => v.ToVideoInfoDto()).ToList()
             */
            CreateMap<Channel, ChannelWithVideoDto>().ForMember(dest => dest.ChannelInfo, opt =>opt.MapFrom(channel => channel));

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
            CreateMap<User, UserDto>().ForMember(dest => dest.Username, opt => opt.MapFrom(user => user.Name))
                .ForMember(dest => dest.ProfilePic, opt => opt.MapFrom(user => user.ProfilePicture.ResourceURI));

            /*
                Id = video.Id,
                Name = video.Name,
                Description = video.Description ?? string.Empty,
                Cover = video.Thumbnail?.ResourceURI ?? string.Empty,
             */
            CreateMap<Video, VideoDto>().ForMember(dest => dest.Cover, opt => opt.MapFrom(video => video.Thumbnail.ResourceURI));
            /*
                VideoInfo = video.ToVideoInfoDto(),
                Frames = video.Frames.Select(f => f.ToFrameDto()).ToList(),
                Audio = video.Sound.Select(a => a.File.ResourceURI).ToList()
             */
            CreateMap<Video, VideoWithFramesDto>().ForMember(dest => dest.VideoInfo, opt => opt.MapFrom(video => video))
                .ForMember(dest=>dest.Audio, opt=>opt.MapFrom(video=>video.Sound));
            CreateMap<Sound, string>().ConvertUsing(audio => audio.File.ResourceURI);


            CreateMap<Playlist, PlaylistDto>().ForMember(dest => dest.Cover, opt => opt.MapFrom(playlist => playlist.Picture.ResourceURI));
            CreateMap<Playlist, PlaylistWithOwnerDto>().ForMember(dest => dest.Cover, opt => opt.MapFrom(playlist => playlist.Picture.ResourceURI));
            CreateMap<Playlist, PlaylistWithVideoDto>().ForMember(dest => dest.PlaylistInfo, opt => opt.MapFrom(playlist => playlist));
        }
    }
}
