namespace CleanArchApi.Persistence.Repositories;

using Application.Persistence.Contracts;
using Domain;

public class PublisherRepository : GenericRepository<Publisher, int>, IPublisherRepository
{
	public PublisherRepository(CleanArchApiDbContext dbContext) : base(dbContext)
	{
	}
}
