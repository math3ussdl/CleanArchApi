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

public class UpdateAuthorCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly UpdateAuthorCommandHandler _handler;
	private readonly AuthorUpdateDto _authorUpdateDto;

	public UpdateAuthorCommandHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new UpdateAuthorCommandHandler(
			_mockRepo.Object, _mapper);

		var authorFaker = new Faker<AuthorUpdateDto>()
			.RuleFor(a => a.Id, f => f.IndexFaker)
			.RuleFor(a => a.Name, f => f.Name.FullName())
			.RuleFor(a => a.Email, f => f.Internet.Email())
			.RuleFor(a => a.Phone, f => f.Person.Phone);

		_authorUpdateDto = authorFaker.Generate(1)[0];
	}

	[Fact]
	public async Task UpdateAuthor_ValidationErrorTestCase()
	{
		_authorUpdateDto.Name = "";

		var result = await _handler.Handle(
			new UpdateAuthorCommand { AuthorUpdateDto = _authorUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");
	}

	[Fact]
	public async Task UpdateAuthor_NotFoundErrorTestCase()
	{
		_authorUpdateDto.Id = 9;

		var result = await _handler.Handle(
			new UpdateAuthorCommand { AuthorUpdateDto = _authorUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(false);
		result.Message.ShouldBe<string>("Author not found!");
	}

	[Fact]
	public async Task UpdateAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new UpdateAuthorCommand { AuthorUpdateDto = _authorUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(true);
		result.Message.ShouldBe<string>("Author successfully updated!");
	}
}
