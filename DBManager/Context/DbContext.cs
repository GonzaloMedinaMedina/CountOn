using DBManager.Entities;
using DBManager.QueryResults;
using Microsoft.EntityFrameworkCore;

namespace DBManager.Context
{
	public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
	{
		private static DbContext instance;
		public DbSet<Bill> Bills { get; set; }

		public static DbContext Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DbContext();
				}

				return instance;
			}
		}

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

		public T ExecuteQuery<T>(Func<DbContext, T> queryFunc) where T : QueryResult
		{
			T queryResult = (T) Activator.CreateInstance(typeof(T));
			try
			{
				queryResult = queryFunc.Invoke(this);
				SaveChanges();
				return queryResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return queryResult;
		}
	}
}
