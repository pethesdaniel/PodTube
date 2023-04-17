using AutoMapper;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Mapper
{
    public class RequestBodyToDbProfile : Profile {
        public RequestBodyToDbProfile() {
            CreateMap<string, DataAccess.Entities.File>()
                .ForMember(dest => dest.ResourceURI, opt => opt.MapFrom(file => file))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(file => MimeTypes.GetMimeType(file)));
            CreateMap<PlaylistRequestBody, Playlist>();
            CreateMap<VideoRequestBody, Video>()
                .ForMember(dest => dest.Audio, opt => opt.MapFrom(video => video.AudioFilename));
            CreateMap<FrameRequestBody, Frame>();


        }
    }
}
