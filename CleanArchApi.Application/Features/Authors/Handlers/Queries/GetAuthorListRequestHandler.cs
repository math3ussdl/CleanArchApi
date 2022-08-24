namespace CleanArchApi.Application.Features.Authors.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Author;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetAuthorListRequestHandler :
	IRequestHandler<GetAuthorListRequest, BaseQueryResponse<List<AuthorListDto>>>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public GetAuthorListRequestHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<List<AuthorListDto>>> Handle(GetAuthorListRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<List<AuthorListDto>> response = new();

		try
		{
			var authors = await _authorRepository.GetAll();

			response.Success = true;
			response.Data = _mapper.Map<List<AuthorListDto>>(authors);
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
