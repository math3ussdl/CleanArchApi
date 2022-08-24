namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetBookListByAuthorIdRequestHandler :
	IRequestHandler<GetBookListByAuthorIdRequest, BaseQueryResponse<List<BookDetailDto>>>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookListByAuthorIdRequestHandler(IAuthorRepository authorRepository,
		IBookRepository bookRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<List<BookDetailDto>>> Handle(GetBookListByAuthorIdRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<List<BookDetailDto>> response = new();

		try
		{
			var authorExists = await _authorRepository.Exists(request.AuthorId);
			
			if (!authorExists)
			{
				response.Success = false;
				response.Message = "Author not found!";
				response.ErrorType = ErrorTypes.NotFound;
			}
			else
			{
				var books = await _bookRepository.GetByAuthor(request.AuthorId);

				response.Success = true;
				response.Data = _mapper.Map<List<BookDetailDto>>(books);
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
