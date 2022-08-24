namespace CleanArchApi.Application.UnitTests.Features.Publishers.Commands;

using AutoMapper;

using Application.Features.Publishers.Requests.Commands;
using Application.Features.Publishers.Handlers.Commands;
using Contracts.Persistence;
using DTOs.Publisher;
using Profiles;
using Responses;
using Mocks.Repositories;

public class CreatePublisherCommandHandlerTests
{
	private readonly Mock<IPublisherRepository> _mockRepo;
	private readonly CreatePublisherCommandHandler _handler;
	private PublisherCreateDto _publisherCreateDto;
	
	public CreatePublisherCommandHandlerTests()
	{
		_mockRepo = MockPublisherRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		var mapper = mapperConfig.CreateMapper();
        
		_handler = new CreatePublisherCommandHandler(_mockRepo.Object, mapper);

		var publisherFaker = new Faker<PublisherCreateDto>();
		_publisherCreateDto = publisherFaker.Generate(1)[0];
	}
	
	[Fact]
	public async Task CreatePublisher_ValidationErrorTestCase()
	{
		_publisherCreateDto.Name = "";

		var result = await _handler.Handle(
			new CreatePublisherCommand { PublisherCreateDto = _publisherCreateDto },
			CancellationToken.None
		);

		var publishers = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.ErrorType.ShouldBe(ErrorTypes.MalformedBody);
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");

		publishers.Count.ShouldBe(4);
	}
    
	[Fact]
	public async Task CreatePublisher_UnexpectedErrorTestCase()
	{
		var result = await _handler.Handle(
			new CreatePublisherCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task CreatePublisher_SuccessTestCase()
	{
		_publisherCreateDto = new PublisherCreateDto
		{
			Name = "AMAZONIA LTDA"
		};
		
		var result = await _handler.Handle(
			new CreatePublisherCommand { PublisherCreateDto = _publisherCreateDto },
			CancellationToken.None
		);

		var publishers = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Publisher successfully created!");
		result.Errors.ShouldBeNull();

		publishers.Count.ShouldBe(5);
	}
}
