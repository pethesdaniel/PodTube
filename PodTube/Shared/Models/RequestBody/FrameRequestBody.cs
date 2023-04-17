using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodTube.Shared.Models.RequestBody {
    public class FrameRequestBody {
        [Required]
        [DataMember(Name = "filename")]
        public string File { get; set; }

        [Required]
        [DataMember(Name = "timestampStart")]
        public int TimestampStart { get; set; }

        [Required]
        [DataMember(Name = "timestampEnd")]
        public int TimestampEnd { get; set; }
    }
}
