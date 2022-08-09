namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using MediatR;

public class DeleteAuthorCommand : IRequest<Unit>
{
	public int Id { get; set; }
}
