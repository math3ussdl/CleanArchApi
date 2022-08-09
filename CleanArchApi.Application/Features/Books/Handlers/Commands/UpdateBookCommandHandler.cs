namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Book.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class UpdateBookCommandHandler :
	IRequestHandler<UpdateBookCommand, Unit>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public UpdateBookCommandHandler(IBookRepository bookRepository,
		IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(UpdateBookCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.BookUpdateDto;

		var validator = new BookUpdateDtoValidator(_bookRepository, _publisherRepository);
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var book = await _bookRepository.Get(body.Id);

		if (book == null)
			throw new NotFoundException(nameof(Book), body.Id);

		_mapper.Map(body, book);

		await _bookRepository.Update(book);

		return Unit.Value;
	}
}
