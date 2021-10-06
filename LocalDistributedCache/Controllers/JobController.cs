using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LocalDistributedCache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LocalDistributedCache.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class JobController : ControllerBase
	{
		private readonly ILogger<JobController> _logger;
		private readonly IDistributedCache _distributedCache;

		public JobController(ILogger<JobController> logger, IDistributedCache cache)
		{
			_logger = logger;
			_distributedCache = cache;
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Result(Guid uniqueIdentifier, CancellationToken token)
		{
			var cacheKey = uniqueIdentifier.ToString();
			var dataAsString = "";

			while (string.IsNullOrEmpty(dataAsString) && !token.IsCancellationRequested)
			{
				dataAsString = _distributedCache.GetString(cacheKey);
				if (!string.IsNullOrEmpty(dataAsString))
				{
					var data = JsonSerializer.Deserialize<CallbackData>(dataAsString);
					return Ok(data);
				}

				await Task.Delay(1000);
			}

			return BadRequest("An Error Occured");
		}
	}
}
