namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using DTOs.Book;
using MediatR;

public class UpdateBookCommand : IRequest<Unit>
{
	public BookUpdateDto BookUpdateDto { get; set; }
}
