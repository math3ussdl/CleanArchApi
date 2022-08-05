namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using DTOs.Book;
using MediatR;

public class GetBookDetailRequest : IRequest<BookDetailDto>
{
	public int Id { get; set; }
}
