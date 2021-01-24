using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Repositories;
using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Applicant> _applicantRepository;
        public ApplicantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _applicantRepository = _unitOfWork.GetRepository<Applicant>();
        }
        public async Task<int> CreateAsync(Applicant applicant, CancellationToken cancellationToken = default)
        {
            _applicantRepository.Create(applicant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return applicant.ID;
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Applicant applicant = await _applicantRepository.Get(id);
            _applicantRepository.Delete(applicant);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Applicant request, CancellationToken cancellationToken = default)
        {
            Applicant applicant = await _applicantRepository.Get(request.ID);

            applicant.Name = request.Name;
            applicant.FamilyName = request.FamilyName;
            applicant.EmailAddress = request.EmailAddress;
            applicant.Address = request.Address;
            applicant.CountryOfOrigin = request.CountryOfOrigin;
            applicant.Age = request.Age;
            applicant.Hired = request.Hired;

            _applicantRepository.Update(applicant);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<Applicant> GetByIdAsync(int id)
        {
            Applicant applicant = await _applicantRepository.Get(id);
            return applicant;
        }

        public async Task<List<Applicant>> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = await _applicantRepository.Where(s=>s.ID > 0).ToListAsync();
            return result;
        }

    }
}
