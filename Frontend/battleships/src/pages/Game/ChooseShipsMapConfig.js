import React, { useState } from "react";
import {
    useParams,
  } from "react-router-dom";
import ChooseShipsConfigurator from "./ChooseShipsConfigurator";
import { AVAILABLE_SHIPS, shipConfigationForApi } from "./utils";
import apiService from "../../services/apiService"

const ChooseShipsMapConfig = () => {
  const [ships, setShips] = useState(JSON.parse(JSON.stringify(AVAILABLE_SHIPS)));
  const [message, setMessage] = useState(null);
  let { productId } = useParams();

    const handleSendMapToApi = async (e) => {
        e.preventDefault();
        let shipsPlaced = ships.filter((ship) => ship.placed);

        shipsPlaced = shipsPlaced.map((ship) =>
            shipConfigationForApi(ship.row, ship.column, ship.length, ship.isVertical)
        );
        console.log(shipsPlaced, "to api");

        const res = await apiService.sendMapToApi(productId, shipsPlaced);
        if (!res.success) {
            setShips(JSON.parse(JSON.stringify(AVAILABLE_SHIPS)));
        }
        setMessage(res.message);
    };

  return (
    <>
      {message !==null && <h5>{message}</h5>}
      <ChooseShipsConfigurator ships={ships} setShips={setShips} />
      <button onClick={handleSendMapToApi}>send to api</button>
    </>
  );
};

export default ChooseShipsMapConfig;
