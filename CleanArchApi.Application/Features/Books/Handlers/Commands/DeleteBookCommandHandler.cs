namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;
using Responses;

public class DeleteBookCommandHandler :
	IRequestHandler<DeleteBookCommand, BaseCommandResponse>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public DeleteBookCommandHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(DeleteBookCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var book = await _bookRepository.Get(request.Id);

		if (book == null)
		{
			response.Success = false;
			response.Message = "Book not found!";
		}
		else
		{
			await _bookRepository.Delete(book);

			response.Success = true;
			response.Message = "Book successfully deleted!";
		}

		return response;
	}
}
