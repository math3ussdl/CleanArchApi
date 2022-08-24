namespace CleanArchApi.Application.UnitTests.Features.Books.Queries;

using AutoMapper;

using Application.Features.Books.Requests.Queries;
using Application.Features.Books.Handlers.Queries;
using Contracts.Persistence;
using DTOs.Book;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetBookListRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IBookRepository> _mockRepo;

	public GetBookListRequestHandlerTests()
	{
		_mockRepo = MockBookRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();
	}

	[Fact]
	public async Task GetBookList_UnexpectedErrorTestCase()
	{
		var mockRepo = MockBookRepository.GetMockWithExcept();
		
		var customHandler = new GetBookListRequestHandler(mockRepo.Object, _mapper);
		var result = await customHandler.Handle(new GetBookListRequest(),
			CancellationToken.None);
		
		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task GetBookList_SuccessCaseTest()
	{
		var handler = new GetBookListRequestHandler(_mockRepo.Object, _mapper);
		var result = await handler.Handle(new GetBookListRequest(),
			CancellationToken.None);

		result.ShouldBeOfType<BaseQueryResponse<List<BookDetailDto>>>();
		result.Success.ShouldBe(true);
		result.Data.Count.ShouldBe(4);
	}
}
