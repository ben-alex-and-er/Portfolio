using Microsoft.EntityFrameworkCore;


namespace Portfolio.Configuration.Database
{
	using Tables.Users;


	/// <summary>
	/// Database context for portfolio
	/// </summary>
	public class Context : DbContext
	{
		/// <summary>
		/// DbSet for <see cref="User"/>
		/// </summary>
		public DbSet<User> Users => Set<User>();


		/// <inheritdoc/>
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source=portfolio.db");


		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasKey(entry => entry.Id);

			modelBuilder.Entity<User>()
				.HasIndex(entry => entry.Email)
				.IsUnique();
		}
	}
}
