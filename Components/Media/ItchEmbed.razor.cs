using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Media
{
	/// <summary>
	/// Represents an Itch Embed object
	/// </summary>
	public partial class ItchEmbed
	{
		/// <summary>
		/// Source for the game to be embedded
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string EmbedSource { get; set; }

		/// <summary>
		/// URL of the game
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string ItchUrl { get; set; }
	}
}