using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.Repository
{
	public class BaseRepository
	{
		internal readonly IConfiguration _configuration;
		internal readonly ILogger<BaseRepository> _logger;
		internal readonly string _connString;

		public BaseRepository(IConfiguration configuration, ILogger<BaseRepository> logger)
		{
			_configuration = configuration;
			_logger = logger;

			_connString = _configuration.GetConnectionString("DefaultConnString");
		}
	}
}
