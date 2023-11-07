using DBManager.Entities;

namespace DBManager.QueryResults
{
	public class ReadQueryResult : QueryResult
	{
		private readonly IEnumerable<Entity> readEntities;

		public ReadQueryResult() : base()
		{
			readEntities = new List<Entity>();
		}

		public ReadQueryResult(Entity entity, bool result) : base(result)
		{
			readEntities = entity != null ? new List<Entity> () { entity } : new List<Entity>();
		}

		public ReadQueryResult(IEnumerable<Entity> re, bool result) : base(result)
		{
			readEntities = re;
		}

		public IEnumerable<Entity> GetEntities() { return readEntities; }
	}
}
