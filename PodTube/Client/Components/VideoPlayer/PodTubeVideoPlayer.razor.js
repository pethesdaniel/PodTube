export function getCurrentTime(element) {
    if (!element) {
        return 0;
    }
    return element.currentTime;
}

export function getDuration(element) {
    if (!element) {
        return 0;
    }
    return element.duration;
}

export function getIsPaused(element) {
    if (!element) {
        return false;
    }
    return element.paused;
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