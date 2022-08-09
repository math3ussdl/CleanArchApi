namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;

public class DeletePublisherCommand : IRequest<Unit>
{
	public int Id { get; set; }
}
