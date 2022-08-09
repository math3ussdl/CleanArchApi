namespace CleanArchApi.Application.DTOs.Author.Validators;

using FluentValidation;

public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
{
	public AuthorUpdateDtoValidator()
	{
		Include(new IAuthorDtoValidator());

		RuleFor(a => a.Id).NotNull().WithMessage("{PropertyName} must be present");
	}
}
