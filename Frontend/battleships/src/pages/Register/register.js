import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { Routes } from "../../utils/routes-definition";
import "./register.css";
//import register from "../../services/registerApiCall";
import apiService from '../../services/apiService';

//componenta trebuie sa fie definita exportata in acest fell pentru lazy loading
//https://reactrouter.com/en/main/route/lazy

export const Component = () => {
    const [Username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState(null);
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        e.preventDefault();
        const { id, value } = e.target;
        if (id === "Username") {
            setUsername(value);
        }
        if (id === "password") {
            setPassword(value);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        let registerData = {
            Username: Username,
            password: password,
        };
        const answer = await apiService.register(registerData);
        if (answer.id && answer.Username) {
            setMessage("Cont creat cu succes. Acum va puteti loga in cont.");
        } else if (answer.message) {
            setMessage(answer.message);
        }
    };

    return (
        <div
            style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                flexDirection: "column",
            }}
        >
            {message && <h1>{message}</h1>}
            <h1 className="title">Register</h1>
            <div className="register">
                <label htmlFor="Username">
                    <b>Username</b>
                </label>
                <input
                    type="text"
                    placeholder="Enter Username"
                    name="Username"
                    id="Username"
                    value={Username}
                    onChange={(e) => handleInputChange(e)}
                    required
                ></input>

                <label htmlFor="psw">
                    <b>Password</b>
                </label>
                <input
                    type="password"
                    placeholder="Enter Password"
                    name="password"
                    id="password"
                    value={password}
                    onChange={(e) => handleInputChange(e)}
                    required
                ></input>
                <button type="submit" onClick={handleSubmit}>
                    Register
                </button>
            </div>
            <button
                onClick={() => {
                    navigate(Routes.DashboardRoute);
                }}
            >
                Go to Login
            </button>
        </div>
    );
};

// export default Register;
