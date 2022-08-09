namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Publisher.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class UpdatePublisherCommandHandler :
	IRequestHandler<UpdatePublisherCommand, Unit>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(UpdatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.PublisherUpdateDto;

		var validator = new PublisherUpdateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var publisher = await _publisherRepository.Get(body.Id);

		if (publisher == null)
			throw new NotFoundException(nameof(Publisher), body.Id);

		_mapper.Map(body, publisher);

		await _publisherRepository.Update(publisher);

		return Unit.Value;
	}
}

