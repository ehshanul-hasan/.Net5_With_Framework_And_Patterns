using Hahn.ApplicatonProcess.December2020.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public interface IApplicantService
    {
        Task<int> CreateAsync(Applicant applicant, CancellationToken cancellationToken = default);
        Task<Applicant> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Applicant request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> ListAsync(CancellationToken cancellationToken = default);
    }
}
