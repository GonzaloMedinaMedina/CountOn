using DBManager.Entities;
using DBManager.Provider;
using Moq;
using DBManager.Repository;
using DBManager.QueryResults;

namespace TestDBManager
{
	public class TestRepository
	{
		IRepository<Bill> _billRepository;
		public TestRepository()
		{
            _billRepository = new Repository<Bill>(DbContextMock.GetMockProvider().Object);
        }

        [Fact]
		public void GetAllEntitiesMustReturnTwoBills()
		{
            AddBills();
            ReadQueryResult billsReadQueryResult = new ReadQueryResult();

			billsReadQueryResult = _billRepository.GetAllEntities();

			Assert.Equal(4, billsReadQueryResult.GetEntities().Count());
			Assert.True(billsReadQueryResult.GetResult());
		}

		[Fact]
		public void GetAllEntitiesWithEmptyDBMustReturnNoneBill()
		{
            ReadQueryResult billsReadQueryResult = new ReadQueryResult();

            billsReadQueryResult = _billRepository.GetAllEntities();

            Assert.Empty(billsReadQueryResult.GetEntities());
            Assert.False(billsReadQueryResult.GetResult());
        }

        [Fact]
        public void GetEntityByIdWithNonExistingIdMust()
        {
            AddBills();
            ReadQueryResult billReadQueryResult = new ReadQueryResult();

            billReadQueryResult = _billRepository.GetEntityById(-1);

            Assert.True(billReadQueryResult.GetEntities().Count() == 0);
            Assert.False(billReadQueryResult.GetResult());
        }

        [Fact]
        public void GetEntityByIdWithValidIdMustReturnSpecificEntity()
        {
            AddBills();
            ReadQueryResult billReadQueryResult = new ReadQueryResult();

            billReadQueryResult = _billRepository.GetEntityById(1);

            Assert.True(billReadQueryResult.GetEntities().First().Id == 1);
        }

        private void AddBills()
		{
            _billRepository.AddEntity(new Bill("platanos", 3, BillType.FOOD, DateTime.Now));
            _billRepository.AddEntity(new Bill("caracoles", 5, BillType.FOOD, DateTime.Now));
            _billRepository.AddEntity(new Bill("camisas", 50, BillType.CLOTHES, DateTime.Now.AddDays(-1)));
            _billRepository.AddEntity(new Bill("karts", 35, BillType.HOBBY, DateTime.Now.AddDays(-1)));
        }

    }
}