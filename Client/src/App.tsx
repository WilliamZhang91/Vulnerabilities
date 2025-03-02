import { useEffect, useState } from 'react';
import './App.css';
import FullWidthTabs from './Components/Tabs/FullWidthTabs';
import LoginModal from './Components/LoginModal/LoginModal';

function App() {

  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [token, setToken] = useState<string>("");

  useEffect(() => {
    console.log(token)
  }, [token])

  return (
    <div className="App">
      <LoginModal
        isAuthenticated={isAuthenticated}
        setIsAuthenticated={setIsAuthenticated}
        setToken={setToken}
      />
      <FullWidthTabs
        isAuthenticated={isAuthenticated}
        setIsAuthenticated={setIsAuthenticated}
        token={token}
      />
    </div>
  );
}

export default App;