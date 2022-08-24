namespace CleanArchApi.Application.UnitTests.Features.Publishers.Commands;

using AutoMapper;

using Application.Features.Publishers.Requests.Commands;
using Application.Features.Publishers.Handlers.Commands;
using Contracts.Persistence;
using Mocks.Repositories;
using Profiles;
using Responses;

public class DeletePublisherCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IPublisherRepository> _mockRepo;
	private readonly DeletePublisherCommandHandler _handler;
	private readonly int _id;

	public DeletePublisherCommandHandlerTests()
	{
		_mockRepo = MockPublisherRepository.GetMock();
		
		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new DeletePublisherCommandHandler(_mockRepo.Object, _mapper);
		_id = It.IsInRange(1, 4, Moq.Range.Inclusive);
	}
	
	[Fact]
	public async Task DeletePublisher_NotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new DeletePublisherCommand { Id = 8 },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Publisher not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}

	[Fact]
	public async Task DeletePublisher_UnexpectedErrorTestCase()
	{
		var mockRepo = MockPublisherRepository.GetMockWithExcept();
		var customHandler = new DeletePublisherCommandHandler(
			mockRepo.Object, _mapper);
		
		var result = await customHandler.Handle(
			new DeletePublisherCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task DeletePublisher_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new DeletePublisherCommand { Id = _id },
			CancellationToken.None
		);

		var publishers = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Publisher successfully deleted!");

		publishers.Count.ShouldBe(4);
	}
}
