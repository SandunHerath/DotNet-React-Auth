import React, { useState } from "react";
import axios from "axios";

const CreateCompany = ({ history }) => {
  const [company, setCompany] = useState({ name: "", description: "" });

  const handleSubmit = (e) => {
    e.preventDefault();
    // Send a POST request to create a new company
    axios
      .post("/api/companies", company)
      .then(() => {
        history.push("/company-list");
      })
      .catch((error) => {
        console.error("Error creating company:", error);
      });
  };

  return (
    <div>
      <h2>Create Company</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Name:</label>
          <input
            type="text"
            value={company.name}
            onChange={(e) => setCompany({ ...company, name: e.target.value })}
          />
        </div>
        <div>
          <label>Description:</label>
          <textarea
            value={company.description}
            onChange={(e) =>
              setCompany({ ...company, description: e.target.value })
            }
          />
        </div>
        <button type="submit">Create</button>
      </form>
    </div>
  );
};

export default CreateCompany;
