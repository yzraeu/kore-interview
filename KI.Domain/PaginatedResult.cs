using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.Domain
{
	public class PaginatedResult<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int RowCount { get; set; }
	}
}
