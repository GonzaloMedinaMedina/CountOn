using DBManager.Entities;
using DBManager.QueryResults;
using Service.Repository;

namespace TestDBManager
{
	public class TestRepository
	{
		IRepository<Bill> _repository;

		public TestRepository()
		{
            _repository = new Repository<Bill>(DbContextMock.GetMockProvider().Object);
        }

        [Fact]
		public void GetAllEntitiesMustReturnFourBills()
		{
            ReadQueryResult<Bill> billsReadQueryResult = new ReadQueryResult<Bill>();

			billsReadQueryResult = _repository.GetAllEntities();

			Assert.Equal(4, billsReadQueryResult.GetEntities().Count());
			Assert.True(billsReadQueryResult.GetResult());
		} 

        [Fact]
        public void GetEntityByIdWithNonExistingIdMust()
        {
            ReadQueryResult<Bill> billReadQueryResult = new ReadQueryResult<Bill>();

            billReadQueryResult = _repository.GetEntityById(-1);

            Assert.True(billReadQueryResult.GetEntities().Count() == 0);
            Assert.False(billReadQueryResult.GetResult());
        }

        [Fact]
        public void GetEntityByIdWithValidIdMustReturnSpecificEntity()
        {
            ReadQueryResult<Bill> billReadQueryResult = new ReadQueryResult<Bill>();

            billReadQueryResult = _repository.GetEntityById(1);

            Assert.True(billReadQueryResult.GetEntities().First().Id == 1);
        }

        [Fact]
        public void RemoveEntityByIdMustReturnTrueWhenEntityIsRemoved()
        {
            QueryResult billRemovedQueryResult = new QueryResult();

			billRemovedQueryResult = _repository.RemoveEntity(_repository.GetAllEntities().GetEntities().First());

            Assert.Equal(3, _repository.GetAllEntities().GetEntities().Count());
            Assert.True(billRemovedQueryResult.GetResult());
		}

		[Fact]
		public void RemoveEntityByIdMustReturnFalseIfEntityIsNotRemoved()
		{
			QueryResult billRemovedQueryResult = new QueryResult();

            var bill = new Bill();

			billRemovedQueryResult = _repository.RemoveEntity(bill);

			Assert.Equal(4, _repository.GetAllEntities().GetEntities().Count());
			Assert.False(billRemovedQueryResult.GetResult());
		}
	}
}