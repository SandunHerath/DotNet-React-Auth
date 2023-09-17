import React from "react";
import AuthService from "../../Services/AuthService";

const Logout = () => {
  AuthService.logout();
  return (
    <div>
      <p>You have been logged out.</p>
    </div>
  );
};

export default Logout;
