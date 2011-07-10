using System.Collections.Generic;
using System.Linq;

namespace Khronos
{
	public interface IScheduler
	{
		IScheduler Setup(params ScheduledJob[] newScheduledJobs);
		void Start();
		void Stop();
	}

	public class DefaultScheduler : IScheduler
	{
		private IEnumerable<ScheduledJob> scheduledJobs;

		public DefaultScheduler()
		{
			scheduledJobs = new List<ScheduledJob>();
		}

		public IScheduler Setup(params ScheduledJob[] newScheduledJobs)
		{
			scheduledJobs = scheduledJobs.Concat(newScheduledJobs);
			return this;
		}

		public void Start()
		{
			foreach(var job in scheduledJobs)
			{
				job.Start();
			}
		}

		public void Stop()
		{
			foreach (var job in scheduledJobs)
			{
				job.Stop();
			}
		}
	}
}