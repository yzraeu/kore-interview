using Dapper;
using KI.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KI.Repository
{
	public interface IOrderDetailsRepository
	{
		Task<IEnumerable<OrderDetails>> GetAllPaginated(string searchQuery, int offset, int next);
	}

	public class OrderDetailsRepository : BaseRepository, IOrderDetailsRepository
	{
		public OrderDetailsRepository(IConfiguration configuration, ILogger<OrderDetailsRepository> logger) : base(configuration, logger) { }

		public async Task<IEnumerable<OrderDetails>> GetAllPaginated(string searchQuery, int offset, int next)
		{
			var sw = Stopwatch.StartNew();

			var query = @"
SELECT 
	pr.Name AS ProductName,
	pr.ProductNumber,
	pm.Name AS ProductModelName,
	psc.Name AS ProductSubCategoryName,
	soh.PurchaseOrderNumber,
	soh.AccountNumber,
	pe.FirstName + ' ' + pe.LastName AS SalesPersonName,
	sod.CarrierTrackingNumber,
	sod.OrderQty,
	sod.UnitPrice,
	sod.LineTotal,
	sod.ModifiedDate
FROM
	Sales.SalesOrderDetail AS sod
	INNER JOIN Sales.SalesOrderHeader as soh ON soh.SalesOrderID = sod.SalesOrderID
		LEFT JOIN Person.Person AS pe ON pe.BusinessEntityID = soh.SalesPersonID
	INNER JOIN Production.Product AS pr ON pr.ProductID = sod.ProductID
		LEFT JOIN Production.ProductModel AS pm ON pm.ProductModelID = pr.ProductModelID
		LEFT JOIN Production.ProductSubcategory AS psc ON psc.ProductSubcategoryID = pr.ProductSubcategoryID
WHERE 
	pm.Name LIKE @searchQuery
	OR pr.ProductNumber LIKE @searchQuery
	OR pm.Name LIKE @searchQuery
	OR psc.Name LIKE @searchQuery
	OR soh.PurchaseOrderNumber LIKE @searchQuery
	OR soh.AccountNumber LIKE @searchQuery
	OR pe.FirstName LIKE @searchQuery
	OR pe.LastName LIKE @searchQuery
	OR sod.CarrierTrackingNumber LIKE @searchQuery
ORDER BY pr.Name
OFFSET @offset ROWS 
FETCH NEXT @next ROWS ONLY
";

			try
			{
				using (var connection = new SqlConnection(_connString))
				{
					await connection.OpenAsync();

					return await connection.QueryAsync<OrderDetails>(query, new { searchQuery = $"%{searchQuery}%", offset, next });
				}
			}
			finally
			{
				_logger.LogInformation($"GetAllPaginated took {sw.ElapsedMilliseconds} ms");
			}

		}

	}
}
