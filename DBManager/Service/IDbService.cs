using DBManager.Entities;
using DBManager.QueryResults;

namespace DBManager.Service
{
    public interface IDbService<T> where T : Entity
	{
		CreateQueryResult<T> AddEntity(T entity);
		ReadQueryResult<T> GetAllEntities();
        ReadQueryResult<T> GetEntityById(int id);
		QueryResult RemoveEntity(T entity);
		CreateQueryResult<T> UpdateEntity(T entity);
	}
}
