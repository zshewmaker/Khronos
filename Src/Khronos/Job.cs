using System;
using System.Reactive.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Khronos
{
	public class Job
	{
		private TimeSpan interval;
		private readonly Action action;
		private IObservable<long> timer;

		public void Start()
		{
			timer = Observable.Interval(interval);
			timer.Subscribe(x => action());
		}

		public void Stop()
		{
			timer = null;
		}

		private Job(Action newAction)
		{
			action = newAction;
		}

		private static T GetServiceInstance<T>()
		{
			try
			{
				return ServiceLocator.Current.GetInstance<T>();
			}
			catch
			{
				return Activator.CreateInstance<T>();
			}
		}

		public static Job Run<T>(Action<T> action)
		{
			return new Job(() => action(GetServiceInstance<T>()));
		}

		public static Job Run(Action action)
		{
			return new Job(action);
		}

		public Job Every(TimeSpan newInterval)
		{
			interval = newInterval;
			return this;
		}
	}
}