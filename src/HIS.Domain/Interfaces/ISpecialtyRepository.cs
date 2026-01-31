using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface ISpecialtyRepository : IBaseRepository<Specialty>
{
    Task<Specialty?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Specialty>> GetActiveSpecialtiesAsync(CancellationToken cancellationToken = default);
    Task<bool> SpecialtyCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default);
}