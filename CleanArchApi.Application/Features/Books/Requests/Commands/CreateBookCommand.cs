namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using MediatR;

using DTOs.Book;
using Responses;

public class CreateBookCommand : IRequest<BaseResponse>
{
	public BookCreateDto BookCreateDto { get; set; }
}
