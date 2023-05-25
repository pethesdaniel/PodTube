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

        public PlaylistPagedListDto GetAllPlaylists(int page, int limit) {
            var playlistsPaged = dbContext.Playlists.ProjectTo<PlaylistDto>(mapper.ConfigurationProvider).ToPagedList(playlist => playlist.Id, page, limit);
            return mapper.Map<PlaylistPagedListDto>(playlistsPaged);
        }

        public VideoDto[] GetVideosByPlaylistId(long id) {
            var playlist = dbContext.Playlists.Include(playlist => playlist.Videos).ThenInclude(video => video.Video).ThenInclude(video => video.Thumbnail).FirstOrDefault(playlist => playlist.Id == id);
            var videoDtos = mapper.Map<List<VideoDto>>(playlist.Videos.OrderBy(pv => pv.Index).Select(pv =>pv.Video));
            if(playlist == null) {
                return default!;
            }
            return videoDtos.ToArray();
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

        public bool ReorderVideoById(long playlistId, long videoId, long index) {
            try {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction()) {
                    var playlist = dbContext.Playlists.Include(playlist => playlist.Videos).FirstOrDefault(playlist => playlist.Id == playlistId);

                    if (playlist == null) {
                        throw new ArgumentException("Invalid playlistId");
                    }


                    var playlistVideo = playlist.Videos.FirstOrDefault(e => e.VideoId == videoId);
                    if (playlistVideo == null) {
                        throw new ArgumentException("Invalid playlistId");
                    }
                    if(playlistVideo.Index < index) {
                        dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index <= index).ExecuteUpdate(s => s.SetProperty(pv => pv.Index, pv => pv.Index - 1));
                    } else {
                        dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index >= index).ExecuteUpdate(s => s.SetProperty(pv => pv.Index, pv => pv.Index + 1));
                    }
                    
                    playlistVideo.Index = index;
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public bool ReorderPlaylistById(long playlistId, long[] reordered) {
            var playlist = dbContext.Playlists.Include(playlist => playlist.Videos).FirstOrDefault(playlist => playlist.Id == playlistId);

            if(playlist == null) {
                throw new ArgumentException("Invalid playlistId");
            }

            if (playlist.Videos.All(pv => reordered.Contains(pv.VideoId)) == false) {
                throw new ArgumentException("Missing videos in order");
            }

            for(int i = 0; i < reordered.Length; ++i) {
                var videoId = reordered[i];
                playlist.Videos.FirstOrDefault(pv => pv.VideoId == videoId)!.Index = i;
            }
            try {
                dbContext.SaveChanges();
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public bool RemoveVideoFromPlaylistByIds(long playlistId, long videoId) {
            var playlistVideo = dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.VideoId == videoId).FirstOrDefault();
            try {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction()) {
                    dbContext.PlaylistVideos.Remove(playlistVideo);
                    dbContext.SaveChanges();
                    dbContext.PlaylistVideos.Where(pv => pv.PlaylistId == playlistId && pv.Index > playlistVideo.Index).ExecuteUpdate(s => s.SetProperty(pv => pv.Index, pv => pv.Index - 1));
                    dbContextTransaction.Commit();
                }
            } catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}
