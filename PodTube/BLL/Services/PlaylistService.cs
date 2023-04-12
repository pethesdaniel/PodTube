using AutoMapper;
using AutoMapper.QueryableExtensions;
using PodTube.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;

namespace PodTube.BLL.Services
{
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
            var playlist = dbContext.Playlists.Include(playlist => playlist.Videos).ThenInclude(video => video.Video).FirstOrDefault(playlist => playlist.Id == id);
            var videoDtos = mapper.Map<List<VideoDto>>(playlist.Videos.OrderBy(pv => pv.Index).Select(pv =>pv.Video));
            if(playlist == null) {
                return mapper.Map<PagedListDto<VideoDto>>(new List<VideoDto>().ToPagedList());
            }
            return mapper.Map<PagedListDto<VideoDto>>(videoDtos.ToPagedList(video => video.Id, page, limit));
        }

        public long CreateNewPlaylist(PlaylistRequestBody playlistData) {
            var dbEntity = mapper.Map<Playlist>(playlistData);
            try {
                dbContext.Add(dbEntity);
                dbContext.SaveChanges();
            } catch (Exception e) {
                return 0;
            }
            return dbEntity.Id;
        }

        public bool DeletePlaylistById(long id) {
            var playlist = dbContext.Playlists.FirstOrDefault(playlist => playlist.Id == id);
            try {
                dbContext.Remove(playlist);
                dbContext.SaveChanges();
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public bool AddVideoToPlaylistByIds(long playlistId, long videoId) {
            var playlist = dbContext.Playlists.FirstOrDefault(playlist => playlist.Id == playlistId);
            var video = dbContext.Videos.FirstOrDefault(video => video.Id == videoId);
            var index = dbContext.PlaylistVideos.Max(pv => pv.Index) + 1;
            try {
                dbContext.PlaylistVideos.Add(new PlaylistVideo { Playlist = playlist, Video = video, Index = index});
                dbContext.SaveChanges();
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public bool RemoveVideoFromPlaylistByIds(long playlistId, long videoId) {
            var playlistVideo = dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.VideoId == videoId).FirstOrDefault();
            try {
                dbContext.PlaylistVideos.Remove(playlistVideo);
                dbContext.SaveChanges();
                dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index > playlistVideo.Index).ExecuteUpdate(s => s.SetProperty(pv => pv.Index, pv => pv.Index - 1));
            } catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}
