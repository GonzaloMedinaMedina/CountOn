using DBManager.Context;

namespace DBManager.Provider
{
	internal class DbProvider : IDbProvider
	{
		public DbProvider() { }
		public IDbContext GetDbContext()
		{
			return new DbContext();
		}
	}
}
