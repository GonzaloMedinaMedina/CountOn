using DBManager.Entities;
using Service.Services;

namespace Service.Resolvers
{
	public interface IBillResolver
	{
		public decimal? GetTotal(BillDateSummary billDateSummary);
		public IList<Bill> GetAllBills(BillDateSummary billDateSummary);
	}
}
