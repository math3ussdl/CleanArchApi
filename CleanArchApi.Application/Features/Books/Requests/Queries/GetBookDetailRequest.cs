namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;
using Responses;

public class GetBookDetailRequest : IRequest<BaseQueryResponse<BookDetailDto>>
{
	public int Id { get; init; }
}
