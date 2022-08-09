namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using DTOs.Publisher;
using MediatR;
using Responses;

public class UpdatePublisherCommand : IRequest<BaseCommandResponse>
{
	public PublisherUpdateDto PublisherUpdateDto { get; set; }
}
