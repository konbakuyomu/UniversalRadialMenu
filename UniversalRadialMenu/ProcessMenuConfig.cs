using System;
using System.Collections.Generic;

namespace UniversalRadialMenu;

[Serializable]
public class ProcessMenuConfig
{
	public string ProcessName { get; set; }

	public List<MenuItemConfig> MenuItems { get; set; }

	public ProcessMenuConfig()
	{
		ProcessName = "";
		MenuItems = new List<MenuItemConfig>();
	}
}
