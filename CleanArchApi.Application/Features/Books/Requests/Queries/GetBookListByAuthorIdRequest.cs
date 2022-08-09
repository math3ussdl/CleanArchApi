namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;

public class GetBookListByAuthorIdRequest : IRequest<List<BookDetailDto>>
{
	public int AuthorId { get; set; }
}
