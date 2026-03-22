using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace UniversalRadialMenu;

public class Logger
{
	private static string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UniversalRadialMenu_Debug.log");

	private static object logLock = RuntimeHelpers.GetObjectValue(new object());

	public static bool Enabled { get; set; } = true;

	public static void Log(string msg)
	{
		if (!Enabled) return;
		try
		{
			lock (logLock)
			{
				File.AppendAllText(logPath, DateTime.Now.ToString("HH:mm:ss.fff") + " " + msg + "\r\n");
			}
		}
		catch { }
	}
}
