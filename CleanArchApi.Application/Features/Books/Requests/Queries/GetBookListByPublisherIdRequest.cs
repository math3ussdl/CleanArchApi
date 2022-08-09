namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;

public class GetBookListByPublisherIdRequest : IRequest<List<BookDetailDto>>
{
	public int PublisherId { get; set; }
}
