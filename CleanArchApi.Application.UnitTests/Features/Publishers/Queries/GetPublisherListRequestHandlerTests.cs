namespace CleanArchApi.Application.UnitTests.Features.Publishers.Queries;

using AutoMapper;

using Application.Features.Publishers.Requests.Queries;
using Application.Features.Publishers.Handlers.Queries;
using Contracts.Persistence;
using DTOs.Publisher;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetPublisherListRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IPublisherRepository> _mockRepo;
	
	public GetPublisherListRequestHandlerTests()
	{
		_mockRepo = MockPublisherRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();
	}
	
	[Fact]
	public async Task GetPublisherList_UnexpectedErrorTestCase()
	{
		var mockRepo = MockPublisherRepository.GetMockWithExcept();
		
		var customHandler = new GetPublisherListRequestHandler(mockRepo.Object, _mapper);
		var result = await customHandler.Handle(new GetPublisherListRequest(),
			CancellationToken.None);
		
		result.ShouldBeOfType<BaseQueryResponse<List<PublisherDetailDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task GetPublisherList_SuccessCaseTest()
	{
		var handler = new GetPublisherListRequestHandler(_mockRepo.Object, _mapper);
		var result = await handler.Handle(new GetPublisherListRequest(),
			CancellationToken.None);

		result.ShouldBeOfType<BaseQueryResponse<List<PublisherDetailDto>>>();
		result.Success.ShouldBe(true);
		result.Data.Count.ShouldBe(4);
	}
}
