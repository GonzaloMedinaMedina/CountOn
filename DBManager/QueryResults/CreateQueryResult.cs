using DBManager.Entities;

namespace DBManager.QueryResults
{
	public class CreateQueryResult<T> : QueryResult where T : Entity
	{
		private readonly T createdEntity = null;

		public CreateQueryResult() : base()
		{}

		public CreateQueryResult(T createdEntity, bool result) : base(result)
		{
			this.createdEntity = createdEntity;
		}

		public T GetEntity() { return  createdEntity; }

	}
}
