using DBManager.QueryResults;

namespace DBManager.Context
{
	public interface IDbContext
	{
		T ExecuteQuery<T>(Func<DbContext, T> queryFunc) where T : QueryResult;
	}
}
