namespace PodTube.Shared.Models.DTO {
    public class PlaylistPagedListDto : PagedListDto<PlaylistDto> {
        public PlaylistPagedListDto() { }
        public PlaylistPagedListDto(PagedListDto<PlaylistDto> other) : base(other) { }
    }
}