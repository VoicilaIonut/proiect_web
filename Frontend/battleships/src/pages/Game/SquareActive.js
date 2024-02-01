import React from "react";

const SquareActive = ({ indexX, indexY, value, setPlaceOnGameTable }) => {
  return (
    <>
      <button
        className={"square " + value}
        onClick={() => setPlaceOnGameTable([indexX, indexY])}
      ></button>
    </>
  );
};

export default SquareActive;
