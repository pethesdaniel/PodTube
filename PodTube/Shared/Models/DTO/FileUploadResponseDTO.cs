using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.DTO {
    public class FileUploadResponseDTO {
        public string Url { get; set; } = "";
        public string MimeType { get; set; } = "";
        public long FileId { get; set; }
    }
}
