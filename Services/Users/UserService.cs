using Microsoft.EntityFrameworkCore;


namespace Portfolio.Services.Users
{
	using DataAccessors.Users.Interfaces;
	using Interfaces;


	/// <inheritdoc/>
	public class UserService : IUserService
	{
		private readonly IUserDA userDA;


		/// <summary>
		/// Constructor for <see cref="UserService"/>
		/// </summary>
		/// <param name="userDA"></param>
		public UserService(IUserDA userDA)
		{
			this.userDA = userDA;
		}


		async Task<bool> IUserService.TryAddUser(string email)
		{
			var exists = await userDA.Read()
				.AnyAsync(user => user == email);

			if (exists)
				return false;

			var createUser = await userDA.Create(email);

			if (!createUser)
				return false;

			// Could add more entries

			return true;
		}
	}
}
