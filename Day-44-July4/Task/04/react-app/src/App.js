import { useEffect, useState } from 'react';
import './App.css';

const App = () => {
  const [message, setMessage] = useState('Loading...');

  useEffect(() => {
    const fetchMessage = async () => {
      try {
        const res = await fetch('/hello');
        console.log(res)
        if (!res.ok) throw new Error('Network response was not ok');
        const data = await res.text();
        setMessage(data);
      } catch (error) {
        console.error('Fetch error:', error);
        setMessage('Failed to fetch from backend');
      }
    };

    fetchMessage();
  }, []);

  return (
    <div style={{ textAlign: 'center' }}>
      <h1>React Frontend 🥳</h1>
      <p>{message}</p>
    </div>
  );
};

export default App;
