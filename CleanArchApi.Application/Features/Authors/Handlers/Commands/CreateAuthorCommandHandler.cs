namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Author.Validators;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;
using Responses;

public class CreateAuthorCommandHandler :
	IRequestHandler<CreateAuthorCommand, BaseCommandResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public CreateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(CreateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.AuthorCreateDto;

		var validator = new AuthorCreateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
		{
			response.Success = false;
			response.Message = "Validation failed!";
			response.Errors = validationResult.Errors
				.Select(e => e.ErrorMessage)
				.ToList();
		}
		else
		{
			var author = _mapper.Map<Author>(body);
			await _authorRepository.Add(author);

			response.Success = true;
			response.Message = "Author successfully created!";
		}

		return response;
	}
}
