import { useState } from 'react';
import './App.css';
import FullWidthTabs from './Components/Tabs/FullWidthTabs';
import LoginModal from './Components/LoginModal/LoginModal';

function App() {

  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [csrfToken, setCsrfToken] = useState<string>("");

  return (
    <div className="App">
      <LoginModal
        isAuthenticated={isAuthenticated}
        setIsAuthenticated={setIsAuthenticated}
        setCsrfToken={setCsrfToken}
      />
      <FullWidthTabs isAuthenticated={isAuthenticated} csrfToken={csrfToken} />
    </div>
  );
}

export default App;