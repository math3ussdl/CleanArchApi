namespace CleanArchApi.Application.Features.Books.Requests.Commands;

using MediatR;

using Responses;

public class DeleteBookCommand : IRequest<BaseResponse>
{
	public int Id { get; set; }
}
