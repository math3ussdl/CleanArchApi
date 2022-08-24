namespace CleanArchApi.Application.UnitTests.Features.Books.Commands;

using AutoMapper;

using Application.Features.Books.Requests.Commands;
using Application.Features.Books.Handlers.Commands;
using Contracts.Persistence;
using DTOs.Book;
using Profiles;
using Responses;
using Mocks.Repositories;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockRepo;
    private readonly CreateBookCommandHandler _handler;
    private readonly BookCreateDto _bookCreateDto;
    
    public CreateBookCommandHandlerTests()
    {
	    var mockPublisherRepository = MockPublisherRepository.GetMock();
        _mockRepo = MockBookRepository.GetMock();
        
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        var mapper = mapperConfig.CreateMapper();
        
        _handler = new CreateBookCommandHandler(
            _mockRepo.Object, mockPublisherRepository.Object, mapper);

        var bookFaker = new Faker<BookCreateDto>()
            .RuleFor(b => b.Title, f => f.Company.CompanyName())
            .RuleFor(b => b.AuthorId, f => f.Random.Int(1, 6))
            .RuleFor(b => b.PublisherId, f => f.Random.Int(1, 4));
        _bookCreateDto = bookFaker.Generate(1)[0];
    }
    
    [Fact]
    public async Task CreateBook_ValidationErrorTestCase()
    {
    	_bookCreateDto.Title = "";

    	var result = await _handler.Handle(
    		new CreateBookCommand { BookCreateDto = _bookCreateDto },
    		CancellationToken.None
    	);

    	var books = await _mockRepo.Object.GetAll();

    	result.ShouldBeOfType<BaseResponse>();
    	result.Success.ShouldBe(false);
    	result.Message.ShouldBe<string>("Validation failed!");
    	result.ErrorType.ShouldBe(ErrorTypes.MalformedBody);
    	result.Errors.ShouldNotBeNull();
    	result.Errors.ShouldNotBeEmpty();
    	result.Errors[0].ShouldBe<string>("Title is required.");

    	books.Count.ShouldBe(4);
    }
    
	[Fact]
	public async Task CreateBook_UnexpectedErrorTestCase()
	{
	    var result = await _handler.Handle(
    		new CreateBookCommand(),
    		CancellationToken.None
	    );

	    result.ShouldBeOfType<BaseResponse>();
	    result.Success.ShouldBe(false);
	    result.Message.ShouldNotBeNullOrEmpty();
	    result.ErrorType.ShouldBe(ErrorTypes.Internal);
	}

	[Fact]
	public async Task CreateBook_SuccessTestCase()
	{
		var result = await _handler.Handle(
    		new CreateBookCommand { BookCreateDto = _bookCreateDto },
    		CancellationToken.None
	    );

	    var books = await _mockRepo.Object.GetAll();

	    result.ShouldBeOfType<BaseResponse>();
	    result.Success.ShouldBe(true);
	    result.Message.ShouldBe<string>("Book successfully created!");
	    result.Errors.ShouldBeNull();

	    books.Count.ShouldBe(5);
	}
}
