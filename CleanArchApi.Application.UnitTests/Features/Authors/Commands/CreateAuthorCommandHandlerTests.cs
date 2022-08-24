namespace CleanArchApi.Application.UnitTests.Features.Authors.Commands;

using AutoMapper;

using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Handlers.Commands;
using Contracts.Persistence;
using DTOs.Author;
using Profiles;
using Responses;
using Mocks.Infrastructure;
using Mocks.Repositories;

public class CreateAuthorCommandHandlerTests
{
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly CreateAuthorCommandHandler _handler;
	private readonly AuthorCreateDto _authorCreateDto;

	public CreateAuthorCommandHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		var mapper = mapperConfig.CreateMapper();
		var mockEmailSender = MockEmailSender.GetMock();

		_handler = new CreateAuthorCommandHandler(
			_mockRepo.Object, mapper, mockEmailSender.Object);

		var authorFaker = new Faker<AuthorCreateDto>()
			.RuleFor(a => a.Name, f => f.Name.FullName())
			.RuleFor(a => a.Email, f => f.Internet.Email())
			.RuleFor(a => a.Phone, f => f.Person.Phone);
		_authorCreateDto = authorFaker.Generate(1)[0];
	}

	[Fact]
	public async Task CreateAuthor_ValidationErrorTestCase()
	{
		_authorCreateDto.Name = "";

		var result = await _handler.Handle(
			new CreateAuthorCommand { AuthorCreateDto = _authorCreateDto },
			CancellationToken.None
		);

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.ErrorType.ShouldBe(ErrorTypes.MalformedBody);
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");

		authors.Count.ShouldBe(6);
	}

	[Fact]
	public async Task CreateAuthor_UnexpectedErrorTestCase()
	{
		var result = await _handler.Handle(
			new CreateAuthorCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task CreateAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new CreateAuthorCommand { AuthorCreateDto = _authorCreateDto },
			CancellationToken.None
		);

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Author successfully created!");

		authors.Count.ShouldBe(7);
	}
}
