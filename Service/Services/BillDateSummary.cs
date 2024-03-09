
using DBManager.Entities;

namespace Service.Services
{
    public class BillDateSummary : IBillDateSummary
    {
        private DateTime _date;
        private decimal? _total = 0;
        private IDictionary<BillType, decimal?> _billTypeTotals = new Dictionary<BillType, decimal?>();
        private IDictionary<BillType, List<Bill>> _billTypes = new Dictionary<BillType, List<Bill>>();

        public BillDateSummary()
        { }

        public BillDateSummary(DateTime date, decimal? total)
        {
            _date = date;
            _total = total;
        }

        public void SetTotal(decimal? total)
        {
            _total = total;
        }


        public void SetDate(DateTime date)
        {
            _date = date;
        }

        public void AddBill(Bill bill, BillType billType)
        {
            if (_billTypes.TryGetValue(billType, out List<Bill> bills))
            {
                bills.Add(bill);
                _billTypes[billType] = bills;

            }
            else
            {
                _billTypes.Add(billType, new List<Bill>() { bill });
            }

            if (_billTypeTotals.TryGetValue(billType, out decimal? billTypeTotal))
            {
                _billTypeTotals[billType] = billTypeTotal + bill.Price;
            }
            else
            {
                _billTypeTotals.Add(billType, bill.Price);
            }
        }

        public decimal? GetTotal()
        {
			if (UserContext.Instance.BillType != BillType.ALL &&
				_billTypeTotals.TryGetValue(UserContext.Instance.BillType, out var filteredTotal))
			{
				return filteredTotal;
			}

			return _total;
        }

        public DateTime GetDate()
        {
            return _date;
        }

        public IDictionary<BillType, List<Bill>> GetBillTypes()
        {
            return _billTypes;
        }

        public IList<Bill> GetAllBills()
        {
            List<Bill> bills = new List<Bill>();

            if (UserContext.Instance.BillType != BillType.ALL &&
                _billTypes.TryGetValue(UserContext.Instance.BillType, out var filteredBills))
            {
                return filteredBills;
            }

            foreach (var billType in _billTypes)
            {
                bills.AddRange(billType.Value);
            }

            return bills;
        }

        public IDictionary<BillType, decimal?> GetTotalValuesByBillType()
        {
            return _billTypeTotals;
        }
    }
}
