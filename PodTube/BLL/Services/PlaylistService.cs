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

        public PlaylistInfoWithOwner GetPlaylistById(long id) {
            var entity = dbContext.Playlists.Include(p => p.Picture).Include(p=>p.Owner).FirstOrDefault(playlist => playlist.Id == id);
            return mapper.Map<PlaylistInfoWithOwner>(entity);
        }

        public PagedListDto<PlaylistInfo> GetAllPlaylists(int page, int limit) {
            var playlistsPaged = dbContext.Playlists.Include(p => p.Picture).ProjectTo<PlaylistInfo>(mapper.ConfigurationProvider).ToPagedList(playlist => playlist.Id, page, limit);
            return mapper.Map<PagedListDto<PlaylistInfo>>(playlistsPaged);
        }

        public FullPlaylistInfo GetPlaylistWithVideoById(long id) {
            var entity = dbContext.Playlists.Include(playlist => playlist.Videos).FirstOrDefault(playlist => playlist.Id == id);
            return mapper.Map<FullPlaylistInfo>(entity);
        }
    }
}
