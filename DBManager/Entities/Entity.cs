namespace DBManager.Entities
{
	public class Entity
	{
		public string Id { get { return _id; } }
		private string _id { get; set; }

		public Entity(string id)
		{  
			_id = id;
		}
	}
}
