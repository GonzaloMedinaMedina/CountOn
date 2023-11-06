using DBManager.Entities;
using DBManager.QueryResults;
using Microsoft.EntityFrameworkCore;

namespace DBManager.Context
{
	public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
	{
		public DbSet<Bill> Bills { get; set; }
		public DbContext()
		{
			Database.EnsureCreated();
			Database.OpenConnection();
		}

		public DbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			Database.EnsureCreated();
			Database.OpenConnection();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bill>().HasKey(x => x.Id);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlite($"Data Source={DBConstants.DatabasePath}");
			}
		}

		public QueryResult ExecuteQuery(Func<DbContext, QueryResult> queryFunc)
		{
			try
			{
				QueryResult queryResult = queryFunc.Invoke(this);
				SaveChanges();
				return queryResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}

			return new QueryResult();
		}
	}
}
