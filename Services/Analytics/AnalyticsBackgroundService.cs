using TransactionToolkit.Interfaces;


namespace Portfolio.Services.Analytics
{
	using DataAccessors.Analytics.Interfaces;
	using Portfolio.Data.Analytics;


	/// <summary>
	/// Background service for filling psuedo analytics
	/// </summary>
	public class AnalyticsBackgroundService : BackgroundService, IDisposable
	{
		private readonly IServiceScope serviceScope;

		private readonly ILogger logger;

		private readonly ITransactionCreator transactionCreator;

		private readonly IAnalyticsDA analyticsDA;

		private readonly Random random = new();

		private int shortBurstCount = 0;


		/// <summary>
		/// Constructor for <see cref="AnalyticsBackgroundService"/>
		/// </summary>
		/// <param name="serviceScopeFactory"></param>
		public AnalyticsBackgroundService(IServiceScopeFactory serviceScopeFactory)
		{
			serviceScope = serviceScopeFactory.CreateScope();

			logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<AnalyticsBackgroundService>>();

			transactionCreator = serviceScope.ServiceProvider.GetRequiredService<ITransactionCreator>();

			analyticsDA = serviceScope.ServiceProvider.GetRequiredService<IAnalyticsDA>();
		}

		void IDisposable.Dispose()
		{
			serviceScope.Dispose();
		}


		/// <inheritdoc/>
		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await RecordAnalytics(cancellationToken);

				var delay = TimeSpan.FromMinutes(MinutesDelay());
				await Task.Delay(delay, cancellationToken);
			}
		}


		private async Task RecordAnalytics(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					if (!IsValidTime())
						return;

					using var transaction = await transactionCreator.CreateTransactionAsync();

					var eventType = GetEventType();

					var create = await CreateEvent(eventType);

					if (!create)
					{
						logger.LogError("Failed to create Analytics event: {Type}", eventType);
						return;
					}

					if (eventType == AnalyticsEventTypes.REGISTER)
					{
						create = await CreateEvent(eventType);

						if (!create)
						{
							logger.LogError("Failed to create Analytics event: {Type}", eventType);
							return;
						}
					}

					await transaction.CommitAsync(cancellationToken);

					return;
				}
				catch (Exception ex)
				{
					// Log error and retry
					logger.LogError(ex, "Background service task failed");
					await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
				}
			}
		}

		private bool IsValidTime()
		{
			var now = DateTime.UtcNow;

			if (now.Hour < 7 || now.Hour > 20)
			{
				// 5% chance to do allow an off peak event
				if (random.NextDouble() < 0.05)
					return true;

				return false;
			}

			return true;
		}

		private Guid GetEventType() => random.Next(2) switch
		{
			0 => AnalyticsEventTypes.REGISTER,
			_ => AnalyticsEventTypes.LOGIN,
		};

		private Task<bool> CreateEvent(Guid eventType)
		{
			var dto = new AnalyticsDTO(eventType);

			return analyticsDA.Create(dto);
		}

		private double MinutesDelay()
		{
			if (shortBurstCount > 0)
			{
				shortBurstCount--;
				return ApplyJitter(random.Next(2, 15));
			}

			// 10% chance to do a short burst of multiple events
			if (random.NextDouble() < 0.1)
			{
				shortBurstCount = random.Next(1, 3);
				return ApplyJitter(random.Next(2, 10));
			}

			return ApplyJitter(random.Next(30, 300));
		}

		private double ApplyJitter(double minutes)
			=> minutes * (1 + (random.NextDouble() - 0.5) * 0.3);
	}
}
