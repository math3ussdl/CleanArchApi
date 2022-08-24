namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using MediatR;

using Domain;
using DTOs.Book.Validators;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class CreateBookCommandHandler :
	IRequestHandler<CreateBookCommand, BaseResponse>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public CreateBookCommandHandler(IBookRepository bookRepository,
		IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(CreateBookCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var body = request.BookCreateDto;

			var validator = new BookCreateDtoValidator(_bookRepository, _publisherRepository);
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
				var book = _mapper.Map<Book>(body);
				await _bookRepository.Add(book);

				response.Success = true;
				response.Message = "Book successfully created!";
			}
		}
		catch (Exception ex)
		{
			response.Success = false;
			response.Message = ex.Message;
			response.ErrorType = ErrorTypes.Internal;
		}

		return response;
	}
}
