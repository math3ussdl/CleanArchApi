namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using Domain;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class DeleteBookCommandHandler :
	IRequestHandler<DeleteBookCommand, Unit>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public DeleteBookCommandHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(DeleteBookCommand request,
		CancellationToken cancellationToken)
	{
		var book = await _bookRepository.Get(request.Id);

		if (book == null)
			throw new NotFoundException(nameof(Book), request.Id);

		await _bookRepository.Delete(book);

		return Unit.Value;
	}
}
