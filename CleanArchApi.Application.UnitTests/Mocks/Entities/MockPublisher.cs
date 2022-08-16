namespace CleanArchApi.Application.UnitTests.Mocks.Entities;

using Domain;

public static class MockPublisher
{
	public static Faker<Publisher> Mock()
	{
		return new Faker<Publisher>("pt_BR")
			.RuleFor(p => p.Id, f => f.IndexFaker)
			.RuleFor(p => p.Name, f => f.Company.CompanyName())
			.RuleFor(p => p.CreateAt, f => f.Date.Recent(100));
	}
}
