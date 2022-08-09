namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;

public class GetBookListByPublisherIdRequestHandler :
	IRequestHandler<GetBookListByPublisherIdRequest, List<BookDetailDto>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookListByPublisherIdRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<List<BookDetailDto>> Handle(GetBookListByPublisherIdRequest request,
		CancellationToken cancellationToken)
	{
		var books = await _bookRepository.GetByPublisher(request.PublisherId);
		return _mapper.Map<List<BookDetailDto>>(books);
	}
}
