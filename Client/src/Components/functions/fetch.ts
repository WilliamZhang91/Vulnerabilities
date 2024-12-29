import config from "../../config";

interface FetchRequestResult {
    [key: string]: string;
};

const baseUrl: string = config.baseUrl;

export const fetchRequest = async (
    path: string,
    method: string,
    csrfToken: string | null,
    setErrorMessage: React.Dispatch<React.SetStateAction<string>>,
    body?: any
): Promise<FetchRequestResult> => {
    try {
        setErrorMessage("");

        const headers: HeadersInit = {
            'Content-Type': "application/json",
        };

        if (csrfToken) {
            headers['RequestVerificationToken'] = csrfToken;
        }

        const options: RequestInit = {
            method,
            headers,
            credentials: "include",
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

