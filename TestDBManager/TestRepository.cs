using DBManager.Entities;
using DBManager.QueryResults;
using Service.Repository;

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
            ReadQueryResult<Bill> billsReadQueryResult = new ReadQueryResult<Bill>();

			billsReadQueryResult = _billRepository.GetAllEntities();

			Assert.Equal(4, billsReadQueryResult.GetEntities().Count());
			Assert.True(billsReadQueryResult.GetResult());
		} 

        [Fact]
        public void GetEntityByIdWithNonExistingIdMust()
        {
            ReadQueryResult<Bill> billReadQueryResult = new ReadQueryResult<Bill>();

            billReadQueryResult = _billRepository.GetEntityById(-1);

            Assert.True(billReadQueryResult.GetEntities().Count() == 0);
            Assert.False(billReadQueryResult.GetResult());
        }

        [Fact]
        public void GetEntityByIdWithValidIdMustReturnSpecificEntity()
        {
            ReadQueryResult<Bill> billReadQueryResult = new ReadQueryResult<Bill>();

            billReadQueryResult = _billRepository.GetEntityById(1);

            Assert.True(billReadQueryResult.GetEntities().First().Id == 1);
        }
    }
}