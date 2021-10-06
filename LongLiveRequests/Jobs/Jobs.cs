using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LongLiveRequests
{
	public class Jobs
	{
		public static async Task<IActionResult> TimeoutTask(int delay, CancellationToken cancellationToken)
		{
			try
			{
				System.Console.WriteLine("Timeout Started");
				await Task.Delay(delay, cancellationToken);
				System.Console.WriteLine("Timeout Finished");
			}
			catch (System.Exception)
			{
				System.Console.WriteLine("Timeout Cancelled...");
			}

			return new BadRequestObjectResult("Timeout Finished");
		}

		public static async Task<IActionResult> JobTask(int delay, CancellationToken cancellationToken)
		{
			try
			{
				System.Console.WriteLine("Job Started");
				await Task.Delay(delay, cancellationToken);
				System.Console.WriteLine("Job Finished");
			}
			catch (System.Exception)
			{
				System.Console.WriteLine("Job Cancelled");
			}

			return new OkObjectResult("Job Finished");
		}
	}
}
