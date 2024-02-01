import React from "react";
import apiService from "../../services/apiService"

const CreateGameButton = ({ navigate, setErrorMessage }) => {
    const handleCreateGame = async (e) => {
        e.preventDefault();
        const res = await apiService.createGame();
        if (!res.success) {
            setErrorMessage(res.message);
        } else {
            setErrorMessage(null);
            window.location.reload();
        }
    };

    return (
        <>
            <button type="button" onClick={handleCreateGame}>
                Create new game
            </button>
        </>
    );
};

export default CreateGameButton;
