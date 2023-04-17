using PodTube.DataAccess.Contexts;
using PodTube.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Seed {
    public static class DbInitializer {
        public static void Initialize(PodTubeDbContext context) {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Videos.Any()) {
                return;   // DB has been seeded
            }

            var users = new User[] {
                new User { Name = "TestUser1" },
                new User { Name = "TestUser2" },
                new User { Name = "TestUser3" },
            };
            context.AddRange(users);

            var channels = new Channel[] {
                new Channel { Name = "Test Channel 1", Owner = users[0]},
                new Channel { Name = "Test Channel 2", Owner = users[0]},
                new Channel { Name = "Test Channel 3", Owner = users[1]},
            };
            context.AddRange(channels);

            var files = new Entities.File[] {
                new Entities.File { ResourceURI = "image.png", MimeType = "image/png" },
                new Entities.File { ResourceURI = "image2.png", MimeType = "image/png" },
                new Entities.File { ResourceURI = "image3.png", MimeType = "image/png" },
                new Entities.File { ResourceURI = "sound.ogg", MimeType = "audio/ogg" },
                new Entities.File { ResourceURI = "sound2.ogg", MimeType = "audio/ogg" },
                new Entities.File { ResourceURI = "icon-192.png", MimeType = "image/png" },
            };

            context.AddRange(files);

            var frames = new Frame[] {
                new Frame { File = files[0], TimeStampStart = 0, TimeStampEnd = 5000},
                new Frame { File = files[1], TimeStampStart = 0, TimeStampEnd = 10000},
                new Frame { File = files[2], TimeStampStart = 10000, TimeStampEnd = 20000},
            };

            context.AddRange(frames);

            var video = new Video[] {
                new Video {
                   Name = "My Amazing Test Podcast: Episode 4",
                   Description = "This is a test video :)",
                   DatePublished = DateTime.Now,
                   Frames = new List<Frame> { frames[0], frames[1] },
                   Audio = files[3],
                   Thumbnail = files[5],
                   Channel = channels[0]
                },
                new Video {
                   Name = "My Amazing Test Podcast: Episode 5",
                   Description = "This is also a test video :3",
                   DatePublished = DateTime.Now,
                   Frames = new List<Frame> { frames[2] },
                   Audio = files[4],
                   Thumbnail = files[5],
                   Channel = channels[0]
                }
            };

            context.AddRange(video);

            var playlist = new Playlist[] {
                new Playlist {
                   Name = "This is a playlist",
                   Description = "My favourite videos",
                   Thumbnail = files[5],
                   Owner = users[0]
                },
                new Playlist {
                   Name = "This is a second playlist",
                   Description = "My favourite videos again",
                   Thumbnail = files[5],
                   Owner = users[1]
                },
            };

            context.AddRange(playlist);

            var playlistVideo = new PlaylistVideo[] {
                new PlaylistVideo {
                    Playlist = playlist[0],
                    Video = video[0],
                    Index = 1
                },
                new PlaylistVideo {
                    Playlist = playlist[1],
                    Video = video[0],
                    Index = 1
                },
                new PlaylistVideo {
                    Playlist = playlist[1],
                    Video = video[1],
                    Index = 2
                }
            };

            context.AddRange(playlistVideo);

            context.SaveChanges();
        }
    }
}
