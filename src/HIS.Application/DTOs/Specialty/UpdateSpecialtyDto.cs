using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Specialty;

public class UpdateSpecialtyDto
{
    [Required]
    public Guid Oid { get; set; }

    [Required(ErrorMessage = "Specialty code is required")]
    [StringLength(20, ErrorMessage = "Code cannot exceed 20 characters")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Arabic name is required")]
    [StringLength(100, ErrorMessage = "Arabic name cannot exceed 100 characters")]
    public string NameAr { get; set; } = string.Empty;

    [Required(ErrorMessage = "English name is required")]
    [StringLength(100, ErrorMessage = "English name cannot exceed 100 characters")]
    public string NameEn { get; set; } = string.Empty;

    [Range(5, 180, ErrorMessage = "Visit duration must be between 5 and 180 minutes")]
    public int? DefaultVisitDuration { get; set; }

    [Range(0, 999999.99, ErrorMessage = "Price must be between 0 and 999999.99")]
    public decimal? DefaultPrice { get; set; }

    public bool IsActive { get; set; }
}