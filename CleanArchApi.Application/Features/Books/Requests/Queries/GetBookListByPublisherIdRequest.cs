namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using DTOs.Book;
using MediatR;

public class GetBookListByPublisherIdRequest : IRequest<List<BookDetailDto>>
{
	public int PublisherId { get; set; }
}
