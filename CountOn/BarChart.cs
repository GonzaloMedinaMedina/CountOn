using PSC.Blazor.Components.Chartjs.Models.Bar;
using PSC.Blazor.Components.Chartjs.Models.Common;
using Service.Repository;

namespace CountOn
{
    public class BarChart
    {
        private static List<decimal?> GetYValues(IList<IBillDateSummary> billsByDate)
        {
            List<decimal?> bills = new List<decimal?>();
            foreach (var billDateSummary in billsByDate)
            {
                bills.Add(billDateSummary.GetTotal());
            }

            return bills;
        }

        private static List<string> GetXLabels(IList<IBillDateSummary> billsByDate)
        {
            List<string> result = new List<string>();
            foreach (var billDateSummary in billsByDate)
            {
                DateTime date = billDateSummary.GetDate();
                result.Add(date.DayOfWeek.ToString() + "\n" + date.Day.ToString() + "/" + date.Month.ToString());
            }

            return result;
        }
    
        public static BarChartConfig GetBarChartConfig(IList<IBillDateSummary> billsByDate, Func<CallbackGenericContext, ValueTask>? onClickAsync)
        {
            BarChartConfig _config = new BarChartConfig()
            {
                Options = new Options()
                {
                    Plugins = new Plugins()
                    {
                        Legend = new Legend()
                        {
                            Align = Align.Center,
                            Display = false,
                            Position = LegendPosition.Right
                        }
                    },
                    Scales = new Dictionary<string, Axis>()
                    {
                        {
                            Scales.XAxisId, new Axis()
                            {
                                Stacked = true,
                                Ticks = new Ticks()
                                {
                                    MaxRotation = 0,
                                    MinRotation = 0
                                }
                            }
                        },
                        {
                            Scales.YAxisId, new Axis()
                            {
                                Stacked = true
                            }
                        }
                    },
                    OnClickAsync = onClickAsync
                }
            };

            _config.Data.Labels = GetXLabels(billsByDate);
            _config.Data.Datasets.Add(new BarDataset()
            {
                Label = "Value",
                Data = GetYValues(billsByDate),
                BackgroundColor = new List<string>() { "rgb(255, 0, 0)" },
                BorderColor = new List<string>() { "rgb(0, 0, 0)" },
                BorderWidth = 3,
                Fill = true
            });

            return _config;
        }
    }
}
