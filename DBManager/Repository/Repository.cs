using DBManager.Entities;
using DBManager.QueryResults;
using DBManager.Service;

namespace DBManager.Repository
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		private IDbService<T> _dbService;

		public Repository()
		{
			_dbService = new DbService<T>();
		}

		public CreateQueryResult AddEntity(T entity)
		{
			return _dbService.AddEntity(entity);
		}

		public ReadQueryResult GetAllEntities()
		{
			return _dbService.GetAllEntities();
		}

		public T GetEntityById(int id)
		{
			return null;//_dbService.GetEntityById(id);
		}
	}
}
