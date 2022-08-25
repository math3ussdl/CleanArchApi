namespace CleanArchApi.Application.Features.Authors.Handlers.Commands;

using AutoMapper;
using MediatR;

using DTOs.Author.Validators;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class UpdateAuthorCommandHandler :
	IRequestHandler<UpdateAuthorCommand, BaseResponse>
{
	private readonly IAuthorRepository _authorRepository;
	private readonly IMapper _mapper;

	public UpdateAuthorCommandHandler(IAuthorRepository authorRepository,
		IMapper mapper)
	{
		_authorRepository = authorRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(UpdateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var body = request.AuthorUpdateDto;

			var validator = new AuthorUpdateDtoValidator();
			var validationResult = await validator.ValidateAsync(body, cancellationToken);

			if (!validationResult.IsValid)
			{
				response.Success = false;
				response.Message = "Validation failed!";
				response.ErrorType = ErrorTypes.MalformedBody;
				response.Errors = validationResult.Errors
					.Select(e => e.ErrorMessage)
					.ToList();
			}
			else
			{
				var author = await _authorRepository.Get(request.AuthorUpdateDto.Id);

				if (author == null)
				{
					response.Success = false;
					response.Message = "Author not found!";
					response.ErrorType = ErrorTypes.NotFound;
				}
				else
				{
					_mapper.Map(body, author);
					await _authorRepository.Update(author);

					response.Success = true;
					response.Message = "Author successfully updated!";
				}
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
