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
		public IScheduler Create()
		{
			try
			{
				return ServiceLocator.Current.GetInstance<IScheduler>();
			}
			catch
			{
				return new DefaultScheduler();
			}
		}

		public ISchedulerFactory UseContainer(ServiceLocatorProvider serviceProvider)
		{
			ServiceLocator.SetLocatorProvider(serviceProvider);
			return this;
		}
	}
}