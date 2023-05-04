using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace PodTube.Shared.Models.DTO {
    public class PagedListDto<T> {
        public PagedListDto() { }
        public PagedListDto(PagedListDto<T> other) {
            Page = other.Page;
            Total = other.Total;
            Limit = other.Limit;
            Content = other.Content;
        }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [DataMember(Name = "page")]
        [JsonPropertyName("page")]
        public int Page { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Total
        /// </summary>
        [DataMember(Name = "total")]
        [JsonPropertyName("total")]
        public int Total { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Limit
        /// </summary>
        [DataMember(Name = "limit")]
        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 0;

        /// <summary>
        /// Gets or Sets Videos
        /// </summary>
        [DataMember(Name = "content")]
        [JsonPropertyName("content")]
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
    }
}
