import React, { useState } from "react";

function ShipActive({ ship, setSelected }) {
  const [isVertical, setIsVertical] = useState(ship.isVertical);

  const handleSwitchButton = (e) => {
    e.preventDefault();
      ship.isVertical = !isVertical;
      setIsVertical(!isVertical);
  };

  const handleSelectButton = (e) => {
    e.preventDefault();
    console.log(ship.name);
    setSelected(ship.name);
  };

  return (
    <>
      <h5>Nume: {ship.name}</h5>
      <h5>Lungime: {ship.length}</h5>
      <h5>Orientare: {ship.isVertical ? "vertical" : "orizontal"}</h5>
      <button onClick={handleSwitchButton}>Press to switch orientation</button>
      <br></br>
      <button onClick={handleSelectButton}> Press to place</button>
    </>
  );
}

export default ShipActive;
