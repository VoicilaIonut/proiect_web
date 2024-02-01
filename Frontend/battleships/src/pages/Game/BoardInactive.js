import React from "react";
import SquareInactive from './SquareInactive';

const BoardInactive = ({ board }) => {
    const generateRow = (row, rowIndex) => row.map((value, colIndex) => (
        <SquareInactive key={`${rowIndex}-${colIndex}`} value={value} />
    ));
    const generate2DBoard = board.map((row, rowIndex) => (
        <div key={rowIndex} className='board-row'>{generateRow(row, rowIndex)}</div>
    ));
    return (
        <div className='board'>
            {generate2DBoard}
        </div>
    )
}

export default BoardInactive;