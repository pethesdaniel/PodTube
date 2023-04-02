using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class Frame
    {
        public long Id { get; set; }

        public virtual File File { get; set; } = null!;
        public long FileId { get; set; }

        public int TimeStampStart { get; set; }
        public int TimeStampEnd { get; set; }
    }
}
