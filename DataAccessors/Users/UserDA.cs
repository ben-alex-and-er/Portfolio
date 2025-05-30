using Microsoft.EntityFrameworkCore;


namespace Portfolio.DataAccessors.Users
{
	using Configuration.Database;
	using Configuration.Database.Tables.User;
	using DataAccessors.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class UserDA : IUserDA
	{
		private readonly Context context;


		/// <summary>
		/// Constructor for <see cref="UserDA"/>
		/// </summary>
		/// <param name="context"></param>
		public UserDA(Context context)
		{
			this.context = context;
		}


		async Task<bool> ICreate<string>.Create(string email)
		{
			var exists = await context.Users
				.AnyAsync(user => user.Email == email);

			if (exists)
				return false;

			var entry = new User
			{
				Email = email
			};

			context.Users.Add(entry);

			await context.SaveChangesAsync();

			return true;
		}

		IQueryable<string> IRead<string>.Read()
			=> context.Users
				.AsNoTracking()
				.Select(user => user.Email);
	}
}
