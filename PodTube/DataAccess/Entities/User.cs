using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class User : IdentityUser<long>
    {
        [ForeignKey("File")]
        public long? ProfilePictureId { get; set; }
        public virtual File? ProfilePicture { get; set; }

        public virtual List<Channel> Favorites { get; set; } = new();

        public virtual List<Playlist> Playlists { get; set; } = new();

        public virtual List<File> Files { get; set; } = new();
    }
}
