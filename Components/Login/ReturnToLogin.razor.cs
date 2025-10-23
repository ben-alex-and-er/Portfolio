using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Login
{
	/// <summary>
	/// Represents a return to login component
	/// </summary>
	public partial class ReturnToLogin : ComponentBase
	{
		[Inject]
		private NavigationManager Navigation { get; set; } = default!;


		private bool alreadyNavigated;


		/// <inheritdoc/>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (!firstRender || alreadyNavigated)
				return;

			alreadyNavigated = true;
			await Task.Yield();
			Navigation.NavigateTo("/login", forceLoad: true);
		}
	}
}