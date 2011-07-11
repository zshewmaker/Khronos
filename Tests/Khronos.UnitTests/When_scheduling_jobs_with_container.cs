using System;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using NUnit.Framework;

namespace Khronos.UnitTests
{
	[TestFixture]
	public class When_scheduling_jobs_with_container
	{
		[Test]
		public void Should_run_every_scheduled_second()
		{
			var container = new WindsorContainer();
			var testJob = new TestJob();
			container.Register(Component.For<TestJob>().Instance(testJob));

			var scheduler = new DefaultSchedulerFactory().UseContainer(() => new WindsorServiceLocator(container)).Create();

			scheduler.Setup(
				Job.Run<TestJob>(x => x.RunJob()).Every(1.Second())
				).Start();

			Thread.Sleep(TimeSpan.FromSeconds(5));
			scheduler.Stop();

			Assert.That(testJob.TimesCalled, Is.EqualTo(5));

			container.Dispose();
		}

		[Test]
		public void Should_run_multiple_jobs_at_scheduled_interval()
		{
			var container = new WindsorContainer();
			var testJob = new TestJob();
			var testJob2 = new TestJob2();
			container.Register(
				Component.For<TestJob>().Instance(testJob),
				Component.For<TestJob2>().Instance(testJob2));

			var scheduler = new DefaultSchedulerFactory().UseContainer(() => new WindsorServiceLocator(container)).Create();

			scheduler.Setup(
				Job.Run<TestJob>(x => x.RunJob()).Every(1.Second()),
				Job.Run<TestJob2>(x => x.RunJob()).Every(2.Seconds())
				).Start();

			Thread.Sleep(TimeSpan.FromSeconds(6));
			scheduler.Stop();

			Assert.That(testJob.TimesCalled, Is.EqualTo(6));
			Assert.That(testJob2.TimesCalled, Is.EqualTo(3));

			container.Dispose();
		}

		private class TestJob
		{
			public int TimesCalled { get; private set; }

			public TestJob()
			{
				TimesCalled = 0;
			}

			public void RunJob()
			{
				TimesCalled++;
			}
		}

		private class TestJob2
		{
			public int TimesCalled { get; private set; }

			public TestJob2()
			{
				TimesCalled = 0;
			}

			public void RunJob()
			{
				TimesCalled++;
			}
		}
	}
}