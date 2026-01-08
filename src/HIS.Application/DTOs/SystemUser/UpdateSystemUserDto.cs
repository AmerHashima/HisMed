namespace HIS.Application.DTOs.SystemUser;

public class UpdateSystemUserDto
{
    public Guid Oid { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public char? Gender { get; set; }
    public DateOnly? BirthDate { get; set; }
    public int RoleID { get; set; }
    public bool IsActive { get; set; }
    public bool TwoFactorEnabled { get; set; }
}