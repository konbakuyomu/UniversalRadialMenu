using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

[StandardModule]
public sealed class Program
{
	private class UniversalRadialMenuAppContext : ApplicationContext
	{
		[CompilerGenerated]
		internal sealed class _Closure_0024__18_002D0
		{
			public string _0024VB_0024Local_cmd;

			public UniversalRadialMenuAppContext _0024VB_0024Me;

			public _Closure_0024__18_002D0(_Closure_0024__18_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_cmd = arg0._0024VB_0024Local_cmd;
				}
			}

			[SpecialName]
			internal void _Lambda_0024__0()
			{
				_0024VB_0024Me.HandleCommand(_0024VB_0024Local_cmd);
			}
		}

		private readonly Control invoker;

		private readonly RadialMenuConfigStore store;

		private readonly RadialMenuController controller;

		private readonly Thread pipeThread;

		private NotifyIcon trayIcon;

		private bool exiting;

		private StartupToastForm toastForm;

		private bool warmupStarted;

		private bool warmupDone;

		private List<Action> warmupCallbacks;

		public UniversalRadialMenuAppContext(string initialCommand)
		{
			UniversalRadialMenuAppContext universalRadialMenuAppContext = this;
			exiting = false;
			toastForm = null;
			warmupStarted = false;
			warmupDone = false;
			warmupCallbacks = new List<Action>();
			invoker = new Control();
			invoker.CreateControl();
			pipeThread = new Thread(PipeServerLoop);
			pipeThread.IsBackground = true;
			pipeThread.SetApartmentState(ApartmentState.MTA);
			try
			{
				pipeThread.Priority = ThreadPriority.Highest;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			pipeThread.Start();
			store = new RadialMenuConfigStore();
			controller = new RadialMenuController(store, invoker);
			InitializeTrayIcon();
			Application.ApplicationExit += [SpecialName] (object a0, EventArgs a1) =>
			{
				Cleanup();
			};
			invoker.BeginInvoke((Action)([SpecialName] () =>
			{
				universalRadialMenuAppContext.HandleCommand(initialCommand);
			}));
		}

		private void InitializeTrayIcon()
		{
			trayIcon = new NotifyIcon();
			try
			{
				int num = 16;
				try
				{
					num = Math.Max(16, SystemInformation.SmallIconSize.Width);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					num = 16;
					ProjectData.ClearProjectError();
				}
				trayIcon.Icon = RadialMenuController.CreateTitleIcon("轮盘", num);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				trayIcon.Icon = SystemIcons.Application;
				ProjectData.ClearProjectError();
			}
			trayIcon.Text = "通用轮盘";
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			contextMenuStrip.Items.Add("打开设置", null, [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__11_002D0();
			});
			contextMenuStrip.Items.Add(new ToolStripSeparator());
			contextMenuStrip.Items.Add("退出", null, [SpecialName] (object a0, EventArgs a1) =>
			{
				ExitRequested();
			});
			trayIcon.ContextMenuStrip = contextMenuStrip;
			trayIcon.Visible = true;
			trayIcon.DoubleClick += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__11_002D3();
			};
		}

		private void ExitRequested()
		{
			if (!exiting)
			{
				exiting = true;
				Cleanup();
				ExitThread();
			}
		}

