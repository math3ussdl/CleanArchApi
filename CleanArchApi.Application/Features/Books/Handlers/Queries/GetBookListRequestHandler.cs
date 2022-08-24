namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetBookListRequestHandler :
	IRequestHandler<GetBookListRequest, BaseQueryResponse<List<BookDetailDto>>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookListRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<List<BookDetailDto>>> Handle(GetBookListRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<List<BookDetailDto>> response = new();

		try
		{
			var books = await _bookRepository.GetAll();
			
			response.Success = true;
			response.Data = _mapper.Map<List<BookDetailDto>>(books);
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
