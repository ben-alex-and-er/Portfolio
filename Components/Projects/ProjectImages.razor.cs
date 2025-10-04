using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Projects
{
	/// <summary>
	/// Represents a project images object
	/// </summary>
	public partial class ProjectImages
	{
		/// <summary>
		/// Image sources to be displayed
		/// </summary>
		[EditorRequired]
		[Parameter]
		public IEnumerable<string> Images { get; set; } = [];
	}
}