namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using DTOs.Publisher;
using MediatR;

public class CreatePublisherCommand : IRequest<Unit>
{
	public PublisherCreateDto PublisherCreateDto { get; set; }
}
