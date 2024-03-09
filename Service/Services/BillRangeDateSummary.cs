
using DBManager.Entities;

namespace Service.Services
{
    public class BillRangeDateSummary
    {
        private IList<IBillDateSummary> billsByDate = new List<IBillDateSummary>();
        private decimal? _totalAmount { get; set; }
        private IDictionary<BillType, decimal?> _totalAmountByBillType { get; set; } = new Dictionary<BillType, decimal?>();

        public BillRangeDateSummary() { }

        public decimal? GetTotal()
        {
            return _totalAmount;
        }

        public IDictionary<BillType, decimal?> TotalPerType()
        {
            return _totalAmountByBillType;
        }

        public IList<IBillDateSummary> GetBillsByDate()
        {
            return billsByDate;
        }

        public void SetBills(IList<IBillDateSummary> bills)
        {
            billsByDate = bills;

            _totalAmount = 0;
            _totalAmountByBillType.Clear();

            foreach (var billDate in bills)
            {
                _totalAmount += billDate.GetTotal();
                AddBillTypeAmount(billDate.GetTotalValuesByBillType());
            }
        }

        private void AddBillTypeAmount(IDictionary<BillType, decimal?> dictionary)
        {
            foreach (var billTypeEntry in dictionary)
            {
                if (_totalAmountByBillType.TryGetValue(billTypeEntry.Key, out decimal? value))
                {
                    _totalAmountByBillType[billTypeEntry.Key] = value + billTypeEntry.Value;
                }
                else
                {
                    _totalAmountByBillType.Add(billTypeEntry.Key, billTypeEntry.Value);
                }
            }
        }

    }
}
