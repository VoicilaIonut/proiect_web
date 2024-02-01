import React from "react";
import apiService from "../../services/apiService";


const JoinGameButton = ({ productId }) => {
    const handleJoinGame = async (e) => {
        e.preventDefault();
        const res = await apiService.joinGame(productId);
        if (res.success) {
            window.location.reload();
        } 
        console.log(res.message);
    };

    return <button onClick={handleJoinGame}>Join the game</button>;
};

export default JoinGameButton;