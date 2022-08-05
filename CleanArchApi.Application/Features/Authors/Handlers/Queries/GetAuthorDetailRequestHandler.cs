namespace CleanArchApi.Application.Features.Authors.Handlers.Queries;

using AutoMapper;
using DTOs.Author;
using MediatR;
using Persistence.Contracts;
using Requests.Queries;

public class GetAuthorDetailRequestHandler :
	IRequestHandler<GetAuthorDetailRequest, AuthorDetailDto>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public GetAuthorDetailRequestHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<AuthorDetailDto> Handle(GetAuthorDetailRequest request,
		CancellationToken cancellationToken)
	{
		var author = await _authorRepository.Get(request.Id);
		return _mapper.Map<AuthorDetailDto>(author);
	}
}
