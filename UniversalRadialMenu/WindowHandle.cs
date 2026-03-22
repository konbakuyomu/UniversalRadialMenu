using System;
using System.Windows.Forms;

namespace UniversalRadialMenu;

public class WindowHandle : IWin32Window
{
	private readonly IntPtr _handle;

	public IntPtr Handle => _handle;

	public WindowHandle(IntPtr handle)
	{
		_handle = handle;
	}
}
