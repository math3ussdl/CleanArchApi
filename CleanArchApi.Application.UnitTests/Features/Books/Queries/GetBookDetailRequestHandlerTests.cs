namespace CleanArchApi.Application.UnitTests.Features.Books.Queries;

using AutoMapper;

using Application.Features.Books.Requests.Queries;
using Application.Features.Books.Handlers.Queries;
using DTOs.Book;
using Mocks.Repositories;
using Profiles;
using Responses;

public class GetBookDetailRequestHandlerTests
{
  private readonly IMapper _mapper;
  private readonly GetBookDetailRequestHandler _handler;
  private readonly int _id;

  public GetBookDetailRequestHandlerTests()
  {
    var mockRepo = MockBookRepository.GetMock();

    var mapperConfig = new MapperConfiguration(c =>
    {
      c.AddProfile<MappingProfile>();
    });
    _mapper = mapperConfig.CreateMapper();

    _handler = new GetBookDetailRequestHandler(mockRepo.Object, _mapper);

    _id = It.IsInRange(1, 4, Moq.Range.Inclusive);
  }
  
  [Fact]
  public async Task GetBookDetail_NotFoundErrorTestCase()
  {
    var result = await _handler.Handle(
      new GetBookDetailRequest { Id = 12 },
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<BookDetailDto>>();
    result.Success.ShouldBe(false);
    result.Message.ShouldBe<string>("Book not found!");
    result.ErrorType.ShouldBe(ErrorTypes.NotFound);
  }
  
  [Fact]
  public async Task GetBookDetail_UnexpectedErrorTestCase()
  {
    var mockRepo = MockBookRepository.GetMockWithExcept();
    var customHandler = new GetBookDetailRequestHandler(
      mockRepo.Object, _mapper);

    var result = await customHandler.Handle(
      new GetBookDetailRequest(),
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<BookDetailDto>>();
    result.Success.ShouldBe(false);
    result.Message.ShouldNotBeNullOrEmpty();
    result.ErrorType.ShouldBe(ErrorTypes.Internal);
  }
  
  [Fact]
  public async Task GetBookDetail_SuccessCaseTest()
  {
    var result = await _handler.Handle(
      new GetBookDetailRequest { Id = _id },
      CancellationToken.None
    );

    result.ShouldBeOfType<BaseQueryResponse<BookDetailDto>>();
    result.Success.ShouldBe(true);
    result.Data.Id.ShouldBe(_id);
  }
}
