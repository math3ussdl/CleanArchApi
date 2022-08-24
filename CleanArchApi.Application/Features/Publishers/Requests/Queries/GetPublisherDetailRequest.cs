namespace CleanArchApi.Application.Features.Publishers.Requests.Queries;

using MediatR;

using DTOs.Publisher;
using Responses;

public class GetPublisherDetailRequest : IRequest<BaseQueryResponse<PublisherDetailDto>>
{
	public int Id { get; set; }
}
