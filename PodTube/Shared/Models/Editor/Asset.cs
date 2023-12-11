﻿using PodTube.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.Editor {
    public class Asset {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsAudio { get; set; } = false;

        public byte[] Data { get; set; } = Array.Empty<byte>();
        public string MimeType { get; set; } = "";

        public long FileId { get; set; }

        public bool UsedElsewhere { get; set; } = false;

        public bool Deleted { get; set; } = false;

    }
}
