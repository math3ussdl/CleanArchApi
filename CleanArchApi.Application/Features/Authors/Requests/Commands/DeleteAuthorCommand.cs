namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using MediatR;
using Responses;

public class DeleteAuthorCommand : IRequest<BaseCommandResponse>
{
	public int Id { get; set; }
}
