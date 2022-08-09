namespace CleanArchApi.Application.DTOs.Book.Validators;

using FluentValidation;

using Contracts.Persistence;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;

	public BookCreateDtoValidator(IBookRepository bookRepository,
		IPublisherRepository publisherRepository)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;

		Include(new IBookDtoValidator(_bookRepository, _publisherRepository));
	}
}
