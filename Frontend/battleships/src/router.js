import {
    Route,
    createBrowserRouter,
    createRoutesFromElements,
} from "react-router-dom";
import ErrorPage from "./pages/ErrorPage";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Games from "./pages/Games";
import Game from "./pages/Game";
import NotFound from "./pages/NotFound";
import AppLayout from "./layout/app-layout";
import { AuthLayout } from "./layout/auth-layout/auth-layout";
import ProtectedRoute from "./utils/protected-route";
import Profile from "./pages/Profile";
import { Routes } from "./utils/routes-definition";
import React from "react";
import apiService from "./services/apiService"

const {
    DashboardRoute,
    LoginRoute,
    RegisterRoute,
    ProfileRoute,
    GamesRoute,
    GameRoute,
} = Routes;

export const router = createBrowserRouter(
    createRoutesFromElements(
        <Route errorElement={<ErrorPage />}>
            <Route element={<AuthLayout />}>
                <Route path={LoginRoute} element={<Login />} />
                <Route
                    path={RegisterRoute}
                    lazy={() => import("./pages/Register/register")}
                />
            </Route>
            <Route element={<AppLayout />}>
                <Route element={<ProtectedRoute />}>
                    <Route path={DashboardRoute} element={<Dashboard />} />
                </Route>
                <Route element={<ProtectedRoute />}>
                    <Route path={GamesRoute} element={<Games />} />
                    <Route
                        errorElement={<p>Error element from product id</p>}
                        path={GameRoute}
                        element={<Game />}
                        loader={apiService.fetchGame}
                    />
                </Route>
                <Route path={ProfileRoute} element={<Profile />} />
            </Route>
            <Route path="*" element={<NotFound />} />
        </Route>
    )
);
