using FluentValidation;
using Dot.Net.Core.Startup.Data.Entities;
using Dot.Net.Core.Startup.Domain.Services;

namespace Dot.Net.Core.Startup.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private readonly ICountryService _countryService;
        public ApplicantValidator(ICountryService countryService)
        {
            _countryService = countryService;
            RuleFor(model => model.Name).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(model => model.FamilyName).NotEmpty().NotNull().MinimumLength(5);
            RuleFor(model => model.Address).NotEmpty().NotNull().MinimumLength(10);
            RuleFor(model => model.CountryOfOrigin).NotEmpty().NotNull().MustAsync(async (model, CountryOfOrigin, cancellationToken) =>
            {
                return await _countryService.IsValidCountry(CountryOfOrigin, cancellationToken);
            });
            RuleFor(model => model.EmailAddress).NotNull().NotEmpty().EmailAddress().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            RuleFor(model => model.Age).NotNull().NotEmpty().InclusiveBetween(20, 60);
        }
    }
}
