using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Media
{
	/// <summary>
	/// Represents a YouTube Embed object
	/// </summary>
	public partial class YouTubeEmbed
	{
		/// <summary>
		/// Hash for the youtube video to display
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string VideoHash { get; set; }
	}
}