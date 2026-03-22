using System;
using System.Collections.Generic;
using System.Drawing;

namespace UniversalRadialMenu;

[Serializable]
public class MenuSizeConfig
{
	public int OuterRadius { get; set; }

	public int InnerRadius { get; set; }

	public int FontSize { get; set; }

	public double Scale { get; set; }

	public string DragAreaText { get; set; }

	public int DragAreaTextColorArgb { get; set; }

	public int DragAreaBackgroundColorArgb { get; set; }

	public string DragAreaBackgroundImage { get; set; }

	public double SkinOpacity { get; set; }

	public double FallbackOpacity { get; set; }

	public int SettingsWindowWidth { get; set; }

	public int SettingsWindowHeight { get; set; }

	public bool? ShowDividers { get; set; }

	public bool? ShowInnerDividers { get; set; }

	public bool? OuterRingGray { get; set; }

	public List<MenuSizeDpiProfile> DpiProfiles { get; set; }

	public MenuSizeConfig()
	{
		OuterRadius = 260;
		InnerRadius = 40;
		FontSize = 12;
		Scale = 1.0;
		DragAreaText = "拖动";
		DragAreaTextColorArgb = Color.White.ToArgb();
		DragAreaBackgroundColorArgb = Color.FromArgb(100, 0, 0, 0).ToArgb();
		DragAreaBackgroundImage = "";
		SkinOpacity = 1.0;
		FallbackOpacity = 1.0;
		SettingsWindowWidth = 900;
		SettingsWindowHeight = 790;
		ShowDividers = null;
		ShowInnerDividers = null;
		OuterRingGray = null;
		DpiProfiles = null;
	}
}
