using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

public class RadialMenuController
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__114_002D0
	{
		public int _0024VB_0024Local_retries;

		public string _0024VB_0024Local_text;

		public bool _0024VB_0024Local_ok;

		public int _0024VB_0024Local_sleepMs;

		public _Closure_0024__114_002D0(_Closure_0024__114_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_retries = arg0._0024VB_0024Local_retries;
				_0024VB_0024Local_text = arg0._0024VB_0024Local_text;
				_0024VB_0024Local_ok = arg0._0024VB_0024Local_ok;
				_0024VB_0024Local_sleepMs = arg0._0024VB_0024Local_sleepMs;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			int num = Math.Max(1, _0024VB_0024Local_retries);
			for (int i = 1; i <= num; i = checked(i + 1))
			{
				try
				{
					Clipboard.SetText(_0024VB_0024Local_text);
					_0024VB_0024Local_ok = true;
					break;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					if (_0024VB_0024Local_sleepMs > 0)
					{
						Thread.Sleep(_0024VB_0024Local_sleepMs);
					}
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D0
	{
		public string _0024VB_0024Local_target;

		public string _0024VB_0024Local_parentText;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D0(_Closure_0024__123_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_target = arg0._0024VB_0024Local_target;
				_0024VB_0024Local_parentText = arg0._0024VB_0024Local_parentText;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			if (_0024VB_0024Me.radialForm != null)
			{
				_0024VB_0024Me.radialForm.NavigateTo(_0024VB_0024Local_target, _0024VB_0024Local_parentText);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D1
	{
		public string _0024VB_0024Local_scope;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D1(_Closure_0024__123_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_scope = arg0._0024VB_0024Local_scope;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			_0024VB_0024Me.CloseRadialForm();
			_0024VB_0024Me.OpenSettings(_0024VB_0024Local_scope);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D2
	{
		public string _0024VB_0024Local_shortcut;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D2(_Closure_0024__123_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_shortcut = arg0._0024VB_0024Local_shortcut;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			Logger.Log("[SHORTCUT] === 开始发送快捷键: " + (_0024VB_0024Local_shortcut ?? "(null)"));
			Logger.Log("[SHORTCUT] 关闭轮盘前 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X"));
			_0024VB_0024Me.CloseRadialForm();
			Logger.Log("[SHORTCUT] CloseRadialForm 后 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X"));
			_0024VB_0024Me.RestoreForegroundWindowForInput();
			Logger.Log("[SHORTCUT] RestoreForeground 后 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X") + ", lastForegroundHwnd=" + _0024VB_0024Me.lastForegroundHwnd.ToString("X"));
			Thread.Sleep(200);
			Logger.Log("[SHORTCUT] Sleep(200) 后 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X"));
			string text = (_0024VB_0024Local_shortcut ?? "").Trim();
			if (text.Length == 0)
			{
				return;
			}
			if (!text.StartsWith("|", StringComparison.Ordinal) && ShouldUseLocalSendForShortcut(text))
			{
				Logger.Log("[SHORTCUT] 走 SendShortcutLocally 路径");
				SendShortcutLocally(text);
				return;
			}
			Logger.Log("[SHORTCUT] 走 Quicker RunAction 路径");
			string text2 = ((!text.StartsWith("|", StringComparison.Ordinal)) ? BuildSendKeysShortcut(text) : text.Substring(1));
			Logger.Log("[SHORTCUT] BuildSendKeysShortcut 结果: [" + text2 + "]");
			if (!string.IsNullOrWhiteSpace(text2))
			{
				string spec = BuildQuickerSubProgramSpecRaw("发送快捷键", text2);
				Logger.Log("[SHORTCUT] RunAction spec: [" + spec + "]");
				RunQuickerRunAction(spec);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D3
	{
		public string _0024VB_0024Local_t;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D3(_Closure_0024__123_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_t = arg0._0024VB_0024Local_t;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__5()
		{
			_0024VB_0024Me.CloseRadialForm();
			_0024VB_0024Me.RestoreForegroundWindowForInput();
			Thread.Sleep(200);
			SendTextLocally(_0024VB_0024Local_t);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D4
	{
		public string _0024VB_0024Local_t;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D4(_Closure_0024__123_002D4 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_t = arg0._0024VB_0024Local_t;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__6()
		{
			_0024VB_0024Me.CloseRadialForm();
			_0024VB_0024Me.RestoreForegroundWindowForInput();
			Thread.Sleep(200);
			RunQuickerRunAction(BuildQuickerSubProgramSpec("发送文本", _0024VB_0024Local_t));
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D5
	{
		public string _0024VB_0024Local_toRun;

		public string _0024VB_0024Local_toArgs;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D5(_Closure_0024__123_002D5 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_toRun = arg0._0024VB_0024Local_toRun;
				_0024VB_0024Local_toArgs = arg0._0024VB_0024Local_toArgs;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__7()
		{
			_0024VB_0024Me.CloseRadialForm();
			RunOpenOrCmd(_0024VB_0024Local_toRun, _0024VB_0024Local_toArgs);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D6
	{
		public string _0024VB_0024Local_payload;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D6(_Closure_0024__123_002D6 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_payload = arg0._0024VB_0024Local_payload;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__8()
		{
			_0024VB_0024Me.CloseRadialForm();
			_0024VB_0024Me.RestoreForegroundWindowForInput();
			Thread.Sleep(200);
			RunQuickerRunAction("QCAD_Bridge?" + _0024VB_0024Local_payload);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D7
	{
		public string _0024VB_0024Local_raw;

		public RadialMenuController _0024VB_0024Me;

		public _Closure_0024__123_002D7(_Closure_0024__123_002D7 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_raw = arg0._0024VB_0024Local_raw;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__9()
		{
			_0024VB_0024Me.CloseRadialForm();
			_0024VB_0024Me.RestoreForegroundWindowForInput();
			Thread.Sleep(200);
			RunQuickerRunAction(DecodeQuickerPercentNewLines(_0024VB_0024Local_raw));
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__123_002D8
	{
		public string _0024VB_0024Local_toRun;

		public _Closure_0024__123_002D8(_Closure_0024__123_002D8 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_toRun = arg0._0024VB_0024Local_toRun;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__10()
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = _0024VB_0024Local_toRun,
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				MessageBox.Show("无法执行命令：" + _0024VB_0024Local_toRun + "\r\n" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__25_002D0
	{
		public int _0024VB_0024Local_dpi;

		public _Closure_0024__25_002D0(_Closure_0024__25_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_dpi = arg0._0024VB_0024Local_dpi;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(MenuSizeDpiProfile x)
		{
			if (x != null)
			{
				return x.Dpi == _0024VB_0024Local_dpi;
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__28_002D0
	{
		public int _0024VB_0024Local_pending;

		public Action _0024VB_0024Local_onCompleted;

		public int _0024VB_0024Local_finished;

		public Action _0024VB_0024Local_signalDone;

		public RadialMenuController _0024VB_0024Me;

		public Action _0024I1;

		public _Closure_0024__28_002D0(_Closure_0024__28_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_pending = arg0._0024VB_0024Local_pending;
				_0024VB_0024Local_onCompleted = arg0._0024VB_0024Local_onCompleted;
				_0024VB_0024Local_finished = arg0._0024VB_0024Local_finished;
				_0024VB_0024Local_signalDone = arg0._0024VB_0024Local_signalDone;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			try
			{
				if (Interlocked.Decrement(ref _0024VB_0024Local_pending) != 0 || _0024VB_0024Local_onCompleted == null || _0024VB_0024Me.uiInvoker == null || _0024VB_0024Me.uiInvoker.IsDisposed || Interlocked.Exchange(ref _0024VB_0024Local_finished, 1) != 0)
				{
					return;
				}
				_0024VB_0024Me.uiInvoker.BeginInvoke((Action)([SpecialName] () =>
				{
					try
					{
						_0024VB_0024Local_onCompleted();
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						ProjectData.ClearProjectError();
					}
				}));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			try
			{
				_0024VB_0024Local_onCompleted();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R2(object a0)
		{
			_Lambda_0024__2();
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			try
			{
				RadialMenuAppConfig config = _0024VB_0024Me.config;
				if (config == null || config.ProcessMenus == null)
				{
					return;
				}
				foreach (ProcessMenuConfig processMenu in config.ProcessMenus)
				{
					if (processMenu == null)
					{
						continue;
					}
					string text = NormalizeProcessName(processMenu.ProcessName);
					if (text.Length != 0)
					{
						try
						{
							_0024VB_0024Me.GetCachedProcessSectors(text, processMenu.MenuItems);
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
					}
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			finally
			{
				_0024VB_0024Local_signalDone();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			try
			{
				_0024VB_0024Me.WarmUpForm();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			finally
			{
				_0024VB_0024Local_signalDone();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__33_002D0
	{
		public string _0024VB_0024Local_t;

		public _Closure_0024__33_002D0(_Closure_0024__33_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_t = arg0._0024VB_0024Local_t;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(string x)
		{
			return string.Equals(x, _0024VB_0024Local_t, StringComparison.OrdinalIgnoreCase);
		}
	}

	private readonly RadialMenuConfigStore store;

	private RadialMenuAppConfig config;

	private readonly Control uiInvoker;

	private RadialSectorMenuForm radialForm;

	private RadialMenuSettingsForm settingsForm;

	private readonly object sectorCacheLock;

	private readonly Dictionary<string, List<RadialSector>> cachedGlobalSectors;

	private readonly Dictionary<string, List<RadialSector>> cachedProcessSectors;

	private int sectorCacheGeneration;

	private IntPtr lastForegroundHwnd;

	private string lastForegroundProcessName;

	private System.Threading.Timer foregroundWatchTimer;

	private int foregroundWatchTicksLeft;

	private string foregroundWatchTag;

	private readonly object foregroundWatchLock;

	private static bool preferVkSpaceForCurrentSend = false;

	private static bool preferShiftInsertPasteForCurrentSend = false;

	private static readonly string currentProcessName = Process.GetCurrentProcess().ProcessName;

	private int lastForegroundPid;

	private static readonly object iconCacheLock = RuntimeHelpers.GetObjectValue(new object());

	private static readonly Dictionary<string, Image> letterIconCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

	private static readonly Dictionary<string, Image> scaledIconCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

	private static readonly Dictionary<string, Icon> formIconCache = new Dictionary<string, Icon>(StringComparer.OrdinalIgnoreCase);

	private static readonly object skinCacheLock = RuntimeHelpers.GetObjectValue(new object());

	internal const string QuickerStarterExePath = "C:\\Program Files\\Quicker\\QuickerStarter.exe";

	internal const string QuickerBridgePrefix = "QCAD_BRIDGE:";

	internal const string QuickerActionPrefix = "QCAD_ACTION:";

	internal static string NormalizeNewLinesToLf(string text)
	{
		string text2 = text ?? "";
		if (text2.Length == 0)
		{
			return "";
		}
		text2 = text2.Replace("\r\n", "\n");
		return text2.Replace("\r", "\n");
	}

	internal static string NormalizeNewLinesToCrLf(string text)
	{
		string text2 = NormalizeNewLinesToLf(text);
		if (text2.Length == 0)
		{
			return "";
		}
		return text2.Replace("\n", "\r\n");
	}

	internal static string DecodeQuickerPercentNewLines(string text)
	{
		string text2 = text ?? "";
		if (text2.Length == 0)
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(text2.Length);
		int num = 0;
		checked
		{
			while (num < text2.Length)
			{
				char c = text2[num];
				if (c == '%' && num + 2 < text2.Length)
				{
					int num2 = HexNibble(text2[num + 1]);
					int num3 = HexNibble(text2[num + 2]);
					if (num2 >= 0 && num3 >= 0)
					{
						switch ((num2 << 4) | num3)
						{
						case 10:
							stringBuilder.Append("\n");
							num += 3;
							continue;
						case 13:
							stringBuilder.Append("\r");
							num += 3;
							continue;
						}
					}
				}
				stringBuilder.Append(c);
				num++;
			}
			return stringBuilder.ToString();
		}
	}

	internal static int GetSafeDpiValue(int dpi)
	{
		if (dpi <= 0)
		{
			return 96;
		}
		return dpi;
	}

	internal static void NormalizeMenuSizeDpiProfiles(MenuSizeConfig menuSize)
	{
		if (menuSize == null)
		{
			return;
		}
		if (menuSize.DpiProfiles == null || menuSize.DpiProfiles.Count == 0)
		{
			menuSize.DpiProfiles = null;
			return;
		}
		List<MenuSizeDpiProfile> list = (from p in menuSize.DpiProfiles
			where p != null && p.Dpi > 0
			group p by p.Dpi into g
			select g.First() into p
			orderby p.Dpi
			select p).ToList();
		foreach (MenuSizeDpiProfile item in list)
		{
			if (item.OuterRadius <= 0)
			{
				item.OuterRadius = 260;
			}
			if (item.InnerRadius <= 0)
			{
				item.InnerRadius = 40;
			}
			if (item.FontSize <= 0)
			{
				item.FontSize = 12;
			}
			if (double.IsNaN(item.Scale) || double.IsInfinity(item.Scale) || item.Scale <= 0.0)
			{
				item.Scale = 1.0;
			}
		}
		menuSize.DpiProfiles = ((list.Count == 0) ? null : list);
	}

	internal static void ApplyMenuSizeDpiProfileToBase(MenuSizeConfig menuSize, int dpi)
	{
		if (menuSize != null)
		{
			dpi = GetSafeDpiValue(dpi);
			MenuSizeDpiProfile menuSizeDpiProfile = FindBestMenuSizeDpiProfile(menuSize, dpi);
			if (menuSizeDpiProfile != null)
			{
				menuSize.OuterRadius = menuSizeDpiProfile.OuterRadius;
				menuSize.InnerRadius = menuSizeDpiProfile.InnerRadius;
				menuSize.FontSize = menuSizeDpiProfile.FontSize;
				menuSize.Scale = menuSizeDpiProfile.Scale;
			}
		}
	}

	internal static void UpsertMenuSizeDpiProfileFromBase(MenuSizeConfig menuSize, int dpi)
	{
		if (menuSize != null)
		{
			dpi = GetSafeDpiValue(dpi);
			if (menuSize.DpiProfiles == null)
			{
				menuSize.DpiProfiles = new List<MenuSizeDpiProfile>();
			}
			MenuSizeDpiProfile menuSizeDpiProfile = menuSize.DpiProfiles.FirstOrDefault([SpecialName] (MenuSizeDpiProfile x) => x != null && x.Dpi == dpi);
			if (menuSizeDpiProfile == null)
			{
				menuSizeDpiProfile = new MenuSizeDpiProfile
				{
					Dpi = dpi
				};
				menuSize.DpiProfiles.Add(menuSizeDpiProfile);
			}
			menuSizeDpiProfile.Dpi = dpi;
			menuSizeDpiProfile.OuterRadius = menuSize.OuterRadius;
			menuSizeDpiProfile.InnerRadius = menuSize.InnerRadius;
			menuSizeDpiProfile.FontSize = menuSize.FontSize;
			menuSizeDpiProfile.Scale = menuSize.Scale;
			NormalizeMenuSizeDpiProfiles(menuSize);
		}
	}

	private static MenuSizeDpiProfile FindBestMenuSizeDpiProfile(MenuSizeConfig menuSize, int dpi)
	{
		_Closure_0024__25_002D0 arg = default(_Closure_0024__25_002D0);
		_Closure_0024__25_002D0 CS_0024_003C_003E8__locals5 = new _Closure_0024__25_002D0(arg);
		CS_0024_003C_003E8__locals5._0024VB_0024Local_dpi = dpi;
		if (menuSize == null || menuSize.DpiProfiles == null || menuSize.DpiProfiles.Count == 0)
		{
			return null;
		}
		CS_0024_003C_003E8__locals5._0024VB_0024Local_dpi = GetSafeDpiValue(CS_0024_003C_003E8__locals5._0024VB_0024Local_dpi);
		MenuSizeDpiProfile menuSizeDpiProfile = menuSize.DpiProfiles.FirstOrDefault([SpecialName] (MenuSizeDpiProfile x) => x != null && x.Dpi == CS_0024_003C_003E8__locals5._0024VB_0024Local_dpi);
		if (menuSizeDpiProfile != null)
		{
			return menuSizeDpiProfile;
		}
		MenuSizeDpiProfile menuSizeDpiProfile2 = null;
		int num = int.MaxValue;
		foreach (MenuSizeDpiProfile dpiProfile in menuSize.DpiProfiles)
		{
			if (dpiProfile != null && dpiProfile.Dpi > 0)
			{
				int num2 = Math.Abs(checked(dpiProfile.Dpi - CS_0024_003C_003E8__locals5._0024VB_0024Local_dpi));
				if (num2 < num)
				{
					num = num2;
					menuSizeDpiProfile2 = dpiProfile;
				}
			}
		}
		if (menuSizeDpiProfile2 == null)
		{
			return null;
		}
		if (num <= 24)
		{
			return menuSizeDpiProfile2;
		}
		return null;
	}

	private static int HexNibble(char c)
	{
		checked
		{
			if (c >= '0' && c <= '9')
			{
				return c - 48;
			}
			char c2 = char.ToUpperInvariant(c);
			if (c2 >= 'A' && c2 <= 'F')
			{
				return 10 + (c2 - 65);
			}
			return -1;
		}
	}

	public RadialMenuController(RadialMenuConfigStore store, Control uiInvoker)
	{
		sectorCacheLock = RuntimeHelpers.GetObjectValue(new object());
		cachedGlobalSectors = new Dictionary<string, List<RadialSector>>(StringComparer.OrdinalIgnoreCase);
		cachedProcessSectors = new Dictionary<string, List<RadialSector>>(StringComparer.OrdinalIgnoreCase);
		sectorCacheGeneration = 0;
		lastForegroundHwnd = IntPtr.Zero;
		lastForegroundProcessName = "";
		foregroundWatchTimer = null;
		foregroundWatchTicksLeft = 0;
		foregroundWatchTag = "";
		foregroundWatchLock = RuntimeHelpers.GetObjectValue(new object());
		lastForegroundPid = 0;
		this.store = store;
		this.uiInvoker = uiInvoker;
		config = store.Load();
		ThreadPool.QueueUserWorkItem([SpecialName] (object a0) =>
		{
			_Lambda_0024__27_002D0();
		});
	}

	public void WarmUp(Action onCompleted = null)
	{
		_Closure_0024__28_002D0 arg = default(_Closure_0024__28_002D0);
		_Closure_0024__28_002D0 CS_0024_003C_003E8__locals18 = new _Closure_0024__28_002D0(arg);
		CS_0024_003C_003E8__locals18._0024VB_0024Me = this;
		CS_0024_003C_003E8__locals18._0024VB_0024Local_onCompleted = onCompleted;
		CS_0024_003C_003E8__locals18._0024VB_0024Local_pending = 2;
		CS_0024_003C_003E8__locals18._0024VB_0024Local_finished = 0;
		CS_0024_003C_003E8__locals18._0024VB_0024Local_signalDone = [SpecialName] () =>
		{
			try
			{
				if (Interlocked.Decrement(ref CS_0024_003C_003E8__locals18._0024VB_0024Local_pending) == 0 && CS_0024_003C_003E8__locals18._0024VB_0024Local_onCompleted != null && CS_0024_003C_003E8__locals18._0024VB_0024Me.uiInvoker != null && !CS_0024_003C_003E8__locals18._0024VB_0024Me.uiInvoker.IsDisposed && Interlocked.Exchange(ref CS_0024_003C_003E8__locals18._0024VB_0024Local_finished, 1) == 0)
				{
					CS_0024_003C_003E8__locals18._0024VB_0024Me.uiInvoker.BeginInvoke((Action)([SpecialName] () =>
					{
						try
						{
							CS_0024_003C_003E8__locals18._0024VB_0024Local_onCompleted();
						}
						catch (Exception projectError4)
						{
							ProjectData.SetProjectError(projectError4);
							ProjectData.ClearProjectError();
						}
					}));
				}
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		};
		try
		{
			ThreadPool.QueueUserWorkItem([SpecialName] (object a0) =>
			{
				CS_0024_003C_003E8__locals18._Lambda_0024__2();
			});
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			CS_0024_003C_003E8__locals18._0024VB_0024Local_signalDone();
			ProjectData.ClearProjectError();
		}
		try
		{
			if (uiInvoker == null || uiInvoker.IsDisposed)
			{
				CS_0024_003C_003E8__locals18._0024VB_0024Local_signalDone();
				return;
			}
			uiInvoker.BeginInvoke((Action)([SpecialName] () =>
			{
				try
				{
					CS_0024_003C_003E8__locals18._0024VB_0024Me.WarmUpForm();
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				finally
				{
					CS_0024_003C_003E8__locals18._0024VB_0024Local_signalDone();
				}
			}));
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			CS_0024_003C_003E8__locals18._0024VB_0024Local_signalDone();
			ProjectData.ClearProjectError();
		}
	}

	private void WarmUpForm()
	{
		if ((radialForm == null || radialForm.IsDisposed) && config != null)
		{
			Rectangle bounds = Screen.FromPoint(Cursor.Position).Bounds;
			RECT hostWindowRect = new RECT
			{
				Left = bounds.Left,
				Top = bounds.Top,
				Right = bounds.Right,
				Bottom = bounds.Bottom
			};
			List<RadialSector> globalSectors = GetCachedGlobalSectors("", isGlobalMenu: true);
			radialForm = new RadialSectorMenuForm(globalSectors, null, "", config.MenuSize, hostWindowRect, startGlobal: true, 2, warmupOnly: true);
			radialForm.ActionTriggered += OnActionTriggered;
			radialForm.MenuSizeChanged += SaveMenuSize;
			radialForm.RequestOpenSettings += OpenSettings;
			radialForm.RequestExitApp += [SpecialName] () =>
			{
				radialForm.Close();
			};
			radialForm.FormClosed += [SpecialName] (object a0, FormClosedEventArgs a1) =>
			{
				_Lambda_0024__29_002D1();
			};
			try
			{
				_ = radialForm.Handle;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			try
			{
				radialForm.WarmUpFastShow();
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				radialForm.PrimeForFastShow();
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		}
	}

	public void Reload()
	{
		config = store.Load();
		InvalidateSectorCaches();
	}

	public void ShowForForegroundProcess()
	{
		ShowForForegroundProcessAtPoint(Cursor.Position);
	}

	internal static string NormalizeProcessName(string processName)
	{
		string text = (processName ?? "").Trim();
		if (text.Length == 0)
		{
			return "";
		}
		try
		{
			if (text.IndexOf('\\') >= 0 || text.IndexOf('/') >= 0)
			{
				text = Path.GetFileName(text);
			}
			if (text.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
			{
				text = Path.GetFileNameWithoutExtension(text);
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return (text ?? "").Trim();
	}

	internal static List<string> SplitProcessAliases(string scopeKey)
	{
		string text = (scopeKey ?? "").Trim();
		if (text.Length == 0)
		{
			return new List<string>();
		}
		string[] array = text.Split(new char[3] { '|', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
		List<string> list = new List<string>();
		string[] array2 = array;
		_Closure_0024__33_002D0 closure_0024__33_002D = default(_Closure_0024__33_002D0);
		foreach (string processName in array2)
		{
			closure_0024__33_002D = new _Closure_0024__33_002D0(closure_0024__33_002D);
			closure_0024__33_002D._0024VB_0024Local_t = NormalizeProcessName(processName);
			if (closure_0024__33_002D._0024VB_0024Local_t.Length != 0 && !list.Any(closure_0024__33_002D._Lambda_0024__0))
			{
				list.Add(closure_0024__33_002D._0024VB_0024Local_t);
			}
		}
		return list;
	}

	internal static string NormalizeScopeKey(string scopeKey)
	{
		List<string> list = SplitProcessAliases(scopeKey);
		if (list == null || list.Count == 0)
		{
			return NormalizeProcessName(scopeKey);
		}
		list.Sort(StringComparer.OrdinalIgnoreCase);
		return string.Join("|", list);
	}

	internal static bool ScopeKeyMatchesProcess(string scopeKey, string processName)
	{
		string text = NormalizeProcessName(processName);
		if (text.Length == 0)
		{
			return false;
		}
		List<string> list = SplitProcessAliases(scopeKey);
		if (list == null || list.Count == 0)
		{
			return false;
		}
		return list.Any([SpecialName] (string t) => string.Equals(t, text, StringComparison.OrdinalIgnoreCase));
	}

	private void ShowForForegroundProcessAtPoint(Point showPos)
	{
		IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
		if (foregroundWindow == IntPtr.Zero || NativeMethods.IsIconic(foregroundWindow))
		{
			return;
		}
		string text = NormalizeProcessName(GetForegroundProcessName(foregroundWindow));
		if (config != null && config.UseCadInfiniteRadialMenu && !string.IsNullOrEmpty(text) && text.IndexOf("acad", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			try
			{
				Process.Start("quicker:runaction:CAD无限轮盘");
				return;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
				return;
			}
		}
		if (string.IsNullOrWhiteSpace(text))
		{
			ShowGlobalMenu(foregroundWindow, showPos);
			return;
		}
		ProcessMenuConfig processMenuConfig = EnsureProcessMenu(text);
		if (ProcessMenuHasUserContent(processMenuConfig.MenuItems))
		{
			ShowProcessMenu(foregroundWindow, text, processMenuConfig.MenuItems, showPos);
		}
		else
		{
			ShowGlobalMenuForScope(foregroundWindow, text, showPos);
		}
	}

	private void ShowGlobalMenuForScope(IntPtr fg, string scopeProcessName, Point showPos)
	{
		List<RadialSector> globalSectors = GetCachedGlobalSectors(scopeProcessName, isGlobalMenu: false);
		ShowRadialForm(fg, scopeProcessName, globalSectors, null, startGlobal: true, showPos);
	}

	private ProcessMenuConfig EnsureProcessMenu(string processName)
	{
		string text = NormalizeProcessName(processName);
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		ProcessMenuConfig processMenuConfig = FindProcessMenu(text);
		if (processMenuConfig != null)
		{
			if (config.IgnoredProcesses != null && config.IgnoredProcesses.Count > 0 && config.IgnoredProcesses.Any([SpecialName] (string s) => string.Equals(NormalizeProcessName(s), text, StringComparison.OrdinalIgnoreCase)))
			{
				config.IgnoredProcesses = config.IgnoredProcesses.Where([SpecialName] (string s) => !string.Equals(NormalizeProcessName(s), text, StringComparison.OrdinalIgnoreCase)).ToList();
				RadialMenuConfigStore.NormalizeConfig(config);
				store.Save(config);
			}
			string text2 = NormalizeScopeKey(processMenuConfig.ProcessName);
			if (text2.Length > 0 && !string.Equals(processMenuConfig.ProcessName, text2, StringComparison.OrdinalIgnoreCase))
			{
				processMenuConfig.ProcessName = text2;
				RadialMenuConfigStore.NormalizeConfig(config);
				store.Save(config);
			}
			return processMenuConfig;
		}
		processMenuConfig = new ProcessMenuConfig
		{
			ProcessName = text,
			MenuItems = RadialMenuConfigStore.CloneMenuItems(RadialMenuConfigStore.GetDefaultSeedMenuItems())
		};
		RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
		config.ProcessMenus.Add(processMenuConfig);
		RadialMenuConfigStore.NormalizeConfig(config);
		store.Save(config);
		return processMenuConfig;
	}

	internal static bool ProcessMenuHasUserContent(List<MenuItemConfig> items)
	{
		return items?.Any([SpecialName] (MenuItemConfig i) => MenuItemHasUserContent(i)) ?? false;
	}

	private static bool MenuItemHasUserContent(MenuItemConfig i)
	{
		if (i == null || !i.Enabled)
		{
			return false;
		}
		string text = (i.Command ?? "").Trim();
		if (string.Equals(text, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "_EXIT", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		if (i.IsSubMenu)
		{
			return true;
		}
		if (!string.IsNullOrWhiteSpace(text))
		{
			return true;
		}
		string a = (i.OperationType ?? "").Trim();
		if (string.Equals(a, "发送快捷键", StringComparison.OrdinalIgnoreCase))
		{
			return !string.IsNullOrWhiteSpace((i.Shortcut ?? "").Trim());
		}
		if (string.Equals(a, "键入文本", StringComparison.OrdinalIgnoreCase))
		{
			return !string.IsNullOrWhiteSpace(i.TypedText ?? "");
		}
		if (string.Equals(a, "粘贴文本", StringComparison.OrdinalIgnoreCase))
		{
			return !string.IsNullOrWhiteSpace(i.PasteText ?? "");
		}
		if (string.Equals(a, "打开或运行（文件/目录/命令/网址）", StringComparison.OrdinalIgnoreCase))
		{
			return !string.IsNullOrWhiteSpace((i.RunPath ?? "").Trim());
		}
		return false;
	}

	private void ShowProcessMenu(IntPtr fg, string processName, List<MenuItemConfig> items, Point showPos)
	{
		List<RadialSector> globalSectors = GetCachedGlobalSectors(processName, isGlobalMenu: false);
		List<RadialSector> processSectors = GetCachedProcessSectors(processName, items);
		processSectors = MergeProcessOuterRingWithGlobal(processSectors, globalSectors);
		ShowRadialForm(fg, processName, globalSectors, processSectors, startGlobal: false, showPos);
	}

	private static List<RadialSector> MergeProcessOuterRingWithGlobal(List<RadialSector> processSectors, List<RadialSector> globalSectors)
	{
		if (processSectors == null || processSectors.Count == 0)
		{
			return processSectors;
		}
		if (globalSectors == null || globalSectors.Count == 0)
		{
			return processSectors;
		}
		Dictionary<int, RadialSector> dictionary = new Dictionary<int, RadialSector>();
		foreach (RadialSector globalSector in globalSectors)
		{
			if (globalSector != null && globalSector.Level == 2 && string.Equals((globalSector.ParentMenuName ?? "").Trim(), "ROOT", StringComparison.OrdinalIgnoreCase) && globalSector.SectorIndex >= 0 && globalSector.SectorIndex < 8 && !IsSectorVisuallyEmpty(globalSector))
			{
				dictionary[globalSector.SectorIndex] = globalSector;
			}
		}
		if (dictionary.Count == 0)
		{
			return processSectors;
		}
		bool[] array = new bool[8];
		List<RadialSector> list = new List<RadialSector>(checked(processSectors.Count + 8));
		foreach (RadialSector processSector in processSectors)
		{
			if (processSector == null)
			{
				continue;
			}
			if (processSector.Level == 2 && string.Equals((processSector.ParentMenuName ?? "").Trim(), "ROOT", StringComparison.OrdinalIgnoreCase) && processSector.SectorIndex >= 0 && processSector.SectorIndex < 8)
			{
				array[processSector.SectorIndex] = true;
				if (IsSectorVisuallyEmpty(processSector) && dictionary.ContainsKey(processSector.SectorIndex))
				{
					list.Add(CloneSector(dictionary[processSector.SectorIndex]));
				}
				else
				{
					list.Add(processSector);
				}
			}
			else
			{
				list.Add(processSector);
			}
		}
		foreach (KeyValuePair<int, RadialSector> item in dictionary)
		{
			int key = item.Key;
			if (!array[key])
			{
				list.Add(CloneSector(item.Value));
			}
		}
		return list;
	}

	private static bool IsSectorVisuallyEmpty(RadialSector s)
	{
		if (s == null)
		{
			return true;
		}
		if (!s.Enabled)
		{
			return true;
		}
		if (s.IsSubMenu)
		{
			return false;
		}
		if (!string.IsNullOrWhiteSpace(s.Text ?? ""))
		{
			return false;
		}
		string text = (s.IconSpec ?? "").Trim();
		if (text.Length > 0)
		{
			return false;
		}
		if (s.UseFirstCharIcon)
		{
			return false;
		}
		if (s.OnlyShowIcon && (text.Length > 0 || s.UseFirstCharIcon))
		{
			return false;
		}
		return true;
	}

	private static RadialSector CloneSector(RadialSector s)
	{
		if (s == null)
		{
			return null;
		}
		return new RadialSector(s.Text, s.Command, s.FillColor, s.Action, s.Level, s.Icon, s.IsSubMenu, s.SubMenuName, s.ParentMenuName, s.FontSize, s.Page, s.SectorIndex, s.QuickerMenuItems, s.IconSpec, s.Enabled, s.UseFirstCharIcon, s.OnlyShowIcon);
	}

	private void ShowGlobalMenu(IntPtr fg, Point showPos)
	{
		List<RadialSector> globalSectors = GetCachedGlobalSectors("", isGlobalMenu: true);
		ShowRadialForm(fg, "", globalSectors, null, startGlobal: true, showPos);
	}

	private void ShowRadialForm(IntPtr fg, string scopeProcessName, List<RadialSector> globalSectors, List<RadialSector> processSectors, bool startGlobal, Point showPos)
	{
		RECT lpRect = default(RECT);
		RECT hostWindowRect;
		if (fg != IntPtr.Zero && NativeMethods.IsWindow(fg) && NativeMethods.GetWindowRect(fg, ref lpRect))
		{
			hostWindowRect = lpRect;
		}
		else
		{
			Rectangle bounds = Screen.FromPoint(showPos).Bounds;
			hostWindowRect = new RECT
			{
				Left = bounds.Left,
				Top = bounds.Top,
				Right = bounds.Right,
				Bottom = bounds.Bottom
			};
		}
		if (radialForm == null || radialForm.IsDisposed)
		{
			radialForm = new RadialSectorMenuForm(globalSectors, processSectors, scopeProcessName, config.MenuSize, hostWindowRect, startGlobal);
			radialForm.ActionTriggered += OnActionTriggered;
			radialForm.MenuSizeChanged += SaveMenuSize;
			radialForm.RequestOpenSettings += OpenSettings;
			radialForm.RequestExitApp += [SpecialName] () =>
			{
				radialForm.Close();
			};
			radialForm.RequestRestoreForeground += [SpecialName] () =>
			{
				RestoreForegroundWindowForInput();
			};
			radialForm.RequestStartForegroundWatch += [SpecialName] (string tag) =>
			{
				StartForegroundWatch(tag, 6);
			};
			radialForm.FormClosed += [SpecialName] (object a0, FormClosedEventArgs a1) =>
			{
				_Lambda_0024__46_002D3();
			};
		}
		radialForm.ResetForShow(globalSectors, processSectors, scopeProcessName, config.MenuSize, hostWindowRect, startGlobal, fg);
		string text = "";
		if (fg != IntPtr.Zero)
		{
			text = GetForegroundProcessName(fg);
		}
		bool flag = NativeMethods.IsDesktopManagerProcessName(text);
		Logger.Log("ShowRadialForm: fg=" + fg.ToString("X") + ", process=" + text + ", isDesktop=" + flag);
		Point point = showPos;
		Screen screen = Screen.FromPoint(point);
		int width = radialForm.Width;
		int height = radialForm.Height;
		checked
		{
			int x = Math.Max(screen.Bounds.Left, Math.Min(point.X - unchecked(width / 2), screen.Bounds.Right - width));
			int y = Math.Max(screen.Bounds.Top, Math.Min(point.Y - unchecked(height / 2), screen.Bounds.Bottom - height));
			radialForm.StartPosition = FormStartPosition.Manual;
			radialForm.Location = new Point(x, y);
			if (radialForm.IsTriggerButtonDown())
			{
				radialForm.SetInitialTriggerButtonState(state: true);
			}
			radialForm.SetInitialDragState(point);
			try
			{
				Logger.Log("ShowRadialForm: Show()");
				radialForm.Show();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Log("ShowRadialForm: Show failed: " + ex2.Message);
				radialForm.Show();
				ProjectData.ClearProjectError();
			}
			try
			{
				Logger.Log("ShowRadialForm: BumpToFrontNoActivate()");
				radialForm.BumpToFrontNoActivate();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Log("ShowRadialForm: BumpToFrontNoActivate failed: " + ex4.Message);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void Log(string msg)
	{
	}

	private void StartForegroundWatch(string tag, int seconds)
	{
		int num = Math.Max(1, seconds);
		object obj = foregroundWatchLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			foregroundWatchTag = (tag ?? "").Trim();
			foregroundWatchTicksLeft = num;
			try
			{
				if (foregroundWatchTimer != null)
				{
					try
					{
						foregroundWatchTimer.Dispose();
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					foregroundWatchTimer = null;
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				foregroundWatchTimer = new System.Threading.Timer(checked([SpecialName] (object state) =>
				{
					try
					{
						object obj2 = foregroundWatchLock;
						ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
						bool lockTaken2 = false;
						int num2;
						string text2;
						try
						{
							Monitor.Enter(obj2, ref lockTaken2);
							num2 = foregroundWatchTicksLeft;
							text2 = foregroundWatchTag;
							if (num2 <= 0)
							{
								return;
							}
							foregroundWatchTicksLeft = num2 - 1;
						}
						finally
						{
							if (lockTaken2)
							{
								Monitor.Exit(obj2);
							}
						}
						IntPtr zero2 = IntPtr.Zero;
						try
						{
							zero2 = NativeMethods.GetForegroundWindow();
						}
						catch (Exception projectError6)
						{
							ProjectData.SetProjectError(projectError6);
							zero2 = IntPtr.Zero;
							ProjectData.ClearProjectError();
						}
						string text3 = TryGetProcessNameFromWindow(zero2);
						Logger.Log("ForegroundWatch: tag=" + text2 + ", fg=" + zero2.ToString("X") + ", process=" + text3);
						if (num2 - 1 <= 0)
						{
							object obj3 = foregroundWatchLock;
							ObjectFlowControl.CheckForSyncLockOnValueType(obj3);
							bool lockTaken3 = false;
							try
							{
								Monitor.Enter(obj3, ref lockTaken3);
								try
								{
									if (foregroundWatchTimer != null)
									{
										foregroundWatchTimer.Dispose();
									}
								}
								catch (Exception projectError7)
								{
									ProjectData.SetProjectError(projectError7);
									ProjectData.ClearProjectError();
								}
								foregroundWatchTimer = null;
								return;
							}
							finally
							{
								if (lockTaken3)
								{
									Monitor.Exit(obj3);
								}
							}
						}
					}
					catch (Exception projectError8)
					{
						ProjectData.SetProjectError(projectError8);
						ProjectData.ClearProjectError();
					}
				}), null, 1000, 1000);
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
			try
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					zero = NativeMethods.GetForegroundWindow();
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					zero = IntPtr.Zero;
					ProjectData.ClearProjectError();
				}
				string text = TryGetProcessNameFromWindow(zero);
				Logger.Log("ForegroundWatchStart: tag=" + foregroundWatchTag + ", fg=" + zero.ToString("X") + ", process=" + text);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
	}

	private static string TryGetProcessNameFromWindow(IntPtr hwnd)
	{
		string result;
		if (hwnd == IntPtr.Zero)
		{
			result = "";
		}
		else
		{
			try
			{
				if (!NativeMethods.IsWindow(hwnd))
				{
					result = "";
					goto IL_00b5;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = "";
				ProjectData.ClearProjectError();
				goto IL_00b5;
			}
			int lpdwProcessId = 0;
			try
			{
				NativeMethods.GetWindowThreadProcessId(hwnd, ref lpdwProcessId);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				lpdwProcessId = 0;
				ProjectData.ClearProjectError();
			}
			if (lpdwProcessId <= 0)
			{
				result = "";
			}
			else
			{
				try
				{
					using Process process = Process.GetProcessById(lpdwProcessId);
					string text = process.ProcessName ?? "";
					result = ((!string.Equals(text, currentProcessName, StringComparison.OrdinalIgnoreCase)) ? text : "");
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					result = "";
					ProjectData.ClearProjectError();
				}
			}
		}
		goto IL_00b5;
		IL_00b5:
		return result;
	}

	private string GetForegroundProcessName(IntPtr fg)
	{
		string result;
		if (fg == IntPtr.Zero)
		{
			result = "";
		}
		else
		{
			int lpdwProcessId = 0;
			NativeMethods.GetWindowThreadProcessId(fg, ref lpdwProcessId);
			if (lpdwProcessId <= 0)
			{
				result = "";
			}
			else if (lpdwProcessId == lastForegroundPid && !string.IsNullOrEmpty(lastForegroundProcessName))
			{
				lastForegroundHwnd = fg;
				result = lastForegroundProcessName;
			}
			else
			{
				try
				{
					string text = "";
					using (Process process = Process.GetProcessById(lpdwProcessId))
					{
						if (process != null)
						{
							text = process.ProcessName;
							goto end_IL_0066;
						}
						result = "";
						goto end_IL_0059;
						end_IL_0066:;
					}
					if (string.Equals(text, Process.GetCurrentProcess().ProcessName, StringComparison.OrdinalIgnoreCase))
					{
						result = "";
					}
					else
					{
						lastForegroundHwnd = fg;
						lastForegroundPid = lpdwProcessId;
						lastForegroundProcessName = text;
						result = text;
					}
					end_IL_0059:;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					result = "";
					ProjectData.ClearProjectError();
				}
			}
		}
		return result;
	}

	private static bool IsCadLikeProcessName(string processName)
	{
		string text = (processName ?? "").Trim().ToLowerInvariant();
		if (text.Length == 0)
		{
			return false;
		}
		if (text.Contains("acad"))
		{
			return true;
		}
		if (text.Contains("zwcad"))
		{
			return true;
		}
		if (text.Contains("gstar"))
		{
			return true;
		}
		if (text.Contains("icad"))
		{
			return true;
		}
		return false;
	}

	private static string EscapeQuickerQueryValue(string value, bool keepPlus = false)
	{
		string text = value ?? "";
		if (text.Length == 0)
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(text.Length);
		string text2 = text;
		foreach (char c in text2)
		{
			if (c == ' ')
			{
				stringBuilder.Append("%20");
				continue;
			}
			if (Operators.CompareString(Conversions.ToString(c), "\r", TextCompare: false) == 0)
			{
				stringBuilder.Append("%0D");
				continue;
			}
			if (Operators.CompareString(Conversions.ToString(c), "\n", TextCompare: false) == 0)
			{
				stringBuilder.Append("%0A");
				continue;
			}
			switch (c)
			{
			case '&':
				stringBuilder.Append("%26");
				continue;
			case '=':
				stringBuilder.Append("%3D");
				continue;
			case '?':
				stringBuilder.Append("%3F");
				continue;
			case '#':
				stringBuilder.Append("%23");
				continue;
			case '%':
				stringBuilder.Append("%25");
				continue;
			case '+':
				if (!keepPlus)
				{
					stringBuilder.Append("%2B");
					continue;
				}
				break;
			}
			stringBuilder.Append(c);
		}
		return stringBuilder.ToString();
	}

	private static string BuildQuickerSubProgramSpec(string subProgramName, string text, bool keepPlus = false)
	{
		string text2 = subProgramName ?? "";
		string text3 = EscapeQuickerQueryValue(text ?? "", keepPlus);
		return "无限轮盘?sp=" + text2 + "&text=" + text3;
	}

	private static string BuildQuickerSubProgramSpecRaw(string subProgramName, string text)
	{
		string text2 = subProgramName ?? "";
		string text3 = text ?? "";
		return "无限轮盘?sp=" + text2 + "&text=" + text3;
	}

	private ProcessMenuConfig FindProcessMenu(string processName)
	{
		string processName2 = NormalizeProcessName(processName);
		return config.ProcessMenus.FirstOrDefault([SpecialName] (ProcessMenuConfig m) => m != null && ScopeKeyMatchesProcess(m.ProcessName, processName2));
	}

	public void OpenSettings(string scopeProcessName)
	{
		checked
		{
			try
			{
				string text = NormalizeProcessName(scopeProcessName);
				if (string.IsNullOrEmpty(text))
				{
					IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
					if (foregroundWindow != IntPtr.Zero)
					{
						text = NormalizeProcessName(GetForegroundProcessName(foregroundWindow));
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					EnsureProcessMenu(text);
				}
				if (settingsForm != null && !settingsForm.IsDisposed)
				{
					if (!string.IsNullOrEmpty(text))
					{
						settingsForm.SwitchScope(text);
					}
					try
					{
						if (settingsForm.WindowState == FormWindowState.Minimized)
						{
							settingsForm.WindowState = FormWindowState.Normal;
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					try
					{
						settingsForm.Show();
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						ProjectData.ClearProjectError();
					}
					try
					{
						settingsForm.TopMost = true;
						settingsForm.TopMost = false;
					}
					catch (Exception projectError3)
					{
						ProjectData.SetProjectError(projectError3);
						ProjectData.ClearProjectError();
					}
					settingsForm.BringToFront();
					settingsForm.Activate();
					try
					{
						settingsForm.Focus();
					}
					catch (Exception projectError4)
					{
						ProjectData.SetProjectError(projectError4);
						ProjectData.ClearProjectError();
					}
					try
					{
						NativeMethods.BringWindowToTop(settingsForm.Handle);
						NativeMethods.SetForegroundWindow(settingsForm.Handle);
						return;
					}
					catch (Exception projectError5)
					{
						ProjectData.SetProjectError(projectError5);
						ProjectData.ClearProjectError();
						return;
					}
				}
				RadialMenuAppConfig radialMenuAppConfig = CloneConfig(config);
				settingsForm = new RadialMenuSettingsForm(store, radialMenuAppConfig, text);
				settingsForm.ConfigSaved += [SpecialName] () =>
				{
					config = store.Load();
					InvalidateSectorCaches();
				};
				settingsForm.FormClosed += [SpecialName] (object a0, FormClosedEventArgs a1) =>
				{
					_Lambda_0024__58_002D1();
				};
				settingsForm.ShowInTaskbar = true;
				try
				{
					Rectangle workingArea;
					try
					{
						workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
					}
					catch (Exception projectError6)
					{
						ProjectData.SetProjectError(projectError6);
						workingArea = Screen.PrimaryScreen.WorkingArea;
						ProjectData.ClearProjectError();
					}
					settingsForm.StartPosition = FormStartPosition.Manual;
					settingsForm.Location = new Point(workingArea.Left + Math.Max(0, unchecked(checked(workingArea.Width - settingsForm.Width) / 2)), workingArea.Top + Math.Max(0, unchecked(checked(workingArea.Height - settingsForm.Height) / 2)));
				}
				catch (Exception projectError7)
				{
					ProjectData.SetProjectError(projectError7);
					ProjectData.ClearProjectError();
				}
				settingsForm.Show();
				try
				{
					settingsForm.TopMost = true;
					settingsForm.TopMost = false;
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					ProjectData.ClearProjectError();
				}
				settingsForm.BringToFront();
				settingsForm.Activate();
				try
				{
					settingsForm.Focus();
				}
				catch (Exception projectError9)
				{
					ProjectData.SetProjectError(projectError9);
					ProjectData.ClearProjectError();
				}
				try
				{
					NativeMethods.BringWindowToTop(settingsForm.Handle);
					NativeMethods.SetForegroundWindow(settingsForm.Handle);
				}
				catch (Exception projectError10)
				{
					ProjectData.SetProjectError(projectError10);
					ProjectData.ClearProjectError();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				MessageBox.Show("打开设置失败：" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				ProjectData.ClearProjectError();
			}
		}
	}

	private static RadialMenuAppConfig CloneConfig(RadialMenuAppConfig cfg)
	{
		RadialMenuAppConfig result;
		if (cfg == null)
		{
			result = null;
		}
		else
		{
			try
			{
				using MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(RadialMenuAppConfig));
				xmlSerializer.Serialize(memoryStream, cfg);
				memoryStream.Position = 0L;
				result = (RadialMenuAppConfig)xmlSerializer.Deserialize(memoryStream);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = cfg;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	public void Shutdown()
	{
		try
		{
			if (radialForm != null && !radialForm.IsDisposed)
			{
				radialForm.ForceClose();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (settingsForm != null && !settingsForm.IsDisposed)
			{
				settingsForm.Close();
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	private void InvalidateSectorCaches()
	{
		object obj = sectorCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		checked
		{
			try
			{
				Monitor.Enter(obj, ref lockTaken);
				cachedGlobalSectors.Clear();
				cachedProcessSectors.Clear();
				sectorCacheGeneration++;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(obj);
				}
			}
		}
	}

	private List<RadialSector> GetCachedGlobalSectors(string scopeProcessName, bool isGlobalMenu)
	{
		string key = (isGlobalMenu ? "G|" : "S|") + (scopeProcessName ?? "").Trim();
		List<RadialSector> value = null;
		int num = 0;
		object obj = sectorCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (cachedGlobalSectors.TryGetValue(key, out value))
			{
				return value;
			}
			num = sectorCacheGeneration;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
		List<RadialSector> list = BuildSectors(config.GlobalMenuItems, scopeProcessName, isGlobalMenu);
		object obj2 = sectorCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
		bool lockTaken2 = false;
		try
		{
			Monitor.Enter(obj2, ref lockTaken2);
			if (num == sectorCacheGeneration)
			{
				if (cachedGlobalSectors.TryGetValue(key, out value))
				{
					return value;
				}
				cachedGlobalSectors[key] = list;
				return list;
			}
			value = null;
			if (cachedGlobalSectors.TryGetValue(key, out value))
			{
				return value;
			}
		}
		finally
		{
			if (lockTaken2)
			{
				Monitor.Exit(obj2);
			}
		}
		return GetCachedGlobalSectors(scopeProcessName, isGlobalMenu);
	}

	private List<RadialSector> GetCachedProcessSectors(string processName, List<MenuItemConfig> items)
	{
		string text = NormalizeProcessName(processName);
		if (text.Length == 0)
		{
			return BuildSectors(items, "", isGlobalMenu: false);
		}
		List<RadialSector> value = null;
		int num = 0;
		object obj = sectorCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (cachedProcessSectors.TryGetValue(text, out value))
			{
				return value;
			}
			num = sectorCacheGeneration;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
		List<RadialSector> list = BuildSectors(items, text, isGlobalMenu: false);
		object obj2 = sectorCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj2);
		bool lockTaken2 = false;
		try
		{
			Monitor.Enter(obj2, ref lockTaken2);
			if (num == sectorCacheGeneration)
			{
				if (cachedProcessSectors.TryGetValue(text, out value))
				{
					return value;
				}
				cachedProcessSectors[text] = list;
				return list;
			}
			value = null;
			if (cachedProcessSectors.TryGetValue(text, out value))
			{
				return value;
			}
		}
		finally
		{
			if (lockTaken2)
			{
				Monitor.Exit(obj2);
			}
		}
		return GetCachedProcessSectors(processName, items);
	}

	private void ApplyHotkeyFromConfig()
	{
	}

	private void ApplyHoldTriggerFromConfig()
	{
	}

	private bool AreHoldModifiersPressed(int requiredMods)
	{
		return false;
	}

	private static IntPtr HoldMouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
	{
		return IntPtr.Zero;
	}

	private void OnActionTriggered(Action action)
	{
		if (action != null)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				MessageBox.Show("执行失败：" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				ProjectData.ClearProjectError();
			}
		}
	}

	internal static Image LoadIcon(string iconSpec)
	{
		string text = ResolveIconSpecToLocalFile(iconSpec);
		Image result;
		if (string.IsNullOrEmpty(text) || !File.Exists(text))
		{
			result = null;
		}
		else
		{
			try
			{
				using FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				using MemoryStream memoryStream = new MemoryStream();
				fileStream.CopyTo(memoryStream);
				memoryStream.Position = 0L;
				using Image original = Image.FromStream(memoryStream);
				result = new Bitmap(original);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = null;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	internal static Icon CreateTitleIcon(string text, int size = 32)
	{
		if (size <= 0)
		{
			size = 32;
		}
		string text2 = (text ?? "").Trim();
		if (text2.Length == 0)
		{
			text2 = "?";
		}
		string key = size + "|" + text2;
		object obj = iconCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (formIconCache.ContainsKey(key))
			{
				return formIconCache[key];
			}
			Image image = null;
			bool flag = false;
			if (string.Equals(text2, "轮盘", StringComparison.OrdinalIgnoreCase))
			{
				image = CreateWheelIcon(size);
				flag = true;
			}
			else
			{
				image = CreateLetterIcon(text2, size);
				flag = false;
			}
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(image, new Size(size, size));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				bitmap = new Bitmap(size, size);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.Clear(Color.Transparent);
					if (image != null)
					{
						graphics.DrawImage(image, new Rectangle(0, 0, size, size));
					}
				}
				ProjectData.ClearProjectError();
			}
			finally
			{
				if (flag && image != null)
				{
					try
					{
						image.Dispose();
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						ProjectData.ClearProjectError();
					}
				}
			}
			IntPtr hicon = bitmap.GetHicon();
			Icon icon = null;
			try
			{
				icon = (Icon)Icon.FromHandle(hicon).Clone();
			}
			finally
			{
				NativeMethods.DestroyIcon(hicon);
				try
				{
					bitmap.Dispose();
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
			}
			if (icon == null)
			{
				icon = SystemIcons.Application;
			}
			formIconCache[key] = icon;
			return icon;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
	}

	internal static Image CreateWheelIcon(int size = 64)
	{
		if (size <= 0)
		{
			size = 64;
		}
		Bitmap bitmap = new Bitmap(size, size);
		checked
		{
			using Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.Clear(Color.Transparent);
			Rectangle rect = new Rectangle(1, 1, size - 3, size - 3);
			using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, 50, 50)))
			{
				graphics.FillEllipse(brush, rect);
			}
			PointF pt = new PointF((float)size / 2f, (float)size / 2f);
			float num = (float)(size - 3) / 2f;
			int num2 = 8;
			float num3 = 360f / (float)num2;
			using (Pen pen = new Pen(Color.White, Math.Max(1f, (float)size / 32f)))
			{
				graphics.DrawEllipse(pen, rect);
				int num4 = num2 - 1;
				for (int i = 0; i <= num4; i++)
				{
					double num5 = (double)((float)i * num3) * Math.PI / 180.0;
					float x = pt.X + (float)(Math.Cos(num5) * (double)num);
					float y = pt.Y + (float)(Math.Sin(num5) * (double)num);
					graphics.DrawLine(pen, pt, new PointF(x, y));
				}
			}
			float num6 = num * 0.3f;
			RectangleF rect2 = new RectangleF(pt.X - num6, pt.Y - num6, num6 * 2f, num6 * 2f);
			using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(100, 149, 237)))
			{
				graphics.FillEllipse(brush2, rect2);
			}
			using Pen pen2 = new Pen(Color.White, Math.Max(1f, (float)size / 32f));
			graphics.DrawEllipse(pen2, rect2);
			return bitmap;
		}
	}

	internal static string ResolveIconSpecToLocalFile(string iconSpec)
	{
		if (string.IsNullOrWhiteSpace(iconSpec))
		{
			return "";
		}
		string text = iconSpec.Trim();
		if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
		{
			return EnsureUrlIconCached(text);
		}
		if (text.StartsWith("fa:", StringComparison.OrdinalIgnoreCase))
		{
			return "";
		}
		if (Path.IsPathRooted(text))
		{
			return text;
		}
		try
		{
			string text2 = "";
			try
			{
				text2 = Application.StartupPath;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				text2 = "";
				ProjectData.ClearProjectError();
			}
			if (string.IsNullOrWhiteSpace(text2))
			{
				text2 = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (!string.IsNullOrWhiteSpace(text2))
			{
				string text3 = Path.Combine(text2, text);
				if (File.Exists(text3))
				{
					return text3;
				}
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return text;
	}

	internal static Image GetScaledIcon(string iconSpec, int size = 24)
	{
		if (size <= 0)
		{
			size = 24;
		}
		string text = ResolveIconSpecToLocalFile(iconSpec);
		if (string.IsNullOrEmpty(text) || !File.Exists(text))
		{
			return null;
		}
		string key = size + "|" + text;
		object obj = iconCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (scaledIconCache.ContainsKey(key))
			{
				return scaledIconCache[key];
			}
			string text2 = "";
			try
			{
				text2 = Path.GetExtension(text).ToLowerInvariant();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				text2 = "";
				ProjectData.ClearProjectError();
			}
			Bitmap bitmap = null;
			try
			{
				switch (text2)
				{
				case ".ico":
				{
					using (Icon icon3 = new Icon(text, size, size))
					{
						bitmap = icon3.ToBitmap();
					}
					break;
				}
				case ".exe":
				case ".dll":
				{
					Icon icon = null;
					try
					{
						icon = Icon.ExtractAssociatedIcon(text);
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						icon = null;
						ProjectData.ClearProjectError();
					}
					if (icon == null)
					{
						break;
					}
					using (icon)
					{
						using Icon icon2 = new Icon(icon, size, size);
						bitmap = icon2.ToBitmap();
					}
					break;
				}
				}
				if (bitmap == null)
				{
					Image image = LoadIcon(text);
					if (image == null)
					{
						return null;
					}
					try
					{
						bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
						using Graphics graphics = Graphics.FromImage(bitmap);
						graphics.SmoothingMode = SmoothingMode.HighQuality;
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.CompositingQuality = CompositingQuality.HighQuality;
						graphics.Clear(Color.Transparent);
						graphics.DrawImage(image, new Rectangle(0, 0, size, size));
					}
					finally
					{
						try
						{
							image.Dispose();
						}
						catch (Exception projectError3)
						{
							ProjectData.SetProjectError(projectError3);
							ProjectData.ClearProjectError();
						}
					}
				}
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				if (bitmap != null)
				{
					try
					{
						bitmap.Dispose();
					}
					catch (Exception projectError5)
					{
						ProjectData.SetProjectError(projectError5);
						ProjectData.ClearProjectError();
					}
				}
				bitmap = null;
				ProjectData.ClearProjectError();
			}
			if (bitmap == null)
			{
				return null;
			}
			scaledIconCache[key] = bitmap;
			return bitmap;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
	}

	internal static Image CreateLetterIcon(string text, int size = 64)
	{
		if (size <= 0)
		{
			size = 64;
		}
		string text2 = (text ?? "").Trim();
		string text3 = "";
		checked
		{
			int num = text2.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				char c = text2[i];
				if (!char.IsWhiteSpace(c))
				{
					text3 = c.ToString();
					break;
				}
			}
			if (string.IsNullOrEmpty(text3))
			{
				text3 = "?";
			}
			string key = size + "|" + text3;
			object obj = iconCacheLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(obj, ref lockTaken);
				if (letterIconCache.ContainsKey(key))
				{
					return letterIconCache[key];
				}
				Bitmap bitmap = new Bitmap(size, size);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.Clear(Color.Transparent);
					using (SolidBrush brush = new SolidBrush(Color.FromArgb(235, 235, 235)))
					{
						graphics.FillEllipse(brush, 0, 0, size - 1, size - 1);
					}
					using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), Math.Max(1, unchecked(size / 32))))
					{
						graphics.DrawEllipse(pen, 0, 0, size - 1, size - 1);
					}
					float emSize = (float)Math.Max(10.0, (double)size * 0.5);
					using Font font = new Font("Microsoft YaHei", emSize, FontStyle.Bold, GraphicsUnit.Pixel);
					SizeF sizeF = graphics.MeasureString(text3, font);
					using SolidBrush brush2 = new SolidBrush(Color.FromArgb(80, 80, 80));
					graphics.DrawString(text3, font, brush2, ((float)size - sizeF.Width) / 2f, ((float)size - sizeF.Height) / 2f);
				}
				letterIconCache[key] = bitmap;
				return bitmap;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(obj);
				}
			}
		}
	}

	private static string GetIconCacheDir()
	{
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UniversalRadialMenu", "IconCache");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	private static string HashString(string value)
	{
		if (value == null)
		{
			value = "";
		}
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		using SHA1Managed sHA1Managed = new SHA1Managed();
		byte[] array = sHA1Managed.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder(checked(array.Length * 2));
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	private static string EnsureUrlIconCached(string url)
	{
		string iconCacheDir = GetIconCacheDir();
		string text = ".png";
		try
		{
			string extension = Path.GetExtension(new Uri(url).AbsolutePath);
			if (!string.IsNullOrEmpty(extension) && extension.Length <= 5)
			{
				text = extension;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		string text2 = Path.Combine(iconCacheDir, HashString(url) + text);
		if (File.Exists(text2))
		{
			return text2;
		}
		object obj = iconCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (File.Exists(text2))
			{
				return text2;
			}
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers[HttpRequestHeader.UserAgent] = "UniversalRadialMenu";
					webClient.DownloadFile(url, text2);
				}
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				try
				{
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
		return "";
	}

	private static string GetSkinCacheDir()
	{
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UniversalRadialMenu", "SkinCache");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	private static string EnsureUrlSkinCached(string url)
	{
		string skinCacheDir = GetSkinCacheDir();
		string text = ".png";
		try
		{
			string extension = Path.GetExtension(new Uri(url).AbsolutePath);
			if (!string.IsNullOrEmpty(extension) && extension.Length <= 5)
			{
				text = extension;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		string text2 = Path.Combine(skinCacheDir, HashString(url) + text);
		if (File.Exists(text2))
		{
			return text2;
		}
		object obj = skinCacheLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(obj, ref lockTaken);
			if (File.Exists(text2))
			{
				return text2;
			}
			string text3 = text2 + ".tmp";
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				try
				{
					if (File.Exists(text3))
					{
						File.Delete(text3);
					}
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers[HttpRequestHeader.UserAgent] = "UniversalRadialMenu";
					webClient.DownloadFile(url, text3);
				}
				if (File.Exists(text3))
				{
					try
					{
						if (File.Exists(text2))
						{
							File.Delete(text2);
						}
					}
					catch (Exception projectError4)
					{
						ProjectData.SetProjectError(projectError4);
						ProjectData.ClearProjectError();
					}
					try
					{
						File.Move(text3, text2);
					}
					catch (Exception projectError5)
					{
						ProjectData.SetProjectError(projectError5);
						try
						{
							File.Copy(text3, text2, overwrite: true);
						}
						catch (Exception projectError6)
						{
							ProjectData.SetProjectError(projectError6);
							ProjectData.ClearProjectError();
						}
						try
						{
							File.Delete(text3);
						}
						catch (Exception projectError7)
						{
							ProjectData.SetProjectError(projectError7);
							ProjectData.ClearProjectError();
						}
						ProjectData.ClearProjectError();
					}
				}
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			catch (Exception projectError8)
			{
				ProjectData.SetProjectError(projectError8);
				try
				{
					if (File.Exists(text3))
					{
						File.Delete(text3);
					}
				}
				catch (Exception projectError9)
				{
					ProjectData.SetProjectError(projectError9);
					ProjectData.ClearProjectError();
				}
				try
				{
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
				}
				catch (Exception projectError10)
				{
					ProjectData.SetProjectError(projectError10);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
		}
		return "";
	}

	internal static string ResolveSkinSpecToLocalFile(string spec)
	{
		if (string.IsNullOrWhiteSpace(spec))
		{
			return "";
		}
		string text = spec.Trim();
		if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
		{
			return EnsureUrlSkinCached(text);
		}
		if (Path.IsPathRooted(text))
		{
			return text;
		}
		try
		{
			string text2 = "";
			try
			{
				text2 = Application.StartupPath;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				text2 = "";
				ProjectData.ClearProjectError();
			}
			if (string.IsNullOrWhiteSpace(text2))
			{
				text2 = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (!string.IsNullOrWhiteSpace(text2))
			{
				string text3 = Path.Combine(text2, text);
				if (File.Exists(text3))
				{
					return text3;
				}
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return text;
	}

	private static string QuoteCommandLineArg(string arg)
	{
		if (arg == null)
		{
			arg = "";
		}
		if (arg.Length != 0 && !arg.Any([SpecialName] (char ch) => char.IsWhiteSpace(ch) || ch == '"'))
		{
			return arg;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('"');
		int num = 0;
		string text = arg;
		checked
		{
			foreach (char c in text)
			{
				switch (c)
				{
				case '\\':
					num++;
					continue;
				case '"':
					stringBuilder.Append(new string('\\', num * 2 + 1));
					stringBuilder.Append('"');
					num = 0;
					continue;
				}
				if (num > 0)
				{
					stringBuilder.Append(new string('\\', num));
					num = 0;
				}
				stringBuilder.Append(c);
			}
			if (num > 0)
			{
				stringBuilder.Append(new string('\\', num * 2));
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}
	}

	private static void RunQuickerRunAction(string runActionSpec)
	{
		if (!string.IsNullOrWhiteSpace(runActionSpec))
		{
			if (!File.Exists("C:\\Program Files\\Quicker\\QuickerStarter.exe"))
			{
				MessageBox.Show("未找到 QuickerStarter.exe：C:\\Program Files\\Quicker\\QuickerStarter.exe", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string fullArgs = "-c " + QuoteCommandLineArg("runaction:" + runActionSpec);
			Logger.Log("[QUICKER] 启动命令: QuickerStarter.exe " + fullArgs);
			Logger.Log("[QUICKER] 启动前 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X"));
			Process.Start(new ProcessStartInfo
			{
				FileName = "C:\\Program Files\\Quicker\\QuickerStarter.exe",
				Arguments = fullArgs,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false
			});
			Thread.Sleep(100);
			Logger.Log("[QUICKER] 启动后 foreground hwnd: " + NativeMethods.GetForegroundWindow().ToString("X"));
		}
	}

	private static string BuildQuickerBridgePayload(string raw)
	{
		if (raw == null)
		{
			raw = "";
		}
		if (raw.Contains("\r\n") || raw.Contains("\n"))
		{
			List<string> values = (from s in raw.Replace("\r\n", "\n").Split(new string[1] { "\n" }, StringSplitOptions.None)
				select s.Trim() into s
				where s.Length > 0
				select s).ToList();
			return string.Join("@@@", values);
		}
		return raw;
	}

	private static string BuildSendKeysShortcut(string shortcut)
	{
		string text = (shortcut ?? "").Trim();
		if (text.Length == 0)
		{
			return "";
		}
		List<string> list = (from s in text.Split(new char[1] { '+' }, StringSplitOptions.RemoveEmptyEntries)
			select s.Trim() into s
			where s.Length > 0
			select s).ToList();
		if (list.Count == 0)
		{
			return "";
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		string text4;
		string text5;
		Keys keys;
		checked
		{
			string text2 = list[list.Count - 1];
			int num = list.Count - 2;
			for (int num2 = 0; num2 <= num; num2++)
			{
				string text3 = list[num2];
				if (text3.Equals("Ctrl", StringComparison.OrdinalIgnoreCase) || text3.Equals("Control", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else if (text3.Equals("Alt", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				else if (text3.Equals("Shift", StringComparison.OrdinalIgnoreCase))
				{
					flag3 = true;
				}
			}
			text4 = "";
			if (flag)
			{
				text4 += "^";
			}
			if (flag2)
			{
				text4 += "%";
			}
			if (flag3)
			{
				text4 += "+";
			}
			text5 = (text2 ?? "").Trim();
			if (text5.Length == 0)
			{
				return "";
			}
			if (text5.Length == 1)
			{
				char c = text5[0];
				string text6 = (char.IsLetter(c) ? char.ToLowerInvariant(c).ToString() : ((!char.IsDigit(c)) ? EscapeSendKeysChar(c) : c.ToString()));
				if (text6.Length == 0)
				{
					return "";
				}
				return text4 + text6;
			}
			if (!flag && !flag2 && !flag3)
			{
				bool flag4 = true;
				string text7 = text5;
				foreach (char c2 in text7)
				{
					if (c2 < 'a' || c2 > 'z')
					{
						flag4 = false;
						break;
					}
				}
				if (flag4)
				{
					StringBuilder stringBuilder = new StringBuilder(text5.Length * 3);
					string text8 = text5;
					foreach (char c3 in text8)
					{
						stringBuilder.Append(char.ToLowerInvariant(c3));
					}
					string text9 = stringBuilder.ToString();
					if (text9.Length == 0)
					{
						return "";
					}
					return text4 + text9;
				}
			}
			string text10 = TryGetExplicitTokenName(text5);
			if (text10.Length > 0)
			{
				return text4 + "{" + text10 + "}";
			}
			keys = Keys.None;
		}
		try
		{
			object objectValue = RuntimeHelpers.GetObjectValue(new KeysConverter().ConvertFromString(NormalizeKeyNameForKeysConverter(text5)));
			if (objectValue != null)
			{
				keys = (Keys)Conversions.ToInteger(objectValue);
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			keys = Keys.None;
			ProjectData.ClearProjectError();
		}
		string text11 = "";
		if (keys >= Keys.A && keys <= Keys.Z)
		{
			text11 = Strings.ChrW((int)checked(97 + (keys - 65))).ToString();
		}
		else if (keys >= Keys.D0 && keys <= Keys.D9)
		{
			text11 = Strings.ChrW((int)checked(48 + (keys - 48))).ToString();
		}
		else if (keys >= Keys.F1 && keys <= Keys.F24)
		{
			text11 = "{F" + (int)checked(keys - 112 + 1) + "}";
		}
		else if (keys >= Keys.NumPad0 && keys <= Keys.NumPad9)
		{
			text11 = "{NUMPAD" + (int)checked(keys - 96) + "}";
		}
		else if (keys == Keys.Add)
		{
			text11 = "{ADD}";
		}
		else if (keys == Keys.Subtract)
		{
			text11 = "{SUBTRACT}";
		}
		else if (keys == Keys.Multiply)
		{
			text11 = "{MULTIPLY}";
		}
		else if (keys == Keys.Divide)
		{
			text11 = "{DIVIDE}";
		}
		else if (keys == Keys.Decimal)
		{
			text11 = "{DECIMAL}";
		}
		else if (keys == Keys.Return || keys == Keys.Return)
		{
			text11 = "{ENTER}";
		}
		else
		{
			switch (keys)
			{
			case Keys.Tab:
				text11 = "{TAB}";
				break;
			case Keys.Escape:
				text11 = "{ESC}";
				break;
			case Keys.Space:
				text11 = "{SPACE}";
				break;
			case Keys.Back:
				text11 = "{BACKSPACE}";
				break;
			case Keys.Delete:
				text11 = "{DELETE}";
				break;
			case Keys.Insert:
				text11 = "{INSERT}";
				break;
			case Keys.Home:
				text11 = "{HOME}";
				break;
			case Keys.End:
				text11 = "{END}";
				break;
			case Keys.Prior:
				text11 = "{PGUP}";
				break;
			case Keys.Next:
				text11 = "{PGDN}";
				break;
			case Keys.Left:
				text11 = "{LEFT}";
				break;
			case Keys.Right:
				text11 = "{RIGHT}";
				break;
			case Keys.Up:
				text11 = "{UP}";
				break;
			case Keys.Down:
				text11 = "{DOWN}";
				break;
			case Keys.Capital:
				text11 = "{CAPSLOCK}";
				break;
			case Keys.NumLock:
				text11 = "{NUMLOCK}";
				break;
			case Keys.Scroll:
				text11 = "{SCROLLLOCK}";
				break;
			case Keys.Snapshot:
				text11 = "{PRTSC}";
				break;
			case Keys.Pause:
				text11 = "{PAUSE}";
				break;
			case Keys.Apps:
				text11 = "{APPS}";
				break;
			default:
			{
				string text12 = KeysNameToTokenName(keys.ToString());
				if (text12.Length > 0)
				{
					text11 = "{" + text12 + "}";
				}
				break;
			}
			case Keys.None:
				break;
			}
		}
		if (text11.Length == 0)
		{
			if (flag || flag2 || flag3)
			{
				text11 = "{" + text5.ToUpperInvariant() + "}";
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder(checked(text5.Length * 3));
				string text13 = text5;
				foreach (char c4 in text13)
				{
					if (char.IsLetter(c4))
					{
						stringBuilder2.Append(char.ToLowerInvariant(c4));
					}
					else
					{
						stringBuilder2.Append(EscapeSendKeysChar(c4));
					}
				}
				text11 = stringBuilder2.ToString();
			}
		}
		if (text11.Length == 0)
		{
			return "";
		}
		return text4 + text11;
	}

	private static bool IsSubProgramSupportedMainKey(string mainKeyName)
	{
		string text = (mainKeyName ?? "").Trim();
		if (text.Length == 0)
		{
			return false;
		}
		string text2 = BuildSendKeysShortcut(text);
		if (string.IsNullOrWhiteSpace(text2))
		{
			return false;
		}
		checked
		{
			if (text2.Length >= 2 && text2[0] == '{' && text2[text2.Length - 1] == '}')
			{
				return IsTokenSupportedBySubProgram(text2.Substring(1, text2.Length - 2));
			}
			return true;
		}
	}

	private static bool IsTokenSupportedBySubProgram(string token)
	{
		string text = (token ?? "").Trim();
		if (text.Length == 0)
		{
			return false;
		}
		string text2 = text.ToUpperInvariant();
		switch (text2)
		{
		case "BACKSPACE":
		case "BREAK":
		case "CAPSLOCK":
		case "DELETE":
		case "DOWN":
		case "END":
		case "ENTER":
		case "ESC":
		case "HELP":
		case "HOME":
		case "INSERT":
		case "LEFT":
		case "NUMLOCK":
		case "PGDN":
		case "PGUP":
		case "PRTSC":
		case "RIGHT":
		case "SCROLLLOCK":
		case "TAB":
		case "UP":
			return true;
		default:
			if (text2.StartsWith("F", StringComparison.Ordinal) && text2.Length >= 2)
			{
				int result = 0;
				if (int.TryParse(text2.Substring(1), out result) && result >= 1 && result <= 16)
				{
					return true;
				}
			}
			switch (text2)
			{
			case "ADD":
			case "SUBTRACT":
			case "MULTIPLY":
			case "DIVIDE":
				return true;
			default:
				return false;
			}
		}
	}

	private static bool ShouldUseLocalSendForShortcut(string shortcut)
	{
		// 强制所有快捷键走本地 SendInput 路径
		// 根因：Quicker 子程序路径 (sp=发送快捷键) 在 Altium Designer 等应用中不生效
		// 原逻辑通过 QuickerStarter 回调 "无限轮盘" 动作自身的子程序发送按键，
		// 但该子程序执行上下文与独立 Quicker 动作不同，导致按键无法到达目标窗口
		return true;
	}

	private static void SendShortcutLocally(string shortcut)
	{
		Logger.Log("[LOCAL_SEND] 输入: " + shortcut);
		IntPtr hwnd = NativeMethods.GetForegroundWindow();
		if (hwnd == IntPtr.Zero)
		{
			Logger.Log("[LOCAL_SEND] 无前景窗口, 退出");
			return;
		}
		Logger.Log("[LOCAL_SEND] 目标 hwnd: " + hwnd.ToString("X"));

		string text = (shortcut ?? "").Trim();
		if (text.Length == 0) return;

		var parts = text.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
		if (parts.Count == 0) return;

		bool ctrl = false, alt = false, shift = false;
		string mainKeyName = parts[parts.Count - 1];
		for (int i = 0; i < parts.Count - 1; i++)
		{
			string mod = parts[i];
			if (mod.Equals("Ctrl", StringComparison.OrdinalIgnoreCase) || mod.Equals("Control", StringComparison.OrdinalIgnoreCase))
				ctrl = true;
			else if (mod.Equals("Alt", StringComparison.OrdinalIgnoreCase))
				alt = true;
			else if (mod.Equals("Shift", StringComparison.OrdinalIgnoreCase))
				shift = true;
		}

		byte mainVk = ResolveVkCode(mainKeyName);
		if (mainVk == 0)
		{
			Logger.Log("[LOCAL_SEND] 无法解析主键: " + mainKeyName);
			return;
		}
		Logger.Log("[LOCAL_SEND] 解析结果: ctrl=" + ctrl + " alt=" + alt + " shift=" + shift + " mainVk=0x" + mainVk.ToString("X2"));

		const uint WM_KEYDOWN = 0x0100;
		const uint WM_KEYUP = 0x0101;
		const int KEYEVENTF_KEYUP = 0x0002;

		try
		{
			// 第一步: 用 keybd_event 按下修饰键 (经过正常输入管线, 会更新系统键盘状态)
			// PostMessage 不更新 GetKeyState, 所以修饰键必须通过 keybd_event 发送
			if (ctrl)
			{
				byte scanCtrl = (byte)(NativeMethods.MapVirtualKey(0xA2, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA2, scanCtrl, 0, UIntPtr.Zero);
				Logger.Log("[LOCAL_SEND] keybd_event Ctrl DOWN");
			}
			if (alt)
			{
				byte scanAlt = (byte)(NativeMethods.MapVirtualKey(0xA4, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA4, scanAlt, 0, UIntPtr.Zero);
				Logger.Log("[LOCAL_SEND] keybd_event Alt DOWN");
			}
			if (shift)
			{
				byte scanShift = (byte)(NativeMethods.MapVirtualKey(0xA0, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA0, scanShift, 0, UIntPtr.Zero);
				Logger.Log("[LOCAL_SEND] keybd_event Shift DOWN");
			}

			// 第二步: 等待 AD 的消息循环处理修饰键事件, 更新其线程键盘状态
			if (ctrl || alt || shift)
			{
				Thread.Sleep(50);
				Logger.Log("[LOCAL_SEND] 修饰键等待 50ms 完成");
			}

			// 第三步: 用 PostMessage 发送主键 (直接投递到 AD 队列, 绕过所有 LL 钩子)
			PostKeyMsg(hwnd, WM_KEYDOWN, mainVk);
			PostKeyMsg(hwnd, WM_KEYUP,   mainVk);
			Logger.Log("[LOCAL_SEND] PostMessage 主键完成: vk=0x" + mainVk.ToString("X2"));

			// 第四步: 等待主键处理完毕后释放修饰键
			Thread.Sleep(30);

			if (shift)
			{
				byte scanShift = (byte)(NativeMethods.MapVirtualKey(0xA0, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA0, scanShift, KEYEVENTF_KEYUP, UIntPtr.Zero);
			}
			if (alt)
			{
				byte scanAlt = (byte)(NativeMethods.MapVirtualKey(0xA4, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA4, scanAlt, KEYEVENTF_KEYUP, UIntPtr.Zero);
			}
			if (ctrl)
			{
				byte scanCtrl = (byte)(NativeMethods.MapVirtualKey(0xA2, 0u) & 0xFF);
				NativeMethods.keybd_event(0xA2, scanCtrl, KEYEVENTF_KEYUP, UIntPtr.Zero);
				Logger.Log("[LOCAL_SEND] keybd_event Ctrl UP");
			}

			Logger.Log("[LOCAL_SEND] 混合发送全部完成");
		}
		catch (Exception ex)
		{
			Logger.Log("[LOCAL_SEND] 异常: " + ex.GetType().Name + " - " + ex.Message);
			// 确保修饰键被释放, 防止按键粘滞
			try
			{
				if (shift) NativeMethods.keybd_event(0xA0, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
				if (alt) NativeMethods.keybd_event(0xA4, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
				if (ctrl) NativeMethods.keybd_event(0xA2, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
			}
			catch { }
		}
	}

	private static void PostKeyMsg(IntPtr hwnd, uint msg, byte vk)
	{
		uint scanCode = NativeMethods.MapVirtualKey(vk, 0u);
		bool isUp = (msg == 0x0101u || msg == 0x0105u);

		int lParam = 1; // repeat count = 1
		lParam |= (int)((scanCode & 0xFF) << 16); // scan code at bits 16-23
		if (isUp)
		{
			lParam |= (1 << 30); // previous key state
			lParam |= unchecked((int)(1u << 31)); // transition state
		}

		bool ok = NativeMethods.PostMessage(hwnd, msg, (IntPtr)vk, (IntPtr)lParam);
		if (!ok)
		{
			int err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
			Logger.Log("[LOCAL_SEND] PostMessage 失败: vk=0x" + vk.ToString("X2") + " msg=0x" + msg.ToString("X4") + " err=" + err);
		}
	}

	private static byte ResolveVkCode(string keyName)
	{
		string k = (keyName ?? "").Trim();
		if (k.Length == 0) return 0;

		// 单字符: 字母/数字
		if (k.Length == 1)
		{
			char c = char.ToUpperInvariant(k[0]);
			if (c >= 'A' && c <= 'Z') return (byte)c;
			if (c >= '0' && c <= '9') return (byte)c;
		}

		// 功能键 F1-F24
		if (k.StartsWith("F", StringComparison.OrdinalIgnoreCase) && k.Length >= 2)
		{
			int fn;
			if (int.TryParse(k.Substring(1), out fn) && fn >= 1 && fn <= 24)
				return (byte)(0x6F + fn); // VK_F1=0x70
		}

		// 常用特殊键
		switch (k.ToUpperInvariant())
		{
			case "SPACE": return 0x20;
			case "ENTER": case "RETURN": return 0x0D;
			case "TAB": return 0x09;
			case "ESC": case "ESCAPE": return 0x1B;
			case "BACKSPACE": case "BKSP": case "BS": return 0x08;
			case "DELETE": case "DEL": return 0x2E;
			case "INSERT": case "INS": return 0x2D;
			case "HOME": return 0x24;
			case "END": return 0x23;
			case "PGUP": case "PAGEUP": return 0x21;
			case "PGDN": case "PAGEDOWN": return 0x22;
			case "UP": return 0x26;
			case "DOWN": return 0x28;
			case "LEFT": return 0x25;
			case "RIGHT": return 0x27;
			case "PRTSC": case "PRINTSCREEN": return 0x2C;
			case "BREAK": case "PAUSE": return 0x13;
			case "NUMLOCK": return 0x90;
			case "SCROLLLOCK": return 0x91;
			case "CAPSLOCK": return 0x14;
		}

		// 数字小键盘 Numpad0-Numpad9
		if (k.StartsWith("Numpad", StringComparison.OrdinalIgnoreCase) && k.Length >= 7)
		{
			int n;
			if (int.TryParse(k.Substring(6), out n) && n >= 0 && n <= 9)
				return (byte)(0x60 + n); // VK_NUMPAD0=0x60
		}

		// 尝试 Keys 枚举
		try
		{
			object obj = new System.Windows.Forms.KeysConverter().ConvertFromString(k);
			if (obj != null)
			{
				int val = (int)(System.Windows.Forms.Keys)obj;
				if (val > 0 && val <= 255) return (byte)val;
			}
		}
		catch { }

		return 0;
	}

	private static void SendTextLocally(string text)
	{
		Logger.Log("[LOCAL_TEXT] 输入: " + (text ?? "(null)"));
		string t = text ?? "";
		if (t.Length == 0) return;

		IntPtr hwnd = NativeMethods.GetForegroundWindow();
		if (hwnd == IntPtr.Zero)
		{
			Logger.Log("[LOCAL_TEXT] 无前景窗口, 退出");
			return;
		}

		int pid = 0;
		int adThreadId = NativeMethods.GetWindowThreadProcessId(hwnd, ref pid);
		Logger.Log("[LOCAL_TEXT] 目标 hwnd: " + hwnd.ToString("X") + ", pid: " + pid + ", threadId: " + adThreadId + ", 文本长度: " + t.Length);

		const uint WM_KEYDOWN = 0x0100;
		const uint WM_KEYUP = 0x0101;
		const uint WM_CHAR = 0x0102;
		const int KEYEVENTF_KEYUP = 0x0002;

		try
		{
			for (int i = 0; i < t.Length; i++)
			{
				char c = t[i];
				byte vk = CharToVk(c);

				if (i == 0)
				{
					// 第一个字符: 用 PostMessage 投递到 AD 主窗口 (绕过 LL 钩子链)
					if (vk != 0)
					{
						PostKeyMsg(hwnd, WM_KEYDOWN, vk);
						PostKeyMsg(hwnd, WM_KEYUP,   vk);
						Logger.Log("[LOCAL_TEXT] [0] PostMessage main hwnd char='" + c + "' vk=0x" + vk.ToString("X2"));
					}
					else
					{
						NativeMethods.PostMessage(hwnd, WM_CHAR, (IntPtr)c, IntPtr.Zero);
						Logger.Log("[LOCAL_TEXT] [0] WM_CHAR main hwnd char='" + c + "'");
					}

					if (t.Length > 1)
					{
						// 等待弹出菜单出现并进入模态循环
						Thread.Sleep(80);
					}
				}
				else
				{
					// 后续字符: 弹出菜单已打开并获取了键盘焦点
					// 使用 keybd_event → 走 Windows 输入管线 → 投递给当前焦点窗口(弹出菜单)
					// 裸字母键不会被 LL 钩子消耗 (仅组合键 Ctrl+X 等会被拦截)
					if (vk != 0)
					{
						byte scan = (byte)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFF);
						NativeMethods.keybd_event(vk, scan, 0, UIntPtr.Zero);
						NativeMethods.keybd_event(vk, scan, KEYEVENTF_KEYUP, UIntPtr.Zero);
						Logger.Log("[LOCAL_TEXT] [" + i + "] keybd_event char='" + c + "' vk=0x" + vk.ToString("X2") + " (follow focus)");
					}
					else
					{
						// 对无 VK 映射字符回退到 PostMessage WM_CHAR
						NativeMethods.PostMessage(hwnd, WM_CHAR, (IntPtr)c, IntPtr.Zero);
						Logger.Log("[LOCAL_TEXT] [" + i + "] WM_CHAR fallback char='" + c + "'");
					}

					if (i < t.Length - 1)
						Thread.Sleep(50);
				}
			}
			Logger.Log("[LOCAL_TEXT] 全部完成, 共 " + t.Length + " 字符");
		}
		catch (Exception ex)
		{
			Logger.Log("[LOCAL_TEXT] 异常: " + ex.GetType().Name + " - " + ex.Message);
		}
	}

	/// <summary>
	/// 诊断: 枚举所有 #32768 弹出菜单窗口, 记录其线程和进程信息
	/// </summary>
	private static void DiagLogPopupWindows(int targetPid, int targetThreadId)
	{
		int count = 0;
		IntPtr hSearch = IntPtr.Zero;
		for (int round = 0; round < 100; round++)
		{
			hSearch = NativeMethods.FindWindowEx(IntPtr.Zero, hSearch, "#32768", null);
			if (hSearch == IntPtr.Zero) break;
			int wpid = 0;
			int wtid = NativeMethods.GetWindowThreadProcessId(hSearch, ref wpid);
			bool match = (wpid == targetPid);
			Logger.Log("[DIAG] #32768 hwnd=" + hSearch.ToString("X") + " pid=" + wpid + " tid=" + wtid
				+ (match ? " ★MATCH★" : "") + (wtid == targetThreadId ? " (same thread)" : ""));
			count++;
		}
		// 也检查当前前景窗口
		IntPtr fg = NativeMethods.GetForegroundWindow();
		var sb = new System.Text.StringBuilder(260);
		NativeMethods.GetClassName(fg, sb, 260);
		Logger.Log("[DIAG] 共找到 " + count + " 个 #32768 窗口. 当前前景=" + fg.ToString("X") + " class=" + sb.ToString());
	}

	private static byte CharToVk(char c)
	{
		// 小写字母 → VK_A..VK_Z
		if (c >= 'a' && c <= 'z') return (byte)(c - 'a' + 0x41);
		// 大写字母 → VK_A..VK_Z (Shift 由调用者处理)
		if (c >= 'A' && c <= 'Z') return (byte)c;
		// 数字 0-9
		if (c >= '0' && c <= '9') return (byte)c;
		// 常用控制字符
		switch (c)
		{
			case ' ': return 0x20; // VK_SPACE
			case '\t': return 0x09; // VK_TAB
			case '\r': return 0x0D; // VK_RETURN
			case '\n': return 0x0D; // VK_RETURN
		}
		return 0;
	}

	// SendInput 辅助方法 — 64 位
	private static void AppendVkKey64Down(List<NativeMethods.INPUT64> list, byte vk)
	{
		ushort scan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		list.Add(new NativeMethods.INPUT64 { type = 1u, ki = new NativeMethods.KEYBDINPUT { wVk = vk, wScan = scan, dwFlags = 0u, time = 0u, dwExtraInfo = UIntPtr.Zero } });
	}

	private static void AppendVkKey64Up(List<NativeMethods.INPUT64> list, byte vk)
	{
		ushort scan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		list.Add(new NativeMethods.INPUT64 { type = 1u, ki = new NativeMethods.KEYBDINPUT { wVk = vk, wScan = scan, dwFlags = 2u, time = 0u, dwExtraInfo = UIntPtr.Zero } });
	}

	private static void AppendMouseClick64(List<NativeMethods.INPUT64> list, int downFlag, int upFlag, int xButton)
	{
		list.Add(new NativeMethods.INPUT64 { type = 0u, ki = default(NativeMethods.KEYBDINPUT) });
		list.Add(new NativeMethods.INPUT64 { type = 0u, ki = default(NativeMethods.KEYBDINPUT) });
	}

	// SendInput 辅助方法 — 32 位
	private static void AppendVkKey32Down(List<NativeMethods.INPUT32> list, byte vk)
	{
		ushort scan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		list.Add(new NativeMethods.INPUT32 { type = 1u, ki = new NativeMethods.KEYBDINPUT { wVk = vk, wScan = scan, dwFlags = 0u, time = 0u, dwExtraInfo = UIntPtr.Zero } });
	}

	private static void AppendVkKey32Up(List<NativeMethods.INPUT32> list, byte vk)
	{
		ushort scan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		list.Add(new NativeMethods.INPUT32 { type = 1u, ki = new NativeMethods.KEYBDINPUT { wVk = vk, wScan = scan, dwFlags = 2u, time = 0u, dwExtraInfo = UIntPtr.Zero } });
	}

	private static void AppendMouseClick32(List<NativeMethods.INPUT32> list, int downFlag, int upFlag, int xButton)
	{
		list.Add(new NativeMethods.INPUT32 { type = 0u, ki = default(NativeMethods.KEYBDINPUT) });
		list.Add(new NativeMethods.INPUT32 { type = 0u, ki = default(NativeMethods.KEYBDINPUT) });
	}

	private static string TryGetExplicitTokenName(string name)
	{
		string text = (name ?? "").Trim();
		if (text.Length == 0)
		{
			return "";
		}
		string text2 = text.Replace(' ', '_').Replace('-', '_').ToUpperInvariant();
		switch (text2)
		{
		case "BREAK":
			return "BREAK";
		case "PGUP":
			return "PGUP";
		case "PGDN":
			return "PGDN";
		case "INS":
			return "INSERT";
		case "DEL":
			return "DELETE";
		case "BKSP":
		case "BS":
			return "BACKSPACE";
		case "ESC":
			return "ESC";
		case "PRTSC":
			return "PRTSC";
		default:
			if (text2.StartsWith("F", StringComparison.Ordinal) && text2.Length >= 2)
			{
				int result = 0;
				if (int.TryParse(text2.Substring(1), out result) && result >= 1 && result <= 24)
				{
					return "F" + result;
				}
			}
			if (text2.StartsWith("NUMPAD", StringComparison.Ordinal) && text2.Length >= 7)
			{
				string s = text2.Substring(6);
				int result2 = 0;
				if (int.TryParse(s, out result2) && result2 >= 0 && result2 <= 9)
				{
					return "NUMPAD" + result2;
				}
			}
			if (text2.Contains("_"))
			{
				return text2;
			}
			switch (text2)
			{
			case "LBUTTON":
			case "RBUTTON":
			case "MBUTTON":
			case "XBUTTON1":
			case "XBUTTON2":
				return text2;
			default:
				return "";
			}
		}
	}

	private static string NormalizeKeyNameForKeysConverter(string name)
	{
		string text = (name ?? "").Trim();
		if (text.Length == 0)
		{
			return "";
		}
		string text2 = text.Replace(' ', '_').Replace('-', '_').ToUpperInvariant();
		switch (text2)
		{
		case "BREAK":
			return "Pause";
		case "PGUP":
			return "PageUp";
		case "PGDN":
			return "PageDown";
		case "INS":
			return "Insert";
		case "DEL":
			return "Delete";
		case "BKSP":
		case "BS":
			return "Back";
		case "ESC":
			return "Escape";
		case "PRTSC":
			return "PrintScreen";
		case "LBUTTON":
			return "LButton";
		case "RBUTTON":
			return "RButton";
		case "MBUTTON":
			return "MButton";
		case "XBUTTON1":
			return "XButton1";
		case "XBUTTON2":
			return "XButton2";
		case "LAUNCH_APP1":
			return "LaunchApplication1";
		case "LAUNCH_APP2":
			return "LaunchApplication2";
		case "LAUNCH_MAIL":
			return "LaunchMail";
		case "LAUNCH_MEDIA_SELECT":
			return "LaunchMediaSelect";
		default:
			if (text2.StartsWith("NUMPAD", StringComparison.Ordinal) && text2.Length >= 7)
			{
				string s = text2.Substring(6);
				int result = 0;
				if (int.TryParse(s, out result) && result >= 0 && result <= 9)
				{
					return "NumPad" + result;
				}
			}
			if (text2.Contains("_"))
			{
				string[] array = text2.Split(new char[1] { '_' }, StringSplitOptions.RemoveEmptyEntries);
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string text3 in array2)
				{
					if (text3.Length != 0)
					{
						string text4 = text3.ToLowerInvariant();
						stringBuilder.Append(char.ToUpperInvariant(text4[0]));
						if (text4.Length > 1)
						{
							stringBuilder.Append(text4.Substring(1));
						}
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}
	}

	private static string KeysNameToTokenName(string keysName)
	{
		string text = (keysName ?? "").Trim();
		if (text.Length == 0)
		{
			return "";
		}
		checked
		{
			switch (text)
			{
			case "LWin":
				return "LWIN";
			case "RWin":
				return "RWIN";
			case "LButton":
				return "LBUTTON";
			case "RButton":
				return "RBUTTON";
			case "MButton":
				return "MBUTTON";
			case "XButton1":
				return "XBUTTON1";
			case "XButton2":
				return "XBUTTON2";
			default:
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length + 8);
				int num = text.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					char c = text[i];
					if (i > 0 && char.IsUpper(c) && (char.IsLower(text[i - 1]) || char.IsDigit(text[i - 1])))
					{
						stringBuilder.Append('_');
					}
					stringBuilder.Append(char.ToUpperInvariant(c));
				}
				return stringBuilder.ToString();
			}
			}
		}
	}

	private static string EscapeSendKeysChar(char ch)
	{
		switch (ch)
		{
		case '+':
			return "{+}";
		case '^':
			return "{^}";
		case '%':
			return "{%}";
		case '~':
			return "{~}";
		case '(':
			return "{(}";
		case ')':
			return "{)}";
		case '{':
			return "{{}";
		case '}':
			return "{}}";
		case '[':
			return "{[}";
		case ']':
			return "{]}";
		case ' ':
			return "{SPACE}";
		case '\t':
			return "{TAB}";
		case '\n':
		case '\r':
			return "{ENTER}";
		default:
			return ch.ToString();
		}
	}

	private static void SendShortcutBySendKeys(string shortcut)
	{
		string text = BuildSendKeysShortcut(shortcut);
		if (!string.IsNullOrWhiteSpace(text))
		{
			try
			{
				SendKeys.SendWait(text);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private static void SendTextBySendKeys(string text)
	{
		string text2 = text ?? "";
		if (text2.Length == 0)
		{
			return;
		}
		string text3 = text2;
		for (int i = 0; i < text3.Length; i = checked(i + 1))
		{
			string text4 = EscapeSendKeysChar(text3[i]);
			if (text4.Length != 0)
			{
				try
				{
					SendKeys.SendWait(text4);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				Thread.Sleep(8);
			}
		}
	}

	private static bool TrySendTextBySendInputMixed(string text)
	{
		string text2 = text ?? "";
		bool result;
		if (text2.Length == 0)
		{
			result = true;
		}
		else
		{
			try
			{
				result = ((IntPtr.Size != 8) ? SendKeyboardInputs32(BuildMixedTextInputs32(text2)) : SendKeyboardInputs64(BuildMixedTextInputs64(text2)));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private static NativeMethods.INPUT64[] BuildMixedTextInputs64(string text)
	{
		string text2 = text ?? "";
		uint dwFlags = 4u;
		uint dwFlags2 = 6u;
		uint flagsVkUp = 2u;
		checked
		{
			List<NativeMethods.INPUT64> list = new List<NativeMethods.INPUT64>(Math.Max(4, text2.Length * 2));
			int num = 0;
			while (num < text2.Length)
			{
				char c = text2[num];
				switch (c)
				{
				case '\r':
					if (num + 1 < text2.Length && text2[num + 1] == '\n')
					{
						num++;
					}
					AppendScanKey64(list, 13, flagsVkUp);
					num++;
					continue;
				case '\n':
					AppendScanKey64(list, 13, flagsVkUp);
					num++;
					continue;
				case '\t':
					AppendScanKey64(list, 9, flagsVkUp);
					num++;
					continue;
				case ' ':
					if (preferVkSpaceForCurrentSend)
					{
						AppendScanKey64(list, 32, flagsVkUp);
						num++;
						continue;
					}
					break;
				}
				ushort wScan = (ushort)unchecked((int)c);
				list.Add(new NativeMethods.INPUT64
				{
					type = 1u,
					ki = new NativeMethods.KEYBDINPUT
					{
						wVk = 0,
						wScan = wScan,
						dwFlags = dwFlags,
						time = 0u,
						dwExtraInfo = UIntPtr.Zero
					}
				});
				list.Add(new NativeMethods.INPUT64
				{
					type = 1u,
					ki = new NativeMethods.KEYBDINPUT
					{
						wVk = 0,
						wScan = wScan,
						dwFlags = dwFlags2,
						time = 0u,
						dwExtraInfo = UIntPtr.Zero
					}
				});
				num++;
			}
			return list.ToArray();
		}
	}

	private static void AppendVkKey64(List<NativeMethods.INPUT64> list, byte vk, uint flagsVkUp)
	{
		list.Add(new NativeMethods.INPUT64
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = vk,
				wScan = 0,
				dwFlags = 0u,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
		list.Add(new NativeMethods.INPUT64
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = vk,
				wScan = 0,
				dwFlags = flagsVkUp,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
	}

	private static void AppendScanKey64(List<NativeMethods.INPUT64> list, byte vk, uint flagsVkUp)
	{
		ushort wScan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		uint dwFlags = 8u;
		uint dwFlags2 = 10u;
		list.Add(new NativeMethods.INPUT64
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = 0,
				wScan = wScan,
				dwFlags = dwFlags,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
		list.Add(new NativeMethods.INPUT64
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = 0,
				wScan = wScan,
				dwFlags = dwFlags2,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
	}

	private static NativeMethods.INPUT32[] BuildMixedTextInputs32(string text)
	{
		string text2 = text ?? "";
		uint dwFlags = 4u;
		uint dwFlags2 = 6u;
		uint flagsVkUp = 2u;
		checked
		{
			List<NativeMethods.INPUT32> list = new List<NativeMethods.INPUT32>(Math.Max(4, text2.Length * 2));
			int num = 0;
			while (num < text2.Length)
			{
				char c = text2[num];
				switch (c)
				{
				case '\r':
					if (num + 1 < text2.Length && text2[num + 1] == '\n')
					{
						num++;
					}
					AppendScanKey32(list, 13, flagsVkUp);
					num++;
					continue;
				case '\n':
					AppendScanKey32(list, 13, flagsVkUp);
					num++;
					continue;
				case '\t':
					AppendScanKey32(list, 9, flagsVkUp);
					num++;
					continue;
				case ' ':
					if (preferVkSpaceForCurrentSend)
					{
						AppendScanKey32(list, 32, flagsVkUp);
						num++;
						continue;
					}
					break;
				}
				ushort wScan = (ushort)unchecked((int)c);
				list.Add(new NativeMethods.INPUT32
				{
					type = 1u,
					ki = new NativeMethods.KEYBDINPUT
					{
						wVk = 0,
						wScan = wScan,
						dwFlags = dwFlags,
						time = 0u,
						dwExtraInfo = UIntPtr.Zero
					}
				});
				list.Add(new NativeMethods.INPUT32
				{
					type = 1u,
					ki = new NativeMethods.KEYBDINPUT
					{
						wVk = 0,
						wScan = wScan,
						dwFlags = dwFlags2,
						time = 0u,
						dwExtraInfo = UIntPtr.Zero
					}
				});
				num++;
			}
			return list.ToArray();
		}
	}

	private static void AppendVkKey32(List<NativeMethods.INPUT32> list, byte vk, uint flagsVkUp)
	{
		list.Add(new NativeMethods.INPUT32
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = vk,
				wScan = 0,
				dwFlags = 0u,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
		list.Add(new NativeMethods.INPUT32
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = vk,
				wScan = 0,
				dwFlags = flagsVkUp,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
	}

	private static void AppendScanKey32(List<NativeMethods.INPUT32> list, byte vk, uint flagsVkUp)
	{
		ushort wScan = checked((ushort)(NativeMethods.MapVirtualKey(vk, 0u) & 0xFFFF));
		uint dwFlags = 8u;
		uint dwFlags2 = 10u;
		list.Add(new NativeMethods.INPUT32
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = 0,
				wScan = wScan,
				dwFlags = dwFlags,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
		list.Add(new NativeMethods.INPUT32
		{
			type = 1u,
			ki = new NativeMethods.KEYBDINPUT
			{
				wVk = 0,
				wScan = wScan,
				dwFlags = dwFlags2,
				time = 0u,
				dwExtraInfo = UIntPtr.Zero
			}
		});
	}

	private static bool SendKeyboardInputs64(NativeMethods.INPUT64[] inputs)
	{
		checked
		{
			bool result;
			try
			{
				if (inputs == null || inputs.Length == 0)
				{
					result = true;
				}
				else
				{
					int num = Marshal.SizeOf(typeof(NativeMethods.INPUT64));
					Logger.Log("[SendInput64] cbSize=" + num + " count=" + inputs.Length);
					IntPtr intPtr = Marshal.AllocHGlobal(num * inputs.Length);
					try
					{
						int num2 = inputs.Length - 1;
						for (int i = 0; i <= num2; i++)
						{
							IntPtr ptr = IntPtr.Add(intPtr, i * num);
							Marshal.StructureToPtr(inputs[i], ptr, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)inputs.Length, intPtr, num) == (uint)inputs.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	private static bool SendKeyboardInputs32(NativeMethods.INPUT32[] inputs)
	{
		checked
		{
			bool result;
			try
			{
				if (inputs == null || inputs.Length == 0)
				{
					result = true;
				}
				else
				{
					int num = Marshal.SizeOf(typeof(NativeMethods.INPUT32));
					IntPtr intPtr = Marshal.AllocHGlobal(num * inputs.Length);
					try
					{
						int num2 = inputs.Length - 1;
						for (int i = 0; i <= num2; i++)
						{
							IntPtr ptr = IntPtr.Add(intPtr, i * num);
							Marshal.StructureToPtr(inputs[i], ptr, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)inputs.Length, intPtr, num) == (uint)inputs.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	private static void PasteTextByClipboard(string text)
	{
		string text2 = text ?? "";
		if (text2.Length == 0)
		{
			return;
		}
		IDataObject dataObject = null;
		try
		{
			dataObject = Clipboard.GetDataObject();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			dataObject = null;
			ProjectData.ClearProjectError();
		}
		if (!TrySetClipboardTextWithRetries(text2, 6, 20))
		{
			return;
		}
		try
		{
			Thread.Sleep(70);
			if (preferShiftInsertPasteForCurrentSend)
			{
				if (!TrySendShiftInsertBySendInput())
				{
					NativeMethods.keybd_event(16, 0, 0, UIntPtr.Zero);
					NativeMethods.keybd_event(45, 0, 0, UIntPtr.Zero);
					NativeMethods.keybd_event(45, 0, 2, UIntPtr.Zero);
					NativeMethods.keybd_event(16, 0, 2, UIntPtr.Zero);
				}
			}
			else if (!TrySendCtrlVBySendInput())
			{
				NativeMethods.keybd_event(17, 0, 0, UIntPtr.Zero);
				NativeMethods.keybd_event(86, 0, 0, UIntPtr.Zero);
				NativeMethods.keybd_event(86, 0, 2, UIntPtr.Zero);
				NativeMethods.keybd_event(17, 0, 2, UIntPtr.Zero);
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		if (dataObject != null)
		{
			try
			{
				Thread.Sleep(350);
				Clipboard.SetDataObject(dataObject, copy: true);
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		}
	}

	private static bool TrySetClipboardTextWithRetries(string text, int retries, int sleepMs)
	{
		string text2 = text ?? "";
		if (text2.Length == 0)
		{
			return false;
		}
		bool flag = false;
		try
		{
			flag = Thread.CurrentThread.GetApartmentState() != ApartmentState.STA;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			flag = false;
			ProjectData.ClearProjectError();
		}
		if (flag)
		{
			return TrySetClipboardTextOnStaThread(text2, retries, sleepMs);
		}
		int num = Math.Max(1, retries);
		for (int i = 1; i <= num; i = checked(i + 1))
		{
			try
			{
				Clipboard.SetText(text2);
				return true;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				if (sleepMs > 0)
				{
					Thread.Sleep(sleepMs);
				}
				ProjectData.ClearProjectError();
			}
		}
		return false;
	}

	private static bool TrySetClipboardTextOnStaThread(string text, int retries, int sleepMs)
	{
		_Closure_0024__114_002D0 arg = default(_Closure_0024__114_002D0);
		_Closure_0024__114_002D0 CS_0024_003C_003E8__locals10 = new _Closure_0024__114_002D0(arg);
		CS_0024_003C_003E8__locals10._0024VB_0024Local_text = text;
		CS_0024_003C_003E8__locals10._0024VB_0024Local_retries = retries;
		CS_0024_003C_003E8__locals10._0024VB_0024Local_sleepMs = sleepMs;
		CS_0024_003C_003E8__locals10._0024VB_0024Local_ok = false;
		Thread thread = new Thread([SpecialName] () =>
		{
			int num = Math.Max(1, CS_0024_003C_003E8__locals10._0024VB_0024Local_retries);
			for (int i = 1; i <= num; i = checked(i + 1))
			{
				try
				{
					Clipboard.SetText(CS_0024_003C_003E8__locals10._0024VB_0024Local_text);
					CS_0024_003C_003E8__locals10._0024VB_0024Local_ok = true;
					break;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					if (CS_0024_003C_003E8__locals10._0024VB_0024Local_sleepMs > 0)
					{
						Thread.Sleep(CS_0024_003C_003E8__locals10._0024VB_0024Local_sleepMs);
					}
					ProjectData.ClearProjectError();
				}
			}
		});
		thread.IsBackground = true;
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
		thread.Join(1000);
		return CS_0024_003C_003E8__locals10._0024VB_0024Local_ok;
	}

	private static bool TrySendCtrlVBySendInput()
	{
		checked
		{
			bool result;
			try
			{
				if (IntPtr.Size == 8)
				{
					NativeMethods.INPUT64[] array = BuildCtrlVInputs64();
					int num = Marshal.SizeOf(typeof(NativeMethods.INPUT64));
					IntPtr intPtr = Marshal.AllocHGlobal(num * array.Length);
					try
					{
						int num2 = array.Length - 1;
						for (int i = 0; i <= num2; i++)
						{
							IntPtr ptr = IntPtr.Add(intPtr, i * num);
							Marshal.StructureToPtr(array[i], ptr, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)array.Length, intPtr, num) == (uint)array.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
				else
				{
					NativeMethods.INPUT32[] array2 = BuildCtrlVInputs32();
					int num3 = Marshal.SizeOf(typeof(NativeMethods.INPUT32));
					IntPtr intPtr2 = Marshal.AllocHGlobal(num3 * array2.Length);
					try
					{
						int num4 = array2.Length - 1;
						for (int j = 0; j <= num4; j++)
						{
							IntPtr ptr2 = IntPtr.Add(intPtr2, j * num3);
							Marshal.StructureToPtr(array2[j], ptr2, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)array2.Length, intPtr2, num3) == (uint)array2.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	private static bool TrySendShiftInsertBySendInput()
	{
		checked
		{
			bool result;
			try
			{
				if (IntPtr.Size == 8)
				{
					NativeMethods.INPUT64[] array = BuildShiftInsertInputs64();
					int num = Marshal.SizeOf(typeof(NativeMethods.INPUT64));
					IntPtr intPtr = Marshal.AllocHGlobal(num * array.Length);
					try
					{
						int num2 = array.Length - 1;
						for (int i = 0; i <= num2; i++)
						{
							IntPtr ptr = IntPtr.Add(intPtr, i * num);
							Marshal.StructureToPtr(array[i], ptr, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)array.Length, intPtr, num) == (uint)array.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
				else
				{
					NativeMethods.INPUT32[] array2 = BuildShiftInsertInputs32();
					int num3 = Marshal.SizeOf(typeof(NativeMethods.INPUT32));
					IntPtr intPtr2 = Marshal.AllocHGlobal(num3 * array2.Length);
					try
					{
						int num4 = array2.Length - 1;
						for (int j = 0; j <= num4; j++)
						{
							IntPtr ptr2 = IntPtr.Add(intPtr2, j * num3);
							Marshal.StructureToPtr(array2[j], ptr2, fDeleteOld: false);
						}
						result = NativeMethods.SendInputPtr((uint)array2.Length, intPtr2, num3) == (uint)array2.Length;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	private static NativeMethods.INPUT64[] BuildShiftInsertInputs64()
	{
		uint dwFlags = 2u;
		return new NativeMethods.INPUT64[4]
		{
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 16,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 45,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 45,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 16,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			}
		};
	}

	private static NativeMethods.INPUT32[] BuildShiftInsertInputs32()
	{
		uint dwFlags = 2u;
		return new NativeMethods.INPUT32[4]
		{
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 16,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 45,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 45,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 16,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			}
		};
	}

	private static void SendBrowserBackByKeybdEvent()
	{
		try
		{
			NativeMethods.keybd_event(166, 0, 0, UIntPtr.Zero);
			Thread.Sleep(40);
			NativeMethods.keybd_event(166, 0, 2, UIntPtr.Zero);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private static NativeMethods.INPUT64[] BuildCtrlVInputs64()
	{
		uint dwFlags = 2u;
		return new NativeMethods.INPUT64[4]
		{
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 17,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 86,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 86,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT64
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 17,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			}
		};
	}

	private static NativeMethods.INPUT32[] BuildCtrlVInputs32()
	{
		uint dwFlags = 2u;
		return new NativeMethods.INPUT32[4]
		{
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 17,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 86,
					wScan = 0,
					dwFlags = 0u,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 86,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			},
			new NativeMethods.INPUT32
			{
				type = 1u,
				ki = new NativeMethods.KEYBDINPUT
				{
					wVk = 17,
					wScan = 0,
					dwFlags = dwFlags,
					time = 0u,
					dwExtraInfo = UIntPtr.Zero
				}
			}
		};
	}

	private static void RunOpenOrCmd(string runPathOrUrlOrCmd, string runParams)
	{
		string text = (runPathOrUrlOrCmd ?? "").Trim();
		string text2 = runParams ?? "";
		text2 = text2.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ")
			.Trim();
		if (text.Length == 0)
		{
			return;
		}
		if (text2.Length == 0)
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = text,
					UseShellExecute = true
				});
				return;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
				return;
			}
		}
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = "/c start \"\" " + QuoteCommandLineArg(text) + " " + text2,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false
			});
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	private List<RadialSector> BuildSectors(List<MenuItemConfig> items, string scopeProcessName, bool isGlobalMenu)
	{
		List<RadialSector> list = new List<RadialSector>();
		if (items == null)
		{
			return list;
		}
		_Closure_0024__123_002D0 closure_0024__123_002D = default(_Closure_0024__123_002D0);
		_Closure_0024__123_002D1 closure_0024__123_002D2 = default(_Closure_0024__123_002D1);
		_Closure_0024__123_002D2 closure_0024__123_002D3 = default(_Closure_0024__123_002D2);
		_Closure_0024__123_002D3 closure_0024__123_002D4 = default(_Closure_0024__123_002D3);
		_Closure_0024__123_002D4 closure_0024__123_002D5 = default(_Closure_0024__123_002D4);
		_Closure_0024__123_002D5 closure_0024__123_002D6 = default(_Closure_0024__123_002D5);
		_Closure_0024__123_002D6 closure_0024__123_002D7 = default(_Closure_0024__123_002D6);
		_Closure_0024__123_002D7 closure_0024__123_002D8 = default(_Closure_0024__123_002D7);
		_Closure_0024__123_002D8 closure_0024__123_002D9 = default(_Closure_0024__123_002D8);
		foreach (MenuItemConfig item in items)
		{
			if (item == null || !item.Enabled)
			{
				continue;
			}
			string text = ((!string.IsNullOrWhiteSpace(item.DisplayName)) ? item.DisplayName : item.Text);
			string text2 = (item.IconPath ?? "").Trim();
			bool num = string.Equals(item.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(item.Command, "_EXIT", StringComparison.OrdinalIgnoreCase);
			bool useFirstCharIcon = item.UseFirstCharIcon;
			if (num && text2.Length == 0)
			{
				useFirstCharIcon = false;
			}
			string text3 = (item.Command ?? "").Trim();
			string a = (item.OperationType ?? "").Trim();
			Action action = null;
			if (item.IsSubMenu && !string.IsNullOrEmpty(item.SubMenuName))
			{
				closure_0024__123_002D = new _Closure_0024__123_002D0(closure_0024__123_002D);
				closure_0024__123_002D._0024VB_0024Me = this;
				closure_0024__123_002D._0024VB_0024Local_target = item.SubMenuName;
				closure_0024__123_002D._0024VB_0024Local_parentText = text;
				action = closure_0024__123_002D._Lambda_0024__0;
			}
			else if (string.Equals(text3, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
			{
				closure_0024__123_002D2 = new _Closure_0024__123_002D1(closure_0024__123_002D2);
				closure_0024__123_002D2._0024VB_0024Me = this;
				closure_0024__123_002D2._0024VB_0024Local_scope = (isGlobalMenu ? "" : scopeProcessName);
				action = closure_0024__123_002D2._Lambda_0024__1;
			}
			else if (string.Equals(text3, "_EXIT", StringComparison.OrdinalIgnoreCase))
			{
				action = [SpecialName] () =>
				{
					CloseRadialForm();
				};
			}
			else if (string.Equals(a, "发送快捷键", StringComparison.OrdinalIgnoreCase))
			{
				string text4 = item.Shortcut ?? "";
				if (string.Equals(text4.Trim(), "BROWSER_BACK", StringComparison.OrdinalIgnoreCase))
				{
					action = [SpecialName] () =>
					{
						CloseRadialForm();
						RestoreForegroundWindowForInput();
						Thread.Sleep(200);
						SendBrowserBackByKeybdEvent();
					};
				}
				else
				{
					closure_0024__123_002D3 = new _Closure_0024__123_002D2(closure_0024__123_002D3);
					closure_0024__123_002D3._0024VB_0024Me = this;
					closure_0024__123_002D3._0024VB_0024Local_shortcut = text4;
					action = closure_0024__123_002D3._Lambda_0024__4;
				}
			}
			else if (string.Equals(a, "键入文本", StringComparison.OrdinalIgnoreCase))
			{
				closure_0024__123_002D4 = new _Closure_0024__123_002D3(closure_0024__123_002D4);
				closure_0024__123_002D4._0024VB_0024Me = this;
				closure_0024__123_002D4._0024VB_0024Local_t = item.TypedText ?? "";
				action = closure_0024__123_002D4._Lambda_0024__5;
			}
			else if (string.Equals(a, "粘贴文本", StringComparison.OrdinalIgnoreCase))
			{
				closure_0024__123_002D5 = new _Closure_0024__123_002D4(closure_0024__123_002D5);
				closure_0024__123_002D5._0024VB_0024Me = this;
				closure_0024__123_002D5._0024VB_0024Local_t = NormalizeNewLinesToLf(item.PasteText);
				action = closure_0024__123_002D5._Lambda_0024__6;
			}
			else if (string.Equals(a, "打开或运行（文件/目录/命令/网址）", StringComparison.OrdinalIgnoreCase))
			{
				closure_0024__123_002D6 = new _Closure_0024__123_002D5(closure_0024__123_002D6);
				closure_0024__123_002D6._0024VB_0024Me = this;
				string text5 = (item.RunPath ?? "").Trim();
				if (text5.Length == 0)
				{
					text5 = text3;
				}
				string _0024VB_0024Local_toArgs = item.RunParams ?? "";
				closure_0024__123_002D6._0024VB_0024Local_toRun = text5;
				closure_0024__123_002D6._0024VB_0024Local_toArgs = _0024VB_0024Local_toArgs;
				action = closure_0024__123_002D6._Lambda_0024__7;
			}
			else if (!string.IsNullOrWhiteSpace(text3))
			{
				if (text3.StartsWith("QCAD_BRIDGE:", StringComparison.OrdinalIgnoreCase))
				{
					closure_0024__123_002D7 = new _Closure_0024__123_002D6(closure_0024__123_002D7);
					closure_0024__123_002D7._0024VB_0024Me = this;
					string text6 = text3.Substring("QCAD_BRIDGE:".Length);
					if (text6.StartsWith("QCAD_Bridge?", StringComparison.OrdinalIgnoreCase))
					{
						text6 = text6.Substring("QCAD_Bridge?".Length);
					}
					closure_0024__123_002D7._0024VB_0024Local_payload = BuildQuickerBridgePayload(text6);
					action = closure_0024__123_002D7._Lambda_0024__8;
				}
				else if (text3.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase))
				{
					closure_0024__123_002D8 = new _Closure_0024__123_002D7(closure_0024__123_002D8);
					closure_0024__123_002D8._0024VB_0024Me = this;
					closure_0024__123_002D8._0024VB_0024Local_raw = text3.Substring("QCAD_ACTION:".Length).Trim();
					action = closure_0024__123_002D8._Lambda_0024__9;
				}
				else
				{
					closure_0024__123_002D9 = new _Closure_0024__123_002D8(closure_0024__123_002D9);
					closure_0024__123_002D9._0024VB_0024Local_toRun = text3;
					action = closure_0024__123_002D9._Lambda_0024__10;
				}
			}
			Color fillColor;
			if (string.Equals(text3, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
			{
				fillColor = ColorTranslator.FromHtml("#5b87ff");
			}
			else if (string.Equals(text3, "_EXIT", StringComparison.OrdinalIgnoreCase))
			{
				fillColor = ColorTranslator.FromHtml("#666666");
			}
			else
			{
				fillColor = Color.FromArgb(item.ColorArgb);
				if (string.Equals(item.ParentMenuName, "ROOT", StringComparison.OrdinalIgnoreCase) && item.IsSubMenu)
				{
					fillColor = ColorTranslator.FromHtml("#FFB650");
				}
				if (item.Level != 2)
				{
					fillColor = Color.White;
					if (item.IsSubMenu)
					{
						fillColor = Color.FromArgb(240, 240, 255);
					}
				}
				else if (item.ColorArgb == 0)
				{
					fillColor = ColorTranslator.FromHtml("#F0F0F0");
				}
			}
			list.Add(new RadialSector(text, item.Command, fillColor, action, item.Level, null, item.IsSubMenu, item.SubMenuName, item.ParentMenuName, item.FontSize, item.Page, item.SectorIndex, item.QuickerMenuItems, text2, item.Enabled, useFirstCharIcon, item.OnlyShowIcon));
		}
		return list;
	}

	private void RestoreForegroundWindowForInput()
	{
		IntPtr intPtr = lastForegroundHwnd;
		if (intPtr == IntPtr.Zero || !NativeMethods.IsWindow(intPtr))
		{
			return;
		}
		IntPtr zero = IntPtr.Zero;
		try
		{
			zero = NativeMethods.GetForegroundWindow();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			zero = IntPtr.Zero;
			ProjectData.ClearProjectError();
		}
		if (NativeMethods.IsDesktopManagerProcessName(lastForegroundProcessName))
		{
			Logger.Log("RestoreForegroundWindowForInput: Detected desktop process (" + lastForegroundProcessName + ")");
			if (zero == intPtr)
			{
				Logger.Log("RestoreForegroundWindowForInput: Desktop is already foreground. Skipping restoration to prevent selection loss.");
			}
			else
			{
				Logger.Log("RestoreForegroundWindowForInput: Desktop is NOT foreground (fg=" + zero.ToString("X") + "). Skipping restoration anyway to be safe.");
			}
			return;
		}
		int lpdwProcessId = 0;
		int lpdwProcessId2 = 0;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		try
		{
			num3 = NativeMethods.GetCurrentThreadId();
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			num3 = 0;
			ProjectData.ClearProjectError();
		}
		try
		{
			if (zero != IntPtr.Zero)
			{
				num = NativeMethods.GetWindowThreadProcessId(zero, ref lpdwProcessId);
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			num = 0;
			ProjectData.ClearProjectError();
		}
		try
		{
			num2 = NativeMethods.GetWindowThreadProcessId(intPtr, ref lpdwProcessId2);
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			num2 = 0;
			ProjectData.ClearProjectError();
		}
		bool flag = false;
		bool flag2 = false;
		try
		{
			if (num3 != 0 && num != 0 && num != num3)
			{
				flag = NativeMethods.AttachThreadInput(num3, num, fAttach: true);
			}
			if (num3 != 0 && num2 != 0 && num2 != num3)
			{
				flag2 = NativeMethods.AttachThreadInput(num3, num2, fAttach: true);
			}
			try
			{
				if (NativeMethods.IsIconic(intPtr))
				{
					NativeMethods.ShowWindowAsync(intPtr, 9);
				}
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			try
			{
				NativeMethods.BringWindowToTop(intPtr);
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			try
			{
				NativeMethods.SetForegroundWindow(intPtr);
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				ProjectData.ClearProjectError();
			}
			try
			{
				NativeMethods.SetFocus(intPtr);
			}
			catch (Exception projectError8)
			{
				ProjectData.SetProjectError(projectError8);
				ProjectData.ClearProjectError();
			}
		}
		finally
		{
			try
			{
				if (flag2)
				{
					NativeMethods.AttachThreadInput(num3, num2, fAttach: false);
				}
			}
			catch (Exception projectError9)
			{
				ProjectData.SetProjectError(projectError9);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (flag)
				{
					NativeMethods.AttachThreadInput(num3, num, fAttach: false);
				}
			}
			catch (Exception projectError10)
			{
				ProjectData.SetProjectError(projectError10);
				ProjectData.ClearProjectError();
			}
		}
	}

	public void SaveMenuSize(MenuSizeConfig menuSize)
	{
		if (menuSize != null)
		{
			config.MenuSize = menuSize;
			store.Save(config);
		}
	}

	public static Image GetPreviewIcon(string path)
	{
		Image result;
		try
		{
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				result = null;
			}
			else
			{
				using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				using MemoryStream memoryStream = new MemoryStream();
				fileStream.CopyTo(memoryStream);
				memoryStream.Position = 0L;
				using Image original = Image.FromStream(memoryStream);
				result = new Bitmap(original);
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = null;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private void CloseRadialForm()
	{
		if (radialForm != null && !radialForm.IsDisposed)
		{
			try
			{
				radialForm.HideMenu();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__27_002D0()
	{
		try
		{
			GetCachedGlobalSectors("", isGlobalMenu: true);
			GetCachedGlobalSectors("", isGlobalMenu: false);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__29_002D1()
	{
		radialForm = null;
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__46_002D3()
	{
		radialForm = null;
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__58_002D1()
	{
		settingsForm = null;
	}
}
