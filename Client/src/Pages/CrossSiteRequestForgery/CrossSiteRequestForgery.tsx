import { CSSProperties, useEffect, useState } from "react";
import "./CrossSiteRequestForgery.css";
import config from '../../config';
import { Button, TextField, Typography } from "@mui/material";
import { form } from "../../Components/Styling/MuiComponentsStyling";

const baseUrl: string = config.baseUrl;

interface ProfileToUpdate {
    name: string;
    email: string;
    address: string;
}

interface Profile extends ProfileToUpdate {
    id: number
}

interface Props {
    isAuthenticated: boolean,
}

const CrossSiteRequestForgery: React.FC<Props> = ({ isAuthenticated }): JSX.Element => {

    const [profile, setProfile] = useState<ProfileToUpdate>({
        name: "",
        email: "",
        address: ""
    });

    const [message, setMessage] = useState("");


    return <div className="csrf-page">
        {
            !isAuthenticated ?
                <Typography variant="h5">Unauthorised</Typography>
                :
                <div>
                    <Typography variant="h5">Update Profile</Typography>
                    <br />
                    <form >
                        <TextField
                            sx={form}
                            type="text"
                            variant="outlined"
                            value={profile.name}
                            label="Name"
                        />

                        <TextField
                            sx={form}
                            type="text"
                            variant="outlined"
                            value={profile.email}
                            label="Email"
                        />
                        <TextField
                            sx={form}
                            type="text"
                            variant="outlined"
                            value={profile.address}
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
};

export default CrossSiteRequestForgery;