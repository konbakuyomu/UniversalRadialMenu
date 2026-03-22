using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

internal class RadialSectorMenuForm : Form
{
	public delegate void ActionTriggeredEventHandler(Action action);

	public delegate void MenuSizeChangedEventHandler(MenuSizeConfig newSize);

	public delegate void RequestOpenSettingsEventHandler(string scopeProcessName);

	public delegate void RequestExitAppEventHandler();

	public delegate void RequestRestoreForegroundEventHandler();

	public delegate void RequestStartForegroundWatchEventHandler(string tag);

	private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

	private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

	private struct MSLLHOOKSTRUCT
	{
		public Point pt;

		public int mouseData;

		public int flags;

		public int time;

		public IntPtr dwExtraInfo;
	}

	private struct KBDLLHOOKSTRUCT
	{
		public uint vkCode;

		public uint scanCode;

		public uint flags;

		public uint time;

		public IntPtr dwExtraInfo;
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__197_002D0
	{
		public ContextMenuStrip _0024VB_0024Local_newMenu;

		public RadialSectorMenuForm _0024VB_0024Me;

		public Action _0024I2;

		public _Closure_0024__197_002D0(_Closure_0024__197_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_newMenu = arg0._0024VB_0024Local_newMenu;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R1(object a0, ToolStripDropDownClosedEventArgs a1)
		{
			_Lambda_0024__1();
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			if (_0024VB_0024Me.quickerMenu == _0024VB_0024Local_newMenu)
			{
				_0024VB_0024Me.quickerMenuActive = false;
				_0024VB_0024Me.quickerMenu = null;
				_0024VB_0024Me.quickerMenuHotkeyItems = null;
				_0024VB_0024Me.quickerMenuEditHotItem = null;
				_0024VB_0024Me.quickerMenuDebugHotItem = null;
				_0024VB_0024Me.quickerMenuIconSize = 16;
				_0024VB_0024Me.quickerMenuActionId = "";
			}
			try
			{
				try
				{
					if (_0024VB_0024Local_newMenu.Tag is Font font)
					{
						font.Dispose();
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				if (!_0024VB_0024Me.IsDisposed && _0024VB_0024Me.IsHandleCreated)
				{
					_0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
					{
						try
						{
							_0024VB_0024Local_newMenu.Dispose();
						}
						catch (Exception projectError3)
						{
							ProjectData.SetProjectError(projectError3);
							ProjectData.ClearProjectError();
						}
					}));
				}
				else
				{
					_0024VB_0024Local_newMenu.Dispose();
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			try
			{
				_0024VB_0024Local_newMenu.Dispose();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__197_002D1
	{
		public ToolStripMenuItem _0024VB_0024Local_hoverMi;

		public _Closure_0024__197_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__197_002D1(_Closure_0024__197_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_hoverMi = arg0._0024VB_0024Local_hoverMi;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R2(object a0, EventArgs a1)
		{
			_Lambda_0024__3();
		}

		[SpecialName]
		internal async void _Lambda_0024__3()
		{
			_Closure_0024__197_002D2 arg = default(_Closure_0024__197_002D2);
			_Closure_0024__197_002D2 CS_0024_003C_003E8__locals4 = new _Closure_0024__197_002D2(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3 = this,
				_0024VB_0024Local_clickParam = ""
			};
			try
			{
				CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam = Convert.ToString(RuntimeHelpers.GetObjectValue(_0024VB_0024Local_hoverMi.Tag));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam = "";
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerMenu != null)
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerMenu.Close();
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.Hide();
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RequestRestoreForeground?.Invoke();
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				ProjectData.ClearProjectError();
			}
			try
			{
				await Task.Delay(80);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			try
			{
				await Task.Run([SpecialName] () =>
				{
					CS_0024_003C_003E8__locals4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ExecuteQuickerMenuParam(CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam);
				});
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.Close();
				}));
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				try
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.Close();
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__197_002D2
	{
		public string _0024VB_0024Local_clickParam;

		public _Closure_0024__197_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__197_002D2(_Closure_0024__197_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_clickParam = arg0._0024VB_0024Local_clickParam;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ExecuteQuickerMenuParam(_0024VB_0024Local_clickParam);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__198_002D0
	{
		public ToolStripMenuItem _0024VB_0024Local_mi;

		public RadialSectorMenuForm _0024VB_0024Me;

		[SpecialName]
		internal void _Lambda_0024__R3(object a0, EventArgs a1)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal async void _Lambda_0024__0()
		{
			_Closure_0024__198_002D1 CS_0024_003C_003E8__locals4 = new _Closure_0024__198_002D1
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2 = this,
				_0024VB_0024Local_clickParam = ""
			};
			try
			{
				CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam = Convert.ToString(RuntimeHelpers.GetObjectValue(_0024VB_0024Local_mi.Tag));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam = "";
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024Me.quickerMenu != null)
				{
					_0024VB_0024Me.quickerMenu.Close();
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024Me.Hide();
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024Me.RequestRestoreForeground?.Invoke();
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				ProjectData.ClearProjectError();
			}
			try
			{
				await Task.Delay(80);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			try
			{
				await Task.Run([SpecialName] () =>
				{
					CS_0024_003C_003E8__locals4._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ExecuteQuickerMenuParam(CS_0024_003C_003E8__locals4._0024VB_0024Local_clickParam);
				});
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			try
			{
				_0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
				{
					_0024VB_0024Me.Close();
				}));
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				try
				{
					_0024VB_0024Me.Close();
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__198_002D1
	{
		public string _0024VB_0024Local_clickParam;

		public _Closure_0024__198_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ExecuteQuickerMenuParam(_0024VB_0024Local_clickParam);
		}
	}

	private List<RadialSector> globalSectors;

	private List<RadialSector> processSectors;

	private string scopeProcessName;

	private List<RadialSector> activeSectors;

	private bool isGlobalMode;

	private RECT hostWindowRect;

	private MenuSizeConfig menuSize;

	private Font menuFont;

	private const double CenterInnerScale = 0.75;

	private const float WheelRotationDeg = 22.5f;

	private bool isDragging;

	private Point dragStartMousePos;

	private Point? dragFeedbackPosition;

	private bool isDraggingToSector;

	private int targetSectorIndex;

	private int targetSectorLevel;

	private int highlightedSectorIndex;

	private int highlightedSectorLevel;

	private int hoverAnimFromIndex;

	private int hoverAnimFromLevel;

	private int hoverAnimToIndex;

	private int hoverAnimToLevel;

	private float hoverAnimProgress;

	private DateTime hoverAnimLastAt;

	private DateTime lastClickTime;

	private Point lastClickPos;

	private Image dragImage;

	private MemoryStream dragImageStream;

	private string dragImagePath;

	private Timer animationTimer;

	private int animationAlpha;

	private RadialSector clickedSector;

	private readonly Dictionary<int, Font> sectorFontCache;

	private int CurrentPage;

	private int TotalPages;

	private string currentMenuName;

	private Stack<KeyValuePair<string, string>> menuStack;

	private readonly RadialSector backSector;

	private Timer rightButtonCheckTimer;

	private bool lastRightButtonState;

	private bool releaseHandling;

	private DateTime lastReleaseHandledAt;

	private bool releaseHandledForThisPress;

	private bool skipSimulatedMouseUpOnce;

	private DateTime lastGlobalMoveUpdateAt;

	private Timer followCursorTimer;

	private ContextMenuStrip quickerMenu;

	private bool quickerMenuActive;

	private bool lastSpaceKeyDown;

	private List<ToolStripMenuItem> quickerMenuHotkeyItems;

	private ToolStripMenuItem quickerMenuEditHotItem;

	private ToolStripMenuItem quickerMenuDebugHotItem;

	private int quickerMenuIconSize;

	private string quickerMenuActionId;

	private const float QuickerMenuSpaceFontIncrease = 3f;

	private const int QuickerMenuSpaceIconSize = 24;

	private List<RadialSector> currentMenuGlobalItems;

	private List<RadialSector> currentMenuProcessItems;

	private const int WH_MOUSE_LL = 14;

	private const int WH_KEYBOARD_LL = 13;

	private const int WM_MOUSEMOVE = 512;

	private const int WM_LBUTTONDOWN = 513;

	private const int WM_LBUTTONUP = 514;

	private const int WM_RBUTTONDOWN = 516;

	private const int WM_RBUTTONUP = 517;

	private const int WM_MBUTTONDOWN = 519;

	private const int WM_MBUTTONUP = 520;

	private const int WM_KEYDOWN = 256;

	private const int WM_SYSKEYDOWN = 260;

	private static IntPtr mouseHookId = IntPtr.Zero;

	private static LowLevelMouseProc mouseHookProc = null;

	private static WeakReference mouseHookOwner = null;

	private static IntPtr keyboardHookId = IntPtr.Zero;

	private static LowLevelKeyboardProc keyboardHookProc = null;

	private static WeakReference keyboardHookOwner = null;

	private bool shouldStayOpen;

	private DateTime showTime;

	private int triggerMouseButton;

	private bool menuActive;

	private bool allowClose;

	private const ushort RI_MOUSE_LEFT_BUTTON_DOWN = 1;

	private const ushort RI_MOUSE_LEFT_BUTTON_UP = 2;

	private const ushort RI_MOUSE_RIGHT_BUTTON_DOWN = 4;

	private const ushort RI_MOUSE_RIGHT_BUTTON_UP = 8;

	private const ushort RI_MOUSE_MIDDLE_BUTTON_DOWN = 16;

	private const ushort RI_MOUSE_MIDDLE_BUTTON_UP = 32;

	protected override bool ShowWithoutActivation => true;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams obj = base.CreateParams;
			obj.ClassStyle |= 131072;
			obj.ExStyle |= 134217728;
			return obj;
		}
	}

	public event ActionTriggeredEventHandler ActionTriggered;

	public event MenuSizeChangedEventHandler MenuSizeChanged;

	public event RequestOpenSettingsEventHandler RequestOpenSettings;

	public event RequestExitAppEventHandler RequestExitApp;

	public event RequestRestoreForegroundEventHandler RequestRestoreForeground;

	public event RequestStartForegroundWatchEventHandler RequestStartForegroundWatch;

	private Font GetSectorFont(int fontSize)
	{
		int num = Math.Max(6, fontSize);
		if (sectorFontCache.ContainsKey(num))
		{
			return sectorFontCache[num];
		}
		Font font = new Font("Microsoft YaHei", num, FontStyle.Regular);
		sectorFontCache[num] = font;
		return font;
	}

	private Image GetIconForSector(RadialSector sector, int desiredSize)
	{
		if (sector == null)
		{
			return null;
		}
		if (desiredSize <= 0)
		{
			desiredSize = 24;
		}
		if (sector.UseFirstCharIcon)
		{
			return RadialMenuController.CreateLetterIcon(sector.Text, desiredSize);
		}
		string text = (sector.IconSpec ?? "").Trim();
		if (text.Length == 0)
		{
			return null;
		}
		return RadialMenuController.GetScaledIcon(text, desiredSize);
	}

	private double GetDpiScale()
	{
		double result;
		try
		{
			int num = base.DeviceDpi;
			result = ((num > 0) ? ((double)num / 96.0) : 1.0);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = 1.0;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private double GetScaledOuterRadius()
	{
		return (double)menuSize.OuterRadius * menuSize.Scale * GetDpiScale();
	}

	private double GetScaledInnerRadius()
	{
		return (double)menuSize.InnerRadius * menuSize.Scale * 0.75 * GetDpiScale();
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetWindowsHookExW", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetWindowsHookExW", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool UnhookWindowsHookEx(IntPtr hhk);

	[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetModuleHandleW", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetModuleHandle(string lpModuleName);

	public RadialSectorMenuForm(List<RadialSector> globalSectors, List<RadialSector> processSectors, string scopeProcessName, MenuSizeConfig sizeConfig, RECT hostWindowRect, bool startGlobal, int triggerMouseButton = 2, bool warmupOnly = false)
	{
		isDragging = false;
		dragFeedbackPosition = null;
		isDraggingToSector = false;
		targetSectorIndex = -1;
		targetSectorLevel = -1;
		highlightedSectorIndex = -1;
		highlightedSectorLevel = -1;
		hoverAnimFromIndex = -1;
		hoverAnimFromLevel = -1;
		hoverAnimToIndex = -1;
		hoverAnimToLevel = -1;
		hoverAnimProgress = 1f;
		hoverAnimLastAt = DateTime.MinValue;
		lastClickTime = DateTime.MinValue;
		lastClickPos = Point.Empty;
		dragImage = null;
		dragImageStream = null;
		dragImagePath = "";
		animationAlpha = 255;
		clickedSector = null;
		sectorFontCache = new Dictionary<int, Font>();
		CurrentPage = 0;
		TotalPages = 1;
		currentMenuName = "ROOT";
		menuStack = new Stack<KeyValuePair<string, string>>();
		lastRightButtonState = false;
		releaseHandling = false;
		lastReleaseHandledAt = DateTime.MinValue;
		releaseHandledForThisPress = false;
		skipSimulatedMouseUpOnce = false;
		lastGlobalMoveUpdateAt = DateTime.MinValue;
		quickerMenu = null;
		quickerMenuActive = false;
		lastSpaceKeyDown = false;
		quickerMenuHotkeyItems = null;
		quickerMenuEditHotItem = null;
		quickerMenuDebugHotItem = null;
		quickerMenuIconSize = 16;
		quickerMenuActionId = "";
		currentMenuGlobalItems = new List<RadialSector>();
		currentMenuProcessItems = new List<RadialSector>();
		shouldStayOpen = false;
		this.triggerMouseButton = 2;
		menuActive = true;
		allowClose = false;
		this.globalSectors = globalSectors ?? new List<RadialSector>();
		this.processSectors = processSectors ?? null;
		this.scopeProcessName = (scopeProcessName ?? "").Trim();
		menuSize = sizeConfig ?? new MenuSizeConfig();
		this.hostWindowRect = hostWindowRect;
		showTime = DateTime.Now;
		this.triggerMouseButton = triggerMouseButton;
		menuActive = !warmupOnly;
		backSector = new RadialSector("<- 返回", "", Color.DarkGray, [SpecialName] () =>
		{
			NavigateBack();
		}, 0, null, isSubMenu: false, "", "VIRTUAL", 12, 0);
		base.FormBorderStyle = FormBorderStyle.None;
		base.ShowInTaskbar = false;
		base.TopMost = false;
		BackColor = Color.Black;
		base.Opacity = 1.0;
		DoubleBuffered = true;
		base.AutoScaleMode = AutoScaleMode.Dpi;
		SetGlobalMode(startGlobal);
		try
		{
			RadialMenuController.ApplyMenuSizeDpiProfileToBase(menuSize, base.DeviceDpi);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		float emSize = (float)Math.Max(1.0, (double)menuSize.FontSize * menuSize.Scale);
		menuFont = new Font("Microsoft YaHei", emSize, FontStyle.Bold);
		base.MouseDown += Form_MouseDown;
		base.MouseMove += Form_MouseMove;
		base.MouseUp += Form_MouseUp;
		base.MouseWheel += Form_MouseWheel;
		base.Paint += Form_Paint;
		base.MouseLeave += Form_MouseLeave;
		try
		{
			NativeMethods.MARGINS pMarInset = new NativeMethods.MARGINS
			{
				cxLeftWidth = -1,
				cxRightWidth = -1,
				cyTopHeight = -1,
				cyBottomHeight = -1
			};
			NativeMethods.DwmExtendFrameIntoClientArea(base.Handle, ref pMarInset);
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		RegisterRawMouseInput();
		EnsureDragImageLoaded();
		animationTimer = new Timer
		{
			Interval = 100
		};
		animationTimer.Tick += AnimationTimer_Tick;
		rightButtonCheckTimer = new Timer
		{
			Interval = 20
		};
		rightButtonCheckTimer.Tick += CheckRightButtonState;
		if (!warmupOnly)
		{
			lastRightButtonState = IsTriggerButtonDown();
			if (lastRightButtonState)
			{
				isDraggingToSector = true;
				UpdateDragStateFromGlobalState();
			}
			rightButtonCheckTimer.Start();
		}
		followCursorTimer = new Timer
		{
			Interval = 16
		};
		followCursorTimer.Tick += FollowCursorTimer_Tick;
		if (!warmupOnly)
		{
			followCursorTimer.Start();
		}
		UpdateSize();
		UpdateFormRegion();
		if (!warmupOnly)
		{
			InstallMouseHook();
			InstallKeyboardHook();
		}
		base.FormClosed += RadialSectorMenuForm_FormClosed;
		Logger.Log("RadialSectorMenuForm: Ctor complete. TopMost=" + base.TopMost);
	}

	private ushort[] GetTriggerRawButtonFlags()
	{
		return triggerMouseButton switch
		{
			1 => new ushort[2] { 1, 2 }, 
			4 => new ushort[2] { 16, 32 }, 
			_ => new ushort[2] { 4, 8 }, 
		};
	}

	private void RegisterRawMouseInput()
	{
		try
		{
			NativeMethods.RAWINPUTDEVICE rAWINPUTDEVICE = new NativeMethods.RAWINPUTDEVICE
			{
				usUsagePage = 1,
				usUsage = 2,
				dwFlags = 256,
				hwndTarget = base.Handle
			};
			if (NativeMethods.RegisterRawInputDevices(new NativeMethods.RAWINPUTDEVICE[1] { rAWINPUTDEVICE }, 1u, checked((uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICE)))))
			{
				Logger.Log("RegisterRawMouseInput: OK");
			}
			else
			{
				Logger.Log("RegisterRawMouseInput: FAILED err=" + Marshal.GetLastWin32Error());
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Log("RegisterRawMouseInput: EX " + ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	protected override void WndProc(ref Message m)
	{
		if (m.Msg == 33)
		{
			m.Result = new IntPtr(3);
			return;
		}
		if (m.Msg == 255)
		{
			try
			{
				if (menuActive)
				{
					ProcessRawMouseInput(m.LParam);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Log("WM_INPUT: EX " + ex2.Message);
				ProjectData.ClearProjectError();
			}
		}
		base.WndProc(ref m);
	}

	private void ProcessRawMouseInput(IntPtr hRawInput)
	{
		uint pcbSize = 0u;
		checked
		{
			uint cbSizeHeader = (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTHEADER));
			NativeMethods.GetRawInputData(hRawInput, 268435459, IntPtr.Zero, ref pcbSize, cbSizeHeader);
			if (pcbSize == 0)
			{
				return;
			}
			IntPtr intPtr = Marshal.AllocHGlobal((int)pcbSize);
			try
			{
				uint rawInputData = NativeMethods.GetRawInputData(hRawInput, 268435459, intPtr, ref pcbSize, cbSizeHeader);
				if (rawInputData == 0 || rawInputData == uint.MaxValue)
				{
					return;
				}
				object obj = Marshal.PtrToStructure(intPtr, typeof(NativeMethods.RAWINPUTHEADER));
				NativeMethods.RAWINPUTHEADER obj2 = ((obj != null) ? ((NativeMethods.RAWINPUTHEADER)obj) : default(NativeMethods.RAWINPUTHEADER));
				if (obj2.dwType == 0)
				{
					object obj3 = Marshal.PtrToStructure(IntPtr.Add(intPtr, Marshal.SizeOf(typeof(NativeMethods.RAWINPUTHEADER))), typeof(NativeMethods.RAWMOUSE));
					NativeMethods.RAWMOUSE obj4 = ((obj3 != null) ? ((NativeMethods.RAWMOUSE)obj3) : default(NativeMethods.RAWMOUSE));
					ushort num = (ushort)(obj4.ulButtons & 0xFFFF);
					ushort[] triggerRawButtonFlags = GetTriggerRawButtonFlags();
					ushort num2 = triggerRawButtonFlags[0];
					ushort num3 = triggerRawButtonFlags[1];
					if ((num & num2) != 0)
					{
						lastRightButtonState = true;
						releaseHandledForThisPress = false;
						Logger.Log("RawInput: TriggerDown");
					}
					if ((num & num3) != 0)
					{
						lastRightButtonState = false;
						Logger.Log("RawInput: TriggerUp");
						TryHandleTriggerReleased("RawInput");
					}
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
	}

	public void UpdateContext(List<RadialSector> globalSectors, List<RadialSector> processSectors, string scopeProcessName, bool startGlobal)
	{
		this.globalSectors = globalSectors ?? new List<RadialSector>();
		this.processSectors = processSectors;
		this.scopeProcessName = (scopeProcessName ?? "").Trim();
		SetGlobalMode(startGlobal);
		Invalidate();
	}

	public void ResetForShow(List<RadialSector> globalSectors, List<RadialSector> processSectors, string scopeProcessName, MenuSizeConfig sizeConfig, RECT hostWindowRect, bool startGlobal, IntPtr fgForOwner = default(IntPtr))
	{
		menuActive = true;
		this.hostWindowRect = hostWindowRect;
		menuSize = sizeConfig ?? new MenuSizeConfig();
		try
		{
			RadialMenuController.ApplyMenuSizeDpiProfileToBase(menuSize, base.DeviceDpi);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (menuFont != null)
			{
				menuFont.Dispose();
				menuFont = null;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			menuFont = null;
			ProjectData.ClearProjectError();
		}
		try
		{
			sectorFontCache.Clear();
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		float emSize = (float)Math.Max(1.0, (double)menuSize.FontSize * menuSize.Scale);
		menuFont = new Font("Microsoft YaHei", emSize, FontStyle.Bold);
		EnsureDragImageLoaded();
		showTime = DateTime.Now;
		releaseHandling = false;
		releaseHandledForThisPress = false;
		lastReleaseHandledAt = DateTime.MinValue;
		quickerMenuActive = false;
		lastSpaceKeyDown = false;
		clickedSector = null;
		isDragging = false;
		isDraggingToSector = false;
		dragFeedbackPosition = null;
		targetSectorIndex = -1;
		targetSectorLevel = -1;
		highlightedSectorIndex = -1;
		highlightedSectorLevel = -1;
		hoverAnimFromIndex = -1;
		hoverAnimFromLevel = -1;
		hoverAnimToIndex = -1;
		hoverAnimToLevel = -1;
		hoverAnimProgress = 1f;
		checked
		{
			int num = (int)Math.Round(GetScaledOuterRadius());
			Size size = new Size(num * 2 + 60, num * 2 + 60);
			if (base.Size != size)
			{
				UpdateSize();
			}
			UpdateContext(globalSectors, processSectors, scopeProcessName, startGlobal);
			bool flag = true;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				IntPtr intPtr2 = fgForOwner;
				if (intPtr2 == IntPtr.Zero)
				{
					intPtr2 = NativeMethods.GetForegroundWindow();
				}
				string text = "";
				if (intPtr2 != IntPtr.Zero && NativeMethods.IsWindow(intPtr2))
				{
					int lpdwProcessId = 0;
					NativeMethods.GetWindowThreadProcessId(intPtr2, ref lpdwProcessId);
					if (lpdwProcessId > 0)
					{
						using Process process = Process.GetProcessById(lpdwProcessId);
						text = process.ProcessName ?? "";
					}
				}
				if (NativeMethods.IsDesktopManagerProcessName(text))
				{
					flag = false;
					intPtr = intPtr2;
					Logger.Log("ResetForShow: 检测到桌面管理器(" + text + "), 强制TopMost=False, 并将Owner设为前台窗口");
				}
				else
				{
					Logger.Log("ResetForShow: 非桌面管理器(" + text + "), TopMost=True");
				}
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				flag = true;
				intPtr = IntPtr.Zero;
				ProjectData.ClearProjectError();
			}
			try
			{
				if (base.TopMost != flag)
				{
					base.TopMost = flag;
					Logger.Log("ResetForShow: TopMost changed to " + flag);
				}
				if (intPtr != IntPtr.Zero)
				{
					NativeMethods.SetWindowLongPtr(base.Handle, -8, intPtr);
				}
				else
				{
					NativeMethods.SetWindowLongPtr(base.Handle, -8, IntPtr.Zero);
				}
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (rightButtonCheckTimer != null && !rightButtonCheckTimer.Enabled)
				{
					rightButtonCheckTimer.Start();
				}
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (followCursorTimer != null && !followCursorTimer.Enabled)
				{
					followCursorTimer.Start();
				}
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				ProjectData.ClearProjectError();
			}
			try
			{
				EnsureHooksInstalled();
			}
			catch (Exception projectError8)
			{
				ProjectData.SetProjectError(projectError8);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void EnsureDragImageLoaded()
	{
		string text = "";
		try
		{
			text = (menuSize.DragAreaBackgroundImage ?? "").Trim();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			text = "";
			ProjectData.ClearProjectError();
		}
		if (string.Equals(text, dragImagePath ?? "", StringComparison.OrdinalIgnoreCase) && dragImage != null)
		{
			return;
		}
		dragImagePath = text;
		try
		{
			if (dragImage != null)
			{
				try
				{
					ImageAnimator.StopAnimate(dragImage, OnFrameChanged);
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				dragImage.Dispose();
				dragImage = null;
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (dragImageStream != null)
			{
				dragImageStream.Dispose();
				dragImageStream = null;
			}
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			ProjectData.ClearProjectError();
		}
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		string text2 = RadialMenuController.ResolveSkinSpecToLocalFile(text);
		if (string.IsNullOrWhiteSpace(text2) || !File.Exists(text2))
		{
			return;
		}
		try
		{
			using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				MemoryStream memoryStream = new MemoryStream();
				fileStream.CopyTo(memoryStream);
				memoryStream.Position = 0L;
				dragImageStream = memoryStream;
				dragImage = Image.FromStream(dragImageStream);
			}
			if (ImageAnimator.CanAnimate(dragImage))
			{
				ImageAnimator.Animate(dragImage, OnFrameChanged);
			}
		}
		catch (Exception projectError5)
		{
			ProjectData.SetProjectError(projectError5);
			try
			{
				if (dragImage != null)
				{
					dragImage.Dispose();
				}
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			dragImage = null;
			try
			{
				if (dragImageStream != null)
				{
					dragImageStream.Dispose();
				}
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				ProjectData.ClearProjectError();
			}
			dragImageStream = null;
			ProjectData.ClearProjectError();
		}
	}

	public void HideMenu()
	{
		menuActive = false;
		try
		{
			if (quickerMenu != null)
			{
				quickerMenu.Close();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		quickerMenuActive = false;
		try
		{
			rightButtonCheckTimer.Stop();
			followCursorTimer.Stop();
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (skipSimulatedMouseUpOnce)
			{
				Logger.Log("HideMenu: Skip simulated mouse release (handled by trigger-up)");
			}
			else if (IsTriggerButtonDown())
			{
				bool flag = false;
				try
				{
					IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
					if (foregroundWindow != IntPtr.Zero && NativeMethods.IsWindow(foregroundWindow))
					{
						int lpdwProcessId = 0;
						NativeMethods.GetWindowThreadProcessId(foregroundWindow, ref lpdwProcessId);
						if (lpdwProcessId > 0)
						{
							string text = "";
							using (Process process = Process.GetProcessById(lpdwProcessId))
							{
								text = process.ProcessName ?? "";
							}
							if (NativeMethods.IsDesktopManagerProcessName(text))
							{
								flag = true;
								Logger.Log("HideMenu: 检测到桌面管理器(" + text + "),跳过模拟鼠标释放,避免清除选中状态");
							}
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Log("HideMenu: 检测前台进程失败 " + ex2.Message);
					ProjectData.ClearProjectError();
				}
				if (!flag)
				{
					Logger.Log("HideMenu: 模拟鼠标释放");
					switch (triggerMouseButton)
					{
					case 1:
						NativeMethods.mouse_event(4, 0, 0, 0, IntPtr.Zero);
						break;
					case 4:
						NativeMethods.mouse_event(64, 0, 0, 0, IntPtr.Zero);
						break;
					default:
						NativeMethods.mouse_event(16, 0, 0, 0, IntPtr.Zero);
						break;
					}
				}
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Log("HideMenu: 模拟鼠标释放出错 " + ex4.Message);
			ProjectData.ClearProjectError();
		}
		skipSimulatedMouseUpOnce = false;
		try
		{
			if (base.Visible)
			{
				Hide();
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
	}

	public void BumpToFrontNoActivate()
	{
		try
		{
			if (base.IsHandleCreated)
			{
				try
				{
					NativeMethods.ShowWindow(base.Handle, 4);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				uint uFlags = 83u;
				if (base.TopMost)
				{
					NativeMethods.SetWindowPos(base.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, uFlags);
					return;
				}
				NativeMethods.SetWindowPos(base.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, uFlags);
				NativeMethods.SetWindowPos(base.Handle, NativeMethods.HWND_NOTOPMOST, 0, 0, 0, 0, uFlags);
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		if (!allowClose)
		{
			e.Cancel = true;
			HideMenu();
		}
		else
		{
			base.OnFormClosing(e);
		}
	}

	public void WarmUpFastShow()
	{
		try
		{
			EnsureHooksInstalled();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	public void PrimeForFastShow()
	{
		try
		{
			double opacity = base.Opacity;
			Point location = base.Location;
			bool flag = menuActive;
			menuActive = false;
			base.Opacity = 0.0;
			base.StartPosition = FormStartPosition.Manual;
			base.Location = new Point(-20000, -20000);
			if (!base.Visible)
			{
				Show();
			}
			Update();
			Hide();
			base.Opacity = opacity;
			base.Location = location;
			menuActive = flag;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	public void ForceClose()
	{
		try
		{
			allowClose = true;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			Close();
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	private void EnsureHooksInstalled()
	{
		if (mouseHookId == IntPtr.Zero)
		{
			InstallMouseHook();
		}
		else
		{
			mouseHookOwner = new WeakReference(this);
		}
		if (keyboardHookId == IntPtr.Zero)
		{
			InstallKeyboardHook();
		}
		else
		{
			keyboardHookOwner = new WeakReference(this);
		}
	}

	private void SetGlobalMode(bool value)
	{
		if (value && processSectors == null)
		{
			isGlobalMode = true;
			activeSectors = globalSectors;
		}
		else if (value)
		{
			isGlobalMode = true;
			activeSectors = globalSectors;
		}
		else
		{
			isGlobalMode = false;
			activeSectors = processSectors ?? globalSectors;
		}
		currentMenuName = "ROOT";
		menuStack.Clear();
		CurrentPage = 0;
		RefreshCurrentMenuCaches();
		RecalculateTotalPages();
	}

	private void RadialSectorMenuForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		try
		{
			UninstallMouseHook();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			UninstallKeyboardHook();
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		try
		{
			rightButtonCheckTimer.Stop();
			followCursorTimer.Stop();
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (skipSimulatedMouseUpOnce)
			{
				Logger.Log("FormClosed: Skip simulated mouse release (handled by trigger-up)");
			}
			else if (IsTriggerButtonDown())
			{
				bool flag = false;
				try
				{
					IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
					if (foregroundWindow != IntPtr.Zero && NativeMethods.IsWindow(foregroundWindow))
					{
						int lpdwProcessId = 0;
						NativeMethods.GetWindowThreadProcessId(foregroundWindow, ref lpdwProcessId);
						if (lpdwProcessId > 0)
						{
							string text = "";
							using (Process process = Process.GetProcessById(lpdwProcessId))
							{
								text = process.ProcessName ?? "";
							}
							if (NativeMethods.IsDesktopManagerProcessName(text))
							{
								flag = true;
								Logger.Log("FormClosed: 检测到桌面管理器(" + text + "),跳过模拟鼠标释放,避免清除选中状态");
							}
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Log("FormClosed: 检测前台进程失败 " + ex2.Message);
					ProjectData.ClearProjectError();
				}
				if (!flag)
				{
					Logger.Log("FormClosed: 模拟鼠标释放");
					switch (triggerMouseButton)
					{
					case 1:
						NativeMethods.mouse_event(4, 0, 0, 0, IntPtr.Zero);
						break;
					case 4:
						NativeMethods.mouse_event(64, 0, 0, 0, IntPtr.Zero);
						break;
					default:
						NativeMethods.mouse_event(16, 0, 0, 0, IntPtr.Zero);
						break;
					}
				}
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Log("FormClosed: 模拟鼠标释放出错 " + ex4.Message);
			ProjectData.ClearProjectError();
		}
		skipSimulatedMouseUpOnce = false;
		try
		{
			if (rightButtonCheckTimer != null)
			{
				rightButtonCheckTimer.Dispose();
				rightButtonCheckTimer = null;
			}
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (followCursorTimer != null)
			{
				followCursorTimer.Dispose();
				followCursorTimer = null;
			}
		}
		catch (Exception projectError5)
		{
			ProjectData.SetProjectError(projectError5);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (quickerMenu != null)
			{
				quickerMenu.Close();
				quickerMenu = null;
			}
		}
		catch (Exception projectError6)
		{
			ProjectData.SetProjectError(projectError6);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (dragImage != null)
			{
				try
				{
					ImageAnimator.StopAnimate(dragImage, OnFrameChanged);
				}
				catch (Exception projectError7)
				{
					ProjectData.SetProjectError(projectError7);
					ProjectData.ClearProjectError();
				}
				dragImage.Dispose();
				dragImage = null;
			}
		}
		catch (Exception projectError8)
		{
			ProjectData.SetProjectError(projectError8);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (dragImageStream != null)
			{
				dragImageStream.Dispose();
				dragImageStream = null;
			}
		}
		catch (Exception projectError9)
		{
			ProjectData.SetProjectError(projectError9);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (menuFont != null)
			{
				menuFont.Dispose();
				menuFont = null;
			}
		}
		catch (Exception projectError10)
		{
			ProjectData.SetProjectError(projectError10);
			ProjectData.ClearProjectError();
		}
		try
		{
			foreach (KeyValuePair<int, Font> item in sectorFontCache)
			{
				if (item.Value != null)
				{
					try
					{
						item.Value.Dispose();
					}
					catch (Exception projectError11)
					{
						ProjectData.SetProjectError(projectError11);
						ProjectData.ClearProjectError();
					}
				}
			}
			sectorFontCache.Clear();
		}
		catch (Exception projectError12)
		{
			ProjectData.SetProjectError(projectError12);
			ProjectData.ClearProjectError();
		}
	}

	private void CloseMenuSkippingSimulatedMouseUp(string tag)
	{
		skipSimulatedMouseUpOnce = true;
		try
		{
			RequestStartForegroundWatch?.Invoke((tag ?? "").Trim());
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		Close();
	}

	public void NavigateTo(string newMenuName, string parentText)
	{
		shouldStayOpen = true;
		menuStack.Push(new KeyValuePair<string, string>(currentMenuName, Text));
		currentMenuName = newMenuName;
		Text = parentText;
		CurrentPage = 0;
		RefreshCurrentMenuCaches();
		RecalculateTotalPages();
		Invalidate();
	}

	private void NavigateBack()
	{
		if (menuStack.Count != 0)
		{
			KeyValuePair<string, string> keyValuePair = menuStack.Pop();
			currentMenuName = keyValuePair.Key;
			Text = keyValuePair.Value;
			RefreshCurrentMenuCaches();
			RecalculateTotalPages();
			Invalidate();
		}
	}

	private void RecalculateTotalPages()
	{
		List<RadialSector> list = (isGlobalMode ? currentMenuGlobalItems : currentMenuProcessItems);
		int num = -1;
		if (list != null)
		{
			foreach (RadialSector item in list)
			{
				if (item != null && item.Level != 2)
				{
					num = Math.Max(num, item.Page);
				}
			}
		}
		TotalPages = ((num < 0) ? 1 : checked(num + 1));
	}

	private void RefreshCurrentMenuCaches()
	{
		currentMenuGlobalItems = FilterMenuItems(globalSectors, currentMenuName);
		currentMenuProcessItems = FilterMenuItems(processSectors, currentMenuName);
	}

	private static List<RadialSector> FilterMenuItems(List<RadialSector> src, string menuName)
	{
		List<RadialSector> list = new List<RadialSector>();
		if (src == null)
		{
			return list;
		}
		string b = (menuName ?? "").Trim();
		foreach (RadialSector item in src)
		{
			if (item != null && string.Equals((item.ParentMenuName ?? "").Trim(), b, StringComparison.OrdinalIgnoreCase))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public void SetInitialRightButtonState(bool state)
	{
		lastRightButtonState = state;
	}

	public void SetInitialTriggerButtonState(bool state)
	{
		lastRightButtonState = state;
	}

	public bool IsTriggerButtonDown()
	{
		int triggerVk = GetTriggerVk();
		if (triggerVk <= 0)
		{
			return false;
		}
		return (NativeMethods.GetAsyncKeyState(triggerVk) & -32768) != 0;
	}

	private int GetTriggerVk()
	{
		return triggerMouseButton switch
		{
			1 => 1, 
			4 => 4, 
			_ => 2, 
		};
	}

	private int[] GetTriggerDownUpMsgs()
	{
		return triggerMouseButton switch
		{
			1 => new int[2] { 513, 514 }, 
			4 => new int[2] { 519, 520 }, 
			_ => new int[2] { 516, 517 }, 
		};
	}

	public void SetInitialDragState(Point screenPos)
	{
		isDraggingToSector = true;
		dragFeedbackPosition = PointToClient(screenPos);
		if (dragFeedbackPosition.HasValue)
		{
			Point value = dragFeedbackPosition.Value;
			int[] sectorAtPoint = GetSectorAtPoint(value.X, value.Y);
			if (sectorAtPoint[0] >= 0)
			{
				highlightedSectorIndex = sectorAtPoint[0];
				highlightedSectorLevel = sectorAtPoint[1];
				targetSectorIndex = sectorAtPoint[0];
				targetSectorLevel = sectorAtPoint[1];
			}
		}
		Invalidate();
	}

	private void Form_MouseWheel(object sender, MouseEventArgs e)
	{
		Point point = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			int num = e.X - point.X;
			int num2 = e.Y - point.Y;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			double scaledInnerRadius = GetScaledInnerRadius();
			if (num3 < scaledInnerRadius && (Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				double dpiScale = GetDpiScale();
				Rectangle workingArea;
				try
				{
					workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					workingArea = Screen.PrimaryScreen.WorkingArea;
					ProjectData.ClearProjectError();
				}
				int num4 = Math.Max(1, Math.Min(workingArea.Width, workingArea.Height));
				int num5 = Math.Max(240, (int)Math.Floor((double)num4 * 0.45));
				int val = Math.Max(120, (int)Math.Floor((double)num5 / Math.Max(0.0001, menuSize.Scale * dpiScale)));
				if (e.Delta > 0)
				{
					menuSize.OuterRadius = Math.Min(val, menuSize.OuterRadius + 10);
				}
				else
				{
					menuSize.OuterRadius = Math.Max(120, menuSize.OuterRadius - 10);
				}
				menuSize.InnerRadius = (int)Math.Round((double)menuSize.OuterRadius * 0.15);
				UpdateSize();
				Invalidate();
				try
				{
					RadialMenuController.UpsertMenuSizeDpiProfileFromBase(menuSize, base.DeviceDpi);
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				MenuSizeChanged?.Invoke(menuSize);
				return;
			}
			if (string.Equals(currentMenuName, "ROOT", StringComparison.OrdinalIgnoreCase))
			{
				if (!isGlobalMode && e.Delta > 0 && CurrentPage == 0)
				{
					SetGlobalMode(value: true);
					Invalidate();
					return;
				}
				if (isGlobalMode && e.Delta < 0 && processSectors != null && CurrentPage >= TotalPages - 1)
				{
					SetGlobalMode(value: false);
					Invalidate();
					return;
				}
			}
		}
		if (TotalPages > 1)
		{
			if (e.Delta > 0)
			{
				CurrentPage = checked(CurrentPage - 1 + TotalPages) % TotalPages;
			}
			else
			{
				CurrentPage = checked(CurrentPage + 1) % TotalPages;
			}
			Invalidate();
		}
	}

	private void UpdateSize()
	{
		checked
		{
			int num = (int)Math.Round(GetScaledOuterRadius());
			base.Size = new Size(num * 2 + 60, num * 2 + 60);
			UpdateFormRegion();
		}
	}

	private void UpdateFormRegion()
	{
		Point point = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			int num = (int)Math.Round(GetScaledOuterRadius());
			using GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddEllipse(point.X - num, point.Y - num, num * 2, num * 2);
			Region region = base.Region;
			base.Region = new Region(graphicsPath);
			if (region != null)
			{
				try
				{
					region.Dispose();
					return;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
					return;
				}
			}
		}
	}

	private void Form_MouseDown(object sender, MouseEventArgs e)
	{
		Point point = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			int num = e.X - point.X;
			int num2 = e.Y - point.Y;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			double scaledInnerRadius = GetScaledInnerRadius();
			if (true)
			{
				int[] sectorAtPoint = GetSectorAtPoint(e.X, e.Y);
				if (sectorAtPoint[0] >= 0)
				{
					clickedSector = GetSectorsInLevel(sectorAtPoint[1])[sectorAtPoint[0]];
					isDraggingToSector = true;
					dragFeedbackPosition = new Point(e.X, e.Y);
					Invalidate();
					return;
				}
			}
			if (e.Button == MouseButtons.Right && num3 <= scaledInnerRadius)
			{
				isDraggingToSector = true;
				dragStartMousePos = new Point(e.X, e.Y);
				dragFeedbackPosition = new Point(e.X, e.Y);
				Cursor = Cursors.Cross;
				Timer timer = new Timer();
				timer.Interval = 50;
				timer.Tick += [SpecialName] (object s, EventArgs ev) =>
				{
					timer.Stop();
					timer.Dispose();
					animationTimer.Start();
				};
				timer.Start();
			}
		}
	}

	private void Form_MouseMove(object sender, MouseEventArgs e)
	{
		Point point = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			int num = e.X - point.X;
			int num2 = e.Y - point.Y;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			double scaledInnerRadius = GetScaledInnerRadius();
			int num4 = highlightedSectorIndex;
			int num5 = highlightedSectorLevel;
			bool flag = false;
			bool flag2 = lastRightButtonState;
			if (num3 <= scaledInnerRadius)
			{
				flag = true;
			}
			int[] sectorAtPoint = GetSectorAtPoint(e.X, e.Y);
			if (isDraggingToSector || flag2)
			{
				dragFeedbackPosition = new Point(e.X, e.Y);
				flag = true;
			}
			if (sectorAtPoint[0] >= 0)
			{
				highlightedSectorIndex = sectorAtPoint[0];
				highlightedSectorLevel = sectorAtPoint[1];
				if (isDraggingToSector || flag2)
				{
					targetSectorIndex = sectorAtPoint[0];
					targetSectorLevel = sectorAtPoint[1];
				}
			}
			else
			{
				highlightedSectorIndex = -1;
				highlightedSectorLevel = -1;
				if (isDraggingToSector || flag2)
				{
					targetSectorIndex = -1;
					targetSectorLevel = -1;
				}
			}
			SetHoverAnimationTarget(highlightedSectorIndex, highlightedSectorLevel);
			if (num4 != highlightedSectorIndex || num5 != highlightedSectorLevel)
			{
				flag = true;
			}
			if (flag)
			{
				Invalidate();
			}
		}
	}

	private void Form_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			animationTimer.Stop();
			Cursor = Cursors.Default;
			isDragging = false;
			isDraggingToSector = false;
			clickedSector = null;
			TryHandleTriggerReleased("MouseUp");
		}
		else if (e.Button == MouseButtons.Left && !lastRightButtonState)
		{
			if (GetSectorAtPoint(e.X, e.Y)[0] < 0)
			{
				Close();
			}
			else
			{
				HandleRightButtonReleased(PointToScreen(new Point(e.X, e.Y)));
			}
		}
	}

	private void Form_MouseLeave(object sender, EventArgs e)
	{
	}

	private void OnFrameChanged(object sender, EventArgs e)
	{
		Invalidate();
	}

	private void AnimationTimer_Tick(object sender, EventArgs e)
	{
		animationAlpha = ((animationAlpha == 255) ? 128 : 255);
		Invalidate();
	}

	private void SetHoverAnimationTarget(int index, int level)
	{
		if (index != hoverAnimToIndex || level != hoverAnimToLevel)
		{
			hoverAnimFromIndex = hoverAnimToIndex;
			hoverAnimFromLevel = hoverAnimToLevel;
			hoverAnimToIndex = index;
			hoverAnimToLevel = level;
			hoverAnimProgress = 0f;
			hoverAnimLastAt = DateTime.Now;
		}
	}

	private void AdvanceHoverAnimation()
	{
		if (!(hoverAnimProgress >= 1f))
		{
			DateTime now = DateTime.Now;
			if (DateTime.Compare(hoverAnimLastAt, DateTime.MinValue) == 0)
			{
				hoverAnimLastAt = now;
				return;
			}
			float num = (float)(now - hoverAnimLastAt).TotalMilliseconds;
			hoverAnimLastAt = now;
			float num2 = 120f;
			hoverAnimProgress = Math.Min(1f, hoverAnimProgress + num / num2);
		}
	}

	private static float EaseOutCubic(float t)
	{
		if (t <= 0f)
		{
			return 0f;
		}
		if (t >= 1f)
		{
			return 1f;
		}
		float num = 1f - t;
		return 1f - num * num * num;
	}

	private float GetHoverRaiseFactor(int level, int index)
	{
		float num = EaseOutCubic(hoverAnimProgress);
		if (level == hoverAnimToLevel && index == hoverAnimToIndex)
		{
			return num;
		}
		if (level == hoverAnimFromLevel && index == hoverAnimFromIndex)
		{
			return 1f - num;
		}
		return 0f;
	}

	private static int ClampByte(int v)
	{
		if (v < 0)
		{
			return 0;
		}
		if (v > 255)
		{
			return 255;
		}
		return v;
	}

	private static Color ShiftColorRgb(Color c, int delta)
	{
		return checked(Color.FromArgb(ClampByte(c.R + delta), ClampByte(c.G + delta), ClampByte(c.B + delta)));
	}

	private List<int> GetUsedLevels()
	{
		return new List<int>(new int[3] { 0, 1, 2 });
	}

	private List<RadialSector> GetSectorsInLevel(int level)
	{
		int num = ((level == 1) ? 16 : 8);
		List<RadialSector> list = new List<RadialSector>(Enumerable.Repeat<RadialSector>(null, num));
		List<RadialSector> list2 = null;
		list2 = (isGlobalMode ? currentMenuGlobalItems : currentMenuProcessItems);
		if (list2 != null)
		{
			foreach (RadialSector item in list2)
			{
				if (item != null && item.Level == level && (level == 2 || item.Page == CurrentPage) && item.SectorIndex >= 0 && item.SectorIndex < num)
				{
					if (!item.Enabled)
					{
						list[item.SectorIndex] = null;
					}
					else
					{
						list[item.SectorIndex] = item;
					}
				}
			}
		}
		checked
		{
			if (level == 0 && menuStack.Count > 0)
			{
				RadialSector radialSector = list[0];
				list[0] = backSector;
				if (radialSector != null)
				{
					int num2 = num - 1;
					for (int i = 1; i <= num2; i++)
					{
						if (list[i] == null)
						{
							list[i] = radialSector;
							break;
						}
					}
				}
			}
			return list;
		}
	}

	private double[] GetRingStartEnd(int ringIndex, int numRings)
	{
		double scaledInnerRadius = GetScaledInnerRadius();
		double num = (GetScaledOuterRadius() - scaledInnerRadius) / (double)((numRings <= 0) ? 1 : numRings);
		return new double[2]
		{
			scaledInnerRadius + (double)ringIndex * num,
			scaledInnerRadius + (double)checked(ringIndex + 1) * num
		};
	}

	private static void DrawImageCover(Graphics g, Image img, RectangleF dest, double opacity = 1.0)
	{
		if (img == null)
		{
			return;
		}
		float num = img.Width;
		float num2 = img.Height;
		if (num <= 0f || num2 <= 0f || dest.Width <= 0f || dest.Height <= 0f)
		{
			return;
		}
		float num3 = Math.Max(dest.Width / num, dest.Height / num2);
		float num4 = num * num3;
		float num5 = num2 * num3;
		float num6 = dest.X + (dest.Width - num4) / 2f;
		float num7 = dest.Y + (dest.Height - num5) / 2f;
		double num8 = opacity;
		if (double.IsNaN(num8) || double.IsInfinity(num8))
		{
			num8 = 1.0;
		}
		num8 = Math.Max(0.0, Math.Min(1.0, num8));
		if (num8 >= 0.999)
		{
			g.DrawImage(img, new RectangleF(num6, num7, num4, num5));
			return;
		}
		using ImageAttributes imageAttributes = new ImageAttributes();
		ColorMatrix colorMatrix = new ColorMatrix();
		colorMatrix.Matrix33 = (float)num8;
		imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
		Rectangle destRect = Rectangle.Round(new RectangleF(num6, num7, num4, num5));
		g.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttributes);
	}

	private void Form_Paint(object sender, PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Point pt = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			int num = (int)Math.Round(GetScaledInnerRadius());
			float num2 = (float)GetDpiScale();
			AdvanceHoverAnimation();
			bool flag = !menuSize.ShowDividers.HasValue || menuSize.ShowDividers.Value;
			bool flag2 = !menuSize.ShowInnerDividers.HasValue || menuSize.ShowInnerDividers.Value;
			graphics.PixelOffsetMode = (flag ? PixelOffsetMode.HighQuality : PixelOffsetMode.None);
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			if (dragImage != null && ImageAnimator.CanAnimate(dragImage))
			{
				ImageAnimator.UpdateFrames(dragImage);
			}
			float num3 = (float)Math.Ceiling(GetScaledOuterRadius()) + 4f;
			if (dragImage != null && num3 > 0f)
			{
				using GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddEllipse((float)pt.X - num3, (float)pt.Y - num3, num3 * 2f, num3 * 2f);
				GraphicsState gstate = graphics.Save();
				graphics.SetClip(graphicsPath, CombineMode.Replace);
				InterpolationMode interpolationMode = graphics.InterpolationMode;
				try
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					double num4 = 1.0;
					try
					{
						num4 = menuSize.SkinOpacity;
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						num4 = 1.0;
						ProjectData.ClearProjectError();
					}
					num4 = Math.Max(0.0, Math.Min(1.0, num4));
					DrawImageCover(graphics, dragImage, new RectangleF((float)pt.X - num3, (float)pt.Y - num3, num3 * 2f, num3 * 2f), num4);
				}
				finally
				{
					graphics.InterpolationMode = interpolationMode;
				}
				graphics.Restore(gstate);
			}
			using (GraphicsPath graphicsPath2 = new GraphicsPath())
			{
				graphicsPath2.AddEllipse(pt.X - num, pt.Y - num, num * 2, num * 2);
				graphics.SetClip(graphicsPath2);
				if (dragImage == null)
				{
					graphics.Clear(Color.White);
				}
				string s = (isGlobalMode ? "全局" : $"{CurrentPage + 1}/{Math.Max(1, TotalPages)}");
				using (SolidBrush brush = new SolidBrush(Color.Black))
				{
					float num5 = Math.Max(8f, (float)num * 0.4f);
					using (Font font = new Font("Microsoft YaHei", num5, FontStyle.Bold))
					{
						StringFormat format = new StringFormat
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Center
						};
						graphics.DrawString(s, font, brush, pt.X, pt.Y, format);
					}
					string text = (scopeProcessName ?? "").Trim();
					if (text.Length > 0 && !isGlobalMode)
					{
						float emSize = Math.Max(7f, num5 / 2.4f);
						using Font font2 = new Font("Microsoft YaHei", emSize, FontStyle.Regular);
						RectangleF layoutRectangle = new RectangleF((float)pt.X - (float)num * 0.9f, (float)pt.Y + num5 * 0.55f, (float)num * 1.8f, (float)num * 0.8f);
						StringFormat format2 = new StringFormat
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Near
						};
						graphics.DrawString(text, font2, brush, layoutRectangle, format2);
					}
				}
				graphics.ResetClip();
			}
			if (highlightedSectorLevel == 0 && highlightedSectorIndex >= 0 && flag)
			{
				using Pen pen = new Pen(Color.FromArgb(Math.Min(255, (int)Math.Round(220.0 * menuSize.FallbackOpacity)), 0, 0, 0), 1.6f);
				graphics.DrawEllipse(pen, (float)(pt.X - num), (float)(pt.Y - num), (float)(num * 2), (float)(num * 2));
			}
			List<int> usedLevels = GetUsedLevels();
			int count = usedLevels.Count;
			if (count <= 0)
			{
				return;
			}
			Color color = ((!menuSize.OuterRingGray.HasValue || menuSize.OuterRingGray.Value) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White);
			float num6 = 0f;
			try
			{
				int level = usedLevels[count - 1];
				List<RadialSector> sectorsInLevel = GetSectorsInLevel(level);
				double[] ringStartEnd = GetRingStartEnd(count - 1, count);
				double num7 = ringStartEnd[0];
				double num8 = ringStartEnd[1];
				float num9 = (float)(num8 - num7);
				float num10 = (float)((num7 + num8) / 2.0);
				int num11 = Math.Max(1, sectorsInLevel.Count);
				float num12 = 360f / (float)num11;
				float num13 = num10 * (float)((double)num12 * Math.PI / 180.0);
				float val = Math.Max(42f, num13 * 0.88f);
				float val2 = Math.Max(24f, num9 * 0.98f);
				num6 = Math.Min(val, val2) * 0.92f;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				num6 = 0f;
				ProjectData.ClearProjectError();
			}
			using (StringFormat format3 = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Near,
				Trimming = StringTrimming.None
			})
			{
				int num14 = count - 1;
				for (int i = 0; i <= num14; i++)
				{
					int num15 = usedLevels[i];
					List<RadialSector> sectorsInLevel2 = GetSectorsInLevel(num15);
					if (sectorsInLevel2.Count == 0)
					{
						continue;
					}
					double[] ringStartEnd2 = GetRingStartEnd(i, count);
					double num16 = ringStartEnd2[0];
					double num17 = ringStartEnd2[1];
					int count2 = sectorsInLevel2.Count;
					float num18 = 360f / (float)count2;
					int num19 = ((menuSize.FallbackOpacity >= 1.0) ? 255 : ((int)Math.Round(150.0 * menuSize.FallbackOpacity)));
					Color baseColor = ((num15 == 2) ? color : Color.White);
					if (!flag && dragImage == null)
					{
						using GraphicsPath graphicsPath3 = new GraphicsPath();
						graphicsPath3.AddEllipse((float)((double)pt.X - num17), (float)((double)pt.Y - num17), (float)(num17 * 2.0), (float)(num17 * 2.0));
						graphicsPath3.AddEllipse((float)((double)pt.X - num16), (float)((double)pt.Y - num16), (float)(num16 * 2.0), (float)(num16 * 2.0));
						using SolidBrush brush2 = new SolidBrush(Color.FromArgb(num19, baseColor));
						graphics.FillPath(brush2, graphicsPath3);
					}
					Color color2 = Color.FromArgb(Math.Min(255, (int)Math.Round(200.0 * menuSize.FallbackOpacity)), Color.Gray);
					using Pen pen2 = new Pen(color2, 1f);
					if (flag && !flag2)
					{
						graphics.DrawEllipse(pen2, (float)((double)pt.X - num17), (float)((double)pt.Y - num17), (float)(num17 * 2.0), (float)(num17 * 2.0));
					}
					int num20 = count2 - 1;
					for (int j = 0; j <= num20; j++)
					{
						RadialSector radialSector = sectorsInLevel2[j];
						float num21 = num18 * (float)j + 22.5f;
						double num22 = (double)(num21 + num18 / 2f) * Math.PI / 180.0;
						float hoverRaiseFactor = GetHoverRaiseFactor(num15, j);
						float num23 = (float)(num17 - num16);
						float num24 = (float)Math.Min(4.0, Math.Max(1.0, num23 * 0.04f)) * hoverRaiseFactor;
						float num25 = (float)(Math.Cos(num22) * (double)num24);
						float num26 = (float)(Math.Sin(num22) * (double)num24);
						using (GraphicsPath graphicsPath4 = new GraphicsPath())
						{
							graphicsPath4.AddArc((float)((double)pt.X - num17), (float)((double)pt.Y - num17), (float)(num17 * 2.0), (float)(num17 * 2.0), num21, num18);
							graphicsPath4.AddArc((float)((double)pt.X - num16), (float)((double)pt.Y - num16), (float)(num16 * 2.0), (float)(num16 * 2.0), num21 + num18, 0f - num18);
							graphicsPath4.CloseFigure();
							int alpha = num19;
							bool flag3 = false;
							if (radialSector != null)
							{
								if (object.ReferenceEquals(radialSector, backSector))
								{
									flag3 = true;
								}
								else if (radialSector.IsSubMenu)
								{
									flag3 = true;
								}
								else if (string.Equals(radialSector.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(radialSector.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
								{
									flag3 = true;
								}
							}
							Color color3 = radialSector?.FillColor ?? ((num15 != 2) ? Color.White : color);
							Color color4 = color3;
							if (num15 == 2 && !flag3)
							{
								color4 = color;
							}
							Color baseColor2 = color4;
							if (hoverRaiseFactor > 0f && flag3)
							{
								baseColor2 = Color.White;
							}
							Color color5 = Color.FromArgb(alpha, baseColor2);
							bool flag4 = true;
							if (dragImage != null && hoverRaiseFactor <= 0f && !flag3)
							{
								if (baseColor2.ToArgb() == baseColor.ToArgb())
								{
									flag4 = false;
								}
							}
							else if (!flag && hoverRaiseFactor <= 0f && !flag3 && baseColor2.ToArgb() == baseColor.ToArgb())
							{
								flag4 = false;
							}
							if (flag4)
							{
								using SolidBrush brush3 = new SolidBrush(color5);
								graphics.FillPath(brush3, graphicsPath4);
							}
							if (flag && flag2)
							{
								graphics.DrawPath(pen2, graphicsPath4);
							}
							if (hoverRaiseFactor > 0f)
							{
								RectangleF bounds = graphicsPath4.GetBounds();
								int num27 = Math.Min(255, (int)Math.Round((double)(110f * hoverRaiseFactor) * menuSize.FallbackOpacity));
								float num28 = Math.Max(0.6f, num24 * 0.22f);
								if (num27 > 0)
								{
									GraphicsState gstate2 = graphics.Save();
									graphics.TranslateTransform(num25 + num28, num26 + num28);
									using (SolidBrush brush4 = new SolidBrush(Color.FromArgb(num27, 0, 0, 0)))
									{
										graphics.FillPath(brush4, graphicsPath4);
									}
									if (flag && flag2)
									{
										using Pen pen3 = new Pen(Color.FromArgb(Math.Min(255, (int)Math.Round((double)(150f * hoverRaiseFactor) * menuSize.FallbackOpacity)), 0, 0, 0), 1.6f);
										graphics.DrawPath(pen3, graphicsPath4);
									}
									graphics.Restore(gstate2);
								}
								GraphicsState gstate3 = graphics.Save();
								graphics.TranslateTransform(num25, num26);
								Color baseColor3 = ShiftColorRgb(color4, (int)Math.Round(26f * hoverRaiseFactor));
								Color baseColor4 = ShiftColorRgb(color4, (int)Math.Round(-16f * hoverRaiseFactor));
								using (LinearGradientBrush brush5 = new LinearGradientBrush(bounds, Color.FromArgb(alpha, baseColor3), Color.FromArgb(alpha, baseColor4), 45f))
								{
									graphics.FillPath(brush5, graphicsPath4);
								}
								if (flag && flag2)
								{
									using Pen pen4 = new Pen(Color.FromArgb(Math.Min(255, (int)Math.Round(220.0 * menuSize.FallbackOpacity)), Color.Gray), 1.25f);
									graphics.DrawPath(pen4, graphicsPath4);
								}
								graphics.Restore(gstate3);
							}
						}
						if (radialSector == null)
						{
							continue;
						}
						double num29 = (num16 + num17) / 2.0;
						double num30 = (double)pt.X + num29 * Math.Cos(num22) + (double)num25;
						double num31 = (double)pt.Y + num29 * Math.Sin(num22) + (double)num26;
						bool onlyShowIcon = radialSector.OnlyShowIcon;
						string text2 = (onlyShowIcon ? "" : radialSector.Text);
						if (radialSector.IsSubMenu && !onlyShowIcon)
						{
							text2 += " >";
						}
						Brush brush6 = Brushes.Black;
						if (string.Equals(radialSector.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(radialSector.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
						{
							brush6 = Brushes.White;
						}
						float num32 = (float)num29 * (float)((double)num18 * Math.PI / 180.0);
						float num33 = Math.Max(42f, num32 * 0.88f);
						float num34 = Math.Max(24f, num23 * 0.98f);
						RectangleF rectangleF = new RectangleF((float)(num30 - (double)(num33 / 2f)), (float)(num31 - (double)(num34 / 2f)), num33, num34);
						Region region = null;
						try
						{
							region = graphics.Clip.Clone();
							using (GraphicsPath graphicsPath5 = new GraphicsPath())
							{
								graphicsPath5.AddArc((float)((double)pt.X - num17), (float)((double)pt.Y - num17), (float)(num17 * 2.0), (float)(num17 * 2.0), num21, num18);
								graphicsPath5.AddArc((float)((double)pt.X - num16), (float)((double)pt.Y - num16), (float)(num16 * 2.0), (float)(num16 * 2.0), num21 + num18, 0f - num18);
								graphicsPath5.CloseFigure();
								if (num24 != 0f)
								{
									using Matrix matrix = new Matrix();
									matrix.Translate(num25, num26);
									graphicsPath5.Transform(matrix);
								}
								graphics.SetClip(graphicsPath5, CombineMode.Intersect);
							}
							Font sectorFont = GetSectorFont(radialSector.FontSize);
							float num35 = 1f;
							bool flag5 = i == count - 1;
							float maxWidth = Math.Max(10f, rectangleF.Width * (flag5 ? 0.52f : 0.92f));
							float num36 = 24f * num2;
							if (onlyShowIcon)
							{
								float num37 = Math.Min(rectangleF.Width, rectangleF.Height) * 0.92f;
								if (num37 > 0f)
								{
									float num38 = 0.53f;
									float num39 = 1f;
									if (count > 1)
									{
										num39 = 0.8f + 0.2f * (float)i / (float)(count - 1);
									}
									float val3 = ((num6 > 0f) ? num6 : num37) * num39 * num38;
									num36 = Math.Max(8f, Math.Min(val3, num37));
								}
							}
							int num40 = Math.Max(8, (int)Math.Round(num36));
							float num41 = num40;
							Image iconForSector = GetIconForSector(radialSector, num40);
							if (iconForSector != null)
							{
								float num42 = (onlyShowIcon ? 0f : (rectangleF.Height - num41 - num35));
								Font usedFont = null;
								float lineHeight = 0f;
								List<string> list = new List<string>();
								if (!onlyShowIcon && num42 >= 8f)
								{
									list = TextLayoutUtil.FitLines(graphics, text2, sectorFont, maxWidth, num42, ref usedFont, ref lineHeight);
								}
								float num43 = ((list == null) ? 0f : ((float)list.Count * lineHeight));
								float num44 = num41 + ((list != null && list.Count > 0) ? (num35 + num43) : 0f);
								float num45 = rectangleF.Top + (rectangleF.Height - num44) / 2f;
								float num46 = (float)Math.Round(num30 - (double)(num41 / 2f));
								float num47 = (float)Math.Round(num45);
								RectangleF rect = new RectangleF(num46, num47, num41, num41);
								graphics.DrawImage(iconForSector, rect);
								if (list != null && list.Count > 0)
								{
									float num48 = rect.Bottom + num35;
									foreach (string item in list)
									{
										RectangleF layoutRectangle2 = new RectangleF(rectangleF.Left, num48, rectangleF.Width, lineHeight);
										graphics.DrawString(item, usedFont, brush6, layoutRectangle2, format3);
										num48 += lineHeight;
									}
								}
								if (usedFont != null && !object.ReferenceEquals(usedFont, sectorFont))
								{
									try
									{
										usedFont.Dispose();
									}
									catch (Exception projectError3)
									{
										ProjectData.SetProjectError(projectError3);
										ProjectData.ClearProjectError();
									}
								}
							}
							else if (!onlyShowIcon)
							{
								Font usedFont2 = null;
								float lineHeight2 = 0f;
								List<string> list2 = TextLayoutUtil.FitLines(graphics, text2, sectorFont, maxWidth, rectangleF.Height, ref usedFont2, ref lineHeight2);
								float num49 = (float)list2.Count * lineHeight2;
								float num50 = rectangleF.Top + (rectangleF.Height - num49) / 2f;
								foreach (string item2 in list2)
								{
									RectangleF layoutRectangle3 = new RectangleF(rectangleF.Left, num50, rectangleF.Width, lineHeight2);
									graphics.DrawString(item2, usedFont2, brush6, layoutRectangle3, format3);
									num50 += lineHeight2;
								}
								if (usedFont2 != null && !object.ReferenceEquals(usedFont2, sectorFont))
								{
									try
									{
										usedFont2.Dispose();
									}
									catch (Exception projectError4)
									{
										ProjectData.SetProjectError(projectError4);
										ProjectData.ClearProjectError();
									}
								}
							}
							if (HasExtraQuickerMenu(radialSector))
							{
								float num51 = Math.Min(5f, Math.Max(2f, num23 * 0.06f));
								float num52 = (float)(num16 + (num17 - num16) * 0.11999999731779099);
								float num53 = (float)((double)pt.X + (double)num52 * Math.Cos(num22) + (double)num25);
								float num54 = (float)((double)pt.Y + (double)num52 * Math.Sin(num22) + (double)num26);
								RectangleF rect2 = new RectangleF(num53 - num51 / 2f, num54 - num51 / 2f, num51, num51);
								using (SolidBrush brush7 = new SolidBrush(Color.FromArgb(80, Color.Red)))
								{
									graphics.FillEllipse(brush7, rect2);
								}
								using Pen pen5 = new Pen(Color.White, 1f);
								graphics.DrawEllipse(pen5, rect2);
							}
						}
						finally
						{
							if (region != null)
							{
								graphics.SetClip(region, CombineMode.Replace);
								region.Dispose();
							}
							else
							{
								graphics.ResetClip();
							}
						}
					}
				}
			}
			if (hoverAnimProgress < 1f)
			{
				Invalidate();
			}
			if ((isDraggingToSector || lastRightButtonState) && dragFeedbackPosition.HasValue)
			{
				using (Pen pen6 = new Pen(Color.FromArgb(animationAlpha, Color.DeepSkyBlue), 2f))
				{
					pen6.DashStyle = DashStyle.Dash;
					graphics.DrawLine(pen6, pt, dragFeedbackPosition.Value);
				}
			}
		}
	}

	private int[] GetSectorAtPoint(int x, int y)
	{
		Point point = new Point(base.Width / 2, base.Height / 2);
		checked
		{
			double num = x - point.X;
			double num2 = y - point.Y;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			double num4 = (Math.Atan2(num2, num) * 180.0 / Math.PI + 360.0) % 360.0;
			num4 = (num4 - 22.5 + 360.0) % 360.0;
			double scaledInnerRadius = GetScaledInnerRadius();
			if (num3 < scaledInnerRadius)
			{
				return new int[2] { -1, -1 };
			}
			List<int> usedLevels = GetUsedLevels();
			int count = usedLevels.Count;
			double scaledOuterRadius = GetScaledOuterRadius();
			double num5 = (scaledOuterRadius - scaledInnerRadius) / (double)((count <= 0) ? 1 : count);
			int index;
			if (num3 >= scaledOuterRadius)
			{
				int num6 = usedLevels.IndexOf(2);
				index = ((num6 < 0) ? (count - 1) : num6);
			}
			else
			{
				index = (int)Math.Floor((num3 - scaledInnerRadius) / num5);
				index = Math.Max(0, Math.Min(count - 1, index));
			}
			int num7 = usedLevels[index];
			int count2 = GetSectorsInLevel(num7).Count;
			if (count2 <= 0)
			{
				return new int[2] { -1, -1 };
			}
			int num8 = (int)Math.Floor(num4 / (360.0 / (double)count2));
			if (num8 < 0)
			{
				num8 = 0;
			}
			if (num8 >= count2)
			{
				num8 = 0;
			}
			return new int[2] { num8, num7 };
		}
	}

	private void CheckRightButtonState(object sender, EventArgs e)
	{
		bool flag = (NativeMethods.GetAsyncKeyState(GetTriggerVk()) & -32768) != 0;
		if (lastRightButtonState && !flag)
		{
			TryHandleTriggerReleased("Timer");
		}
		if (!lastRightButtonState && flag)
		{
			releaseHandledForThisPress = false;
		}
		lastRightButtonState = flag;
	}

	private void TryHandleTriggerReleased(string source, Point? screenPoint = null)
	{
		DateTime now = DateTime.Now;
		if (!releaseHandling && !releaseHandledForThisPress && !((now - lastReleaseHandledAt).TotalMilliseconds < 150.0) && !((now - showTime).TotalMilliseconds <= 80.0))
		{
			releaseHandledForThisPress = true;
			lastReleaseHandledAt = now;
			Logger.Log("TriggerRelease: " + source);
			HandleRightButtonReleased(screenPoint);
		}
	}

	private void HandleRightButtonReleased(Point? screenPoint = null)
	{
		if (releaseHandling)
		{
			return;
		}
		releaseHandling = true;
		try
		{
			Point point = ((!screenPoint.HasValue) ? PointToClient(Cursor.Position) : PointToClient(screenPoint.Value));
			Logger.Log(string.Format("HandleRightButtonReleased: Screen={0}, Client={1}", screenPoint.HasValue ? screenPoint.Value.ToString() : "Cursor", point));
			if (quickerMenuActive)
			{
				lastRightButtonState = false;
				return;
			}
			int num = targetSectorIndex;
			int num2 = targetSectorLevel;
			int[] sectorAtPoint = GetSectorAtPoint(point.X, point.Y);
			if (sectorAtPoint[0] >= 0 && sectorAtPoint[1] >= 0)
			{
				targetSectorIndex = sectorAtPoint[0];
				targetSectorLevel = sectorAtPoint[1];
			}
			if (targetSectorIndex < 0 || targetSectorLevel < 0)
			{
				Logger.Log($"HandleRightButtonReleased: Invalid target. ClientPos={point}, Sector={sectorAtPoint[0]}/{sectorAtPoint[1]}");
				CloseMenuSkippingSimulatedMouseUp("InvalidTarget");
				return;
			}
			RadialSector radialSector = GetSectorsInLevel(targetSectorLevel)[targetSectorIndex];
			if (radialSector == null && num >= 0)
			{
				try
				{
					RadialSector radialSector2 = GetSectorsInLevel(num2)[num];
					if (radialSector2 != null)
					{
						Logger.Log($"HandleRightButtonReleased: Recalculated sector is Nothing (Level={targetSectorLevel}, Index={targetSectorIndex}), using stored sector (Level={num2}, Index={num})");
						radialSector = radialSector2;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Log("HandleRightButtonReleased: Error retrieving stored sector: " + ex2.Message);
					ProjectData.ClearProjectError();
				}
			}
			if (radialSector == null)
			{
				List<RadialSector> sectorsInLevel = GetSectorsInLevel(targetSectorLevel);
				string text = string.Join(", ", sectorsInLevel.Select([SpecialName] (RadialSector s) => (s != null) ? s.Text : "null").ToArray());
				Logger.Log($"HandleRightButtonReleased: Sector is Nothing. Level={targetSectorLevel}, Index={targetSectorIndex}, Menu={currentMenuName}, Page={CurrentPage}, Global={isGlobalMode}, Sectors=[{text}]");
				CloseMenuSkippingSimulatedMouseUp("EmptySector L=" + targetSectorLevel + " I=" + targetSectorIndex);
			}
			else if (radialSector == backSector)
			{
				shouldStayOpen = true;
				NavigateBack();
				shouldStayOpen = false;
				lastRightButtonState = false;
				rightButtonCheckTimer.Start();
			}
			else if (radialSector.IsSubMenu && !string.IsNullOrEmpty(radialSector.SubMenuName))
			{
				shouldStayOpen = true;
				NavigateTo(radialSector.SubMenuName, radialSector.Text);
				shouldStayOpen = false;
				lastRightButtonState = false;
				rightButtonCheckTimer.Start();
			}
			else if (string.Equals(radialSector.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
			{
				RequestOpenSettings?.Invoke(string.IsNullOrEmpty(scopeProcessName) ? "" : scopeProcessName);
				CloseMenuSkippingSimulatedMouseUp("_SETTINGS");
			}
			else if (string.Equals(radialSector.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
			{
				RequestExitApp?.Invoke();
				CloseMenuSkippingSimulatedMouseUp("_EXIT");
			}
			else
			{
				ActionTriggered?.Invoke(radialSector.Action);
				CloseMenuSkippingSimulatedMouseUp("ActionTriggered");
			}
		}
		finally
		{
			releaseHandling = false;
		}
	}

	private void FollowCursorTimer_Tick(object sender, EventArgs e)
	{
		int triggerVk = GetTriggerVk();
		bool flag = false;
		if (triggerVk > 0)
		{
			flag = (NativeMethods.GetAsyncKeyState(triggerVk) & -32768) != 0;
		}
		if (!isDraggingToSector && !lastRightButtonState && !flag)
		{
			lastSpaceKeyDown = false;
			return;
		}
		bool flag2 = (NativeMethods.GetAsyncKeyState(32) & -32768) != 0;
		if (flag2 && !lastSpaceKeyDown && !quickerMenuActive)
		{
			Logger.Log("SpacePoll: SPACE edge while triggerDown=True");
			ShowQuickerMenuUnderCursor();
		}
		lastSpaceKeyDown = flag2;
		DateTime now = DateTime.Now;
		if ((now - lastGlobalMoveUpdateAt).TotalMilliseconds < 12.0)
		{
			return;
		}
		lastGlobalMoveUpdateAt = now;
		Point position = Cursor.Position;
		dragFeedbackPosition = PointToClient(position);
		if (dragFeedbackPosition.HasValue)
		{
			Point value = dragFeedbackPosition.Value;
			int[] sectorAtPoint = GetSectorAtPoint(value.X, value.Y);
			if (sectorAtPoint[0] >= 0)
			{
				highlightedSectorIndex = sectorAtPoint[0];
				highlightedSectorLevel = sectorAtPoint[1];
				targetSectorIndex = sectorAtPoint[0];
				targetSectorLevel = sectorAtPoint[1];
				SetHoverAnimationTarget(sectorAtPoint[0], sectorAtPoint[1]);
			}
			else
			{
				targetSectorIndex = -1;
				targetSectorLevel = -1;
				SetHoverAnimationTarget(-1, -1);
			}
		}
		Invalidate();
	}

	private void UpdateDragStateFromGlobalState()
	{
		dragFeedbackPosition = PointToClient(Cursor.Position);
	}

	private void InstallMouseHook()
	{
		try
		{
			mouseHookOwner = new WeakReference(this);
			if (mouseHookProc == null)
			{
				mouseHookProc = MouseHookCallback;
			}
			if (mouseHookId != IntPtr.Zero)
			{
				UnhookWindowsHookEx(mouseHookId);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				zero = GetModuleHandle(null);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				zero = IntPtr.Zero;
				ProjectData.ClearProjectError();
			}
			if (zero == IntPtr.Zero)
			{
				try
				{
					zero = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					zero = IntPtr.Zero;
					Logger.Log("InstallMouseHook: GetModuleHandle EX: " + ex2.ToString());
					ProjectData.ClearProjectError();
				}
			}
			mouseHookId = SetWindowsHookEx(14, mouseHookProc, zero, 0u);
			Logger.Log("InstallMouseHook: hookId=" + mouseHookId + ", module=" + zero);
			if (mouseHookId == IntPtr.Zero)
			{
				Logger.Log("InstallMouseHook: FAILED err=" + Marshal.GetLastWin32Error());
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Log("InstallMouseHook: EX: " + ex4.ToString());
			ProjectData.ClearProjectError();
		}
	}

	private void UninstallMouseHook()
	{
		try
		{
			if (mouseHookId != IntPtr.Zero)
			{
				UnhookWindowsHookEx(mouseHookId);
				mouseHookId = IntPtr.Zero;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Log("UninstallMouseHook: EX: " + ex2.ToString());
			ProjectData.ClearProjectError();
		}
	}

	private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
	{
		try
		{
			if (nCode >= 0 && mouseHookOwner != null && mouseHookOwner.IsAlive)
			{
				RadialSectorMenuForm radialSectorMenuForm = mouseHookOwner.Target as RadialSectorMenuForm;
				if (radialSectorMenuForm == null || radialSectorMenuForm.IsDisposed)
				{
					return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
				}
				if (!radialSectorMenuForm.menuActive)
				{
					return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
				}
				int num = wParam.ToInt32();
				int[] triggerDownUpMsgs = radialSectorMenuForm.GetTriggerDownUpMsgs();
				int num2 = triggerDownUpMsgs[0];
				int num3 = triggerDownUpMsgs[1];
				if (num == num2 || num == num3)
				{
					object obj = Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
					MSLLHOOKSTRUCT mSLLHOOKSTRUCT = ((obj != null) ? ((MSLLHOOKSTRUCT)obj) : default(MSLLHOOKSTRUCT));
					radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
					{
						if (num == num2)
						{
							radialSectorMenuForm.lastRightButtonState = true;
							radialSectorMenuForm.releaseHandledForThisPress = false;
						}
						else if (num == num3)
						{
							radialSectorMenuForm.lastRightButtonState = false;
							radialSectorMenuForm.TryHandleTriggerReleased("Hook", mSLLHOOKSTRUCT.pt);
						}
					}));
					// 轮盘激活期间，吞掉触发键事件，不传递给目标应用
					// 避免 Altium Designer 等应用因接收到右键拖拽事件导致内部状态异常，
					// 进而忽略后续发送的快捷键
					return (IntPtr)1;
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
	}

	private void InstallKeyboardHook()
	{
		try
		{
			keyboardHookOwner = new WeakReference(this);
			if (keyboardHookProc == null)
			{
				keyboardHookProc = KeyboardHookCallback;
			}
			if (keyboardHookId != IntPtr.Zero)
			{
				UnhookWindowsHookEx(keyboardHookId);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				zero = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				zero = IntPtr.Zero;
				Logger.Log("InstallKeyboardHook: GetModuleHandle EX: " + ex2.ToString());
				ProjectData.ClearProjectError();
			}
			keyboardHookId = SetWindowsHookEx(13, keyboardHookProc, zero, 0u);
			Logger.Log("InstallKeyboardHook: hookId=" + keyboardHookId + ", module=" + zero);
			if (keyboardHookId == IntPtr.Zero)
			{
				Logger.Log("InstallKeyboardHook: FAILED GetLastWin32Error=" + Marshal.GetLastWin32Error());
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Log("InstallKeyboardHook: EX: " + ex4.ToString());
			ProjectData.ClearProjectError();
		}
	}

	private void UninstallKeyboardHook()
	{
		try
		{
			if (keyboardHookId != IntPtr.Zero)
			{
				UnhookWindowsHookEx(keyboardHookId);
				keyboardHookId = IntPtr.Zero;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
	{
		checked
		{
			try
			{
				if (nCode >= 0 && keyboardHookOwner != null && keyboardHookOwner.IsAlive)
				{
					RadialSectorMenuForm radialSectorMenuForm = keyboardHookOwner.Target as RadialSectorMenuForm;
					if (radialSectorMenuForm == null || radialSectorMenuForm.IsDisposed)
					{
						return CallNextHookEx(keyboardHookId, nCode, wParam, lParam);
					}
					if (!radialSectorMenuForm.menuActive)
					{
						return CallNextHookEx(keyboardHookId, nCode, wParam, lParam);
					}
					int num = wParam.ToInt32();
					if (num == 256 || num == 260)
					{
						object obj = Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
						KBDLLHOOKSTRUCT kBDLLHOOKSTRUCT = ((obj != null) ? ((KBDLLHOOKSTRUCT)obj) : default(KBDLLHOOKSTRUCT));
						if (radialSectorMenuForm.quickerMenuActive)
						{
							int num2 = (int)kBDLLHOOKSTRUCT.vkCode;
							switch (num2)
							{
							case 69:
								if (radialSectorMenuForm.quickerMenuEditHotItem == null)
								{
									break;
								}
								radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
								{
									try
									{
										radialSectorMenuForm.quickerMenuEditHotItem.PerformClick();
									}
									catch (Exception projectError2)
									{
										ProjectData.SetProjectError(projectError2);
										ProjectData.ClearProjectError();
									}
								}));
								return (IntPtr)1;
							case 82:
								if (radialSectorMenuForm.quickerMenuDebugHotItem == null)
								{
									break;
								}
								radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
								{
									try
									{
										radialSectorMenuForm.quickerMenuDebugHotItem.PerformClick();
									}
									catch (Exception projectError2)
									{
										ProjectData.SetProjectError(projectError2);
										ProjectData.ClearProjectError();
									}
								}));
								return (IntPtr)1;
							}
							int num3 = 0;
							if (num2 >= 49 && num2 <= 57)
							{
								num3 = num2 - 48;
							}
							else if (num2 >= 97 && num2 <= 105)
							{
								num3 = num2 - 96;
							}
							else if (num2 == 27)
							{
								radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
								{
									try
									{
										if (radialSectorMenuForm.quickerMenu != null)
										{
											radialSectorMenuForm.quickerMenu.Close();
										}
									}
									catch (Exception projectError2)
									{
										ProjectData.SetProjectError(projectError2);
										ProjectData.ClearProjectError();
									}
								}));
								return (IntPtr)1;
							}
							if (num3 > 0)
							{
								List<ToolStripMenuItem> list = radialSectorMenuForm.quickerMenuHotkeyItems;
								if (list != null && num3 <= list.Count)
								{
									radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
									{
										radialSectorMenuForm.TryInvokeQuickerMenuHotkey(num3);
									}));
									return (IntPtr)1;
								}
							}
						}
						if (kBDLLHOOKSTRUCT.vkCode == 32)
						{
							int triggerVk = radialSectorMenuForm.GetTriggerVk();
							bool flag = radialSectorMenuForm.isDraggingToSector || radialSectorMenuForm.lastRightButtonState;
							if (!flag && triggerVk > 0)
							{
								flag = (NativeMethods.GetAsyncKeyState(triggerVk) & -32768) != 0;
							}
							bool flag2 = (NativeMethods.GetAsyncKeyState(2) & -32768) != 0;
							Logger.Log("KeyboardHook: SPACE down. triggerVk=" + triggerVk + ", triggerDown=" + flag + ", rightDown=" + flag2 + ", quickerMenuActive=" + radialSectorMenuForm.quickerMenuActive);
							if ((flag || flag2) && !radialSectorMenuForm.quickerMenuActive)
							{
								radialSectorMenuForm.BeginInvoke((Action)([SpecialName] () =>
								{
									Logger.Log("KeyboardHook: BeginInvoke ShowQuickerMenuUnderCursor");
									radialSectorMenuForm.ShowQuickerMenuUnderCursor();
								}));
								return (IntPtr)1;
							}
						}
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				Logger.Log("KeyboardHook: EX");
				ProjectData.ClearProjectError();
			}
			return CallNextHookEx(keyboardHookId, nCode, wParam, lParam);
		}
	}

	private void ShowQuickerMenuUnderCursor()
	{
		if (!quickerMenuActive)
		{
			Point point = PointToClient(Cursor.Position);
			Logger.Log("ShowQuickerMenuUnderCursor: cursorScreen=" + Cursor.Position.ToString() + ", cursorClient=" + point.ToString());
			int[] sectorAtPoint = GetSectorAtPoint(point.X, point.Y);
			Logger.Log("ShowQuickerMenuUnderCursor: sectorInfo=" + sectorAtPoint[0] + "/" + sectorAtPoint[1]);
			if (sectorAtPoint[0] >= 0 && sectorAtPoint[1] >= 0)
			{
				targetSectorIndex = sectorAtPoint[0];
				targetSectorLevel = sectorAtPoint[1];
			}
			ShowQuickerMenuForCurrentTarget(fromSpace: true);
		}
	}

	private void ShowQuickerMenuForCurrentTarget(bool fromSpace = false)
	{
		_Closure_0024__197_002D0 arg = default(_Closure_0024__197_002D0);
		_Closure_0024__197_002D0 CS_0024_003C_003E8__locals20 = new _Closure_0024__197_002D0(arg);
		CS_0024_003C_003E8__locals20._0024VB_0024Me = this;
		if (quickerMenuActive)
		{
			Logger.Log("ShowQuickerMenuForCurrentTarget: skipped quickerMenuActive=True");
			return;
		}
		if (targetSectorIndex < 0 || targetSectorLevel < 0)
		{
			Logger.Log("ShowQuickerMenuForCurrentTarget: skipped invalid target " + targetSectorIndex + "/" + targetSectorLevel);
			return;
		}
		RadialSector radialSector = null;
		try
		{
			radialSector = GetSectorsInLevel(targetSectorLevel)[targetSectorIndex];
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			radialSector = null;
			ProjectData.ClearProjectError();
		}
		if (radialSector == null)
		{
			Logger.Log("ShowQuickerMenuForCurrentTarget: sector is Nothing");
			return;
		}
		if (radialSector.QuickerMenuItems == null || radialSector.QuickerMenuItems.Count == 0)
		{
			Logger.Log("ShowQuickerMenuForCurrentTarget: no QuickerMenuItems for sector=" + radialSector.Text);
			return;
		}
		quickerMenuActionId = "";
		try
		{
			string text = (radialSector.Command ?? "").Trim();
			if (text.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = text.Substring("QCAD_ACTION:".Length).Trim();
				int num = text2.IndexOf('?');
				if (num < 0)
				{
					quickerMenuActionId = text2.Trim();
				}
				else
				{
					quickerMenuActionId = text2.Substring(0, num).Trim();
				}
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			quickerMenuActionId = "";
			ProjectData.ClearProjectError();
		}
		List<QuickerRightClickMenuItemConfig> list = radialSector.QuickerMenuItems.Where([SpecialName] (QuickerRightClickMenuItemConfig x) => x != null).ToList();
		if (list.Count == 0)
		{
			Logger.Log("ShowQuickerMenuForCurrentTarget: items filtered to 0 for sector=" + radialSector.Text);
			return;
		}
		Logger.Log("ShowQuickerMenuForCurrentTarget: sector=" + radialSector.Text + ", items=" + list.Count);
		try
		{
			if (quickerMenu != null)
			{
				ContextMenuStrip contextMenuStrip = quickerMenu;
				quickerMenu = null;
				contextMenuStrip.Close();
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			quickerMenu = null;
			ProjectData.ClearProjectError();
		}
		CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu = new ContextMenuStrip();
		quickerMenu = CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu;
		quickerMenuActive = true;
		quickerMenuHotkeyItems = null;
		quickerMenuEditHotItem = null;
		quickerMenuDebugHotItem = null;
		quickerMenuIconSize = 16;
		if (fromSpace)
		{
			quickerMenuIconSize = 24;
			try
			{
				Font font = new Font(SystemFonts.MenuFont.FontFamily, SystemFonts.MenuFont.SizeInPoints + 3f, SystemFonts.MenuFont.Style);
				CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.Font = font;
				CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.Tag = font;
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				ProjectData.ClearProjectError();
			}
			try
			{
				CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.ImageScalingSize = new Size(24, 24);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
		}
		CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.Closed += [SpecialName] (object a0, ToolStripDropDownClosedEventArgs a1) =>
		{
			CS_0024_003C_003E8__locals20._Lambda_0024__1();
		};
		Dictionary<string, ToolStripMenuItem> dictionary = new Dictionary<string, ToolStripMenuItem>(StringComparer.OrdinalIgnoreCase);
		Dictionary<QuickerRightClickMenuItemConfig, ToolStripMenuItem> dictionary2 = new Dictionary<QuickerRightClickMenuItemConfig, ToolStripMenuItem>();
		List<ToolStripMenuItem> list2 = new List<ToolStripMenuItem>();
		foreach (QuickerRightClickMenuItemConfig item in list)
		{
			if (item.IsGroupHeader)
			{
				string text3 = item.DisplayText ?? "";
				if (text3.Length != 0)
				{
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text3);
					toolStripMenuItem.Enabled = true;
					toolStripMenuItem.Image = CreateMenuIcon(item.Icon, text3);
					dictionary[text3] = toolStripMenuItem;
					dictionary2[item] = toolStripMenuItem;
				}
			}
		}
		foreach (QuickerRightClickMenuItemConfig item2 in list)
		{
			if (item2.IsGroupHeader)
			{
				if (dictionary2.ContainsKey(item2))
				{
					ToolStripMenuItem toolStripMenuItem2 = dictionary2[item2];
					if (!quickerMenu.Items.Contains(toolStripMenuItem2))
					{
						quickerMenu.Items.Add(toolStripMenuItem2);
						list2.Add(toolStripMenuItem2);
					}
				}
			}
			else if (string.Equals(item2.Marker, "-", StringComparison.Ordinal) && !string.IsNullOrEmpty(item2.GroupParent) && dictionary.ContainsKey(item2.GroupParent))
			{
				ToolStripMenuItem toolStripMenuItem3 = dictionary[item2.GroupParent];
				ToolStripMenuItem value = CreateMenuItem(item2);
				toolStripMenuItem3.DropDownItems.Add(value);
			}
			else
			{
				ToolStripMenuItem toolStripMenuItem4 = CreateMenuItem(item2);
				quickerMenu.Items.Add(toolStripMenuItem4);
				if (toolStripMenuItem4 != null && toolStripMenuItem4.Enabled && toolStripMenuItem4.Tag != null && !IsQuickerSystemMenuParam(Convert.ToString(RuntimeHelpers.GetObjectValue(toolStripMenuItem4.Tag))))
				{
					list2.Add(toolStripMenuItem4);
				}
			}
		}
		checked
		{
			if (fromSpace && list2.Count > 0 && list2.Count <= 9)
			{
				int num2 = list2.Count - 1;
				for (int num3 = 0; num3 <= num2; num3++)
				{
					ToolStripMenuItem toolStripMenuItem5 = list2[num3];
					if (toolStripMenuItem5 != null)
					{
						ApplyQuickerHotkeyIndicator(toolStripMenuItem5, (num3 + 1).ToString());
					}
				}
				quickerMenuHotkeyItems = list2;
			}
			int num4 = -1;
			int num5 = -1;
			bool flag = false;
			int num6 = quickerMenu.Items.Count - 1;
			for (int num7 = 0; num7 <= num6; num7++)
			{
				if (!(quickerMenu.Items[num7] is ToolStripMenuItem { Tag: not null } toolStripMenuItem6))
				{
					continue;
				}
				string text4 = Convert.ToString(RuntimeHelpers.GetObjectValue(toolStripMenuItem6.Tag));
				if (!string.IsNullOrWhiteSpace(text4))
				{
					string text5 = text4.Trim().ToLowerInvariant();
					if (text5.StartsWith("quicker:runaction:无限轮盘?sp=编辑动作&targetid=", StringComparison.Ordinal))
					{
						quickerMenuEditHotItem = toolStripMenuItem6;
						toolStripMenuItem6.Text = "编辑动作";
						num4 = num7;
						ApplyQuickerHotkeyIndicator(toolStripMenuItem6, "E");
					}
					else if (text5.StartsWith("quicker:runaction:无限轮盘?sp=悬浮动作&targetid=", StringComparison.Ordinal))
					{
						flag = true;
						ApplyQuickerHotkeyIndicator(toolStripMenuItem6, "F");
					}
					else if (text5.StartsWith("quicker:debugaction:", StringComparison.Ordinal))
					{
						quickerMenuDebugHotItem = toolStripMenuItem6;
						num5 = num7;
						ApplyQuickerHotkeyIndicator(toolStripMenuItem6, "R");
					}
				}
			}
			if (fromSpace && !flag)
			{
				string text6 = (quickerMenuActionId ?? "").Trim();
				if (text6.Length > 0)
				{
					_Closure_0024__197_002D1 arg2 = default(_Closure_0024__197_002D1);
					_Closure_0024__197_002D1 CS_0024_003C_003E8__locals21 = new _Closure_0024__197_002D1(arg2);
					CS_0024_003C_003E8__locals21._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals20;
					CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi = new ToolStripMenuItem("悬浮动作");
					CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi.Enabled = true;
					CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi.Tag = "quicker:runaction:无限轮盘?sp=悬浮动作&targetid=" + text6;
					CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi.Image = CreateMenuIcon("", "悬");
					ApplyQuickerHotkeyIndicator(CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi, "F");
					CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi.Click += [SpecialName] (object a0, EventArgs a1) =>
					{
						CS_0024_003C_003E8__locals21._Lambda_0024__3();
					};
					int val = ((num5 >= 0) ? num5 : ((num4 >= 0) ? (num4 + 1) : quickerMenu.Items.Count));
					val = Math.Max(0, Math.Min(val, quickerMenu.Items.Count));
					quickerMenu.Items.Insert(val, CS_0024_003C_003E8__locals21._0024VB_0024Local_hoverMi);
				}
			}
			try
			{
				CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.Show(Cursor.Position);
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				try
				{
					CS_0024_003C_003E8__locals20._0024VB_0024Local_newMenu.Show(this, PointToClient(Cursor.Position));
				}
				catch (Exception projectError7)
				{
					ProjectData.SetProjectError(projectError7);
					quickerMenuActive = false;
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
			}
		}
	}

	private ToolStripMenuItem CreateMenuItem(QuickerRightClickMenuItemConfig it)
	{
		_Closure_0024__198_002D0 CS_0024_003C_003E8__locals8 = new _Closure_0024__198_002D0();
		CS_0024_003C_003E8__locals8._0024VB_0024Me = this;
		string text = it.DisplayText ?? "";
		if (!string.IsNullOrEmpty(it.DisplayDescription))
		{
			text = text + " (" + it.DisplayDescription + ")";
		}
		CS_0024_003C_003E8__locals8._0024VB_0024Local_mi = new ToolStripMenuItem(text);
		string text2 = it.Parameter ?? "";
		string text3 = (text2 ?? "").Trim().ToLowerInvariant();
		if (text3.Length > 0 && !text3.StartsWith("quicker:") && !text3.StartsWith("runaction:") && !text3.StartsWith("debugaction:") && !text3.StartsWith("previewaction:") && !text3.StartsWith("settings:") && !text3.StartsWith("exesettings:") && !text3.StartsWith("installskin:") && text3.IndexOf("://", StringComparison.OrdinalIgnoreCase) < 0)
		{
			string text4 = (quickerMenuActionId ?? "").Trim();
			if (text4.Length > 0)
			{
				text2 = "runaction:" + text4 + "?" + text2.Trim();
			}
		}
		if (Regex.IsMatch(text2, "^\\s*\\d+\\s*$"))
		{
			string text5 = (quickerMenuActionId ?? "").Trim();
			if (text5.Length > 0)
			{
				text2 = "runaction:" + text5 + "?" + text2.Trim();
			}
		}
		CS_0024_003C_003E8__locals8._0024VB_0024Local_mi.Tag = text2;
		CS_0024_003C_003E8__locals8._0024VB_0024Local_mi.Enabled = !string.IsNullOrWhiteSpace(it.Parameter ?? "");
		CS_0024_003C_003E8__locals8._0024VB_0024Local_mi.Image = CreateMenuIcon(it.Icon, it.DisplayText ?? "");
		CS_0024_003C_003E8__locals8._0024VB_0024Local_mi.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			CS_0024_003C_003E8__locals8._Lambda_0024__0();
		};
		return CS_0024_003C_003E8__locals8._0024VB_0024Local_mi;
	}

	private Image CreateMenuIcon(string iconSpec, string fallbackText)
	{
		string text = (iconSpec ?? "").Trim();
		Image result;
		if (text.Length == 0)
		{
			result = RadialMenuController.CreateLetterIcon(fallbackText, quickerMenuIconSize);
		}
		else if (text.StartsWith("fa:", StringComparison.OrdinalIgnoreCase))
		{
			result = RadialMenuController.CreateLetterIcon(fallbackText, quickerMenuIconSize);
		}
		else
		{
			if (text.StartsWith("url:", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(4).Trim();
			}
			if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || File.Exists(text))
			{
				try
				{
					Image image = RadialMenuController.LoadIcon(text);
					if (image == null)
					{
						result = RadialMenuController.CreateLetterIcon(fallbackText, quickerMenuIconSize);
					}
					else
					{
						Bitmap bitmap = new Bitmap(image, new Size(quickerMenuIconSize, quickerMenuIconSize));
						try
						{
							image.Dispose();
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
						result = bitmap;
					}
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					result = RadialMenuController.CreateLetterIcon(fallbackText, quickerMenuIconSize);
					ProjectData.ClearProjectError();
				}
			}
			else
			{
				result = RadialMenuController.CreateLetterIcon(fallbackText, quickerMenuIconSize);
			}
		}
		return result;
	}

	private static bool IsQuickerSystemMenuParam(string param)
	{
		string text = (param ?? "").Trim();
		if (text.Length == 0)
		{
			return false;
		}
		string text2 = text.ToLowerInvariant();
		if (text2.StartsWith("quicker:debugaction:", StringComparison.Ordinal))
		{
			return true;
		}
		if (text2.StartsWith("quicker:runaction:无限轮盘?sp=编辑动作&targetid=", StringComparison.Ordinal))
		{
			return true;
		}
		if (text2.StartsWith("quicker:runaction:无限轮盘?sp=悬浮动作&targetid=", StringComparison.Ordinal))
		{
			return true;
		}
		return false;
	}

	private static bool HasExtraQuickerMenu(RadialSector sector)
	{
		if (sector == null)
		{
			return false;
		}
		List<QuickerRightClickMenuItemConfig> quickerMenuItems = sector.QuickerMenuItems;
		if (quickerMenuItems == null || quickerMenuItems.Count == 0)
		{
			return false;
		}
		foreach (QuickerRightClickMenuItemConfig item in quickerMenuItems)
		{
			if (item != null && !item.IsGroupHeader)
			{
				string text = (item.Parameter ?? "").Trim();
				if (text.Length != 0 && !IsQuickerSystemMenuParam(text))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void ApplyQuickerHotkeyIndicator(ToolStripMenuItem mi, string label)
	{
		if (mi == null)
		{
			return;
		}
		string text = (label ?? "").Trim();
		if (text.Length == 0)
		{
			return;
		}
		Image image = null;
		try
		{
			image = mi.Image;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			image = null;
			ProjectData.ClearProjectError();
		}
		if (image == null)
		{
			return;
		}
		int num = Math.Max(16, quickerMenuIconSize);
		checked
		{
			int num2 = (int)Math.Ceiling((double)num * 0.55);
			Bitmap image2 = new Bitmap(num2 + num, num, PixelFormat.Format32bppArgb);
			Image image3 = null;
			try
			{
				image3 = (Image)image.Clone();
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				image3 = null;
				ProjectData.ClearProjectError();
			}
			using (Graphics graphics = Graphics.FromImage(image2))
			{
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				graphics.Clear(Color.Transparent);
				using (Font font = new Font(SystemFonts.MenuFont.FontFamily, Math.Max(8f, (float)num * 0.5f), FontStyle.Bold))
				{
					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};
					using SolidBrush brush = new SolidBrush(Color.FromArgb(220, Color.Black));
					graphics.DrawString(text, font, brush, new RectangleF(0f, 0f, num2, num), format);
				}
				if (image3 != null)
				{
					try
					{
						graphics.DrawImage(image3, new Rectangle(num2, 0, num, num));
					}
					catch (Exception projectError3)
					{
						ProjectData.SetProjectError(projectError3);
						ProjectData.ClearProjectError();
					}
				}
			}
			if (image3 != null)
			{
				try
				{
					image3.Dispose();
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
				}
			}
			mi.ImageScaling = ToolStripItemImageScaling.None;
			mi.Image = image2;
		}
	}

	private void TryInvokeQuickerMenuHotkey(int n)
	{
		if (n <= 0)
		{
			return;
		}
		List<ToolStripMenuItem> list = quickerMenuHotkeyItems;
		if (list == null || n > list.Count)
		{
			return;
		}
		ToolStripMenuItem toolStripMenuItem = list[checked(n - 1)];
		if (toolStripMenuItem != null && toolStripMenuItem.Enabled)
		{
			try
			{
				toolStripMenuItem.PerformClick();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void ExecuteQuickerMenuParam(string param)
	{
		string text = (param ?? "").Trim();
		if (text.Length == 0)
		{
			Logger.Log("ExecuteQuickerMenuParam: empty param");
			return;
		}
		string text2 = text;
		string text3 = text2.ToLowerInvariant();
		if (text3.StartsWith("runaction:") || text3.StartsWith("debugaction:") || text3.StartsWith("previewaction:") || text3.StartsWith("settings:") || text3.StartsWith("exesettings:") || text3.StartsWith("installskin:"))
		{
			text2 = "quicker:" + text2;
		}
		Logger.Log("ExecuteQuickerMenuParam: param=" + text + ", uri=" + text2);
		try
		{
			Process.Start(new ProcessStartInfo(text2)
			{
				UseShellExecute = true
			});
			Logger.Log("ExecuteQuickerMenuParam: Process.Start ok");
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			Logger.Log("ExecuteQuickerMenuParam: Process.Start failed");
			ProjectData.ClearProjectError();
		}
	}
}
