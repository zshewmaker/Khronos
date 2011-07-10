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

			var result = factory.Create();

			Assert.That(result, Is.InstanceOf<DefaultScheduler>());
		}

		[Test]
		public void Should_use_castle_dependency_if_castle_set_as_container()
		{
			var container = new WindsorContainer();
			container.Register(Component.For<IScheduler>().ImplementedBy<TestScheduler>());

			var factory = new DefaultSchedulerFactory();
			var result = factory.UseContainer(() => new CastleServiceLocator(container)).Create();

			Assert.That(result, Is.InstanceOf<TestScheduler>());
		}

		private class TestScheduler : IScheduler{}
	}
}