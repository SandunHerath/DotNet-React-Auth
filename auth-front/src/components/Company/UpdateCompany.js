import React, { useState, useEffect } from "react";
import axios from "axios";

const UpdateCompany = ({ match, history }) => {
  const companyId = match.params.id;
  const [company, setCompany] = useState({ name: "", description: "" });

  useEffect(() => {
    // Fetch company details for update
    axios
      .get(`/api/companies/${companyId}`)
      .then((response) => {
        setCompany(response.data);
      })
      .catch((error) => {
        console.error("Error fetching company:", error);
      });
  }, [companyId]);

  const handleUpdate = (e) => {
    e.preventDefault();
    // Send a PUT request to update the company
    axios
      .put(`/api/companies/${companyId}`, company)
      .then(() => {
        history.push("/company-list");
      })
      .catch((error) => {
        console.error("Error updating company:", error);
      });
  };

  const handleDelete = () => {
    // Send a DELETE request to delete the company
    axios
      .delete(`/api/companies/${companyId}`)
      .then(() => {
        history.push("/company-list");
      })
      .catch((error) => {
        console.error("Error deleting company:", error);
      });
  };

  return (
    <div>
      <h2>Edit Company</h2>
      <form onSubmit={handleUpdate}>
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
        <button type="submit">Update</button>
        <button type="button" onClick={handleDelete}>
          Delete
        </button>
      </form>
    </div>
  );
};

export default UpdateCompany;
