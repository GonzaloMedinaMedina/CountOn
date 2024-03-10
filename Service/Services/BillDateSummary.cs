
using DBManager.Entities;
using Service.Resolvers;

namespace Service.Services
{
    public class BillDateSummary : IBillDateSummary
    {
        private DateTime _date;
        private decimal? _total = 0;
        private IDictionary<BillType, decimal?> _billTypeTotals = new Dictionary<BillType, decimal?>();
        private IDictionary<BillType, List<Bill>> _billTypes = new Dictionary<BillType, List<Bill>>();
        private readonly IBillResolver billResolver;

        public decimal? Total
        {
            get
            {
                return _total;
            }
            set 
            {
                _total = value;
            }
        }

        public BillDateSummary(IBillResolver billResolver)
        {
            this.billResolver = billResolver;
        }

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
            return this.billResolver.GetTotal(this);
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
            return this.billResolver.GetAllBills(this);
        }

        public IDictionary<BillType, decimal?> GetTotalValuesByBillType()
        {
            return _billTypeTotals;
        }

        public bool GetTypeTotal(BillType billType, out decimal? typeTotal)
        {
			return _billTypeTotals.TryGetValue(UserContext.Instance.BillType, out typeTotal);
        }
	}
}
