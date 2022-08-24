namespace CleanArchApi.Application.UnitTests.Features.Books.Commands;

using AutoMapper;

using Application.Features.Books.Requests.Commands;
using Application.Features.Books.Handlers.Commands;
using Contracts.Persistence;
using Mocks.Repositories;
using Profiles;
using Responses;

public class DeleteBookCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IBookRepository> _mockRepo;
	private readonly DeleteBookCommandHandler _handler;
	private readonly int _id;

	public DeleteBookCommandHandlerTests()
	{
		_mockRepo = MockBookRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new DeleteBookCommandHandler(_mockRepo.Object, _mapper);
		_id = It.IsInRange(1, 4, Moq.Range.Inclusive);
	}
	
	[Fact]
	public async Task DeleteBook_NotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new DeleteBookCommand { Id = 8 },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Book not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}

	[Fact]
	public async Task DeleteBook_UnexpectedErrorTestCase()
	{
		var mockRepo = MockBookRepository.GetMockWithExcept();
		var customHandler = new DeleteBookCommandHandler(
			mockRepo.Object, _mapper);
		
		var result = await customHandler.Handle(
			new DeleteBookCommand(),
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
			new DeleteBookCommand { Id = _id },
			CancellationToken.None
		);

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Book successfully deleted!");

		authors.Count.ShouldBe(4);
	}
}
