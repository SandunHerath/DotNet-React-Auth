import React from "react";
import Navbar from "../NavBar/Navbar";
import Footer from "../Footer/Footer";
import "./Home.css";
const Home = () => {
  return (
    <div className="home">
      <Navbar />
      <div className="home_body">
        <h1 className="welcome-text">WELCOME TO THE SOLUTION</h1>
      </div>

      <Footer />
    </div>
  );
};

export default Home;
