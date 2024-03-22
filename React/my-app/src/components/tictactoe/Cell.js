import React from 'react';

const Cell = ({value, onClick, className}) => {
    return (
        <div className={`Game-Cell ${className}`} onClick={onClick}>
        {value}
            
        </div>
    );
};

export default Cell;