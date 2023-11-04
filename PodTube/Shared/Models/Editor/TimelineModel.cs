using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.Editor {
    public class TimelineModel {
        public List<TimelineItem> VisualItems { get; set; } = new List<TimelineItem>();
        public List<TimelineItem> AudioItems { get; set; } = new List<TimelineItem>();
    }
}
