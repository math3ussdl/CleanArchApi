namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using DTOs.Publisher.Validators;
using MediatR;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class UpdatePublisherCommandHandler :
	IRequestHandler<UpdatePublisherCommand, BaseCommandResponse>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(UpdatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.PublisherUpdateDto;

		var validator = new PublisherUpdateDtoValidator();
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
			var publisher = await _publisherRepository.Get(body.Id);

			if (publisher == null)
			{
				response.Success = false;
				response.Message = "Publisher not found!";
			}
			else
			{
				_mapper.Map(body, publisher);
				await _publisherRepository.Update(publisher);

				response.Success = true;
				response.Message = "Publisher successfully updated!";
			}
		}

		return response;
	}
}

