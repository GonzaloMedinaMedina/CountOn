using DBManager.Entities;

namespace CountOn.Repository
{
    public interface IBillDateSummary
    {
        DateTime GetDate();
        decimal? GetTotal();
		IDictionary<BillType, List<Bill>> GetBillTypes();
        IList<Bill> GetAllBills();
        List<decimal?> GetBillTypeValues();
	}
}
