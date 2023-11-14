namespace PodTube.Client.Components.Videos.VideoPlayer {
    public class AudioController {
        public event Action<string> PlayAudioWithUrlEvent;
        public event Action<string> PauseAudioWithUrlEvent;
        public event Action<string, double> SeekAudioWithUrlEvent;
        public event Action<double> SetVolumeEvent;

        public event Action<AudioMetadata> OnMetadataLoadedEvent;
        public event Action<AudioPlayerState> OnTimeUpdatedEvent;


        public void PlayAudioWithUrl(string url) {
            PlayAudioWithUrlEvent?.Invoke(url);
        }

        public void PauseAudioWithUrl(string url) {
            PauseAudioWithUrlEvent?.Invoke(url);
        }

        public void SeekAudioWithUrl(string url, double time) {
            SeekAudioWithUrlEvent?.Invoke(url, time);
        }

        public void SetVolume(double volume) {
            SetVolumeEvent?.Invoke(volume);
        }

        public void OnTimeUpdated(AudioPlayerState metadata) {
            OnTimeUpdatedEvent?.Invoke(metadata);
        }

        public void OnMetadataLoaded(AudioMetadata metadata) {
            OnMetadataLoadedEvent?.Invoke(metadata);
        }
    }
}
