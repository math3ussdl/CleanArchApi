namespace CleanArchApi.Application.UnitTests.Features.Publishers.Queries;

using AutoMapper;

using Application.Features.Publishers.Requests.Queries;
using Application.Features.Publishers.Handlers.Queries;
using DTOs.Publisher;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetPublisherDetailRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly GetPublisherDetailRequestHandler _handler;
	private readonly int _id;

	public GetPublisherDetailRequestHandlerTests()
	{
		var mockRepo = MockPublisherRepository.GetMock();
		
		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});
		_mapper = mapperConfig.CreateMapper();

		_handler = new GetPublisherDetailRequestHandler(mockRepo.Object, _mapper);
		
		_id = It.IsInRange(1, 4, Moq.Range.Inclusive);
	}
	
	[Fact]
	public async Task GetPublisherDetail_NotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new GetPublisherDetailRequest { Id = 12 },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseQueryResponse<PublisherDetailDto>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Publisher not found!");
		result.ErrorType.ShouldBe(ErrorTypes.NotFound);
	}
	
	[Fact]
	public async Task GetPublisherDetail_UnexpectedErrorTestCase()
	{
		var mockRepo = MockPublisherRepository.GetMockWithExcept();
		var customHandler = new GetPublisherDetailRequestHandler(
			mockRepo.Object, _mapper);

		var result = await customHandler.Handle(
			new GetPublisherDetailRequest(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseQueryResponse<PublisherDetailDto>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}
	
	[Fact]
	public async Task GetPublisherDetail_SuccessCaseTest()
	{
		var result = await _handler.Handle(
			new GetPublisherDetailRequest { Id = _id },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseQueryResponse<PublisherDetailDto>>();
		result.Success.ShouldBe(true);
		result.Data.Id.ShouldBe(_id);
	}
}
