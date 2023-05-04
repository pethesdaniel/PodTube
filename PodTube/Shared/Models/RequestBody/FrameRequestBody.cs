using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodTube.Shared.Models.RequestBody {
    public class FrameRequestBody {
        [Required]
        [DataMember(Name = "file")]
        [JsonPropertyName("file")]
        public string File { get; set; } = string.Empty;

        [Required]
        [DataMember(Name = "timestampStart")]
        [JsonPropertyName("timestampStart")]
        public int TimestampStart { get; set; }

        [Required]
        [DataMember(Name = "timestampEnd")]
        [JsonPropertyName("timestampEnd")]
        public int TimestampEnd { get; set; }
    }
}
