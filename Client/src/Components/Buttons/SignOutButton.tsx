import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { logout } from '../functions/logout';

interface Props {
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    setErrorMessage: React.Dispatch<React.SetStateAction<string>>;
}

const SignOutButton:React.FC<Props> = ({
    setIsAuthenticated,
    setErrorMessage
}) => {

    const signOff = async (): Promise<void> => {
        await logout(setIsAuthenticated, setErrorMessage);
    }

    return (
        <div style={{ display: "flex", justifyContent: "flex-end", width: "100%" }}>
            <Stack spacing={2} direction="row" sx={{ mt: 5, mb: 0, mx: 3 }}>
                <Button color="error" variant="outlined" onClick={signOff}>Sign out</Button>
            </Stack>
        </div>
    );
}

export default SignOutButton