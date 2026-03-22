using System;
using System.Collections.Generic;
using System.Drawing;

namespace UniversalRadialMenu;

internal class RadialSector
{
	public string Text { get; set; }

	public string Command { get; set; }

	public Color FillColor { get; set; }

	public Action Action { get; set; }

	public int Level { get; set; }

	public Image Icon { get; set; }

	public List<QuickerRightClickMenuItemConfig> QuickerMenuItems { get; set; }

	public bool IsSubMenu { get; set; }

	public string SubMenuName { get; set; }

	public string ParentMenuName { get; set; }

	public int FontSize { get; set; }

	public int Page { get; set; }

	public int SectorIndex { get; set; }

	public string IconSpec { get; set; }

	public bool Enabled { get; set; }

	public bool UseFirstCharIcon { get; set; }

	public bool OnlyShowIcon { get; set; }

	public RadialSector(string text, string command, Color fillColor, Action action, int level, Image icon, bool isSubMenu, string subMenuName, string parentMenuName, int fontSize, int page, int sectorIndex = 0, List<QuickerRightClickMenuItemConfig> quickerMenuItems = null, string iconSpec = "", bool enabled = true, bool useFirstCharIcon = false, bool onlyShowIcon = false)
	{
		FontSize = 12;
		Page = 0;
		SectorIndex = 0;
		IconSpec = "";
		Enabled = true;
		UseFirstCharIcon = false;
		OnlyShowIcon = false;
		Text = text;
		Command = command;
		FillColor = fillColor;
		Action = action;
		Level = level;
		Icon = icon;
		QuickerMenuItems = quickerMenuItems;
		IsSubMenu = isSubMenu;
		SubMenuName = subMenuName;
		ParentMenuName = parentMenuName;
		FontSize = fontSize;
		Page = page;
		SectorIndex = sectorIndex;
		IconSpec = iconSpec;
		Enabled = enabled;
		UseFirstCharIcon = useFirstCharIcon;
		OnlyShowIcon = onlyShowIcon;
	}
}
