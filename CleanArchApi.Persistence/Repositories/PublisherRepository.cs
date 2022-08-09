namespace CleanArchApi.Persistence.Repositories;

using Application.Contracts.Persistence;
using Domain;

public class PublisherRepository : GenericRepository<Publisher, int>, IPublisherRepository
{
	public PublisherRepository(CleanArchApiDbContext dbContext) : base(dbContext)
	{
	}
}
