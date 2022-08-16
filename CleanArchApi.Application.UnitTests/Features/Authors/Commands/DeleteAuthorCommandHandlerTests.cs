namespace CleanArchApi.Application.UnitTests.Features.Authors.Commands;

using AutoMapper;

using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.DTOs.Author;
using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Handlers.Commands;
using Application.Profiles;
using Application.Responses;
using Mocks.Infrastructure;
using Mocks.Repositories;

public class DeleteAuthorCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly DeleteAuthorCommandHandler _handler;
	private readonly int _id;

	public DeleteAuthorCommandHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new DeleteAuthorCommandHandler(_mockRepo.Object, _mapper);
		_id = It.IsInRange<int>(1, 6, Moq.Range.Inclusive);
	}

	[Fact]
	public async Task DeleteAuthor_NotFoundErrorTestCase()
	{
		var result = await _handler.Handle(
			new DeleteAuthorCommand { Id = 8 },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(false);
		result.Message.ShouldBe<string>("Author not found!");
	}

	[Fact]
	public async Task DeleteAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new DeleteAuthorCommand { Id = _id },
			CancellationToken.None
		);

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(true);
		result.Message.ShouldBe<string>("Author successfully deleted!");

		authors.Count.ShouldBe<int>(6);
	}
}
