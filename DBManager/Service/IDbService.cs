﻿using DBManager.Entities;
using DBManager.QueryResults;

namespace DBManager.Service
{
    public interface IDbService<T> where T : Entity
	{
		CreateQueryResult AddEntity(T entity);
		ReadQueryResult GetAllEntities();
		//T GetEntityById(int id);
	}
}