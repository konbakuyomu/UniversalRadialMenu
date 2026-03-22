using System;

namespace UniversalRadialMenu;

[Serializable]
public class QuickerRightClickMenuItemConfig
{
	public string Icon { get; set; }

	public string DisplayText { get; set; }

	public string DisplayDescription { get; set; }

	public string Parameter { get; set; }

	public string Marker { get; set; }

	public bool IsGroupHeader { get; set; }

	public string GroupParent { get; set; }

	public QuickerRightClickMenuItemConfig()
	{
		Icon = "";
		DisplayText = "";
		DisplayDescription = "";
		Parameter = "";
		Marker = "";
		IsGroupHeader = false;
		GroupParent = "";
	}
}
