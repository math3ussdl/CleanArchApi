namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using MediatR;

public class DeleteBookCommand : IRequest<Unit>
{
	public int Id { get; set; }
}
