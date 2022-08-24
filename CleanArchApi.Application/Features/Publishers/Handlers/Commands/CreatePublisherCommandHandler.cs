namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Persistence;
using Domain;
using DTOs.Publisher.Validators;
using Requests.Commands;
using Responses;

public class CreatePublisherCommandHandler :
	IRequestHandler<CreatePublisherCommand, BaseResponse>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public CreatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(CreatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var body = request.PublisherCreateDto;

			var validator = new PublisherCreateDtoValidator();
			var validationResult = await validator.ValidateAsync(body, cancellationToken);

			if (validationResult.IsValid == false)
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
				var publisher = _mapper.Map<Publisher>(body);
				await _publisherRepository.Add(publisher);

				response.Success = true;
				response.Message = "Publisher successfully created!";
			}
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
