namespace CleanArchApi.Application.Features.Publishers.Handlers.Commands;

using AutoMapper;
using Domain;
using Exceptions;
using MediatR;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class DeletePublisherCommandHandler :
	IRequestHandler<DeletePublisherCommand, BaseCommandResponse>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public DeletePublisherCommandHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseCommandResponse> Handle(DeletePublisherCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var publisher = await _publisherRepository.Get(request.Id);

		if (publisher == null)
		{
			response.Success = false;
			response.Message = "Publisher not found!";
		}
		else
		{
			await _publisherRepository.Delete(publisher);

			response.Success = true;
			response.Message = "Publisher successfully deleted!";
		}

		return response;
	}
}
