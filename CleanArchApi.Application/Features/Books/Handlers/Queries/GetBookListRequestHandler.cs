namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;

public class GetBookListRequestHandler :
	IRequestHandler<GetBookListRequest, List<BookDetailDto>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookListRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<List<BookDetailDto>> Handle(GetBookListRequest request,
		CancellationToken cancellationToken)
	{
		var books = await _bookRepository.GetAll();
		return _mapper.Map<List<BookDetailDto>>(books);
	}
}
