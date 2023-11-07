using DBManager.Entities;
using DBManager.QueryResults;
using DBManager.Provider;

namespace DBManager.Service
{
    internal class DbService<T> : IDbService<T> where T : Entity
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

		public CreateQueryResult AddEntity(T entity)
		{
			Func<Context.DbContext, CreateQueryResult> AddEntityFunc = context =>
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

				return new CreateQueryResult(entity, true);
			};

			var context = _dbProvider.GetDbContext();
			return context.ExecuteQuery(AddEntityFunc);
		}
	
		public ReadQueryResult GetAllEntities()
		{
			Func<Context.DbContext, ReadQueryResult> GetAllEntitiesFunc = context =>
			{
				List<T> entities = new List<T>(context.Set<T>().AsEnumerable());
				return new ReadQueryResult(entities, entities?.Count != 0);
			};

			var context = _dbProvider.GetDbContext();
			return context.ExecuteQuery(GetAllEntitiesFunc);
		}

		public ReadQueryResult GetEntityById(int id)
		{
			Func<Context.DbContext, ReadQueryResult> GetEntityById = context =>
			{ 
				T entity = context.Set<T>().FirstOrDefault(x => x.Id == id);
				return new ReadQueryResult(entity, entity != null);
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
