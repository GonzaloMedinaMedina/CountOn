using System.Text.Json;

namespace DBManager.Entities
{
	public enum BillType
	{
		HOBBY,
		FOOD,
		CLOTHES,
		RENT,
		BILLS,
		OTHERS
	}

	public class Bill : Entity
	{
		public string Name { get; set; }
		public int Price { get; set; }
		public BillType BillType { get; set; }
		public DateTime Date { get; set; }

		public Bill() : base(-1)
		{ }

		public Bill(string name, int price, BillType billType, DateTime date) : base(-1)
		{
			Name = name;
			Price = price;
			BillType = billType;
			Date = date;
		}

		public static Bill Deserialize(string billString)
		{
			var bill = String.IsNullOrWhiteSpace(billString) ? null : JsonSerializer.Deserialize<DBManager.Entities.Bill>(billString);
			JsonDocument jsonDocument = JsonDocument.Parse(billString);
			JsonElement root = jsonDocument.RootElement;
			if(root.TryGetProperty("Id", out var idValue))
			{
				bill.SetId(idValue.GetInt32());
			}

			return bill;
		}
	}
}
