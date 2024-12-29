import { Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from "@mui/material";
import { CSSProperties, useState } from "react";
import { form } from "../../Components/Styling/MuiComponentsStyling";

import config from "../../config";

const input: CSSProperties = {
    margin: "0px 20px 0 00px",
};

const tableHeading: CSSProperties = {
    fontSize: '1.2rem', 
    fontWeight: 'bold', 
};

interface Response {
    name: string,
    email: string,
    address: string,
};

const baseUrl: string = config.baseUrl;
const path: string = "Profile/Search";
const url: string = `${baseUrl}/${path}`

const SqlInjection: React.FC = (): JSX.Element => {

    //Be able to view profile after selecting

    const [searchValue, setSearchValue] = useState<string>("");
    const [searchResults, setSearchResults] = useState<Response[]>([]);
    const [errorMessage, setErrorMessage] = useState<string>("");

    const onChangeInput = (e: React.ChangeEvent<HTMLInputElement>): void => {
        const value = e.target.value;
        setSearchValue(value);
    };

    const searchUser = async (): Promise<void> => {
        setErrorMessage("");
        setSearchResults([]);

        try {
            const response = await fetch(`${url}?searchValue=${encodeURIComponent(searchValue)}`);
            console.log({ response })

            if (response.status === 200) {
                const data = await response.json();
                console.log({ data })
                if (data.length > 0) {
                    setSearchResults(data);
                }
            } else {
                setErrorMessage("Server error");
            }
        } catch (err) {
            console.error(err);
            setErrorMessage("Something went wrong")
        }
    };

    return <div className="sql-injection-page">
        <Typography variant="h5">
            Search User (hint %' OR '1'='1)
        </Typography>
        <br />
        <form style={form}>
            <TextField
                sx={input}
                label="Name"
                variant="outlined"
                value={searchValue}
                onChange={onChangeInput}
            />
        </form>
        <Button
            variant="contained"
            size="large"
            style={{ margin: "20px 10px 0 0" }}
            onClick={searchUser}
        >
            Search
        </Button>
        <div style={{ marginTop: "50px" }}>
            {searchResults.length > 0 && <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={tableHeading}>User</TableCell>
                            <TableCell align="right" sx={tableHeading}>Name</TableCell>
                            <TableCell align="right" sx={tableHeading}>Email</TableCell>
                            <TableCell align="right" sx={tableHeading}>Address</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {searchResults.map((row, index) => (
                            <TableRow
                                key={row.name}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {index + 1}
                                </TableCell>
                                <TableCell align="right">{row.name}</TableCell>
                                <TableCell align="right">{row.email}</TableCell>
                                <TableCell align="right">{row.address}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>}
            {errorMessage && <Typography variant="h6">{errorMessage}</Typography>}
        </div>
    </div>
};

export default SqlInjection;    