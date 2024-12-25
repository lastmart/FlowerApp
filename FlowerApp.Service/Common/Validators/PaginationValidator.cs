using FlowerApp.DTOs.Common;
using FluentValidation;

namespace FlowerApp.Service.Common.Validators;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage($"{nameof(Pagination.Skip)} must be no less than 0.");

        RuleFor(x => x.Take)
            .GreaterThan(0)
            .WithMessage($"{nameof(Pagination.Take)} must be greater than 0.");
    }
}