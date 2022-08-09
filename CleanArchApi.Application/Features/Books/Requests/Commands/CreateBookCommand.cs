namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using MediatR;

using DTOs.Book;
using Responses;

public class CreateBookCommand : IRequest<BaseCommandResponse>
{
	public BookCreateDto BookCreateDto { get; set; }
}
