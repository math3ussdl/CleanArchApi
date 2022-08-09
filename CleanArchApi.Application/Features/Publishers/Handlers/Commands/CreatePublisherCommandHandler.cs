namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Publisher.Validators;
using Exceptions;
using MediatR;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class CreatePublisherCommandHandler :
	IRequestHandler<CreatePublisherCommand, BaseCommandResponse>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public CreatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(CreatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.PublisherCreateDto;

		var validator = new PublisherCreateDtoValidator();
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
			var publisher = _mapper.Map<Publisher>(body);
			await _publisherRepository.Add(publisher);

			response.Success = true;
			response.Message = "Publisher successfully created!";
		}

		return response;
	}
}
