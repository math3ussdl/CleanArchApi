namespace CleanArchApi.Application.Persistence.Contracts;

using Domain;

public interface IBookRepository : IGenericRepository<Book, int>
{
	Task<IReadOnlyList<Book>> GetByAuthor(int authorId);
	Task<IReadOnlyList<Book>> GetByPublisher(int publisherId);
}
