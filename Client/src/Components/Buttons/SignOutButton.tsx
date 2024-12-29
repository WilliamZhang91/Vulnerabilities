import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';

export default function SignOutButton() {
    return (
        <div style={{ display: "flex", justifyContent: "flex-end", width: "100%" }}>
            <Stack spacing={2} direction="row" sx={{ mt: 5, mb: 0, mx: 3 }}>
                <Button variant="contained">Sign out</Button>
            </Stack>
        </div>
    );
}