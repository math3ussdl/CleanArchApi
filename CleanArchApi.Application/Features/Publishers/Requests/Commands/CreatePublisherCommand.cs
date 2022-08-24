namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;

using DTOs.Publisher;
using Responses;

public class CreatePublisherCommand : IRequest<BaseResponse>
{
	public PublisherCreateDto PublisherCreateDto { get; set; }
}
