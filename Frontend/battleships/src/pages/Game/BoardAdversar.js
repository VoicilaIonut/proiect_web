import React, { useState, useEffect } from "react";
import BoardInactive from "./BoardInactive";
import { boardLoadedWithMoves, boardLoadedWithShips } from "./utils";

const BoardAdversar = ({ moves, shipsCoord }) => {
  const [board, setBoard] = useState(boardLoadedWithShips(shipsCoord));

  useEffect(() => {
    setBoard(boardLoadedWithMoves(board, moves));
  }, [moves]);

  return (
    <div className="start-game">
      <BoardInactive board={board} />
    </div>
  );
};

export default BoardAdversar;
