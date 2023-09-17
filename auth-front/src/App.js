import React from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import Home from "./components/Base/Home/Home";
import Login from "./components/User/Login";
import Register from "./components/User/Register";
import CompanyList from "./components/Company/CompanyList";
import CreateCompany from "./components/Company/CreateCompany";
import UpdateCompany from "./components/Company/UpdateCompany";
import ProtectedRoute from "./components/Helper/ProtectedRoute";
import Forbidden from "./components/Helper/Forbidden";
import Dashboard from "./components/Base/DashBoard/Dashboard";
function App() {
  return (
    <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/forbidden" element={<Forbidden />} />
          <Route
            path="/company-list"
            element={
              <ProtectedRoute
                element={<CompanyList />}
                roles={["admin", "companyUser"]}
              />
            }
          />
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute
                element={<Dashboard />}
                roles={["admin", "companyUser", "AppUser"]}
              />
            }
          />
          <Route
            path="/create-company"
            element={
              <ProtectedRoute
                element={<CreateCompany />}
                roles={["admin", "companyUser"]}
              />
            }
          />
          <Route
            path="/update-company/:id"
            element={
              <ProtectedRoute
                element={<UpdateCompany />}
                roles={["admin", "companyUser"]}
              />
            }
          />
          <Route path="/" element={<Home />} />
        </Routes>

    </Router>
  );
}

export default App;
