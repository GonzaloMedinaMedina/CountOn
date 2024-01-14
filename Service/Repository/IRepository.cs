using DBManager.Entities;
using DBManager.QueryResults;

namespace Service.Repository
{
	public interface IRepository<T> where T : Entity
	{
		CreateQueryResult<T> AddEntity(T entity);
		ReadQueryResult<T> GetAllEntities();
        ReadQueryResult<T> GetEntityById(int id);
		QueryResult RemoveEntity(T entity);
		CreateQueryResult<T> UpdateEntity(T entity);
	}
}
