using System;
using System.Threading;
using NUnit.Framework;

namespace Khronos.UnitTests
{
	[TestFixture]
	public class When_scheduling_jobs_without_container
	{
		[Test]
		public void Should_run_every_scheduled_second_without_providing_instance()
		{
			var scheduler = new DefaultSchedulerFactory().Create();

			scheduler.Setup(
				Job.Run<TestJob>(x => x.RunJob()).Every(1.Second())
				).Start();

			Thread.Sleep(TimeSpan.FromSeconds(5));
			scheduler.Stop();

			Assert.That(TestJob.TimesCalled, Is.EqualTo(5));
		}

		[Test]
		public void Should_run_every_scheduled_second_with_providing_instance()
		{
			var testJob = new TestJob2();
			var scheduler = new DefaultSchedulerFactory().Create();

			scheduler.Setup(
				Job.Run(testJob.RunJob).Every(1.Second())
				).Start();

			Thread.Sleep(TimeSpan.FromSeconds(5));
			scheduler.Stop();

			Assert.That(testJob.TimesCalled, Is.EqualTo(5));
		}

		private class TestJob
		{
			public static int TimesCalled { get; private set; }

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