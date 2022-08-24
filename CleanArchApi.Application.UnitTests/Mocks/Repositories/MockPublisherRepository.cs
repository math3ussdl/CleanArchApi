namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Contracts.Persistence;
using Domain;
using Entities;

public static class MockPublisherRepository
{
	public static Mock<IPublisherRepository> GetMock()
	{
		var publisherFaker = MockPublisher.Mock();

		var publishers = publisherFaker.Generate(4);
		var mockRepo = new Mock<IPublisherRepository>();

		var id = It.IsInRange<int>(1, 4, Moq.Range.Inclusive);

		mockRepo.Setup(r => r.Exists(id)).ReturnsAsync(true);

		mockRepo.Setup(r => r.GetAll()).ReturnsAsync(publishers);

		mockRepo.Setup(r => r.Get(id)).ReturnsAsync(() =>
		{
			return publishers.First(p => p.Id == id);
		});

		mockRepo.Setup(r => r.Add(It.IsAny<Publisher>()))
			.ReturnsAsync((Publisher publisher) =>
			{
				publishers.Add(publisher);
				return publisher;
			});
		
		mockRepo.Setup(r => r.Delete(publishers.First(p => p.Id == id))).Callback(
			() =>
			{
				publishers.Remove(publishers.First(p => p.Id == id));
			});

		return mockRepo;
	}

	public static Mock<IPublisherRepository> GetMockWithExcept()
	{
		var mockRepo = GetMock();
		
		mockRepo.Setup(r => r.Exists(It.IsAny<int>())).Throws<Exception>();
		mockRepo.Setup(r => r.GetAll()).Throws<Exception>();
		mockRepo.Setup(r => r.Get(It.IsAny<int>())).Throws<Exception>();
		mockRepo.Setup(r => r.Add(It.IsAny<Publisher>())).Throws<Exception>();
		mockRepo.Setup(r => r.Update(It.IsAny<Publisher>())).Throws<Exception>();
		mockRepo.Setup(r => r.Delete(It.IsAny<Publisher>())).Throws<Exception>();

		return mockRepo;
	}
}
