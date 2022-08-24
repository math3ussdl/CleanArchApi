namespace CleanArchApi.Application.UnitTests.Features.Authors.Queries;

using AutoMapper;

using Application.Features.Authors.Requests.Queries;
using Application.Features.Authors.Handlers.Queries;
using DTOs.Author;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetAuthorDetailRequestHandlerTests
{
  private readonly IMapper _mapper;
  private readonly GetAuthorDetailRequestHandler _handler;
  private readonly int _id;

  public GetAuthorDetailRequestHandlerTests()
  {
    var mockRepo = MockAuthorRepository.GetMock();

    var mapperConfig = new MapperConfiguration(c =>
    {
      c.AddProfile<MappingProfile>();
    });
    _mapper = mapperConfig.CreateMapper();

    _handler = new GetAuthorDetailRequestHandler(mockRepo.Object, _mapper);

    _id = It.IsInRange(1, 6, Moq.Range.Inclusive);
  }

	[Fact]
  public async Task GetAuthorDetail_NotFoundErrorTestCase()
  {
		var result = await _handler.Handle(
      new GetAuthorDetailRequest { Id = 8 },
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<AuthorDetailDto>>();
    result.Success.ShouldBe(false);
    result.Message.ShouldBe<string>("Author not found!");
    result.ErrorType.ShouldBe(ErrorTypes.NotFound);
  }

  [Fact]
  public async Task GetAuthorDetail_UnexpectedErrorTestCase()
  {
    var mockRepo = MockAuthorRepository.GetMockWithExcept();
    var customHandler = new GetAuthorDetailRequestHandler(
      mockRepo.Object, _mapper);

    var result = await customHandler.Handle(
      new GetAuthorDetailRequest(),
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<AuthorDetailDto>>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
  }

  [Fact]
  public async Task GetAuthorDetail_SuccessCaseTest()
  {
    var result = await _handler.Handle(
      new GetAuthorDetailRequest { Id = _id },
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<AuthorDetailDto>>();
    result.Success.ShouldBe(true);
    result.Data.Id.ShouldBe(_id);
  }
}
