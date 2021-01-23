using Dapper;
using KI.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KI.Repository
{
	public interface IOrderDetailsRepository
	{
		Task<PaginatedResult<OrderDetails>> GetAllPaginated(string searchQuery, int offset, int next);
	}

	public class OrderDetailsRepository : BaseRepository, IOrderDetailsRepository
	{
		public OrderDetailsRepository(IConfiguration configuration, ILogger<OrderDetailsRepository> logger) : base(configuration, logger) { }

		public async Task<PaginatedResult<OrderDetails>> GetAllPaginated(string searchQuery, int offset, int next)
		{
			var sw = Stopwatch.StartNew();

			var output = new PaginatedResult<OrderDetails>();

			var queryCore = @"FROM
	Sales.SalesOrderDetail AS sod
	INNER JOIN Sales.SalesOrderHeader as soh ON soh.SalesOrderID = sod.SalesOrderID
		LEFT JOIN Person.Person AS pe ON pe.BusinessEntityID = soh.SalesPersonID
	INNER JOIN Production.Product AS pr ON pr.ProductID = sod.ProductID
		LEFT JOIN Production.ProductModel AS pm ON pm.ProductModelID = pr.ProductModelID
		LEFT JOIN Production.ProductSubcategory AS psc ON psc.ProductSubcategoryID = pr.ProductSubcategoryID
WHERE 
	soh.PurchaseOrderNumber LIKE @searchQuery
	OR pr.ProductNumber LIKE @searchQuery
	OR pr.Name LIKE @searchQuery
	OR soh.AccountNumber LIKE @searchQuery
	OR pe.FirstName LIKE @searchQuery
	OR pe.LastName LIKE @searchQuery
	OR pe.FirstName + ' ' + pe.LastName LIKE @searchQuery
	OR sod.CarrierTrackingNumber LIKE @searchQuery";

			var query = @$"
SELECT 
	soh.PurchaseOrderNumber,
	pr.ProductNumber,
	pr.Name AS ProductName,
	soh.AccountNumber,
	pe.FirstName + ' ' + pe.LastName AS SalesPersonName,
	sod.CarrierTrackingNumber,
	sod.OrderQty,
	sod.UnitPrice,
	sod.LineTotal,
	sod.ModifiedDate
{queryCore}
ORDER BY pr.Name
OFFSET @offset ROWS 
FETCH NEXT @next ROWS ONLY

SELECT 
	COUNT(*)
{queryCore}
";

			try
			{
				using (var connection = new SqlConnection(_connString))
				{
					await connection.OpenAsync();

					var multiResult = await connection.QueryMultipleAsync(query, new { searchQuery = $"%{searchQuery}%", offset, next });

					output.Items = multiResult.Read<OrderDetails>();
					output.RowCount = multiResult.ReadFirst<int>();

					return output;
				}
			}
			finally
			{
				_logger.LogInformation($"GetAllPaginated took {sw.ElapsedMilliseconds} ms");
			}

		}

	}
}
