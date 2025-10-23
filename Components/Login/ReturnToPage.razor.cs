using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Login
{
	/// <summary>
	/// Represents a return to page component
	/// </summary>
	public partial class ReturnToPage : ComponentBase
	{
		[Inject]
		private NavigationManager Navigation { get; set; } = default!;


		/// <summary>
		/// Page to return to
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Page { get; set; }


		private bool alreadyNavigated;


		/// <inheritdoc/>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (!firstRender || alreadyNavigated)
				return;

			alreadyNavigated = true;
			await Task.Yield();
			Navigation.NavigateTo(Page, forceLoad: true);
		}
	}
}