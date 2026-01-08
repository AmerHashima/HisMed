using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("Users")]
public class SystemUser : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(128)]
    public string PasswordSalt { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Mobile { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName { get;  set; } = string.Empty;

    [MaxLength(1)]
    public char? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int RoleID { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLogin { get; set; }


    public int FailedLoginCount { get; set; } = 0;

    public DateTime? LockoutEnd { get; set; }

    public DateTime? PasswordExpiry { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;



}