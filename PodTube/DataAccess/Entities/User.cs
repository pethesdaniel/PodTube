using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual File? ProfilePicture { get; set; }
        public long? ProfilePictureId { get; set; }

        public virtual List<Channel> Favorites { get; set; } = new();

        public virtual List<Playlist> Playlists { get; set; } = new();
    }
}
