using Dot.Net.Core.Startup.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Domain.Services
{
    public interface IApplicantService
    {
        Task<int> CreateAsync(Applicant applicant, CancellationToken cancellationToken = default);
        Task<Applicant> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Applicant request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> ListAsync(CancellationToken cancellationToken = default);
        Task<IQueryable<Applicant>> ListQueryableAsync(CancellationToken cancellationToken = default);
    }
}
