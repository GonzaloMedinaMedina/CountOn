using DBManager.Entities;

namespace DBManager.Repository
{
    public interface IBillRepository
    {
        IList<Bill> GetBillsByDate(DateTime date);
        IDictionary<DateTime, IList<Bill>> GetBillsByDateRange(DateTime dateFrom, DateTime dateTo);
    }
}
