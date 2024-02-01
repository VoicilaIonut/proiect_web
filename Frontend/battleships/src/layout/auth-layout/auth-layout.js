import React from 'react';
import { Outlet, Navigate } from "react-router-dom";
import { Routes } from "../../utils/routes-definition";

export const AuthLayout = () => {
  const tokenFromLS = localStorage.getItem("accessToken");
  if (tokenFromLS && tokenFromLS.length) {
    return <Navigate to={Routes.DashboardRoute} replace />;
  }
  return (
    <div style={{ backgroundColor: "#fafafa" }}>
      <h3 style={{ textAlign: "center" }}>Authentication page</h3>
      <Outlet />
    </div>
  );
};