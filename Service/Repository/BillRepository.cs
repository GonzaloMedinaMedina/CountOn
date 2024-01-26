using DBManager.QueryResults;
using DBManager.Provider;
using DBManager.Entities;

namespace Service.Repository
{
    public class BillRepository : IBillRepository
    {
        private IRepository<Bill> _repository;

        public BillRepository() { }
        public BillRepository(IRepository<Bill> repository) 
        {
            _repository = repository;
		}

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
