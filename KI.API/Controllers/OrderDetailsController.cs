using KI.AppService.ViewModel;
using KI.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KI.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderDetailsController : ControllerBase
	{
		private readonly ILogger<OrderDetailsController> _logger;
		private readonly IOrderDetailsAppService _orderDetailsAppService;

		public OrderDetailsController(ILogger<OrderDetailsController> logger, IOrderDetailsAppService orderDetailsAppService)
		{
			_logger = logger;
			_orderDetailsAppService = orderDetailsAppService;
		}

		[HttpGet]
		public async Task<IEnumerable<OrderDetailsViewModel>> Get([FromQuery]string searchQuery, [FromQuery] int offset, [FromQuery] int next)
		{
			//if (string.IsNullOrEmpty(searchQuery) || offset < 0 || next < 0 || offset >= next)
			//	return BadRequest("request parameters are invalid");

			return await _orderDetailsAppService.GetAllPaginated(searchQuery.Trim(), offset, next);
		}
	}
}
