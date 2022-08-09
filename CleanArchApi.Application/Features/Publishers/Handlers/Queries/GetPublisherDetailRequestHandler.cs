﻿namespace CleanArchApi.Application.Features.Publishers.Handlers.Queries;

using AutoMapper;
using Domain;
using DTOs.Publisher;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Queries;

public class GetPublisherDetailRequestHandler :
	IRequestHandler<GetPublisherDetailRequest, PublisherDetailDto>
{
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public GetPublisherDetailRequestHandler(IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<PublisherDetailDto> Handle(GetPublisherDetailRequest request,
		CancellationToken cancellationToken)
	{
		var publisher = await _publisherRepository.Get(request.Id);

		if (publisher == null)
			throw new NotFoundException(nameof(Publisher), request.Id);

		return _mapper.Map<PublisherDetailDto>(publisher);
	}
}
