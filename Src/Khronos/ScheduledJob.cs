using System;
using System.Reactive.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Khronos
{
	public abstract class ScheduledJob
	{
		private TimeSpan interval;
		private Action action;
		private IObservable<long> timer;

		public void Start()
		{
			timer = Observable.Interval(interval);
			timer.Subscribe(x => action.Invoke());
		}

		public void Stop()
		{
			timer = null;
		}

		protected void SetAction(Action newAction)
		{
			action = newAction;
		}

		protected void SetInterval(TimeSpan newInterval)
		{
			interval = newInterval;
		}
	}

	public class Job<T> : ScheduledJob
	{
		private Job(Action<T> action)
		{
			SetAction(() => action.Invoke(ServiceLocator.Current.GetInstance<T>()));
		}

		public static Job<T> Run(Action<T> action)
		{
			return new Job<T>(action);
		}

		public Job<T> Every(TimeSpan interval)
		{
			SetInterval(interval);
			return this;
		}
	}
}