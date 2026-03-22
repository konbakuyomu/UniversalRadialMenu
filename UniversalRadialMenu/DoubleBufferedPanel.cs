using System.Windows.Forms;

namespace UniversalRadialMenu;

internal class DoubleBufferedPanel : Panel
{
	public DoubleBufferedPanel()
	{
		DoubleBuffered = true;
		SetStyle((ControlStyles)(-1), value: true);
	}
}
