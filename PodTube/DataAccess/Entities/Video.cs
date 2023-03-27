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

        public virtual List<Sound> Sound { get; set; } = new ();
        public int SoundId { get; set; }

        public virtual Channel Channel { get; set; } = null!;
        public int ChannelId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual File? Thumbnail { get; set; }

        // TODO: Playlists???
    }
}
