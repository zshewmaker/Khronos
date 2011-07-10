using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Khronos.UnitTests
{
	[TestFixture]
	public class When_creating_scheduler
	{
		[Test]
		public void Should_return_DefaultScheduler_if_no_container_set()
		{
			ISchedulerFactory factory = new DefaultSchedulerFactory();

			var scheduler = factory.Create();

			Assert.That(scheduler, Is.InstanceOf<DefaultScheduler>());
		}

		[Test]
		public void Should_use_castle_dependency_if_castle_set_as_container()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<IScheduler>().ImplementedBy<TestScheduler>());

			var factory = new DefaultSchedulerFactory();
			var scheduler = factory.UseContainer(() => new CastleServiceProvider(container)).Create();

			Assert.That(scheduler, Is.InstanceOf<TestScheduler>());
		}

		[Test]
		public void Should_return_DefaultScheduler_if_container_set_but_scheduler_not_registered()
		{
			var container = new WindsorContainer();
			container.Register();

			var factory = new DefaultSchedulerFactory();
			var scheduler = factory.UseContainer(() => new CastleServiceProvider(container)).Create();

			Assert.That(scheduler, Is.InstanceOf<DefaultScheduler>());
		}


		private class TestScheduler : IScheduler
		{
			public IScheduler Setup(params ScheduledJob[] newScheduledJobs)
			{
				return null;
			}

			public void Start()
			{
			}

			public void Stop()
			{
			}
		}
	}
}