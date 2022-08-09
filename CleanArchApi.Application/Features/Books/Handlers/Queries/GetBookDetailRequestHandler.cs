﻿namespace CleanArchApi.Application.Features.Books.Handlers.Queries;

using AutoMapper;
using Domain;
using DTOs.Book;
using Exceptions;
using MediatR;
using Persistence.Contracts;
using Requests.Queries;

public class GetBookDetailRequestHandler :
	IRequestHandler<GetBookDetailRequest, BookDetailDto>
{
	private readonly IBookRepository _bookRepository;
	private readonly IMapper _mapper;

	public GetBookDetailRequestHandler(IBookRepository bookRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_mapper = mapper;
	}

	public async Task<BookDetailDto> Handle(GetBookDetailRequest request,
		CancellationToken cancellationToken)
	{
		var book = await _bookRepository.Get(request.Id);

		if (book == null)
			throw new NotFoundException(nameof(Book), request.Id);

		return _mapper.Map<BookDetailDto>(book);
	}
}
