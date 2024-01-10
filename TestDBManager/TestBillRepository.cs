
using Service.Repository;

namespace TestDBManager
{
    public class TestBillRepository
    {
        private IBillRepository _billRepository = new BillRepository();
        public TestBillRepository()
        {
            _billRepository = new BillRepository(DbContextMock.GetMockProvider().Object);
        }

        [Fact]
        public void GetBillsByDateMustReteurnAllBillsWithSpecificDate()
        {
            var bills = _billRepository.GetBillsByDate(DateTime.Now);

            Assert.True(bills.GetAllBills().Count() == 1);
        }

        [Fact]
        public void GetBillsByDateMustReturnEmptyListForNonExistingDate() 
        {
			var bills = _billRepository.GetBillsByDate(DateTime.Now.AddDays(-5));
            
			Assert.True(bills.GetAllBills().Count() == 0);
		}

        [Fact]
        public void GetBillsByDateMustReturnEmptyListForFutureDates()
        {
			var bills = _billRepository.GetBillsByDate(DateTime.Now.AddDays(1));

			Assert.True(bills.GetAllBills().Count() == 0);
		}

        public void GetBillsByRangeDateMustReturnDictionaryWithEntriesForEachDateInRange()
        {
            var bills = _billRepository.GetBillsByDateRange(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));
            Assert.True(bills.Count == 5);
        }
    }
}
