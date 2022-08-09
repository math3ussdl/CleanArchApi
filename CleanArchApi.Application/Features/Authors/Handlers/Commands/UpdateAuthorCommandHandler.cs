namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Author.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class UpdateAuthorCommandHandler :
	IRequestHandler<UpdateAuthorCommand, Unit>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public UpdateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(UpdateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.AuthorUpdateDto;

		var validator = new AuthorUpdateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var author = await _authorRepository.Get(body.Id);

		if (author == null)
			throw new NotFoundException(nameof(Author), body.Id);

		_mapper.Map(body, author);

		await _authorRepository.Update(author);

		return Unit.Value;
	}
}
