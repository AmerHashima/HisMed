namespace HIS.Application.DTOs.Specialty;

public class SpecialtyDto
{
    public Guid Oid { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public int? DefaultVisitDuration { get; set; }
    public decimal? DefaultPrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}