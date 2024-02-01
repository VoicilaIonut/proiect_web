import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Routes } from "../../utils/routes-definition";
import "./login.css";
//import loginApiCall from "../../services/loginApiCall";
import apiService from '../../services/apiService';

const Login = () => {
    const [Username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState(null);
    const navigate = useNavigate();

    const handleInputChange = (e) => {
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
        let obj = {
            Username: Username,
            password: password,
        };
        const response = await apiService.login(obj);
        if (response.status === 200) {
            navigate(Routes.ProfileRoute);
        }
        setMessage('Ceva nu a functionat bine')
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
            <h1 className="title">Login page</h1>
            <div className="login">
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

                <label forhtml="psw">
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
                    Login
                </button>
            </div>

            <button
                onClick={() => {
                    navigate(Routes.RegisterRoute);
                }}
            >
                Go to Register
            </button>
        </div>
    );
};
export default Login;
