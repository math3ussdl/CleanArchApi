namespace CleanArchApi.Application.UnitTests.Features.Books.Commands;

using AutoMapper;

using Application.Features.Books.Requests.Commands;
using Application.Features.Books.Handlers.Commands;
using DTOs.Book;
using Mocks.Repositories;
using Profiles;
using Responses;

public class UpdateBookCommandHandlerTests
{
	private readonly UpdateBookCommandHandler _handler;
	private readonly BookUpdateDto _bookUpdateDto;

	public UpdateBookCommandHandlerTests()
	{
		var mockRepo = MockBookRepository.GetMock();
		var mockPubRepo = MockPublisherRepository.GetMock();

		var mapperConfig = new MapperConfiguration(c =>
		{
			c.AddProfile<MappingProfile>();
		});

		var mapper = mapperConfig.CreateMapper();

		_handler = new UpdateBookCommandHandler(
			mockRepo.Object, mockPubRepo.Object, mapper);
		
		var bookFaker = new Faker<BookUpdateDto>()
			.RuleFor(b => b.Id, f => f.IndexFaker)
			.RuleFor(b => b.Title, f => f.Company.CompanyName())
			.RuleFor(b => b.AuthorId, f => f.Random.Int(1, 6))
			.RuleFor(b => b.PublisherId, f => f.Random.Int(1, 4));
		_bookUpdateDto = bookFaker.Generate(1)[0];
	}
	
	[Fact]
	public async Task UpdateBook_ValidationErrorTestCase()
	{
		_bookUpdateDto.Title = "";

		var result = await _handler.Handle(
			new UpdateBookCommand { BookUpdateDto = _bookUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Validation failed!");
		result.Errors.ShouldNotBeNull();
		result.Errors.ShouldNotBeEmpty();
		result.Errors[0].ShouldBe<string>("Title is required.");
	}

	[Fact]
	public async Task UpdateBook_UnexpectedErrorTestCase()
	{
		var result = await _handler.Handle(
			new UpdateBookCommand(),
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldNotBeNullOrEmpty();
		result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task UpdateBook_NotFoundErrorTestCase()
	{
		_bookUpdateDto.Id = 9;

		var result = await _handler.Handle(
			new UpdateBookCommand { BookUpdateDto = _bookUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(false);
		result.Message.ShouldBe<string>("Book not found!");
	}

	[Fact]
	public async Task UpdateBook_SuccessTestCase()
	{
		var result = await _handler.Handle(
			new UpdateBookCommand { BookUpdateDto = _bookUpdateDto },
			CancellationToken.None
		);

		result.ShouldBeOfType<BaseResponse>();
		result.Success.ShouldBe(true);
		result.Message.ShouldBe<string>("Book successfully updated!");
	}
}
