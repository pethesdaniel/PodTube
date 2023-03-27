using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class Sound
    {
        public long Id { get; set; }

        public virtual File File { get; set; } = null!;
        public int FileId { get; set; }
        
    }
}
