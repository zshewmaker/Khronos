using System.Collections.Generic;
using System.Linq;

namespace Khronos
{
	public interface IScheduler
	{
		IScheduler Setup(params Job[] newJobs);
		void Start();
		void Stop();
	}

	public class DefaultScheduler : IScheduler
	{
		private IEnumerable<Job> scheduledJobs;

		public DefaultScheduler()
		{
			scheduledJobs = new List<Job>();
		}

		public IScheduler Setup(params Job[] newJobs)
		{
			scheduledJobs = scheduledJobs.Concat(newJobs);
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