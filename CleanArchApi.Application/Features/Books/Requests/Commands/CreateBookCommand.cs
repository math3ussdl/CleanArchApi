namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using DTOs.Book;
using MediatR;

public class CreateBookCommand : IRequest<Unit>
{
	public BookCreateDto BookCreateDto { get; set; }
}
