﻿namespace AuthenticationOne.DTOs
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
