export function getCurrentTime(element) {
    if (!element) {
        return 0;
    }
    return element.currentTime ?? 0;
}

export function getDuration(element) {
    if (!element) {
        return 0;
    }
    return element.duration ?? 0;
}

export function getVolume(element) {
    if (!element) {
        return 0;
    }
    return element.volume ?? 0;
}

export function getIsPaused(element) {
    if (!element) {
        return false;
    }
    return element.paused ?? false;
}

export function play(element) {
    return element.play();
}

export function pause(element) {
    return element.pause();
}

export function setCurrentTime(element, value) {
    if (!element || value === undefined || value === null) {
        return;
    }
    element.currentTime = value;
}

export function setVolume(element, value) {
    if (!element || value === undefined || value === null) {
        return;
    }
    element.volume = value;
}