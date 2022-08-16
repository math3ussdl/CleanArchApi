namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Application.Contracts.Persistence;
using Domain;
using Entities;

public static class MockAuthorRepository
{
	public static Mock<IAuthorRepository> GetMock()
	{
		var authorFaker = MockAuthor.Mock();

		List<Author> authors = authorFaker.Generate(6);
		var mockRepo = new Mock<IAuthorRepository>();

		var id = It.IsInRange<int>(1, 6, Moq.Range.Inclusive);

		mockRepo.Setup(r => r.GetAll()).ReturnsAsync(authors);
		mockRepo.Setup(r => r.Get(id)).ReturnsAsync(() =>
		{
			return authors.First(a => a.Id == id);
		});
		mockRepo.Setup(r => r.Add(It.IsAny<Author>())).ReturnsAsync((Author author) =>
		{
			authors.Add(author);
			return author;
		});
		mockRepo.Setup(r => r.Delete(authors.First(a => a.Id == id))).Callback(() =>
		{
			authors.Remove(authors.First(a => a.Id == id));
		});

		return mockRepo;
	}
}
