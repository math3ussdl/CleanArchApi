namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;

public class GetBookDetailRequest : IRequest<BookDetailDto>
{
	public int Id { get; set; }
}
