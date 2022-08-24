namespace CleanArchApi.Application.UnitTests.Features.Publishers.Commands;

using AutoMapper;

using Application.Features.Publishers.Requests.Commands;
using Application.Features.Publishers.Handlers.Commands;
using DTOs.Publisher;
using Mocks.Repositories;
using Profiles;
using Responses;

public class UpdatePublisherCommandHandlerTests
{
	private readonly UpdatePublisherCommandHandler _handler;
	private readonly PublisherUpdateDto _publisherUpdateDto;

	public UpdatePublisherCommandHandlerTests()
	{
		var mockRepo = MockPublisherRepository.GetMock();
		
		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		var mapper = mapperConfig.CreateMapper();

		_handler = new UpdatePublisherCommandHandler(
			mockRepo.Object, mapper);

		var publisherFaker = new Faker<PublisherUpdateDto>()
			.RuleFor(p => p.Name, f => f.Company.CompanyName());
		_publisherUpdateDto = publisherFaker.Generate(1)[0];
	}
	
	[Fact]
	public async Task UpdatePublisher_ValidationErrorTestCase()
	{
		_publisherUpdateDto.Name = "";

		var result = await _handler.Handle(
			new UpdatePublisherCommand { PublisherUpdateDto = _publisherUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");
	}

	[Fact]
	public async Task UpdatePublisher_UnexpectedErrorTestCase()
	{
		var result = await _handler.Handle(
			new UpdatePublisherCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task UpdatePublisher_NotFoundErrorTestCase()
	{
		_publisherUpdateDto.Id = 9;

		var result = await _handler.Handle(
			new UpdatePublisherCommand { PublisherUpdateDto = _publisherUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Publisher not found!");
	}

	[Fact]
	public async Task UpdatePublisher_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new UpdatePublisherCommand { PublisherUpdateDto = _publisherUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Publisher successfully updated!");
	}
}
