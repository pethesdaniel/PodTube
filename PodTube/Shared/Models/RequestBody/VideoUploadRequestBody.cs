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
    public class VideoUploadRequestBody {
        [Required]
        [DataMember(Name = "channelId")]
        [JsonPropertyName("channelId")]
        public long ChannelId { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        [DataMember(Name = "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [DataMember(Name = "frames")]
        [JsonPropertyName("frames")]
        public List<FrameRequestBody> Frames { get; set; } = new();

        [DataMember(Name = "audios")]
        [JsonPropertyName("audios")]
        public List<AudioRequestBody> Audios { get; set; } = new();
    }
}
