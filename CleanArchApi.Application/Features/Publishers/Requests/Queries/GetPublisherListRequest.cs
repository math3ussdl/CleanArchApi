namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using DTOs.Publisher;
using MediatR;

public class GetPublisherListRequest : IRequest<List<PublisherDetailDto>>
{
}
