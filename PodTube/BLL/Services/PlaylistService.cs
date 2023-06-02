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
using DelegateDecompiler;
using System.Data.Common;

namespace PodTube.BLL.Services
{
    public class PlaylistService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;

        public PlaylistService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PlaylistDto?> GetPlaylistById(long id) {
            return await dbContext.Playlists.ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(playlist => playlist.Id == id);
        }

        public async Task<PlaylistPagedListDto> GetAllPlaylists(int page, int limit) {
            if (page < 0 || limit < 0) {
                throw new ArgumentException("One or more paging parameters are invalid");
            }

            var playlistsPaged = await dbContext.Playlists.ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).ToPagedListAsync(page, limit);
            return mapper.Map<PlaylistPagedListDto>(playlistsPaged);
        }

        public async Task<VideoDto[]?> GetVideosByPlaylistId(long id) {
            var playlist = await dbContext.Playlists.Include(playlist => playlist.Videos).ThenInclude(video => video.Video).ThenInclude(video => video.Thumbnail).FirstOrDefaultAsync(playlist => playlist.Id == id);
            if (playlist == null) {
                return null;
            }
            var videoDtos = mapper.Map<List<VideoDto>>(playlist.Videos.OrderBy(pv => pv.Index).Select(pv =>pv.Video));
            return videoDtos.ToArray();
        }

        public async Task<long> CreateNewPlaylist(PlaylistRequestBody playlistData) {
            var dbEntity = mapper.Map<Playlist>(playlistData);
            dbContext.Add(dbEntity);
            await dbContext.SaveChangesAsync();
            return dbEntity.Id;
        }

        public async Task DeletePlaylistById(long id) {
            var playlist = await dbContext.Playlists.FirstOrDefaultAsync(playlist => playlist.Id == id);

            if(playlist == null) {
                throw new ArgumentException("Invalid playlist id");
            }
            dbContext.Remove(playlist);
            await dbContext.SaveChangesAsync();

            return;
        }

        public async Task AddVideoToPlaylistByIds(long playlistId, long videoId) {
            var playlist = await dbContext.Playlists.FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
            if(playlist == null) {
                throw new ArgumentException("Invalid playlist id");
            }
            var video = await dbContext.Videos.FirstOrDefaultAsync(video => video.Id == videoId);
            if(video == null) {
                throw new ArgumentException("Invalid video id");
            }
            var index = dbContext.PlaylistVideos.Max(pv => pv.Index) + 1;
            dbContext.PlaylistVideos.Add(new PlaylistVideo { Playlist = playlist, Video = video, Index = index });
            await dbContext.SaveChangesAsync();
        }

        public async Task ReorderVideoById(long playlistId, long videoId, long index) {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction()) {
                var playlist = await dbContext.Playlists.Include(playlist => playlist.Videos).FirstOrDefaultAsync(playlist => playlist.Id == playlistId);

                if (playlist == null) {
                    throw new ArgumentException("Invalid playlist id");
                }

                var playlistVideo = playlist.Videos.FirstOrDefault(e => e.VideoId == videoId);
                if (playlistVideo == null) {
                    throw new ArgumentException("Invalid video id");
                }

                if(index <= 0 || index > playlist.Videos.Max(pv => pv.Index)) {
                    throw new ArgumentException("Invalid index");
                }

                if (playlistVideo.Index < index) {
                    await dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index <= index).ExecuteUpdateAsync(s => s.SetProperty(pv => pv.Index, pv => pv.Index - 1));
                } else {
                    await dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index >= index).ExecuteUpdateAsync(s => s.SetProperty(pv => pv.Index, pv => pv.Index + 1));
                }

                playlistVideo.Index = index;
                await dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
        }

        public async Task RemoveVideoFromPlaylistByIds(long playlistId, long videoId) {
            var playlistVideo = await dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.VideoId == videoId).FirstOrDefaultAsync();
            if (playlistVideo == null) {
                throw new ArgumentException("Invalid playlist or video id");
            }
            using (var dbContextTransaction = dbContext.Database.BeginTransaction()) {
                dbContext.PlaylistVideos.Remove(playlistVideo);
                await dbContext.SaveChangesAsync();
                await dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index > playlistVideo.Index).ExecuteUpdateAsync(s => s.SetProperty(pv => pv.Index, pv => pv.Index - 1));
                await dbContextTransaction.CommitAsync();
            }
        }
    }
}
