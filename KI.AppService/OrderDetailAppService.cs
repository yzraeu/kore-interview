using KI.AppService.ViewModel;
using KI.Domain;
using KI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;

namespace KI.AppService
{
	public interface IOrderDetailsAppService
	{
		Task<PaginatedResult<OrderDetailsViewModel>> GetAllPaginated(string searchQuery, int offset, int next);
	}

	public class OrderDetailsAppService : IOrderDetailsAppService
	{
		private readonly IOrderDetailsRepository _repository;
		public OrderDetailsAppService(IOrderDetailsRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedResult<OrderDetailsViewModel>> GetAllPaginated(string searchQuery, int offset, int next)
		{
			var data = await _repository.GetAllPaginated(searchQuery, offset, next);

			var mapped = data.Adapt<PaginatedResult<OrderDetailsViewModel>>();

			return mapped;
		}
	}
}
