using KI.AppService;
using KI.AppService.ViewModel;
using KI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
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
		[ProducesResponseType(typeof(PaginatedResult<OrderDetailsViewModel>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Get([FromQuery]string searchQuery, [FromQuery] int offset, [FromQuery] int next)
		{
			if (string.IsNullOrEmpty(searchQuery) || offset < 0 || next <= 0)
				return BadRequest("request parameters are invalid"); // less info on purpose - avoid security issues

			try
			{
				return Ok(await _orderDetailsAppService.GetAllPaginated(searchQuery, offset, next));
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
