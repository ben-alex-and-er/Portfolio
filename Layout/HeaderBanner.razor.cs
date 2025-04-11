using Microsoft.AspNetCore.Components;


namespace Portfolio.Layout
{
	/// <summary>
	/// Represents a Header Banner
	/// </summary>
	public partial class HeaderBanner
	{
		/// <summary>
		/// Determines whether the sign out button should be shown
		/// </summary>
		[Parameter]
		public bool ShowSignOut { get; set; } = true;
	}
}