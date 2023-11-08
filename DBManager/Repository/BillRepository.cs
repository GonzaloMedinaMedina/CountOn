using DBManager.Provider;
using DBManager.Entities;
using DBManager.QueryResults;

namespace DBManager.Repository
{
    public class BillRepository : IBillRepository
    {
        private Repository<Bill> _repository = new Repository<Bill>();
        public BillRepository() { }
        public BillRepository(IDbProvider dbProvider) 
        {
            _repository = new Repository<Bill>(dbProvider);
        }
        
        public IList<Bill> GetBillsByDate(DateTime date)
        {
            if (date.Date > DateTime.Now)
            {
                return new List<Bill>();
            }

            ReadQueryResult<Bill> allBillsQuery = _repository.GetAllEntities();
            return allBillsQuery.GetEntities().Where(x => date.Date == x.BillDate.Date).ToList();
        }

		public IDictionary<DateTime, IList<Bill>> GetBillsByDateRange(DateTime dateFrom, DateTime dateTo)
		{
            IDictionary<DateTime, IList<Bill>> result = new Dictionary<DateTime, IList<Bill>>();

            if (dateFrom < dateTo && (dateTo - dateFrom).TotalDays <= 90)
            {
                DateTime currentDate = dateFrom;

				while (currentDate <= dateTo)
                {
                    result.Add(currentDate, GetBillsByDate(currentDate));
					currentDate = currentDate.AddDays(1);
				}
            }
      
            return result;
		}
	}
}
