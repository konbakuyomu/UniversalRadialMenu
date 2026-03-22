using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

public sealed class RadialMenuConfigStore
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D0
	{
		public Func<ProcessMenuConfig, bool> _0024VB_0024Local_menuHasUserContent;

		public HashSet<string> _0024VB_0024Local_claimed;

		public Func<string, bool> _0024I9;

		public _Closure_0024__6_002D0(_Closure_0024__6_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_menuHasUserContent = arg0._0024VB_0024Local_menuHasUserContent;
				_0024VB_0024Local_claimed = arg0._0024VB_0024Local_claimed;
			}
		}

		[SpecialName]
		internal int _Lambda_0024__7(ProcessMenuConfig pm)
		{
			return _0024VB_0024Local_menuHasUserContent(pm) ? 1 : 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__9(string t)
		{
			return _0024VB_0024Local_claimed.Contains(t);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__7_002D0
	{
		public MenuItemConfig _0024VB_0024Local_exitItem;

		public _Closure_0024__7_002D0(_Closure_0024__7_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_exitItem = arg0._0024VB_0024Local_exitItem;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__7_002D1
	{
		public string _0024VB_0024Local_parKey;

		public _Closure_0024__7_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__7_002D1(_Closure_0024__7_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_parKey = arg0._0024VB_0024Local_parKey;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__3(MenuItemConfig x)
		{
			if (x != null && x.Level == _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.Level && x.Page == _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.Page)
			{
				return string.Equals((x.ParentMenuName ?? "").Trim(), _0024VB_0024Local_parKey, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
	}

	private readonly string defaultConfigPath;

	public RadialMenuConfigStore()
	{
		defaultConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Quicker", "LDH", "无限轮盘", "RadialMenu_AppConfig.xml");
	}

	public string GetActualConfigPath()
	{
		string directoryName = Path.GetDirectoryName(defaultConfigPath);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		return defaultConfigPath;
	}

	public RadialMenuAppConfig Load()
	{
		string actualConfigPath = GetActualConfigPath();
		if (File.Exists(actualConfigPath))
		{
			using (FileStream stream = new FileStream(actualConfigPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				RadialMenuAppConfig obj = (RadialMenuAppConfig)new XmlSerializer(typeof(RadialMenuAppConfig)).Deserialize(stream);
				NormalizeConfig(obj);
				return obj;
			}
		}
		string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RadialMenu_AppConfig.xml");
		if (File.Exists(path))
		{
			try
			{
				using FileStream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				RadialMenuAppConfig radialMenuAppConfig = (RadialMenuAppConfig)new XmlSerializer(typeof(RadialMenuAppConfig)).Deserialize(stream2);
				NormalizeConfig(radialMenuAppConfig);
				try
				{
					Save(radialMenuAppConfig);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				return radialMenuAppConfig;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
		}
		RadialMenuAppConfig obj2 = new RadialMenuAppConfig
		{
			GlobalMenuItems = CloneMenuItems(GetDefaultSeedMenuItems())
		};
		NormalizeMenuItems(obj2.GlobalMenuItems);
		return obj2;
	}

	public void Save(RadialMenuAppConfig config)
	{
		string actualConfigPath = GetActualConfigPath();
		string directoryName = Path.GetDirectoryName(actualConfigPath);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		using FileStream stream = new FileStream(actualConfigPath, FileMode.Create, FileAccess.Write, FileShare.Read);
		new XmlSerializer(typeof(RadialMenuAppConfig)).Serialize(stream, config);
	}

	public static List<MenuItemConfig> GetDefaultSeedMenuItems()
	{
		List<MenuItemConfig> list = new List<MenuItemConfig>();
		Color color = ColorTranslator.FromHtml("#5b87ff");
		Color color2 = ColorTranslator.FromHtml("#666666");
		list.Add(new MenuItemConfig("设置", "_SETTINGS", color, 2)
		{
			Page = 0,
			SectorIndex = 6
		});
		list.Add(new MenuItemConfig("退出", "_EXIT", color2, 2)
		{
			Page = 0,
			SectorIndex = 7
		});
		return list;
	}

	public static void NormalizeConfig(RadialMenuAppConfig config)
	{
		_Closure_0024__6_002D0 arg = default(_Closure_0024__6_002D0);
		_Closure_0024__6_002D0 CS_0024_003C_003E8__locals5 = new _Closure_0024__6_002D0(arg);
		if (config == null)
		{
			return;
		}
		if (config.GlobalMenuItems == null)
		{
			config.GlobalMenuItems = new List<MenuItemConfig>();
		}
		if (config.ProcessMenus == null)
		{
			config.ProcessMenus = new List<ProcessMenuConfig>();
		}
		if (config.IgnoredProcesses == null)
		{
			config.IgnoredProcesses = new List<string>();
		}
		if (config.MenuSize == null)
		{
			config.MenuSize = new MenuSizeConfig();
		}
		if (!config.MenuSize.ShowDividers.HasValue)
		{
			config.MenuSize.ShowDividers = true;
		}
		if (!config.MenuSize.ShowInnerDividers.HasValue)
		{
			config.MenuSize.ShowInnerDividers = true;
		}
		if (!config.MenuSize.OuterRingGray.HasValue)
		{
			config.MenuSize.OuterRingGray = true;
		}
		try
		{
			if (double.IsNaN(config.MenuSize.SkinOpacity) || double.IsInfinity(config.MenuSize.SkinOpacity))
			{
				config.MenuSize.SkinOpacity = 1.0;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			config.MenuSize.SkinOpacity = 1.0;
			ProjectData.ClearProjectError();
		}
		if (config.MenuSize.SkinOpacity < 0.0)
		{
			config.MenuSize.SkinOpacity = 0.0;
		}
		if (config.MenuSize.SkinOpacity > 1.0)
		{
			config.MenuSize.SkinOpacity = 1.0;
		}
		RadialMenuController.NormalizeMenuSizeDpiProfiles(config.MenuSize);
		NormalizeMenuItems(config.GlobalMenuItems);
		foreach (ProcessMenuConfig processMenu in config.ProcessMenus)
		{
			if (processMenu != null)
			{
				processMenu.ProcessName = RadialMenuController.NormalizeScopeKey(processMenu.ProcessName);
				if (processMenu.MenuItems == null)
				{
					processMenu.MenuItems = new List<MenuItemConfig>();
				}
				NormalizeMenuItems(processMenu.MenuItems);
			}
		}
		config.IgnoredProcesses = (from s in config.IgnoredProcesses
			where !string.IsNullOrWhiteSpace(s)
			select s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		List<ProcessMenuConfig> source = (from g in config.ProcessMenus.Where([SpecialName] (ProcessMenuConfig pm) => pm != null && !string.IsNullOrWhiteSpace(pm.ProcessName)).GroupBy([SpecialName] (ProcessMenuConfig pm) => pm.ProcessName.Trim(), StringComparer.OrdinalIgnoreCase)
			select g.First()).ToList();
		CS_0024_003C_003E8__locals5._0024VB_0024Local_menuHasUserContent = [SpecialName] (ProcessMenuConfig pm) =>
		{
			if (pm == null || pm.MenuItems == null)
			{
				return false;
			}
			foreach (MenuItemConfig menuItem in pm.MenuItems)
			{
				if (menuItem != null && menuItem.Enabled)
				{
					string text = (menuItem.Command ?? "").Trim();
					if (!string.Equals(text, "_SETTINGS", StringComparison.OrdinalIgnoreCase) && !string.Equals(text, "_EXIT", StringComparison.OrdinalIgnoreCase))
					{
						if (menuItem.IsSubMenu)
						{
							return true;
						}
						if (!string.IsNullOrWhiteSpace(text))
						{
							return true;
						}
						string a = (menuItem.OperationType ?? "").Trim();
						if (string.Equals(a, "发送快捷键", StringComparison.OrdinalIgnoreCase))
						{
							if (!string.IsNullOrWhiteSpace((menuItem.Shortcut ?? "").Trim()))
							{
								return true;
							}
						}
						else if (string.Equals(a, "键入文本", StringComparison.OrdinalIgnoreCase))
						{
							if (!string.IsNullOrWhiteSpace(menuItem.TypedText ?? ""))
							{
								return true;
							}
						}
						else if (string.Equals(a, "粘贴文本", StringComparison.OrdinalIgnoreCase))
						{
							if (!string.IsNullOrWhiteSpace(menuItem.PasteText ?? ""))
							{
								return true;
							}
						}
						else if (string.Equals(a, "打开或运行（文件/目录/命令/网址）", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace((menuItem.RunPath ?? "").Trim()))
						{
							return true;
						}
					}
				}
			}
			return false;
		};
		List<ProcessMenuConfig> list = (from pm in source
			orderby RadialMenuController.SplitProcessAliases(pm.ProcessName).Count descending, CS_0024_003C_003E8__locals5._0024VB_0024Local_menuHasUserContent(pm) ? 1 : 0 descending
			select pm).ThenBy([SpecialName] (ProcessMenuConfig pm) => pm.ProcessName, StringComparer.OrdinalIgnoreCase).ToList();
		CS_0024_003C_003E8__locals5._0024VB_0024Local_claimed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		List<ProcessMenuConfig> list2 = new List<ProcessMenuConfig>();
		foreach (ProcessMenuConfig item in list)
		{
			List<string> list3 = RadialMenuController.SplitProcessAliases(item.ProcessName);
			if (list3 == null || list3.Count == 0)
			{
				list3 = new List<string> { RadialMenuController.NormalizeProcessName(item.ProcessName) };
			}
			if (list3.Any([SpecialName] (string t) => CS_0024_003C_003E8__locals5._0024VB_0024Local_claimed.Contains(t)))
			{
				continue;
			}
			list2.Add(item);
			foreach (string item2 in list3)
			{
				if (!string.IsNullOrWhiteSpace(item2))
				{
					CS_0024_003C_003E8__locals5._0024VB_0024Local_claimed.Add(item2);
				}
			}
		}
		config.ProcessMenus = list2.OrderBy([SpecialName] (ProcessMenuConfig pm) => pm.ProcessName, StringComparer.OrdinalIgnoreCase).ToList();
	}

	public static void NormalizeMenuItems(List<MenuItemConfig> items)
	{
		_Closure_0024__7_002D0 closure_0024__7_002D = new _Closure_0024__7_002D0(null);
		if (items == null)
		{
			return;
		}
		MenuItemConfig menuItemConfig = items.FirstOrDefault([SpecialName] (MenuItemConfig i) => string.Equals(i.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase));
		closure_0024__7_002D._0024VB_0024Local_exitItem = items.FirstOrDefault([SpecialName] (MenuItemConfig i) => string.Equals(i.Command, "_EXIT", StringComparison.OrdinalIgnoreCase));
		Action<MenuItemConfig, int, string> obj = [SpecialName] (MenuItemConfig sysItem, int defaultIndex, string colorHex) =>
		{
			if (sysItem != null)
			{
				sysItem.ColorArgb = ColorTranslator.FromHtml(colorHex).ToArgb();
				if (sysItem.Level < 0 || sysItem.Level > 2)
				{
					sysItem.Level = 2;
				}
				int num6 = ((sysItem.Level == 1) ? 16 : 8);
				if (sysItem.SectorIndex < 0 || sysItem.SectorIndex >= num6)
				{
					int sectorIndex = defaultIndex;
					if (num6 == 16)
					{
						sectorIndex = ((defaultIndex == 6) ? 14 : 15);
					}
					sysItem.SectorIndex = sectorIndex;
				}
			}
		};
		obj(menuItemConfig, 6, "#5b87ff");
		obj(closure_0024__7_002D._0024VB_0024Local_exitItem, 7, "#666666");
		checked
		{
			if (menuItemConfig != null && closure_0024__7_002D._0024VB_0024Local_exitItem != null)
			{
				bool flag = string.Equals((menuItemConfig.ParentMenuName ?? "").Trim(), (closure_0024__7_002D._0024VB_0024Local_exitItem.ParentMenuName ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
				if (menuItemConfig.Level == closure_0024__7_002D._0024VB_0024Local_exitItem.Level && menuItemConfig.Page == closure_0024__7_002D._0024VB_0024Local_exitItem.Page && flag && menuItemConfig.SectorIndex == closure_0024__7_002D._0024VB_0024Local_exitItem.SectorIndex)
				{
					_Closure_0024__7_002D1 arg = default(_Closure_0024__7_002D1);
					_Closure_0024__7_002D1 CS_0024_003C_003E8__locals10 = new _Closure_0024__7_002D1(arg);
					CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__7_002D;
					int num = ((menuItemConfig.Level == 1) ? 16 : 8);
					CS_0024_003C_003E8__locals10._0024VB_0024Local_parKey = (menuItemConfig.ParentMenuName ?? "").Trim();
					if (CS_0024_003C_003E8__locals10._0024VB_0024Local_parKey.Length == 0)
					{
						CS_0024_003C_003E8__locals10._0024VB_0024Local_parKey = "ROOT";
					}
					HashSet<int> hashSet = new HashSet<int>(from x in items
						where x != null && x.Level == CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.Level && x.Page == CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.Page && string.Equals((x.ParentMenuName ?? "").Trim(), CS_0024_003C_003E8__locals10._0024VB_0024Local_parKey, StringComparison.OrdinalIgnoreCase)
						select x.SectorIndex);
					hashSet.Remove(CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.SectorIndex);
					int num2 = ((num == 16) ? 15 : 7);
					if (num2 != menuItemConfig.SectorIndex && !hashSet.Contains(num2))
					{
						CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.SectorIndex = num2;
					}
					else
					{
						int num3 = num - 1;
						for (int num4 = 0; num4 <= num3; num4++)
						{
							if (num4 != menuItemConfig.SectorIndex && !hashSet.Contains(num4))
							{
								CS_0024_003C_003E8__locals10._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_exitItem.SectorIndex = num4;
								break;
							}
						}
					}
				}
			}
			int colorArgb = ColorTranslator.FromHtml("#F0F0F0").ToArgb();
			int colorArgb2 = ColorTranslator.FromHtml("#FFB650").ToArgb();
			foreach (MenuItemConfig item in items)
			{
				if (item == null)
				{
					continue;
				}
				item.ParentMenuName = (item.ParentMenuName ?? "").Trim();
				if (string.IsNullOrWhiteSpace(item.ParentMenuName))
				{
					item.ParentMenuName = "ROOT";
				}
				if (string.Equals(item.ParentMenuName, "ROOT", StringComparison.OrdinalIgnoreCase))
				{
					item.ParentMenuName = "ROOT";
				}
				string value = (item.OperationType ?? "").Trim();
				if (item.IsSubMenu)
				{
					item.OperationType = "子菜单";
				}
				else if (string.IsNullOrWhiteSpace(value))
				{
					string text = (item.Command ?? "").Trim();
					if (text.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase) || text.StartsWith("QCAD_BRIDGE:", StringComparison.OrdinalIgnoreCase))
					{
						item.OperationType = "Quicker动作";
					}
					else if (string.IsNullOrWhiteSpace(text) || string.Equals(text, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "_EXIT", StringComparison.OrdinalIgnoreCase))
					{
						item.OperationType = "Quicker动作";
					}
					else
					{
						item.OperationType = "打开或运行（文件/目录/命令/网址）";
					}
				}
				if (string.Equals(item.OperationType, "打开或运行（文件/目录/命令/网址）", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace((item.RunPath ?? "").Trim()))
				{
					item.RunPath = (item.Command ?? "").Trim();
				}
				if (item.Level == 2 && !string.Equals(item.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) && !string.Equals(item.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
				{
					item.ColorArgb = colorArgb;
				}
				if (string.Equals(item.ParentMenuName, "ROOT", StringComparison.OrdinalIgnoreCase) && item.IsSubMenu)
				{
					item.ColorArgb = colorArgb2;
				}
			}
			foreach (IGrouping<VB_0024AnonymousType_0<int, int, string>, MenuItemConfig> item2 in from i in items
				where i != null
				group i by new VB_0024AnonymousType_0<int, int, string>(i.Level, i.Page, i.ParentMenuName))
			{
				if (!item2.All([SpecialName] (MenuItemConfig i) => i.SectorIndex == 0) || item2.Count() <= 1)
				{
					continue;
				}
				int num5 = 0;
				foreach (MenuItemConfig item3 in item2)
				{
					item3.SectorIndex = num5;
					num5++;
				}
			}
		}
	}

	public static List<MenuItemConfig> CloneMenuItems(List<MenuItemConfig> items)
	{
		if (items == null)
		{
			return new List<MenuItemConfig>();
		}
		List<MenuItemConfig> list = new List<MenuItemConfig>();
		foreach (MenuItemConfig item in items)
		{
			if (item != null)
			{
				list.Add(new MenuItemConfig
				{
					Text = item.Text,
					DisplayName = item.DisplayName,
					Command = item.Command,
					ColorArgb = item.ColorArgb,
					Enabled = item.Enabled,
					Level = item.Level,
					IconPath = item.IconPath,
					QuickerMenuItems = ((item.QuickerMenuItems == null) ? null : (from x in item.QuickerMenuItems
						select (x != null) ? new QuickerRightClickMenuItemConfig
						{
							Icon = x.Icon,
							DisplayText = x.DisplayText,
							DisplayDescription = x.DisplayDescription,
							Parameter = x.Parameter,
							Marker = x.Marker,
							IsGroupHeader = x.IsGroupHeader,
							GroupParent = x.GroupParent
						} : null into x
						where x != null
						select x).ToList()),
					IsSubMenu = item.IsSubMenu,
					SubMenuName = item.SubMenuName,
					ParentMenuName = item.ParentMenuName,
					FontSize = item.FontSize,
					Page = item.Page,
					SectorIndex = item.SectorIndex,
					OperationType = item.OperationType,
					Shortcut = item.Shortcut,
					TypedText = item.TypedText,
					PasteText = item.PasteText,
					RunPath = item.RunPath,
					RunParams = item.RunParams,
					UseFirstCharIcon = item.UseFirstCharIcon,
					OnlyShowIcon = item.OnlyShowIcon
				});
			}
		}
		return list;
	}
}
