using DBManager.Entities;
using DBManager.Provider;
using DBManager.QueryResults;
using DBManager.Service;

namespace Service.Repository
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		private IDbService<T> _dbService;

		public Repository(IDbService<T> dbService)
		{
			_dbService = dbService;
		}

		public Repository(IDbProvider provider)
		{
			_dbService = new DbService<T>(provider);
		}

		public CreateQueryResult<T> AddEntity(T entity)
		{
			return _dbService.AddEntity(entity);
		}

		public ReadQueryResult<T> GetAllEntities()
		{
			return _dbService.GetAllEntities();
		}

		public ReadQueryResult<T> GetEntityById(int id)
		{
			return _dbService.GetEntityById(id);
		}

		public QueryResult RemoveEntity(T entity)
		{
			return _dbService.RemoveEntity(entity);
		}

		public CreateQueryResult<T> UpdateEntity(T entity) 
		{
			return _dbService.UpdateEntity(entity);
		}
	}
}
