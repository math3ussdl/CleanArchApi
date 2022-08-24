namespace CleanArchApi.Application.Features.Publishers.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Publisher;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetPublisherListRequestHandler :
	IRequestHandler<GetPublisherListRequest, BaseQueryResponse<List<PublisherDetailDto>>>
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

	public async Task<BaseQueryResponse<List<PublisherDetailDto>>> Handle(GetPublisherListRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<List<PublisherDetailDto>> response = new();

		try
		{
			var publishers = await _publisherRepository.GetAll();

			response.Success = true;
			response.Data = _mapper.Map<List<PublisherDetailDto>>(publishers);
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
