namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using Domain;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class DeleteAuthorCommandHandler :
	IRequestHandler<DeleteAuthorCommand, Unit>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public DeleteAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(DeleteAuthorCommand request,
		CancellationToken cancellationToken)
	{
		var author = await _authorRepository.Get(request.Id);

		if (author == null)
			throw new NotFoundException(nameof(Author), request.Id);

		await _authorRepository.Delete(author);

		return Unit.Value;
	}
}
