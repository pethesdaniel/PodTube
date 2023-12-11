using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.DomainObject {
    public class FileCreate {
        public long UserId { get; set; }
        public string Path { get; set; } = null!;
        public string MimeType { get; set; } = "";
        public string UserFriendlyName { get; set; } = "";
    }
}
