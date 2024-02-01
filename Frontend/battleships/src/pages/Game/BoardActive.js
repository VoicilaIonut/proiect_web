import React from "react";
import SquareActive from "./SquareActive";

const BoardActive = ({ board, setPlaceOnGameTable }) => {
  const generateRow = (row, indexX) =>
    row.map((value, indexY) => (
        <SquareActive
        key={indexX.toString() + indexY.toString()}
        indexX={indexX}
        indexY={indexY}
        value={value}
        setPlaceOnGameTable={setPlaceOnGameTable}
      />
    ));
  const generate2DBoard = board.map((row, indexX) => (
      <div key={indexX} className="board-row indexX">{generateRow(row, indexX)}</div>
  ));
  return <div className="board">{generate2DBoard}</div>;
};

export default BoardActive;
