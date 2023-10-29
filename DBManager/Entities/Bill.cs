using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
	public class Bill
	{
		public string Id { get { return _id; } }
		public string Name { get { return _name; } set { _name = value; } }
		public int Price { get { return _price; } set { _price = value; } }
		public BillType BillType { get { return _billType; } set { _billType= value; } }
		public DateTime Date { get { return _date; } set { _date = value; } }

		private string _id { get; set; }
		private string _name { get; set; }
		private int _price { get; set; }
		private BillType _billType { get; set; }
		private DateTime _date { get; set; }

		public Bill() 
		{}

		public Bill(string id, string name, int price, BillType billType, DateTime date)
		{
			_id = id;
			_name = name;
			_price = price;
			_billType = billType;
			_date = date;
		}
	}
}
