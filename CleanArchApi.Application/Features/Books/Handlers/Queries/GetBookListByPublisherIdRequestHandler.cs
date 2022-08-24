namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Book;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetBookListByPublisherIdRequestHandler :
	IRequestHandler<GetBookListByPublisherIdRequest, BaseQueryResponse<List<BookDetailDto>>>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public GetBookListByPublisherIdRequestHandler(IPublisherRepository publisherRepository,
		IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<List<BookDetailDto>>> Handle(GetBookListByPublisherIdRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<List<BookDetailDto>> response = new();

		try
		{
			var publisherExists = await _publisherRepository.Exists(request.PublisherId);

			if (!publisherExists)
			{
				response.Success = false;
				response.Message = "Publisher not found!";
				response.ErrorType = ErrorTypes.NotFound;
			}
			else
			{
				var books = await _bookRepository.GetByPublisher(request.PublisherId);

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
