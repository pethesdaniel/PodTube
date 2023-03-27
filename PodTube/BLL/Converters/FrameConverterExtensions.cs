using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Converters {
    public static class FrameConverterExtensions {

        public static Shared.Models.Frame ToFrameDto(this DataAccess.Entities.Frame frame) {
            return new Shared.Models.Frame {
                Url = frame.File.ResourceURI,
                TimestampStart = frame.TimeStampStart,
                TimestampEnd = frame.TimeStampEnd
            };
        }
    }
}
