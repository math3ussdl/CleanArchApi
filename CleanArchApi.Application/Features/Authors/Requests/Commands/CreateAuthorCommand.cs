namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using MediatR;

using DTOs.Author;
using Responses;

public class CreateAuthorCommand : IRequest<BaseResponse>
{
	public AuthorCreateDto AuthorCreateDto { get; set; }
}
