namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;

public class GetBookListByAuthorIdRequestHandler :
	IRequestHandler<GetBookListByAuthorIdRequest, List<BookDetailDto>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookListByAuthorIdRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<List<BookDetailDto>> Handle(GetBookListByAuthorIdRequest request,
		CancellationToken cancellationToken)
	{
		var books = await _bookRepository.GetByAuthor(request.AuthorId);
		return _mapper.Map<List<BookDetailDto>>(books);
	}
}
