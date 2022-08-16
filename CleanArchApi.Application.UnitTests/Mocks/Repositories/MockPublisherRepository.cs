namespace CleanArchApi.Application.UnitTests.Mocks.Repositories;

using Application.Contracts.Persistence;
using Domain;
using Entities;

public static class MockPublisherRepository
{
	public static Mock<IPublisherRepository> GetMock()
	{
		var publisherFaker = MockPublisher.Mock();

		List<Publisher> publishers = publisherFaker.Generate(4);
		var mockRepo = new Mock<IPublisherRepository>();

		var id = It.IsInRange<int>(1, 6, Moq.Range.Inclusive);

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
}
