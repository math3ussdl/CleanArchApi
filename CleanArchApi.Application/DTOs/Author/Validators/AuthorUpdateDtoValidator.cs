namespace CleanArchApi.Application.DTOs.Author.Validators;

using FluentValidation;

public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
{
	public AuthorUpdateDtoValidator()
	{
		Include(new IAuthorDtoValidator());
	}
}
