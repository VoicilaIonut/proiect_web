import { useParams, useLoaderData, useNavigation } from "react-router-dom";
import React, { useEffect, useState } from "react";
import JoinGameButton from "./JoinGameButton";
import BoardPersonalActive from "./BoardPersonalActive";
import BoardAdversar from "./BoardAdversar";
import {
    boardLoadedWithMoves,
    createEmptyBoard,
    boardLoadedWithShips,
    checkWinnerWithMoves,
    convertGameState,
} from "./utils";
import BoardInactive from "./BoardInactive";
import ChooseShipsMapConfig from "./ChooseShipsMapConfig";
import apiService from "../../services/apiService";

const CREATED = 0;
const MAP_CONFIG = 1;
const ACTIVE = 2;
const FINISHED = 3;


const renderPlayerInfo = (player1Id, player2Id) => (
    <>
        <h5>Player 1 {player1Id}</h5>
        {player2Id !== null && <h5>Player 2 {player2Id}</h5>}
    </>
);

const renderActivePlayer = (playerToMoveId) => (
    <h5>PlayerID care trebuie sa mute: {playerToMoveId}</h5>
);

const renderJoinGameButton = (productId) => (
    <JoinGameButton productId={productId} />
);

const renderChooseShipsMapConfig = () => (
    <ChooseShipsMapConfig />
);

const renderBoardInactive = (shipsCoord) => (
    <>
        <h5>Se asteapta adversarul...</h5>
        <BoardInactive board={boardLoadedWithShips(shipsCoord)} />
    </>
);

const renderActiveGame = (movesPlayer1, movesPlayer2, shipsCoord) => (
    <>
        <BoardPersonalActive moves={movesPlayer1} />
        <BoardAdversar
            moves={movesPlayer2}
            shipsCoord={shipsCoord}
        />
    </>
);

const renderFinishedGame = (winnerId, movesPlayer1, movesPlayer2) => (
    <>
        <h1>WINNER: {winnerId}</h1>
        <div className="start-game">
            <BoardInactive board={boardLoadedWithMoves(createEmptyBoard(), movesPlayer1)} />
            <BoardInactive board={boardLoadedWithMoves(createEmptyBoard(), movesPlayer2)} />
        </div>
    </>
);

const Game = () => {
    const [gameState, setGameState] = useState(useLoaderData());
    const navigationState = useNavigation();
    let { productId } = useParams();
    let params = useParams();
    const personalId = localStorage.getItem("id");

    useEffect(() => {
        if (navigationState.state !== "loading") {
            let interval = setInterval(async () => {
                const newGameState = await apiService.fetchGame({ params });

                if (JSON.stringify(newGameState) !== JSON.stringify(gameState)) {
                    console.log("Change game state.");
                    setGameState(newGameState);
                }
            }, 5000);
            return () => {
                clearInterval(interval);
            };
        }
    }, [gameState, navigationState.state, params]);

    const movesPlayer1 = gameState.moves?.filter(
        (move) => move.userId === personalId
    );
    const movesPlayer2 = gameState.moves?.filter(
        (move) => move.userId !== personalId
    );

    return (
        <div className="gameContainer">
            <h1>Id joc: {gameState.gameId}</h1>
            <h5>Status {convertGameState(gameState.gameState)}</h5>
            {renderPlayerInfo(gameState.player1Id, gameState.player2Id)}
            {gameState.gameState === ACTIVE && renderActivePlayer(gameState.currentPlayer === gameState.player1Id ? gameState.player1Id : gameState.player2Id)}
            {gameState.gameState === CREATED && gameState.player1Id !== personalId && gameState.player2Id === null && renderJoinGameButton(productId)}
            {gameState.gameState === MAP_CONFIG && gameState.shipsCoord.length === 0 && renderChooseShipsMapConfig()}
            {gameState.gameState === MAP_CONFIG && gameState.shipsCoord.length !== 0 && renderBoardInactive(gameState.shipsCoord)}
            {gameState.gameState === ACTIVE && renderActiveGame(movesPlayer1, movesPlayer2, gameState.shipsCoord)}
            {gameState.gameState === FINISHED && renderFinishedGame(checkWinnerWithMoves(movesPlayer1, gameState.shipsCoord.length) ? gameState.player1Id : gameState.player2Id, movesPlayer1, movesPlayer2)}
        </div>
    );
};

export default Game;
