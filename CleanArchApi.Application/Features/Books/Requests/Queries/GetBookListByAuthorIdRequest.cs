namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using DTOs.Book;
using MediatR;

public class GetBookListByAuthorIdRequest : IRequest<List<BookDetailDto>>
{
	public int AuthorId { get; set; }
}
