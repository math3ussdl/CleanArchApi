namespace CleanArchApi.Application.DTOs.Publisher.Validators;

using FluentValidation;

public class PublisherUpdateDtoValidator : AbstractValidator<PublisherUpdateDto>
{
	public PublisherUpdateDtoValidator()
	{
		Include(new IPublisherDtoValidator());

		RuleFor(a => a.Id).NotNull().WithMessage("{PropertyName} must be present");
	}
}
