using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.Editor;

namespace PodTube.Client.Editor.Util {
    public class FileConverter {
        public static async Task<Asset> AssetFromFileUploadDto(FileUploadResponseDTO dto, HttpClient client) {
            var data = await client.GetByteArrayAsync(dto.Url);
            return new Asset {
                //Name = NewItemName == "" ? substituteFilename : NewItemName,
                Name = !string.IsNullOrEmpty(dto.UserFriendlyName) ? dto.UserFriendlyName : dto.Url.Split("/").LastOrDefault(""),
                Url = dto.Url,
                IsAudio = dto.MimeType.Contains("audio"),
                Data = data,
                MimeType = dto.MimeType,
                FileId = dto.FileId,
                UsedElsewhere = dto.IsUsedElsewhere
            };
        }
    }
}
