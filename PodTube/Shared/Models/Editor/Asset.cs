using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.Editor {
    public class Asset {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsAudio { get; set; } = false;
    }
}
