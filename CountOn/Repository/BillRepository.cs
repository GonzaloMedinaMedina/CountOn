using DBManager.Provider;
using DBManager.Entities;
using DBManager.QueryResults;

namespace CountOn.Repository
{
    public class BillRepository : IBillRepository
    {
        private Repository<Bill> _repository = new Repository<Bill>();
        public BillRepository() { }
        public BillRepository(IDbProvider dbProvider) 
        {
            _repository = new Repository<Bill>(dbProvider);
        }
        
        public IBillDateSummary GetBillsByDate(DateTime date)
        {
            BillDateSummary result = new BillDateSummary();
            if (date.Date <= DateTime.Now)
            {
                ReadQueryResult<Bill> allBillsQuery = _repository.GetAllEntities();
                var bills = allBillsQuery.GetEntities().Where(x => date.Date == x.BillDate.Date).ToList();
                decimal? total = 0;
                bills.ForEach(x => total += x.Price);
                
                result.SetTotal(total);
				result.SetDate(date);

                foreach (var bill in bills) 
                {
					result.AddBill(bill, bill.BillType);
				}
			}

            return result;
        }

		public List<IBillDateSummary> GetBillsByDateRange(DateTime dateFrom, DateTime dateTo)
		{
            List<IBillDateSummary> result = new List<IBillDateSummary>();

            if (dateFrom < dateTo && (dateTo - dateFrom).TotalDays <= 90)
            {
                DateTime currentDate = dateFrom;

				while (currentDate <= dateTo)
                {
					result.Add(GetBillsByDate(currentDate));
					currentDate = currentDate.AddDays(1);
				}
            }
      
            return result;
		}
	}
}
