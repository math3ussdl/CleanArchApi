namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using Domain;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;

public class DeletePublisherCommandHandler :
	IRequestHandler<DeletePublisherCommand, Unit>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public DeletePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(DeletePublisherCommand request,
		CancellationToken cancellationToken)
	{
		var publisher = await _publisherRepository.Get(request.Id);

		if (publisher == null)
			throw new NotFoundException(nameof(Publisher), request.Id);

		await _publisherRepository.Delete(publisher);

		return Unit.Value;
	}
}
