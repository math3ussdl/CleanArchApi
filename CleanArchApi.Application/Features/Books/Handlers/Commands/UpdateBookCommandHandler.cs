namespace CleanArchApi.Application.Features.Books.Handlers.Commands;

using AutoMapper;
using DTOs.Book.Validators;
using MediatR;
using Persistence.Contracts;
using Requests.Commands;
using Responses;

public class UpdateBookCommandHandler :
	IRequestHandler<UpdateBookCommand, BaseCommandResponse>
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

	public async Task<BaseCommandResponse> Handle(UpdateBookCommand request,
		CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		var body = request.BookUpdateDto;

		var validator = new BookUpdateDtoValidator(_bookRepository, _publisherRepository);
		var validationResult = await validator.ValidateAsync(body, cancellationToken);

		if (validationResult.IsValid == false)
		{
			response.Success = false;
			response.Message = "Validation failed!";
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

		return response;
	}
}
