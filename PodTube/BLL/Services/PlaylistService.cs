﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace PodTube.BLL.Services {
    public class PlaylistService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;

        public PlaylistService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public PlaylistDto? GetPlaylistById(long id) {
            return dbContext.Playlists.ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).FirstOrDefault(playlist => playlist.Id == id);
        }

        public PagedListDto<PlaylistDto> GetAllPlaylists(int page, int limit) {
            var playlistsPaged = dbContext.Playlists.ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).ToPagedList(playlist => playlist.Id, page, limit);
            return mapper.Map<PagedListDto<PlaylistDto>>(playlistsPaged);
        }

        public PagedListDto<VideoDto> GetPagedVideosByPlaylistId(long id, int page, int limit) {
            var playlist = dbContext.Playlists.Include(playlist => playlist.Videos).FirstOrDefault(playlist => playlist.Id == id);
            var videoDtos = mapper.Map<List<VideoDto>>(playlist.Videos);
            if(playlist == null) {
                return mapper.Map<PagedListDto<VideoDto>>(new List<VideoDto>().ToPagedList());
            }
            return mapper.Map<PagedListDto<VideoDto>>(videoDtos.ToPagedList(video => video.Id, page, limit));
        }
    }
}
