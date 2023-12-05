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
    public class AudioRequestBody {
        [Required]
        [DataMember(Name = "fileId")]
        [JsonPropertyName("fileId")]
        public long FileId { get; set; }

        [Required]
        [DataMember(Name = "index")]
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
