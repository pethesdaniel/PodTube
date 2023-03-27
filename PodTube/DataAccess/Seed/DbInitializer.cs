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
            if (context.Video.Any()) {
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

            var sounds = new Sound[] {
                new Sound { File = files[3] },
                new Sound { File = files[4] }
            };

            context.AddRange(sounds);

            var video = new Video[] {
                new Video {
                   Name = "My Amazing Test Podcast: Episode 4",
                   Description = "This is a test video :)",
                   Frames = new List<Frame> { frames[0], frames[1] },
                   Sound = new List<Sound>{sounds[0]},
                   Thumbnail = files[5],
                   Channel = channels[0]
                },
                new Video {
                   Name = "My Amazing Test Podcast: Episode 5",
                   Description = "This is also a test video :3",
                   Frames = new List<Frame> { frames[2] },
                   Sound = new List<Sound>{sounds[1]},
                   Thumbnail = files[5],
                   Channel = channels[0]
                }
            };

            context.AddRange(video);

            context.SaveChanges();
        }
    }
}
