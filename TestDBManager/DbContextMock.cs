﻿using DBManager.Provider;
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
			var context = DbContextMock.GetMockContext();
			mockProvider.Setup(x => x.GetDbContext()).Returns(context);

			return mockProvider;
		}
	}
}