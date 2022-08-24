namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;
using Responses;

public class GetBookListByAuthorIdRequest : IRequest<BaseQueryResponse<List<BookDetailDto>>>
{
	public int AuthorId { get; set; }
}
