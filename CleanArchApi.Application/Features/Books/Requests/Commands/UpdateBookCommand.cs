namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using DTOs.Book;
using MediatR;
using Responses;

public class UpdateBookCommand : IRequest<BaseCommandResponse>
{
	public BookUpdateDto BookUpdateDto { get; set; }
}
