using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.DTO {
    [DataContract]
    public class PlaylistBasicDto {
        [DataMember(Name = "id")]
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [DataMember(Name = "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
