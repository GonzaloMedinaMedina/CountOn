using System.ComponentModel.DataAnnotations;

namespace DBManager.Entities
{
	public abstract class Entity
	{
		public int Id { get { return _id; } }
		[Key]
		private int _id;

		public Entity() 
		{
			_id = -1;
		}

		public Entity(int id)
		{
			_id = id;
		}

		public void SetId(int id) { _id = id; }
	}
}
