using Newtonsoft.Json;
using PodTube.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodTube.Shared.Models.RequestBody {
    public class PlaylistRequestBody {
        /// <summary>
        /// Gets or Sets Name
        /// </summary>

        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Cover
        /// </summary>

        [DataMember(Name = "cover")]
        public string Cover { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class PlaylistInfo {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Cover: ").Append(Cover).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
