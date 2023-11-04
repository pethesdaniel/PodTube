using PodTube.Shared.Models.Editor;

namespace PodTube.Client.Editor.Services {
    public class TimelineService {
        public TimelineModel Timeline { get; set; } = new TimelineModel();

        public void AddAssetToTimeline(Asset asset) {
            if (asset.IsAudio) {
                Timeline.AudioItems.Add(new TimelineItem {
                    Asset = asset,
                    Duration = 100
                });
            } else {
                Timeline.VisualItems.Add(new TimelineItem {
                    Asset = asset,
                    Duration = 100
                });
            }
        }
    }
}
