namespace PodTube.Shared.Models.DTO {
    public class ChannelPagedListDto : PagedListDto<ChannelDto> {
        public ChannelPagedListDto(){ }
        public ChannelPagedListDto(PagedListDto<ChannelDto> other) : base(other) { }
    }
}
