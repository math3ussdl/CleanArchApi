namespace CleanArchApi.Application.UnitTests.Features.Authors.Queries;

using AutoMapper;

using Application.Features.Authors.Requests.Queries;
using Application.Features.Authors.Handlers.Queries;
using Contracts.Persistence;
using DTOs.Author;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetAuthorListRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;

	public GetAuthorListRequestHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();
	}

	[Fact]
	public async Task GetAuthorList_UnexpectedErrorCaseTest()
	{
		var mockRepo = MockAuthorRepository.GetMockWithExcept();
		
		var customHandler = new GetAuthorListRequestHandler(mockRepo.Object, _mapper);
		var result = await customHandler.Handle(new GetAuthorListRequest(),
			CancellationToken.None);
		
		result.ShouldBeOfType<BaseQueryResponse<List<AuthorListDto>>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task GetAuthorList_SuccessCaseTest()
	{
		var handler = new GetAuthorListRequestHandler(_mockRepo.Object, _mapper);
		var result = await handler.Handle(new GetAuthorListRequest(),
			CancellationToken.None);

		result.ShouldBeOfType<BaseQueryResponse<List<AuthorListDto>>>();
		result.Success.ShouldBe(true);
		result.Data.Count.ShouldBe(6);
	}
}
