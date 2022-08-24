namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class DeleteBookCommandHandler :
	IRequestHandler<DeleteBookCommand, BaseResponse>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public DeleteBookCommandHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(DeleteBookCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var book = await _bookRepository.Get(request.Id);

			if (book == null)
			{
				response.Success = false;
				response.ErrorType = ErrorTypes.NotFound;
				response.Message = "Book not found!";
			}
			else
			{
				await _bookRepository.Delete(book);

				response.Success = true;
				response.Message = "Book successfully deleted!";
			}
		}
		catch (System.Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;
			response.ErrorType = ErrorTypes.Internal;
		}

		return response;
	}
}
