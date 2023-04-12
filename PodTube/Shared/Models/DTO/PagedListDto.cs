using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodTube.Shared.Models.DTO
{
    public class PagedListDto<T>
    {
        /// <summary>
        /// Gets or Sets Page
        /// </summary>

        [DataMember(Name = "page")]
        public int Page { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Total
        /// </summary>

        [DataMember(Name = "total")]
        public int Total { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Limit
        /// </summary>

        [DataMember(Name = "limit")]
        public int Limit { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Videos
        /// </summary>

        [DataMember(Name = "content")]
        public List<T> Content { get; set; } = new();

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PagedVideoList {\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  Total: ").Append(Total).Append("\n");
            sb.Append("  Limit: ").Append(Limit).Append("\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
