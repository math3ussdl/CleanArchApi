namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Contracts.Persistence;
using Domain;
using Entities;

public static class MockBookRepository
{
	public static Mock<IBookRepository> GetMock()
	{
		var bookFaker = MockBook.Mock();

		var books = bookFaker.Generate(4);
		var mockRepo = new Mock<IBookRepository>();

		var id = It.IsInRange<int>(1, 4, Moq.Range.Inclusive);

		mockRepo.Setup(r => r.GetAll()).ReturnsAsync(books);

		mockRepo.Setup(r => r.Get(id)).ReturnsAsync(() =>
		{
			return books.First(b => b.Id == id);
		});
		
		mockRepo.Setup(r => r.GetByAuthor(id)).ReturnsAsync(() =>
		{
			return books.Where(b => b.Author.Id == id).ToList();
		});

		mockRepo.Setup(r => r.GetByPublisher(id)).ReturnsAsync(() =>
		{
			return books.Where(b => b.Publisher.Id == id).ToList();
		});

		mockRepo.Setup(r => r.Add(It.IsAny<Book>())).ReturnsAsync((Book book) =>
		{
			books.Add(book);
			return book;
		});

		mockRepo.Setup(r => r.Update(books.First(b => b.Id == id)));

		mockRepo.Setup(r => r.Delete(books.First(b => b.Id == id))).Callback(() =>
		{
			books.Remove(books.First(b => b.Id == id));
		});

		return mockRepo;
	}

	public static Mock<IBookRepository> GetMockWithExcept()
	{
		var mockRepo = GetMock();
		
		mockRepo.Setup(r => r.GetAll()).Throws<Exception>();
		mockRepo.Setup(r => r.Get(It.IsAny<int>())).Throws<Exception>();
		mockRepo.Setup(r => r.GetByAuthor(It.IsAny<int>())).Throws<Exception>();
		mockRepo.Setup(r => r.GetByPublisher(It.IsAny<int>())).Throws<Exception>();
		mockRepo.Setup(r => r.Add(It.IsAny<Book>())).Throws<Exception>();
		mockRepo.Setup(r => r.Update(It.IsAny<Book>())).Throws<Exception>();
		mockRepo.Setup(r => r.Delete(It.IsAny<Book>())).Throws<Exception>();

		return mockRepo;
	}
}
