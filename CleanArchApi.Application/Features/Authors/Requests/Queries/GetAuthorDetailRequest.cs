namespace CleanArchApi.Application.Features.Authors.Requests.Queries;

using MediatR;

using DTOs.Author;

public class GetAuthorDetailRequest : IRequest<AuthorDetailDto>
{
	public int Id { get; set; }
}
