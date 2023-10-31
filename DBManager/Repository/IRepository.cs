using DBManager.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DBManager.Repository
{
	internal interface IRepository<T> where T : Entity
	{
		EntityEntry<T> AddEntity(T entity);
		IEnumerable<T> GetAllEntities(T entity);
		T GetEntityById(T entity);
	}
}
