using CountOn.Charts;
using PSC.Blazor.Components.Chartjs.Interfaces;
using PSC.Blazor.Components.Chartjs.Models.Common;
using Service.Services;

namespace CountOn.Models
{
    /// <summary>
    /// Class to store bill info to be shown in pages and components. 
    /// </summary>
    public static class BillsInfoModel
	{
		#region Properties
		public static BillRangeDateSummary billRangeDateSummary = new BillRangeDateSummary();
		/// <summary>
		/// The date selected.
		/// </summary>
		public static string selectedDateName;
		/// <summary>
		/// The config object of the chart
		/// Values can be PieChart or BarChart
		/// </summary>
		public static IChartConfig config;
		/// <summary>
		/// The selected bill by the user
		/// </summary>
		public static IBillDateSummary selectedBill;
		#endregion Properties
	}
}
