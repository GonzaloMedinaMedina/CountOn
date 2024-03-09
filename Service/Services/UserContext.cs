using DBManager.Entities;

namespace Service.Services
{
	public class UserContext
	{
		public UserContext() 
		{
			BillType = BillType.ALL;
		}

		private readonly static UserContext _instance = new UserContext();
		public static UserContext Instance
		{
			get
			{
				return _instance;
			}
		}

		public BillType BillType { get; set; }
	}
}
