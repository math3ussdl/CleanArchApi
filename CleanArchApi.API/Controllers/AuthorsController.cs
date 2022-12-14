namespace CleanArchApi.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.Author;
using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Requests.Queries;
using Application.Responses;

[Route("authors")]
[ApiController]
public class AuthorsController : ControllerBase
{
  private readonly IMediator _mediator;

	public AuthorsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<List<AuthorListDto>>> GetAuthors()
	{
		var response = await _mediator.Send(new GetAuthorListRequest());
		return response.Success ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<AuthorDetailDto>> GetAuthor(int id)
	{
		var response = await _mediator.Send(new GetAuthorDetailRequest { Id = id });

		if (response.Success) return Ok(response);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPost]
	public async Task<ActionResult> CreateAuthor([FromBody] AuthorCreateDto author)
	{
		var command = new CreateAuthorCommand { AuthorCreateDto = author };
		var response = await _mediator.Send(command);

		if (response.Success) return Ok(response);
		
		return response.ErrorType switch
		{
			ErrorTypes.MalformedBody => BadRequest(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPut]
	public async Task<ActionResult> UpdateAuthor([FromBody] AuthorUpdateDto author)
	{
		var command = new UpdateAuthorCommand { AuthorUpdateDto = author };
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
	public async Task<ActionResult> DeleteAuthor(int id)
	{
		var command = new DeleteAuthorCommand { Id = id };
		var response = await _mediator.Send(command);

		if (response.Success) return StatusCode(StatusCodes.Status204NoContent);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}
}
