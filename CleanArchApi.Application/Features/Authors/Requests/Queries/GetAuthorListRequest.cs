namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using MediatR;

using DTOs.Author;

public class GetAuthorListRequest : IRequest<List<AuthorListDto>>
{
}
