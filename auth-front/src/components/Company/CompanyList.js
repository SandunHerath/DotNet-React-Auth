import React, { useEffect, useState } from "react";
import axios from "axios";

const CompanyList = () => {
  const [companies, setCompanies] = useState([]);

  useEffect(() => {
    // Fetch the list of companies from the API
    axios
      .get("/api/companies")
      .then((response) => {
        setCompanies(response.data);
      })
      .catch((error) => {
        console.error("Error fetching companies:", error);
      });
  }, []);

  return (
    <div>
      <h2>Company List</h2>
      <ul>
        {companies.map((company) => (
          <li key={company.id}>{company.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default CompanyList;
