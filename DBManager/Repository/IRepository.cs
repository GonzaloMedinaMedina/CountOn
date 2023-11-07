﻿using DBManager.Entities;
using DBManager.QueryResults;

namespace DBManager.Repository
{
	public interface IRepository<T> where T : Entity
	{
		CreateQueryResult AddEntity(T entity);
		ReadQueryResult GetAllEntities();
        ReadQueryResult GetEntityById(int id);
		
	}
}
