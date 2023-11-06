using DBManager.QueryResults;

namespace DBManager.Context
{
	public interface IDbContext
	{
		QueryResult ExecuteQuery(Func<DbContext, QueryResult> queryFunc);
	}
}
