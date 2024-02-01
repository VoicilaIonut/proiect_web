import React from 'react';
import { Outlet } from "react-router-dom";
import { Navigation } from "../../components";

const AppLayout = () => {
  return (
    <div>
      <Navigation />
      <Outlet />
    </div>
  );
};

export default AppLayout;