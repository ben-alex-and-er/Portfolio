namespace Portfolio.Data.Analytics
{
	/// <summary>
	/// Data object for use in radzen charts
	/// </summary>
	/// <param name="X">Value of X</param>
	/// <param name="Y">Value of Y</param>
	public record ChartData(string X, double Y);
}
