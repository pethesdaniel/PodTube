/*
 * PodTube - OpenAPI 3.0
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PodTube.Shared.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class FullChannelInfo : IEquatable<FullChannelInfo>
    { 
        /// <summary>
        /// Gets or Sets ChannelInfo
        /// </summary>
        [Required]

        [DataMember(Name="channelInfo")]
        public ChannelInfoWithOwner ChannelInfo { get; set; }

        /// <summary>
        /// Gets or Sets Videos
        /// </summary>

        [DataMember(Name="videos")]
        public List<VideoInfo> Videos { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FullChannelInfo {\n");
            sb.Append("  ChannelInfo: ").Append(ChannelInfo).Append("\n");
            sb.Append("  Videos: ").Append(Videos).Append("\n");
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

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FullChannelInfo)obj);
        }

        /// <summary>
        /// Returns true if FullChannelInfo instances are equal
        /// </summary>
        /// <param name="other">Instance of FullChannelInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(FullChannelInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ChannelInfo == other.ChannelInfo ||
                    ChannelInfo != null &&
                    ChannelInfo.Equals(other.ChannelInfo)
                ) && 
                (
                    Videos == other.Videos ||
                    Videos != null &&
                    Videos.SequenceEqual(other.Videos)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (ChannelInfo != null)
                    hashCode = hashCode * 59 + ChannelInfo.GetHashCode();
                    if (Videos != null)
                    hashCode = hashCode * 59 + Videos.GetHashCode();
                return hashCode;
            }
        }
    }
}
