using Service.Repository;
using DBManager.Entities;
using PSC.Blazor.Components.Chartjs.Models.Common;
using PSC.Blazor.Components.Chartjs.Models.Pie;

namespace CountOn.Charts
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

            IDictionary<BillType, List<Bill>> billTypes = billDateSummary.GetBillTypes();
            IList<BillType> billTypesList = billTypes.Keys.ToList();
            config.Data.Labels = billTypesList.ToList().ConvertAll(x => x.ToString());
            config.Data.Datasets.Add(new PieDataset()
            {
                Label = "Pie chart Label",
                Data = billDateSummary.GetBillTypeValues(),
                BackgroundColor = new List<string>() { "rgb(255, 0, 0)", "rgb(0, 255, 0)", "rgb(0, 0, 255)" },
                BorderWidth = 1
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
