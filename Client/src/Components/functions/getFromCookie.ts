const getFromCookie = (name: string): string | null => {
    const cookieArray = document.cookie.split(";");
    for (let cookie of cookieArray) {
        cookie = cookie.trim();
        if (cookie.startsWith(name + "=")) {
            return cookie.substring(name.length + 1);
        }
    }
    return null;
}

export default getFromCookie;
