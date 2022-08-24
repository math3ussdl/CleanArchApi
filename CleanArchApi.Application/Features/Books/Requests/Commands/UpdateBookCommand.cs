namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using MediatR;

using DTOs.Book;
using Responses;

public class UpdateBookCommand : IRequest<BaseResponse>
{
	public BookUpdateDto BookUpdateDto { get; set; }
}
