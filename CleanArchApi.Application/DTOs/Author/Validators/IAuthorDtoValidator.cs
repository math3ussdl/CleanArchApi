namespace CleanArchApi.Application.DTOs.Author.Validators;

using FluentValidation;

public class IAuthorDtoValidator : AbstractValidator<IAuthorDto>
{
	public IAuthorDtoValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(50)
				.WithMessage("{PropertyName} must not exceed 50 characters.");

		RuleFor(a => a.Email)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(120)
				.WithMessage("{PropertyName} must not exceed 120 characters.")
			.EmailAddress()
				.WithMessage("{PropertyName} must be a valid email.");

		RuleFor(a => a.Phone)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(20)
				.WithMessage("{PropertyName} must not exceed 20 characters.");
	}
}

