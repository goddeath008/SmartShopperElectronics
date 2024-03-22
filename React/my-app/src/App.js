import logo from './logo.svg';
import './App.css';
import Game from './components/tictactoe/game';



function App() {
  return (
    
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Công Thái.
        </p>
       <Game></Game>
       
      </header>
    </div>
  );
}

export default App;
