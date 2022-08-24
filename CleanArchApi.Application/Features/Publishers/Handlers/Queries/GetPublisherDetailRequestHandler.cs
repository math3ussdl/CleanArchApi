namespace CleanArchApi.Application.Features.Publishers.Handlers.Queries;

using AutoMapper;
using MediatR;

using DTOs.Publisher;
using Contracts.Persistence;
using Requests.Queries;
using Responses;

public class GetPublisherDetailRequestHandler :
	IRequestHandler<GetPublisherDetailRequest, BaseQueryResponse<PublisherDetailDto>>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public GetPublisherDetailRequestHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseQueryResponse<PublisherDetailDto>> Handle(GetPublisherDetailRequest request,
		CancellationToken cancellationToken)
	{
		BaseQueryResponse<PublisherDetailDto> response = new();

		try
		{
			var publisher = await _publisherRepository.Get(request.Id);

			if (publisher == null)
			{
				response.Success = false;
				response.Message = "Publisher not found!";
				response.ErrorType = ErrorTypes.NotFound;
			}
			else
			{
				response.Success = true;
				response.Data = _mapper.Map<PublisherDetailDto>(publisher);
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
