namespace CleanArchApi.Application.DTOs.Author.Validators;

using FluentValidation;

public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
{
	public AuthorCreateDtoValidator()
	{
		Include(new IAuthorDtoValidator());
	}
}
