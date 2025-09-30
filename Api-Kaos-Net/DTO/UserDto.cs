namespace Api_Kaos_Net.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int? SessionTime { get; set; }
        public int? FailedLogin { get; set; }
        public int? CurrentSessions { get; set; }
        public string Email { get; set; } = null!;
        public string? SecurityQuestion { get; set; }
        public string? SecretAnswer { get; set; }
        public int RoleFk { get; set; }
        public string? UserStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
