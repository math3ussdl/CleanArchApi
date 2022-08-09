namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;
using Responses;

public class DeletePublisherCommand : IRequest<BaseCommandResponse>
{
	public int Id { get; set; }
}
