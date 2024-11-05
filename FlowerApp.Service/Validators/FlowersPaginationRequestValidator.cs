using FlowerApp.Domain.DTOModels;
using FluentValidation;

namespace FlowerApp.Service.Validators;

public class FlowersPaginationRequestValidator : AbstractValidator<FlowersPaginationRequest>
{
    public FlowersPaginationRequestValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage($"{nameof(FlowersPaginationRequest.Skip)} must be no less than 0.");

        RuleFor(x => x.Take)
            .GreaterThan(0)
            .WithMessage($"{nameof(FlowersPaginationRequest.Take)} must be greater than 0.");
    }
}