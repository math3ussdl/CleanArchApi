namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using DTOs.Publisher;
using MediatR;

public class GetPublisherDetailRequest : IRequest<PublisherDetailDto>
{
	public int Id { get; set; }
}
