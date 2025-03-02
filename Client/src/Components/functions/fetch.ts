import config from "../../config";

const baseUrl: string = config.baseUrl;

export const fetchRequest = async (
    path: string,
    method: string,
    token: string | null,
    setErrorMessage: React.Dispatch<React.SetStateAction<string>>,
    body?: any
): Promise<any> => {
    try {
        setErrorMessage("");

        const headers: HeadersInit = {
            'Content-Type': "application/json",
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        const options: RequestInit = {
            method,
            headers,
        };

        if (method === "POST" && body) {
            options.body = JSON.stringify(body);
        }

        const response = await fetch(`${baseUrl}/${path}`, options);

        if (!response.ok) {
            const errorText = await response.text();
            setErrorMessage(`Error: ${errorText}`);
            throw new Error(`Request failed with status ${response.status}: ${errorText}`);
        }

        const result = await response.json();
        return result;

    } catch (e: any) {
        setErrorMessage("Something went wrong");
        throw e;
    }
};

