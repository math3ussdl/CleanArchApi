namespace CleanArchApi.Application.DTOs.Book.Validators;

using FluentValidation;

using Contracts.Persistence;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
	public BookCreateDtoValidator(IBookRepository bookRepository,
		IPublisherRepository publisherRepository)
	{
		Include(new IBookDtoValidator(bookRepository, publisherRepository));
	}
}
