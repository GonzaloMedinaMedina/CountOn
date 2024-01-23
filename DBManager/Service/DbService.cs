using DBManager.Entities;
using DBManager.QueryResults;
using DBManager.Provider;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using DBManager.Context;

namespace DBManager.Service
{
    public class DbService<T> : IDbService<T> where T : Entity
	{
		private readonly IDbContext _dbContext;
		public DbService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public DbService(IDbProvider provider)
		{
			_dbContext = provider.GetDbContext();
		}

		public CreateQueryResult<T> AddEntity(T entity)
		{
			Func<Context.DbContext, CreateQueryResult<T>> AddEntityFunc = context =>
			{
				var entities = context.Set<T>().AsEnumerable();
				bool result = true;

				if (entity.Id == -1)
				{
					int lastId = 0;

					if (entities.Any())
					{
						lastId = entities.Max(x => x.Id);
					}

					entity.SetId(lastId + 1);
					context.Add(entity);
				}
				else if (entities.Any(x => x.Id == entity.Id))
				{
					result = false;
				}

				return new CreateQueryResult<T>(entity, result);
			};

			return _dbContext.ExecuteQuery(AddEntityFunc);
		}
	
		public ReadQueryResult<T> GetAllEntities()
		{
			Func<Context.DbContext, ReadQueryResult<T>> GetAllEntitiesFunc = context =>
			{
				List<T> entities = new List<T>(context.Set<T>().AsEnumerable());
				return new ReadQueryResult<T>(entities, entities?.Count != 0);
			};

			return _dbContext.ExecuteQuery(GetAllEntitiesFunc);
		}

		public ReadQueryResult<T> GetEntityById(int id)
		{
			Func<Context.DbContext, ReadQueryResult<T>> GetEntityById = context =>
			{ 
				T entity = context.Set<T>().FirstOrDefault(x => x.Id == id);
				return new ReadQueryResult<T>(entity, entity != null);
			};

            return _dbContext.ExecuteQuery(GetEntityById);
        }

		public QueryResult RemoveEntity(T entity)
		{
			Func<Context.DbContext, QueryResult> RemoveEntity = context =>
			{
				bool result = false;
				T entityToDelete = context.Set<T>().FirstOrDefault(T => T.Id == entity.Id);

				if (entityToDelete != null)
				{
					EntityEntry<T> entityResult = context.Remove(entityToDelete);
					result = entityResult.State == EntityState.Deleted;
				}

				return new QueryResult(result);
			};

			return _dbContext.ExecuteQuery(RemoveEntity);
		}

		public CreateQueryResult<T> UpdateEntity(T entity) 
		{
			Func<Context.DbContext, CreateQueryResult<T>> UpdateEntity = context =>
			{
				T entityToUpdate = context.Set<T>().FirstOrDefault(T => T.Id == entity.Id);

				if (entityToUpdate != null)
				{
					context.Entry(entityToUpdate).State = EntityState.Detached;
					var updatedEntity = context.Update(entity);
					return new CreateQueryResult<T>(updatedEntity.Entity, true);
				}

				return new CreateQueryResult<T>(null, false);
			};

			return _dbContext.ExecuteQuery(UpdateEntity);
		}
	}
}
