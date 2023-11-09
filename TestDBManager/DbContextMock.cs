using DBManager.Provider;
using DBManager.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestDBManager
{
	internal static class DbContextMock
	{
		public static DBManager.Context.DbContext GetMockContext()
		{
			// Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
			// at the end of the test (see Dispose below).
			var _connection = new SqliteConnection("Data Source=:memory:");
			_connection.Open();

			// These options will be used by the context instances in this test suite, including the connection opened above.
			var _contextOptions = new DbContextOptionsBuilder<DBManager.Context.DbContext>()
				.UseSqlite(_connection)
				.Options;

			return new DBManager.Context.DbContext(_contextOptions);
		}

		public static Mock<IDbProvider> GetMockProvider()
		{
			var mockProvider = new Mock<IDbProvider>();
			var context = GetMockContext();
			AddDefaultBills(context);

			mockProvider.Setup(x => x.GetDbContext()).Returns(context);

			return mockProvider;
		}

		private static void AddDefaultBills(DBManager.Context.DbContext context)
		{
			context.Database.EnsureCreated();

			Bill entity = new Bill("platanos", 3, BillType.FOOD, DateTime.Now.AddDays(-2));
			entity.SetId(1);
            context.Bills.Add(entity);

            entity = new Bill("caracoles", 5, BillType.FOOD, DateTime.Now.AddDays(-1));
            entity.SetId(2);
            context.Bills.Add(entity);

            entity = new Bill("camisas", 50, BillType.CLOTHES, DateTime.Now);
            entity.SetId(3);
            context.Bills.Add(entity);

            entity = new Bill("karts", 35, BillType.HOBBY, DateTime.Now.AddDays(1));
            entity.SetId(4);
            context.Bills.Add(entity);

			context.SaveChanges();
        }
    }
}
