import React, { useState } from "react";
import AuthService from "../../Services/AuthService";
import { Form, Input, Button, Typography, Row, Col, message } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";

const { Title } = Typography;

const Register = () => {
  const [messageText, setMessageText] = useState("");
  const [form] = Form.useForm();

  const onFinish = async (values) => {
    try {
      await AuthService.register(
        values.username,
        values.password,
        values.roles
      );
      setMessageText("Registration successful! You can now log in.");
      form.resetFields(); // Clear form fields on successful registration
    } catch (error) {
      setMessageText("Registration failed. Please try again.");
    }
  };

  return (
    <Row justify="center" align="middle" style={{ height: "100vh" }}>
      <Col span={6}>
        <Form
          form={form}
          name="register-form"
          onFinish={onFinish}
          initialValues={{ roles: [] }}
        >
          <Title level={2}>Register</Title>
          <Form.Item
            name="username"
            rules={[{ required: true, message: "Please enter your username!" }]}
          >
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              placeholder="Username"
            />
          </Form.Item>
          <Form.Item
            name="password"
            rules={[{ required: true, message: "Please enter your password!" }]}
          >
            <Input
              prefix={<LockOutlined className="site-form-item-icon" />}
              type="password"
              placeholder="Password"
            />
          </Form.Item>
          <Form.Item name="roles">
            <Input
              placeholder="Roles (comma-separated)"
              onChange={(e) =>
                form.setFieldsValue({ roles: e.target.value.split(",") })
              }
            />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit">
              Register
            </Button>
          </Form.Item>
        </Form>
        {messageText && <div style={{ marginTop: "10px" }}>{messageText}</div>}
      </Col>
    </Row>
  );
};

export default Register;
