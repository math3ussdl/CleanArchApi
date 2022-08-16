namespace CleanArchApi.Application.UnitTests.Mocks.Entities;

using Domain;

public static class MockAuthor
{
	public static Faker<Author> Mock()
	{
		return new Faker<Author>("pt_BR")
			.RuleFor(a => a.Id, f => f.IndexFaker)
			.RuleFor(a => a.Name, f => f.Name.FullName())
			.RuleFor(
				a => a.Email, f => f.Internet.Email(f.Person.FirstName).ToLower())
			.RuleFor(a => a.Phone, f => f.Person.Phone)
			.RuleFor(a => a.CreateAt, f => f.Date.Recent(100));
	}
}
