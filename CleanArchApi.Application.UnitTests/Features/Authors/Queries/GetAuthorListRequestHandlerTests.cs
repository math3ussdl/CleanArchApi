namespace CleanArchApi.Application.UnitTests.Features.Authors.Queries;

using AutoMapper;

using Application.Contracts.Persistence;
using Application.DTOs.Author;
using Application.Features.Authors.Requests.Queries;
using Application.Features.Authors.Handlers.Queries;
using Application.Profiles;
using Mocks.Repositories;

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
	public async Task GetAuthorList_SuccessCaseTest()
	{
		var handler = new GetAuthorListRequestHandler(_mockRepo.Object, _mapper);
		var result = await handler.Handle(new GetAuthorListRequest(), CancellationToken.None);

		result.ShouldBeOfType<List<AuthorListDto>>();
		result.Count.ShouldBe(6);
	}
}
