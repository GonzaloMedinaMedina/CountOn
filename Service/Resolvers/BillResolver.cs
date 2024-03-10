using DBManager.Entities;
using Service.Services;

namespace Service.Resolvers
{
	public class BillResolver
	{
		public BillResolver() 
		{
		}

		public decimal? GetTotal(BillDateSummary billDateSummary)
		{
			if (UserContext.Instance.BillType != BillType.ALL &&
				billDateSummary.GetTypeTotal(UserContext.Instance.BillType, out var filteredTotal))
			{
				return filteredTotal;
			}

			return billDateSummary.Total;
		}

		public IList<Bill> GetAllBills(BillDateSummary billDateSummary)
		{
			List<Bill> bills = new List<Bill>();
			var billTypes = billDateSummary.GetBillTypes();

			if (UserContext.Instance.BillType != BillType.ALL &&
				billTypes.TryGetValue(UserContext.Instance.BillType, out var filteredBills))
			{
				return filteredBills;
			}

			foreach (var billType in billTypes)
			{
				bills.AddRange(billType.Value);
			}

			return bills;
		}

	}
}
