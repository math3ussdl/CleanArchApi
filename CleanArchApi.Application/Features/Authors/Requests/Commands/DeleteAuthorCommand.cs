namespace CleanArchApi.Application.Features.Authors.Requests.Commands;

using MediatR;

using Responses;

public class DeleteAuthorCommand : IRequest<BaseResponse>
{
	public int Id { get; set; }
}
