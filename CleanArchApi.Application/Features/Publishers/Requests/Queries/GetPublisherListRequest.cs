namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using MediatR;

using DTOs.Publisher;
using Responses;

public class GetPublisherListRequest : IRequest<BaseQueryResponse<List<PublisherDetailDto>>>
{
}
