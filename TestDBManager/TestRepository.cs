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
        public void RemoveEntitydMustReturnTrueWhenEntityIsRemoved()
        {
            QueryResult billRemovedQueryResult = new QueryResult();

			billRemovedQueryResult = _repository.RemoveEntity(_repository.GetAllEntities().GetEntities().First());

            Assert.Equal(3, _repository.GetAllEntities().GetEntities().Count());
            Assert.True(billRemovedQueryResult.GetResult());
		}

		[Fact]
		public void RemoveEntityMustReturnFalseIfEntityIsNotRemoved()
		{
			QueryResult billRemovedQueryResult = new QueryResult();

            var bill = new Bill();

			billRemovedQueryResult = _repository.RemoveEntity(bill);

			Assert.Equal(4, _repository.GetAllEntities().GetEntities().Count());
			Assert.False(billRemovedQueryResult.GetResult());
		}

        [Fact]
        public void AddEntityMustSaveEntityInDB()
        {
			var bill = new Bill("test bill", 50, BillType.HOBBY, DateTime.Today);

			var savedBill = _repository.AddEntity(bill).GetEntity();
			var retrievedBill = _repository.GetEntityById(savedBill.Id).GetEntities().First();
            var allBills = _repository.GetAllEntities().GetEntities();

            Assert.Equal(savedBill.Id, retrievedBill.Id);
            Assert.Equal(5, allBills.Count());
		}

		[Fact]
		public void AddEntityMustAvoidSaveEntityAndReturnFalseAsResultIfEntityAlreadyExistsInDB()
		{
			var bill = new Bill("test bill", 50, BillType.HOBBY, DateTime.Today);

			var savedBill = _repository.AddEntity(bill).GetEntity();
            var reSavedBill = _repository.AddEntity(savedBill);
			var allBills = _repository.GetAllEntities().GetEntities();

			Assert.Equal(savedBill.Id, reSavedBill.GetEntity().Id);
            Assert.False(reSavedBill.GetResult());
			Assert.Equal(5, allBills.Count());
		}

		[Fact]
        public void UpdateEntityMustUpdateEntityInDBWithNewChanges()
        {
            var bill = new Bill("test bill", 50, BillType.HOBBY, DateTime.Today);

			var savedBill = _repository.AddEntity(bill).GetEntity();
            savedBill.Price = 100;
			savedBill.Name = "updated bill";
			savedBill.BillType = BillType.FOOD;

			_repository.UpdateEntity(savedBill);
			var billUpdatedQueryResult =_repository.GetEntityById(savedBill.Id).GetEntities().First();

			Assert.Equal(100, billUpdatedQueryResult.Price);
			Assert.Equal("updated bill", billUpdatedQueryResult.Name);
			Assert.Equal(BillType.FOOD, billUpdatedQueryResult.BillType);
		}
	}
}