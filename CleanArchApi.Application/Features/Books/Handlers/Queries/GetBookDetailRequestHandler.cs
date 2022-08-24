namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetBookDetailRequestHandler :
	IRequestHandler<GetBookDetailRequest, BaseQueryResponse<BookDetailDto>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookDetailRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<BookDetailDto>> Handle(GetBookDetailRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<BookDetailDto> response = new();

		try
		{
			var book = await _bookRepository.Get(request.Id);

			if (book == null)
			{
				response.Success = false;
				response.Message = "Book not found!";
				response.ErrorType = ErrorTypes.NotFound;
			}
			else
			{
				response.Success = true;
				response.Data = _mapper.Map<BookDetailDto>(book);				
			}
		}
		catch (Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;
			response.ErrorType = ErrorTypes.Internal;
		}

		return response;
	}
}
