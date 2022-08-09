namespace CleanArchApi.Persistence.Repositories;

using Application.Persistence.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;

public class BookRepository : GenericRepository<Book, int>, IBookRepository
{
	private readonly CleanArchApiDbContext _dbContext;

	public BookRepository(CleanArchApiDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IReadOnlyList<Book>> GetByAuthor(int authorId)
	{
		var booksByAuthor = await _dbContext.Books
			.Where(b => b.Author.Id == authorId)
			.ToListAsync();

		return booksByAuthor;
	}

	public async Task<IReadOnlyList<Book>> GetByPublisher(int publisherId)
	{
		var booksByPublisher = await _dbContext.Books
			.Where(b => b.Publisher.Id == publisherId)
			.ToListAsync();

		return booksByPublisher;
	}
}
