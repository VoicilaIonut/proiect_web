import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { Routes } from "../../utils/routes-definition";
import "./register.css";
import apiService from '../../services/apiService';

export const Component = () => {
    const [Username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [Email, setEmail] = useState("");
    const [Street, setStreet] = useState("");
    const [City, setCity] = useState("");
    const [Country, setCountry] = useState("");
    const [message, setMessage] = useState(null);
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        e.preventDefault();
        const { id, value } = e.target;
        switch (id) {
            case 'Username':
                setUsername(value);
                break;
            case 'password':
                setPassword(value);
                break;
            case 'Email':
                setEmail(value);
                break;
            case 'Street':
                setStreet(value);
                break;
            case 'City':
                setCity(value);
                break;
            case 'Country':
                setCountry(value);
                break;
            default:
                break;
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        let registerData = {
            Username: Username,
            password: password,
            Email: Email,
            Street: Street,
            City: City,
            Country: Country
        };
        const answer = await apiService.register(registerData);
        if (answer.success) {
            setMessage("Account created successfully. You can now log in.");
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
                    onChange={handleInputChange}
                    required
                ></input>

                <label htmlFor="password">
                    <b>Password</b>
                </label>
                <input
                    type="password"
                    placeholder="Enter Password"
                    name="password"
                    id="password"
                    value={password}
                    onChange={handleInputChange}
                    required
                ></input>

                <label htmlFor="Email">
                    <b>Email</b>
                </label>
                <input
                    type="email"
                    placeholder="Enter Email"
                    name="Email"
                    id="Email"
                    value={Email}
                    onChange={handleInputChange}
                    required
                ></input>

                <label htmlFor="Street">
                    <b>Street</b>
                </label>
                <input
                    type="text"
                    placeholder="Enter Street"
                    name="Street"
                    id="Street"
                    value={Street}
                    onChange={handleInputChange}
                    required
                ></input>

                <label htmlFor="City">
                    <b>City</b>
                </label>
                <input
                    type="text"
                    placeholder="Enter City"
                    name="City"
                    id="City"
                    value={City}
                    onChange={handleInputChange}
                    required
                ></input>

                <label htmlFor="Country">
                    <b>Country</b>
                </label>
                <input
                    type="text"
                    placeholder="Enter Country"
                    name="Country"
                    id="Country"
                    value={Country}
                    onChange={handleInputChange}
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