using System.ComponentModel.DataAnnotations;

namespace DBManager.Entities
{
	public abstract class Entity
	{
		public int Id { get { return _id; } }
		[Key]
		private int _id;

		public Entity() { }
		public Entity(int id)
		{
			_id = id;
		}

		public void SetId(int id) { _id = id; }
	}
}
