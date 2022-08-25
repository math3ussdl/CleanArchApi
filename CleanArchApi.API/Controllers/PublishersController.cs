namespace CleanArchApi.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.Publisher;
using Application.Features.Publishers.Requests.Commands;
using Application.Features.Publishers.Requests.Queries;
using Application.Responses;

[Route("publishers")]
[ApiController]
public class PublishersController : ControllerBase
{
	private readonly IMediator _mediator;
	
	public PublishersController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet]
	public async Task<ActionResult<List<PublisherDetailDto>>> GetPublishers()
	{
		var response = await _mediator.Send(new GetPublisherListRequest());
		return response.Success ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<PublisherDetailDto>> GetPublisher(int id)
	{
		var response = await _mediator.Send(new GetPublisherDetailRequest { Id = id });

		if (response.Success) return Ok(response);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPost]
	public async Task<ActionResult> CreatePublisher([FromBody] PublisherCreateDto publisher)
	{
		var command = new CreatePublisherCommand { PublisherCreateDto = publisher };
		var response = await _mediator.Send(command);

		if (response.Success) return Ok(response);
		
		return response.ErrorType switch
		{
			ErrorTypes.MalformedBody => BadRequest(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}

	[HttpPut]
	public async Task<ActionResult> UpdatePublisher([FromBody] PublisherUpdateDto publisher)
	{
		var command = new UpdatePublisherCommand { PublisherUpdateDto = publisher };
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
	public async Task<ActionResult> DeletePublisher(int id)
	{
		var command = new DeletePublisherCommand { Id = id };
		var response = await _mediator.Send(command);

		if (response.Success) return StatusCode(StatusCodes.Status204NoContent);

		return response.ErrorType switch
		{
			ErrorTypes.NotFound => NotFound(response),
			_ => StatusCode(StatusCodes.Status500InternalServerError, response)
		};
	}
}
