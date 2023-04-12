using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PodTube.DataAccess.Entities {
    [PrimaryKey(nameof(PlaylistId), nameof(VideoId), nameof(Index))]
    public class PlaylistVideo {
        public virtual Playlist Playlist { get; set; } = new();
        public virtual Video Video { get; set; } = new();
        public long PlaylistId { get; set; }
        public long VideoId { get; set; }
        public long Index { get; set; }
    }
}
