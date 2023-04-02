using AutoMapper;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace PodTube.BLL.Mapper {
    public class PagedListProfile : Profile {
        public PagedListProfile() {
            CreateMap<IPagedList<Channel>, PagedChannelList>().ForMember(dest => dest.Limit, opt => opt.MapFrom(pl => pl.PageSize))
                .ForMember(dest => dest.Page, opt => opt.MapFrom(pl => pl.PageNumber))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(pl => pl.PageCount))
                .ForMember(dest => dest.Channels, opt => opt.MapFrom(pl => pl));
        }
    }
}
