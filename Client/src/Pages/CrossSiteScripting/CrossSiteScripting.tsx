import { Avatar, Box, Button, Divider, Grid, Paper, TextField, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { fetchRequest } from '../../Components/functions/fetch';
import { dateFormatter } from '../../Components/functions/dateFormatter';

interface Comment {
    commentText: string;
    createdAt: string;
}

interface Props {
    isAuthenticated: boolean;
    token: string,
}

const CrossSiteScripting: React.FC<Props> = ({ isAuthenticated, token }): JSX.Element => {

    const [comments, setComments] = useState<Comment[]>([]);
    const [commentToSubmit, setCommentToSubmit] = useState<string>("");
    const [errorMessage, setErrorMessage] = useState<string>("");

    const onChange = (e: React.ChangeEvent<HTMLTextAreaElement>): void => {
        setCommentToSubmit(e.target.value);
    }

    const submitComment = async (): Promise<void> => {
        setErrorMessage("");
        try {
            const response = await fetchRequest("Comment", "POST", null, setErrorMessage, commentToSubmit);
            await retrieveComments();
        } catch (err: any) {
            console.error(err);
            setErrorMessage("Could not post comment");
        }
    }

    const retrieveComments = async (): Promise<void> => {
        try {
            const response = await fetchRequest("Comment", "GET", token, setErrorMessage);
            setComments(response);
        } catch (err: any) {
            console.error(err);
            setErrorMessage("Could not retrieve comments");
        }
    }

    useEffect(() => {
        (async () => {
            retrieveComments();
        })();
    }, [isAuthenticated]);


    return <div className="cross-site-scripting">
        <Typography variant="h5">
            Cross Site Scripting
        </Typography>
        <br />
        <Box
            component="form"
            sx={{ '& .MuiTextField-root': { m: 0, width: '100%' } }}
            noValidate
            autoComplete="off"
        >
            <TextField
                id="outlined-multiline-static"
                label="Enter Comment"
                multiline
                rows={7}
                value={commentToSubmit}
                onChange={onChange}
            />
        </Box>
        <Button
            variant="contained"
            size="large"
            style={{ margin: "20px 10px 0 0" }}
            onClick={submitComment}
        >
            Submit
        </Button>
        <div style={{ marginTop: "20px" }}>
            {errorMessage && <div>{errorMessage}</div>}
            {comments && <Paper style={{ padding: "10px 20px", paddingBottom: "40px" }}>
                {comments && comments.map((c, i) => {
                    return <>
                        <Grid style={{ marginTop: "10px" }} container wrap="nowrap" spacing={3} key={i}>
                            <Grid item>
                                <Avatar alt="Remy Sharp" />
                            </Grid>
                            <Grid justifyContent="left" item xs zeroMinWidth>
                                <h4 style={{ textAlign: "left" }}>User</h4>
                                <p>{c.commentText}</p>
                                <p style={{ textAlign: "left", color: "gray" }}>
                                    {dateFormatter(c.createdAt)}
                                </p>
                            </Grid>
                        </Grid>
                        {i !== comments.length - 1 && <Divider variant="fullWidth" style={{ margin: "30px 0" }} />}
                    </>
                })}
            </Paper>}
        </div>
    </div>
}

export default CrossSiteScripting
