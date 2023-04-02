using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodTube.Shared.Models {
    public class PagedListDto<T> : IEquatable<PagedListDto<T>> {
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
        public override string ToString() {
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
        public string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PagedListDto<T>)obj);
        }

        /// <summary>
        /// Returns true if PagedVideoList instances are equal
        /// </summary>
        /// <param name="other">Instance of PagedVideoList to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PagedListDto<T> other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Page == other.Page ||
                    Page != null &&
                    Page.Equals(other.Page)
                ) &&
                (
                    Total == other.Total ||
                    Total != null &&
                    Total.Equals(other.Total)
                ) &&
                (
                    Limit == other.Limit ||
                    Limit != null &&
                    Limit.Equals(other.Limit)
                ) &&
                (
                    Content == other.Content ||
                    Content != null &&
                    Content.SequenceEqual(other.Content)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Page != null)
                    hashCode = hashCode * 59 + Page.GetHashCode();
                if (Total != null)
                    hashCode = hashCode * 59 + Total.GetHashCode();
                if (Limit != null)
                    hashCode = hashCode * 59 + Limit.GetHashCode();
                if (Content != null)
                    hashCode = hashCode * 59 + Content.GetHashCode();
                return hashCode;
            }
        }
    }
}
