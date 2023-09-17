import React, { useState } from "react";
import AuthService from "../../Services/AuthService";
import { useNavigate } from "react-router-dom";
import { Form, Input, Button, Typography, Row, Col } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import "./Login.css";
const { Title } = Typography;

const Login = () => {
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  const onFinish = async (values) => {
    try {
      await AuthService.login(values.email, values.password).then(() => {
        navigate("/dashboard");
      });
    } catch (error) {
      setMessage("Login failed. Please check your credentials.");
    }
  };

  return (
    <div className="login_form">
      <Row
        className="login_form"
        justify="center"
        align="middle"
        style={{
          height: "90vh",
        }}
      >
        <Col span={8} style={{ height: "60vh" }}>
          <Form
            name="login-form"
            initialValues={{ remember: true }}
            style={{ border: "2px solid black", padding: "40px" }}
            onFinish={onFinish}
          >
            <Title level={2}>Login</Title>
            <Form.Item
              name="email"
              rules={[{ required: true, message: "Please enter your email!" }]}
            >
              <Input
                prefix={<UserOutlined className="site-form-item-icon" />}
                placeholder="Email"
              />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[
                { required: true, message: "Please enter your password!" },
              ]}
            >
              <Input
                prefix={<LockOutlined className="site-form-item-icon" />}
                type="password"
                placeholder="Password"
              />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">
                Login
              </Button>
            </Form.Item>
          </Form>
          {message && <div>{message}</div>}
        </Col>
      </Row>
    </div>
  );
};

export default Login;
