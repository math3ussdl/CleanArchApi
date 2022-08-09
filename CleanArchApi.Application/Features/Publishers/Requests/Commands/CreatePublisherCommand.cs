namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using DTOs.Publisher;
using MediatR;
using Responses;

public class CreatePublisherCommand : IRequest<BaseCommandResponse>
{
	public PublisherCreateDto PublisherCreateDto { get; set; }
}
