using FluentValidation;
using server.DTOs;

public class UpdateBusinessSettingsValidator : AbstractValidator<UpdateBusinessSettingsDto>
{
    public UpdateBusinessSettingsValidator()
    {
        RuleFor(x => x.DefaultInterestRate)
            .InclusiveBetween(0, 100)
            .WithMessage("Interest rate must be between 0 and 100.");

        RuleFor(x => x.DefaultPenaltyRate)
            .InclusiveBetween(0, 100)
            .WithMessage("Penalty rate must be between 0 and 100.");

        RuleFor(x => x.DefaultLoanTermDays)
            .GreaterThan(0)
            .WithMessage("Loan term must be greater than 0.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .MaximumLength(10);
    }
}