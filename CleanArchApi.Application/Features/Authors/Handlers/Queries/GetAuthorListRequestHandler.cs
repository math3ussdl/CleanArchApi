namespace CleanArchApi.Application.Features.Authors.Handlers.Queries;

using AutoMapper;
using DTOs.Author;
using MediatR;
using Persistence.Contracts;
using Requests.Queries;

public class GetAuthorListRequestHandler :
	IRequestHandler<GetAuthorListRequest, List<AuthorListDto>>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public GetAuthorListRequestHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<List<AuthorListDto>> Handle(GetAuthorListRequest request,
		CancellationToken cancellationToken)
	{
		var authors = await _authorRepository.GetAll();
		return _mapper.Map<List<AuthorListDto>>(authors);
	}
}
