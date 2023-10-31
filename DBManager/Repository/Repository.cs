using DBManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DBManager.Repository
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		private readonly Context.DbContext _context;
		public Repository()
		{
			_context = new Context.DbContext();
		}

		public EntityEntry<T> AddEntity(T entity)
		{
			try
			{
				EntityEntry<T> result = null;
				_context.Database.EnsureCreated();
				_context.Database.OpenConnection();

				if (entity.GetId() != 0)
				{
					_context.Bills.Add(entity as Bill);
				}
				else
					result = _context.Add(entity);

				_context.SaveChanges();

				return result;
			}catch(Exception ex) 
			{
				return null;
			}finally 
			{
				_context.Database.CloseConnection();
				_context.Dispose();
			}
		}

		public IEnumerable<T> GetAllEntities(T entity)
		{
			return _context.Set<T>().AsEnumerable<T>();
		}
		public T GetEntityById(T entity)
		{
			return _context.Set<T>().Where(e =>  e.GetId() == entity.GetId()).FirstOrDefault();
		}
	}
}
