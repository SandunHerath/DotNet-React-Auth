using AuthenticationOne.DTOs.Enums;

namespace AuthenticationOne.DTOs
{
    public class ChangeRoleDTO
    {
        public string? Email { get; set; }
        public RoleTypeEnum RoleTypeEnum { get; set; }
    }
}
