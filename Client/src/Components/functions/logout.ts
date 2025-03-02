import { fetchRequest } from "./fetch"

export const logout = async (
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>,
    setErrorMessage: React.Dispatch<React.SetStateAction<string>>
) => {

    const url = "api/auth/Logout";
    const method = "POST";

    try {
        await fetchRequest(url, method, null, setErrorMessage);
    } catch {
        console.error("Could not log out properly")
    } finally {
        setIsAuthenticated(false)
    }
}
