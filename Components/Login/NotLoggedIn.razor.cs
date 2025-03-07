using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Login
{
	public partial class NotLoggedIn : ComponentBase
	{
		/// <summary>
		/// Custom message
		/// </summary>
		[Parameter]
		public string? Message { get; set; } = null;


		/// <inheritdoc/>
		protected override void OnInitialized()
		{
			Message ??= "You are not logged in.";
		}
	}
}