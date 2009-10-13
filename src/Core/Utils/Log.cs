using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
// COPIED FROM CRUISECONTROL.NET

namespace ThoughtWorks.TreeSurgeon.Core.Utils
{
	public class Log
	{
		/// <summary>
		/// Utility type, not intended for instantiation.
		/// </summary>
		private Log()
		{
		}

		public static void Info(string message)
		{
			WriteLine("Info", message, LogSwitch.TraceInfo);
		}

		public static void Debug(string message)
		{
			WriteLine("Debug", message, LogSwitch.TraceVerbose);
		}

		public static void Warning(string message)
		{
			WriteLine("Warning", message, LogSwitch.TraceWarning);
		}

		public static void Warning(Exception ex)
		{
			WriteLine("Warning", ex, LogSwitch.TraceWarning);
		}

		public static void Error(string message)
		{
			WriteLine("Error", message, LogSwitch.TraceError);
		}

		public static void Error(Exception ex)
		{
			WriteLine("Error", ex, LogSwitch.TraceError);
		}

		private static string CreateExceptionMessage(Exception ex)
		{
			StringBuilder buffer = new StringBuilder();
			buffer.Append(ex.Message).Append(Environment.NewLine);
			buffer.Append("----------").Append(Environment.NewLine);
			buffer.Append(ex.ToString()).Append(Environment.NewLine);
			buffer.Append("----------").Append(Environment.NewLine);
			return buffer.ToString();
		}

		private static string GetContextName()
		{
			return (Thread.CurrentThread.Name == null) ? "CruiseControl Server" : Thread.CurrentThread.Name;
		}

		private static void WriteLine(string level, Exception ex, bool traceSwitch)
		{
			WriteLine(level, CreateExceptionMessage(ex), traceSwitch);
		}

		private static void WriteLine(string level, string message, bool traceSwitch)
		{
			if (traceSwitch)
			{
				WriteLine(level, message);
			}
		}

		private static void WriteLine(string level, string message)
		{
			string category = string.Format("[{0}:{1}]", GetContextName(), level);
			Trace.WriteLine(message, category);
		}

		private static TraceSwitch LogSwitch = new CruiseControlTraceSwitch();

		private class CruiseControlTraceSwitch : TraceSwitch
		{
			public CruiseControlTraceSwitch() : base("CruiseControlSwitch", "TraceSwitch used for instrumenting CruiseControl.NET")
			{
				if (Level == TraceLevel.Off)
				{
					Level = TraceLevel.Error;
				}
			}
		}
	}
}