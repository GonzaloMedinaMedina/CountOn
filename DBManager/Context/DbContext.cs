using DBManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBManager.Context
{
	public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
	{
		public DbSet<Bill> Bills { get; set; }

		public DbContext() 
		{
		}

		public DbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bill>().HasKey(x => x.Id);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{ 
			options.UseSqlite($"Data Source={DBConstants.DatabasePath}");
		}
	}
}
