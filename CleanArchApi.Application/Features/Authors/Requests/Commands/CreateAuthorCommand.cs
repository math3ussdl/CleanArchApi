namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using DTOs.Author;
using MediatR;

public class CreateAuthorCommand : IRequest<Unit>
{
	public AuthorCreateDto AuthorCreateDto { get; set; }
}
