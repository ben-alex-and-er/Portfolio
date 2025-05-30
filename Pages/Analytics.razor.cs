using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;


namespace Portfolio.Pages
{
	using Data.Analytics;
	using Portfolio.Providers.Analytics;
	using Services.Analytics.Interfaces;


	/// <summary>
	/// Represents an analytics page object
	/// </summary>
	public partial class Analytics : ComponentBase
	{
		private Dictionary<string, IReadOnlyList<ChartData>> date = [];


		[Inject]
		private IAnalyticsService AnalyticsService { get; set; } = default!;


		/// <inheritdoc/>
		protected override async Task OnInitializedAsync()
		{
			await Update();
		}

		private async Task Update()
		{
			var from = DateTime.UtcNow.AddDays(-7);

			IReadOnlyList<ChartData> registers = await AnalyticsService
				.GetAnalytics(from)
				.Select(r => new ChartData(r.Date.Day.WithOrdinalIndicator(), r.Registers))
				.ToListAsync();

			IReadOnlyList<ChartData> logins = await AnalyticsService
				.GetAnalytics(from)
				.Select(r => new ChartData(r.Date.Day.WithOrdinalIndicator(), r.Logins))
				.ToListAsync();

			date = new Dictionary<string, IReadOnlyList<ChartData>>
			{
				{ "Registers", registers },
				{ "Logins", logins }
			};
		}
	}
}