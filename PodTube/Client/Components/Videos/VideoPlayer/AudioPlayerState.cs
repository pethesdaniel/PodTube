namespace PodTube.Client.Components.Videos.VideoPlayer {
    public class AudioPlayerState {
        public string Url { get; set; }
        public double CurrentTime { get; set; }
        public double Duration { get; set; }
        public bool IsPaused { get; set; }
        public double Volume { get; set; }
    }
}
