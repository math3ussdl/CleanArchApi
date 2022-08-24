namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using MediatR;

using DTOs.Book.Validators;
using Contracts.Persistence;
using Requests.Commands;
using Responses;

public class UpdateBookCommandHandler :
	IRequestHandler<UpdateBookCommand, BaseResponse>
{
	private readonly IBookRepository _bookRepository;
	private readonly IPublisherRepository _publisherRepository;
	private readonly IMapper _mapper;

	public UpdateBookCommandHandler(IBookRepository bookRepository,
		IPublisherRepository publisherRepository,
		IMapper mapper)
	{
		_bookRepository = bookRepository;
		_publisherRepository = publisherRepository;
		_mapper = mapper;
	}

	public async Task<BaseResponse> Handle(UpdateBookCommand request,
		CancellationToken cancellationToken)
	{
		BaseResponse response = new();

		try
		{
			var body = request.BookUpdateDto;

			var validator = new BookUpdateDtoValidator(_bookRepository, _publisherRepository);
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
				var book = await _bookRepository.Get(body.Id);

				if (book == null)
				{
					response.Success = false;
					response.ErrorType = ErrorTypes.NotFound;
					response.Message = "Book not found!";
				}
				else
				{
					_mapper.Map(body, book);
					await _bookRepository.Update(book);

					response.Success = true;
					response.Message = "Book successfully updated!";
				}
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