		private void Cleanup()
		{
			try
			{
				controller.Shutdown();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (trayIcon != null)
				{
					trayIcon.Visible = false;
					trayIcon.Dispose();
					trayIcon = null;
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
		}

		private void EnsureWarmUp(Action onCompleted = null)
		{
			if (onCompleted != null)
			{
				if (warmupDone)
				{
					try
					{
						invoker.BeginInvoke(onCompleted);
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
				}
				else
				{
					warmupCallbacks.Add(onCompleted);
				}
			}
			if (warmupStarted)
			{
				return;
			}
			warmupStarted = true;
			controller.WarmUp([SpecialName] () =>
			{
				warmupDone = true;
				Action[] array = warmupCallbacks.ToArray();
				warmupCallbacks.Clear();
				Action[] array2 = array;
				foreach (Action action in array2)
				{
					try
					{
						action();
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						ProjectData.ClearProjectError();
					}
				}
			});
		}

		private void ShowToast(string text)
		{
			try
			{
				if (toastForm == null || toastForm.IsDisposed)
				{
					toastForm = new StartupToastForm();
				}
				toastForm.SetText(text);
				if (!toastForm.Visible)
				{
					toastForm.Show();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}

		private void CloseToast()
		{
			try
			{
				if (toastForm != null && !toastForm.IsDisposed)
				{
					toastForm.Close();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}

		private void HandleCommand(string cmd)
		{
			string text = cmd ?? "";
			int num = text.IndexOf('|');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			text = text.Trim();
			if (string.Equals(text, "SETTINGS", StringComparison.OrdinalIgnoreCase))
			{
				EnsureWarmUp();
				controller.OpenSettings("");
			}
			else if (string.Equals(text, "SHOW", StringComparison.OrdinalIgnoreCase))
			{
				EnsureWarmUp();
				controller.ShowForForegroundProcess();
			}
			else if (string.Equals(text, "BOOT", StringComparison.OrdinalIgnoreCase))
			{
				ShowToast("无限轮盘正在启动");
				EnsureWarmUp([SpecialName] () =>
				{
					ShowToast("无限轮盘已加载完毕，请再次触发动作");
					try
					{
						toastForm.AutoClose(2000);
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
				});
			}
			else if (string.Equals(text, "EXIT", StringComparison.OrdinalIgnoreCase))
			{
				ExitRequested();
			}
			else if (string.Equals(text, "BACKGROUND", StringComparison.OrdinalIgnoreCase))
			{
				EnsureWarmUp();
			}
			else
			{
				EnsureWarmUp();
				controller.ShowForForegroundProcess();
			}
		}

		private void PipeServerLoop()
		{
			_Closure_0024__18_002D0 closure_0024__18_002D = default(_Closure_0024__18_002D0);
			while (true)
			{
				try
				{
					using NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("LDH_UniversalRadialMenu_Pipe", PipeDirection.In, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
					namedPipeServerStream.WaitForConnection();
					using StreamReader streamReader = new StreamReader(namedPipeServerStream);
					closure_0024__18_002D = new _Closure_0024__18_002D0(closure_0024__18_002D);
					closure_0024__18_002D._0024VB_0024Me = this;
					string text = streamReader.ReadLine();
					if (!string.IsNullOrWhiteSpace(text))
					{
						closure_0024__18_002D._0024VB_0024Local_cmd = text.Trim();
						invoker.BeginInvoke(new Action(closure_0024__18_002D._Lambda_0024__0));
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					Thread.Sleep(100);
					ProjectData.ClearProjectError();
				}
			}
		}

		[SpecialName]
		[CompilerGenerated]
		private void _Lambda_0024__11_002D0()
		{
			invoker.BeginInvoke((Action)([SpecialName] () =>
			{
				controller.OpenSettings("");
			}));
		}

		[SpecialName]
		[CompilerGenerated]
		private void _Lambda_0024__11_002D3()
		{
			invoker.BeginInvoke((Action)([SpecialName] () =>
			{
				controller.ShowForForegroundProcess();
			}));
		}
	}

	private const string MutexName = "LDH_UniversalRadialMenu_SingleInstance";

	private const string PipeName = "LDH_UniversalRadialMenu_Pipe";

	private static bool quckerResolveHooked = false;

	[STAThread]
	public static void Main()
	{
		string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();
		string text = GetInitialCommand(args);
		if (TrySendCommandToExisting(text, 50))
		{
			return;
		}
		bool createdNew = false;
		Mutex mutex = null;
		try
		{
			mutex = new Mutex(initiallyOwned: true, "LDH_UniversalRadialMenu_SingleInstance", out createdNew);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			createdNew = false;
			ProjectData.ClearProjectError();
		}
		if (!createdNew)
		{
			TrySendCommandToExisting(text, 200);
			return;
		}
		if (string.Equals(text, "SHOW", StringComparison.OrdinalIgnoreCase))
		{
			text = "BOOT";
		}
		if (Operators.CompareString(text, "EXIT", TextCompare: false) == 0)
		{
			try
			{
				mutex.ReleaseMutex();
				return;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
				return;
			}
		}
		SetupUnhandledExceptionHandlers();
		LogStartupInfo(args);
		EnsureQuickerAssemblyResolve();
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		UniversalRadialMenuAppContext context = new UniversalRadialMenuAppContext(text);
		try
		{
			Application.Run(context);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Log("Main loop error: " + ex2.ToString());
			MessageBox.Show("程序发生严重错误：" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			ProjectData.ClearProjectError();
		}
		try
		{
			mutex.ReleaseMutex();
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
	}

	private static string GetInitialCommand(string[] args)
	{
		string result = "BACKGROUND";
		if (args.Any([SpecialName] (string a) => string.Equals(a, "--exit", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "EXIT", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/exit", StringComparison.OrdinalIgnoreCase)))
		{
			result = "EXIT";
		}
		else if (args.Any([SpecialName] (string a) => string.Equals(a, "--settings", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/settings", StringComparison.OrdinalIgnoreCase)))
		{
			result = "SETTINGS";
		}
		else if (args.Any([SpecialName] (string a) => string.Equals(a, "--show", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "SHOW", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "/show", StringComparison.OrdinalIgnoreCase)))
		{
			result = "SHOW";
		}
		else if (args.Length > 0)
		{
			result = "SHOW";
		}
		return result;
	}

	private static bool TrySendCommandToExisting(string initialCommand, int connectTimeoutMs)
	{
		string text = initialCommand;
		if (Operators.CompareString(text, "BACKGROUND", TextCompare: false) == 0)
		{
			text = "SHOW";
		}
		bool result;
		try
		{
			using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", "LDH_UniversalRadialMenu_Pipe", PipeDirection.Out))
			{
				namedPipeClientStream.Connect(connectTimeoutMs);
				using StreamWriter streamWriter = new StreamWriter(namedPipeClientStream);
				streamWriter.AutoFlush = true;
				streamWriter.WriteLine(text);
			}
			result = true;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private static void SetupUnhandledExceptionHandlers()
	{
		AppDomain.CurrentDomain.UnhandledException += [SpecialName] (object sender, UnhandledExceptionEventArgs e) =>
		{
			try
			{
				string text = ((e.ExceptionObject is Exception ex) ? ex.ToString() : "Unknown Error");
				Logger.Log("FATAL UnhandledException: " + text);
				File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UniversalRadialMenu_Crash.log"), DateTime.Now.ToString() + " FATAL: " + text + "\r\n");
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		};
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		Application.ThreadException += [SpecialName] (object sender, ThreadExceptionEventArgs e) =>
		{
			try
			{
				Logger.Log("FATAL ThreadException: " + e.Exception.ToString());
				File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UniversalRadialMenu_Crash.log"), DateTime.Now.ToString() + " FATAL: " + e.Exception.ToString() + "\r\n");
				MessageBox.Show("程序发生未处理的错误，即将尝试继续运行。\r\n" + e.Exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		};
	}

	private static void LogStartupInfo(string[] args)
	{
		try
		{
			string text = "";
			try
			{
				text = Application.ExecutablePath;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				text = "";
				ProjectData.ClearProjectError();
			}
			string text2 = "";
			try
			{
				text2 = Assembly.GetExecutingAssembly().Location;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				text2 = "";
				ProjectData.ClearProjectError();
			}
			string text3 = "";
			try
			{
				text3 = AppDomain.CurrentDomain.BaseDirectory;
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				text3 = "";
				ProjectData.ClearProjectError();
			}
			string text4 = "";
			string text5 = "";
			try
			{
				if (!string.IsNullOrWhiteSpace(text) && File.Exists(text))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
					text4 = versionInfo.FileVersion ?? "";
					text5 = versionInfo.ProductVersion ?? "";
				}
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				text4 = "";
				text5 = "";
				ProjectData.ClearProjectError();
			}
			string text6 = "";
			try
			{
				text6 = string.Join(" ", args);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				text6 = "";
				ProjectData.ClearProjectError();
			}
			Logger.Log("Startup: exe=" + text + ", asm=" + text2 + ", base=" + text3 + ", fileVer=" + text4 + ", productVer=" + text5 + ", args=" + text6);
		}
		catch (Exception projectError6)
		{
			ProjectData.SetProjectError(projectError6);
			ProjectData.ClearProjectError();
		}
	}

	private static void EnsureQuickerAssemblyResolve()
	{
		if (quckerResolveHooked)
		{
			return;
		}
		quckerResolveHooked = true;
		AppDomain.CurrentDomain.AssemblyResolve += [SpecialName] (object sender, ResolveEventArgs e) =>
		{
			try
			{
				string text = "";
				try
				{
					text = new AssemblyName(e.Name).Name;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					text = e.Name;
					ProjectData.ClearProjectError();
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					return (Assembly)null;
				}
				if (!text.StartsWith("Quicker", StringComparison.OrdinalIgnoreCase))
				{
					return (Assembly)null;
				}
				List<string> list = new List<string>();
				try
				{
					string text2 = "C:\\Program Files\\Quicker\\QuickerStarter.exe";
					if (!string.IsNullOrWhiteSpace(text2))
					{
						string directoryName = Path.GetDirectoryName(text2);
						if (!string.IsNullOrWhiteSpace(directoryName))
						{
							list.Add(directoryName);
						}
					}
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				try
				{
					list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Quicker"));
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				try
				{
					list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Quicker"));
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
				}
				List<string> list2 = list.Where([SpecialName] (string x) => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
				foreach (string item in list2)
				{
					string text3 = Path.Combine(item, text + ".dll");
					if (File.Exists(text3))
					{
						try
						{
							Logger.Log("AssemblyResolve: load " + text + " from " + text3);
						}
						catch (Exception projectError5)
						{
							ProjectData.SetProjectError(projectError5);
							ProjectData.ClearProjectError();
						}
						return Assembly.LoadFrom(text3);
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				try
				{
					Logger.Log("AssemblyResolve error: " + ex2.Message);
				}
				catch (Exception projectError6)
				{
					ProjectData.SetProjectError(projectError6);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
			return (Assembly)null;
		};
	}
}
