namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using MediatR;

using DTOs.Publisher;

public class GetPublisherDetailRequest : IRequest<PublisherDetailDto>
{
	public int Id { get; set; }
}
