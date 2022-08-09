namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Book.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class CreateBookCommandHandler :	IRequestHandler<CreateBookCommand, Unit>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public CreateBookCommandHandler(IBookRepository bookRepository,
		IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(CreateBookCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.BookCreateDto;

		var validator = new BookCreateDtoValidator(_bookRepository, _publisherRepository);
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var book = _mapper.Map<Book>(body);
		await _bookRepository.Add(book);

		return Unit.Value;
	}
}
