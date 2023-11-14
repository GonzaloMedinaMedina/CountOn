using DBManager.Entities;
using DBManager.QueryResults;
using DBManager.Provider;

namespace DBManager.Service
{
    public class DbService<T> : IDbService<T> where T : Entity
	{
		private IDbProvider _dbProvider;

		public DbService()
		{
			_dbProvider = new DbProvider();
		}
		public DbService(IDbProvider provider)
		{
			_dbProvider = provider;
		}

		public CreateQueryResult<T> AddEntity(T entity)
		{
			Func<Context.DbContext, CreateQueryResult<T>> AddEntityFunc = context =>
			{
				if (entity.Id != 0)
				{
					var entities = context.Set<T>().AsEnumerable();
					int lastId = 0;

					if (entities.Any())
					{
						lastId = entities.Max(x => x.Id);
					}

					entity.SetId(lastId + 1);
					context.Add(entity);
				}
				else
				{
					context.Update(entity);
				}

				return new CreateQueryResult<T>(entity, true);
			};

			var context = _dbProvider.GetDbContext();
			return context.ExecuteQuery(AddEntityFunc);
		}
	
		public ReadQueryResult<T> GetAllEntities()
		{
			Func<Context.DbContext, ReadQueryResult<T>> GetAllEntitiesFunc = context =>
			{
				List<T> entities = new List<T>(context.Set<T>().AsEnumerable());
				return new ReadQueryResult<T>(entities, entities?.Count != 0);
			};

			var context = _dbProvider.GetDbContext();
			return context.ExecuteQuery(GetAllEntitiesFunc);
		}

		public ReadQueryResult<T> GetEntityById(int id)
		{
			Func<Context.DbContext, ReadQueryResult<T>> GetEntityById = context =>
			{ 
				T entity = context.Set<T>().FirstOrDefault(x => x.Id == id);
				return new ReadQueryResult<T>(entity, entity != null);
			};

            var context = _dbProvider.GetDbContext();
            return context.ExecuteQuery(GetEntityById);
        }

		public QueryResult ExecuteQuery(Func<Context.DbContext, QueryResult> queryFunc)
		{
			var context = _dbProvider.GetDbContext();
			return context.ExecuteQuery(queryFunc);
		}
	}
}
