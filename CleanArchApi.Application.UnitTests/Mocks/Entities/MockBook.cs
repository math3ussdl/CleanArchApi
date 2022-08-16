namespace CleanArchApi.Application.UnitTests.Mocks.Entities;

using Domain;

public static class MockBook
{
	public static Faker<Book> Mock()
	{
		var authorFaker = MockAuthor.Mock();
		var publisherFaker = MockPublisher.Mock();

		return new Faker<Book>("pt_BR")
			.RuleFor(b => b.Id, f => f.IndexFaker)
			.RuleFor(b => b.Title, f => f.Name.JobTitle())
			.RuleFor(b => b.IsDisponible, true)
			.RuleFor(b => b.Author, authorFaker.Generate(1)[0])
			.RuleFor(b => b.Publisher, publisherFaker.Generate(1)[0])
			.RuleFor(b => b.CreateAt, f => f.Date.Recent(100));
	}
}
