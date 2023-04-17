using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class Video
    {
        public long Id { get; set; }

        public virtual List<Frame> Frames { get; set; } = new();

        public virtual File Audio { get; set; } = new ();
        public long AudioId { get; set; }

        public virtual Channel Channel { get; set; } = null!;
        public long ChannelId { get; set; }

        public string Name { get; set; } = null!;
        public DateTime DatePublished { get; set; } = new();
        public string? Description { get; set; }

        public virtual File? Thumbnail { get; set; }
        public virtual long? ThumbnailId { get; set; }

        public virtual List<PlaylistVideo> Playlists { get; set; } = new();
    }
}
