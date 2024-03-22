import React, { useState } from 'react';
import Board from './Board';
import "./GameStyle.css";
import { caculatorWinner } from '../../ultils/helpers';

const Game = () => {
    // const [board, setBoard]= useState(Array(9).fill(null));
    // const [xIsNext, setXIsNext]= useState(true);
    const [state, setState] = useState({
        board: Array(9).fill(null),
        xIsNext: true,
    });
    
    const winner = caculatorWinner(state.board);
    const HandleClick =(index)=>{
        const boardCopy = [... state.board];
        if(winner || boardCopy[index]) return;
        boardCopy[index]=state.xIsNext? 'X':'O';
        setState({
            ...state,
            board: boardCopy,
            xIsNext: !state.xIsNext
        });
        // setBoard(boardCopy);
        // setXIsNext(!xIsNext);
    };
    const HandleResetGame = ()=>{
        setState({
           ...state,
            board: Array(9).fill(null),
            xIsNext: true
        });
        // setBoard(Array(9).fill(null));
        // setXIsNext(true);
    }
    return (
        <div>
            <Board cells={state.board} onClick={HandleClick}></Board>
            {winner &&<div className='game-winner'>
               Winner is {winner}
               </div>}
            <button className='buton' onClick={HandleResetGame}>Reset Game</button>

        </div>
    );
};

export default Game;