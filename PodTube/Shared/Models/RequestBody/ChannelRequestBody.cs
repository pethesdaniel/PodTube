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
    public class ChannelRequestBody {
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]

        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Cover
        /// </summary>

        [JsonPropertyName("cover")]
        public string Cover { get; set; }
    }
}
