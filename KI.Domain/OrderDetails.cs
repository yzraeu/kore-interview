using System;

namespace KI.Domain
{
	public class OrderDetails
	{
		public string ProductName { get; set; }
		public string ProductNumber { get; set; }
		public string ProductModelName { get; set; }
		public string ProductSubCategoryName { get; set; }
		public string PurchaseOrderNumber { get; set; }
		public string AccountNumber { get; set; }
		public string SalesPersonName { get; set; }
		public string CarrierTrackingNumber { get; set; }
		public int OrderQty { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal LineTotal { get; set; }
		public DateTime ModifiedDate { get; set; }

	}
}
