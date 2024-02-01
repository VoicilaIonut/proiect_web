import { useRouteError } from "react-router-dom";
import React from 'react';

const ErrorPage = () => {
  const error = useRouteError();

  return (
    <div>
      Error page
      <h2>Error message:{error.message}</h2>
    </div>
  );
};

export default ErrorPage;
