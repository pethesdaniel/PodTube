using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities {
    public class Playlist {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public virtual User? Owner { get; set; }
        public long? OwnerId { get; set; }

        public virtual File? Picture { get; set; }
        public long? PictureId { get; set; }

        public virtual List<PlaylistVideo> Videos { get; set; } = new();
    }
}
