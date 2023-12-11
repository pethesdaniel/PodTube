export function createWaveform(htmlElement, url, media) {
    console.log("Test")
    return WaveSurfer.create({
        container: htmlElement,
        waveColor: '#4F4A85',
        progressColor: '#383351',
        media: media,
        url: url,
        interact: false
    })
}
export function getDuration(element) {
    if (!element) {
        return 0;
    }
    return element.duration ?? 0;
}