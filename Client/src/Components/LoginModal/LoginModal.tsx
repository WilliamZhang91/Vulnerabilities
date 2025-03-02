import "./LoginModal.css";
import React, { CSSProperties } from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import { useState } from "react";
import { Button, Fade, TextField, Typography, Zoom } from "@mui/material";
import config from '../../config';
import getFromCookie from "../functions/getFromCookie";

const modalContent = {
    position: "fixed",
    zIndex: 1,
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    borderRadius: "10px",
    boxShadow: 24,
    p: 4,
};

const form: CSSProperties = {
    display: "flex",
    flexDirection: "column"
};

const input: CSSProperties = {
    margin: "20px 0",
};

interface Props {
    isAuthenticated: boolean,
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>,
    setToken: React.Dispatch<React.SetStateAction<string>>,
};

interface LoginCredentials {
    username: string,
    password: string,
}

interface LoginResponse {
    id: number,
    username: string,
    role: string,
    statusCode: number,
    status: string,
    access_token: string | null,
    refresh_token: string | null,
}

const baseUrl: string = config.baseUrl;
const path: string = "Auth/Login";
const url: string = `${baseUrl}/${path}`

const LoginModal: React.FC<Props> = ({ isAuthenticated, setIsAuthenticated, setToken }): JSX.Element => {
    const [open, setOpen] = useState<boolean>(true);
    const [checked, setChecked] = useState<boolean>(true);
    const handleOpen = (): void => setOpen(true);
    const handleClose = (): void => setOpen(false);
    const [loginCredentials, setLoginCredentials] = useState<LoginCredentials>({
        username: "",
        password: ""
    });
    const [errorMessage, setErrorMessage] = useState("");

    const onChangeInput = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
        key: keyof LoginCredentials
    ): void => {
        const value: string = e.target.value;
        setLoginCredentials(prev => ({
            ...prev,
            [key]: value,
        }));
    };

    const login = async (): Promise<void> => {
        console.log("Login clicked")
        try {
            const response = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(loginCredentials),
                credentials: 'include' // Required for sending and receiving cookies 
            });

            const data: LoginResponse = await response.json();
            console.log({ data });

            if (data.statusCode === 200) {
                setIsAuthenticated(true);
                // const token = getFromCookie("XSRF-TOKEN");
                // const token = getFromCookie("__RequestVerificationToken");
                const jwtToken = data.access_token;
                console.log({ jwtToken })
                if (jwtToken) {
                    setIsAuthenticated(true);
                    setToken(jwtToken);
                } else {
                    setErrorMessage("Could not generate token");
                }
            } else {
                setErrorMessage("Incorrect username or password");
            }
        } catch (err) {
            setErrorMessage("Something went wrong");
        }
    };

    return (
        !isAuthenticated ? <div className="login-modal">
            <Modal
                open={open}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Fade in={open}>
                    <Box sx={modalContent}>
                        <Typography variant="h5">
                            Login
                        </Typography>
                        <form style={form}>
                            <TextField
                                sx={input}
                                id="username"
                                label="Username"
                                variant="outlined"
                                value={loginCredentials.username}
                                onChange={(e) => onChangeInput(e, "username")}
                            />
                            <TextField
                                sx={input}
                                id="password"
                                label="Password"
                                variant="outlined"
                                value={loginCredentials.password}
                                onChange={(e) => onChangeInput(e, "password")}
                            />
                        </form>
                        <Button
                            variant="contained"
                            size="large"
                            style={{ margin: "20px 10px 0 0" }}
                            onClick={login}
                        >
                            Login
                        </Button>
                        <Button
                            variant="contained"
                            size="large"
                            style={{ margin: "20px 10px 0 0" }}
                        >
                            Sign up
                        </Button>
                    </Box>
                </Fade>
            </Modal>
        </div>
            :
            <div></div>
    );
};

export default LoginModal;