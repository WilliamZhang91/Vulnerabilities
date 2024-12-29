import { CSSProperties, useState } from "react";
import Results from "../../Components/Results/Results";
import "./OsCommandInjection.css";
import { fetchRequest } from "../../Components/functions/fetch";
import { Button, TextField, Typography } from "@mui/material";
import { form } from "../../Components/Styling/MuiComponentsStyling";

interface Props {
    queryResults: string,
    setQueryResults: React.Dispatch<React.SetStateAction<string>>,
    csrfToken: string
}

const OsCommandInjection: React.FC<Props> = ({ queryResults, setQueryResults, csrfToken }): JSX.Element => {

    const [inputValue, setInputValue] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>("");

    const handleInputValue = (e: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(e.target.value);
    };

    const submitForm = async (e: React.MouseEvent<HTMLButtonElement | HTMLAnchorElement>): Promise<any> => {
        setQueryResults("");
        e.preventDefault();
        setIsLoading(true);
        try {
            const response = await fetchRequest("PingWebsite/ping", "POST", csrfToken, setErrorMessage, inputValue);
            setQueryResults(response.output)
        } catch (err: any) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
    };

    return <div className="command-injection-page">
        <Typography variant="h5">
            Ping IP/Website
        </Typography>
        <br />
        <form className="command-injection-form" >
            <TextField
                sx={form}
                label="Ping website/IP address"
                variant="outlined"
                value={inputValue}
                onChange={handleInputValue}
            />
        </form>
        <Button
            variant="contained"
            size="large"
            style={{ margin: "20px 10px 0 0" }}
            onClick={submitForm}
        >
            Search
        </Button>
        <div style={{marginTop: "20px"}}>
            {isLoading && <div>Retrieving...</div>}
            {errorMessage && <div>{errorMessage}</div>}
            {queryResults ? <Results results={queryResults} /> : null}
        </div>
    </div>
};

export default OsCommandInjection;
