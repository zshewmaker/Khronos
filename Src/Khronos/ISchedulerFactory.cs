using Microsoft.Practices.ServiceLocation;

namespace Khronos
{
	public interface ISchedulerFactory
	{
		IScheduler Create();
		ISchedulerFactory UseContainer(ServiceLocatorProvider serviceProvider);
	}

	public class DefaultSchedulerFactory : ISchedulerFactory
	{
		private bool useServiceLocator;

		public IScheduler Create()
		{
			if (useServiceLocator)
			{
				return ServiceLocator.Current.GetInstance<IScheduler>();
			}
			return new DefaultScheduler();
		}

		public ISchedulerFactory UseContainer(ServiceLocatorProvider serviceProvider)
		{
			useServiceLocator = true;
			ServiceLocator.SetLocatorProvider(serviceProvider);
			return this;
		}
	}
}