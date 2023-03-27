using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Converters {
    public static class VideoConverterExtensions {
        public static VideoInfo ToVideoInfoDto(this Video video) {
            return new VideoInfo {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description ?? string.Empty,
                Cover = video.Thumbnail?.ResourceURI ?? string.Empty,
            };
        }

        public static FullVideoInfo ToFullVideoInfoDto(this Video video) {
            return new FullVideoInfo {
                VideoInfo = video.ToVideoInfoDto(),
                Frames = video.Frames.Select(f => f.ToFrameDto()).ToList(),
                Audio = video.Sound.Select(a => a.File.ResourceURI).ToList()
            };
        }
    }
}
