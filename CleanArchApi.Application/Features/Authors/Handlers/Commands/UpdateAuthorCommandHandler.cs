namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using DTOs.Author.Validators;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;
using Responses;

public class UpdateAuthorCommandHandler :
	IRequestHandler<UpdateAuthorCommand, BaseCommandResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public UpdateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(UpdateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.AuthorUpdateDto;

		var validator = new AuthorUpdateDtoValidator();
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
			var author = await _authorRepository.Get(body.Id);

			if (author == null)
			{
				response.Success = false;
				response.Message = "Author not found!";
			}
			else
			{
				_mapper.Map(body, author);
				await _authorRepository.Update(author);

				response.Success = true;
				response.Message = "Author successfully updated!";
			}
		}

		return response;
	}
}
