import React, { CSSProperties, useEffect } from 'react';
import SwipeableViews from 'react-swipeable-views';
import { useTheme } from '@mui/material/styles';
import AppBar from '@mui/material/AppBar';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import OsCommandInjection from '../../Pages/OsCommandInjection/OsCommandInjection';
import CrossSiteRequestForgery from '../../Pages/CrossSiteRequestForgery/CrossSiteRequestForgery';
import BrokenAccessControl from '../../Pages/BrokenAccessControl/BrokenAccessControl';
import SqlInjection from '../../Pages/SqlInjection/SqlInjection';
import CrossSiteScripting from '../../Pages/CrossSiteScripting/CrossSiteScripting';
import SignOutButton from '../Buttons/SignOutButton';

interface TabPanelProps {
    children?: React.ReactNode;
    dir?: string;
    index: number;
    value: number;
}

interface Props {
    isAuthenticated: boolean,
    setIsAuthenticated: React.Dispatch<React.SetStateAction<boolean>>,
    token: string,
}

const tabStyles: CSSProperties = {
    height: "80px",
    fontSize: "16px",
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`full-width-tabpanel-${index}`}
            aria-labelledby={`full-width-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box sx={{ p: 3 }}>
                    <Typography>{children}</Typography>
                </Box>
            )}
        </div>
    );
}

function a11yProps(index: number) {
    return {
        id: `full-width-tab-${index}`,
        'aria-controls': `full-width-tabpanel-${index}`,
    };
}

const FullWidthTabs: React.FC<Props> = ({ isAuthenticated, setIsAuthenticated, token }): JSX.Element => {
    const theme = useTheme();
    const [value, setValue] = React.useState<number>(0);
    const [queryResults, setQueryResults] = React.useState<string>("");
    const [errorMessage, setErrorMessage] = React.useState<string>("");

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    const handleChangeIndex = (index: number) => {
        setValue(index);
    };

    return (
        <>
            <Box sx={{ bgcolor: 'background.paper' }}>
                <AppBar position="static">
                    <Tabs
                        value={value}
                        onChange={handleChange}
                        indicatorColor="secondary"
                        textColor="inherit"
                        variant="fullWidth"
                        aria-label="full width tabs example"
                    >
                        <Tab label="Cross Site Scripting (XSS)" {...a11yProps(0)} style={tabStyles} />
                        <Tab label="Command Injection" {...a11yProps(1)} style={tabStyles} />
                        <Tab label="SQL Injection" {...a11yProps(2)} style={tabStyles} />
                        <Tab label="Broken Access" {...a11yProps(3)} style={tabStyles} />
                        <Tab label="Cross Site Request Forgery (CSRF)" {...a11yProps(4)} style={tabStyles} />
                    </Tabs>
                </AppBar>
                <SwipeableViews
                    axis={theme.direction === 'rtl' ? 'x-reverse' : 'x'}
                    index={value}
                    onChangeIndex={handleChangeIndex}
                >
                    <TabPanel value={value} index={0} dir={theme.direction}>
                        <CrossSiteScripting isAuthenticated={isAuthenticated} token={token} />
                    </TabPanel>
                    <TabPanel value={value} index={1} dir={theme.direction}>
                        <OsCommandInjection queryResults={queryResults} setQueryResults={setQueryResults} token={token} />
                    </TabPanel>
                    <TabPanel value={value} index={2} dir={theme.direction}>
                        <SqlInjection />
                    </TabPanel>
                    <TabPanel value={value} index={3} dir={theme.direction}>
                        <BrokenAccessControl />
                    </TabPanel>
                    <TabPanel value={value} index={4} dir={theme.direction}>
                        <CrossSiteRequestForgery isAuthenticated={isAuthenticated} />
                    </TabPanel>
                </SwipeableViews>
            </Box>
            {isAuthenticated && <SignOutButton setIsAuthenticated={setIsAuthenticated} setErrorMessage={setErrorMessage} />}
        </>
    );
};

export default FullWidthTabs;
