using System;

namespace Khronos
{
	public static class Time
	{
		public static TimeSpan Second(this int interval)
		{
			return TimeSpan.FromSeconds(interval);
		}

		public static TimeSpan Seconds(this int interval)
		{
			return TimeSpan.FromSeconds(interval);
		}

		public static TimeSpan Minute(this int interval)
		{
			return TimeSpan.FromMinutes(interval);
		}

		public static TimeSpan Minutes(this int interval)
		{
			return TimeSpan.FromMinutes(interval);
		}

		public static TimeSpan Hour(this int interval)
		{
			return TimeSpan.FromHours(interval);
		}

		public static TimeSpan Hours(this int interval)
		{
			return TimeSpan.FromHours(interval);
		}

		public static TimeSpan Seconds(this double interval)
		{
			return TimeSpan.FromSeconds(interval);
		}
		
		public static TimeSpan Minutes(this double interval)
		{
			return TimeSpan.FromMinutes(interval);
		}

		public static TimeSpan Hours(this double interval)
		{
			return TimeSpan.FromHours(interval);
		}
	}
}