export const dateFormatter = (timestamp: string) => {
    const date = new Date(timestamp);
    const day = String(date.getDate()).padStart(2, '0'); // Ensures 2-digit day
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Ensures 2-digit month
    const year = date.getFullYear();
    const hours = String(date.getHours()).padStart(2, '0'); // Ensures 2-digit hours
    const minutes = String(date.getMinutes()).padStart(2, '0'); // Ensures 2-digit minutes
    const seconds = String(date.getSeconds()).padStart(2, '0'); // Ensures 2-digit seconds

    const formattedDate = `${day}/${month}/${year} ${hours}:${minutes}:${seconds} UTC`;

    return formattedDate;
}