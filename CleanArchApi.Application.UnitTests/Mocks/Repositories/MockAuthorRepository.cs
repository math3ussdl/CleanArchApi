namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Contracts.Persistence;
using Domain;
using Entities;

public static class MockAuthorRepository
{
  public static Mock<IAuthorRepository> GetMock()
  {
    var authorFaker = MockAuthor.Mock();

    var authors = authorFaker.Generate(6);
    var mockRepo = new Mock<IAuthorRepository>();

    var id = It.IsInRange(1, 6, Moq.Range.Inclusive);

    mockRepo.Setup(r => r.Exists(id)).ReturnsAsync(true);

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

    mockRepo.Setup(r => r.Update(It.IsAny<Author>()));

    mockRepo.Setup(r => r.Delete(authors.First(a => a.Id == id))).Callback(() =>
    {
      authors.Remove(authors.First(a => a.Id == id));
    });

    return mockRepo;
  }

  public static Mock<IAuthorRepository> GetMockWithExcept()
  {
    var mockRepo = GetMock();

    mockRepo.Setup(r => r.Exists(It.IsAny<int>())).Throws<Exception>();
    mockRepo.Setup(r => r.GetAll()).Throws<Exception>();
    mockRepo.Setup(r => r.Get(It.IsAny<int>())).Throws<Exception>();
    mockRepo.Setup(r => r.Add(It.IsAny<Author>())).Throws<Exception>();
    mockRepo.Setup(r => r.Update(It.IsAny<Author>())).Throws<Exception>();
    mockRepo.Setup(r => r.Delete(It.IsAny<Author>())).Throws<Exception>();

    return mockRepo;
  }
}
