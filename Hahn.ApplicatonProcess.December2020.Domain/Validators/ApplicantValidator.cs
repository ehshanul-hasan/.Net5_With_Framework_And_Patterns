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
            RuleFor(model => model.Name).NotEmpty().NotNull().MinimumLength(5).WithMessage("Name must be at least 5 charecters");
            RuleFor(model => model.FamilyName).NotEmpty().NotNull().MinimumLength(5).WithMessage("Family must be at least 5 charecters");
            RuleFor(model => model.Address).NotEmpty().NotNull().MinimumLength(10).WithMessage("Address must be with 10 Charecters");
            RuleFor(model => model.CountryOfOrigin).NotEmpty().NotNull().MustAsync(async (model, CountryOfOrigin, cancellationToken) =>
            {
                return await _countryService.IsValidCountry(CountryOfOrigin, cancellationToken);
            }).WithMessage("Country doesn't exist");
            RuleFor(model => model.EmailAddress).NotNull().NotEmpty().EmailAddress().WithMessage("Email address is not valid");
            RuleFor(model => model.Age).NotNull().NotEmpty().InclusiveBetween(20, 60).WithMessage("Age must be between 20 and 60");
        }
    }
}
