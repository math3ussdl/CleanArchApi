namespace CleanArchApi.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.Author;
using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Requests.Queries;

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
		var authors = await _mediator.Send(new GetAuthorListRequest());
		return StatusCode(StatusCodes.Status200OK, authors);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<AuthorDetailDto>> GetAuthor(int id)
	{
		var author = await _mediator.Send(new GetAuthorDetailRequest { Id = id });
		return StatusCode(StatusCodes.Status200OK, author);
	}

	[HttpPost]
	public async Task<ActionResult> CreateAuthor([FromBody] AuthorCreateDto author)
	{
		var command = new CreateAuthorCommand { AuthorCreateDto = author };
		var response = await _mediator.Send(command);

		if (response.Success)
		{
			return Ok(response);
		}
		return BadRequest(response);
	}

	[HttpPut]
	public async Task<ActionResult> UpdateAuthor([FromBody] AuthorUpdateDto author)
	{
		var command = new UpdateAuthorCommand { AuthorUpdateDto = author };
		await _mediator.Send(command);

		return StatusCode(StatusCodes.Status204NoContent);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAuthor(int id)
	{
		var command = new DeleteAuthorCommand { Id = id };
		await _mediator.Send(command);

		return StatusCode(StatusCodes.Status204NoContent);
	}
}
