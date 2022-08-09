namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using MediatR;

using DTOs.Publisher;

public class GetPublisherListRequest : IRequest<List<PublisherDetailDto>>
{
}
