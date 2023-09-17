import axios from "axios";
import jwtDecode from "jwt-decode";

const API_URL = "https://localhost:7251/api/Auth";

class AuthService {
  async login(email, password) {
    try {
      const response = await axios.post(`${API_URL}/login`, {
        email,
        password,
      });
      //console.log(response.data.token);

      if (response.data.token) {
        const decodedToken = JSON.stringify(jwtDecode(response.data.token));
        console.log(decodedToken);
        const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        localStorage.setItem("user", decodedToken);
        localStorage.setItem("userRole", userRole);
        console.log("This is role "+userRole);
      }
    } catch (error) {
      throw error;
    }
  }

  logout() {
    localStorage.removeItem("user");
  }

  register(username, password, roles) {
    return axios.post(`${API_URL}/register`, {
      username,
      password,
      roles,
    });
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem("user"));
  }
}

export default new AuthService();
