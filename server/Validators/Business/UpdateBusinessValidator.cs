using FluentValidation;
using server.DTOs;

namespace server.Validators;

public class UpdateBusinessValidator : AbstractValidator<UpdateBusinessDto>
{
    public UpdateBusinessValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .WithMessage("Please enter a valid email address");

        RuleFor(x => x.PhoneNumber)
        .NotEmpty()
        .Length(11)
        .WithMessage("Phone number must exactly 11 digits");
    }
}