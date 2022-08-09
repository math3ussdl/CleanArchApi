namespace CleanArchApi.Application.Contracts.Persistence;

using Domain;

public interface IBookRepository : IGenericRepository<Book, int>
{
	Task<IReadOnlyList<Book>> GetByAuthor(int authorId);
	Task<IReadOnlyList<Book>> GetByPublisher(int publisherId);
}
