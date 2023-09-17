import React from "react";
import { Route, Navigate } from "react-router-dom";
import AuthService from "../../Services/AuthService";

const ProtectedRoute = ({ element: Element, roles, ...rest }) => {
  const user = AuthService.getCurrentUser();

  if (!user) {
    // Redirect to the login page if the user is not authenticated
    return <Navigate to="/login" replace />;
  }

  // Check if the user has the required roles
  if (roles && roles.length && !user.roles.some((r) => roles.includes(r))) {
    // Redirect to the forbidden page if the user doesn't have the required roles
    return <Navigate to="/forbidden" replace />;
  }

  // Render the protected component if authorized
  return <Element {...rest} />;
};

export default ProtectedRoute;
