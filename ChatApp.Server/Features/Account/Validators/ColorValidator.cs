using FluentValidation;

namespace ChatApp.Server.Features.Account.Validators;

public class ColorValidator : AbstractValidator<string>
{
    public ColorValidator()
    {
        RuleFor(x => x).
            NotEmpty().WithMessage("Color is required.")
            .Matches("^#([0-9a-fA-F]{6})$").WithMessage("Color must be a valid hex color code");
    }
}
