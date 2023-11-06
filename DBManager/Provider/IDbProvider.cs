using DBManager.Context;
namespace DBManager.Provider
{
	public interface IDbProvider
	{
		IDbContext GetDbContext();
	}
}
