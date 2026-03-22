using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

[StandardModule]
public sealed class NativeMethods
{
	public struct CURSORPOINT
	{
		public int X;

		public int Y;
	}

	public struct CURSORINFO
	{
		public int cbSize;

		public int flags;

		public IntPtr hCursor;

		public CURSORPOINT ptScreenPos;
	}

	public struct RAWINPUTDEVICE
	{
		public ushort usUsagePage;

		public ushort usUsage;

		public int dwFlags;

		public IntPtr hwndTarget;
	}

	public struct RAWINPUTHEADER
	{
		public uint dwType;

		public uint dwSize;

		public IntPtr hDevice;

		public IntPtr wParam;
	}

	public struct RAWMOUSE
	{
		public ushort usFlags;

		public uint ulButtons;

		public uint ulRawButtons;

		public int lLastX;

		public int lLastY;

		public uint ulExtraInformation;
	}

	public struct INPUT
	{
		public int type;

		public MOUSEINPUT mi;
	}

	public struct MOUSEINPUT
	{
		public int dx;

		public int dy;

		public int mouseData;

		public int dwFlags;

		public int time;

		public IntPtr dwExtraInfo;
	}

	public struct KEYBDINPUT
	{
		public ushort wVk;

		public ushort wScan;

		public uint dwFlags;

		public uint time;

		public UIntPtr dwExtraInfo;
	}

	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct INPUT32
	{
		[FieldOffset(0)]
		public uint type;

		[FieldOffset(4)]
		public KEYBDINPUT ki;
	}

	[StructLayout(LayoutKind.Explicit, Size = 40)]
	public struct INPUT64
	{
		[FieldOffset(0)]
		public uint type;

		[FieldOffset(8)]
		public KEYBDINPUT ki;
	}

	public struct MARGINS
	{
		public int cxLeftWidth;

		public int cxRightWidth;

		public int cyTopHeight;

		public int cyBottomHeight;
	}

	public const int SW_RESTORE = 9;

	public const int CURSOR_SHOWING = 1;

	public const int WM_INPUT = 255;

	public const int RID_INPUT = 268435459;

	public const int RIM_TYPEMOUSE = 0;

	public const int RIDEV_INPUTSINK = 256;

	public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

	public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

	public const uint SWP_NOSIZE = 1u;

	public const uint SWP_NOMOVE = 2u;

	public const uint SWP_NOACTIVATE = 16u;

	public const uint SWP_SHOWWINDOW = 64u;

	public const int GWL_HWNDPARENT = -8;

	public const int MOUSEEVENTF_MOVE = 1;

	public const int MOUSEEVENTF_LEFTDOWN = 2;

	public const int MOUSEEVENTF_LEFTUP = 4;

	public const int MOUSEEVENTF_RIGHTDOWN = 8;

	public const int MOUSEEVENTF_RIGHTUP = 16;

	public const int MOUSEEVENTF_MIDDLEDOWN = 32;

	public const int MOUSEEVENTF_MIDDLEUP = 64;

	public const int MOUSEEVENTF_XDOWN = 128;

	public const int MOUSEEVENTF_XUP = 256;

	public const int KEYEVENTF_KEYUP = 2;

	public const int KEYEVENTF_EXTENDEDKEY = 1;

	public const int KEYEVENTF_UNICODE = 4;

	public const int KEYEVENTF_SCANCODE = 8;

	public const byte VK_CONTROL = 17;

	public const byte VK_MENU = 18;

	public const byte VK_V = 86;

	public const byte VK_SPACE = 32;

	public const byte VK_RETURN = 13;

	public const byte VK_TAB = 9;

	public const byte VK_SHIFT = 16;

	public const byte VK_INSERT = 45;

	public const byte VK_BROWSER_BACK = 166;

	public const int INPUT_KEYBOARD = 1;

	public const int INPUT_MOUSE = 0;

	[DllImport("user32.dll")]
	public static extern short GetAsyncKeyState(int vKey);

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool BringWindowToTop(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern IntPtr SetFocus(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

	[DllImport("kernel32.dll")]
	public static extern int GetCurrentThreadId();

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	[DllImport("user32.dll")]
	public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

	[DllImport("user32.dll")]
	public static extern bool IsWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool IsIconic(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

	[DllImport("user32.dll")]
	public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

	[DllImport("user32.dll")]
	public static extern int ShowCursor(bool bShow);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool DestroyIcon(IntPtr hIcon);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetCursorInfo(ref CURSORINFO pci);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetRawInputData(IntPtr hRawInput, int uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

	[DllImport("user32.dll")]
	public static extern bool SetCursorPos(int X, int Y);

	[DllImport("user32.dll")]
	public static extern IntPtr GetCursor();

	[DllImport("user32.dll")]
	public static extern IntPtr SetCursor(IntPtr hCursor);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
	private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
	private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
	{
		if (IntPtr.Size == 8)
		{
			return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}
		return SetWindowLong32(hWnd, nIndex, dwNewLong);
	}

	[DllImport("user32.dll")]
	public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

	[DllImport("user32.dll")]
	public static extern uint MapVirtualKey(uint uCode, uint uMapType);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, UIntPtr dwExtraInfo);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

	[DllImport("user32.dll", SetLastError = true, EntryPoint = "SendInput")]
	public static extern uint SendInputPtr(uint nInputs, IntPtr pInputs, int cbSize);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("dwmapi.dll")]
	public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

	internal static bool IsDesktopManagerProcessName(string processName)
	{
		string text = (processName ?? "").Trim();
		if (text.Length == 0)
		{
			return false;
		}
		string text2 = text.ToLowerInvariant();
		return text2.Contains("coodesker") || text2.Contains("360desktop") || text2.Contains("360desk");
	}
}
