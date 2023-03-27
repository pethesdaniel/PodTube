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
    public partial class PagedChannelList : IEquatable<PagedChannelList>
    { 
        /// <summary>
        /// Gets or Sets Page
        /// </summary>

        [DataMember(Name="page")]
        public long? Page { get; set; }

        /// <summary>
        /// Gets or Sets Total
        /// </summary>

        [DataMember(Name="total")]
        public long? Total { get; set; }

        /// <summary>
        /// Gets or Sets Limit
        /// </summary>

        [DataMember(Name="limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// Gets or Sets Channels
        /// </summary>

        [DataMember(Name="channels")]
        public List<ChannelInfo> Channels { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PagedChannelList {\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  Total: ").Append(Total).Append("\n");
            sb.Append("  Limit: ").Append(Limit).Append("\n");
            sb.Append("  Channels: ").Append(Channels).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PagedChannelList)obj);
        }

        /// <summary>
        /// Returns true if PagedChannelList instances are equal
        /// </summary>
        /// <param name="other">Instance of PagedChannelList to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PagedChannelList other)
        {
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
                    Channels == other.Channels ||
                    Channels != null &&
                    Channels.SequenceEqual(other.Channels)
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
                    if (Page != null)
                    hashCode = hashCode * 59 + Page.GetHashCode();
                    if (Total != null)
                    hashCode = hashCode * 59 + Total.GetHashCode();
                    if (Limit != null)
                    hashCode = hashCode * 59 + Limit.GetHashCode();
                    if (Channels != null)
                    hashCode = hashCode * 59 + Channels.GetHashCode();
                return hashCode;
            }
        }
    }
}
