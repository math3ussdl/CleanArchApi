namespace CleanArchApi.Application.Persistence.Contracts;

public interface IGenericRepository<T, Id> where T : class
{
	Task<T> Get(Id id);
	Task<IReadOnlyList<T>> GetAll();
	Task<T> Add(T entity);
	Task<T> Update(T entity);
	Task<T> Delete(T entity);
}
