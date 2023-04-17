namespace PodTube.Shared.Models.DTO {
    public class VideoPagedListDto : PagedListDto<VideoDto> {
        public VideoPagedListDto() { }
        public VideoPagedListDto(PagedListDto<VideoDto> other) : base(other) { }
    }
}