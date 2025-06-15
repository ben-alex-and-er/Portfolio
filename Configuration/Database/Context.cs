using Microsoft.EntityFrameworkCore;


namespace Portfolio.Configuration.Database
{
	using Tables.Analytics;
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

		/// <summary>
		/// DbSet for <see cref="AnalyticsEvent"/>
		/// </summary>
		public DbSet<AnalyticsEvent> AnalyticsEvents => Set<AnalyticsEvent>();

		/// <summary>
		/// DbSet for <see cref="Analytics"/>
		/// </summary>
		public DbSet<Analytics> Analytics => Set<Analytics>();


		/// <inheritdoc/>
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source=portfolio.db");


		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// User
			modelBuilder.Entity<User>()
				.HasKey(entry => entry.Id);

			modelBuilder.Entity<User>()
				.HasIndex(entry => entry.Email)
				.IsUnique();


			// Analytics
			modelBuilder.Entity<AnalyticsEvent>()
				.HasKey(entry => entry.Id);

			modelBuilder.Entity<AnalyticsEvent>()
				.HasIndex(entry => entry.Name)
				.IsUnique();

			modelBuilder.Entity<AnalyticsEvent>()
				.HasIndex(entry => entry.Guid)
				.IsUnique();

			modelBuilder.Entity<Analytics>()
				.HasKey(entry => entry.Id);
		}
	}
}
