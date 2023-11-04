using DBManager.Entities;

namespace DBManager.QueryResults
{
	public class ReadQueryResult : QueryResult
	{
		private readonly IEnumerable<Entity> readEntities;

		public ReadQueryResult(IEnumerable<Entity> re, bool result) : base(result)
		{
			this.readEntities = re;
		}

		public IEnumerable<Entity> GetEntities() { return readEntities; }
	}
}
