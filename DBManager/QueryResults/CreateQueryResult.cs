using DBManager.Entities;

namespace DBManager.QueryResults
{
	public class CreateQueryResult : QueryResult
	{
		private readonly Entity createdEntity;

		public CreateQueryResult(Entity createdEntity, bool result) : base(result)
		{
			this.createdEntity = createdEntity;
		}

		public Entity GetEntity() { return  createdEntity; }

	}
}
