namespace CleanArchApi.Application.DTOs.Book.Validators;

using FluentValidation;

using Contracts.Persistence;

public class IBookDtoValidator : AbstractValidator<IBookDto>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;

	public IBookDtoValidator(IBookRepository bookRepository,
		IPublisherRepository publisherRepository)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;

		RuleFor(b => b.Title)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(120)
				.WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

		RuleFor(b => b.AuthorId)
			.GreaterThan(0)
			.MustAsync(async (int id, CancellationToken token) =>
			{
				var authorExists = await _bookRepository.Exists(id);
				return !authorExists;
			}).WithMessage("{PropertyName} does not exist.");

		RuleFor(b => b.PublisherId)
			.GreaterThan(0)
			.MustAsync(async (int id, CancellationToken token) =>
			{
				var publisherExists = await _publisherRepository.Exists(id);
				return !publisherExists;
			}).WithMessage("{PropertyName} does not exist.");
	}
}
