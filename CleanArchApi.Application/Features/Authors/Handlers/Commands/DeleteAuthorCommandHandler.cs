namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class DeleteAuthorCommandHandler :
	IRequestHandler<DeleteAuthorCommand, BaseResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public DeleteAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(DeleteAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var author = await _authorRepository.Get(request.Id);

			if (author == null)
			{
				response.Success = false;
				response.ErrorType = ErrorTypes.NotFound;
				response.Message = "Author not found!";
			}
			else
			{
				await _authorRepository.Delete(author);

				response.Success = true;
				response.Message = "Author successfully deleted!";
			}
		}
		catch (System.Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;
			response.ErrorType = ErrorTypes.Internal;
		}

		return response;
	}
}
