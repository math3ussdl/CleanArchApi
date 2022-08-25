namespace CleanArchApi.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.Book;
using Application.Features.Books.Requests.Commands;
using Application.Features.Books.Requests.Queries;
using Application.Responses;

[Route("books")]
[ApiController]
public class BooksController : ControllerBase
{
	private readonly IMediator _mediator;

	public BooksController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<List<BookDetailDto>>> GetBooks()
	{
		var response = await _mediator.Send(new GetBookListRequest());
		return response.Success ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
	}
	
	[HttpGet("author/{authorId:int}")]
	public async Task<ActionResult<List<BookDetailDto>>> GetBooksByAuthorId(int authorId)
	{
		var response = await _mediator.Send(new GetBookListByAuthorIdRequest { AuthorId = authorId });

		if (response.Success) return Ok(response);
		
		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}
	
	[HttpGet("publisher/{publisherId:int}")]
	public async Task<ActionResult<List<BookDetailDto>>> GetBooksByPublisherId(int publisherId)
	{
		var response = await _mediator.Send(new GetBookListByPublisherIdRequest { PublisherId = publisherId });

		if (response.Success) return Ok(response);
		
		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<BookDetailDto>> GetBook(int id)
	{
		var response = await _mediator.Send(new GetBookDetailRequest { Id = id });

		if (response.Success) return Ok(response);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPost]
	public async Task<ActionResult> CreateBook([FromBody] BookCreateDto book)
	{
		var command = new CreateBookCommand { BookCreateDto = book };
		var response = await _mediator.Send(command);

		if (response.Success) return Ok(response);
		
		return response.ErrorType switch
		{
			ErrorTypes.MalformedBody => BadRequest(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPut]
	public async Task<ActionResult> UpdateBook([FromBody] BookUpdateDto book)
	{
		var command = new UpdateBookCommand { BookUpdateDto = book };
		var response = await _mediator.Send(command);
		
		if (response.Success) return StatusCode(StatusCodes.Status204NoContent);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			ErrorTypes.MalformedBody => BadRequest(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}
	
	[HttpDelete]
	public async Task<ActionResult> DeleteBook(int id)
	{
		var command = new DeleteBookCommand { Id = id };
		var response = await _mediator.Send(command);

		if (response.Success) return StatusCode(StatusCodes.Status204NoContent);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}
}
