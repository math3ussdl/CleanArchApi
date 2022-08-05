namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using DTOs.Author;
using MediatR;

public class GetAuthorDetailRequest : IRequest<AuthorDetailDto>
{
	public int Id { get; set; }
}
