using AutoMapper;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using static PodTube.BLL.Mapper.PagedListProfile;

namespace PodTube.BLL.Mapper {
    public class PagedListProfile : Profile {
        public PagedListProfile() {
            CreateMap(typeof(IPagedList<>), typeof(PagedListDto<>)).ConvertUsing(typeof(PagedListConverter<>));
        }

        public class PagedListConverter<T> : ITypeConverter<IPagedList<T>, PagedListDto<T>> {
            public PagedListConverter() { }
            public PagedListDto<T> Convert(IPagedList<T> source, PagedListDto<T> destination, ResolutionContext context) {
                return new PagedListDto<T> {
                    Limit = source.PageSize,
                    Total = source.PageCount,
                    Page = source.PageNumber,
                    Content = source.ToList()
                };
            }
        }
    }
}
