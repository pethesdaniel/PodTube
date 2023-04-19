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
        [JsonPropertyName("file")]
        public string File { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("timestampStart")]
        public int TimestampStart { get; set; }

        [Required]
        [JsonPropertyName("timestampEnd")]
        public int TimestampEnd { get; set; }
    }
}
