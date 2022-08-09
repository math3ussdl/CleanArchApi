namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using DTOs.Author;
using MediatR;
using Responses;

public class CreateAuthorCommand : IRequest<BaseCommandResponse>
{
	public AuthorCreateDto AuthorCreateDto { get; set; }
}
