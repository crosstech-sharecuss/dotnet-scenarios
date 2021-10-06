using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LongLiveRequests.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		public TestController()
		{ }


		[HttpGet("[action]")]
		public async Task<IActionResult> JobDone()
		{
			var cancellationTokenSource = new CancellationTokenSource();

			Task<IActionResult> timeoutTask = Jobs.TimeoutTask(3000, cancellationTokenSource.Token);
			Task<IActionResult> jobTask = Jobs.JobTask(1000, cancellationTokenSource.Token);

			// Wait for any finished Job
			var resultTask = await Task.WhenAny(timeoutTask, jobTask);
			IActionResult response = await resultTask;

			cancellationTokenSource.Cancel();
			cancellationTokenSource.Dispose();

			return await Task.FromResult(response);
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Timeout()
		{
			var cancellationTokenSource = new CancellationTokenSource();

			Task<IActionResult> timeoutTask = Jobs.TimeoutTask(1000, cancellationTokenSource.Token);
			Task<IActionResult> jobTask = Jobs.JobTask(3000, cancellationTokenSource.Token);

			// Wait for any finished Job
			var resultTask = await Task.WhenAny(timeoutTask, jobTask);
			IActionResult response = await resultTask;

			cancellationTokenSource.Cancel();
			cancellationTokenSource.Dispose();

			return await Task.FromResult(response);
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> Long()
		{
			var cancellationTokenSource = new CancellationTokenSource();

			Task<IActionResult> timeoutTask = Jobs.TimeoutTask(5 * 60 * 1000, cancellationTokenSource.Token);
			Task<IActionResult> jobTask = Jobs.JobTask(9 * 60 * 1000, cancellationTokenSource.Token);

			// Wait for any finished Job
			var resultTask = await Task.WhenAny(timeoutTask, jobTask);
			IActionResult response = await resultTask;

			cancellationTokenSource.Cancel();
			cancellationTokenSource.Dispose();

			return await Task.FromResult(response);
		}
	}
}
