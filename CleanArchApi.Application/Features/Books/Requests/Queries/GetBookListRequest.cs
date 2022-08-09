namespace CleanArchApi.Application.Features.Books.Requests.Queries;

using MediatR;

using DTOs.Book;

public class GetBookListRequest : IRequest<List<BookDetailDto>>
{
}
