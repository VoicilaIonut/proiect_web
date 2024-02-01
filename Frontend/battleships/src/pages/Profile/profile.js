import React, { useState, useEffect } from "react";
import apiService from "../../services/apiService"

const Profile = () => {
    const [data, setData] = useState();
    const [errorMessage, setErrorMessage] = useState(null);

    useEffect(() =>  {
        const fetchData = async () => {
            const res = await apiService.getUserDetails();
            if (!res.success) {
                setErrorMessage(res.message);
            } else {
                setData(res.data);
                setErrorMessage(null);
            }
        };

        fetchData();

    }, []);

    return (
        <>
            <h1 className="title">Profilul vostru</h1>
            {errorMessage && <h1>{errorMessage}</h1>}
            {data && (
                <>
                    <h5> User ID: {data.userId} </h5>
                    <h5> Email: {data.email} </h5>
                    <h5> Admin: {data.roles.includes("Admin") ? "Da" : "Nu"}</h5>
                    <h5> Jocuri in care sunteti inscris {data.gamesPlayed} </h5>
                    <h5> Jocuri pierdute {data.gamesLost} </h5>
                    <h5> Jocuri castigate {data.gamesWon} </h5>
                    <h5> Jocuri active: {data.currentlyGamesPlaying}
                    </h5>
                </>
            )}
        </>
    );
};
export default Profile;
