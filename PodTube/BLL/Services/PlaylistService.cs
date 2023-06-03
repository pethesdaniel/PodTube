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
using static System.Collections.Specialized.BitVector32;

namespace PodTube.BLL.Services
{
    public class PlaylistService {
        private PodTubeDbContext dbContext;
        private IMapper mapper;

        public PlaylistService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> DoesUserOwnPlaylist(long playlistId, long userId) {
            return await dbContext.Playlists.Where(playlist => playlist.Id == playlistId && playlist.OwnerId == userId).AnyAsync();
        }

        public async Task<PlaylistDto?> GetPlaylistById(long id) {
            return await dbContext.Playlists.Where(playlist => playlist.Id == id).ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<PlaylistPagedListDto> GetAllPlaylistsForUser(long ownerId, int page, int limit) {
            if (page < 0 || limit < 0) {
                throw new ArgumentException("One or more paging parameters are invalid");
            }

            var playlistsPaged = await dbContext.Playlists.Where(playlist => playlist.OwnerId == ownerId).ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).ToPagedListAsync(page, limit);
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

        public async Task<long> CreateNewPlaylist(PlaylistRequestBody playlistData, long ownerId) {
            if (dbContext.Users.Count(user => user.Id == ownerId) == 0) {
                throw new ArgumentException("Invalid ownerId!");
            }

            var dbEntity = mapper.Map<Playlist>(playlistData);
            dbEntity.OwnerId = ownerId;
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

        public async Task<PlaylistDto?> AuthorizeAndGetPlaylistById(long userId, long playlistId) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                return await GetPlaylistById(playlistId);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }

        public async Task<VideoDto[]?> AuthorizeAndGetVideosByPlaylistId(long userId, long playlistId) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                return await GetVideosByPlaylistId(playlistId);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }

        public async Task AuthorizeAndDeletePlaylistById(long userId, long playlistId) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                await DeletePlaylistById(playlistId);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }

        public async Task AuthorizeAndAddVideoToPlaylistByIds(long userId, long playlistId, long videoId) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                await AddVideoToPlaylistByIds(playlistId, videoId);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }

        public async Task AuthorizeAndReorderVideoById(long userId, long playlistId, long videoId, long index) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                await ReorderVideoById(playlistId, videoId, index);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }

        public async Task AuthorizeAndRemoveVideoFromPlaylistByIds(long userId, long playlistId, long videoId) {
            if (await DoesUserOwnPlaylist(userId, playlistId)) {
                await RemoveVideoFromPlaylistByIds(playlistId, videoId);
            }
            throw new ArgumentException("User doesn't own playlist!");
        }
    }
}
