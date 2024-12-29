import { useState } from "react";
import "./BorkenAccessControl.css";
import { Button, TextField, Typography } from "@mui/material";
import { form } from "../../Components/Styling/MuiComponentsStyling";

interface ProfileToCreate {
    name: string;
    email: string;
    address: string;
}

const BrokenAccessControl: React.FC = (): JSX.Element => {

    const [isAuthenticatedAdmin, setIsAuthenticatedAdmin] = useState<boolean>(true);
    const [message, setMessage] = useState<string>("");
    const [profile, setProfile] = useState<ProfileToCreate>({
        name: "",
        email: "",
        address: ""
    });


    const handleChange = () => {

    }

    return <div className="broken-access-page">
        {
            isAuthenticatedAdmin ?
                <Typography variant="h5">
                    Admin panel
                </Typography>
                :
                <div>
                    <Typography variant="h5">
                        Admin panel
                    </Typography>
                    <br />
                    <form>
                        <TextField
                            sx={form}
                            value={profile.name}
                            variant="outlined"
                            onChange={handleChange}
                            label="Name"
                        />
                        <TextField
                            sx={form}
                            value={profile.email}
                            variant="outlined"
                            onChange={handleChange}
                            label="Email"
                        />
                        <TextField
                            sx={form}
                            value={profile.address}
                            variant="outlined"
                            onChange={handleChange}
                            label="Address"
                        />
                        <Button
                            variant="contained"
                            size="large"
                            style={{ margin: "20px 10px 0 0" }}
                        >
                            Submit
                        </Button>
                    </form>
                    <div>{message}</div>
                </div>
        }

    </div>
}

export default BrokenAccessControl;