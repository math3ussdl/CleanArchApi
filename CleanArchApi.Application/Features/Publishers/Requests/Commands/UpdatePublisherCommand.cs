namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;

using DTOs.Publisher;
using Responses;

public class UpdatePublisherCommand : IRequest<BaseCommandResponse>
{
	public PublisherUpdateDto PublisherUpdateDto { get; set; }
}
