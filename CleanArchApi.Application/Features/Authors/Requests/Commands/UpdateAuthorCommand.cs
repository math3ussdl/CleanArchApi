namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using DTOs.Author;
using MediatR;
using Responses;

public class UpdateAuthorCommand : IRequest<BaseCommandResponse>
{
	public AuthorUpdateDto AuthorUpdateDto { get; set; }
}
