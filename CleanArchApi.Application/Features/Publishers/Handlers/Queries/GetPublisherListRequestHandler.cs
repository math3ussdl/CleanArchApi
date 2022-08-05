namespace CleanArchApi.Application.Features.Publishers.Handlers.Queries;

using AutoMapper;
using DTOs.Publisher;
using MediatR;
using Persistence.Contracts;
using Requests.Queries;

public class GetPublisherListRequestHandler :
	IRequestHandler<GetPublisherListRequest, List<PublisherDetailDto>>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public GetPublisherListRequestHandler(
		IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<List<PublisherDetailDto>> Handle(GetPublisherListRequest request,
		CancellationToken cancellationToken)
	{
		var publishers = await _publisherRepository.GetAll();
		return _mapper.Map<List<PublisherDetailDto>>(publishers);
	}
}
