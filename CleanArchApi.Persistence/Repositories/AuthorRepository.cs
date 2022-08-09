namespace CleanArchApi.Persistence.Repositories;

using Application.Persistence.Contracts;
using Domain;

public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
{
	public AuthorRepository(CleanArchApiDbContext dbContext) : base(dbContext)
	{
	}
}
