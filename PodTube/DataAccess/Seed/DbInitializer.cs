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

            var files = new Entities.File[] {
                new Entities.File { ResourceURI = "sample/profilePics/profilePic (1).jpg", MimeType = "image/jpg" }, //0
                new Entities.File { ResourceURI = "sample/profilePics/profilePic (2).jpg", MimeType = "image/jpg" }, //1
                new Entities.File { ResourceURI = "sample/profilePics/profilePic (3).jpg", MimeType = "image/jpg" }, //2
                new Entities.File { ResourceURI = "sample/thumbnails/thumbnail (1).jpg", MimeType = "image/jpg" }, //3
                new Entities.File { ResourceURI = "sample/thumbnails/thumbnail (2).jpg", MimeType = "image/jpg" }, //4
                new Entities.File { ResourceURI = "sample/thumbnails/thumbnail (3).jpg", MimeType = "image/jpg" }, //5
                new Entities.File { ResourceURI = "sample/thumbnails/thumbnail (4).jpg", MimeType = "image/jpg" }, //6
                new Entities.File { ResourceURI = "sample/thumbnails/thumbnail (5).jpg", MimeType = "image/jpg" }, //7
                new Entities.File { ResourceURI = "sample/frames/frame (1).jpg", MimeType = "image/jpg" }, //8
                new Entities.File { ResourceURI = "sample/frames/frame (2).jpg", MimeType = "image/jpg" }, //9
                new Entities.File { ResourceURI = "sample/frames/frame (3).jpg", MimeType = "image/jpg" }, //10
                new Entities.File { ResourceURI = "sample/frames/frame (4).jpg", MimeType = "image/jpg" }, //11
                new Entities.File { ResourceURI = "sample/audio/audio1.mp3", MimeType = "audio/mp3" }, //12
            };

            context.AddRange(files);


            var users = new User[] {
                new User { UserName = "TestUser1", ProfilePicture = files[0] },
                new User { UserName = "TestUser2", ProfilePicture = files[1] },
                new User { UserName = "TestUser3", ProfilePicture = files[2] },
            };
            context.AddRange(users);

            var channels = new Channel[] {
                new Channel { 
                    Name = "Test Channel 1", 
                    Description="This is my very cool test channel. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam scelerisque imperdiet fringilla. Sed maximus, nulla sit amet rhoncus lacinia, risus risus condimentum ligula, nec luctus leo nisl in enim. Curabitur tortor metus, interdum a justo eu, accumsan pharetra arcu. Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                    Owner = users[0], 
                    Thumbnail= files[3]
                },
                new Channel { Name = "Test Channel 2", Owner = users[0], Thumbnail= files[4]},
                new Channel { Name = "Test Channel 3", Owner = users[1], Thumbnail= files[5]},
            };
            context.AddRange(channels);

            var frames = new Frame[] {
                new Frame { File = files[8], TimeStampStart = 0, TimeStampEnd = 5},
                new Frame { File = files[9], TimeStampStart = 5, TimeStampEnd = 10},
                new Frame { File = files[10], TimeStampStart = 0, TimeStampEnd = 10},
            };

            context.AddRange(frames);

            var video = new Video[] {
                new Video {
                   Name = "My Amazing Test Podcast: Episode 4",
                   Description = "This is a test video :)",
                   DatePublished = DateTime.Now,
                   Frames = new List<Frame> { frames[0], frames[1] },
                   Audio = files[12],
                   Thumbnail = files[6],
                   Channel = channels[0]
                },
                new Video {
                   Name = "My Amazing Test Podcast: Episode 5",
                   Description = "This is also a test video :3",
                   DatePublished = DateTime.Now,
                   Frames = new List<Frame> { frames[2] },
                   Audio = files[12],
                   Thumbnail = files[7],
                   Channel = channels[0]
                }
            };

            context.AddRange(video);

            var playlist = new Playlist[] {
                new Playlist {
                   Name = "This is a playlist",
                   Description = "My favourite videos",
                   Thumbnail = files[3],
                   Owner = users[0]
                },
                new Playlist {
                   Name = "This is a second playlist",
                   Description = "My favourite videos again",
                   Thumbnail = files[4],
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
