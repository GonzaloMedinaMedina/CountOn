using CountOn.Repository;
using DBManager.Entities;
using PSC.Blazor.Components.Chartjs.Models.Common;
using PSC.Blazor.Components.Chartjs.Models.Pie;

namespace CountOn
{
	public class PieChart
	{
		public static PieChartConfig GetPieChartConfig(IBillDateSummary billDateSummary)
		{
			PieChartConfig config = new PieChartConfig()
			{
				Options = new PieOptions()
				{
					Responsive = true,
					Plugins = new Plugins()
					{
						Legend = new Legend()
						{
							Align = Align.Center,
							Display = false,
							Position = LegendPosition.Bottom
						}
					}
				}
			};

			var billTypes = billDateSummary.GetBillTypes().ToList();
			config.Data.Labels = billTypes.ConvertAll(x => x.ToString());
			config.Data.Datasets.Add(new PieDataset()
			{
				Label = "Pie chart Label",
				Data = billDateSummary.GetBillTypeValues(),
				BackgroundColor = new List<string>() { "rgb(255, 0, 0)", "rgb(0, 255, 0)", "rgb(0, 0, 255)" },
				BorderWidth = 3
			});

			return config;
		}

		private List<string> GetBillTypeColors(List<BillType> billTypes)
		{
			var colors = new List<string>();
			return colors;
		}
	}
}
