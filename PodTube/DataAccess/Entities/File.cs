using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class File
    {
        public long Id { get; set; }
        public string ResourceURI { get; set; } = null!;
        public string MimeType { get; set; } = null!;
        [ForeignKey("User")]
        public long? OwnerId { get; set; }
        public virtual User? Owner { get; set; }

        public string UserFriendlyName { get; set; } = "";

        public virtual List<Audio> Audios { get; set; } = new();
        public virtual List<Frame> Frames { get; set; } = new();
        public virtual List<Channel> ChannelThumbnails { get; set; } = new();
        public virtual List<Playlist> PlaylistThumbnails { get; set; } = new();
        public virtual List<Video> VideoThumbnails { get; set; } = new();
        public virtual List<User> ProfilePictures { get; set; } = new();

        public bool IsUsed() {
            return !(Audios.Count() == 0
                && Frames.Count() == 0
                && ChannelThumbnails.Count() == 0
                && VideoThumbnails.Count() == 0
                && ProfilePictures.Count() == 0
                && PlaylistThumbnails.Count() == 0);
        }
    }
}
