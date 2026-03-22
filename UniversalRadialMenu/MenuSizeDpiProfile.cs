using System;

namespace UniversalRadialMenu;

[Serializable]
public class MenuSizeDpiProfile
{
	public int Dpi { get; set; }

	public int OuterRadius { get; set; }

	public int InnerRadius { get; set; }

	public int FontSize { get; set; }

	public double Scale { get; set; }

	public MenuSizeDpiProfile()
	{
		Dpi = 96;
		OuterRadius = 260;
		InnerRadius = 40;
		FontSize = 12;
		Scale = 1.0;
	}
}
