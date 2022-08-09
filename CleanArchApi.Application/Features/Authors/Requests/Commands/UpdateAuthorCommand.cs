namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using DTOs.Author;
using MediatR;

public class UpdateAuthorCommand : IRequest<Unit>
{
	public AuthorUpdateDto AuthorUpdateDto { get; set; }
}
