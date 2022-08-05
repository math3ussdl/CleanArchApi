namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using DTOs.Book;
using MediatR;

public class GetBookListRequest : IRequest<List<BookDetailDto>>
{
}
