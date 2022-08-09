namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Author.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class CreateAuthorCommandHandler :
	IRequestHandler<CreateAuthorCommand, Unit>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public CreateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(CreateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.AuthorCreateDto;

		var validator = new AuthorCreateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var author = _mapper.Map<Author>(body);
		await _authorRepository.Add(author);

		return Unit.Value;
	}
}
