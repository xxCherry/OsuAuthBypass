using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace osuAuthBypass
{
	class Base
	{
		[DllImport("kernel32.dll")]
		private static extern uint SuspendThread(IntPtr hThread);

		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

		static void Main(string[] args)
		{
			var osuProcess = Process.GetProcessesByName("osu!").FirstOrDefault();
			var threads = osuProcess.Threads.Cast<ProcessThread>().OrderByDescending(x => x.UserProcessorTime.Ticks);

			var osuAuthThread = threads.ElementAt(1);

			IntPtr threadHandle = OpenThread(2, false, (uint)osuAuthThread.Id);

			SuspendThread(threadHandle);
		}
	}
}