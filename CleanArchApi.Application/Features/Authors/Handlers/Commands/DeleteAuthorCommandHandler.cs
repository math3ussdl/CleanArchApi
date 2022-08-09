namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;
using Responses;

public class DeleteAuthorCommandHandler :
	IRequestHandler<DeleteAuthorCommand, BaseCommandResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public DeleteAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(DeleteAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var author = await _authorRepository.Get(request.Id);

		if (author == null)
		{
			response.Success = false;
			response.Message = "Author not found!";
		} else
		{
			await _authorRepository.Delete(author);

			response.Success = true;
			response.Message = "Author successfully deleted!";
		}

		return response;
	}
}
