namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using MediatR;

using Contracts.Persistence;
using DTOs.Publisher.Validators;
using Requests.Commands;
using Responses;

public class UpdatePublisherCommandHandler :
	IRequestHandler<UpdatePublisherCommand, BaseResponse>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(UpdatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var body = request.PublisherUpdateDto;

			var validator = new PublisherUpdateDtoValidator();
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
				var publisher = await _publisherRepository.Get(body.Id);

				if (publisher == null)
				{
					response.Success = false;
					response.ErrorType = ErrorTypes.NotFound;
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

