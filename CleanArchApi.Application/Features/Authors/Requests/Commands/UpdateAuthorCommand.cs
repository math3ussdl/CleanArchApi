namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using MediatR;

using DTOs.Author;
using Responses;

public class UpdateAuthorCommand : IRequest<BaseCommandResponse>
{
	public int Id { get; set; }
	public AuthorUpdateDto AuthorUpdateDto { get; set; }
}
