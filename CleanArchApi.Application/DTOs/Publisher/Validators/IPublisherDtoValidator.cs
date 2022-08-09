namespace CleanArchApi.Application.DTOs.Publisher.Validators;

using FluentValidation;

public class IPublisherDtoValidator : AbstractValidator<IPublisherDto>
{
	public IPublisherDtoValidator()
	{
		RuleFor(p => p.Name)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(50)
				.WithMessage("{PropertyName} must not exceed 50 characters.");
	}
}
