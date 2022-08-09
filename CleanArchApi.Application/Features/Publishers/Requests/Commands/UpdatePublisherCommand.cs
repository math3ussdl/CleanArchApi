namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using DTOs.Publisher;
using MediatR;

public class UpdatePublisherCommand : IRequest<Unit>
{
	public PublisherUpdateDto PublisherUpdateDto { get; set; }
}
