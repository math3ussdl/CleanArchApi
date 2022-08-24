namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;
using Responses;

public class GetBookListRequest : IRequest<BaseQueryResponse<List<BookDetailDto>>>
{
}
