namespace CleanArchApi.Application.Features.Authors.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Author;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetAuthorDetailRequestHandler :
	IRequestHandler<GetAuthorDetailRequest, BaseQueryResponse<AuthorDetailDto>>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public GetAuthorDetailRequestHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<AuthorDetailDto>> Handle(GetAuthorDetailRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<AuthorDetailDto> response = new();

		try
		{
			var author = await _authorRepository.Get(request.Id);

			if (author == null)
			{
				response.Success = false;
				response.Message = "Author not found!";
				response.ErrorType = ErrorTypes.NotFound;
			}
			else
			{
				response.Success = true;
				response.Data = _mapper.Map<AuthorDetailDto>(author);
			}
		}
		catch (Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;
			response.ErrorType = ErrorTypes.Internal;
		}

		return response;
	}
}
