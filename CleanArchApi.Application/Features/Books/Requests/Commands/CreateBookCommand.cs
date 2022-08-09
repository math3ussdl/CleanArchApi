namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using DTOs.Book;
using MediatR;
using Responses;

public class CreateBookCommand : IRequest<BaseCommandResponse>
{
	public BookCreateDto BookCreateDto { get; set; }
}
