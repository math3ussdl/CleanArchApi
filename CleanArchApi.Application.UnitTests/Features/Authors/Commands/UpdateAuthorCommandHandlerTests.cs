namespace CleanArchApi.Application.UnitTests.Features.Authors.Commands;

using AutoMapper;

using Application.Features.Authors.Requests.Commands;
using Application.Features.Authors.Handlers.Commands;
using DTOs.Author;
using Mocks.Repositories;
using Profiles;
using Responses;

public class UpdateAuthorCommandHandlerTests
{
	private readonly UpdateAuthorCommandHandler _handler;
	private readonly AuthorUpdateDto _authorUpdateDto;

	public UpdateAuthorCommandHandlerTests()
	{
		var mockRepo = MockAuthorRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		var mapper = mapperConfig.CreateMapper();

		_handler = new UpdateAuthorCommandHandler(
			mockRepo.Object, mapper);

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

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Name is required.");
	}

	[Fact]
	public async Task UpdateAuthor_UnexpectedErrorTestCase()
	{
		var result = await _handler.Handle(
			new UpdateAuthorCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task UpdateAuthor_NotFoundErrorTestCase()
	{
		_authorUpdateDto.Id = 9;

		var result = await _handler.Handle(
			new UpdateAuthorCommand { AuthorUpdateDto = _authorUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Author not found!");
	}

	[Fact]
	public async Task UpdateAuthor_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new UpdateAuthorCommand { AuthorUpdateDto = _authorUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Author successfully updated!");
	}
}
