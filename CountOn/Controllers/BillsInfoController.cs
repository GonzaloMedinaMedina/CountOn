using CountOn.Charts;
using CountOn.Models;
using CountOn.Shared;
using PSC.Blazor.Components.Chartjs.Models.Common;
using Service.Repository;
using Service.Services;

namespace CountOn.Controllers
{
    public static class BillsInfoController
	{
		public static Bills BillsRef;
		public static BillSummary BillSummaryRef;
		public static Filters FiltersRef;
		public static ChartComponent ChartComponentRef;

		public static void SetBillTypeFilter(DBManager.Entities.BillType billType)
		{
			UserContext.Instance.BillType = billType;

			BillsRef.UpdateState();
			BillSummaryRef.UpdateState();
			ChartComponentRef.UpdateState();
		}

		public static IList<IBillDateSummary> GetBillsByDate()
		{
			return BillsInfoModel.billRangeDateSummary.GetBillsByDate();
		}

		public static void SetBillsByDateRange(IBillRepository repository, DateTime fromDate, DateTime toDate)
		{
			var billsByDate = repository.GetBillsByDateRange(fromDate, toDate);
			BillsInfoModel.billRangeDateSummary.SetBills(billsByDate);
		}

		public static ValueTask onClickAsyncBarChart(CallbackGenericContext value)
		{
			var billsByDate = GetBillsByDate();
			BillsInfoModel.selectedBill = billsByDate[value.DataIndex];

			SetChartConfig();
			ChartComponentRef.UpdateState();
			return ValueTask.CompletedTask;
		}

		/// <summary>
		/// Method to set and get a chart config based on user selection.
		/// </summary>
		/// <returns></returns>
		public static void SetChartConfig()
		{
			if (BillsInfoModel.selectedBill != null)
			{
				BillsInfoModel.config = PieChart.GetPieChartConfig(BillsInfoModel.selectedBill);
			}
			else
			{
				BillsInfoModel.config = BarChart.GetBarChartConfig(BillsInfoModel.billRangeDateSummary.GetBillsByDate(), onClickAsyncBarChart);
			}
		}
	}
}
