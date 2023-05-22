using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class User : IdentityUser<long>
    {
        public virtual File? ProfilePicture { get; set; }
        public long? ProfilePictureId { get; set; }

        public virtual List<Channel> Favorites { get; set; } = new();

        public virtual List<Playlist> Playlists { get; set; } = new();
    }
}
