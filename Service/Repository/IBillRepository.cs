
using Service.Services;

namespace Service.Repository
{
    public interface IBillRepository
    {
        IBillDateSummary GetBillsByDate(DateTime date);
        List<IBillDateSummary> GetBillsByDateRange(DateTime dateFrom, DateTime dateTo);
    }
}
