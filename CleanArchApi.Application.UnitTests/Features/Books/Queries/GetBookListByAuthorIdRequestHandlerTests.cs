namespace CleanArchApi.Application.UnitTests.Features.Books.Queries;

using AutoMapper;

using Application.Features.Books.Requests.Queries;
using Application.Features.Books.Handlers.Queries;
using Contracts.Persistence;
using DTOs.Book;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetBookListByAuthorIdRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly GetBookListByAuthorIdRequestHandler _handler;
	private readonly Mock<IBookRepository> _mockRepo;
	private readonly int _authorId;

	public GetBookListByAuthorIdRequestHandlerTests()
	{
		_mockRepo = MockBookRepository.GetMock();
		var mockAuthorRepository = MockAuthorRepository.GetMock();
		
		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new GetBookListByAuthorIdRequestHandler(
			mockAuthorRepository.Object, _mockRepo.Object, _mapper);
		_authorId = It.IsInRange(1, 4, Moq.Range.Inclusive);
	}

	[Fact]
	public async Task GetBookListByAuthorId_AuthorNotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new GetBookListByAuthorIdRequest { AuthorId = 10 },
			CancellationToken.None
		);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Author not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}

	[Fact]
	public async Task GetBookListByAuthorId_UnexpectedErrorTestCase()
	{
		var mockAuthorRepo = MockAuthorRepository.GetMockWithExcept();

		var customHandler = new GetBookListByAuthorIdRequestHandler(
			mockAuthorRepo.Object, _mockRepo.Object, _mapper);
		var result = await customHandler.Handle(
			new GetBookListByAuthorIdRequest { AuthorId = _authorId }, CancellationToken.None);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}
	
	[Fact]
	public async Task GetBookListByAuthorId_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new GetBookListByAuthorIdRequest { AuthorId = _authorId },
			CancellationToken.None
		);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(true);
		result.Data.ShouldNotBeNull();
	}
}
