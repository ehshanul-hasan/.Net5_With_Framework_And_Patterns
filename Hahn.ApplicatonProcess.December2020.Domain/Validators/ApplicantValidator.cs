using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private readonly ICountryService _countryService;
        public ApplicantValidator(ICountryService countryService)
        {
            _countryService = countryService;
            RuleFor(model => model.Name).NotEmpty().NotNull().MinimumLength(5).WithMessage("Please specify a name");
            RuleFor(model => model.FamilyName).NotEmpty().NotNull().MinimumLength(5).WithMessage("Please specify a name");
            RuleFor(model => model.Address).NotEmpty().NotNull().MinimumLength(5).WithMessage("Please specify a name");
            RuleFor(model => model.CountryOfOrigin).NotEmpty().NotNull().MustAsync(async (model, CountryOfOrigin, cancellationToken) =>
            {
                return await _countryService.IsValidCountry(CountryOfOrigin, cancellationToken);
            }).WithMessage("invalid country input");
            RuleFor(model => model.EmailAddress).NotNull().NotEmpty().EmailAddress();
            RuleFor(model => model.Age).NotNull().NotEmpty().InclusiveBetween(20, 60);
            RuleFor(model => model.Hired).NotNull();
        }
    }
}
