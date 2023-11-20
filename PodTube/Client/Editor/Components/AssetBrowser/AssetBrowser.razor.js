export function LoadBlob(url) {
    return await fetch(url).then(r => r.blob()).;
}