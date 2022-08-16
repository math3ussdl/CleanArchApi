namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Infrastructure;
using Contracts.Persistence;
using Domain;
using DTOs.Author.Validators;
using Models;
using Requests.Commands;
using Responses;

public class CreateAuthorCommandHandler :
	IRequestHandler<CreateAuthorCommand, BaseCommandResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;
	private readonly IEmailSender _emailSender;

	public CreateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper, IEmailSender emailSender)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
		_emailSender = emailSender;
	}

	public async Task<BaseCommandResponse> Handle(CreateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.AuthorCreateDto;

		var validator = new AuthorCreateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (!validationResult.IsValid)
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

			Email email = new()
			{
				To = author.Email,
				Subject = "Account created!",
				Body = $"Hello, {author.Name}! Your account has been created!"
			};

			await _emailSender.SendEmail(email);

			response.Success = true;
			response.Message = "Author successfully created!";
		}

		return response;
	}
}
