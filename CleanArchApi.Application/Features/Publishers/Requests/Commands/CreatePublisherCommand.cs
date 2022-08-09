namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;

using DTOs.Publisher;
using Responses;

public class CreatePublisherCommand : IRequest<BaseCommandResponse>
{
	public PublisherCreateDto PublisherCreateDto { get; set; }
}
