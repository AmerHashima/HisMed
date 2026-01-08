namespace HIS.Application.DTOs.SystemUser;

public class SystemUserDto
{
    public Guid Oid { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public char? Gender { get; set; }
    public DateOnly? BirthDate { get; set; }
    public int RoleID { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public int FailedLoginCount { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public DateTime? PasswordExpiry { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}