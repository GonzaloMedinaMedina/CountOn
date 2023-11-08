using DBManager.Entities;

namespace DBManager.QueryResults
{
	public class ReadQueryResult<T> : QueryResult where T : Entity
	{
		private readonly IEnumerable<T> readEntities;

		public ReadQueryResult() : base()
		{
			readEntities = new List<T>();
		}

		public ReadQueryResult(T entity, bool result) : base(result)
		{
			readEntities = entity != null ? new List<T> () { entity } : new List<T>();
		}

		public ReadQueryResult(IEnumerable<T> re, bool result) : base(result)
		{
			readEntities = re;
		}

		public IEnumerable<T> GetEntities() { return readEntities; }
	}
}
