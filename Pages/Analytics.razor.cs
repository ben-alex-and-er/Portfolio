using Microsoft.AspNetCore.Components;


namespace Portfolio.Pages
{
	using Data.Analytics;
	using Providers.Analytics;
	using Services.Analytics.Interfaces;


	/// <summary>
	/// Represents an analytics page object
	/// </summary>
	public partial class Analytics : ComponentBase
	{
		[Inject]
		private IAnalyticsService AnalyticsService { get; set; } = default!;


		private Dictionary<string, IReadOnlyList<ChartData>> date = [];

		private DateTime from;
		private Func<DateTime, DateTime> timeIncrements;
		private Func<DateTime, long> bucketDesignate;
		private Func<DateTime, string> dateDisplayer;

		private DateTime firstEvent;


		/// <inheritdoc/>
		protected override async Task OnInitializedAsync()
		{
			firstEvent = await AnalyticsService.GetFirstAnalyticsDate();

			SetSettings(DateTime.UtcNow.AddHours(-23), x => x.AddHours(1), x => x.Hour, x => x.ToString("HH:00"));

			Update();
		}

		private void Update()
		{
			var data = AnalyticsService.GetAnalytics(from, DateTime.UtcNow, timeIncrements, bucketDesignate);

			IReadOnlyList<ChartData> registers = data
				.Select(r => new ChartData(dateDisplayer(r.Date), r.Registers))
				.ToList();

			IReadOnlyList<ChartData> logins = data
				.Select(r => new ChartData(dateDisplayer(r.Date), r.Logins))
				.ToList();

			date = new Dictionary<string, IReadOnlyList<ChartData>>
			{
				{ "Registers", registers },
				{ "Logins", logins }
			};

			StateHasChanged();
		}

		private void FromChanged(ChangeEventArgs e)
		{
			var analyticsDate = Enum.Parse<AnalyticsDateFilter>(e.Value.ToString());

			switch (analyticsDate)
			{
				case AnalyticsDateFilter.LAST_DAY:
					SetSettings(DateTime.UtcNow.AddHours(-23), x => x.AddHours(1), x => x.Hour, x => x.ToString("HH:00"));
					break;

				case AnalyticsDateFilter.LAST_WEEK:
					SetSettings(DateTime.UtcNow.AddDays(-7), x => x.AddDays(1), x => x.Day, x => x.Day.WithOrdinalIndicator());
					break;

				case AnalyticsDateFilter.LAST_MONTH:
					SetSettings(DateTime.UtcNow.AddMonths(-1).AddDays(1), x => x.AddDays(1), x => x.Day, x => x.Day.WithOrdinalIndicator());
					break;

				case AnalyticsDateFilter.LAST_YEAR:
					SetSettings(DateTime.UtcNow.AddYears(-1), x => x.AddMonths(1), x => x.Year * 100 + x.Month, x => x.ToString("MMM yyyy"));
					break;

				case AnalyticsDateFilter.ALL_TIME:
					SetSettings(firstEvent, x => x.AddYears(1), x => x.Year, x => x.Year.ToString());
					break;
			}

			Update();
		}

		private void SetSettings(
			DateTime from,
			Func<DateTime, DateTime> timeIncrements,
			Func<DateTime, long> bucketDesignate,
			Func<DateTime, string> dateDisplayer)
		{
			this.from = from;
			this.timeIncrements = timeIncrements;
			this.bucketDesignate = bucketDesignate;
			this.dateDisplayer = dateDisplayer;
		}
	}
}