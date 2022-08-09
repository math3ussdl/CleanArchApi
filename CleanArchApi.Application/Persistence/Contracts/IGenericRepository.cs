namespace CleanArchApi.Application.Persistence.Contracts;

public interface IGenericRepository<T, Id> where T : class
{
	Task<bool> Exists(Id id);
	Task<T> Get(Id id);
	Task<IReadOnlyList<T>> GetAll();
	Task<T> Add(T entity);
	Task Update(T entity);
	Task Delete(T entity);
}
