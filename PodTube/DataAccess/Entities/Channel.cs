using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class Channel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual User? Owner { get; set; }
        public long? OwnerId { get; set; }

        public virtual File? Thumbnail { get; set; }
        public long? ThumbnailId { get; set; }

        public virtual List<Video> Videos { get; set; } = new();
    }
}
