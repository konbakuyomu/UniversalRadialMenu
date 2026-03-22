using System;
using System.Collections.Generic;

namespace UniversalRadialMenu;

[Serializable]
public class RadialMenuAppConfig
{
	public List<MenuItemConfig> GlobalMenuItems { get; set; }

	public List<ProcessMenuConfig> ProcessMenus { get; set; }

	public List<string> IgnoredProcesses { get; set; }

	public MenuSizeConfig MenuSize { get; set; }

	public bool UseCadInfiniteRadialMenu { get; set; }

	public RadialMenuAppConfig()
	{
		UseCadInfiniteRadialMenu = false;
		GlobalMenuItems = new List<MenuItemConfig>();
		ProcessMenus = new List<ProcessMenuConfig>();
		IgnoredProcesses = new List<string>();
		MenuSize = new MenuSizeConfig();
	}
}
