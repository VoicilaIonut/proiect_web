import React from "react";
import ShipActive from "./ShipActive";

function ListShips({ ships, setSelected }) {
    return (
        <div className="available-ships">
            {ships.map(
                (ship, index) =>
                    !ship.placed && <ShipActive key={index} ship={ship} setSelected={setSelected} />
            )}
        </div>
    );
}

export default ListShips;