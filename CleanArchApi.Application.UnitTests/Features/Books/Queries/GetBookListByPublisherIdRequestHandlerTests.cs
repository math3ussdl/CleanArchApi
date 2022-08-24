namespace CleanArchApi.Application.UnitTests.Features.Books.Queries;

using AutoMapper;

using Application.Features.Books.Requests.Queries;
using Application.Features.Books.Handlers.Queries;
using Contracts.Persistence;
using DTOs.Book;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetBookListByPublisherIdRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly GetBookListByPublisherIdRequestHandler _handler;
	private readonly Mock<IBookRepository> _mockRepo;
	private readonly int _publisherId;

	public GetBookListByPublisherIdRequestHandlerTests()
	{
		_mockRepo = MockBookRepository.GetMock();
		var mockPublisherRepository = MockPublisherRepository.GetMock();
		
		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new GetBookListByPublisherIdRequestHandler(
			mockPublisherRepository.Object, _mockRepo.Object, _mapper);
		_publisherId = It.IsInRange(1, 4, Moq.Range.Inclusive);
	}
	
	[Fact]
	public async Task GetBookListByPublisherId_PublisherNotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new GetBookListByPublisherIdRequest { PublisherId = 10 },
			CancellationToken.None
		);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Publisher not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}

	[Fact]
	public async Task GetBookListByPublisherId_UnexpectedErrorTestCase()
	{
		var mockPublisherRepo = MockPublisherRepository.GetMockWithExcept();

		var customHandler = new GetBookListByPublisherIdRequestHandler(
			mockPublisherRepo.Object, _mockRepo.Object, _mapper);
		var result = await customHandler.Handle(
			new GetBookListByPublisherIdRequest { PublisherId = _publisherId }, CancellationToken.None);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}
	
	[Fact]
	public async Task GetBookListByPublisherId_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new GetBookListByPublisherIdRequest { PublisherId = _publisherId },
			CancellationToken.None
		);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(true);
		result.Data.ShouldNotBeNull();
	}
}
