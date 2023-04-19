using System.Runtime.Serialization;
using System.Text;

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

        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class PlaylistInfo {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Cover: ").Append(Thumbnail).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
