
using DBManager.Entities;

namespace Service.Services
{
    /// <summary>
    /// Represent all the bills in an specific date.
    /// Have information of all the bills, its type and the total amount for the date.
    /// </summary>
    public interface IBillDateSummary
    {
        /// <summary>
        /// Get the date of the BillDateSummary
        /// </summary>
        /// <returns>A DateTime instance with the date.</returns>
        DateTime GetDate();
        /// <summary>
        /// Get the total amount of all the bills.
        /// </summary>
        /// <returns>The add of all bills amount.</returns>
        decimal? GetTotal();
        /// <summary>
        /// Get all bills by its type.
        /// </summary>
        /// <returns>Return a dictionary where the key is the type and the value is all the bills of this type.</returns>
		IDictionary<BillType, List<Bill>> GetBillTypes();
        /// <summary>
        /// Get all the bills in this date.
        /// </summary>
        /// <returns>A List with all the bills.</returns>
        IList<Bill> GetAllBills();
        /// <summary>
        /// Get the total amount of each bill type.
        /// </summary>
        /// <returns>A dictionary with all the totals by billType as key.</returns>
        IDictionary<BillType, decimal?> GetTotalValuesByBillType();
    }
}
