namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;
using Responses;

public class GetBookListByPublisherIdRequest : IRequest<BaseQueryResponse<List<BookDetailDto>>>
{
	public int PublisherId { get; set; }
}
