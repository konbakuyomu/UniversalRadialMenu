using System;
using System.Collections.Generic;
using System.Drawing;

namespace UniversalRadialMenu;

[Serializable]
public class MenuItemConfig
{
	public string Text { get; set; }

	public string DisplayName { get; set; }

	public string Command { get; set; }

	public int ColorArgb { get; set; }

	public bool Enabled { get; set; }

	public int Level { get; set; }

	public string IconPath { get; set; }

	public List<QuickerRightClickMenuItemConfig> QuickerMenuItems { get; set; }

	public bool IsSubMenu { get; set; }

	public string SubMenuName { get; set; }

	public string ParentMenuName { get; set; }

	public int FontSize { get; set; }

	public int Page { get; set; }

	public int SectorIndex { get; set; }

	public string OperationType { get; set; }

	public string Shortcut { get; set; }

	public string TypedText { get; set; }

	public string PasteText { get; set; }

	public string RunPath { get; set; }

	public string RunParams { get; set; }

	public bool UseFirstCharIcon { get; set; }

	public bool OnlyShowIcon { get; set; }

	public MenuItemConfig()
	{
		Text = "未命名";
		DisplayName = "";
		Command = "";
		ColorArgb = Color.LightGray.ToArgb();
		Enabled = true;
		Level = 0;
		IconPath = "";
		QuickerMenuItems = null;
		IsSubMenu = false;
		SubMenuName = "";
		ParentMenuName = "ROOT";
		FontSize = 12;
		Page = 0;
		SectorIndex = 0;
		OperationType = "Quicker动作";
		Shortcut = "";
		TypedText = "";
		PasteText = "";
		RunPath = "";
		RunParams = "";
		UseFirstCharIcon = false;
		OnlyShowIcon = false;
	}

	public MenuItemConfig(string text, string cmd, Color color, int level)
	{
		Text = "未命名";
		DisplayName = "";
		Command = "";
		ColorArgb = Color.LightGray.ToArgb();
		Enabled = true;
		Level = 0;
		IconPath = "";
		QuickerMenuItems = null;
		IsSubMenu = false;
		SubMenuName = "";
		ParentMenuName = "ROOT";
		FontSize = 12;
		Page = 0;
		SectorIndex = 0;
		OperationType = "Quicker动作";
		Shortcut = "";
		TypedText = "";
		PasteText = "";
		RunPath = "";
		RunParams = "";
		UseFirstCharIcon = false;
		OnlyShowIcon = false;
		Text = text;
		Command = cmd;
		ColorArgb = color.ToArgb();
		Level = level;
	}
}
