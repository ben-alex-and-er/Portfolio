namespace Portfolio.Pages
{
	/// <summary>
	/// Represents a Ranked API object
	/// </summary>
	public partial class RankedAPI
	{
		private const string sqlCode = "CREATE TABLE IF NOT EXISTS ranked.user_elo (\r\n\tid INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,\r\n    user_id INT UNSIGNED NOT NULL UNIQUE,\r\n    elo INT UNSIGNED NOT NULL,\r\n    CONSTRAINT fk_user_elo_user FOREIGN KEY (user_id) REFERENCES ranked.user(id)\r\n);\r\n\r\nINSERT INTO application(name, guid) VALUES ('Global', '7b3f9c9f-5c0c-4a54-9e22-04a7a6a6c1f1');\r\nSET @global_application_id = LAST_INSERT_ID();\r\n\r\nINSERT INTO user_game (user_id, game_id)\r\nSELECT id, @global_application_id\r\nFROM user;";

		private const string userServiceCode = "async Task<CreateUserResponse> IUserService.Create(ICreateUserRequest request)\r\n{\r\n\tvar applicationExists = await applicationDA.Read()\r\n\t\t.AnyAsync(app => app.Guid == request.Application);\r\n\r\n\tif (!applicationExists)\r\n\t\treturn new CreateUserResponse(CreateUserStatus.APPLICATION_NOT_FOUND);\r\n\r\n\r\n\tusing var transaction = await transactionCreator.CreateTransactionAsync();\r\n\r\n\r\n\tvar userExists = await userDA.Read()\r\n\t\t.AnyAsync(user => user == request.User);\r\n\r\n\tif (!userExists)\r\n\t{\r\n\t\tvar createUser = await userDA.Create(request.User);\r\n\r\n\t\tif (!createUser)\r\n\t\t\tthrow new InvalidOperationException(\"Failed to create user. This should not happen since user does not exist.\");\r\n\t}\r\n\r\n\r\n\tvar createUserApplication = await userApplicationDA.Create(request);\r\n\r\n\tif (!createUserApplication)\r\n\t\treturn new CreateUserResponse(CreateUserStatus.USER_APPLICATION_ALREADY_EXISTS);\r\n\r\n\r\n\tvar createElo = await userApplicationEloDA.Create(request);\r\n\r\n\tif (!createElo)\r\n\t\treturn new CreateUserResponse(CreateUserStatus.FAILED_TO_CREATE_ELO);\r\n\r\n\tawait transaction.CommitAsync();\r\n\r\n\treturn new CreateUserResponse(CreateUserStatus.SUCCESS);\r\n}";
	}
}