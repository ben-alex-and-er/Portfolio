using Microsoft.AspNetCore.Components;


namespace Portfolio.Components.Projects
{
	/// <summary>
	/// Represents a project summary object
	/// </summary>
	public partial class ProjectSummary : ComponentBase
	{
		/// <summary>
		/// Title of the project
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Title { get; set; } = "Title";

		/// <summary>
		/// Role in the project
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Role { get; set; } = "Role";

		/// <summary>
		/// Description of the project
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Description { get; set; } = "Description";

		/// <summary>
		/// Year(s) the project was worked on
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Year { get; set; } = "Year";

		/// <summary>
		/// Genre of the project
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Genre { get; set; } = "Genre";

		/// <summary>
		/// Platform of the project
		/// </summary>
		[EditorRequired]
		[Parameter]
		public string Platform { get; set; } = "Platform";

		/// <summary>
		/// URL of the github page for the project
		/// </summary>
		/// <remarks>
		/// If null, does not display github link
		/// </remarks>
		[Parameter]
		public string? GitHubURL { get; set; } = null;
	}
}