import { fetchRequest } from "./fetch"

export const logout = async (
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>,
) => {
    setIsAuthenticated(false);
}
