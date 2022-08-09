namespace CleanArchApi.Application.DTOs.Book.Validators;

using FluentValidation;
using Persistence.Contracts;

public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;

	public BookUpdateDtoValidator(IBookRepository bookRepository,
		IPublisherRepository publisherRepository)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;

		Include(new IBookDtoValidator(_bookRepository, _publisherRepository));

		RuleFor(b => b.Id).NotNull().WithMessage("{PropertyName} must be present");
	}
}
