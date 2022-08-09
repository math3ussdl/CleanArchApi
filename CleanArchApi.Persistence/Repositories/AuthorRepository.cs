namespace CleanArchApi.Persistence.Repositories;

using Application.Contracts.Persistence;
using Domain;

public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
{
	public AuthorRepository(CleanArchApiDbContext dbContext) : base(dbContext)
	{
	}
}
