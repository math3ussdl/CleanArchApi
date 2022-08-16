namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Application.Contracts.Persistence;
using Domain;
using Entities;

public static class MockBookRepository
{
	public static Mock<IBookRepository> GetMock()
	{
		var bookFaker = MockBook.Mock();

		List<Book> books = bookFaker.Generate(4);
		var mockRepo = new Mock<IBookRepository>();

		mockRepo.Setup(r => r.GetAll()).ReturnsAsync(books);
		mockRepo.Setup(r => r.Get(It.IsInRange<int>(1, 6, Moq.Range.Inclusive)));
		mockRepo.Setup(r => r.Add(It.IsAny<Book>())).ReturnsAsync((Book book) =>
		{
			books.Add(book);
			return book;
		});

		return mockRepo;
	}
}
