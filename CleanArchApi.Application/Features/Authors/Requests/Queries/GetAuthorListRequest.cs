namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using DTOs.Author;
using MediatR;

public class GetAuthorListRequest : IRequest<List<AuthorListDto>>
{
}
