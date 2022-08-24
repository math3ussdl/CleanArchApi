namespace CleanArchApi.Application.Features.Publishers.Requests.Commands;

using MediatR;

using DTOs.Publisher;
using Responses;

public class UpdatePublisherCommand : IRequest<BaseResponse>
{
	public PublisherUpdateDto PublisherUpdateDto { get; set; }
}
