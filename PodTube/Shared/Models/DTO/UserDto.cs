/*
 * PodTube - OpenAPI 3.0
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System.Text;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PodTube.Shared.Models.DTO {
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class UserDto {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id")]
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [DataMember(Name = "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets ProfilePic
        /// </summary>
        [DataMember(Name = "picture")]
        [JsonPropertyName("picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class UserInfo {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Username: ").Append(Name).Append("\n");
            sb.Append("  ProfilePic: ").Append(Picture).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
