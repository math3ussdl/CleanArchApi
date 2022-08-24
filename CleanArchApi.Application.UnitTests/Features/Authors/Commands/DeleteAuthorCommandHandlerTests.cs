namespace CleanArchApi.Application.UnitTests.Features.Authors.Commands;

using AutoMapper;

using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Handlers.Commands;
using Contracts.Persistence;
using Mocks.Repositories;
using Profiles;
using Responses;

public class DeleteAuthorCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly DeleteAuthorCommandHandler _handler;
	private readonly int _id;

	public DeleteAuthorCommandHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new DeleteAuthorCommandHandler(_mockRepo.Object, _mapper);
		_id = It.IsInRange(1, 6, Moq.Range.Inclusive);
	}

	[Fact]
	public async Task DeleteAuthor_NotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new DeleteAuthorCommand { Id = 8 },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Author not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}

	[Fact]
	public async Task DeleteAuthor_UnexpectedErrorTestCase()
	{
		var mockRepo = MockAuthorRepository.GetMockWithExcept();
		var customHandler = new DeleteAuthorCommandHandler(
			mockRepo.Object, _mapper);
		
		var result = await customHandler.Handle(
			new DeleteAuthorCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task DeleteAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new DeleteAuthorCommand { Id = _id },
			CancellationToken.None
		);

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Author successfully deleted!");

		authors.Count.ShouldBe(6);
	}
}
