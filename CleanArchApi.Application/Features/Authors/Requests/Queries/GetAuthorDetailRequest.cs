namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using MediatR;

using DTOs.Author;
using Responses;

public class GetAuthorDetailRequest : IRequest<BaseQueryResponse<AuthorDetailDto>>
{
	public int Id { get; init; }
}
