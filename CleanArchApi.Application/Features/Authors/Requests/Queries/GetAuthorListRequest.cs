namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using MediatR;

using DTOs.Author;
using Responses;

public class GetAuthorListRequest : IRequest<BaseQueryResponse<List<AuthorListDto>>>
{
}
