namespace CleanArchApi.Application.DTOs.Publisher.Validators;

using FluentValidation;

public class PublisherCreateDtoValidator : AbstractValidator<PublisherCreateDto>
{
	public PublisherCreateDtoValidator()
	{
		Include(new IPublisherDtoValidator());
	}
}
