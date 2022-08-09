namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using Domain;
using DTOs.Publisher.Validators;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class CreatePublisherCommandHandler :
	IRequestHandler<CreatePublisherCommand, Unit>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public CreatePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(CreatePublisherCommand request,
		CancellationToken cancellationToken)
	{
		var body = request.PublisherCreateDto;

		var validator = new PublisherCreateDtoValidator();
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
			throw new ValidationException(validationResult);

		var publisher = _mapper.Map<Publisher>(body);
		await _publisherRepository.Add(publisher);

		return Unit.Value;
	}
}
