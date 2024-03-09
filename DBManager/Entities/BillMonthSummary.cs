namespace DBManager.Entities
{
	public class BillMonthSummary : Entity
	{
		public int _totalAmount { get; set; }
		public IDictionary<BillType, int> _totalAmountByBillType { get; set; } = new Dictionary<BillType, int>();
		BillMonthSummary() : base() { }

		BillMonthSummary(int amount, BillType billType) : base()
		{
			_totalAmount = amount;
			_totalAmountByBillType.Add(billType, amount);
		}
	}
}
