﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.DataAccess.Entities
{
    public class File
    {
        public long Id { get; set; }
        public string ResourceURI { get; set; } = null!;

        public string MimeType { get; set; } = null!;
    }
}
