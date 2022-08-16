namespace CleanArchApi.Application.UnitTests.Features.Authors.Queries;

using AutoMapper;

using Application.Contracts.Persistence;
using Application.DTOs.Author;
using Application.Features.Authors.Requests.Queries;
using Application.Features.Authors.Handlers.Queries;
using Application.Profiles;
using Mocks.Repositories;

public class GetAuthorDetailRequestHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly int _id;

	public GetAuthorDetailRequestHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();
		_id = It.IsInRange<int>(1, 6, Moq.Range.Inclusive);
	}

	[Fact]
	public async Task GetAuthorDetail_SuccessCaseTest()
	{
		var authors = await _mockRepo.Object.GetAll();

		var handler = new GetAuthorDetailRequestHandler(_mockRepo.Object, _mapper);
		var result = await handler.Handle(new GetAuthorDetailRequest { Id = _id },
			CancellationToken.None);

		result.ShouldBeOfType<AuthorDetailDto>();
		result.Id = _id;
	}
}
