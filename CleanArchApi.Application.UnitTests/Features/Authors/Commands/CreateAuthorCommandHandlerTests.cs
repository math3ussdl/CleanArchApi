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

public class CreateAuthorCommandHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAuthorRepository> _mockRepo;
	private readonly Mock<IEmailSender> _mockEmailSender;
	private readonly CreateAuthorCommandHandler _handler;
	private readonly AuthorCreateDto _authorCreateDto;

	public CreateAuthorCommandHandlerTests()
	{
		_mockRepo = MockAuthorRepository.GetMock();
		_mockEmailSender = MockEmailSender.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		_mapper = mapperConfig.CreateMapper();

		_handler = new CreateAuthorCommandHandler(
			_mockRepo.Object, _mapper, _mockEmailSender.Object);

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

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");

		authors.Count.ShouldBe(6);
	}

	[Fact]
	public async Task CreateAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new CreateAuthorCommand { AuthorCreateDto = _authorCreateDto },
			CancellationToken.None
		);
		result.Success = true;

		var authors = await _mockRepo.Object.GetAll();

		result.ShouldBeOfType<BaseCommandResponse>();
		result.Success.ShouldBe<bool>(true);
		result.Message.ShouldBe<string>("Author successfully created!");

		authors.Count.ShouldBe<int>(7);
	}
}
