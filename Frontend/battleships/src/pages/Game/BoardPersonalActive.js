import React, { useEffect, useState } from "react";
import {
    boardLoadedWithMoves,
    convertIndexesToApi,
    createEmptyBoard,
} from "./utils";
import BoardActive from "./BoardActive";
import apiService from "../../services/apiService"
import { useParams } from "react-router-dom";

const BoardPersonalActive = ({ moves }) => {
    const [board, setBoard] = useState(
        boardLoadedWithMoves(createEmptyBoard(), moves)
    );
    const [placeOnGameTable, setPlaceOnGameTable] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);
    let { productId } = useParams();

    useEffect(() => {
        setErrorMessage(null);
    }, [moves, board]);

    const handleSendAttack = async  (e) => {
        e.preventDefault();
        console.log(placeOnGameTable, productId, "send attack to api");

        const res = await apiService.sendAttack(productId, convertIndexesToApi(placeOnGameTable));
        if (res.success) {
            setBoard(boardLoadedWithMoves(board, [res.data]));
        } else {
            setErrorMessage(res.message);
        }
        setPlaceOnGameTable(null);
    };

    return (
        <>
            {errorMessage && <h5>{errorMessage}</h5>}
            <div className="start-game">
                <BoardActive board={board} setPlaceOnGameTable={setPlaceOnGameTable} />
                {placeOnGameTable !== null && (
                    <button onClick={handleSendAttack}>ATAC</button>
                )}
            </div>
        </>
    );
};

export default BoardPersonalActive;
