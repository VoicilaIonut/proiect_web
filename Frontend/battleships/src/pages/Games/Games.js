import React, { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { convertGameState } from "../Game/utils";
import CreateGameButton from "./CreateGameButton";
import apiService from "../../services/apiService";

const Games = () => {
    const [games, setGames] = useState([]);
    const [errorMessage, setErrorMessage] = useState(null);
    const navigate = useNavigate();
    const personalId = localStorage.getItem("id");


    useEffect(() => {
        const fetchData = async () => {
            const res = await apiService.getGames();
            if (!res.success) {
                setErrorMessage(res.message);
            } else {
                const games = res.data.filter((game) => game.player1Id === personalId || game.player2Id === personalId || game.player2Id === null);
                setGames(games);
                setErrorMessage(null);
            }
        };

        fetchData();
    }, [personalId]);

    return (
        <div>
            {errorMessage && <h1>{errorMessage}</h1>}
            <CreateGameButton navigate={navigate} setErrorMessage={setErrorMessage} />
            <h1 className="title"> Games</h1>
            <div className="gamesContainer">
                {games.map((game) => (
                    <div key={game.gameId}  className={"gameContainer " + game.gameId}>
                        <h5 className="id">Joc ID: {game.gameId}</h5>
                        <h5 className="status">Status joc: {convertGameState(game.gameState)}</h5>
                        <h5 className="player1">Player 1 ID: {game.player1Id}</h5>

                        {game.player2Id !== null && (
                            <>
                                <h5 className="player2">Player 2 ID: {game.player2Id}</h5>
                            </>
                        )}

                        <button
                            className="gotobutton"
                            onClick={() => navigate(game.gameId)}>
                            To the game
                        </button>
                    </div>
                ))}
            </div>
            <Outlet />
        </div>
    );
};

export default Games;
