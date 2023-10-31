
using System.ComponentModel.DataAnnotations;

namespace DBManager.Entities
{
	public enum BillType
	{
		HOBBY,
		FOOD,
		CLOTHE,
		RENT,
		BILLS,
		OTHERS
	}

	public class Bill : Entity
	{
		[Key]
		public int BillId { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public BillType BillType { get; set; }
		public DateTime Date { get; set; }

		public Bill()
		{ }

		public Bill(int id, string name, int price, BillType billType, DateTime date)
		{
			BillId = id;
			Name = name;
			Price = price;
			BillType = billType;
			Date = date;
		}

		public override int GetId()
		{
			return BillId;
		}
	}
}
