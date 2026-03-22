using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

public class RadialMenuSettingsForm : Form
{
	public delegate void ConfigSavedEventHandler();

	private class QuickerMenuFetchCacheEntry
	{
		public long FetchedAtUtcTicks { get; set; }

		public List<QuickerRightClickMenuItemConfig> Items { get; set; }
	}

	internal struct HitInfo
	{
		public int Index;

		public int Level;
	}

	private class QuickerActionMetadata
	{
		public string Title { get; set; }

		public string Icon { get; set; }

		public string Source { get; set; }

		public List<QuickerRightClickMenuItemConfig> QuickerMenuItems { get; set; }
	}

	internal class QuickerWheelParsedItem
	{
		public string RawPositionKey { get; set; }

		public string Title { get; set; }

		public string Icon { get; set; }

		public int ActionType { get; set; }

		public string Data { get; set; }

		public string Id { get; set; }

		public string Param { get; set; }

		public string ActionId { get; set; }

		public string ActionParam { get; set; }

		public string ActionName { get; set; }

		public List<QuickerRightClickMenuItemConfig> QuickerMenuItems { get; set; }
	}

	internal class ScopeItem
	{
		public string DisplayName { get; set; }

		public string ProcessName { get; set; }

		public bool IsIgnored { get; set; }

		public override string ToString()
		{
			return DisplayName;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__100_002D0
	{
		public int _0024VB_0024Local_sz;

		public _Closure_0024__100_002D0(_Closure_0024__100_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_sz = arg0._0024VB_0024Local_sz;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(List<MenuItemConfig> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (MenuItemConfig item in list)
			{
				if (item != null)
				{
					item.FontSize = _0024VB_0024Local_sz;
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__103_002D0
	{
		public string _0024VB_0024Local_currentKey;

		public ProcessMenuConfig _0024VB_0024Local_pm;

		public List<string> _0024VB_0024Local_newTokens;

		public Func<string, bool> _0024I3;

		public _Closure_0024__103_002D0(_Closure_0024__103_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_currentKey = arg0._0024VB_0024Local_currentKey;
				_0024VB_0024Local_pm = arg0._0024VB_0024Local_pm;
				_0024VB_0024Local_newTokens = arg0._0024VB_0024Local_newTokens;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(ProcessMenuConfig m)
		{
			if (m != null)
			{
				return string.Equals(RadialMenuController.NormalizeScopeKey(m.ProcessName), _0024VB_0024Local_currentKey, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__1(ProcessMenuConfig m)
		{
			if (m != null)
			{
				return !object.ReferenceEquals(m, _0024VB_0024Local_pm);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__2(ProcessMenuConfig m)
		{
			List<string> list = RadialMenuController.SplitProcessAliases(m.ProcessName);
			if (list == null || list.Count == 0)
			{
				return false;
			}
			return list.Any([SpecialName] (string x) =>
			{
				_Closure_0024__103_002D1 arg = default(_Closure_0024__103_002D1);
				_Closure_0024__103_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__103_002D1(arg)
				{
					_0024VB_0024Local_x = x
				};
				return _0024VB_0024Local_newTokens.Any([SpecialName] (string y) => string.Equals(CS_0024_003C_003E8__locals1._0024VB_0024Local_x, y, StringComparison.OrdinalIgnoreCase));
			});
		}

		[SpecialName]
		internal bool _Lambda_0024__3(string x)
		{
			_Closure_0024__103_002D1 arg = default(_Closure_0024__103_002D1);
			_Closure_0024__103_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__103_002D1(arg)
			{
				_0024VB_0024Local_x = x
			};
			return _0024VB_0024Local_newTokens.Any([SpecialName] (string y) => string.Equals(CS_0024_003C_003E8__locals1._0024VB_0024Local_x, y, StringComparison.OrdinalIgnoreCase));
		}

		[SpecialName]
		internal bool _Lambda_0024__5(string t)
		{
			_Closure_0024__103_002D2 arg = default(_Closure_0024__103_002D2);
			_Closure_0024__103_002D2 CS_0024_003C_003E8__locals1 = new _Closure_0024__103_002D2(arg)
			{
				_0024VB_0024Local_t = t
			};
			return !_0024VB_0024Local_newTokens.Any([SpecialName] (string n) => string.Equals(n, CS_0024_003C_003E8__locals1._0024VB_0024Local_t, StringComparison.OrdinalIgnoreCase));
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__103_002D1
	{
		public string _0024VB_0024Local_x;

		public _Closure_0024__103_002D1(_Closure_0024__103_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_x = arg0._0024VB_0024Local_x;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__4(string y)
		{
			return string.Equals(_0024VB_0024Local_x, y, StringComparison.OrdinalIgnoreCase);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__103_002D2
	{
		public string _0024VB_0024Local_t;

		public _Closure_0024__103_002D2(_Closure_0024__103_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_t = arg0._0024VB_0024Local_t;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__6(string n)
		{
			return string.Equals(n, _0024VB_0024Local_t, StringComparison.OrdinalIgnoreCase);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__103_002D3
	{
		public string _0024VB_0024Local_key;

		public _Closure_0024__103_002D3(_Closure_0024__103_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_key = arg0._0024VB_0024Local_key;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__7(string s)
		{
			return string.Equals(RadialMenuController.NormalizeProcessName(s), _0024VB_0024Local_key, StringComparison.OrdinalIgnoreCase);
		}

		[SpecialName]
		internal bool _Lambda_0024__8(ProcessMenuConfig m)
		{
			if (m != null)
			{
				return RadialMenuController.ScopeKeyMatchesProcess(m.ProcessName, _0024VB_0024Local_key);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__9(ProcessMenuConfig m)
		{
			if (m != null)
			{
				return string.Equals(RadialMenuController.NormalizeScopeKey(m.ProcessName), _0024VB_0024Local_key, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__109_002D0
	{
		public string _0024VB_0024Local_q;

		public _Closure_0024__109_002D0(_Closure_0024__109_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_q = arg0._0024VB_0024Local_q;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(ScopeItem it)
		{
			return ScopeMatchesQuery(it, _0024VB_0024Local_q);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__119_002D0
	{
		public TextBox _0024VB_0024Local_txt;

		public RadialMenuSettingsForm _0024VB_0024Me;

		public _Closure_0024__119_002D0(_Closure_0024__119_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_txt = arg0._0024VB_0024Local_txt;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R25(object a0, EventArgs a1)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			List<string> list = _0024VB_0024Me.ShowProcessSelectionDialog();
			if (list.Count <= 0)
			{
				return;
			}
			List<string> list2 = _0024VB_0024Local_txt.Text.Trim().Split(new char[2] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			foreach (string item in list)
			{
				if (!list2.Contains(item, StringComparer.OrdinalIgnoreCase))
				{
					list2.Add(item);
				}
			}
			_0024VB_0024Local_txt.Text = string.Join(";", list2);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__119_002D1
	{
		public string _0024VB_0024Local_procName;

		public _Closure_0024__119_002D1(_Closure_0024__119_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_procName = arg0._0024VB_0024Local_procName;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__1(ProcessMenuConfig m)
		{
			return string.Equals(m.ProcessName, _0024VB_0024Local_procName, StringComparison.OrdinalIgnoreCase);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__120_002D0
	{
		public ListBox _0024VB_0024Local_lst;

		public _Closure_0024__120_002D0(_Closure_0024__120_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_lst = arg0._0024VB_0024Local_lst;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R26(object a0, EventArgs a1)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			string text = Interaction.InputBox("请输入忽略的进程名：", "添加忽略");
			text = (text ?? "").Trim();
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			bool flag = false;
			foreach (object item in _0024VB_0024Local_lst.Items)
			{
				if (string.Equals(RuntimeHelpers.GetObjectValue(item).ToString(), text, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				_0024VB_0024Local_lst.Items.Add(text);
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R27(object a0, EventArgs a1)
		{
			_Lambda_0024__1();
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			if (_0024VB_0024Local_lst.SelectedIndex >= 0)
			{
				_0024VB_0024Local_lst.Items.RemoveAt(_0024VB_0024Local_lst.SelectedIndex);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__120_002D1
	{
		public Form _0024VB_0024Local_dlg;

		public _Closure_0024__120_002D1(_Closure_0024__120_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_dlg = arg0._0024VB_0024Local_dlg;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R28(object a0, EventArgs a1)
		{
			_Lambda_0024__2();
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			_0024VB_0024Local_dlg.DialogResult = DialogResult.OK;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__126_002D0
	{
		public int _0024VB_0024Local_sectorIndex;

		public _Closure_0024__126_002D0(_Closure_0024__126_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_sectorIndex = arg0._0024VB_0024Local_sectorIndex;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(MenuItemConfig x)
		{
			return x.SectorIndex == _0024VB_0024Local_sectorIndex;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__135_002D0
	{
		public HitInfo _0024VB_0024Local_hitRight;

		public _Closure_0024__135_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public Func<MenuItemConfig, bool> _0024I8;

		[SpecialName]
		internal bool _Lambda_0024__0(MenuItemConfig x)
		{
			return x.SectorIndex == _0024VB_0024Local_hitRight.Index;
		}

		[SpecialName]
		internal void _Lambda_0024__R29(object a0, EventArgs a1)
		{
			_Lambda_0024__1();
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.CreateNewItemAt(_0024VB_0024Local_hitRight);
		}

		[SpecialName]
		internal void _Lambda_0024__R35(object a0, EventArgs a1)
		{
			_Lambda_0024__7();
		}

		[SpecialName]
		internal void _Lambda_0024__7()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardItemTemplate == null)
			{
				return;
			}
			MenuItemConfig menuItemConfig = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.GetItemsForLevel(_0024VB_0024Local_hitRight.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == _0024VB_0024Local_hitRight.Index);
			if (menuItemConfig != null && string.Equals(menuItemConfig.Command, "_BACK", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			MenuItemConfig menuItemConfig2 = RadialMenuConfigStore.CloneMenuItems(new List<MenuItemConfig> { _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardItemTemplate }).FirstOrDefault();
			if (menuItemConfig2 == null)
			{
				return;
			}
			if (menuItemConfig == null)
			{
				menuItemConfig2.Level = _0024VB_0024Local_hitRight.Level;
				menuItemConfig2.ParentMenuName = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.currentParentMenuName;
				menuItemConfig2.SectorIndex = _0024VB_0024Local_hitRight.Index;
				if (_0024VB_0024Local_hitRight.Level == 2)
				{
					menuItemConfig2.Page = 0;
				}
				else
				{
					menuItemConfig2.Page = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.currentPage;
				}
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyOuterRingDefaultColor(menuItemConfig2);
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.configs.Add(menuItemConfig2);
			}
			else
			{
				menuItemConfig.Text = menuItemConfig2.Text;
				menuItemConfig.DisplayName = menuItemConfig2.DisplayName;
				menuItemConfig.Command = menuItemConfig2.Command;
				menuItemConfig.ColorArgb = menuItemConfig2.ColorArgb;
				menuItemConfig.Enabled = menuItemConfig2.Enabled;
				menuItemConfig.IconPath = menuItemConfig2.IconPath;
				menuItemConfig.UseFirstCharIcon = menuItemConfig2.UseFirstCharIcon;
				menuItemConfig.QuickerMenuItems = menuItemConfig2.QuickerMenuItems;
				menuItemConfig.IsSubMenu = menuItemConfig2.IsSubMenu;
				menuItemConfig.SubMenuName = menuItemConfig2.SubMenuName;
				menuItemConfig.ParentMenuName = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.currentParentMenuName;
				menuItemConfig.FontSize = menuItemConfig2.FontSize;
				menuItemConfig.OperationType = menuItemConfig2.OperationType;
				menuItemConfig.Shortcut = menuItemConfig2.Shortcut;
				menuItemConfig.TypedText = menuItemConfig2.TypedText;
				menuItemConfig.PasteText = menuItemConfig2.PasteText;
				menuItemConfig.RunPath = menuItemConfig2.RunPath;
				menuItemConfig.RunParams = menuItemConfig2.RunParams;
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyOuterRingDefaultColor(menuItemConfig);
			}
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardIsCut)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardItemTemplate = null;
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardIsCut = false;
			}
			RadialMenuConfigStore.NormalizeMenuItems(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.configs);
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RecalculatePages();
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.renderPanel.Invalidate();
		}

		[SpecialName]
		internal bool _Lambda_0024__8(MenuItemConfig x)
		{
			return x.SectorIndex == _0024VB_0024Local_hitRight.Index;
		}

		[SpecialName]
		internal void _Lambda_0024__R36(object a0, EventArgs a1)
		{
			_Lambda_0024__9();
		}

		[SpecialName]
		internal void _Lambda_0024__9()
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.PasteQuickerActionToHit(_0024VB_0024Local_hitRight, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_e.Location);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__135_002D1
	{
		public MenuItemConfig _0024VB_0024Local_item;

		public _Closure_0024__135_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		[SpecialName]
		internal void _Lambda_0024__R30(object a0, EventArgs a1)
		{
			_Lambda_0024__2();
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.EnterSubMenu(_0024VB_0024Local_item.SubMenuName);
		}

		[SpecialName]
		internal void _Lambda_0024__R31(object a0, EventArgs a1)
		{
			_Lambda_0024__3();
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			if (_0024VB_0024Local_item != null)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.EditItem(_0024VB_0024Local_item);
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R32(object a0, EventArgs a1)
		{
			_Lambda_0024__4();
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			if (_0024VB_0024Local_item != null)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.DeleteItem(_0024VB_0024Local_item);
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R33(object a0, EventArgs a1)
		{
			_Lambda_0024__5();
		}

		[SpecialName]
		internal void _Lambda_0024__5()
		{
			if (_0024VB_0024Local_item != null)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardItemTemplate = RadialMenuConfigStore.CloneMenuItems(new List<MenuItemConfig> { _0024VB_0024Local_item }).FirstOrDefault();
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardIsCut = true;
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.DeleteItem(_0024VB_0024Local_item);
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.renderPanel.Invalidate();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R34(object a0, EventArgs a1)
		{
			_Lambda_0024__6();
		}

		[SpecialName]
		internal void _Lambda_0024__6()
		{
			if (_0024VB_0024Local_item != null)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardItemTemplate = RadialMenuConfigStore.CloneMenuItems(new List<MenuItemConfig> { _0024VB_0024Local_item }).FirstOrDefault();
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.clipboardIsCut = false;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__135_002D2
	{
		public MouseEventArgs _0024VB_0024Local_e;

		public RadialMenuSettingsForm _0024VB_0024Me;
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__183_002D0
	{
		public string _0024VB_0024Local_mn;

		public _Closure_0024__183_002D0(_Closure_0024__183_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_mn = arg0._0024VB_0024Local_mn;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(MenuItemConfig c)
		{
			if (c != null && string.Equals(c.ParentMenuName, _0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals(c.Command, "_BACK", StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__1(MenuItemConfig c)
		{
			if (c != null && c.Level == 0 && c.Page == 0)
			{
				return string.Equals(c.ParentMenuName, _0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__3(MenuItemConfig c)
		{
			if (c != null && c.Level == 1 && c.Page == 0)
			{
				return string.Equals(c.ParentMenuName, _0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__5(MenuItemConfig c)
		{
			if (c != null && c.Level == 2)
			{
				return string.Equals(c.ParentMenuName, _0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__8(MenuItemConfig c)
		{
			if (c != null && c.Level == 0)
			{
				return string.Equals(c.ParentMenuName, _0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__217_002D0
	{
		public string _0024VB_0024Local_jsonCopy;

		public int _0024VB_0024Local_batchId;

		public RadialMenuSettingsForm _0024VB_0024Me;

		public _Closure_0024__217_002D0(_Closure_0024__217_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_jsonCopy = arg0._0024VB_0024Local_jsonCopy;
				_0024VB_0024Local_batchId = arg0._0024VB_0024Local_batchId;
			}
		}

		[SpecialName]
		internal List<QuickerWheelParsedItem> _Lambda_0024__0()
		{
			return ParseQuickerCircleMenuConfig(_0024VB_0024Local_jsonCopy);
		}

		[SpecialName]
		internal void _Lambda_0024__1(Task<List<QuickerWheelParsedItem>> t)
		{
			_Closure_0024__217_002D1 arg = default(_Closure_0024__217_002D1);
			_Closure_0024__217_002D1 CS_0024_003C_003E8__locals28 = new _Closure_0024__217_002D1(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2 = this,
				_0024VB_0024Local_t = t
			};
			if (_0024VB_0024Me == null || _0024VB_0024Me.IsDisposed)
			{
				return;
			}
			try
			{
				_0024VB_0024Me.BeginInvoke((Action)checked([SpecialName] () =>
				{
					if (CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel != null)
					{
						CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel.Enabled = true;
					}
					if (CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
					{
						if (CS_0024_003C_003E8__locals28._0024VB_0024Local_t != null && !CS_0024_003C_003E8__locals28._0024VB_0024Local_t.IsFaulted && CS_0024_003C_003E8__locals28._0024VB_0024Local_t.Result != null && CS_0024_003C_003E8__locals28._0024VB_0024Local_t.Result.Count != 0)
						{
							List<QuickerWheelParsedItem> result = CS_0024_003C_003E8__locals28._0024VB_0024Local_t.Result;
							AppendQuickerImportLog("ImportQuickerWheelFromClipboard parsed count=" + result.Count);
							int num = 0;
							int num2 = 0;
							HitInfo hit = default(HitInfo);
							foreach (QuickerWheelParsedItem item in result)
							{
								int result2;
								if (item == null)
								{
									num2++;
									AppendQuickerImportLog("Skip item because parsed entry is Nothing");
								}
								else if (!int.TryParse((item.RawPositionKey ?? "").Trim(), out result2))
								{
									num2++;
									AppendQuickerImportLog("Skip item because RawPositionKey is invalid: \"" + item.RawPositionKey + "\"");
								}
								else if (!TryMapQuickerWheelPositionToHit(result2, ref hit))
								{
									num2++;
									AppendQuickerImportLog("Skip item because position mapping failed. keyInt=" + result2 + ", rawKey=\"" + item.RawPositionKey + "\"");
								}
								else
								{
									MenuItemConfig orCreateItemAt = CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.GetOrCreateItemAt(hit, Point.Empty);
									if (orCreateItemAt == null)
									{
										num2++;
										AppendQuickerImportLog("Skip item because target MenuItemConfig is Nothing. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index);
									}
									else
									{
										string text = (orCreateItemAt.IconPath ?? "").Trim();
										CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyParsedQuickerWheelItemToMenuItem(item, orCreateItemAt);
										CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyOuterRingDefaultColor(orCreateItemAt);
										AppendQuickerImportLog("Applied item. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index + ", title=\"" + item.Title + "\", actionName=\"" + item.ActionName + "\", iconBefore=\"" + text + "\", iconAfter=\"" + orCreateItemAt.IconPath + "\"");
										if (!string.Equals(text, (orCreateItemAt.IconPath ?? "").Trim(), StringComparison.OrdinalIgnoreCase) && text.Length > 0 && CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.ContainsKey(text))
										{
											try
											{
												if (CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text] != null)
												{
													CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text].Dispose();
												}
											}
											catch (Exception projectError2)
											{
												ProjectData.SetProjectError(projectError2);
												ProjectData.ClearProjectError();
											}
											CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.Remove(text);
										}
										num++;
									}
								}
							}
							RadialMenuConfigStore.NormalizeMenuItems(CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.configs);
							CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RecalculatePages();
							CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.UpdateStatus();
							CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.renderPanel.Invalidate();
							if (CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
							{
								CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "导入完成（" + num + "项）";
							}
							try
							{
								CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerClipboardDemoUpgradeForImportedItems(CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
							}
							catch (Exception projectError3)
							{
								ProjectData.SetProjectError(projectError3);
								ProjectData.ClearProjectError();
							}
							try
							{
								CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerMenuUpgradeForImportedItems(CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
								return;
							}
							catch (Exception projectError4)
							{
								ProjectData.SetProjectError(projectError4);
								ProjectData.ClearProjectError();
								return;
							}
						}
						if (CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
						{
							CS_0024_003C_003E8__locals28._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "";
						}
						MessageBox.Show("解析失败或轮盘为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__217_002D1
	{
		public Task<List<QuickerWheelParsedItem>> _0024VB_0024Local_t;

		public _Closure_0024__217_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__217_002D1(_Closure_0024__217_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_t = arg0._0024VB_0024Local_t;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel != null)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel.Enabled = true;
			}
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId != _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				return;
			}
			if (_0024VB_0024Local_t == null || _0024VB_0024Local_t.IsFaulted || _0024VB_0024Local_t.Result == null || _0024VB_0024Local_t.Result.Count == 0)
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "";
				}
				MessageBox.Show("解析失败或轮盘为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			List<QuickerWheelParsedItem> result = _0024VB_0024Local_t.Result;
			AppendQuickerImportLog("ImportQuickerWheelFromClipboard parsed count=" + result.Count);
			int num = 0;
			int num2 = 0;
			checked
			{
				HitInfo hit = default(HitInfo);
				foreach (QuickerWheelParsedItem item in result)
				{
					if (item == null)
					{
						num2++;
						AppendQuickerImportLog("Skip item because parsed entry is Nothing");
						continue;
					}
					if (!int.TryParse((item.RawPositionKey ?? "").Trim(), out var result2))
					{
						num2++;
						AppendQuickerImportLog("Skip item because RawPositionKey is invalid: \"" + item.RawPositionKey + "\"");
						continue;
					}
					if (!TryMapQuickerWheelPositionToHit(result2, ref hit))
					{
						num2++;
						AppendQuickerImportLog("Skip item because position mapping failed. keyInt=" + result2 + ", rawKey=\"" + item.RawPositionKey + "\"");
						continue;
					}
					MenuItemConfig orCreateItemAt = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.GetOrCreateItemAt(hit, Point.Empty);
					if (orCreateItemAt == null)
					{
						num2++;
						AppendQuickerImportLog("Skip item because target MenuItemConfig is Nothing. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index);
						continue;
					}
					string text = (orCreateItemAt.IconPath ?? "").Trim();
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyParsedQuickerWheelItemToMenuItem(item, orCreateItemAt);
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyOuterRingDefaultColor(orCreateItemAt);
					AppendQuickerImportLog("Applied item. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index + ", title=\"" + item.Title + "\", actionName=\"" + item.ActionName + "\", iconBefore=\"" + text + "\", iconAfter=\"" + orCreateItemAt.IconPath + "\"");
					if (!string.Equals(text, (orCreateItemAt.IconPath ?? "").Trim(), StringComparison.OrdinalIgnoreCase) && text.Length > 0 && _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.ContainsKey(text))
					{
						try
						{
							if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text] != null)
							{
								_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text].Dispose();
							}
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
						_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.Remove(text);
					}
					num++;
				}
				RadialMenuConfigStore.NormalizeMenuItems(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.configs);
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RecalculatePages();
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.UpdateStatus();
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.renderPanel.Invalidate();
				if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "导入完成（" + num + "项）";
				}
				try
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerClipboardDemoUpgradeForImportedItems(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				try
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerMenuUpgradeForImportedItems(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__219_002D0
	{
		public int _0024VB_0024Local_batchId;

		public RadialMenuSettingsForm _0024VB_0024Me;

		public _Closure_0024__219_002D0(_Closure_0024__219_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_batchId = arg0._0024VB_0024Local_batchId;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__219_002D1
	{
		public string _0024VB_0024Local_actionId;

		public _Closure_0024__219_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__219_002D1(_Closure_0024__219_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_actionId = arg0._0024VB_0024Local_actionId;
			}
		}

		[SpecialName]
		internal async Task _Lambda_0024__0()
		{
			_Closure_0024__219_002D2 arg = default(_Closure_0024__219_002D2);
			_Closure_0024__219_002D2 CS_0024_003C_003E8__locals13 = new _Closure_0024__219_002D2(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3 = this
			};
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me == null || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.IsDisposed || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId != _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				return;
			}
			string clipboardDemoOutFilePath = GetClipboardDemoOutFilePath(_0024VB_0024Local_actionId);
			if (string.IsNullOrWhiteSpace(clipboardDemoOutFilePath))
			{
				return;
			}
			bool flag = false;
			try
			{
				if (File.Exists(clipboardDemoOutFilePath))
				{
					TimeSpan timeSpan = DateTime.UtcNow - File.GetLastWriteTimeUtc(clipboardDemoOutFilePath);
					if (timeSpan.TotalSeconds >= 0.0 && timeSpan.TotalMinutes <= 10.0)
					{
						bool flag2 = false;
						try
						{
							string text = File.ReadAllText(clipboardDemoOutFilePath, Encoding.UTF8);
							if (!string.IsNullOrWhiteSpace(text))
							{
								flag2 = text.IndexOf("Icon=", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("Title=", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("右键菜单=", StringComparison.OrdinalIgnoreCase) >= 0;
							}
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							flag2 = false;
							ProjectData.ClearProjectError();
						}
						if (flag2)
						{
							flag = true;
						}
					}
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				flag = false;
				ProjectData.ClearProjectError();
			}
			if (!flag)
			{
				try
				{
					if (File.Exists(clipboardDemoOutFilePath))
					{
						File.Delete(clipboardDemoOutFilePath);
					}
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				try
				{
					await quickerClipboardDemoGate.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
					return;
				}
				try
				{
					if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me == null || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.IsDisposed || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId != _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
					{
						return;
					}
					TryTriggerClipboardDemoSubprogram(_0024VB_0024Local_actionId);
					try
					{
						await WaitForFileCreatedAsync(clipboardDemoOutFilePath, 8000).ConfigureAwait(continueOnCapturedContext: false);
						await WaitForFileReadyAsync(clipboardDemoOutFilePath, 2000).ConfigureAwait(continueOnCapturedContext: false);
					}
					catch (Exception projectError5)
					{
						ProjectData.SetProjectError(projectError5);
						ProjectData.ClearProjectError();
						return;
					}
				}
				catch (Exception projectError6)
				{
					ProjectData.SetProjectError(projectError6);
					ProjectData.ClearProjectError();
				}
				finally
				{
					try
					{
						quickerClipboardDemoGate.Release();
					}
					catch (Exception projectError7)
					{
						ProjectData.SetProjectError(projectError7);
						ProjectData.ClearProjectError();
					}
				}
			}
			CS_0024_003C_003E8__locals13._0024VB_0024Local_title = "";
			CS_0024_003C_003E8__locals13._0024VB_0024Local_icon = "";
			CS_0024_003C_003E8__locals13._0024VB_0024Local_menuText = "";
			bool flag3;
			try
			{
				flag3 = TryLoadClipboardDemoSubprogramResult(_0024VB_0024Local_actionId, ref CS_0024_003C_003E8__locals13._0024VB_0024Local_title, ref CS_0024_003C_003E8__locals13._0024VB_0024Local_icon, ref CS_0024_003C_003E8__locals13._0024VB_0024Local_menuText);
			}
			catch (Exception projectError8)
			{
				ProjectData.SetProjectError(projectError8);
				flag3 = false;
				ProjectData.ClearProjectError();
			}
			if (!flag3)
			{
				return;
			}
			try
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
				{
					if (CS_0024_003C_003E8__locals13._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals13._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
					{
						CS_0024_003C_003E8__locals13._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyClipboardDemoResultToMatchingItems(CS_0024_003C_003E8__locals13._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, CS_0024_003C_003E8__locals13._0024VB_0024Local_title, CS_0024_003C_003E8__locals13._0024VB_0024Local_icon, CS_0024_003C_003E8__locals13._0024VB_0024Local_menuText);
					}
				}));
			}
			catch (Exception projectError9)
			{
				ProjectData.SetProjectError(projectError9);
				ProjectData.ClearProjectError();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__219_002D2
	{
		public string _0024VB_0024Local_title;

		public string _0024VB_0024Local_icon;

		public string _0024VB_0024Local_menuText;

		public _Closure_0024__219_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__219_002D2(_Closure_0024__219_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_title = arg0._0024VB_0024Local_title;
				_0024VB_0024Local_icon = arg0._0024VB_0024Local_icon;
				_0024VB_0024Local_menuText = arg0._0024VB_0024Local_menuText;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyClipboardDemoResultToMatchingItems(_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, _0024VB_0024Local_title, _0024VB_0024Local_icon, _0024VB_0024Local_menuText);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__221_002D0
	{
		public int _0024VB_0024Local_batchId;

		public RadialMenuSettingsForm _0024VB_0024Me;

		public _Closure_0024__221_002D0(_Closure_0024__221_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_batchId = arg0._0024VB_0024Local_batchId;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__221_002D1
	{
		public string _0024VB_0024Local_actionId;

		public _Closure_0024__221_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__221_002D1(_Closure_0024__221_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_actionId = arg0._0024VB_0024Local_actionId;
			}
		}

		[SpecialName]
		internal async Task _Lambda_0024__1()
		{
			_Closure_0024__221_002D2 arg = default(_Closure_0024__221_002D2);
			_Closure_0024__221_002D2 CS_0024_003C_003E8__locals20 = new _Closure_0024__221_002D2(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3 = this
			};
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me == null || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.IsDisposed || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId != _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				return;
			}
			CS_0024_003C_003E8__locals20._0024VB_0024Local_cached = null;
			try
			{
				object quickerMenuFetchCacheLock = RadialMenuSettingsForm.quickerMenuFetchCacheLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(quickerMenuFetchCacheLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(quickerMenuFetchCacheLock, ref lockTaken);
					QuickerMenuFetchCacheEntry value = null;
					if (quickerMenuFetchCache.TryGetValue(_0024VB_0024Local_actionId, out value) && value != null)
					{
						long num = checked(DateTime.UtcNow.Ticks - value.FetchedAtUtcTicks);
						if (num >= 0 && num <= quickerMenuFetchCacheTtl.Ticks)
						{
							CS_0024_003C_003E8__locals20._0024VB_0024Local_cached = value.Items;
						}
						else
						{
							quickerMenuFetchCache.Remove(_0024VB_0024Local_actionId);
						}
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(quickerMenuFetchCacheLock);
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				CS_0024_003C_003E8__locals20._0024VB_0024Local_cached = null;
				ProjectData.ClearProjectError();
			}
			if (CS_0024_003C_003E8__locals20._0024VB_0024Local_cached != null && HasCustomQuickerMenuItems(CS_0024_003C_003E8__locals20._0024VB_0024Local_cached))
			{
				try
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
					{
						if (CS_0024_003C_003E8__locals20._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals20._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
						{
							CS_0024_003C_003E8__locals20._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(CS_0024_003C_003E8__locals20._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, CS_0024_003C_003E8__locals20._0024VB_0024Local_cached);
						}
					}));
					return;
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
					return;
				}
			}
			try
			{
				QuickerActionMetadata quickerActionMetadata = LookupQuickerMetadataIfLoaded(_0024VB_0024Local_actionId);
				if (quickerActionMetadata != null && quickerActionMetadata.QuickerMenuItems != null && HasCustomQuickerMenuItems(quickerActionMetadata.QuickerMenuItems))
				{
					_Closure_0024__221_002D3 arg2 = default(_Closure_0024__221_002D3);
					_Closure_0024__221_002D3 CS_0024_003C_003E8__locals25 = new _Closure_0024__221_002D3(arg2)
					{
						_0024VB_0024NonLocal__0024VB_0024Closure_4 = CS_0024_003C_003E8__locals20,
						_0024VB_0024Local_snapshot = (from x in quickerActionMetadata.QuickerMenuItems
							where x != null
							select new QuickerRightClickMenuItemConfig
							{
								Icon = x.Icon,
								DisplayText = x.DisplayText,
								DisplayDescription = x.DisplayDescription,
								Parameter = x.Parameter,
								Marker = x.Marker,
								IsGroupHeader = x.IsGroupHeader,
								GroupParent = x.GroupParent
							}).ToList()
					};
					try
					{
						object quickerMenuFetchCacheLock2 = RadialMenuSettingsForm.quickerMenuFetchCacheLock;
						ObjectFlowControl.CheckForSyncLockOnValueType(quickerMenuFetchCacheLock2);
						bool lockTaken2 = false;
						try
						{
							Monitor.Enter(quickerMenuFetchCacheLock2, ref lockTaken2);
							quickerMenuFetchCache[_0024VB_0024Local_actionId] = new QuickerMenuFetchCacheEntry
							{
								FetchedAtUtcTicks = DateTime.UtcNow.Ticks,
								Items = CS_0024_003C_003E8__locals25._0024VB_0024Local_snapshot
							};
						}
						finally
						{
							if (lockTaken2)
							{
								Monitor.Exit(quickerMenuFetchCacheLock2);
							}
						}
					}
					catch (Exception projectError3)
					{
						ProjectData.SetProjectError(projectError3);
						ProjectData.ClearProjectError();
					}
					try
					{
						_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
						{
							if (CS_0024_003C_003E8__locals25._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals25._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
							{
								CS_0024_003C_003E8__locals25._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(CS_0024_003C_003E8__locals25._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, CS_0024_003C_003E8__locals25._0024VB_0024Local_snapshot);
							}
						}));
						return;
					}
					catch (Exception projectError4)
					{
						ProjectData.SetProjectError(projectError4);
						ProjectData.ClearProjectError();
						return;
					}
				}
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			try
			{
				await quickerMenuFetchGate.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
				return;
			}
			try
			{
				_Closure_0024__221_002D4 arg3 = default(_Closure_0024__221_002D4);
				_Closure_0024__221_002D4 CS_0024_003C_003E8__locals30 = new _Closure_0024__221_002D4(arg3)
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_5 = CS_0024_003C_003E8__locals20
				};
				if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me == null || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.IsDisposed || _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId != _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
				{
					return;
				}
				IDictionary<string, object> quickerActionFullMetadata = GetQuickerActionFullMetadata(_0024VB_0024Local_actionId);
				if (quickerActionFullMetadata == null || quickerActionFullMetadata.Count == 0)
				{
					return;
				}
				CS_0024_003C_003E8__locals30._0024VB_0024Local_menu = TryExtractQuickerMenuItemsFromWheelActionData(quickerActionFullMetadata, _0024VB_0024Local_actionId);
				if (CS_0024_003C_003E8__locals30._0024VB_0024Local_menu == null || CS_0024_003C_003E8__locals30._0024VB_0024Local_menu.Count == 0 || !HasCustomQuickerMenuItems(CS_0024_003C_003E8__locals30._0024VB_0024Local_menu))
				{
					return;
				}
				try
				{
					List<QuickerRightClickMenuItemConfig> items = (from x in CS_0024_003C_003E8__locals30._0024VB_0024Local_menu
						where x != null
						select new QuickerRightClickMenuItemConfig
						{
							Icon = x.Icon,
							DisplayText = x.DisplayText,
							DisplayDescription = x.DisplayDescription,
							Parameter = x.Parameter,
							Marker = x.Marker,
							IsGroupHeader = x.IsGroupHeader,
							GroupParent = x.GroupParent
						}).ToList();
					object quickerMenuFetchCacheLock3 = RadialMenuSettingsForm.quickerMenuFetchCacheLock;
					ObjectFlowControl.CheckForSyncLockOnValueType(quickerMenuFetchCacheLock3);
					bool lockTaken3 = false;
					try
					{
						Monitor.Enter(quickerMenuFetchCacheLock3, ref lockTaken3);
						quickerMenuFetchCache[_0024VB_0024Local_actionId] = new QuickerMenuFetchCacheEntry
						{
							FetchedAtUtcTicks = DateTime.UtcNow.Ticks,
							Items = items
						};
						if (quickerMenuFetchCache.Count > 512)
						{
							quickerMenuFetchCache.Clear();
						}
					}
					finally
					{
						if (lockTaken3)
						{
							Monitor.Exit(quickerMenuFetchCacheLock3);
						}
					}
				}
				catch (Exception projectError7)
				{
					ProjectData.SetProjectError(projectError7);
					ProjectData.ClearProjectError();
				}
				try
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.BeginInvoke((Action)([SpecialName] () =>
					{
						if (CS_0024_003C_003E8__locals30._0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals30._0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
						{
							CS_0024_003C_003E8__locals30._0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(CS_0024_003C_003E8__locals30._0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, CS_0024_003C_003E8__locals30._0024VB_0024Local_menu);
						}
					}));
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					ProjectData.ClearProjectError();
				}
			}
			catch (Exception projectError9)
			{
				ProjectData.SetProjectError(projectError9);
				ProjectData.ClearProjectError();
			}
			finally
			{
				try
				{
					quickerMenuFetchGate.Release();
				}
				catch (Exception projectError10)
				{
					ProjectData.SetProjectError(projectError10);
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__221_002D2
	{
		public List<QuickerRightClickMenuItemConfig> _0024VB_0024Local_cached;

		public _Closure_0024__221_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__221_002D2(_Closure_0024__221_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_cached = arg0._0024VB_0024Local_cached;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, _0024VB_0024Local_cached);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__221_002D3
	{
		public List<QuickerRightClickMenuItemConfig> _0024VB_0024Local_snapshot;

		public _Closure_0024__221_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_4;

		public _Closure_0024__221_002D3(_Closure_0024__221_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_snapshot = arg0._0024VB_0024Local_snapshot;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__5()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == _0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, _0024VB_0024Local_snapshot);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__221_002D4
	{
		public List<QuickerRightClickMenuItemConfig> _0024VB_0024Local_menu;

		public _Closure_0024__221_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_5;

		public _Closure_0024__221_002D4(_Closure_0024__221_002D4 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_menu = arg0._0024VB_0024Local_menu;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__8()
		{
			if (_0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == _0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyQuickerMenuToMatchingItems(_0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_actionId, _0024VB_0024Local_menu);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__91_002D0
	{
		public double _0024VB_0024Local_dpiScale;

		public Func<int, int> _0024VB_0024Local_ScaleI;

		public RadialMenuSettingsForm _0024VB_0024Me;

		[SpecialName]
		internal int _Lambda_0024__0(int v)
		{
			return Math.Max(1, checked((int)Math.Round((double)v * _0024VB_0024Local_dpiScale)));
		}

		[SpecialName]
		internal void _Lambda_0024__R14(object a0, EventArgs a1)
		{
			_Lambda_0024__14();
		}

		[SpecialName]
		internal void _Lambda_0024__14()
		{
			double num = 1.0;
			try
			{
				num = _0024VB_0024Me.sizeConfig.SkinOpacity;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				num = 1.0;
				ProjectData.ClearProjectError();
			}
			if (double.IsNaN(num) || double.IsInfinity(num))
			{
				num = 1.0;
			}
			num = Math.Max(0.0, Math.Min(1.0, num));
			checked
			{
				using Form form = new Form();
				_Closure_0024__91_002D1 CS_0024_003C_003E8__locals12 = new _Closure_0024__91_002D1();
				form.Text = "皮肤不透明度";
				form.StartPosition = FormStartPosition.CenterParent;
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.MaximizeBox = false;
				form.MinimizeBox = false;
				form.ShowInTaskbar = false;
				form.AutoScaleMode = AutoScaleMode.Dpi;
				form.ClientSize = new Size(_0024VB_0024Local_ScaleI(360), _0024VB_0024Local_ScaleI(180));
				int num2 = _0024VB_0024Local_ScaleI(16);
				int num3 = _0024VB_0024Local_ScaleI(70);
				int num4 = _0024VB_0024Local_ScaleI(28);
				int num5 = _0024VB_0024Local_ScaleI(12);
				int height = _0024VB_0024Local_ScaleI(45);
				CS_0024_003C_003E8__locals12._0024VB_0024Local_lblValue = new Label
				{
					Text = "0.00",
					AutoSize = true,
					Location = new Point(num2, num2)
				};
				CS_0024_003C_003E8__locals12._0024VB_0024Local_track = new TrackBar
				{
					Minimum = 0,
					Maximum = 100,
					TickFrequency = 5,
					LargeChange = 5,
					SmallChange = 1,
					Value = Math.Max(0, Math.Min(100, (int)Math.Round(num * 100.0))),
					AutoSize = false,
					Location = new Point(num2, CS_0024_003C_003E8__locals12._0024VB_0024Local_lblValue.Bottom + _0024VB_0024Local_ScaleI(10)),
					Size = new Size(Math.Max(_0024VB_0024Local_ScaleI(120), form.ClientSize.Width - num2 * 2), height),
					Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)
				};
				int y = form.ClientSize.Height - num2 - num4;
				Button button = new Button
				{
					Text = "取消",
					DialogResult = DialogResult.Cancel,
					Size = new Size(num3, num4),
					Location = new Point(form.ClientSize.Width - num2 - num3, y),
					Anchor = (AnchorStyles.Bottom | AnchorStyles.Right)
				};
				Button button2 = new Button
				{
					Text = "确定",
					DialogResult = DialogResult.OK,
					Size = new Size(num3, num4),
					Location = new Point(button.Left - num5 - num3, y),
					Anchor = (AnchorStyles.Bottom | AnchorStyles.Right)
				};
				CS_0024_003C_003E8__locals12._0024VB_0024Local_updateLabel = [SpecialName] () =>
				{
					CS_0024_003C_003E8__locals12._0024VB_0024Local_lblValue.Text = ((double)CS_0024_003C_003E8__locals12._0024VB_0024Local_track.Value / 100.0).ToString("0.00");
				};
				CS_0024_003C_003E8__locals12._0024VB_0024Local_track.Scroll += [SpecialName] (object a0, EventArgs a1) =>
				{
					CS_0024_003C_003E8__locals12._Lambda_0024__16();
				};
				CS_0024_003C_003E8__locals12._0024VB_0024Local_updateLabel();
				form.Controls.AddRange(new Control[4] { CS_0024_003C_003E8__locals12._0024VB_0024Local_lblValue, CS_0024_003C_003E8__locals12._0024VB_0024Local_track, button2, button });
				form.AcceptButton = button2;
				form.CancelButton = button;
				if (form.ShowDialog(_0024VB_0024Me) == DialogResult.OK)
				{
					_0024VB_0024Me.sizeConfig.SkinOpacity = Math.Max(0.0, Math.Min(1.0, (double)CS_0024_003C_003E8__locals12._0024VB_0024Local_track.Value / 100.0));
					_0024VB_0024Me.renderPanel.Invalidate();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__91_002D1
	{
		public Label _0024VB_0024Local_lblValue;

		public TrackBar _0024VB_0024Local_track;

		public Action _0024VB_0024Local_updateLabel;

		[SpecialName]
		internal void _Lambda_0024__15()
		{
			_0024VB_0024Local_lblValue.Text = ((double)_0024VB_0024Local_track.Value / 100.0).ToString("0.00");
		}

		[SpecialName]
		internal void _Lambda_0024__R15(object a0, EventArgs a1)
		{
			_Lambda_0024__16();
		}

		[SpecialName]
		internal void _Lambda_0024__16()
		{
			_0024VB_0024Local_updateLabel();
		}
	}

	private const float WheelRotationDeg = 22.5f;

	private const string BackCommand = "_BACK";

	private readonly RadialMenuConfigStore store;

	private RadialMenuAppConfig appConfig;

	private string currentProcessName;

	private List<MenuItemConfig> configs;

	private MenuSizeConfig sizeConfig;

	private string currentParentMenuName;

	private int currentPage;

	private int totalPages;

	private string lastProcessScopeName;

	private string lastProcessScopeParentMenuName;

	private int lastProcessScopePage;

	[CompilerGenerated]
	[AccessedThroughProperty("renderPanel")]
	private Panel _renderPanel;

	private FlowLayoutPanel pageOpsPanel;

	private Button btnCopyPage;

	private Button btnPastePage;

	private Button btnClearPage;

	private Button btnImportQuickerWheel;

	private Button btnDeleteScope;

	private Label lblStatus;

	private Button btnSave;

	private Button btnClose;

	private Button btnBack;

	private ComboBox cboProcess;

	private Button btnEditProcessAliases;

	private CheckBox chkUseCadInfiniteRadialMenu;

	private Label lblContact;

	private NumericUpDown nudRootFontSize;

	private Button btnAppearance;

	private ContextMenuStrip appearanceMenu;

	private ToolStripMenuItem miShowDividers;

	private ToolStripMenuItem miShowInnerDividers;

	private ToolStripMenuItem miOuterRingGray;

	private ToolStripMenuItem miSkinImage;

	private ToolStripMenuItem miSkinOpacity;

	private ToolStripMenuItem miClearSkinImage;

	private bool suppressAppearanceChange;

	private bool suppressScopeChange;

	private List<ScopeItem> scopeAllItems;

	private bool scopeApplyingFilter;

	private bool isDragging;

	private MenuItemConfig dragItem;

	private Point dragStartPos;

	private Point dragCurrentPos;

	private int hoverSectorIndex;

	private int hoverSectorLevel;

	private bool isExternalDragging;

	private int dragOriginalSectorIndex;

	private int dragOriginalLevel;

	private int dragOriginalPage;

	private bool previewActive;

	private MenuItemConfig previewSwapTarget;

	private int previewSwapTargetSectorIndex;

	private int previewSwapTargetLevel;

	private int previewSwapTargetPage;

	private readonly Dictionary<string, Image> previewIconCache;

	private Image previewSkinImage;

	private MemoryStream previewSkinImageStream;

	private string previewSkinImagePath;

	private MenuItemConfig clipboardItemTemplate;

	private bool clipboardIsCut;

	private List<MenuItemConfig> clipboardPageTemplate;

	private static readonly object degLogLock = RuntimeHelpers.GetObjectValue(new object());

	private Font menuFont;

	private bool cursorRestoreQueued;

	private System.Windows.Forms.Timer cursorRestoreTimer;

	private int cursorRestoreTicksLeft;

	private int quickerImportBatchId;

	private static readonly SemaphoreSlim quickerMenuFetchGate = new SemaphoreSlim(2, 2);

	private static readonly SemaphoreSlim quickerClipboardDemoGate = new SemaphoreSlim(1, 1);

	private static readonly TimeSpan quickerMenuFetchCacheTtl = TimeSpan.FromMinutes(10.0);

	private static readonly Dictionary<string, QuickerMenuFetchCacheEntry> quickerMenuFetchCache = new Dictionary<string, QuickerMenuFetchCacheEntry>(StringComparer.OrdinalIgnoreCase);

	private static readonly object quickerMenuFetchCacheLock = RuntimeHelpers.GetObjectValue(new object());

	private static readonly Dictionary<string, QuickerActionMetadata> quickerMetadataIndex = new Dictionary<string, QuickerActionMetadata>(StringComparer.OrdinalIgnoreCase);

	private static bool quickerMetadataLoaded = false;

	private static object quickerMetadataLoadLock = RuntimeHelpers.GetObjectValue(new object());

	private Panel renderPanel
	{
		[CompilerGenerated]
		get
		{
			return _renderPanel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = renderPanel_Paint;
			MouseEventHandler value3 = renderPanel_MouseWheel;
			MouseEventHandler value4 = renderPanel_MouseDown;
			MouseEventHandler value5 = renderPanel_MouseMove;
			MouseEventHandler value6 = renderPanel_MouseUp;
			DragEventHandler value7 = renderPanel_DragEnter;
			DragEventHandler value8 = renderPanel_DragOver;
			EventHandler value9 = renderPanel_DragLeave;
			DragEventHandler value10 = renderPanel_DragDrop;
			MouseEventHandler value11 = renderPanel_DoubleClick;
			Panel panel = _renderPanel;
			if (panel != null)
			{
				panel.Paint -= value2;
				panel.MouseWheel -= value3;
				panel.MouseDown -= value4;
				panel.MouseMove -= value5;
				panel.MouseUp -= value6;
				panel.DragEnter -= value7;
				panel.DragOver -= value8;
				panel.DragLeave -= value9;
				panel.DragDrop -= value10;
				panel.MouseDoubleClick -= value11;
			}
			_renderPanel = value;
			panel = _renderPanel;
			if (panel != null)
			{
				panel.Paint += value2;
				panel.MouseWheel += value3;
				panel.MouseDown += value4;
				panel.MouseMove += value5;
				panel.MouseUp += value6;
				panel.DragEnter += value7;
				panel.DragOver += value8;
				panel.DragLeave += value9;
				panel.DragDrop += value10;
				panel.MouseDoubleClick += value11;
			}
		}
	}

	public event ConfigSavedEventHandler ConfigSaved;

	public RadialMenuSettingsForm(RadialMenuConfigStore store, RadialMenuAppConfig config, string initialProcessName)
	{
		currentProcessName = "";
		currentParentMenuName = "ROOT";
		currentPage = 0;
		totalPages = 1;
		lastProcessScopeName = "";
		lastProcessScopeParentMenuName = "ROOT";
		lastProcessScopePage = 0;
		suppressAppearanceChange = false;
		suppressScopeChange = false;
		scopeAllItems = new List<ScopeItem>();
		scopeApplyingFilter = false;
		isDragging = false;
		dragItem = null;
		hoverSectorIndex = -1;
		hoverSectorLevel = -1;
		isExternalDragging = false;
		dragOriginalSectorIndex = -1;
		dragOriginalLevel = -1;
		dragOriginalPage = -1;
		previewActive = false;
		previewSwapTarget = null;
		previewSwapTargetSectorIndex = -1;
		previewSwapTargetLevel = -1;
		previewSwapTargetPage = -1;
		previewIconCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
		previewSkinImage = null;
		previewSkinImageStream = null;
		previewSkinImagePath = "";
		clipboardItemTemplate = null;
		clipboardIsCut = false;
		clipboardPageTemplate = null;
		cursorRestoreQueued = false;
		cursorRestoreTimer = null;
		cursorRestoreTicksLeft = 0;
		quickerImportBatchId = 0;
		this.store = store;
		appConfig = config;
		currentProcessName = (initialProcessName ?? "").Trim();
		Text = "轮盘菜单设置";
		try
		{
			base.Icon = RadialMenuController.CreateTitleIcon("轮盘");
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		RadialMenuConfigStore.NormalizeConfig(appConfig);
		sizeConfig = appConfig.MenuSize;
		try
		{
			RadialMenuController.ApplyMenuSizeDpiProfileToBase(sizeConfig, base.DeviceDpi);
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		double dpiScale = GetDpiScale();
		Size size = new Size(1040, 790);
		int num = size.Width;
		int num2 = size.Height;
		if (sizeConfig.SettingsWindowWidth > 0)
		{
			num = Math.Max(size.Width, sizeConfig.SettingsWindowWidth);
		}
		if (sizeConfig.SettingsWindowHeight > 0)
		{
			num2 = Math.Max(size.Height, sizeConfig.SettingsWindowHeight);
		}
		Rectangle workingArea;
		try
		{
			workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			workingArea = Screen.PrimaryScreen.WorkingArea;
			ProjectData.ClearProjectError();
		}
		checked
		{
			int val = (int)Math.Round((double)num * dpiScale);
			int val2 = (int)Math.Round((double)num2 * dpiScale);
			int val3 = Math.Max(320, (int)Math.Floor((double)workingArea.Width * 0.95));
			int val4 = Math.Max(240, (int)Math.Floor((double)workingArea.Height * 0.95));
			val = Math.Max(320, Math.Min(val, val3));
			val2 = Math.Max(240, Math.Min(val2, val4));
			base.Size = new Size(val, val2);
			int num3 = Math.Max(320, Math.Min((int)Math.Round((double)size.Width * dpiScale), Math.Max(320, workingArea.Width - 80)));
			int num4 = Math.Max(240, Math.Min((int)Math.Round((double)size.Height * dpiScale), Math.Max(240, workingArea.Height - 80)));
			MinimumSize = new Size(num3, num4);
			base.StartPosition = FormStartPosition.CenterScreen;
			BackColor = Color.White;
			base.AutoScaleMode = AutoScaleMode.Dpi;
			InitializeUI();
			LoadScope(currentProcessName);
		}
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		try
		{
			if (renderPanel != null)
			{
				renderPanel.Focus();
			}
			else
			{
				base.ActiveControl = null;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	protected override void OnActivated(EventArgs e)
	{
		base.OnActivated(e);
		try
		{
			RequestCursorRestore();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private bool EnsureCursorVisible()
	{
		checked
		{
			try
			{
				NativeMethods.CURSORINFO pci = default(NativeMethods.CURSORINFO);
				pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.CURSORINFO));
				if (!NativeMethods.GetCursorInfo(ref pci))
				{
					return true;
				}
				if ((pci.flags & 1) != 0)
				{
					return true;
				}
				try
				{
					Logger.Log("EnsureCursorVisible: Cursor hidden, attempting restore");
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				try
				{
					Cursor.Show();
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				int num = NativeMethods.ShowCursor(bShow: true);
				int num2 = 0;
				while (num < 0 && num2 < 128)
				{
					num = NativeMethods.ShowCursor(bShow: true);
					num2++;
				}
				try
				{
					NativeMethods.SetCursor((Cursor.Current = ((cboProcess != null && cboProcess.ContainsFocus) ? Cursors.IBeam : Cursors.Default)).Handle);
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				try
				{
					NativeMethods.mouse_event(1, 1, 0, 0, IntPtr.Zero);
					NativeMethods.mouse_event(1, -1, 0, 0, IntPtr.Zero);
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
				}
				try
				{
					NativeMethods.INPUT pInputs = new NativeMethods.INPUT
					{
						type = 0,
						mi = new NativeMethods.MOUSEINPUT
						{
							dx = 1,
							dy = 0,
							mouseData = 0,
							dwFlags = 1,
							time = 0,
							dwExtraInfo = IntPtr.Zero
						}
					};
					NativeMethods.SendInput(1u, ref pInputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
					pInputs.mi = new NativeMethods.MOUSEINPUT
					{
						dx = -1,
						dy = 0,
						mouseData = 0,
						dwFlags = 1,
						time = 0,
						dwExtraInfo = IntPtr.Zero
					};
					NativeMethods.SendInput(1u, ref pInputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
				}
				catch (Exception projectError5)
				{
					ProjectData.SetProjectError(projectError5);
					ProjectData.ClearProjectError();
				}
				try
				{
					Point position = Cursor.Position;
					Rectangle bounds = Screen.FromPoint(position).Bounds;
					int num3 = Math.Min(Math.Max(position.X + 1, bounds.Left), bounds.Right - 1);
					int num4 = Math.Min(Math.Max(position.Y, bounds.Top), bounds.Bottom - 1);
					NativeMethods.SetCursorPos(num3, num4);
					NativeMethods.SetCursorPos(position.X, position.Y);
				}
				catch (Exception projectError6)
				{
					ProjectData.SetProjectError(projectError6);
					ProjectData.ClearProjectError();
				}
				pci.cbSize = Marshal.SizeOf(typeof(NativeMethods.CURSORINFO));
				NativeMethods.GetCursorInfo(ref pci);
				bool result = (pci.flags & 1) != 0;
				try
				{
					IntPtr intPtr = IntPtr.Zero;
					try
					{
						intPtr = NativeMethods.GetCursor();
					}
					catch (Exception projectError7)
					{
						ProjectData.SetProjectError(projectError7);
						ProjectData.ClearProjectError();
					}
					Logger.Log("EnsureCursorVisible: done, showing=" + result + ", count=" + num + ", hCursor=" + pci.hCursor + ", cur=" + intPtr);
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					ProjectData.ClearProjectError();
				}
				return result;
			}
			catch (Exception projectError9)
			{
				ProjectData.SetProjectError(projectError9);
				ProjectData.ClearProjectError();
			}
			return true;
		}
	}

	private void RequestCursorRestore()
	{
		cursorRestoreTicksLeft = 60;
		if (cursorRestoreTimer == null)
		{
			cursorRestoreTimer = new System.Windows.Forms.Timer
			{
				Interval = 50
			};
			cursorRestoreTimer.Tick += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__88_002D0();
			};
		}
		if (cursorRestoreTimer.Enabled)
		{
			return;
		}
		cursorRestoreTimer.Start();
		if (cursorRestoreQueued)
		{
			return;
		}
		cursorRestoreQueued = true;
		try
		{
			BeginInvoke((Action)([SpecialName] () =>
			{
				cursorRestoreQueued = false;
				EnsureCursorVisible();
			}));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			cursorRestoreQueued = false;
			ProjectData.ClearProjectError();
		}
	}

	private double GetDpiScale()
	{
		try
		{
			using Graphics graphics = CreateGraphics();
			float dpiX = graphics.DpiX;
			if (dpiX <= 0f)
			{
				return 1.0;
			}
			return (double)dpiX / 96.0;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return 1.0;
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		checked
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				double dpiScale = GetDpiScale();
				sizeConfig.SettingsWindowWidth = Math.Max(1, (int)Math.Round((double)base.Width / dpiScale));
				sizeConfig.SettingsWindowHeight = Math.Max(1, (int)Math.Round((double)base.Height / dpiScale));
			}
			try
			{
				foreach (KeyValuePair<string, Image> item in previewIconCache)
				{
					if (item.Value != null)
					{
						item.Value.Dispose();
					}
				}
				previewIconCache.Clear();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (previewSkinImage != null)
				{
					previewSkinImage.Dispose();
					previewSkinImage = null;
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			try
			{
				if (previewSkinImageStream != null)
				{
					previewSkinImageStream.Dispose();
					previewSkinImageStream = null;
				}
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
			base.OnFormClosing(e);
		}
	}

	private void InitializeUI()
	{
		_Closure_0024__91_002D0 CS_0024_003C_003E8__locals51 = new _Closure_0024__91_002D0();
		CS_0024_003C_003E8__locals51._0024VB_0024Me = this;
		CS_0024_003C_003E8__locals51._0024VB_0024Local_dpiScale = GetDpiScale();
		checked
		{
			CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI = [SpecialName] (int v) => Math.Max(1, (int)Math.Round((double)v * CS_0024_003C_003E8__locals51._0024VB_0024Local_dpiScale));
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
			{
				Dock = DockStyle.Top,
				Height = CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(38),
				BackColor = Color.White,
				FlowDirection = FlowDirection.LeftToRight,
				WrapContents = false,
				Padding = new Padding(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(12), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(12), 0)
			};
			Label label = new Label
			{
				Text = "编辑对象：",
				AutoSize = true,
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0)
			};
			cboProcess = new ComboBox
			{
				DropDownStyle = ComboBoxStyle.DropDown,
				Width = CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(160),
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(2), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(2), 0)
			};
			cboProcess.Cursor = Cursors.IBeam;
			cboProcess.TabIndex = 1;
			cboProcess.GotFocus += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D1();
			};
			cboProcess.MouseDown += [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				_Lambda_0024__91_002D2();
			};
			cboProcess.DropDown += [SpecialName] (object a0, EventArgs a1) =>
			{
				RequestCursorRestore();
			};
			cboProcess.DropDownClosed += [SpecialName] (object a0, EventArgs a1) =>
			{
				RequestCursorRestore();
			};
			cboProcess.MouseWheel += [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				RequestCursorRestore();
			};
			cboProcess.SelectedIndexChanged += OnScopeChanged;
			cboProcess.TextUpdate += OnScopeTextUpdate;
			cboProcess.KeyDown += OnScopeKeyDown;
			btnEditProcessAliases = new Button
			{
				Text = "…",
				Size = new Size(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(26), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28)),
				BackColor = Color.White,
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(0), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(10), 0),
				Enabled = false,
				TextAlign = ContentAlignment.MiddleCenter
			};
			btnEditProcessAliases.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				EditCurrentProcessAliases();
			};
			btnSave = new Button
			{
				Text = "保存配置",
				Size = new Size(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(80), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28)),
				BackColor = Color.White,
				Margin = new Padding(0, 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0)
			};
			btnSave.Click += SaveConfig;
			btnBack = new Button
			{
				Text = "返回上级",
				Size = new Size(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(80), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28)),
				BackColor = Color.White,
				Enabled = false,
				Margin = new Padding(0, 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0)
			};
			btnBack.Click += GoBack;
			btnClose = new Button
			{
				Text = "关闭",
				Size = new Size(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(60), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28)),
				BackColor = Color.White,
				Margin = new Padding(0, 0, 0, 0)
			};
			btnClose.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				Close();
			};
			base.CancelButton = btnClose;
			chkUseCadInfiniteRadialMenu = new CheckBox
			{
				Text = "使用CAD无限轮盘",
				AutoSize = true,
				Margin = new Padding(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0)
			};
			try
			{
				chkUseCadInfiniteRadialMenu.Checked = appConfig != null && appConfig.UseCadInfiniteRadialMenu;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				chkUseCadInfiniteRadialMenu.Checked = false;
				ProjectData.ClearProjectError();
			}
			Label label2 = new Label
			{
				Text = "轮盘字体大小",
				AutoSize = true,
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(7), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0)
			};
			nudRootFontSize = new NumericUpDown
			{
				Minimum = 6m,
				Maximum = 40m,
				Increment = 1m,
				Width = CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(52),
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(4), 0, 0)
			};
			try
			{
				nudRootFontSize.Value = Math.Max(nudRootFontSize.Minimum, Math.Min(nudRootFontSize.Maximum, new decimal(sizeConfig.FontSize)));
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				nudRootFontSize.Value = 12m;
				ProjectData.ClearProjectError();
			}
			nudRootFontSize.ValueChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D9();
			};
			btnAppearance = new Button
			{
				Text = "外观",
				Size = new Size(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(56), CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28)),
				BackColor = Color.White,
				Margin = new Padding(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0, 0, 0)
			};
			appearanceMenu = new ContextMenuStrip();
			miShowDividers = new ToolStripMenuItem("显示分割线")
			{
				CheckOnClick = true
			};
			miShowInnerDividers = new ToolStripMenuItem("显示圈内分隔线")
			{
				CheckOnClick = true
			};
			miOuterRingGray = new ToolStripMenuItem("最外圈灰色")
			{
				CheckOnClick = true
			};
			miSkinImage = new ToolStripMenuItem("皮肤图片...");
			miSkinOpacity = new ToolStripMenuItem("皮肤不透明度...");
			miClearSkinImage = new ToolStripMenuItem("取消皮肤");
			appearanceMenu.Items.Add(miShowDividers);
			appearanceMenu.Items.Add(miShowInnerDividers);
			appearanceMenu.Items.Add(miOuterRingGray);
			appearanceMenu.Items.Add(new ToolStripSeparator());
			appearanceMenu.Items.Add(miSkinImage);
			appearanceMenu.Items.Add(miSkinOpacity);
			appearanceMenu.Items.Add(miClearSkinImage);
			miShowDividers.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D10();
			};
			miShowInnerDividers.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D11();
			};
			miOuterRingGray.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D12();
			};
			miClearSkinImage.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D13();
			};
			miSkinOpacity.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals51._Lambda_0024__14();
			};
			miSkinImage.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D17();
			};
			btnAppearance.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__91_002D18();
			};
			flowLayoutPanel.Controls.AddRange(new Control[10] { label, cboProcess, btnEditProcessAliases, btnSave, btnBack, btnClose, chkUseCadInfiniteRadialMenu, label2, nudRootFontSize, btnAppearance });
			lblStatus = new Label
			{
				Dock = DockStyle.Top,
				Height = CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28),
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("Microsoft YaHei", 11f, FontStyle.Bold),
				BackColor = Color.White,
				Text = ""
			};
			renderPanel = new DoubleBufferedPanel
			{
				Dock = DockStyle.Fill,
				BackColor = Color.White
			};
			renderPanel.AllowDrop = true;
			renderPanel.TabStop = true;
			renderPanel.TabIndex = 0;
			renderPanel.KeyDown += renderPanel_KeyDown;
			pageOpsPanel = new FlowLayoutPanel
			{
				FlowDirection = FlowDirection.TopDown,
				WrapContents = false,
				AutoSize = true,
				BackColor = Color.Transparent,
				Anchor = (AnchorStyles.Top | AnchorStyles.Right),
				Location = new Point(0, 0),
				Margin = new Padding(0),
				Padding = new Padding(0)
			};
			Font font = new Font("Microsoft YaHei", 8.5f, FontStyle.Regular);
			Padding padding = new Padding(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0);
			btnCopyPage = new Button
			{
				Text = "复制当前轮盘设置",
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				BackColor = Color.White,
				Margin = new Padding(0, 0, 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6)),
				Font = font,
				Padding = padding,
				MinimumSize = new Size(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(22))
			};
			btnPastePage = new Button
			{
				Text = "粘贴轮盘设置",
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				BackColor = Color.White,
				Margin = new Padding(0, 0, 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6)),
				Enabled = false,
				Font = font,
				Padding = padding,
				MinimumSize = new Size(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(22))
			};
			btnClearPage = new Button
			{
				Text = "清空轮盘",
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				BackColor = Color.White,
				Margin = new Padding(0, 0, 0, 0),
				Font = font,
				Padding = padding,
				MinimumSize = new Size(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(22))
			};
			btnImportQuickerWheel = new Button
			{
				Text = "导入Quicker轮盘",
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				BackColor = Color.White,
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0, 0),
				Font = font,
				Padding = padding,
				MinimumSize = new Size(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(22))
			};
			btnDeleteScope = new Button
			{
				Text = "删除进程轮盘",
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				BackColor = Color.White,
				Margin = new Padding(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(6), 0, 0),
				Enabled = false,
				Font = font,
				Padding = padding,
				MinimumSize = new Size(0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(22))
			};
			btnCopyPage.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CopyCurrentPageSectors();
			};
			btnPastePage.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				PasteToCurrentPageSectors();
			};
			btnClearPage.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				ClearCurrentPageSectors();
			};
			btnClearPage.MouseUp += [SpecialName] (object sender, MouseEventArgs e) =>
			{
				if (e != null && e.Button == MouseButtons.Right)
				{
					ClearCurrentPageAllSectors();
				}
			};
			btnImportQuickerWheel.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				ImportQuickerWheelFromClipboard();
			};
			btnDeleteScope.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				DeleteCurrentProcessScope();
			};
			btnCopyPage.MouseWheel += renderPanel_MouseWheel;
			btnPastePage.MouseWheel += renderPanel_MouseWheel;
			btnClearPage.MouseWheel += renderPanel_MouseWheel;
			btnImportQuickerWheel.MouseWheel += renderPanel_MouseWheel;
			btnDeleteScope.MouseWheel += renderPanel_MouseWheel;
			pageOpsPanel.Controls.AddRange(new Control[5] { btnCopyPage, btnPastePage, btnClearPage, btnImportQuickerWheel, btnDeleteScope });
			renderPanel.Controls.Add(pageOpsPanel);
			pageOpsPanel.BringToFront();
			NormalizePageOpsButtonWidths();
			renderPanel.Resize += [SpecialName] (object a0, EventArgs a1) =>
			{
				PositionPageOpsPanel();
			};
			pageOpsPanel.SizeChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				PositionPageOpsPanel();
			};
			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
			{
				Dock = DockStyle.Bottom,
				Height = CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(28),
				BackColor = Color.White,
				ColumnCount = 1,
				RowCount = 1,
				Padding = new Padding(CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0, CS_0024_003C_003E8__locals51._0024VB_0024Local_ScaleI(8), 0)
			};
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			lblContact = new Label
			{
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("Microsoft YaHei", 10f, FontStyle.Regular),
				BackColor = Color.White,
				Text = "如有问题请联系冰雷木子，QQ234282231"
			};
			tableLayoutPanel.Controls.Add(lblContact, 0, 0);
			Panel panel = new Panel
			{
				Dock = DockStyle.Top,
				Height = flowLayoutPanel.Height + lblStatus.Height,
				BackColor = Color.White
			};
			panel.Controls.Add(lblStatus);
			panel.Controls.Add(flowLayoutPanel);
			base.Controls.Add(renderPanel);
			base.Controls.Add(tableLayoutPanel);
			base.Controls.Add(panel);
			menuFont = new Font("Microsoft YaHei", 10f, FontStyle.Bold);
			PositionPageOpsPanel();
			try
			{
				BeginInvoke((Action)([SpecialName] () =>
				{
					PositionPageOpsPanel();
				}));
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void NormalizePageOpsButtonWidths()
	{
		if (btnCopyPage != null && btnPastePage != null && btnClearPage != null && btnImportQuickerWheel != null && btnDeleteScope != null)
		{
			int num = Math.Max(btnCopyPage.PreferredSize.Width, Math.Max(btnPastePage.PreferredSize.Width, Math.Max(btnClearPage.PreferredSize.Width, Math.Max(btnImportQuickerWheel.PreferredSize.Width, btnDeleteScope.PreferredSize.Width))));
			int num2 = Math.Max(btnCopyPage.MinimumSize.Height, 22);
			btnCopyPage.AutoSize = false;
			btnPastePage.AutoSize = false;
			btnClearPage.AutoSize = false;
			btnImportQuickerWheel.AutoSize = false;
			btnDeleteScope.AutoSize = false;
			btnCopyPage.Size = new Size(num, num2);
			btnPastePage.Size = new Size(num, num2);
			btnClearPage.Size = new Size(num, num2);
			btnImportQuickerWheel.Size = new Size(num, num2);
			btnDeleteScope.Size = new Size(num, num2);
		}
	}

	private void PositionPageOpsPanel()
	{
		if (renderPanel == null || pageOpsPanel == null || sizeConfig == null)
		{
			return;
		}
		Size clientSize = renderPanel.ClientSize;
		if (clientSize.Width <= 0 || clientSize.Height <= 0)
		{
			return;
		}
		double dpiScale = GetDpiScale();
		Func<int, int> func = [SpecialName] (int v) => Math.Max(1, checked((int)Math.Round((double)v * dpiScale)));
		Point point = new Point(clientSize.Width / 2, clientSize.Height / 2);
		int num = Math.Min(clientSize.Width, clientSize.Height);
		checked
		{
			int num2 = sizeConfig.OuterRadius * 2 + func(24);
			double num3 = (double)num / (double)num2 * 0.995;
			int num4 = (int)Math.Round((double)sizeConfig.OuterRadius * num3);
			int num5 = func(6);
			int num6 = func(8);
			int num7 = func(8);
			if (dpiScale <= 1.05)
			{
				num7 = 0;
			}
			int val = clientSize.Width - pageOpsPanel.Width - num7;
			val = ((!(dpiScale > 1.05)) ? Math.Max(0, Math.Min(val, Math.Max(0, clientSize.Width - pageOpsPanel.Width))) : Math.Max(0, Math.Min(val, Math.Max(0, clientSize.Width - pageOpsPanel.Width))));
			int val2 = num6;
			Rectangle rect = new Rectangle(point.X - num4, point.Y - num4, num4 * 2, num4 * 2);
			rect.Inflate(num5, num5);
			if (new Rectangle(val, val2, pageOpsPanel.Width, pageOpsPanel.Height).IntersectsWith(rect))
			{
				int num8 = rect.Top - pageOpsPanel.Height - num5;
				if (num8 >= num6)
				{
					val2 = num8;
				}
				else
				{
					int num9 = rect.Bottom + num5;
					if (num9 + pageOpsPanel.Height <= clientSize.Height - num5)
					{
						val2 = num9;
					}
				}
			}
			val2 = Math.Max(0, Math.Min(val2, Math.Max(0, clientSize.Height - pageOpsPanel.Height)));
			pageOpsPanel.Location = new Point(val, val2);
		}
	}

	private List<MenuItemConfig> GetCurrentPageEditableItems()
	{
		if (configs == null)
		{
			return new List<MenuItemConfig>();
		}
		return configs.Where([SpecialName] (MenuItemConfig c) => c != null && Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) == 0 && c.Page == currentPage && c.Level != 2).ToList();
	}

	private void CopyCurrentPageSectors()
	{
		clipboardPageTemplate = RadialMenuConfigStore.CloneMenuItems(GetCurrentPageEditableItems());
		if (btnPastePage != null)
		{
			btnPastePage.Enabled = clipboardPageTemplate != null;
		}
	}

	private void PasteToCurrentPageSectors()
	{
		if (clipboardPageTemplate == null || configs == null)
		{
			return;
		}
		configs.RemoveAll([SpecialName] (MenuItemConfig c) => c != null && Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) == 0 && c.Page == currentPage && c.Level != 2);
		List<MenuItemConfig> list = RadialMenuConfigStore.CloneMenuItems(clipboardPageTemplate);
		foreach (MenuItemConfig item in list)
		{
			if (item != null && item.Level != 2)
			{
				int num = ((item.Level == 1) ? 15 : 7);
				if (item.SectorIndex >= 0 && item.SectorIndex <= num)
				{
					item.ParentMenuName = currentParentMenuName;
					item.Page = currentPage;
					configs.Add(item);
				}
			}
		}
		RecalculatePages();
		if (totalPages > 0)
		{
			currentPage = Math.Max(0, Math.Min(currentPage, checked(totalPages - 1)));
		}
		UpdateStatus();
	}

	private void ClearCurrentPageSectors()
	{
		if (configs != null)
		{
			configs.RemoveAll([SpecialName] (MenuItemConfig c) => c != null && Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) == 0 && c.Page == currentPage && c.Level != 2);
			RecalculatePages();
			if (totalPages > 0)
			{
				currentPage = Math.Max(0, Math.Min(currentPage, checked(totalPages - 1)));
			}
			else
			{
				currentPage = 0;
			}
			UpdateStatus();
		}
	}

	private void ClearCurrentPageAllSectors()
	{
		if (configs == null)
		{
			return;
		}
		configs.RemoveAll([SpecialName] (MenuItemConfig c) =>
		{
			if (c == null)
			{
				return false;
			}
			if (Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) != 0)
			{
				return false;
			}
			if (c.Level != 2)
			{
				return c.Page == currentPage;
			}
			string a = (c.Command ?? "").Trim();
			if (string.Equals(a, "_BACK", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.Equals(a, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return !string.Equals(a, "_EXIT", StringComparison.OrdinalIgnoreCase);
		});
		RecalculatePages();
		if (totalPages > 0)
		{
			currentPage = Math.Max(0, Math.Min(currentPage, checked(totalPages - 1)));
		}
		else
		{
			currentPage = 0;
		}
		UpdateStatus();
	}

	private void DeleteCurrentProcessScope()
	{
		string text = RadialMenuController.NormalizeScopeKey(currentProcessName);
		if (string.IsNullOrEmpty(text) || MessageBox.Show("确定删除此进程轮盘配置吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
		{
			return;
		}
		int num = ((cboProcess != null) ? cboProcess.SelectedIndex : 0);
		if (appConfig.ProcessMenus.RemoveAll([SpecialName] (ProcessMenuConfig m) => m != null && string.Equals(RadialMenuController.NormalizeScopeKey(m.ProcessName), text, StringComparison.OrdinalIgnoreCase)) <= 0)
		{
			return;
		}
		RadialMenuConfigStore.NormalizeConfig(appConfig);
		store.Save(appConfig);
		currentProcessName = "";
		RefreshScopeOptions();
		if (cboProcess == null || cboProcess.Items.Count == 0)
		{
			LoadScope("");
			return;
		}
		int num2 = num;
		if (num2 >= cboProcess.Items.Count)
		{
			num2 = checked(cboProcess.Items.Count - 1);
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		if (num2 == 0 && cboProcess.Items.Count > 1 && num > 0)
		{
			num2 = 1;
		}
		string processName = ((cboProcess.Items[num2] is ScopeItem scopeItem) ? scopeItem.ProcessName : "");
		LoadScope(processName);
		RestoreScopeItems();
		if (renderPanel != null)
		{
			renderPanel.Invalidate();
		}
	}

	private void ApplyRootFontSizeToAllScopes(int fontSize)
	{
		_Closure_0024__100_002D0 arg = default(_Closure_0024__100_002D0);
		_Closure_0024__100_002D0 CS_0024_003C_003E8__locals4 = new _Closure_0024__100_002D0(arg);
		CS_0024_003C_003E8__locals4._0024VB_0024Local_sz = Math.Max(6, Math.Min(40, fontSize));
		sizeConfig.FontSize = CS_0024_003C_003E8__locals4._0024VB_0024Local_sz;
		VB_0024AnonymousDelegate_0<List<MenuItemConfig>> vB_0024AnonymousDelegate_ = [SpecialName] (List<MenuItemConfig> list) =>
		{
			if (list != null)
			{
				foreach (MenuItemConfig item in list)
				{
					if (item != null)
					{
						item.FontSize = CS_0024_003C_003E8__locals4._0024VB_0024Local_sz;
					}
				}
			}
		};
		vB_0024AnonymousDelegate_(appConfig.GlobalMenuItems);
		if (appConfig.ProcessMenus != null)
		{
			foreach (ProcessMenuConfig processMenu in appConfig.ProcessMenus)
			{
				if (processMenu != null)
				{
					vB_0024AnonymousDelegate_(processMenu.MenuItems);
				}
			}
		}
		try
		{
			if (menuFont != null)
			{
				menuFont.Dispose();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		menuFont = new Font("Microsoft YaHei", CS_0024_003C_003E8__locals4._0024VB_0024Local_sz, FontStyle.Bold);
		try
		{
			if (renderPanel != null)
			{
				renderPanel.Invalidate();
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	private void RefreshScopeOptions()
	{
		List<ScopeItem> list = new List<ScopeItem>();
		list.Add(new ScopeItem
		{
			DisplayName = "全局",
			ProcessName = "",
			IsIgnored = false
		});
		foreach (ProcessMenuConfig item in appConfig.ProcessMenus.OrderBy([SpecialName] (ProcessMenuConfig m) => m.ProcessName, StringComparer.OrdinalIgnoreCase))
		{
			list.Add(new ScopeItem
			{
				DisplayName = item.ProcessName,
				ProcessName = item.ProcessName,
				IsIgnored = false
			});
		}
		scopeAllItems = list;
		suppressScopeChange = true;
		checked
		{
			try
			{
				cboProcess.BeginUpdate();
				cboProcess.Items.Clear();
				foreach (ScopeItem item2 in list)
				{
					cboProcess.Items.Add(item2);
				}
				cboProcess.EndUpdate();
				string b = (currentProcessName ?? "").Trim();
				int num = -1;
				int num2 = cboProcess.Items.Count - 1;
				for (int num3 = 0; num3 <= num2; num3++)
				{
					if (cboProcess.Items[num3] is ScopeItem scopeItem && string.Equals((scopeItem.ProcessName ?? "").Trim(), b, StringComparison.OrdinalIgnoreCase) && !scopeItem.IsIgnored)
					{
						num = num3;
						break;
					}
				}
				if (num < 0)
				{
					num = 0;
				}
				cboProcess.SelectedIndex = num;
			}
			finally
			{
				suppressScopeChange = false;
			}
		}
	}

	private void LoadScope(string processName)
	{
		string text = (processName ?? "").Trim();
		List<string> list = RadialMenuController.SplitProcessAliases(text);
		string text2 = RadialMenuController.NormalizeScopeKey(text);
		if (string.IsNullOrEmpty(text2))
		{
			currentProcessName = "";
			RefreshScopeOptions();
			configs = appConfig.GlobalMenuItems;
		}
		else
		{
			ProcessMenuConfig processMenuConfig = null;
			if (list != null && list.Count > 1)
			{
				processMenuConfig = appConfig.ProcessMenus.FirstOrDefault([SpecialName] (ProcessMenuConfig m) => m != null && string.Equals(RadialMenuController.NormalizeScopeKey(m.ProcessName), text2, StringComparison.OrdinalIgnoreCase));
				if (processMenuConfig == null)
				{
					processMenuConfig = new ProcessMenuConfig
					{
						ProcessName = text2,
						MenuItems = RadialMenuConfigStore.CloneMenuItems(RadialMenuConfigStore.GetDefaultSeedMenuItems())
					};
					RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
					appConfig.ProcessMenus.Add(processMenuConfig);
				}
				else
				{
					processMenuConfig.ProcessName = RadialMenuController.NormalizeScopeKey(processMenuConfig.ProcessName);
				}
			}
			else
			{
				string processName2 = RadialMenuController.NormalizeProcessName(text);
				processMenuConfig = appConfig.ProcessMenus.FirstOrDefault([SpecialName] (ProcessMenuConfig m) => m != null && RadialMenuController.ScopeKeyMatchesProcess(m.ProcessName, processName2));
				if (processMenuConfig == null)
				{
					processMenuConfig = new ProcessMenuConfig
					{
						ProcessName = processName2,
						MenuItems = RadialMenuConfigStore.CloneMenuItems(RadialMenuConfigStore.GetDefaultSeedMenuItems())
					};
					RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
					appConfig.ProcessMenus.Add(processMenuConfig);
				}
				else
				{
					processMenuConfig.ProcessName = RadialMenuController.NormalizeScopeKey(processMenuConfig.ProcessName);
				}
			}
			currentProcessName = processMenuConfig.ProcessName;
			RefreshScopeOptions();
			configs = processMenuConfig.MenuItems;
		}
		if (btnEditProcessAliases != null)
		{
			btnEditProcessAliases.Enabled = !string.IsNullOrEmpty(currentProcessName);
		}
		if (btnDeleteScope != null)
		{
			btnDeleteScope.Enabled = !string.IsNullOrEmpty(currentProcessName);
		}
		currentParentMenuName = "ROOT";
		currentPage = 0;
		RecalculatePages();
	}

	private void EditCurrentProcessAliases()
	{
		_Closure_0024__103_002D0 arg = default(_Closure_0024__103_002D0);
		_Closure_0024__103_002D0 CS_0024_003C_003E8__locals19 = new _Closure_0024__103_002D0(arg);
		CS_0024_003C_003E8__locals19._0024VB_0024Local_currentKey = RadialMenuController.NormalizeScopeKey(currentProcessName);
		if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals19._0024VB_0024Local_currentKey))
		{
			return;
		}
		CS_0024_003C_003E8__locals19._0024VB_0024Local_pm = appConfig.ProcessMenus.FirstOrDefault([SpecialName] (ProcessMenuConfig m) => m != null && string.Equals(RadialMenuController.NormalizeScopeKey(m.ProcessName), CS_0024_003C_003E8__locals19._0024VB_0024Local_currentKey, StringComparison.OrdinalIgnoreCase));
		if (CS_0024_003C_003E8__locals19._0024VB_0024Local_pm == null)
		{
			return;
		}
		List<string> list = RadialMenuController.SplitProcessAliases(CS_0024_003C_003E8__locals19._0024VB_0024Local_currentKey);
		string text = RadialMenuController.NormalizeScopeKey(Interaction.InputBox("请输入进程名或别名组（支持 | , ; 分隔）：", "编辑进程别名组", CS_0024_003C_003E8__locals19._0024VB_0024Local_pm.ProcessName));
		if (string.IsNullOrEmpty(text) || string.Equals(CS_0024_003C_003E8__locals19._0024VB_0024Local_currentKey, text, StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		CS_0024_003C_003E8__locals19._0024VB_0024Local_newTokens = RadialMenuController.SplitProcessAliases(text);
		if (CS_0024_003C_003E8__locals19._0024VB_0024Local_newTokens == null || CS_0024_003C_003E8__locals19._0024VB_0024Local_newTokens.Count == 0)
		{
			return;
		}
		List<ProcessMenuConfig> list2 = appConfig.ProcessMenus.Where([SpecialName] (ProcessMenuConfig m) => m != null && !object.ReferenceEquals(m, CS_0024_003C_003E8__locals19._0024VB_0024Local_pm)).Where([SpecialName] (ProcessMenuConfig m) =>
		{
			List<string> list4 = RadialMenuController.SplitProcessAliases(m.ProcessName);
			return list4 != null && list4.Count != 0 && list4.Any([SpecialName] (string x) =>
			{
				_Closure_0024__103_002D1 arg2 = default(_Closure_0024__103_002D1);
				_Closure_0024__103_002D1 CS_0024_003C_003E8__locals21 = new _Closure_0024__103_002D1(arg2);
				CS_0024_003C_003E8__locals21._0024VB_0024Local_x = x;
				return CS_0024_003C_003E8__locals19._0024VB_0024Local_newTokens.Any([SpecialName] (string y) => string.Equals(CS_0024_003C_003E8__locals21._0024VB_0024Local_x, y, StringComparison.OrdinalIgnoreCase));
			});
		}).ToList();
		foreach (ProcessMenuConfig item in list2)
		{
			appConfig.ProcessMenus.Remove(item);
		}
		CS_0024_003C_003E8__locals19._0024VB_0024Local_pm.ProcessName = text;
		if (list != null && list.Count > 0)
		{
			List<string> list3 = list.Where([SpecialName] (string t) =>
			{
				_Closure_0024__103_002D2 arg2 = default(_Closure_0024__103_002D2);
				_Closure_0024__103_002D2 CS_0024_003C_003E8__locals20 = new _Closure_0024__103_002D2(arg2);
				CS_0024_003C_003E8__locals20._0024VB_0024Local_t = t;
				return !CS_0024_003C_003E8__locals19._0024VB_0024Local_newTokens.Any([SpecialName] (string n) => string.Equals(n, CS_0024_003C_003E8__locals20._0024VB_0024Local_t, StringComparison.OrdinalIgnoreCase));
			}).ToList();
			_Closure_0024__103_002D3 closure_0024__103_002D = default(_Closure_0024__103_002D3);
			foreach (string item2 in list3)
			{
				closure_0024__103_002D = new _Closure_0024__103_002D3(closure_0024__103_002D);
				closure_0024__103_002D._0024VB_0024Local_key = RadialMenuController.NormalizeProcessName(item2);
				if (!string.IsNullOrEmpty(closure_0024__103_002D._0024VB_0024Local_key) && (appConfig.IgnoredProcesses == null || !appConfig.IgnoredProcesses.Any(closure_0024__103_002D._Lambda_0024__7)) && !appConfig.ProcessMenus.Any(closure_0024__103_002D._Lambda_0024__8) && !appConfig.ProcessMenus.Any(closure_0024__103_002D._Lambda_0024__9))
				{
					ProcessMenuConfig processMenuConfig = new ProcessMenuConfig
					{
						ProcessName = closure_0024__103_002D._0024VB_0024Local_key,
						MenuItems = new List<MenuItemConfig>()
					};
					RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
					appConfig.ProcessMenus.Add(processMenuConfig);
				}
			}
		}
		RadialMenuConfigStore.NormalizeConfig(appConfig);
		LoadScope(text);
		RestoreScopeItems();
		if (renderPanel != null)
		{
			renderPanel.Invalidate();
		}
	}

	private void OnScopeChanged(object sender, EventArgs e)
	{
		if (!suppressScopeChange && cboProcess.SelectedItem is ScopeItem scopeItem)
		{
			LoadScope(scopeItem.ProcessName);
		}
	}

	public void SwitchScope(string processName)
	{
		LoadScope(processName);
		RestoreScopeItems();
		if (renderPanel != null)
		{
			renderPanel.Invalidate();
		}
	}

	private void OnScopeTextUpdate(object sender, EventArgs e)
	{
		if (!suppressScopeChange && !scopeApplyingFilter)
		{
			RequestCursorRestore();
			ApplyScopeFilter(cboProcess.Text, commitIfSingleMatch: false);
		}
	}

	private void OnScopeKeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			RequestCursorRestore();
			ApplyScopeFilter(cboProcess.Text, commitIfSingleMatch: true);
		}
		else if (e.KeyCode == Keys.Escape)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			RestoreScopeItems();
		}
		else if (e.KeyCode == Keys.Down && cboProcess != null && !cboProcess.DroppedDown)
		{
			cboProcess.DroppedDown = true;
		}
	}

	private void RestoreScopeItems()
	{
		if (scopeAllItems == null || scopeAllItems.Count == 0)
		{
			return;
		}
		suppressScopeChange = true;
		scopeApplyingFilter = true;
		checked
		{
			try
			{
				cboProcess.BeginUpdate();
				cboProcess.Items.Clear();
				foreach (ScopeItem scopeAllItem in scopeAllItems)
				{
					cboProcess.Items.Add(scopeAllItem);
				}
				cboProcess.EndUpdate();
				string b = RadialMenuController.NormalizeProcessName(currentProcessName);
				int num = -1;
				int num2 = cboProcess.Items.Count - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (cboProcess.Items[i] is ScopeItem scopeItem && string.Equals(RadialMenuController.NormalizeProcessName(scopeItem.ProcessName), b, StringComparison.OrdinalIgnoreCase) && !scopeItem.IsIgnored)
					{
						num = i;
						break;
					}
				}
				if (num < 0)
				{
					num = 0;
				}
				cboProcess.SelectedIndex = num;
			}
			finally
			{
				scopeApplyingFilter = false;
				suppressScopeChange = false;
			}
		}
	}

	private void ApplyScopeFilter(string filterText, bool commitIfSingleMatch)
	{
		_Closure_0024__109_002D0 arg = default(_Closure_0024__109_002D0);
		_Closure_0024__109_002D0 CS_0024_003C_003E8__locals3 = new _Closure_0024__109_002D0(arg);
		if (scopeAllItems == null || scopeAllItems.Count == 0)
		{
			return;
		}
		cboProcess.Cursor = Cursors.IBeam;
		CS_0024_003C_003E8__locals3._0024VB_0024Local_q = NormalizeSearchKey(filterText);
		if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals3._0024VB_0024Local_q))
		{
			RestoreScopeItems();
			return;
		}
		List<ScopeItem> list = scopeAllItems.Where([SpecialName] (ScopeItem it) => ScopeMatchesQuery(it, CS_0024_003C_003E8__locals3._0024VB_0024Local_q)).ToList();
		if (commitIfSingleMatch && list.Count == 1)
		{
			suppressScopeChange = true;
			scopeApplyingFilter = true;
			try
			{
				cboProcess.BeginUpdate();
				cboProcess.Items.Clear();
				cboProcess.Items.Add(list[0]);
				cboProcess.EndUpdate();
				cboProcess.SelectedIndex = 0;
				cboProcess.DroppedDown = false;
				return;
			}
			finally
			{
				scopeApplyingFilter = false;
				suppressScopeChange = false;
			}
		}
		int selectionStart = cboProcess.SelectionStart;
		string text = cboProcess.Text;
		suppressScopeChange = true;
		scopeApplyingFilter = true;
		try
		{
			cboProcess.BeginUpdate();
			cboProcess.Items.Clear();
			foreach (ScopeItem item in list)
			{
				cboProcess.Items.Add(item);
			}
			cboProcess.EndUpdate();
		}
		finally
		{
			scopeApplyingFilter = false;
			suppressScopeChange = false;
		}
		cboProcess.Text = text;
		cboProcess.SelectionStart = Math.Min(selectionStart, cboProcess.Text.Length);
		cboProcess.SelectionLength = 0;
		if (list.Count > 0)
		{
			cboProcess.DroppedDown = true;
		}
	}

	private static bool ScopeMatchesQuery(ScopeItem item, string q)
	{
		if (item == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(q))
		{
			return true;
		}
		string obj = NormalizeSearchKey(item.DisplayName);
		string text = NormalizeSearchKey(item.ProcessName);
		if (obj.Contains(q) || text.Contains(q))
		{
			return true;
		}
		string text2 = NormalizeSearchKey(GetLatinInitials(item.DisplayName + " " + item.ProcessName));
		if (!string.IsNullOrEmpty(text2) && text2.Contains(q))
		{
			return true;
		}
		string text3 = NormalizeSearchKey(GetChineseInitials(item.DisplayName + " " + item.ProcessName));
		if (!string.IsNullOrEmpty(text3) && text3.Contains(q))
		{
			return true;
		}
		string value = NormalizeSearchKey(PinyinQueryToInitials(q));
		if (!string.IsNullOrEmpty(value) && text3.Contains(value))
		{
			return true;
		}
		return false;
	}

	private static string NormalizeSearchKey(string s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string text = s ?? "";
		foreach (char c in text)
		{
			if (char.IsLetterOrDigit(c))
			{
				stringBuilder.Append(char.ToLowerInvariant(c));
			}
		}
		return stringBuilder.ToString();
	}

	private static string GetLatinInitials(string s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = false;
		bool flag2 = false;
		string text = s ?? "";
		foreach (char c in text)
		{
			if (char.IsLetterOrDigit(c))
			{
				bool num = !flag;
				bool flag3 = char.IsLetter(c) && char.IsUpper(c) && flag2;
				if (num || flag3)
				{
					stringBuilder.Append(char.ToLowerInvariant(c));
				}
				flag = true;
				flag2 = char.IsLetter(c) && char.IsLower(c);
			}
			else
			{
				flag = false;
				flag2 = false;
			}
		}
		return stringBuilder.ToString();
	}

	private static string GetChineseInitials(string s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string text = s ?? "";
		foreach (char c in text)
		{
			if (!char.IsLetterOrDigit(c))
			{
				continue;
			}
			if (c >= '\u0080')
			{
				char gb2312Initial = GetGb2312Initial(c);
				if (gb2312Initial != 0)
				{
					stringBuilder.Append(gb2312Initial);
				}
				else
				{
					stringBuilder.Append(char.ToLowerInvariant(c));
				}
			}
			else
			{
				stringBuilder.Append(char.ToLowerInvariant(c));
			}
		}
		return stringBuilder.ToString();
	}

	private static char GetGb2312Initial(char ch)
	{
		byte[] bytes;
		char result;
		try
		{
			bytes = Encoding.GetEncoding("GB2312").GetBytes(ch.ToString());
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = '\0';
			ProjectData.ClearProjectError();
			goto IL_008e;
		}
		checked
		{
			if (bytes == null || bytes.Length < 2)
			{
				result = '\0';
			}
			else
			{
				int num = bytes[0] * 256 + bytes[1];
				int[] array = new int[23]
				{
					45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 49062,
					49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698,
					52980, 53689, 54481
				};
				char[] array2 = new char[23]
				{
					'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k',
					'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w',
					'x', 'y', 'z'
				};
				int num2 = array.Length - 1;
				while (true)
				{
					if (num2 >= 0)
					{
						if (num >= array[num2])
						{
							result = array2[num2];
							break;
						}
						num2 += -1;
						continue;
					}
					result = '\0';
					break;
				}
			}
			goto IL_008e;
		}
		IL_008e:
		return result;
	}

	private static string PinyinQueryToInitials(string q)
	{
		string text = NormalizeSearchKey(q);
		if (string.IsNullOrEmpty(text))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder();
		string text2 = text;
		foreach (char c in text2)
		{
			if (c >= 'a' && c <= 'z')
			{
				stringBuilder.Append(c);
			}
		}
		string text3 = stringBuilder.ToString();
		if (text3.Length == 0)
		{
			return "";
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		char c2 = '\0';
		checked
		{
			int num = text3.Length - 1;
			for (int j = 0; j <= num; j++)
			{
				char c3 = text3[j];
				char ch = ((j + 1 < text3.Length) ? text3[j + 1] : '\0');
				if (stringBuilder2.Length == 0)
				{
					stringBuilder2.Append(c3);
				}
				else if (IsVowel(c2) && !IsVowel(c3) && IsVowel(ch))
				{
					stringBuilder2.Append(c3);
				}
				else if (!IsVowel(c2) && !IsVowel(c3) && IsVowel(ch) && (c3 == c2 || c2 == 'n' || c2 == 'g' || c2 == 'r'))
				{
					stringBuilder2.Append(c3);
				}
				c2 = c3;
			}
			return stringBuilder2.ToString();
		}
	}

	private static bool IsVowel(char ch)
	{
		switch (ch)
		{
		case 'a':
		case 'e':
		case 'i':
		case 'o':
		case 'u':
		case 'v':
			return true;
		default:
			return false;
		}
	}

	private void AddIgnoredProcess(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("请输入进程名（例如 notepad / acad / chrome）：", "添加进程");
		text = (text ?? "").Trim();
		if (!string.IsNullOrEmpty(text))
		{
			appConfig.IgnoredProcesses = appConfig.IgnoredProcesses.Where([SpecialName] (string s) => !string.Equals(s, text, StringComparison.OrdinalIgnoreCase)).ToList();
			ProcessMenuConfig processMenuConfig = appConfig.ProcessMenus.FirstOrDefault([SpecialName] (ProcessMenuConfig m) => string.Equals(m.ProcessName, text, StringComparison.OrdinalIgnoreCase));
			if (processMenuConfig == null)
			{
				processMenuConfig = new ProcessMenuConfig
				{
					ProcessName = text,
					MenuItems = RadialMenuConfigStore.CloneMenuItems(appConfig.GlobalMenuItems)
				};
				RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
				appConfig.ProcessMenus.Add(processMenuConfig);
			}
			LoadScope(text);
		}
	}

	private List<string> ShowProcessSelectionDialog()
	{
		List<string> list = new List<string>();
		using (Form form = new Form())
		{
			form.Text = "选择运行中的进程";
			form.Size = new Size(350, 450);
			form.StartPosition = FormStartPosition.CenterParent;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.MaximizeBox = false;
			form.MinimizeBox = false;
			CheckedListBox checkedListBox = new CheckedListBox
			{
				Dock = DockStyle.Top,
				Height = 350,
				CheckOnClick = true
			};
			Button button = new Button
			{
				Text = "确定",
				Location = new Point(160, 370),
				Width = 80,
				Height = 30,
				DialogResult = DialogResult.OK
			};
			Button button2 = new Button
			{
				Text = "取消",
				Location = new Point(250, 370),
				Width = 80,
				Height = 30,
				DialogResult = DialogResult.Cancel
			};
			try
			{
				List<string> list2 = (from s in (from p in Process.GetProcesses()
						where p.MainWindowHandle != IntPtr.Zero
						select p.ProcessName).Distinct()
					orderby s
					select s).ToList();
				foreach (string item in list2)
				{
					checkedListBox.Items.Add(item);
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			form.Controls.AddRange(new Control[3] { checkedListBox, button, button2 });
			form.AcceptButton = button;
			if (form.ShowDialog() == DialogResult.OK)
			{
				foreach (object checkedItem in checkedListBox.CheckedItems)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(checkedItem);
					list.Add(objectValue.ToString());
				}
			}
		}
		return list;
	}

	private void AddNewProcess(object sender, EventArgs e)
	{
		using Form form = new Form();
		_Closure_0024__119_002D0 arg = default(_Closure_0024__119_002D0);
		_Closure_0024__119_002D0 CS_0024_003C_003E8__locals5 = new _Closure_0024__119_002D0(arg);
		CS_0024_003C_003E8__locals5._0024VB_0024Me = this;
		form.Text = "添加进程";
		form.Size = new Size(400, 180);
		form.StartPosition = FormStartPosition.CenterParent;
		form.FormBorderStyle = FormBorderStyle.FixedDialog;
		form.MaximizeBox = false;
		form.MinimizeBox = false;
		Label label = new Label
		{
			Text = "请输入或选择进程名(支持多选，分号隔开):",
			Location = new Point(20, 20),
			AutoSize = true
		};
		CS_0024_003C_003E8__locals5._0024VB_0024Local_txt = new TextBox
		{
			Location = new Point(20, 45),
			Width = 260
		};
		Button button = new Button
		{
			Text = "选择...",
			Location = new Point(290, 43),
			Width = 70,
			Height = 25
		};
		Button button2 = new Button
		{
			Text = "确定",
			Location = new Point(200, 90),
			Width = 80,
			Height = 30,
			DialogResult = DialogResult.OK
		};
		Button button3 = new Button
		{
			Text = "取消",
			Location = new Point(290, 90),
			Width = 80,
			Height = 30,
			DialogResult = DialogResult.Cancel
		};
		button.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			CS_0024_003C_003E8__locals5._Lambda_0024__0();
		};
		form.Controls.AddRange(new Control[5] { label, CS_0024_003C_003E8__locals5._0024VB_0024Local_txt, button, button2, button3 });
		form.AcceptButton = button2;
		form.CancelButton = button3;
		if (form.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string text = CS_0024_003C_003E8__locals5._0024VB_0024Local_txt.Text.Trim();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string[] array = text.Split(new char[2] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		_Closure_0024__119_002D1 closure_0024__119_002D = default(_Closure_0024__119_002D1);
		foreach (string text2 in array2)
		{
			closure_0024__119_002D = new _Closure_0024__119_002D1(closure_0024__119_002D);
			closure_0024__119_002D._0024VB_0024Local_procName = text2.Trim();
			if (!string.IsNullOrEmpty(closure_0024__119_002D._0024VB_0024Local_procName))
			{
				ProcessMenuConfig processMenuConfig = appConfig.ProcessMenus.FirstOrDefault(closure_0024__119_002D._Lambda_0024__1);
				if (processMenuConfig == null)
				{
					processMenuConfig = new ProcessMenuConfig
					{
						ProcessName = closure_0024__119_002D._0024VB_0024Local_procName,
						MenuItems = RadialMenuConfigStore.CloneMenuItems(appConfig.GlobalMenuItems)
					};
					RadialMenuConfigStore.NormalizeMenuItems(processMenuConfig.MenuItems);
					appConfig.ProcessMenus.Add(processMenuConfig);
				}
			}
		}
		if (array.Length > 0)
		{
			LoadScope(array.Last().Trim());
		}
	}

	private void ManageIgnoredProcesses(object sender, EventArgs e)
	{
		_Closure_0024__120_002D1 arg = default(_Closure_0024__120_002D1);
		_Closure_0024__120_002D1 CS_0024_003C_003E8__locals20 = new _Closure_0024__120_002D1(arg);
		CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg = new Form();
		try
		{
			_Closure_0024__120_002D0 arg2 = default(_Closure_0024__120_002D0);
			_Closure_0024__120_002D0 CS_0024_003C_003E8__locals19 = new _Closure_0024__120_002D0(arg2);
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.Text = "管理忽略进程";
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.Size = new Size(400, 300);
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.StartPosition = FormStartPosition.CenterParent;
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.ShowInTaskbar = false;
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.MinimizeBox = false;
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.MaximizeBox = false;
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
			CS_0024_003C_003E8__locals19._0024VB_0024Local_lst = new ListBox
			{
				Dock = DockStyle.Top,
				Height = 200,
				IntegralHeight = false
			};
			CS_0024_003C_003E8__locals19._0024VB_0024Local_lst.Items.AddRange(appConfig.IgnoredProcesses.ToArray());
			Button button = new Button
			{
				Text = "添加",
				Location = new Point(20, 220),
				Size = new Size(80, 30),
				BackColor = Color.White
			};
			Button button2 = new Button
			{
				Text = "移除",
				Location = new Point(110, 220),
				Size = new Size(80, 30),
				BackColor = Color.White
			};
			Button button3 = new Button
			{
				Text = "确定",
				Location = new Point(290, 220),
				Size = new Size(80, 30),
				BackColor = Color.White
			};
			button.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals19._Lambda_0024__0();
			};
			button2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals19._Lambda_0024__1();
			};
			button3.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals20._Lambda_0024__2();
			};
			CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.Controls.AddRange(new Control[4] { CS_0024_003C_003E8__locals19._0024VB_0024Local_lst, button, button2, button3 });
			if (CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			appConfig.IgnoredProcesses.Clear();
			foreach (object item in CS_0024_003C_003E8__locals19._0024VB_0024Local_lst.Items)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				appConfig.IgnoredProcesses.Add(objectValue.ToString());
			}
		}
		finally
		{
			if (CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg != null)
			{
				((IDisposable)CS_0024_003C_003E8__locals20._0024VB_0024Local_dlg).Dispose();
			}
		}
	}

	private void RecalculatePages()
	{
		if (Operators.CompareString(currentParentMenuName, "ROOT", TextCompare: false) != 0)
		{
			EnsureBackItemExists(currentParentMenuName);
		}
		List<MenuItemConfig> list = configs.Where([SpecialName] (MenuItemConfig c) => Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) == 0).ToList();
		if (string.IsNullOrEmpty(currentProcessName))
		{
			totalPages = 1;
			currentPage = 0;
			foreach (MenuItemConfig item in list)
			{
				if (item != null && item.Level != 2)
				{
					item.Page = 0;
				}
			}
		}
		else if (list.Count > 0)
		{
			int num = list.Max([SpecialName] (MenuItemConfig c) => c.Page);
			totalPages = checked(num + 1);
		}
		else
		{
			totalPages = 1;
		}
		UpdateStatus();
	}

	private void UpdateStatus()
	{
		string text = (string.IsNullOrEmpty(currentProcessName) ? "全局" : currentProcessName);
		string text2 = ((Operators.CompareString(currentParentMenuName, "ROOT", TextCompare: false) == 0) ? "主轮盘" : currentParentMenuName);
		lblStatus.Text = $"编辑对象: {text} | 当前菜单: {text2} | 第 {checked(currentPage + 1)} 页 (共 {totalPages} 页)";
		btnBack.Enabled = Operators.CompareString(currentParentMenuName, "ROOT", TextCompare: false) != 0;
		renderPanel.Invalidate();
	}

	private void EnsurePreviewSkinLoaded()
	{
		string text = (sizeConfig.DragAreaBackgroundImage ?? "").Trim();
		if (string.Equals(text, previewSkinImagePath ?? "", StringComparison.OrdinalIgnoreCase) && previewSkinImage != null)
		{
			return;
		}
		try
		{
			if (previewSkinImage != null)
			{
				previewSkinImage.Dispose();
				previewSkinImage = null;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (previewSkinImageStream != null)
			{
				previewSkinImageStream.Dispose();
				previewSkinImageStream = null;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		previewSkinImagePath = text;
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		string text2 = RadialMenuController.ResolveSkinSpecToLocalFile(text);
		if (string.IsNullOrWhiteSpace(text2) || !File.Exists(text2))
		{
			return;
		}
		try
		{
			byte[] buffer = File.ReadAllBytes(text2);
			previewSkinImage = Image.FromStream(previewSkinImageStream = new MemoryStream(buffer));
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			try
			{
				if (previewSkinImage != null)
				{
					previewSkinImage.Dispose();
				}
			}
			catch (Exception projectError4)
			{
				ProjectData.SetProjectError(projectError4);
				ProjectData.ClearProjectError();
			}
			previewSkinImage = null;
			try
			{
				if (previewSkinImageStream != null)
				{
					previewSkinImageStream.Dispose();
				}
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				ProjectData.ClearProjectError();
			}
			previewSkinImageStream = null;
			ProjectData.ClearProjectError();
		}
	}

	private static void DrawImageCover(Graphics g, Image img, RectangleF dest, double opacity = 1.0)
	{
		if (img == null)
		{
			return;
		}
		float num = img.Width;
		float num2 = img.Height;
		if (num <= 0f || num2 <= 0f || dest.Width <= 0f || dest.Height <= 0f)
		{
			return;
		}
		float num3 = Math.Max(dest.Width / num, dest.Height / num2);
		float num4 = num * num3;
		float num5 = num2 * num3;
		float num6 = dest.X + (dest.Width - num4) / 2f;
		float num7 = dest.Y + (dest.Height - num5) / 2f;
		double num8 = opacity;
		if (double.IsNaN(num8) || double.IsInfinity(num8))
		{
			num8 = 1.0;
		}
		num8 = Math.Max(0.0, Math.Min(1.0, num8));
		if (num8 >= 0.999)
		{
			g.DrawImage(img, new RectangleF(num6, num7, num4, num5));
			return;
		}
		using ImageAttributes imageAttributes = new ImageAttributes();
		ColorMatrix colorMatrix = new ColorMatrix();
		colorMatrix.Matrix33 = (float)num8;
		imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
		Rectangle destRect = Rectangle.Round(new RectangleF(num6, num7, num4, num5));
		g.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttributes);
	}

	private void renderPanel_Paint(object sender, PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		graphics.Clear(renderPanel.BackColor);
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Size clientSize = renderPanel.ClientSize;
		Point center = new Point(clientSize.Width / 2, clientSize.Height / 2);
		int num = Math.Min(clientSize.Width, clientSize.Height);
		checked
		{
			int num2 = sizeConfig.OuterRadius * 2 + 24;
			double num3 = (double)num / (double)num2 * 0.995;
			int num4 = (int)Math.Round((double)sizeConfig.InnerRadius * num3 * 0.75);
			int num5 = (int)Math.Round((double)sizeConfig.OuterRadius * num3);
			EnsurePreviewSkinLoaded();
			bool flag = previewSkinImage != null;
			if (flag)
			{
				using GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddEllipse((float)(center.X - num5), (float)(center.Y - num5), (float)(num5 * 2), (float)(num5 * 2));
				GraphicsState gstate = graphics.Save();
				graphics.SetClip(graphicsPath, CombineMode.Replace);
				double num6 = 1.0;
				try
				{
					num6 = sizeConfig.SkinOpacity;
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					num6 = 1.0;
					ProjectData.ClearProjectError();
				}
				num6 = Math.Max(0.0, Math.Min(1.0, num6));
				DrawImageCover(graphics, previewSkinImage, new RectangleF(center.X - num5, center.Y - num5, num5 * 2, num5 * 2), num6);
				graphics.Restore(gstate);
			}
			if (!flag)
			{
				graphics.FillEllipse(Brushes.White, center.X - num4, center.Y - num4, num4 * 2, num4 * 2);
			}
			graphics.DrawEllipse(Pens.Gray, center.X - num4, center.Y - num4, num4 * 2, num4 * 2);
			string s;
			float emSize;
			if (string.IsNullOrEmpty(currentProcessName))
			{
				s = "全局";
				emSize = 14f;
			}
			else
			{
				s = $"{currentPage + 1}/{Math.Max(1, totalPages)}";
				emSize = 16f;
			}
			using (Font font = new Font("Microsoft YaHei", emSize, FontStyle.Bold))
			{
				SizeF sizeF = graphics.MeasureString(s, font);
				graphics.DrawString(s, font, Brushes.Black, (float)center.X - sizeF.Width / 2f, (float)center.Y - sizeF.Height / 2f);
			}
			float num7 = 0f;
			try
			{
				int num8 = 3;
				double num9 = (double)(num5 - num4) / (double)num8;
				double num10 = (double)num4 + 2.0 * num9;
				double num11 = (double)num4 + 3.0 * num9;
				float num12 = (float)(num11 - num10);
				float num13 = (float)((num10 + num11) / 2.0);
				int num14 = 8;
				float num15 = 360f / (float)num14;
				float num16 = num13 * (float)((double)num15 * Math.PI / 180.0);
				float val = Math.Max(42f, num16 * 0.88f);
				float val2 = Math.Max(26f, num12 * 0.98f);
				num7 = Math.Min(val, val2) * 0.92f;
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				num7 = 0f;
				ProjectData.ClearProjectError();
			}
			DrawRing(graphics, center, num4, num5, 0, num7);
			DrawRing(graphics, center, num4, num5, 1, num7);
			DrawRing(graphics, center, num4, num5, 2, num7);
			if (isDragging && dragItem != null)
			{
				int num17 = 48;
				Rectangle rectangle = new Rectangle(dragCurrentPos.X - unchecked(num17 / 2), dragCurrentPos.Y - unchecked(num17 / 2), num17, num17);
				string text = ((!string.IsNullOrWhiteSpace(dragItem.DisplayName)) ? dragItem.DisplayName : dragItem.Text);
				string s2 = (dragItem.OnlyShowIcon ? "" : text);
				Image iconForMenuItem = GetIconForMenuItem(dragItem, text, num17);
				if (iconForMenuItem != null)
				{
					graphics.DrawImage(iconForMenuItem, rectangle);
					return;
				}
				graphics.FillEllipse(new SolidBrush(Color.FromArgb(200, Color.White)), rectangle);
				graphics.DrawEllipse(Pens.Gray, rectangle);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};
				graphics.DrawString(s2, new Font("Microsoft YaHei", 9f), Brushes.Black, rectangle, format);
			}
		}
	}

	private void DrawRing(Graphics g, Point center, int innerR, int outerR, int level, float outerRingMaxIconSize)
	{
		int num = 3;
		checked
		{
			double num2 = (double)(outerR - innerR) / (double)num;
			double num3 = (double)innerR + (double)level * num2;
			double num4 = (double)innerR + (double)(level + 1) * num2;
			List<MenuItemConfig> itemsForLevel = GetItemsForLevel(level);
			int num5 = ((level == 1) ? 16 : 8);
			float num6 = 360f / (float)num5;
			bool flag = !sizeConfig.ShowDividers.HasValue || sizeConfig.ShowDividers.Value;
			bool flag2 = !sizeConfig.ShowInnerDividers.HasValue || sizeConfig.ShowInnerDividers.Value;
			bool flag3 = previewSkinImage != null;
			float num7 = (flag3 ? 1.05f : 1f);
			int num8 = 200;
			try
			{
				num8 = Math.Min(255, Math.Max(0, (int)Math.Round(200.0 * sizeConfig.FallbackOpacity)));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				num8 = 200;
				ProjectData.ClearProjectError();
			}
			Color color = (flag3 ? Color.FromArgb(num8, Color.Gray) : Color.LightGray);
			using Pen pen = new Pen(color, num7);
			if (flag)
			{
				g.DrawEllipse(pen, (float)((double)center.X - num3), (float)((double)center.Y - num3), (float)(num3 * 2.0), (float)(num3 * 2.0));
				g.DrawEllipse(pen, (float)((double)center.X - num4), (float)((double)center.Y - num4), (float)(num4 * 2.0), (float)(num4 * 2.0));
				if (flag2)
				{
					int num9 = num5 - 1;
					for (int i = 0; i <= num9; i++)
					{
						double num10 = (double)((float)i * num6 + 22.5f) * Math.PI / 180.0;
						g.DrawLine(pen, (float)((double)center.X + num3 * Math.Cos(num10)), (float)((double)center.Y + num3 * Math.Sin(num10)), (float)((double)center.X + num4 * Math.Cos(num10)), (float)((double)center.Y + num4 * Math.Sin(num10)));
					}
				}
			}
			int num11 = num5 - 1;
			_Closure_0024__126_002D0 closure_0024__126_002D = default(_Closure_0024__126_002D0);
			for (int j = 0; j <= num11; j++)
			{
				closure_0024__126_002D = new _Closure_0024__126_002D0(closure_0024__126_002D);
				closure_0024__126_002D._0024VB_0024Local_sectorIndex = j;
				MenuItemConfig menuItemConfig = itemsForLevel.FirstOrDefault(closure_0024__126_002D._Lambda_0024__0);
				float num12 = (float)j * num6 + 22.5f;
				using GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddArc((float)((double)center.X - num4), (float)((double)center.Y - num4), (float)(num4 * 2.0), (float)(num4 * 2.0), num12, num6);
				graphicsPath.AddArc((float)((double)center.X - num3), (float)((double)center.Y - num3), (float)(num3 * 2.0), (float)(num3 * 2.0), num12 + num6, 0f - num6);
				graphicsPath.CloseFigure();
				Color color2 = Color.Transparent;
				Color color3 = ((!sizeConfig.OuterRingGray.HasValue || sizeConfig.OuterRingGray.Value) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White);
				if (menuItemConfig != null)
				{
					if (menuItemConfig == dragItem)
					{
						color2 = Color.LightYellow;
					}
					else if (string.Equals(menuItemConfig.Command, "_BACK", StringComparison.OrdinalIgnoreCase))
					{
						color2 = Color.DarkGray;
					}
					else if (string.Equals(menuItemConfig.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
					{
						color2 = ColorTranslator.FromHtml("#5b87ff");
					}
					else if (string.Equals(menuItemConfig.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
					{
						color2 = ColorTranslator.FromHtml("#666666");
					}
					else if (level == 2)
					{
						color2 = color3;
					}
					else
					{
						color2 = Color.White;
						if (menuItemConfig.IsSubMenu)
						{
							color2 = Color.FromArgb(240, 240, 255);
						}
					}
				}
				else if (level == 2)
				{
					color2 = color3;
				}
				if ((isDragging || isExternalDragging) && hoverSectorIndex == j && hoverSectorLevel == level)
				{
					color2 = Color.LightGreen;
				}
				if (previewSkinImage != null && color2 != Color.Transparent)
				{
					string a = "";
					try
					{
						if (menuItemConfig != null)
						{
							a = menuItemConfig.Command ?? "";
						}
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						a = "";
						ProjectData.ClearProjectError();
					}
					bool flag4 = false;
					if (menuItemConfig != null)
					{
						if (string.Equals(a, "_BACK", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "_EXIT", StringComparison.OrdinalIgnoreCase))
						{
							flag4 = true;
						}
						if (menuItemConfig == dragItem)
						{
							flag4 = true;
						}
					}
					if (!flag4 && (color2.ToArgb() == Color.White.ToArgb() || color2.ToArgb() == color3.ToArgb() || color2.ToArgb() == Color.FromArgb(240, 240, 255).ToArgb()))
					{
						color2 = Color.Transparent;
					}
				}
				if (color2 != Color.Transparent)
				{
					using SolidBrush brush = new SolidBrush(color2);
					g.FillPath(brush, graphicsPath);
				}
				if (flag && flag2)
				{
					g.DrawPath(pen, graphicsPath);
				}
				if (menuItemConfig == null)
				{
					continue;
				}
				double num13 = (double)(num12 + num6 / 2f) * Math.PI / 180.0;
				double num14 = (num3 + num4) / 2.0;
				double num15 = (double)center.X + num14 * Math.Cos(num13);
				double num16 = (double)center.Y + num14 * Math.Sin(num13);
				bool onlyShowIcon = menuItemConfig.OnlyShowIcon;
				string text = ((!string.IsNullOrWhiteSpace(menuItemConfig.DisplayName)) ? menuItemConfig.DisplayName : menuItemConfig.Text);
				string text2 = (onlyShowIcon ? "" : text);
				if (menuItemConfig.IsSubMenu && !onlyShowIcon)
				{
					text2 += " >";
				}
				Brush brush2 = Brushes.Black;
				if (string.Equals(menuItemConfig.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(menuItemConfig.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
				{
					brush2 = Brushes.White;
				}
				float num17 = (float)(num4 - num3);
				float num18 = (float)num14 * (float)((double)num6 * Math.PI / 180.0);
				float num19 = Math.Max(42f, num18 * 0.88f);
				float num20 = Math.Max(26f, num17 * 0.98f);
				RectangleF rectangleF = new RectangleF((float)(num15 - (double)(num19 / 2f)), (float)(num16 - (double)(num20 / 2f)), num19, num20);
				StringFormat format = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Near,
					Trimming = StringTrimming.None
				};
				Region region = null;
				try
				{
					region = g.Clip.Clone();
					g.SetClip(graphicsPath, CombineMode.Intersect);
					float num21 = 1f;
					Font font = menuFont;
					bool flag5 = level == 2;
					float maxWidth = Math.Max(10f, rectangleF.Width * (flag5 ? 0.52f : 0.92f));
					float num22 = 24f;
					if (onlyShowIcon)
					{
						float num23 = Math.Min(rectangleF.Width, rectangleF.Height) * 0.92f;
						if (num23 > 0f)
						{
							float num24 = 0.53f;
							float num25 = 0.8f + 0.1f * (float)level;
							float val = ((outerRingMaxIconSize > 0f) ? outerRingMaxIconSize : num23) * num25 * num24;
							num22 = Math.Max(8f, Math.Min(val, num23));
						}
					}
					int num26 = Math.Max(8, (int)Math.Round(num22));
					float num27 = num26;
					Image iconForMenuItem = GetIconForMenuItem(menuItemConfig, text, num26);
					if (iconForMenuItem != null)
					{
						float num28 = (onlyShowIcon ? 0f : (rectangleF.Height - num27 - num21));
						Font usedFont = null;
						float lineHeight = 0f;
						List<string> list = new List<string>();
						if (!onlyShowIcon && num28 >= 8f)
						{
							list = TextLayoutUtil.FitLines(g, text2, font, maxWidth, num28, ref usedFont, ref lineHeight);
						}
						float num29 = ((list == null) ? 0f : ((float)list.Count * lineHeight));
						float num30 = num27 + ((list != null && list.Count > 0) ? (num21 + num29) : 0f);
						float num31 = rectangleF.Top + (rectangleF.Height - num30) / 2f;
						float num32 = (float)Math.Round(num15 - (double)(num27 / 2f));
						float num33 = (float)Math.Round(num31);
						RectangleF rect = new RectangleF(num32, num33, num27, num27);
						try
						{
							g.DrawImage(iconForMenuItem, rect);
						}
						catch (Exception projectError3)
						{
							ProjectData.SetProjectError(projectError3);
							ProjectData.ClearProjectError();
						}
						if (list != null && list.Count > 0)
						{
							float num34 = rect.Bottom + num21;
							foreach (string item in list)
							{
								RectangleF layoutRectangle = new RectangleF(rectangleF.Left, num34, rectangleF.Width, lineHeight);
								g.DrawString(item, usedFont, brush2, layoutRectangle, format);
								num34 += lineHeight;
							}
						}
						if (usedFont != null && !object.ReferenceEquals(usedFont, font))
						{
							try
							{
								usedFont.Dispose();
							}
							catch (Exception projectError4)
							{
								ProjectData.SetProjectError(projectError4);
								ProjectData.ClearProjectError();
							}
						}
					}
					else if (!onlyShowIcon)
					{
						Font usedFont2 = null;
						float lineHeight2 = 0f;
						List<string> list2 = TextLayoutUtil.FitLines(g, text2, font, maxWidth, rectangleF.Height, ref usedFont2, ref lineHeight2);
						float num35 = (float)list2.Count * lineHeight2;
						float num36 = rectangleF.Top + (rectangleF.Height - num35) / 2f;
						foreach (string item2 in list2)
						{
							RectangleF layoutRectangle2 = new RectangleF(rectangleF.Left, num36, rectangleF.Width, lineHeight2);
							g.DrawString(item2, usedFont2, brush2, layoutRectangle2, format);
							num36 += lineHeight2;
						}
						if (usedFont2 != null && !object.ReferenceEquals(usedFont2, font))
						{
							try
							{
								usedFont2.Dispose();
							}
							catch (Exception projectError5)
							{
								ProjectData.SetProjectError(projectError5);
								ProjectData.ClearProjectError();
							}
						}
					}
				}
				finally
				{
					if (region != null)
					{
						g.SetClip(region, CombineMode.Replace);
						region.Dispose();
					}
					else
					{
						g.ResetClip();
					}
				}
				if (menuItemConfig.Enabled || string.Equals(menuItemConfig.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(menuItemConfig.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}
				Region region2 = null;
				try
				{
					region2 = g.Clip.Clone();
					g.SetClip(graphicsPath, CombineMode.Intersect);
					float num37 = num12 + num6 * 0.15f;
					float num38 = num12 + num6 * 0.85f;
					double num39 = (double)num37 * Math.PI / 180.0;
					double num40 = (double)num38 * Math.PI / 180.0;
					float num41 = (float)(num3 + 4.0);
					float num42 = (float)(num4 - 4.0);
					PointF pt = new PointF((float)((double)center.X + (double)num41 * Math.Cos(num39)), (float)((double)center.Y + (double)num41 * Math.Sin(num39)));
					PointF pt2 = new PointF((float)((double)center.X + (double)num42 * Math.Cos(num40)), (float)((double)center.Y + (double)num42 * Math.Sin(num40)));
					PointF pt3 = new PointF((float)((double)center.X + (double)num41 * Math.Cos(num40)), (float)((double)center.Y + (double)num41 * Math.Sin(num40)));
					PointF pt4 = new PointF((float)((double)center.X + (double)num42 * Math.Cos(num39)), (float)((double)center.Y + (double)num42 * Math.Sin(num39)));
					using Pen pen2 = new Pen(Color.FromArgb(180, Color.DimGray), 2f);
					pen2.StartCap = LineCap.Round;
					pen2.EndCap = LineCap.Round;
					g.DrawLine(pen2, pt, pt2);
					g.DrawLine(pen2, pt3, pt4);
				}
				finally
				{
					if (region2 != null)
					{
						g.SetClip(region2, CombineMode.Replace);
						region2.Dispose();
					}
					else
					{
						g.ResetClip();
					}
				}
			}
		}
	}

	private Image GetIconForMenuItem(MenuItemConfig item, string letterSource, int desiredSize)
	{
		if (item == null)
		{
			return null;
		}
		if (desiredSize <= 0)
		{
			desiredSize = 24;
		}
		string text = (item.IconPath ?? "").Trim();
		bool flag = string.Equals(item.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(item.Command, "_EXIT", StringComparison.OrdinalIgnoreCase);
		if (item.UseFirstCharIcon && !flag)
		{
			return RadialMenuController.CreateLetterIcon(letterSource, desiredSize);
		}
		if (!string.IsNullOrWhiteSpace(text))
		{
			return RadialMenuController.GetScaledIcon(text, desiredSize);
		}
		return null;
	}

	private Image GetPreviewIcon(string iconSpec)
	{
		if (string.IsNullOrWhiteSpace(iconSpec))
		{
			return null;
		}
		string text = iconSpec.Trim();
		if (previewIconCache.ContainsKey(text))
		{
			return previewIconCache[text];
		}
		Image image = RadialMenuController.LoadIcon(text);
		previewIconCache[text] = image;
		return image;
	}

	private List<MenuItemConfig> GetItemsForLevel(int level)
	{
		IEnumerable<MenuItemConfig> source = configs.Where([SpecialName] (MenuItemConfig c) => Operators.CompareString(c.ParentMenuName, currentParentMenuName, TextCompare: false) == 0 && c.Level == level);
		if (level == 2)
		{
			return source.ToList();
		}
		return source.Where([SpecialName] (MenuItemConfig c) => c.Page == currentPage).ToList();
	}

	private void ApplyOuterRingDefaultColor(MenuItemConfig item)
	{
		if (item != null && item.Level == 2)
		{
			string a = (item.Command ?? "").Trim();
			if (!string.Equals(a, "_BACK", StringComparison.OrdinalIgnoreCase) && !string.Equals(a, "_SETTINGS", StringComparison.OrdinalIgnoreCase) && !string.Equals(a, "_EXIT", StringComparison.OrdinalIgnoreCase))
			{
				item.ColorArgb = ColorTranslator.FromHtml("#F0F0F0").ToArgb();
			}
		}
	}

	private void renderPanel_MouseWheel(object sender, MouseEventArgs e)
	{
		bool flag = (isDragging && dragItem != null) || isExternalDragging;
		if (flag && previewActive)
		{
			RevertPreviewIfNeeded();
		}
		checked
		{
			if (string.IsNullOrEmpty(currentProcessName))
			{
				if (!flag && e.Delta < 0 && !string.IsNullOrEmpty(lastProcessScopeName))
				{
					string processName = lastProcessScopeName;
					string text = lastProcessScopeParentMenuName;
					int val = lastProcessScopePage;
					LoadScope(processName);
					RestoreScopeItems();
					currentParentMenuName = text;
					RecalculatePages();
					if (totalPages > 0)
					{
						currentPage = Math.Max(0, Math.Min(val, totalPages - 1));
					}
					else
					{
						currentPage = 0;
					}
					UpdateStatus();
				}
				return;
			}
			if (!flag && e.Delta > 0 && currentPage == 0)
			{
				lastProcessScopeName = currentProcessName;
				lastProcessScopeParentMenuName = currentParentMenuName;
				lastProcessScopePage = currentPage;
				SwitchScope("");
				return;
			}
			if (e.Delta < 0)
			{
				currentPage++;
				if (currentPage >= totalPages)
				{
					totalPages = currentPage + 1;
				}
			}
			else if (currentPage > 0)
			{
				currentPage--;
			}
			UpdateStatus();
			if (!flag)
			{
				return;
			}
			try
			{
				HitInfo sectorAt = GetSectorAt(dragCurrentPos = renderPanel.PointToClient(Cursor.Position));
				hoverSectorIndex = sectorAt.Index;
				hoverSectorLevel = sectorAt.Level;
				renderPanel.Invalidate();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void renderPanel_KeyDown(object sender, KeyEventArgs e)
	{
		if (e != null && renderPanel != null && (e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
		{
			Point pt = renderPanel.PointToClient(Cursor.Position);
			HitInfo sectorAt = GetSectorAt(pt);
			if (sectorAt.Index >= 0 && sectorAt.Level >= 0)
			{
				PasteQuickerActionToHit(sectorAt, pt);
				e.SuppressKeyPress = true;
				e.Handled = true;
			}
		}
	}

	private static bool HasQuickerClipboardItem(System.Windows.Forms.IDataObject data)
	{
		if (data == null)
		{
			return false;
		}
		try
		{
			string[] formats = data.GetFormats();
			foreach (string a in formats)
			{
				if (string.Equals(a, "quicker-action-drag-item", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				if (string.Equals(a, "quicker-action-item", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (data.GetDataPresent(DataFormats.UnicodeText))
			{
				if (ExtractGuidLike(data.GetData(DataFormats.UnicodeText) as string).Length > 0)
				{
					return true;
				}
			}
			else if (data.GetDataPresent(DataFormats.Text) && ExtractGuidLike(data.GetData(DataFormats.Text) as string).Length > 0)
			{
				return true;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return false;
	}

	private void PasteQuickerActionToHit(HitInfo hit, Point pt)
	{
		System.Windows.Forms.IDataObject dataObject = null;
		try
		{
			dataObject = Clipboard.GetDataObject();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			dataObject = null;
			ProjectData.ClearProjectError();
		}
		if (dataObject == null)
		{
			return;
		}
		string actionId = "";
		string title = "";
		string iconSpec = "";
		if (!TryExtractQuickerAction(dataObject, ref actionId, ref title, ref iconSpec) || string.IsNullOrWhiteSpace(actionId))
		{
			return;
		}
		MenuItemConfig orCreateItemAt = GetOrCreateItemAt(hit, pt);
		if (orCreateItemAt == null)
		{
			return;
		}
		orCreateItemAt.Text = (string.IsNullOrWhiteSpace(title) ? orCreateItemAt.Text : title.Trim());
		string text = ParseQuickerActionParam(orCreateItemAt.Command);
		string actionId2 = "";
		TryGetQuickerActionIdFromCommand(orCreateItemAt.Command, ref actionId2);
		string text2 = actionId.Trim();
		if (!string.IsNullOrWhiteSpace(text))
		{
			text2 = text2 + "?" + text;
		}
		orCreateItemAt.Command = "QCAD_ACTION:" + text2;
		if (!string.IsNullOrWhiteSpace(iconSpec))
		{
			orCreateItemAt.IconPath = iconSpec.Trim();
		}
		if (!string.Equals(actionId2 ?? "", actionId.Trim(), StringComparison.OrdinalIgnoreCase))
		{
			orCreateItemAt.QuickerMenuItems = null;
		}
		List<QuickerRightClickMenuItemConfig> list = TryExtractQuickerRightClickMenuItems(dataObject, actionId.Trim());
		if (list != null && list.Count > 0)
		{
			orCreateItemAt.QuickerMenuItems = list;
		}
		ApplyOuterRingDefaultColor(orCreateItemAt);
		RadialMenuConfigStore.NormalizeMenuItems(configs);
		if (!string.IsNullOrWhiteSpace(orCreateItemAt.IconPath))
		{
			string key = orCreateItemAt.IconPath.Trim();
			if (previewIconCache.ContainsKey(key))
			{
				try
				{
					if (previewIconCache[key] != null)
					{
						previewIconCache[key].Dispose();
					}
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				previewIconCache.Remove(key);
			}
		}
		renderPanel.Invalidate();
	}

	private void renderPanel_MouseDown(object sender, MouseEventArgs e)
	{
		_Closure_0024__135_002D2 closure_0024__135_002D = new _Closure_0024__135_002D2();
		closure_0024__135_002D._0024VB_0024Me = this;
		closure_0024__135_002D._0024VB_0024Local_e = e;
		try
		{
			renderPanel.Focus();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		if (closure_0024__135_002D._0024VB_0024Local_e.Button == MouseButtons.Right)
		{
			_Closure_0024__135_002D0 closure_0024__135_002D2 = new _Closure_0024__135_002D0();
			closure_0024__135_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__135_002D;
			closure_0024__135_002D2._0024VB_0024Local_hitRight = GetSectorAt(closure_0024__135_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_e.Location);
			if (closure_0024__135_002D2._0024VB_0024Local_hitRight.Index < 0)
			{
				return;
			}
			_Closure_0024__135_002D1 CS_0024_003C_003E8__locals23 = new _Closure_0024__135_002D1();
			CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3 = closure_0024__135_002D2;
			List<MenuItemConfig> itemsForLevel = GetItemsForLevel(CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_hitRight.Level);
			CS_0024_003C_003E8__locals23._0024VB_0024Local_item = itemsForLevel.FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_hitRight.Index);
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			bool flag = CS_0024_003C_003E8__locals23._0024VB_0024Local_item != null && string.Equals(CS_0024_003C_003E8__locals23._0024VB_0024Local_item.Command, "_BACK", StringComparison.OrdinalIgnoreCase);
			if (CS_0024_003C_003E8__locals23._0024VB_0024Local_item == null)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("新建");
				toolStripMenuItem.Click += [SpecialName] (object a0, EventArgs a1) =>
				{
					CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._Lambda_0024__1();
				};
				contextMenuStrip.Items.Add(toolStripMenuItem);
			}
			else if ((CS_0024_003C_003E8__locals23._0024VB_0024Local_item.IsSubMenu || string.Equals(CS_0024_003C_003E8__locals23._0024VB_0024Local_item.OperationType ?? "", "子菜单", StringComparison.OrdinalIgnoreCase)) && !string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals23._0024VB_0024Local_item.SubMenuName ?? ""))
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("进入子菜单");
				toolStripMenuItem2.Click += [SpecialName] (object a0, EventArgs a1) =>
				{
					CS_0024_003C_003E8__locals23._Lambda_0024__2();
				};
				contextMenuStrip.Items.Add(toolStripMenuItem2);
			}
			ToolStripItem toolStripItem = contextMenuStrip.Items.Add("编辑");
			toolStripItem.Enabled = CS_0024_003C_003E8__locals23._0024VB_0024Local_item != null && !flag;
			toolStripItem.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._Lambda_0024__3();
			};
			ToolStripItem toolStripItem2 = contextMenuStrip.Items.Add("删除");
			toolStripItem2.Enabled = CS_0024_003C_003E8__locals23._0024VB_0024Local_item != null && !flag;
			toolStripItem2.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._Lambda_0024__4();
			};
			contextMenuStrip.Items.Add(new ToolStripSeparator());
			ToolStripItem toolStripItem3 = contextMenuStrip.Items.Add("剪切");
			toolStripItem3.Enabled = CS_0024_003C_003E8__locals23._0024VB_0024Local_item != null && !flag;
			toolStripItem3.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._Lambda_0024__5();
			};
			ToolStripItem toolStripItem4 = contextMenuStrip.Items.Add("复制");
			toolStripItem4.Enabled = CS_0024_003C_003E8__locals23._0024VB_0024Local_item != null && !flag;
			toolStripItem4.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._Lambda_0024__6();
			};
			ToolStripItem toolStripItem5 = contextMenuStrip.Items.Add("粘贴");
			toolStripItem5.Enabled = clipboardItemTemplate != null && !flag;
			toolStripItem5.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._Lambda_0024__7();
			};
			ToolStripItem toolStripItem6 = contextMenuStrip.Items.Add("粘贴Quicker动作");
			System.Windows.Forms.IDataObject dataObject = null;
			try
			{
				dataObject = Clipboard.GetDataObject();
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				dataObject = null;
				ProjectData.ClearProjectError();
			}
			toolStripItem6.Enabled = !flag && HasQuickerClipboardItem(dataObject);
			toolStripItem6.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._Lambda_0024__9();
			};
			contextMenuStrip.Show(renderPanel, CS_0024_003C_003E8__locals23._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_e.Location);
		}
		else
		{
			if (closure_0024__135_002D._0024VB_0024Local_e.Button != MouseButtons.Left)
			{
				return;
			}
			HitInfo sectorAt = GetSectorAt(closure_0024__135_002D._0024VB_0024Local_e.Location);
			if (sectorAt.Index >= 0)
			{
				MenuItemConfig menuItemConfig = GetItemsForLevel(sectorAt.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == sectorAt.Index);
				if (menuItemConfig != null)
				{
					isDragging = true;
					dragItem = menuItemConfig;
					dragStartPos = closure_0024__135_002D._0024VB_0024Local_e.Location;
					dragCurrentPos = closure_0024__135_002D._0024VB_0024Local_e.Location;
					dragOriginalSectorIndex = dragItem.SectorIndex;
					dragOriginalLevel = dragItem.Level;
					dragOriginalPage = dragItem.Page;
					previewActive = false;
					previewSwapTarget = null;
					hoverSectorIndex = sectorAt.Index;
					hoverSectorLevel = sectorAt.Level;
					renderPanel.Invalidate();
				}
			}
		}
	}

	private void EnsureItemInCorrectStorageList(MenuItemConfig item)
	{
		if (item == null || appConfig == null || appConfig.GlobalMenuItems == null || configs == null)
		{
			return;
		}
		List<MenuItemConfig> globalMenuItems = appConfig.GlobalMenuItems;
		List<MenuItemConfig> list = configs;
		if (!object.ReferenceEquals(list, globalMenuItems))
		{
			globalMenuItems.Remove(item);
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
	}

	private void ClearPreviewState()
	{
		previewActive = false;
		previewSwapTarget = null;
		previewSwapTargetSectorIndex = -1;
		previewSwapTargetLevel = -1;
		previewSwapTargetPage = -1;
	}

	private void RevertPreviewIfNeeded()
	{
		if (!previewActive || dragItem == null)
		{
			return;
		}
		if (previewSwapTarget != null)
		{
			try
			{
				previewSwapTarget.SectorIndex = previewSwapTargetSectorIndex;
				previewSwapTarget.Level = previewSwapTargetLevel;
				previewSwapTarget.Page = previewSwapTargetPage;
				previewSwapTarget.ParentMenuName = currentParentMenuName;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			EnsureItemInCorrectStorageList(previewSwapTarget);
		}
		try
		{
			dragItem.SectorIndex = dragOriginalSectorIndex;
			dragItem.Level = dragOriginalLevel;
			dragItem.Page = dragOriginalPage;
			dragItem.ParentMenuName = currentParentMenuName;
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		EnsureItemInCorrectStorageList(dragItem);
		ClearPreviewState();
	}

	private void ApplyPreviewMove(HitInfo hit)
	{
		if (dragItem != null)
		{
			previewActive = true;
			dragItem.SectorIndex = hit.Index;
			dragItem.Level = hit.Level;
			dragItem.ParentMenuName = currentParentMenuName;
			dragItem.Page = ((hit.Level != 2) ? currentPage : 0);
			EnsureItemInCorrectStorageList(dragItem);
		}
	}

	private void ApplyPreviewSwap(MenuItemConfig targetItem, HitInfo hit)
	{
		if (dragItem != null && targetItem != null)
		{
			previewActive = true;
			previewSwapTarget = targetItem;
			previewSwapTargetSectorIndex = targetItem.SectorIndex;
			previewSwapTargetLevel = targetItem.Level;
			previewSwapTargetPage = targetItem.Page;
			targetItem.SectorIndex = dragOriginalSectorIndex;
			targetItem.Level = dragOriginalLevel;
			targetItem.ParentMenuName = currentParentMenuName;
			targetItem.Page = ((dragOriginalLevel != 2) ? dragOriginalPage : 0);
			dragItem.SectorIndex = hit.Index;
			dragItem.Level = hit.Level;
			dragItem.ParentMenuName = currentParentMenuName;
			dragItem.Page = ((hit.Level != 2) ? currentPage : 0);
			EnsureItemInCorrectStorageList(targetItem);
			EnsureItemInCorrectStorageList(dragItem);
		}
	}

	private void renderPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (!isDragging || dragItem == null)
		{
			return;
		}
		dragCurrentPos = e.Location;
		HitInfo sectorAt = GetSectorAt(e.Location);
		hoverSectorIndex = sectorAt.Index;
		hoverSectorLevel = sectorAt.Level;
		bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
		if (sectorAt.Index < 0 || sectorAt.Level < 0 || flag)
		{
			RevertPreviewIfNeeded();
			renderPanel.Invalidate();
			return;
		}
		if (previewActive)
		{
			if (sectorAt.Index == dragItem.SectorIndex && sectorAt.Level == dragItem.Level)
			{
				renderPanel.Invalidate();
				return;
			}
			RevertPreviewIfNeeded();
		}
		MenuItemConfig menuItemConfig = GetItemsForLevel(sectorAt.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == sectorAt.Index);
		if (menuItemConfig != null && menuItemConfig == dragItem)
		{
			renderPanel.Invalidate();
			return;
		}
		if (menuItemConfig != null)
		{
			ApplyPreviewSwap(menuItemConfig, sectorAt);
		}
		else
		{
			ApplyPreviewMove(sectorAt);
		}
		renderPanel.Invalidate();
	}

	private void renderPanel_MouseUp(object sender, MouseEventArgs e)
	{
		if (!isDragging || dragItem == null)
		{
			return;
		}
		isDragging = false;
		HitInfo sectorAt = GetSectorAt(e.Location);
		bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
		if (previewActive && !flag && sectorAt.Index >= 0 && sectorAt.Level >= 0 && sectorAt.Index == dragItem.SectorIndex && sectorAt.Level == dragItem.Level)
		{
			ClearPreviewState();
			dragItem = null;
			hoverSectorIndex = -1;
			hoverSectorLevel = -1;
			RecalculatePages();
			renderPanel.Invalidate();
			return;
		}
		if (previewActive)
		{
			RevertPreviewIfNeeded();
		}
		if (sectorAt.Index < 0 || sectorAt.Level < 0)
		{
			dragItem.SectorIndex = dragOriginalSectorIndex;
			dragItem.Level = dragOriginalLevel;
			dragItem.Page = dragOriginalPage;
			dragItem.ParentMenuName = currentParentMenuName;
			EnsureItemInCorrectStorageList(dragItem);
			dragItem = null;
			hoverSectorIndex = -1;
			hoverSectorLevel = -1;
			renderPanel.Invalidate();
			return;
		}
		if (flag)
		{
			if (string.Equals(dragItem.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase) || string.Equals(dragItem.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
			{
				dragItem = null;
				hoverSectorIndex = -1;
				hoverSectorLevel = -1;
				renderPanel.Invalidate();
				return;
			}
			if (GetItemsForLevel(sectorAt.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == sectorAt.Index) == null)
			{
				MenuItemConfig menuItemConfig = RadialMenuConfigStore.CloneMenuItems(new List<MenuItemConfig> { dragItem }).FirstOrDefault();
				if (menuItemConfig != null)
				{
					menuItemConfig.Level = sectorAt.Level;
					menuItemConfig.ParentMenuName = currentParentMenuName;
					menuItemConfig.SectorIndex = sectorAt.Index;
					menuItemConfig.Page = ((sectorAt.Level != 2) ? currentPage : 0);
					ApplyOuterRingDefaultColor(menuItemConfig);
					configs.Add(menuItemConfig);
					RadialMenuConfigStore.NormalizeMenuItems(configs);
					RecalculatePages();
				}
				dragItem = null;
				hoverSectorIndex = -1;
				hoverSectorLevel = -1;
				renderPanel.Invalidate();
				return;
			}
		}
		MenuItemConfig menuItemConfig2 = GetItemsForLevel(sectorAt.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == sectorAt.Index);
		int sectorIndex = dragItem.SectorIndex;
		int level = dragItem.Level;
		int page = dragItem.Page;
		dragItem.SectorIndex = sectorAt.Index;
		dragItem.Level = sectorAt.Level;
		dragItem.ParentMenuName = currentParentMenuName;
		dragItem.Page = ((sectorAt.Level != 2) ? currentPage : 0);
		EnsureItemInCorrectStorageList(dragItem);
		ApplyOuterRingDefaultColor(dragItem);
		if (menuItemConfig2 != null && menuItemConfig2 != dragItem)
		{
			menuItemConfig2.SectorIndex = sectorIndex;
			menuItemConfig2.Level = level;
			menuItemConfig2.ParentMenuName = currentParentMenuName;
			menuItemConfig2.Page = ((level != 2) ? page : 0);
			EnsureItemInCorrectStorageList(menuItemConfig2);
			ApplyOuterRingDefaultColor(menuItemConfig2);
		}
		RadialMenuConfigStore.NormalizeMenuItems(configs);
		RecalculatePages();
		dragItem = null;
		hoverSectorIndex = -1;
		hoverSectorLevel = -1;
		renderPanel.Invalidate();
	}

	private void renderPanel_DragEnter(object sender, DragEventArgs e)
	{
		try
		{
			if (e.Data != null && HasQuickerDragItem(e.Data))
			{
				isExternalDragging = true;
				e.Effect = DragDropEffects.Copy;
				LogDeg("DragEnter: ok formats=" + string.Join("|", SafeGetFormats(e.Data)));
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			e.Effect = DragDropEffects.None;
			ProjectData.ClearProjectError();
		}
	}

	private void renderPanel_DragOver(object sender, DragEventArgs e)
	{
		if (isExternalDragging)
		{
			try
			{
				Point pt = renderPanel.PointToClient(new Point(e.X, e.Y));
				HitInfo sectorAt = GetSectorAt(pt);
				hoverSectorIndex = sectorAt.Index;
				hoverSectorLevel = sectorAt.Level;
				LogDeg("DragOver: x=" + Conversions.ToString(pt.X) + " y=" + Conversions.ToString(pt.Y) + " idx=" + Conversions.ToString(sectorAt.Index) + " level=" + Conversions.ToString(sectorAt.Level));
				renderPanel.Invalidate();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void renderPanel_DragLeave(object sender, EventArgs e)
	{
		isExternalDragging = false;
		hoverSectorIndex = -1;
		hoverSectorLevel = -1;
		renderPanel.Invalidate();
	}

	private void renderPanel_DragDrop(object sender, DragEventArgs e)
	{
		try
		{
			isExternalDragging = false;
			Point pt = renderPanel.PointToClient(new Point(e.X, e.Y));
			HitInfo sectorAt = GetSectorAt(pt);
			hoverSectorIndex = -1;
			hoverSectorLevel = -1;
			LogDeg("DragDrop: x=" + Conversions.ToString(pt.X) + " y=" + Conversions.ToString(pt.Y) + " idx=" + Conversions.ToString(sectorAt.Index) + " level=" + Conversions.ToString(sectorAt.Level) + " formats=" + string.Join("|", SafeGetFormats(e.Data)));
			if (sectorAt.Index < 0 || sectorAt.Level < 0)
			{
				renderPanel.Invalidate();
				return;
			}
			string actionId = "";
			string title = "";
			string iconSpec = "";
			if (!TryExtractQuickerAction(e.Data, ref actionId, ref title, ref iconSpec))
			{
				LogDeg("DragDrop: TryExtractQuickerAction=false");
				renderPanel.Invalidate();
				return;
			}
			if (string.IsNullOrWhiteSpace(actionId))
			{
				LogDeg("DragDrop: actionId empty");
				renderPanel.Invalidate();
				return;
			}
			MenuItemConfig orCreateItemAt = GetOrCreateItemAt(sectorAt, pt);
			if (orCreateItemAt == null)
			{
				LogDeg("DragDrop: item null");
				renderPanel.Invalidate();
				return;
			}
			orCreateItemAt.Text = (string.IsNullOrWhiteSpace(title) ? orCreateItemAt.Text : title.Trim());
			string text = ParseQuickerActionParam(orCreateItemAt.Command);
			string actionId2 = "";
			TryGetQuickerActionIdFromCommand(orCreateItemAt.Command, ref actionId2);
			string text2 = actionId.Trim();
			if (!string.IsNullOrWhiteSpace(text))
			{
				text2 = text2 + "?" + text;
			}
			orCreateItemAt.Command = "QCAD_ACTION:" + text2;
			if (!string.IsNullOrWhiteSpace(iconSpec))
			{
				orCreateItemAt.IconPath = iconSpec.Trim();
			}
			if (!string.Equals(actionId2 ?? "", actionId.Trim(), StringComparison.OrdinalIgnoreCase))
			{
				orCreateItemAt.QuickerMenuItems = null;
			}
			List<QuickerRightClickMenuItemConfig> list = TryExtractQuickerRightClickMenuItems(e.Data, actionId.Trim());
			if (list != null && list.Count > 0)
			{
				orCreateItemAt.QuickerMenuItems = list;
			}
			LogDeg("DragDrop: QuickerMenuItems.Count=" + Conversions.ToString((orCreateItemAt.QuickerMenuItems != null) ? orCreateItemAt.QuickerMenuItems.Count : 0));
			LogDeg("DragDrop: ok text=" + orCreateItemAt.Text + " cmd=" + orCreateItemAt.Command + " icon=" + orCreateItemAt.IconPath);
			ApplyOuterRingDefaultColor(orCreateItemAt);
			RadialMenuConfigStore.NormalizeMenuItems(configs);
			if (!string.IsNullOrWhiteSpace(orCreateItemAt.IconPath))
			{
				string key = orCreateItemAt.IconPath.Trim();
				if (previewIconCache.ContainsKey(key))
				{
					try
					{
						if (previewIconCache[key] != null)
						{
							previewIconCache[key].Dispose();
						}
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					previewIconCache.Remove(key);
				}
			}
			renderPanel.Invalidate();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			LogDeg("DragDrop: ex=" + ex2.ToString());
			MessageBox.Show("拖拽导入失败：" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			ProjectData.ClearProjectError();
		}
	}

	private MenuItemConfig GetOrCreateItemAt(HitInfo hit, Point pt)
	{
		MenuItemConfig menuItemConfig = GetItemsForLevel(hit.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == hit.Index);
		if (menuItemConfig != null)
		{
			return menuItemConfig;
		}
		MenuItemConfig menuItemConfig2 = new MenuItemConfig
		{
			Text = "新按钮",
			Command = "",
			ColorArgb = ((hit.Level == 2) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White).ToArgb(),
			Enabled = true,
			Level = hit.Level,
			ParentMenuName = currentParentMenuName,
			Page = ((hit.Level != 2) ? currentPage : 0),
			SectorIndex = hit.Index,
			FontSize = sizeConfig.FontSize
		};
		configs.Add(menuItemConfig2);
		RadialMenuConfigStore.NormalizeMenuItems(configs);
		RecalculatePages();
		return menuItemConfig2;
	}

	private static string ParseQuickerActionParam(string cmd)
	{
		if (string.IsNullOrWhiteSpace(cmd))
		{
			return "";
		}
		if (!cmd.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase))
		{
			return "";
		}
		string text = cmd.Substring("QCAD_ACTION:".Length).Trim();
		int num = text.IndexOf('?');
		if (num < 0)
		{
			return "";
		}
		return text.Substring(checked(num + 1));
	}

	private static bool TryGetQuickerActionIdFromCommand(string cmd, ref string actionId)
	{
		actionId = "";
		if (string.IsNullOrWhiteSpace(cmd))
		{
			return false;
		}
		if (!cmd.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		string text = cmd.Substring("QCAD_ACTION:".Length).Trim();
		if (text.Length == 0)
		{
			return false;
		}
		int num = text.IndexOf('?');
		if (num < 0)
		{
			actionId = text;
		}
		else
		{
			actionId = text.Substring(0, num);
		}
		actionId = (actionId ?? "").Trim();
		return actionId.Length > 0;
	}

	private static List<QuickerRightClickMenuItemConfig> TryExtractQuickerRightClickMenuItems(System.Windows.Forms.IDataObject data, string actionId)
	{
		List<QuickerRightClickMenuItemConfig> list = new List<QuickerRightClickMenuItemConfig>();
		if (data == null)
		{
			return list;
		}
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return list;
		}
		List<string> list2 = new List<string>();
		try
		{
			if (data.GetDataPresent(DataFormats.UnicodeText))
			{
				string text = data.GetData(DataFormats.UnicodeText) as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					list2.Add(text);
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (data.GetDataPresent(DataFormats.Text))
			{
				string text2 = data.GetData(DataFormats.Text) as string;
				if (!string.IsNullOrWhiteSpace(text2))
				{
					list2.Add(text2);
				}
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		try
		{
			if (data.GetDataPresent(DataFormats.Html))
			{
				string text3 = data.GetData(DataFormats.Html) as string;
				if (!string.IsNullOrWhiteSpace(text3))
				{
					list2.Add(text3);
				}
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		try
		{
			string[] formats = data.GetFormats();
			foreach (string text4 in formats)
			{
				if (string.IsNullOrWhiteSpace(text4))
				{
					continue;
				}
				try
				{
					if (data.GetDataPresent(text4, autoConvert: false))
					{
						string text5 = RuntimeHelpers.GetObjectValue(data.GetData(text4, autoConvert: false)) as string;
						if (!string.IsNullOrWhiteSpace(text5))
						{
							list2.Add(text5);
						}
					}
				}
				catch (Exception projectError4)
				{
					ProjectData.SetProjectError(projectError4);
					ProjectData.ClearProjectError();
				}
			}
		}
		catch (Exception projectError5)
		{
			ProjectData.SetProjectError(projectError5);
			ProjectData.ClearProjectError();
		}
		string[] array = new string[2] { "quicker-action-drag-item", "quicker-action-item" };
		string text6 = "";
		object obj = null;
		string[] array2 = array;
		foreach (string text7 in array2)
		{
			string text8 = text7;
			try
			{
				string[] formats2 = data.GetFormats();
				foreach (string text9 in formats2)
				{
					if (string.Equals(text9, text7, StringComparison.OrdinalIgnoreCase))
					{
						text8 = text9;
						break;
					}
				}
			}
			catch (Exception projectError6)
			{
				ProjectData.SetProjectError(projectError6);
				ProjectData.ClearProjectError();
			}
			try
			{
				obj = (data.GetDataPresent(text8, autoConvert: false) ? RuntimeHelpers.GetObjectValue(data.GetData(text8, autoConvert: false)) : (data.GetDataPresent(text8, autoConvert: true) ? RuntimeHelpers.GetObjectValue(data.GetData(text8, autoConvert: true)) : ((!data.GetDataPresent(text8)) ? null : RuntimeHelpers.GetObjectValue(data.GetData(text8)))));
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				obj = null;
				ProjectData.ClearProjectError();
			}
			if (obj != null)
			{
				text6 = text8;
				break;
			}
		}
		byte[] array3 = null;
		if (obj != null)
		{
			if (obj is byte[])
			{
				array3 = (byte[])obj;
			}
			else if (obj is MemoryStream)
			{
				try
				{
					array3 = ((MemoryStream)obj).ToArray();
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					array3 = null;
					ProjectData.ClearProjectError();
				}
			}
			else if (obj is Stream)
			{
				try
				{
					using MemoryStream memoryStream = new MemoryStream();
					((Stream)obj).CopyTo(memoryStream);
					array3 = memoryStream.ToArray();
				}
				catch (Exception projectError9)
				{
					ProjectData.SetProjectError(projectError9);
					array3 = null;
					ProjectData.ClearProjectError();
				}
			}
		}
		if (array3 == null || array3.Length == 0)
		{
			if (text6.Length > 0)
			{
				try
				{
					array3 = TryGetComRawBytes(data, text6);
				}
				catch (Exception projectError10)
				{
					ProjectData.SetProjectError(projectError10);
					array3 = null;
					ProjectData.ClearProjectError();
				}
			}
			if (array3 == null || array3.Length == 0)
			{
				string[] array4 = array;
				foreach (string text10 in array4)
				{
					string text11 = text10;
					try
					{
						string[] formats3 = data.GetFormats();
						foreach (string text12 in formats3)
						{
							if (string.Equals(text12, text10, StringComparison.OrdinalIgnoreCase))
							{
								text11 = text12;
								break;
							}
						}
					}
					catch (Exception projectError11)
					{
						ProjectData.SetProjectError(projectError11);
						ProjectData.ClearProjectError();
					}
					try
					{
						array3 = TryGetComRawBytes(data, text11);
					}
					catch (Exception projectError12)
					{
						ProjectData.SetProjectError(projectError12);
						array3 = null;
						ProjectData.ClearProjectError();
					}
					if (array3 != null && array3.Length > 0)
					{
						text6 = text11;
						break;
					}
				}
			}
		}
		List<string> list3 = new List<string>();
		if (list2.Count > 0)
		{
			list3.AddRange(list2);
		}
		if (array3 != null && array3.Length > 0)
		{
			try
			{
				list3.AddRange(ExtractAsciiStrings(array3, 6));
			}
			catch (Exception projectError13)
			{
				ProjectData.SetProjectError(projectError13);
				ProjectData.ClearProjectError();
			}
			try
			{
				list3.AddRange(ExtractUtf16LeAsciiStrings(array3, 6));
			}
			catch (Exception projectError14)
			{
				ProjectData.SetProjectError(projectError14);
				ProjectData.ClearProjectError();
			}
		}
		if (obj != null)
		{
			List<string> list4 = new List<string>();
			TryCollectStringsFromObjectGraph(RuntimeHelpers.GetObjectValue(obj), list4, 4);
			list3.AddRange(list4);
		}
		list3 = (from s in list3
			where !string.IsNullOrWhiteSpace(s)
			select s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		string text13 = "";
		if (list3 != null && list3.Count > 0)
		{
			try
			{
				text13 = string.Join(" || ", (from x in list3.Take(3)
					select (x.Length <= 120) ? x : x.Substring(0, 120)).ToArray());
			}
			catch (Exception projectError15)
			{
				ProjectData.SetProjectError(projectError15);
				text13 = "";
				ProjectData.ClearProjectError();
			}
		}
		LogDeg("TryExtractMenu: actionId=" + actionId.Trim() + ", fmt=" + text6 + ", dragObj=" + ((obj == null) ? "null" : obj.GetType().FullName) + ", rawLen=" + Conversions.ToString((array3 != null) ? array3.Length : 0) + ", texts=" + list3.Count + ((Operators.CompareString(text13, "", TextCompare: false) == 0) ? "" : (", probe=" + text13)));
		List<string> list5 = null;
		if (list3.Count > 0)
		{
			foreach (string item3 in list3)
			{
				List<string> list6 = (from x in SplitCandidateLines(item3)
					where LooksLikeQuickerMenuLine(x)
					select x).ToList();
				if (list6.Count > 0 && (list5 == null || list6.Count > list5.Count))
				{
					list5 = list6;
				}
			}
		}
		if (list5 != null && list5.Count > 0)
		{
			foreach (string item4 in list5)
			{
				QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig = ParseQuickerMenuLine(item4);
				if (quickerRightClickMenuItemConfig != null)
				{
					quickerRightClickMenuItemConfig.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig.Parameter, actionId.Trim());
					quickerRightClickMenuItemConfig.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig.Marker, "+", StringComparison.Ordinal);
					list.Add(quickerRightClickMenuItemConfig);
				}
			}
		}
		else if (list3.Count > 0)
		{
			foreach (string item5 in list3)
			{
				foreach (string item6 in SplitCandidateLines(item5))
				{
					if (LooksLikeQuickerMenuLine(item6))
					{
						QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig2 = ParseQuickerMenuLine(item6);
						if (quickerRightClickMenuItemConfig2 != null)
						{
							quickerRightClickMenuItemConfig2.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig2.Parameter, actionId.Trim());
							quickerRightClickMenuItemConfig2.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig2.Marker, "+", StringComparison.Ordinal);
							list.Add(quickerRightClickMenuItemConfig2);
						}
					}
				}
				MatchCollection matchCollection = Regex.Matches(item5, "(?:\\[\\s*[+-]\\s*\\]\\s*)?\\[(?i:(?:fa|url):[^\\]]+)\\][^\\r\\n]+");
				foreach (Match item7 in matchCollection)
				{
					if (item7 == null || !item7.Success)
					{
						continue;
					}
					string value = item7.Value;
					if (LooksLikeQuickerMenuLine(value))
					{
						QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig3 = ParseQuickerMenuLine(value);
						if (quickerRightClickMenuItemConfig3 != null)
						{
							quickerRightClickMenuItemConfig3.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig3.Parameter, actionId.Trim());
							quickerRightClickMenuItemConfig3.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig3.Marker, "+", StringComparison.Ordinal);
							list.Add(quickerRightClickMenuItemConfig3);
						}
					}
				}
			}
		}
		List<QuickerRightClickMenuItemConfig> list7 = new List<QuickerRightClickMenuItemConfig>();
		HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		string groupParent = "";
		foreach (QuickerRightClickMenuItemConfig item8 in list)
		{
			if (item8 != null)
			{
				if (item8.IsGroupHeader)
				{
					item8.GroupParent = "";
					groupParent = item8.DisplayText ?? "";
				}
				else if (string.Equals(item8.Marker, "-", StringComparison.Ordinal))
				{
					item8.GroupParent = groupParent;
				}
				else
				{
					item8.GroupParent = "";
					groupParent = "";
				}
				string text14 = (item8.Icon + "|" + item8.DisplayText + "|" + item8.Parameter + "|" + item8.GroupParent + "|" + item8.Marker).Trim();
				if (Operators.CompareString(text14, "||", TextCompare: false) != 0 && hashSet.Add(text14))
				{
					list7.Add(item8);
				}
			}
		}
		string text15 = "quicker:runaction:无限轮盘?sp=编辑动作&targetId=" + actionId.Trim();
		string item = ("编|编辑|" + text15 + "||").Trim();
		if (hashSet.Add(item))
		{
			list7.Add(new QuickerRightClickMenuItemConfig
			{
				Icon = "编",
				DisplayText = "编辑",
				DisplayDescription = "",
				Parameter = text15,
				Marker = "",
				IsGroupHeader = false,
				GroupParent = ""
			});
		}
		string text16 = "quicker:debugaction:" + actionId.Trim();
		string item2 = ("调|调试|" + text16 + "||").Trim();
		if (hashSet.Add(item2))
		{
			list7.Add(new QuickerRightClickMenuItemConfig
			{
				Icon = "调",
				DisplayText = "调试",
				DisplayDescription = "",
				Parameter = text16,
				Marker = "",
				IsGroupHeader = false,
				GroupParent = ""
			});
		}
		LogDeg("TryExtractMenu: parsed=" + list7.Count);
		return list7;
	}

	private static string NormalizeQuickerParam(string param, string actionId)
	{
		if (string.IsNullOrEmpty(param))
		{
			return "";
		}
		string text = param.Trim();
		if (Operators.CompareString(text, "", TextCompare: false) == 0)
		{
			return "";
		}
		string text2 = text.ToLowerInvariant();
		if (text2.StartsWith("runaction:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("debugaction:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("previewaction:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("settings:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("exesettings:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("installskin:"))
		{
			return "quicker:" + text;
		}
		if (text2.StartsWith("quicker:"))
		{
			return text;
		}
		if (text2.StartsWith("quicker://"))
		{
			return text;
		}
		if (Regex.IsMatch(text, "^\\s*\\d+\\s*$"))
		{
			string text3 = (actionId ?? "").Trim();
			if (text3.Length > 0)
			{
				return "runaction:" + text3 + "?" + text.Trim();
			}
		}
		if (text.IndexOf("://", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			return text;
		}
		if (text2.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("https:", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
		{
			return text;
		}
		string text4 = (actionId ?? "").Trim();
		if (text4.Length > 0)
		{
			string text5 = "runaction:" + text4 + "?" + text;
			Logger.Log("NormalizeQuickerParam: raw=" + param + ", actionId=" + text4 + ", result=" + text5);
			return text5;
		}
		Logger.Log("NormalizeQuickerParam: raw=" + param + ", actionId empty, result=" + text);
		return text;
	}

	private static IEnumerable<string> SplitCandidateLines(string t)
	{
		if (t == null)
		{
			return new string[0];
		}
		return from x in t.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
			select x.Trim() into x
			where x.Length > 0
			select x;
	}

	private static bool LooksLikeQuickerMenuLine(string line)
	{
		if (string.IsNullOrEmpty(line))
		{
			return false;
		}
		string text = line.Trim();
		if (text.StartsWith("- ", StringComparison.Ordinal))
		{
			text = text.Substring(2).TrimStart();
		}
		if (Regex.IsMatch(text, "^\\s*\\[\\s*[+-]\\s*\\]\\s*"))
		{
			return true;
		}
		if (Regex.IsMatch(text, "^\\s*\\d+\\s*$"))
		{
			return true;
		}
		text = Regex.Replace(text, "^\\s*\\[\\s*[+-]\\s*\\]\\s*", "", RegexOptions.None);
		if (text.Length < 2)
		{
			return false;
		}
		if (text.StartsWith("[fa:", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (text.StartsWith("[url:", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if ((text.StartsWith("[fa", StringComparison.OrdinalIgnoreCase) || text.StartsWith("[url", StringComparison.OrdinalIgnoreCase)) && text.Contains("]"))
		{
			return true;
		}
		checked
		{
			if (text.Contains("|"))
			{
				if (text.IndexOf("[fa:", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("fa:", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("[url:", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("url:", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
				int num = text.LastIndexOf('|');
				if (num > 0 && num < text.Length - 1)
				{
					string text2 = text.Substring(num + 1).Trim();
					if (Regex.IsMatch(text2, "^\\d+$"))
					{
						return true;
					}
					string text3 = text2.ToLowerInvariant();
					if (text3.StartsWith("runaction:") || text3.StartsWith("debugaction:") || text3.StartsWith("previewaction:") || text3.StartsWith("settings:") || text3.StartsWith("exesettings:") || text3.StartsWith("installskin:") || text3.StartsWith("quicker:") || text3.StartsWith("quicker://"))
					{
						return true;
					}
					if (text2.IndexOf("://", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	private static QuickerRightClickMenuItemConfig ParseQuickerMenuLine(string line)
	{
		if (string.IsNullOrEmpty(line))
		{
			return null;
		}
		string text = line.Trim();
		if (text.StartsWith("- ", StringComparison.Ordinal))
		{
			text = text.Substring(2).TrimStart();
		}
		if (text.Length == 0)
		{
			return null;
		}
		string text2 = "";
		Match match = Regex.Match(text, "^\\s*\\[\\s*(?<m>[+-])\\s*\\]\\s*");
		if (match.Success)
		{
			text2 = match.Groups["m"].Value;
			text = text.Substring(match.Length).TrimStart();
		}
		string text3 = "";
		string text4 = text;
		Match match2 = Regex.Match(text, "^\\s*\\[(?<icon>[^\\]]+)\\]\\s*(?<rest>.*)$");
		if (match2.Success)
		{
			text3 = match2.Groups["icon"].Value.Trim();
			text4 = match2.Groups["rest"].Value.Trim();
		}
		if (text4.Length == 0)
		{
			return null;
		}
		string text5 = text4;
		string parameter = text4;
		int num = text4.LastIndexOf('|');
		if (num >= 0)
		{
			text5 = text4.Substring(0, num).Trim();
			parameter = text4.Substring(checked(num + 1)).Trim();
		}
		else if (Operators.CompareString(text2, "+", TextCompare: false) == 0)
		{
			parameter = "";
		}
		string displayDescription = "";
		Match match3 = Regex.Match(text5, "^(?<name>.*?)(?:\\((?<desc>.*)\\))\\s*$");
		if (match3.Success)
		{
			string text6 = match3.Groups["name"].Value.Trim();
			string obj = match3.Groups["desc"].Value.Trim();
			if (text6.Length > 0)
			{
				text5 = text6;
			}
			displayDescription = obj;
		}
		return new QuickerRightClickMenuItemConfig
		{
			Icon = text3,
			DisplayText = text5,
			DisplayDescription = displayDescription,
			Parameter = parameter,
			Marker = text2
		};
	}

	private static void TryCollectStringsFromObjectGraph(object root, List<string> output, int maxDepth)
	{
		if (root == null || output == null || maxDepth < 0)
		{
			return;
		}
		HashSet<int> hashSet = new HashSet<int>();
		Stack<Tuple<object, int>> stack = new Stack<Tuple<object, int>>();
		stack.Push(Tuple.Create(RuntimeHelpers.GetObjectValue(root), 0));
		checked
		{
			while (stack.Count > 0)
			{
				Tuple<object, int> tuple = stack.Pop();
				object objectValue = RuntimeHelpers.GetObjectValue(tuple.Item1);
				int item = tuple.Item2;
				if (objectValue == null || item > maxDepth)
				{
					continue;
				}
				if (objectValue is string)
				{
					output.Add((string)objectValue);
					continue;
				}
				Type type = objectValue.GetType();
				if (type.IsPrimitive || type.IsEnum)
				{
					continue;
				}
				int hashCode = RuntimeHelpers.GetHashCode(RuntimeHelpers.GetObjectValue(objectValue));
				if (hashSet.Contains(hashCode))
				{
					continue;
				}
				hashSet.Add(hashCode);
				if (objectValue is IEnumerable enumerable && !(objectValue is byte[]))
				{
					int num = 0;
					foreach (object item2 in enumerable)
					{
						object objectValue2 = RuntimeHelpers.GetObjectValue(item2);
						if (num >= 200)
						{
							break;
						}
						num++;
						if (objectValue2 != null)
						{
							if (objectValue2 is string)
							{
								output.Add((string)objectValue2);
							}
							else
							{
								stack.Push(Tuple.Create(RuntimeHelpers.GetObjectValue(objectValue2), item + 1));
							}
						}
					}
					continue;
				}
				PropertyInfo[] array = null;
				try
				{
					array = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					array = null;
					ProjectData.ClearProjectError();
				}
				if (array == null)
				{
					continue;
				}
				int num2 = 0;
				PropertyInfo[] array2 = array;
				foreach (PropertyInfo propertyInfo in array2)
				{
					if ((object)propertyInfo == null || !propertyInfo.CanRead || propertyInfo.GetIndexParameters().Length > 0)
					{
						continue;
					}
					num2++;
					if (num2 > 80)
					{
						break;
					}
					object obj = null;
					try
					{
						obj = RuntimeHelpers.GetObjectValue(propertyInfo.GetValue(RuntimeHelpers.GetObjectValue(objectValue), null));
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						obj = null;
						ProjectData.ClearProjectError();
					}
					if (obj != null)
					{
						if (obj is string)
						{
							output.Add((string)obj);
						}
						else
						{
							stack.Push(Tuple.Create(RuntimeHelpers.GetObjectValue(obj), item + 1));
						}
					}
				}
			}
		}
	}

	private static bool HasQuickerDragItem(System.Windows.Forms.IDataObject data)
	{
		if (data == null)
		{
			return false;
		}
		try
		{
			if (data.GetDataPresent("quicker-action-drag-item"))
			{
				return true;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			string[] formats = data.GetFormats();
			for (int i = 0; i < formats.Length; i = checked(i + 1))
			{
				if (string.Equals(formats[i], "quicker-action-drag-item", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return false;
	}

	private static string[] SafeGetFormats(System.Windows.Forms.IDataObject data)
	{
		string[] result;
		if (data == null)
		{
			result = new string[0];
		}
		else
		{
			try
			{
				result = data.GetFormats();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = new string[0];
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private static string ExtractGuidLike(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return "";
		}
		try
		{
			Match match = Regex.Match(text, "(?i)\\b[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\\b");
			if (match != null && match.Success)
			{
				return match.Value.Trim();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static string FirstNonEmptyLine(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return "";
		}
		try
		{
			string[] array = text.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				string text2 = (array[i] ?? "").Trim();
				if (text2.Length > 0)
				{
					return text2;
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static bool TryExtractQuickerAction(System.Windows.Forms.IDataObject data, ref string actionId, ref string title, ref string iconSpec)
	{
		actionId = "";
		title = "";
		iconSpec = "";
		bool result;
		if (data == null)
		{
			result = false;
		}
		else
		{
			object obj = null;
			string text = "";
			string[] array = new string[2] { "quicker-action-drag-item", "quicker-action-item" };
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				string text3 = text2;
				try
				{
					string[] formats = data.GetFormats();
					foreach (string text4 in formats)
					{
						if (string.Equals(text4, text2, StringComparison.OrdinalIgnoreCase))
						{
							text3 = text4;
							break;
						}
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				try
				{
					obj = (data.GetDataPresent(text3, autoConvert: false) ? RuntimeHelpers.GetObjectValue(data.GetData(text3, autoConvert: false)) : (data.GetDataPresent(text3, autoConvert: true) ? RuntimeHelpers.GetObjectValue(data.GetData(text3, autoConvert: true)) : ((!data.GetDataPresent(text3)) ? null : RuntimeHelpers.GetObjectValue(data.GetData(text3)))));
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					obj = null;
					ProjectData.ClearProjectError();
				}
				if (obj != null)
				{
					text = text3;
					break;
				}
			}
			if (obj == null)
			{
				string[] array3 = array;
				int num = 0;
				while (true)
				{
					if (num < array3.Length)
					{
						string text5 = array3[num];
						string text6 = text5;
						try
						{
							string[] formats2 = data.GetFormats();
							foreach (string text7 in formats2)
							{
								if (string.Equals(text7, text5, StringComparison.OrdinalIgnoreCase))
								{
									text6 = text7;
									break;
								}
							}
						}
						catch (Exception projectError3)
						{
							ProjectData.SetProjectError(projectError3);
							ProjectData.ClearProjectError();
						}
						byte[] array4 = TryGetComRawBytes(data, text6);
						if (array4 != null && array4.Length > 0)
						{
							LogDeg("TryExtract: comRawBytes fmt=" + text6 + " len=" + Conversions.ToString(array4.Length));
							if (TryExtractQuickerActionFromBytes(array4, ref actionId, ref title, ref iconSpec))
							{
								result = !string.IsNullOrWhiteSpace(actionId);
								break;
							}
						}
						num = checked(num + 1);
						continue;
					}
					List<string> list = new List<string>();
					try
					{
						if (data.GetDataPresent(DataFormats.UnicodeText))
						{
							string text8 = data.GetData(DataFormats.UnicodeText) as string;
							if (!string.IsNullOrWhiteSpace(text8))
							{
								list.Add(text8);
							}
						}
					}
					catch (Exception projectError4)
					{
						ProjectData.SetProjectError(projectError4);
						ProjectData.ClearProjectError();
					}
					try
					{
						if (data.GetDataPresent(DataFormats.Text))
						{
							string text9 = data.GetData(DataFormats.Text) as string;
							if (!string.IsNullOrWhiteSpace(text9))
							{
								list.Add(text9);
							}
						}
					}
					catch (Exception projectError5)
					{
						ProjectData.SetProjectError(projectError5);
						ProjectData.ClearProjectError();
					}
					foreach (string item in list)
					{
						string text10 = ExtractGuidLike(item);
						if (text10.Length > 0)
						{
							actionId = text10;
							if (string.IsNullOrWhiteSpace(title))
							{
								title = FirstNonEmptyLine(item);
							}
							result = true;
							goto end_IL_01c8;
						}
					}
					result = false;
					break;
					continue;
					end_IL_01c8:
					break;
				}
			}
			else
			{
				LogDeg("TryExtract: fmt=" + text + ", dragObjType=" + obj.GetType().FullName);
				if (obj is byte[])
				{
					byte[] array5 = (byte[])obj;
					LogDeg("TryExtract: dragObjBytes len=" + Conversions.ToString(array5.Length));
					result = TryExtractQuickerActionFromBytes(array5, ref actionId, ref title, ref iconSpec) && !string.IsNullOrWhiteSpace(actionId);
				}
				else if (obj is MemoryStream)
				{
					try
					{
						byte[] array6 = ((MemoryStream)obj).ToArray();
						LogDeg("TryExtract: dragObjMemoryStream len=" + Conversions.ToString(array6.Length));
						result = TryExtractQuickerActionFromBytes(array6, ref actionId, ref title, ref iconSpec) && !string.IsNullOrWhiteSpace(actionId);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						LogDeg("TryExtract: MemoryStream ex=" + ex2.Message);
						result = false;
						ProjectData.ClearProjectError();
					}
				}
				else if (obj is Stream)
				{
					try
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							((Stream)obj).CopyTo(memoryStream);
							byte[] array7 = memoryStream.ToArray();
							LogDeg("TryExtract: dragObjStream len=" + Conversions.ToString(array7.Length));
							if (!TryExtractQuickerActionFromBytes(array7, ref actionId, ref title, ref iconSpec))
							{
								goto end_IL_03b5;
							}
							result = !string.IsNullOrWhiteSpace(actionId);
							goto end_IL_03ae;
							end_IL_03b5:;
						}
						result = false;
						end_IL_03ae:;
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						LogDeg("TryExtract: Stream ex=" + ex4.Message);
						result = false;
						ProjectData.ClearProjectError();
					}
				}
				else
				{
					object objectValue = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(obj), "Action"));
					if (objectValue == null)
					{
						objectValue = RuntimeHelpers.GetObjectValue(obj);
					}
					try
					{
						object objectValue2 = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(objectValue), "Id"));
						if (objectValue2 != null)
						{
							actionId = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue2));
						}
					}
					catch (Exception projectError6)
					{
						ProjectData.SetProjectError(projectError6);
						actionId = "";
						ProjectData.ClearProjectError();
					}
					if (string.IsNullOrWhiteSpace(actionId))
					{
						actionId = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "ActionId");
					}
					if (string.IsNullOrWhiteSpace(actionId))
					{
						actionId = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "ID");
					}
					title = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "Title");
					if (string.IsNullOrWhiteSpace(title))
					{
						title = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "Name");
					}
					object objectValue3 = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(objectValue), "Icon"));
					if (objectValue3 != null)
					{
						if (objectValue3 is string)
						{
							iconSpec = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue3));
						}
						else if (objectValue3 is Image)
						{
							string text11 = SaveIconImageToCache((Image)objectValue3, "img|" + actionId);
							if (!string.IsNullOrWhiteSpace(text11))
							{
								iconSpec = text11;
							}
						}
						else if (objectValue3 is byte[])
						{
							string text12 = SaveIconBytesToCache((byte[])objectValue3, "bin|" + actionId);
							if (!string.IsNullOrWhiteSpace(text12))
							{
								iconSpec = text12;
							}
						}
						else
						{
							try
							{
								iconSpec = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue3));
							}
							catch (Exception projectError7)
							{
								ProjectData.SetProjectError(projectError7);
								iconSpec = "";
								ProjectData.ClearProjectError();
							}
						}
					}
					if (string.IsNullOrWhiteSpace(iconSpec))
					{
						string text13 = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "IconString");
						if (!string.IsNullOrWhiteSpace(text13))
						{
							iconSpec = text13;
						}
					}
					if (!string.IsNullOrWhiteSpace(iconSpec))
					{
						iconSpec = iconSpec.Trim();
					}
					if (!string.IsNullOrWhiteSpace(actionId))
					{
						actionId = actionId.Trim();
					}
					if (string.IsNullOrWhiteSpace(title))
					{
						try
						{
							if (data.GetDataPresent(DataFormats.UnicodeText))
							{
								string text14 = FirstNonEmptyLine(data.GetData(DataFormats.UnicodeText) as string);
								if (!string.IsNullOrWhiteSpace(text14))
								{
									title = text14;
								}
							}
							else if (data.GetDataPresent(DataFormats.Text))
							{
								string text15 = FirstNonEmptyLine(data.GetData(DataFormats.Text) as string);
								if (!string.IsNullOrWhiteSpace(text15))
								{
									title = text15;
								}
							}
						}
						catch (Exception projectError8)
						{
							ProjectData.SetProjectError(projectError8);
							ProjectData.ClearProjectError();
						}
					}
					bool num2 = !string.IsNullOrWhiteSpace(actionId);
					if (num2)
					{
						LogDeg("TryExtract: ok actionId=" + actionId);
					}
					else
					{
						LogDeg("TryExtract: actionId empty after reflection");
					}
					result = num2;
				}
			}
		}
		return result;
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern ushort RegisterClipboardFormat(string lpszFormat);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GlobalLock(IntPtr hMem);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool GlobalUnlock(IntPtr hMem);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern UIntPtr GlobalSize(IntPtr hMem);

	[DllImport("ole32.dll")]
	private static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

	private static byte[] TryGetComRawBytes(System.Windows.Forms.IDataObject dataObj, string formatName)
	{
		byte[] result;
		checked
		{
			if (!(dataObj is System.Runtime.InteropServices.ComTypes.IDataObject dataObject))
			{
				LogDeg("COM: IDataObject cast failed");
				result = null;
			}
			else
			{
				ushort num;
				try
				{
					num = RegisterClipboardFormat(formatName);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					LogDeg("COM: RegisterClipboardFormat ex=" + ex2.Message);
					result = null;
					ProjectData.ClearProjectError();
					goto IL_0310;
				}
				if (num == 0)
				{
					LogDeg("COM: RegisterClipboardFormat=0");
					result = null;
				}
				else
				{
					short cfFormat = ((unchecked((uint)num) > 32767u) ? ((short)(num - 65536)) : ((short)num));
					FORMATETC format = new FORMATETC
					{
						cfFormat = cfFormat,
						dwAspect = DVASPECT.DVASPECT_CONTENT,
						lindex = -1,
						ptd = IntPtr.Zero,
						tymed = (TYMED.TYMED_HGLOBAL | TYMED.TYMED_ISTREAM | TYMED.TYMED_ISTORAGE)
					};
					STGMEDIUM medium = default(STGMEDIUM);
					try
					{
						dataObject.GetData(ref format, out medium);
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						LogDeg("COM: GetData failed: " + ex4.Message);
						result = null;
						ProjectData.ClearProjectError();
						goto IL_0310;
					}
					try
					{
						if (medium.tymed == TYMED.TYMED_HGLOBAL)
						{
							IntPtr unionmember = medium.unionmember;
							if (unionmember == IntPtr.Zero)
							{
								LogDeg("COM: TYMED_HGLOBAL unionmember null");
								result = null;
							}
							else
							{
								ulong num2;
								try
								{
									num2 = GlobalSize(unionmember).ToUInt64();
								}
								catch (Exception ex5)
								{
									ProjectData.SetProjectError(ex5);
									Exception ex6 = ex5;
									LogDeg("COM: GlobalSize ex=" + ex6.Message);
									result = null;
									ProjectData.ClearProjectError();
									goto end_IL_00ed;
								}
								if (num2 == 0L)
								{
									LogDeg("COM: GlobalSize=0");
									result = null;
								}
								else if (num2 > int.MaxValue)
								{
									LogDeg("COM: GlobalSize too large: " + Conversions.ToString(num2));
									result = null;
								}
								else
								{
									int num3 = (int)num2;
									IntPtr intPtr = GlobalLock(unionmember);
									if (intPtr == IntPtr.Zero)
									{
										LogDeg("COM: GlobalLock failed");
										result = null;
									}
									else
									{
										try
										{
											byte[] array = new byte[num3 - 1 + 1];
											Marshal.Copy(intPtr, array, 0, num3);
											result = array;
										}
										finally
										{
											GlobalUnlock(unionmember);
										}
									}
								}
							}
						}
						else if (medium.tymed == TYMED.TYMED_ISTREAM)
						{
							IntPtr unionmember2 = medium.unionmember;
							if (unionmember2 == IntPtr.Zero)
							{
								LogDeg("COM: TYMED_ISTREAM unionmember null");
								result = null;
							}
							else if (!(Marshal.GetObjectForIUnknown(unionmember2) is IStream stream))
							{
								LogDeg("COM: Marshal IStream failed");
								result = null;
							}
							else
							{
								using MemoryStream memoryStream = new MemoryStream();
								byte[] array2 = new byte[8192];
								do
								{
									int num4 = 0;
									stream.Read(array2, array2.Length, (IntPtr)num4);
									if (num4 <= 0)
									{
										break;
									}
									memoryStream.Write(array2, 0, num4);
								}
								while (memoryStream.Length <= 67108864);
								result = memoryStream.ToArray();
							}
						}
						else
						{
							LogDeg("COM: Unsupported TYMED=" + medium.tymed);
							result = null;
						}
						end_IL_00ed:;
					}
					catch (Exception ex7)
					{
						ProjectData.SetProjectError(ex7);
						Exception ex8 = ex7;
						LogDeg("COM: Exception: " + ex8.Message);
						result = null;
						ProjectData.ClearProjectError();
					}
					finally
					{
						try
						{
							ReleaseStgMedium(ref medium);
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
					}
				}
			}
			goto IL_0310;
		}
		IL_0310:
		return result;
	}

	private static bool TryExtractQuickerActionFromBytes(byte[] raw, ref string actionId, ref string title, ref string iconSpec)
	{
		actionId = "";
		title = "";
		iconSpec = "";
		if (raw == null || raw.Length == 0)
		{
			return false;
		}
		object obj = null;
		try
		{
			using (MemoryStream serializationStream = new MemoryStream(raw))
			{
				obj = RuntimeHelpers.GetObjectValue(new BinaryFormatter().Deserialize(serializationStream));
			}
			if (obj != null)
			{
				LogDeg("TryExtractBytes: BinaryFormatter ok type=" + obj.GetType().FullName);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			LogDeg("TryExtractBytes: BinaryFormatter ex=" + ex2.Message);
			obj = null;
			ProjectData.ClearProjectError();
		}
		if (obj != null && TryExtractQuickerActionFromObject(RuntimeHelpers.GetObjectValue(obj), ref actionId, ref title, ref iconSpec))
		{
			return true;
		}
		List<string> list = new List<string>();
		try
		{
			list.AddRange(ExtractAsciiStrings(raw, 6));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			list.AddRange(ExtractUtf16LeAsciiStrings(raw, 6));
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		list = (from s in list
			where !string.IsNullOrWhiteSpace(s)
			select s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		string text = "";
		try
		{
			text = string.Join(" ", list.ToArray());
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			text = "";
			ProjectData.ClearProjectError();
		}
		actionId = ExtractPossibleActionId(text);
		title = ExtractPossibleTitle(list);
		iconSpec = ExtractPossibleIconSpec(list);
		if (!string.IsNullOrWhiteSpace(title))
		{
			LogDeg("TryExtractBytes: title=" + title);
		}
		if (!string.IsNullOrWhiteSpace(iconSpec))
		{
			LogDeg("TryExtractBytes: icon=" + iconSpec);
		}
		if (!string.IsNullOrWhiteSpace(actionId))
		{
			LogDeg("TryExtractBytes: actionId=" + actionId);
			return true;
		}
		LogDeg("TryExtractBytes: actionId not found");
		return false;
	}

	private static bool TryExtractQuickerActionFromObject(object obj, ref string actionId, ref string title, ref string iconSpec)
	{
		actionId = "";
		title = "";
		iconSpec = "";
		if (obj == null)
		{
			return false;
		}
		object objectValue = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(obj), "Action"));
		if (objectValue == null)
		{
			objectValue = RuntimeHelpers.GetObjectValue(obj);
		}
		try
		{
			object objectValue2 = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(objectValue), "Id"));
			if (objectValue2 != null)
			{
				actionId = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue2));
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			actionId = "";
			ProjectData.ClearProjectError();
		}
		if (string.IsNullOrWhiteSpace(actionId))
		{
			actionId = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "ActionId");
		}
		if (string.IsNullOrWhiteSpace(actionId))
		{
			actionId = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "ID");
		}
		title = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "Title");
		if (string.IsNullOrWhiteSpace(title))
		{
			title = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "Name");
		}
		object objectValue3 = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(objectValue), "Icon"));
		if (objectValue3 != null)
		{
			if (objectValue3 is string)
			{
				iconSpec = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue3));
			}
			else if (objectValue3 is Image)
			{
				string text = SaveIconImageToCache((Image)objectValue3, "img|" + actionId);
				if (!string.IsNullOrWhiteSpace(text))
				{
					iconSpec = text;
				}
			}
			else if (objectValue3 is byte[])
			{
				string text2 = SaveIconBytesToCache((byte[])objectValue3, "bin|" + actionId);
				if (!string.IsNullOrWhiteSpace(text2))
				{
					iconSpec = text2;
				}
			}
			else
			{
				try
				{
					iconSpec = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue3));
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					iconSpec = "";
					ProjectData.ClearProjectError();
				}
			}
		}
		if (string.IsNullOrWhiteSpace(iconSpec))
		{
			string text3 = TryGetStringProperty(RuntimeHelpers.GetObjectValue(objectValue), "IconString");
			if (!string.IsNullOrWhiteSpace(text3))
			{
				iconSpec = text3;
			}
		}
		if (!string.IsNullOrWhiteSpace(iconSpec))
		{
			iconSpec = iconSpec.Trim();
		}
		if (!string.IsNullOrWhiteSpace(actionId))
		{
			actionId = actionId.Trim();
		}
		return !string.IsNullOrWhiteSpace(actionId);
	}

	private static string ExtractPossibleActionId(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return "";
		}
		try
		{
			Match match = Regex.Match(text, "(?i)\\b(?:actionguid|action_guid|actionid|action_id|actionId|id)\\s*[:=]\\s*\"?([0-9a-zA-Z\\-_]{6,})\"?");
			if (match.Success && match.Groups.Count > 1)
			{
				return match.Groups[1].Value;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			Match match2 = Regex.Match(text, "(?i)\\b[0-9a-f]{8}\\-[0-9a-f]{4}\\-[0-9a-f]{4}\\-[0-9a-f]{4}\\-[0-9a-f]{12}\\b");
			if (match2.Success)
			{
				return match2.Value;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static string ExtractPossibleTitle(List<string> texts)
	{
		if (texts == null || texts.Count == 0)
		{
			return "";
		}
		List<string> list = (from s in texts
			where !string.IsNullOrWhiteSpace(s)
			select s.Trim() into s
			where s.Length >= 2 && s.Length <= 50
			where !s.StartsWith("System.", StringComparison.OrdinalIgnoreCase)
			where !s.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !s.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
			where !s.StartsWith("fa:", StringComparison.OrdinalIgnoreCase)
			where !s.StartsWith("QCAD_", StringComparison.OrdinalIgnoreCase)
			where s.IndexOf("action", StringComparison.OrdinalIgnoreCase) < 0 && s.IndexOf("guid", StringComparison.OrdinalIgnoreCase) < 0 && s.IndexOf("actionid", StringComparison.OrdinalIgnoreCase) < 0
			where s.IndexOf('.') < 0
			select s).ToList();
		List<string> list2 = list.Where([SpecialName] (string s) => Regex.IsMatch(s, "[\\u4e00-\\u9fff]")).ToList();
		if (list2.Count > 0)
		{
			return list2.OrderByDescending([SpecialName] (string s) => s.Length).First();
		}
		List<string> list3 = list.Where([SpecialName] (string s) => Regex.IsMatch(s, "^[\\p{L}\\p{N}\\s\\-_·\\.\\(\\)\\[\\]\\{\\}]+$")).ToList();
		if (list3.Count > 0)
		{
			return list3.OrderByDescending([SpecialName] (string s) => s.Length).First();
		}
		if (list.Count > 0)
		{
			return list.OrderByDescending([SpecialName] (string s) => s.Length).First();
		}
		return "";
	}

	private static string ExtractPossibleIconSpec(List<string> texts)
	{
		if (texts == null || texts.Count == 0)
		{
			return "";
		}
		foreach (string text4 in texts)
		{
			if (string.IsNullOrWhiteSpace(text4))
			{
				continue;
			}
			string text = text4.Trim();
			if (text.StartsWith("fa:", StringComparison.OrdinalIgnoreCase))
			{
				return text;
			}
			if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = text.ToLowerInvariant();
				if (text2.Contains(".png") || text2.Contains(".jpg") || text2.Contains(".jpeg") || text2.Contains(".ico") || text2.Contains(".svg"))
				{
					return text;
				}
			}
		}
		string text3 = "";
		try
		{
			text3 = string.Join(" ", texts.ToArray());
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			text3 = "";
			ProjectData.ClearProjectError();
		}
		try
		{
			Match match = Regex.Match(text3, "(?i)\\b(?:icon|string|iconspec|iconpath)\\s*[:=]\\s*\"?(fa:[^\"\\s]+|https?://[^\"\\s]+)\"?");
			if (match.Success && match.Groups.Count > 1)
			{
				return match.Groups[1].Value;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static List<string> ExtractAsciiStrings(byte[] bytes, int minLen)
	{
		List<string> list = new List<string>();
		if (bytes == null || bytes.Length == 0)
		{
			return list;
		}
		StringBuilder stringBuilder = new StringBuilder();
		checked
		{
			int num = bytes.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				byte b = bytes[i];
				if (b >= 32 && b <= 126)
				{
					stringBuilder.Append(Strings.ChrW(b));
					continue;
				}
				if (stringBuilder.Length >= minLen)
				{
					list.Add(stringBuilder.ToString());
				}
				stringBuilder.Length = 0;
			}
			if (stringBuilder.Length >= minLen)
			{
				list.Add(stringBuilder.ToString());
			}
			return list;
		}
	}

	private static List<string> ExtractUtf16LeAsciiStrings(byte[] bytes, int minLen)
	{
		List<string> list = new List<string>();
		if (bytes == null || bytes.Length < 2)
		{
			return list;
		}
		StringBuilder stringBuilder = new StringBuilder();
		checked
		{
			for (int i = 0; i + 1 < bytes.Length; i += 2)
			{
				byte b = bytes[i];
				if (bytes[i + 1] == 0 && b >= 32 && b <= 126)
				{
					stringBuilder.Append(Strings.ChrW(b));
					continue;
				}
				if (stringBuilder.Length >= minLen)
				{
					list.Add(stringBuilder.ToString());
				}
				stringBuilder.Length = 0;
			}
			if (stringBuilder.Length >= minLen)
			{
				list.Add(stringBuilder.ToString());
			}
			return list;
		}
	}

	private static string TryGetStringProperty(object obj, string propName)
	{
		object objectValue = RuntimeHelpers.GetObjectValue(GetPropertyValue(RuntimeHelpers.GetObjectValue(obj), propName));
		string result;
		if (objectValue == null)
		{
			result = "";
		}
		else
		{
			try
			{
				result = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = "";
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private static object GetPropertyValue(object obj, string propName)
	{
		object result;
		if (obj == null || string.IsNullOrEmpty(propName))
		{
			result = null;
		}
		else
		{
			try
			{
				Type type = obj.GetType();
				BindingFlags bindingAttr = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
				PropertyInfo property = type.GetProperty(propName, bindingAttr);
				result = (((object)property != null) ? property.GetValue(RuntimeHelpers.GetObjectValue(obj), null) : type.GetField(propName, bindingAttr)?.GetValue(RuntimeHelpers.GetObjectValue(obj)));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = null;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private static string SaveIconImageToCache(Image img, string key)
	{
		if (img == null)
		{
			return "";
		}
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UniversalRadialMenu", "IconCache");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = Path.Combine(text, HashToHex("img|" + key) + ".png");
			using (Bitmap bitmap = new Bitmap(img))
			{
				bitmap.Save(text2, ImageFormat.Png);
			}
			if (File.Exists(text2))
			{
				return text2;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static string SaveIconBytesToCache(byte[] bytes, string key)
	{
		if (bytes == null || bytes.Length == 0)
		{
			return "";
		}
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UniversalRadialMenu", "IconCache");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = Path.Combine(text, HashToHex("bin|" + key) + ".png");
			File.WriteAllBytes(text2, bytes);
			if (File.Exists(text2))
			{
				return text2;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private static string HashToHex(string value)
	{
		if (value == null)
		{
			value = "";
		}
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		using SHA1Managed sHA1Managed = new SHA1Managed();
		byte[] array = sHA1Managed.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder(checked(array.Length * 2));
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	private static void LogDeg(string message)
	{
		Logger.Log(message);
	}

	private void renderPanel_DoubleClick(object sender, MouseEventArgs e)
	{
		if (e == null)
		{
			return;
		}
		HitInfo sectorAt = GetSectorAt(e.Location);
		if (sectorAt.Index < 0)
		{
			return;
		}
		MenuItemConfig menuItemConfig = GetItemsForLevel(sectorAt.Level).FirstOrDefault([SpecialName] (MenuItemConfig x) => x.SectorIndex == sectorAt.Index);
		if (menuItemConfig != null)
		{
			if (string.Equals(menuItemConfig.Command, "_BACK", StringComparison.OrdinalIgnoreCase))
			{
				GoBack(null, EventArgs.Empty);
			}
			else
			{
				EditItem(menuItemConfig);
			}
		}
		else
		{
			CreateNewItemAt(sectorAt);
		}
	}

	private void EnterSubMenu(string subMenuName)
	{
		string text = (subMenuName ?? "").Trim();
		if (text.Length != 0)
		{
			currentParentMenuName = text;
			EnsureBackItemExists(currentParentMenuName);
			currentPage = 0;
			RecalculatePages();
			if (renderPanel != null)
			{
				renderPanel.Invalidate();
			}
		}
	}

	private MenuItemConfig CreateBackItem(string parentMenuName, int level, int page, int sectorIndex)
	{
		return new MenuItemConfig
		{
			Text = "<- 返回",
			DisplayName = "",
			Command = "_BACK",
			ColorArgb = Color.DarkGray.ToArgb(),
			Enabled = true,
			Level = level,
			IconPath = "",
			QuickerMenuItems = null,
			IsSubMenu = false,
			SubMenuName = "",
			ParentMenuName = parentMenuName,
			FontSize = sizeConfig.FontSize,
			Page = page,
			SectorIndex = sectorIndex,
			OperationType = "",
			Shortcut = "",
			TypedText = "",
			PasteText = "",
			RunPath = "",
			RunParams = "",
			UseFirstCharIcon = false
		};
	}

	private void EnsureBackItemExists(string menuName)
	{
		_Closure_0024__183_002D0 arg = default(_Closure_0024__183_002D0);
		_Closure_0024__183_002D0 CS_0024_003C_003E8__locals12 = new _Closure_0024__183_002D0(arg);
		CS_0024_003C_003E8__locals12._0024VB_0024Local_mn = (menuName ?? "").Trim();
		if (CS_0024_003C_003E8__locals12._0024VB_0024Local_mn.Length == 0 || string.Equals(CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, "ROOT", StringComparison.OrdinalIgnoreCase) || configs == null || configs.Any([SpecialName] (MenuItemConfig c) => c != null && string.Equals(c.ParentMenuName, CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase) && string.Equals(c.Command, "_BACK", StringComparison.OrdinalIgnoreCase)))
		{
			return;
		}
		List<MenuItemConfig> source = configs.Where([SpecialName] (MenuItemConfig c) => c != null && c.Level == 0 && c.Page == 0 && string.Equals(c.ParentMenuName, CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase)).ToList();
		HashSet<int> hashSet = new HashSet<int>(source.Select([SpecialName] (MenuItemConfig c) => c.SectorIndex));
		int num = 0;
		checked
		{
			do
			{
				if (!hashSet.Contains(num))
				{
					configs.Add(CreateBackItem(CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, 0, 0, num));
					return;
				}
				num++;
			}
			while (num <= 7);
			List<MenuItemConfig> source2 = configs.Where([SpecialName] (MenuItemConfig c) => c != null && c.Level == 1 && c.Page == 0 && string.Equals(c.ParentMenuName, CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase)).ToList();
			HashSet<int> hashSet2 = new HashSet<int>(source2.Select([SpecialName] (MenuItemConfig c) => c.SectorIndex));
			int num2 = 0;
			do
			{
				if (!hashSet2.Contains(num2))
				{
					configs.Add(CreateBackItem(CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, 1, 0, num2));
					return;
				}
				num2++;
			}
			while (num2 <= 15);
			List<MenuItemConfig> source3 = configs.Where([SpecialName] (MenuItemConfig c) => c != null && c.Level == 2 && string.Equals(c.ParentMenuName, CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase)).ToList();
			HashSet<int> hashSet3 = new HashSet<int>(source3.Select([SpecialName] (MenuItemConfig c) => c.SectorIndex));
			int num3 = 0;
			do
			{
				if (!hashSet3.Contains(num3))
				{
					MenuItemConfig item = CreateBackItem(CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, 2, 0, num3);
					ApplyOuterRingDefaultColor(item);
					configs.Add(item);
					return;
				}
				num3++;
			}
			while (num3 <= 7);
			if (string.IsNullOrEmpty(currentProcessName))
			{
				return;
			}
			MenuItemConfig menuItemConfig = source.OrderByDescending([SpecialName] (MenuItemConfig c) => c.SectorIndex).FirstOrDefault();
			if (menuItemConfig == null)
			{
				return;
			}
			int sectorIndex = menuItemConfig.SectorIndex;
			int num4 = 0;
			try
			{
				num4 = Math.Max(0, configs.Where([SpecialName] (MenuItemConfig c) => c != null && c.Level == 0 && string.Equals(c.ParentMenuName, CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, StringComparison.OrdinalIgnoreCase)).Max([SpecialName] (MenuItemConfig c) => c.Page));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				num4 = 0;
				ProjectData.ClearProjectError();
			}
			menuItemConfig.Page = num4 + 1;
			configs.Add(CreateBackItem(CS_0024_003C_003E8__locals12._0024VB_0024Local_mn, 0, 0, sectorIndex));
		}
	}

	private void CreateNewItemAt(HitInfo hit)
	{
		MenuItemConfig item = new MenuItemConfig
		{
			Text = "新按钮",
			Command = "",
			ColorArgb = ((hit.Level == 2) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White).ToArgb(),
			Enabled = true,
			Level = hit.Level,
			ParentMenuName = currentParentMenuName,
			Page = ((hit.Level != 2) ? currentPage : 0),
			SectorIndex = hit.Index,
			OperationType = "打开或运行（文件/目录/命令/网址）",
			FontSize = sizeConfig.FontSize
		};
		if (ShowEditItemDialog(item))
		{
			ApplyOuterRingDefaultColor(item);
			configs.Add(item);
			RadialMenuConfigStore.NormalizeMenuItems(configs);
			RecalculatePages();
			renderPanel.Invalidate();
		}
	}

	private void GoBack(object sender, EventArgs e)
	{
		if (Operators.CompareString(currentParentMenuName, "ROOT", TextCompare: false) != 0)
		{
			MenuItemConfig menuItemConfig = configs.FirstOrDefault([SpecialName] (MenuItemConfig c) => c != null && c.IsSubMenu && string.Equals(c.SubMenuName, currentParentMenuName, StringComparison.OrdinalIgnoreCase));
			currentParentMenuName = ((menuItemConfig != null && !string.IsNullOrEmpty(menuItemConfig.ParentMenuName)) ? menuItemConfig.ParentMenuName : "ROOT");
			currentPage = 0;
			RecalculatePages();
		}
	}

	private void AddNewItem(object sender, EventArgs e)
	{
		MenuItemConfig item = new MenuItemConfig
		{
			Text = "新按钮",
			Command = "",
			ColorArgb = Color.White.ToArgb(),
			Enabled = true,
			Level = 0,
			ParentMenuName = currentParentMenuName,
			Page = currentPage,
			SectorIndex = 0,
			OperationType = "打开或运行（文件/目录/命令/网址）",
			FontSize = sizeConfig.FontSize
		};
		if (ShowEditItemDialog(item))
		{
			configs.Add(item);
			RecalculatePages();
		}
	}

	private void EditItem(MenuItemConfig item)
	{
		if (ShowEditItemDialog(item))
		{
			RecalculatePages();
		}
	}

	private bool ShowEditItemDialog(MenuItemConfig item)
	{
		using EditItemForm editItemForm = new EditItemForm(item);
		return editItemForm.ShowDialog() == DialogResult.OK;
	}

	private void DeleteItem(MenuItemConfig item)
	{
		if (item != null && !string.Equals(item.Command, "_BACK", StringComparison.OrdinalIgnoreCase))
		{
			configs.Remove(item);
			RadialMenuConfigStore.NormalizeMenuItems(configs);
			RecalculatePages();
		}
	}

	private void SaveConfig(object sender, EventArgs e)
	{
		appConfig.MenuSize = sizeConfig;
		if (chkUseCadInfiniteRadialMenu != null)
		{
			appConfig.UseCadInfiniteRadialMenu = chkUseCadInfiniteRadialMenu.Checked;
		}
		ApplyRootFontSizeToAllScopes(sizeConfig.FontSize);
		try
		{
			RadialMenuController.UpsertMenuSizeDpiProfileFromBase(sizeConfig, base.DeviceDpi);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		RemoveEmptyProcessMenus(appConfig);
		RadialMenuConfigStore.NormalizeConfig(appConfig);
		store.Save(appConfig);
		ConfigSaved?.Invoke();
		Close();
	}

	private static void RemoveEmptyProcessMenus(RadialMenuAppConfig config)
	{
		if (config == null || config.ProcessMenus == null)
		{
			return;
		}
		config.ProcessMenus = config.ProcessMenus.Where([SpecialName] (ProcessMenuConfig pm) =>
		{
			if (pm == null)
			{
				return false;
			}
			return !string.IsNullOrWhiteSpace(pm.ProcessName) && RadialMenuController.ProcessMenuHasUserContent(pm.MenuItems);
		}).ToList();
	}

	private HitInfo GetSectorAt(Point pt)
	{
		Point point = new Point(renderPanel.Width / 2, renderPanel.Height / 2);
		checked
		{
			int num = pt.X - point.X;
			int num2 = pt.Y - point.Y;
			double num3 = Math.Sqrt(num * num + num2 * num2);
			if (num3 < 10.0)
			{
				return new HitInfo
				{
					Index = -1,
					Level = -1
				};
			}
			int num4 = Math.Min(renderPanel.Width, renderPanel.Height);
			int num5 = sizeConfig.OuterRadius * 2;
			double num6 = (double)num4 / (double)num5 * 0.98;
			int num7 = (int)Math.Round((double)sizeConfig.InnerRadius * num6 * 0.75);
			int num8 = (int)Math.Round((double)sizeConfig.OuterRadius * num6);
			int num9 = 3;
			double num10 = (double)(num8 - num7) / (double)num9;
			int val = (int)Math.Floor((num3 - (double)num7) / num10);
			val = Math.Max(0, Math.Min(2, val));
			double num11 = ((Math.Atan2(num2, num) * 180.0 / Math.PI + 360.0) % 360.0 - 22.5 + 360.0) % 360.0;
			int num12 = ((val == 1) ? 16 : 8);
			int num13 = (int)Math.Floor(num11 / (360.0 / (double)num12));
			if (num13 < 0)
			{
				num13 = 0;
			}
			if (num13 >= num12)
			{
				num13 = 0;
			}
			return new HitInfo
			{
				Index = num13,
				Level = val
			};
		}
	}

	private static QuickerActionMetadata LookupQuickerMetadataIfLoaded(string actionId)
	{
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return null;
		}
		if (!quickerMetadataLoaded)
		{
			return null;
		}
		string key = actionId.Trim();
		QuickerActionMetadata value = null;
		try
		{
			object obj = quickerMetadataLoadLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(obj, ref lockTaken);
				if (!quickerMetadataLoaded)
				{
					return null;
				}
				if (quickerMetadataIndex.TryGetValue(key, out value) && value != null)
				{
					return value;
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(obj);
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return null;
	}

	private static void EnsureQuickerMetadataLoaded()
	{
		if (quickerMetadataLoaded)
		{
			return;
		}
		object obj = quickerMetadataLoadLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(obj);
		bool lockTaken = false;
		checked
		{
			try
			{
				Monitor.Enter(obj, ref lockTaken);
				if (quickerMetadataLoaded)
				{
					return;
				}
				quickerMetadataIndex.Clear();
				try
				{
					int num = 0;
					int num2 = 0;
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					List<string> source = new List<string>
					{
						Path.Combine(folderPath, "Quicker"),
						Path.Combine(folderPath2, "Quicker")
					};
					List<string> list = new List<string>();
					foreach (string item in source.Distinct(StringComparer.OrdinalIgnoreCase))
					{
						try
						{
							if (string.IsNullOrWhiteSpace(item) || !Directory.Exists(item))
							{
								continue;
							}
							string path = Path.Combine(item, "states");
							string text = Path.Combine(item, "data");
							if (Directory.Exists(path))
							{
								list.AddRange(Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly));
							}
							if (Directory.Exists(text))
							{
								list.AddRange(Directory.GetFiles(text, "*.json", SearchOption.TopDirectoryOnly));
								string path2 = Path.Combine(text, "profiles");
								if (Directory.Exists(path2))
								{
									list.AddRange(Directory.GetFiles(path2, "*.json", SearchOption.AllDirectories));
								}
							}
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
					}
					foreach (string item2 in list.Distinct(StringComparer.OrdinalIgnoreCase))
					{
						try
						{
							FileInfo fileInfo = new FileInfo(item2);
							if (fileInfo.Exists && fileInfo.Length > 0 && fileInfo.Length <= 52428800)
							{
								num++;
								IndexQuickerJsonFileForMetadata(item2);
								num2++;
							}
						}
						catch (Exception projectError2)
						{
							ProjectData.SetProjectError(projectError2);
							ProjectData.ClearProjectError();
						}
					}
					AppendQuickerImportLog("Quicker metadata loaded. files=" + num + ", indexed=" + num2 + ", entries=" + quickerMetadataIndex.Count);
				}
				catch (Exception projectError3)
				{
					ProjectData.SetProjectError(projectError3);
					ProjectData.ClearProjectError();
				}
				quickerMetadataLoaded = true;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(obj);
				}
			}
		}
	}

	private static void IndexQuickerJsonFileForMetadata(string filePath)
	{
		try
		{
			string text = File.ReadAllText(filePath);
			if (!string.IsNullOrWhiteSpace(text))
			{
				CollectQuickerMetadata(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(new JavaScriptSerializer
				{
					MaxJsonLength = int.MaxValue
				}.DeserializeObject(text))), Path.GetFileName(filePath));
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private static void CollectQuickerMetadata(object node, string source)
	{
		if (node == null)
		{
			return;
		}
		if (node is IDictionary<string, object> dictionary)
		{
			string text = TryGetFirstGuidString(dictionary, new string[8] { "Id", "ID", "ActionId", "ActionID", "Guid", "GUID", "TargetId", "TargetID" });
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = TryGetFirstString(dictionary, new string[3] { "Title", "Name", "DisplayName" });
				string value = NormalizeIconString(TryGetFirstString(dictionary, new string[4] { "Icon", "IconUrl", "IconURL", "IconText" }));
				List<QuickerRightClickMenuItemConfig> list = null;
				try
				{
					list = TryExtractQuickerMenuItemsFromWheelActionData(dictionary, text);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					list = null;
					ProjectData.ClearProjectError();
				}
				if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(value) || (list != null && list.Count > 0))
				{
					QuickerActionMetadata value2 = null;
					if (!quickerMetadataIndex.TryGetValue(text, out value2) || value2 == null)
					{
						quickerMetadataIndex[text] = new QuickerActionMetadata
						{
							Title = text2,
							Icon = value,
							Source = source,
							QuickerMenuItems = ((list != null && list.Count > 0) ? list : null)
						};
					}
					else
					{
						bool flag = false;
						if (string.IsNullOrEmpty(value2.Title) && !string.IsNullOrEmpty(text2))
						{
							value2.Title = text2;
							flag = true;
						}
						if (string.IsNullOrEmpty(value2.Icon) && !string.IsNullOrEmpty(value))
						{
							value2.Icon = value;
							flag = true;
						}
						if (string.IsNullOrEmpty(value2.Source) && !string.IsNullOrEmpty(source))
						{
							value2.Source = source;
							flag = true;
						}
						if ((value2.QuickerMenuItems == null || value2.QuickerMenuItems.Count == 0) && list != null && list.Count > 0)
						{
							value2.QuickerMenuItems = list;
							flag = true;
						}
						if (flag)
						{
							quickerMetadataIndex[text] = value2;
						}
					}
				}
			}
			{
				foreach (KeyValuePair<string, object> item in dictionary)
				{
					CollectQuickerMetadata(RuntimeHelpers.GetObjectValue(item.Value), source);
				}
				return;
			}
		}
		if (node is string || !(node is IEnumerable enumerable))
		{
			return;
		}
		foreach (object item2 in enumerable)
		{
			CollectQuickerMetadata(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(item2)), source);
		}
	}

	private static string TryGetFirstString(IDictionary<string, object> dict, IEnumerable<string> keys)
	{
		foreach (string key in keys)
		{
			foreach (string key2 in dict.Keys)
			{
				if (string.Equals(key2, key, StringComparison.OrdinalIgnoreCase))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(dict[key2]);
					if (objectValue == null)
					{
						return "";
					}
					string text = Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue));
					if (text == null)
					{
						text = "";
					}
					return text;
				}
			}
		}
		return "";
	}

	private static string TryGetFirstGuidString(IDictionary<string, object> dict, IEnumerable<string> keys)
	{
		string text = TryGetFirstString(dict, keys);
		if (string.IsNullOrWhiteSpace(text))
		{
			return "";
		}
		text = text.Trim();
		if (Guid.TryParse(text, out var result))
		{
			return result.ToString();
		}
		return "";
	}

	internal static string NormalizeIconString(string raw)
	{
		if (raw == null)
		{
			return "";
		}
		string text = raw.Trim();
		if (text.Length == 0)
		{
			return "";
		}
		checked
		{
			if (text.StartsWith("[url:", StringComparison.OrdinalIgnoreCase) && text.EndsWith("]"))
			{
				text = text.Substring(5, text.Length - 6).Trim();
			}
			else if (text.StartsWith("[icon:", StringComparison.OrdinalIgnoreCase) && text.EndsWith("]"))
			{
				text = text.Substring(6, text.Length - 7).Trim();
			}
			if (text.IndexOf("\\/", StringComparison.Ordinal) >= 0)
			{
				text = text.Replace("\\/", "/");
			}
			while (text.Length >= 2)
			{
				char c = text[0];
				char c2 = text[text.Length - 1];
				if ((c != '`' || c2 != '`') && (c != '"' || c2 != '"') && (c != '\'' || c2 != '\''))
				{
					break;
				}
				text = text.Substring(1, text.Length - 2).Trim();
			}
			return text;
		}
	}

	private static void AppendQuickerImportLog(string message)
	{
	}

	private static bool LooksLikeIconSpec(string s)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			return false;
		}
		string text = s.Trim();
		if (text.Length == 0)
		{
			return false;
		}
		if (text.StartsWith("fa:", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (text.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (text.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
		{
			if (text.IndexOf("/_icons/", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return true;
			}
			if (Regex.IsMatch(text, "\\.(png|jpg|jpeg|gif|ico)(\\?.*)?$", RegexOptions.IgnoreCase))
			{
				return true;
			}
		}
		if (Regex.IsMatch(text, "^[a-zA-Z]{2,6}:", RegexOptions.IgnoreCase))
		{
			return true;
		}
		return false;
	}

	private static bool IsLikelyRenderableIconSpec(string s)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			return false;
		}
		string text = s.Trim();
		if (text.Length == 0)
		{
			return false;
		}
		if (LooksLikeIconSpec(text))
		{
			return true;
		}
		try
		{
			if (text.StartsWith("\\\\", StringComparison.Ordinal))
			{
				return true;
			}
			if (Path.IsPathRooted(text))
			{
				return true;
			}
			if (File.Exists(text))
			{
				return true;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		return false;
	}

	private static bool TryLoadClipboardDemoSubprogramResult(string actionId, ref string title, ref string icon, ref string rightClickMenuText)
	{
		title = title ?? "";
		icon = icon ?? "";
		rightClickMenuText = rightClickMenuText ?? "";
		try
		{
			if (string.IsNullOrWhiteSpace(actionId))
			{
				return false;
			}
			if (!Guid.TryParse(actionId.Trim(), out var result))
			{
				return false;
			}
			string text = Path.Combine(Path.Combine(Path.GetTempPath(), "QuickerRadialMenuClipboardDemo", "subprogram_result"), result.ToString() + ".out.txt");
			if (!File.Exists(text))
			{
				return false;
			}
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string[] array = File.ReadAllLines(text, Encoding.UTF8);
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text5 in array2)
			{
				if (text5 == null)
				{
					continue;
				}
				string text6 = text5.Trim();
				if (text6.Length == 0)
				{
					continue;
				}
				if (text2.Length == 0 && text6.StartsWith("Title=", StringComparison.OrdinalIgnoreCase))
				{
					text2 = text6.Substring(6).Trim();
				}
				else if (text3.Length == 0 && text6.StartsWith("Icon=", StringComparison.OrdinalIgnoreCase))
				{
					text3 = NormalizeIconString(text6.Substring(5).Trim());
				}
				else if (text6.StartsWith("右键菜单=", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					string text7 = text6.Substring(5).Trim();
					if (text7.Length > 0)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.Append(text7);
					}
				}
				else if (flag && !text6.StartsWith("Title=", StringComparison.OrdinalIgnoreCase) && !text6.StartsWith("Icon=", StringComparison.OrdinalIgnoreCase))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(text6);
				}
			}
			text4 = stringBuilder.ToString();
			title = text2;
			if (text3.Length > 0 && LooksLikeIconSpec(text3))
			{
				icon = text3;
			}
			rightClickMenuText = text4;
			if (title.Length > 0 || icon.Length > 0 || rightClickMenuText.Length > 0)
			{
				AppendQuickerImportLog("ClipboardDemo out.txt loaded. actionId=" + result.ToString() + ", file=" + text + ", titleLen=" + title.Length + ", iconLen=" + icon.Length + ", menuLen=" + rightClickMenuText.Length);
				return true;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			AppendQuickerImportLog("TryLoadClipboardDemoSubprogramResult failed: " + ex2.Message);
			ProjectData.ClearProjectError();
		}
		return false;
	}

	private static string GetClipboardDemoOutFilePath(string actionId)
	{
		string result;
		try
		{
			result = (string.IsNullOrWhiteSpace(actionId) ? "" : (Guid.TryParse(actionId.Trim(), out var result2) ? Path.Combine(Path.Combine(Path.GetTempPath(), "QuickerRadialMenuClipboardDemo", "subprogram_result"), result2.ToString() + ".out.txt") : ""));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = "";
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private static void TryTriggerClipboardDemoSubprogram(string actionId)
	{
		try
		{
			if (!string.IsNullOrWhiteSpace(actionId) && Guid.TryParse(actionId.Trim(), out var result))
			{
				string fileName = "quicker:runaction:无限轮盘?sp=动作图标&targetId=" + result.ToString();
				Process.Start(new ProcessStartInfo
				{
					FileName = fileName,
					UseShellExecute = true
				});
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private static async Task<bool> WaitForFileCreatedAsync(string path, int timeoutMs)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			return false;
		}
		int tickCount = Environment.TickCount;
		while (true)
		{
			try
			{
				if (File.Exists(path))
				{
					return true;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			if (checked(Environment.TickCount - tickCount) >= timeoutMs)
			{
				break;
			}
			try
			{
				await Task.Delay(60).ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
				break;
			}
		}
		try
		{
			return File.Exists(path);
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			bool result = false;
			ProjectData.ClearProjectError();
			return result;
		}
	}

	private static async Task<bool> WaitForFileReadyAsync(string path, int timeoutMs)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			return false;
		}
		int tickCount = Environment.TickCount;
		while (true)
		{
			try
			{
				if (File.Exists(path))
				{
					using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					if (fileStream.Length > 0)
					{
						return true;
					}
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			if (checked(Environment.TickCount - tickCount) >= timeoutMs)
			{
				break;
			}
			try
			{
				await Task.Delay(60).ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
				break;
			}
		}
		try
		{
			if (!File.Exists(path))
			{
				return false;
			}
			using FileStream fileStream2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return fileStream2.Length > 0;
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			bool result = false;
			ProjectData.ClearProjectError();
			return result;
		}
	}

	private static string TryFindIconDeep(object node, ref string source, int depth = 0)
	{
		source = source ?? "";
		if (node == null)
		{
			return "";
		}
		if (depth > 8)
		{
			return "";
		}
		checked
		{
			if (node is IDictionary<string, object> dictionary)
			{
				string[] array = new string[10] { "Icon", "IconUrl", "IconURL", "IconText", "IconPath", "IconFile", "IconFilePath", "Image", "ImageUrl", "ImageURL" };
				foreach (string b in array)
				{
					foreach (string key in dictionary.Keys)
					{
						if (string.Equals(key, b, StringComparison.OrdinalIgnoreCase))
						{
							string text = NormalizeIconString(Convert.ToString(RuntimeHelpers.GetObjectValue(dictionary[key])));
							if (LooksLikeIconSpec(text))
							{
								source = "Key:" + key;
								return text;
							}
						}
					}
				}
				foreach (KeyValuePair<string, object> item in dictionary)
				{
					if (item.Key != null && item.Key.IndexOf("icon", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						string text2 = NormalizeIconString(Convert.ToString(RuntimeHelpers.GetObjectValue(item.Value)));
						if (LooksLikeIconSpec(text2))
						{
							source = "KeyContains:" + item.Key;
							return text2;
						}
					}
				}
				foreach (KeyValuePair<string, object> item2 in dictionary)
				{
					string source2 = "";
					string text3 = TryFindIconDeep(RuntimeHelpers.GetObjectValue(item2.Value), ref source2, depth + 1);
					if (!string.IsNullOrWhiteSpace(text3))
					{
						source = item2.Key + "->" + source2;
						return text3;
					}
				}
				return "";
			}
			if (node is string)
			{
				string text4 = NormalizeIconString((string)node);
				if (LooksLikeIconSpec(text4))
				{
					source = "String";
					return text4;
				}
				return "";
			}
			if (node is IEnumerable enumerable)
			{
				int num = 0;
				foreach (object item3 in enumerable)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(item3);
					string source3 = "";
					string text5 = TryFindIconDeep(RuntimeHelpers.GetObjectValue(objectValue), ref source3, depth + 1);
					if (!string.IsNullOrWhiteSpace(text5))
					{
						source = "Item" + num + "->" + source3;
						return text5;
					}
					num++;
					if (num > 200)
					{
						break;
					}
				}
			}
			return "";
		}
	}

	private static QuickerActionMetadata LookupQuickerMetadata(string actionId)
	{
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return null;
		}
		EnsureQuickerMetadataLoaded();
		string key = actionId.Trim();
		QuickerActionMetadata value = null;
		if (quickerMetadataIndex.TryGetValue(key, out value) && value != null)
		{
			return value;
		}
		return null;
	}

	internal static string GetQuickerIconSpec(string actionId)
	{
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return "";
		}
		QuickerActionMetadata quickerActionMetadata = LookupQuickerMetadata(actionId);
		if (quickerActionMetadata == null || string.IsNullOrWhiteSpace(quickerActionMetadata.Icon))
		{
			return "";
		}
		return NormalizeIconString(quickerActionMetadata.Icon);
	}

	private void ImportQuickerWheelFromClipboard()
	{
		try
		{
			_Closure_0024__217_002D0 arg = default(_Closure_0024__217_002D0);
			_Closure_0024__217_002D0 CS_0024_003C_003E8__locals11 = new _Closure_0024__217_002D0(arg);
			CS_0024_003C_003E8__locals11._0024VB_0024Me = this;
			System.Windows.Forms.IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
			{
				MessageBox.Show("剪贴板为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			string[] array = SafeGetFormats(dataObject);
			foreach (string text2 in array)
			{
				if (string.Equals(text2, "quicker-circle-menu-config", StringComparison.OrdinalIgnoreCase))
				{
					text = text2;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("剪贴板中没有检测到 Quicker 轮盘配置（quicker-circle-menu-config）。请在 Quicker 中复制轮盘后再导入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text3 = RuntimeHelpers.GetObjectValue(dataObject.GetData(text)) as string;
			if (string.IsNullOrWhiteSpace(text3))
			{
				MessageBox.Show("Quicker 轮盘配置内容为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			AppendQuickerImportLog("ImportQuickerWheelFromClipboard json length=" + text3.Length);
			CS_0024_003C_003E8__locals11._0024VB_0024Local_batchId = Interlocked.Increment(ref quickerImportBatchId);
			if (lblStatus != null)
			{
				lblStatus.Text = "正在导入 Quicker 轮盘…";
			}
			if (btnImportQuickerWheel != null)
			{
				btnImportQuickerWheel.Enabled = false;
			}
			CS_0024_003C_003E8__locals11._0024VB_0024Local_jsonCopy = text3;
			Task.Run([SpecialName] () => ParseQuickerCircleMenuConfig(CS_0024_003C_003E8__locals11._0024VB_0024Local_jsonCopy)).ContinueWith([SpecialName] (Task<List<QuickerWheelParsedItem>> t) =>
			{
				_Closure_0024__217_002D1 arg2 = default(_Closure_0024__217_002D1);
				_Closure_0024__217_002D1 CS_0024_003C_003E8__locals39 = new _Closure_0024__217_002D1(arg2);
				CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals11;
				CS_0024_003C_003E8__locals39._0024VB_0024Local_t = t;
				if (CS_0024_003C_003E8__locals11._0024VB_0024Me != null && !CS_0024_003C_003E8__locals11._0024VB_0024Me.IsDisposed)
				{
					try
					{
						CS_0024_003C_003E8__locals11._0024VB_0024Me.BeginInvoke((Action)checked([SpecialName] () =>
						{
							if (CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel != null)
							{
								CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.btnImportQuickerWheel.Enabled = true;
							}
							if (CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId == CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.quickerImportBatchId)
							{
								if (CS_0024_003C_003E8__locals39._0024VB_0024Local_t != null && !CS_0024_003C_003E8__locals39._0024VB_0024Local_t.IsFaulted && CS_0024_003C_003E8__locals39._0024VB_0024Local_t.Result != null && CS_0024_003C_003E8__locals39._0024VB_0024Local_t.Result.Count != 0)
								{
									List<QuickerWheelParsedItem> result = CS_0024_003C_003E8__locals39._0024VB_0024Local_t.Result;
									AppendQuickerImportLog("ImportQuickerWheelFromClipboard parsed count=" + result.Count);
									int num = 0;
									int num2 = 0;
									HitInfo hit = default(HitInfo);
									foreach (QuickerWheelParsedItem item in result)
									{
										int result2;
										if (item == null)
										{
											num2++;
											AppendQuickerImportLog("Skip item because parsed entry is Nothing");
										}
										else if (!int.TryParse((item.RawPositionKey ?? "").Trim(), out result2))
										{
											num2++;
											AppendQuickerImportLog("Skip item because RawPositionKey is invalid: \"" + item.RawPositionKey + "\"");
										}
										else if (!TryMapQuickerWheelPositionToHit(result2, ref hit))
										{
											num2++;
											AppendQuickerImportLog("Skip item because position mapping failed. keyInt=" + result2 + ", rawKey=\"" + item.RawPositionKey + "\"");
										}
										else
										{
											MenuItemConfig orCreateItemAt = CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.GetOrCreateItemAt(hit, Point.Empty);
											if (orCreateItemAt == null)
											{
												num2++;
												AppendQuickerImportLog("Skip item because target MenuItemConfig is Nothing. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index);
											}
											else
											{
												string text4 = (orCreateItemAt.IconPath ?? "").Trim();
												CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyParsedQuickerWheelItemToMenuItem(item, orCreateItemAt);
												CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.ApplyOuterRingDefaultColor(orCreateItemAt);
												AppendQuickerImportLog("Applied item. keyInt=" + result2 + ", level=" + hit.Level + ", index=" + hit.Index + ", title=\"" + item.Title + "\", actionName=\"" + item.ActionName + "\", iconBefore=\"" + text4 + "\", iconAfter=\"" + orCreateItemAt.IconPath + "\"");
												if (!string.Equals(text4, (orCreateItemAt.IconPath ?? "").Trim(), StringComparison.OrdinalIgnoreCase) && text4.Length > 0 && CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.ContainsKey(text4))
												{
													try
													{
														if (CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text4] != null)
														{
															CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache[text4].Dispose();
														}
													}
													catch (Exception projectError2)
													{
														ProjectData.SetProjectError(projectError2);
														ProjectData.ClearProjectError();
													}
													CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.previewIconCache.Remove(text4);
												}
												num++;
											}
										}
									}
									RadialMenuConfigStore.NormalizeMenuItems(CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.configs);
									CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RecalculatePages();
									CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.UpdateStatus();
									CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.renderPanel.Invalidate();
									if (CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
									{
										CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "导入完成（" + num + "项）";
									}
									try
									{
										CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerClipboardDemoUpgradeForImportedItems(CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
									}
									catch (Exception projectError3)
									{
										ProjectData.SetProjectError(projectError3);
										ProjectData.ClearProjectError();
									}
									try
									{
										CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.StartQuickerMenuUpgradeForImportedItems(CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_batchId, result);
										return;
									}
									catch (Exception projectError4)
									{
										ProjectData.SetProjectError(projectError4);
										ProjectData.ClearProjectError();
										return;
									}
								}
								if (CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus != null)
								{
									CS_0024_003C_003E8__locals39._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.lblStatus.Text = "";
								}
								MessageBox.Show("解析失败或轮盘为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}));
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
				}
			});
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			AppendQuickerImportLog("ImportQuickerWheelFromClipboard failed: " + ex2.ToString());
			MessageBox.Show("导入失败：" + ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			ProjectData.ClearProjectError();
		}
	}

	private static bool HasCustomQuickerMenuItems(List<QuickerRightClickMenuItemConfig> items)
	{
		bool result;
		if (items == null || items.Count == 0)
		{
			result = false;
		}
		else
		{
			try
			{
				result = items.Any([SpecialName] (QuickerRightClickMenuItemConfig x) => x != null && !string.Equals(x.DisplayText ?? "", "编辑", StringComparison.OrdinalIgnoreCase) && !string.Equals(x.DisplayText ?? "", "调试", StringComparison.OrdinalIgnoreCase));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = false;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private void StartQuickerClipboardDemoUpgradeForImportedItems(int batchId, List<QuickerWheelParsedItem> parsed)
	{
		_Closure_0024__219_002D0 closure_0024__219_002D = new _Closure_0024__219_002D0(null);
		closure_0024__219_002D._0024VB_0024Me = this;
		closure_0024__219_002D._0024VB_0024Local_batchId = batchId;
		if (closure_0024__219_002D._0024VB_0024Local_batchId != quickerImportBatchId || parsed == null || parsed.Count == 0)
		{
			return;
		}
		HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		foreach (QuickerWheelParsedItem item in parsed)
		{
			if (item == null || item.ActionType != 12)
			{
				continue;
			}
			string text = (item.ActionId ?? "").Trim();
			if (text.Length != 0)
			{
				string text2 = NormalizeIconString(item.Icon ?? "");
				bool num = string.IsNullOrWhiteSpace(text2) || !IsLikelyRenderableIconSpec(text2);
				bool flag = !HasCustomQuickerMenuItems(item.QuickerMenuItems);
				if (num || flag)
				{
					hashSet.Add(text);
				}
			}
		}
		if (hashSet.Count == 0)
		{
			return;
		}
		_Closure_0024__219_002D1 closure_0024__219_002D2 = default(_Closure_0024__219_002D1);
		foreach (string item2 in hashSet.ToList())
		{
			closure_0024__219_002D2 = new _Closure_0024__219_002D1(closure_0024__219_002D2);
			closure_0024__219_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__219_002D;
			closure_0024__219_002D2._0024VB_0024Local_actionId = item2;
			Task.Run((Func<Task>)closure_0024__219_002D2._Lambda_0024__0);
		}
	}

	private void ApplyClipboardDemoResultToMatchingItems(string actionId, string title, string icon, string menuText)
	{
		if (string.IsNullOrWhiteSpace(actionId) || configs == null)
		{
			return;
		}
		string text = NormalizeIconString(icon ?? "");
		if (text.Length > 0 && !IsLikelyRenderableIconSpec(text))
		{
			text = "";
		}
		List<QuickerRightClickMenuItemConfig> list = null;
		if (!string.IsNullOrWhiteSpace(menuText))
		{
			string text2 = menuText;
			try
			{
				text2 = text2.Replace("\r\n", "\n").Replace("\r", "\n");
				if (text2.IndexOf("\\n", StringComparison.Ordinal) >= 0)
				{
					text2 = text2.Replace("\\n", "\n");
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				text2 = menuText;
				ProjectData.ClearProjectError();
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			dictionary["RightClickMenu"] = text2;
			try
			{
				list = TryExtractQuickerMenuItemsFromWheelActionData(dictionary, actionId);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				list = null;
				ProjectData.ClearProjectError();
			}
		}
		bool flag = false;
		foreach (MenuItemConfig config in configs)
		{
			if (config == null || !string.Equals(config.OperationType ?? "", "Quicker动作", StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}
			string actionId2 = "";
			if (!TryGetQuickerActionIdFromCommand(config.Command, ref actionId2) || !string.Equals(actionId2.Trim(), actionId.Trim(), StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}
			if (string.IsNullOrWhiteSpace(config.DisplayName) && !string.IsNullOrWhiteSpace(title))
			{
				config.DisplayName = title;
				flag = true;
			}
			if (text.Length > 0)
			{
				string text3 = NormalizeIconString(config.IconPath ?? "");
				if (!IsLikelyRenderableIconSpec(text3))
				{
					text3 = "";
				}
				if (string.IsNullOrWhiteSpace(text3))
				{
					config.IconPath = text;
					flag = true;
				}
			}
			if (list != null && HasCustomQuickerMenuItems(list) && (config.QuickerMenuItems == null || !HasCustomQuickerMenuItems(config.QuickerMenuItems)))
			{
				config.QuickerMenuItems = (from x in list
					where x != null
					select new QuickerRightClickMenuItemConfig
					{
						Icon = x.Icon,
						DisplayText = x.DisplayText,
						DisplayDescription = x.DisplayDescription,
						Parameter = x.Parameter,
						Marker = x.Marker,
						IsGroupHeader = x.IsGroupHeader,
						GroupParent = x.GroupParent
					}).ToList();
				flag = true;
			}
		}
		if (flag)
		{
			try
			{
				renderPanel.Invalidate();
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void StartQuickerMenuUpgradeForImportedItems(int batchId, List<QuickerWheelParsedItem> parsed)
	{
		_Closure_0024__221_002D0 closure_0024__221_002D = new _Closure_0024__221_002D0(null);
		closure_0024__221_002D._0024VB_0024Me = this;
		closure_0024__221_002D._0024VB_0024Local_batchId = batchId;
		if (closure_0024__221_002D._0024VB_0024Local_batchId != quickerImportBatchId || parsed == null || parsed.Count == 0)
		{
			return;
		}
		HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		foreach (QuickerWheelParsedItem item in parsed)
		{
			if (item != null && item.ActionType == 12)
			{
				string text = (item.ActionId ?? "").Trim();
				if (text.Length != 0 && !HasCustomQuickerMenuItems(item.QuickerMenuItems))
				{
					hashSet.Add(text);
				}
			}
		}
		if (hashSet.Count == 0)
		{
			return;
		}
		Task.Run([SpecialName] () =>
		{
			try
			{
				EnsureQuickerMetadataLoaded();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		});
		_Closure_0024__221_002D1 closure_0024__221_002D2 = default(_Closure_0024__221_002D1);
		foreach (string item2 in hashSet.ToList())
		{
			closure_0024__221_002D2 = new _Closure_0024__221_002D1(closure_0024__221_002D2);
			closure_0024__221_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__221_002D;
			closure_0024__221_002D2._0024VB_0024Local_actionId = item2;
			Task.Run((Func<Task>)closure_0024__221_002D2._Lambda_0024__1);
		}
	}

	private void ApplyQuickerMenuToMatchingItems(string actionId, List<QuickerRightClickMenuItemConfig> menu)
	{
		if (string.IsNullOrWhiteSpace(actionId) || menu == null || menu.Count == 0 || configs == null)
		{
			return;
		}
		bool flag = false;
		foreach (MenuItemConfig config in configs)
		{
			if (config == null || !string.Equals(config.OperationType ?? "", "Quicker动作", StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}
			string actionId2 = "";
			if (TryGetQuickerActionIdFromCommand(config.Command, ref actionId2) && string.Equals(actionId2.Trim(), actionId.Trim(), StringComparison.OrdinalIgnoreCase) && (config.QuickerMenuItems == null || !HasCustomQuickerMenuItems(config.QuickerMenuItems)))
			{
				config.QuickerMenuItems = (from x in menu
					where x != null
					select new QuickerRightClickMenuItemConfig
					{
						Icon = x.Icon,
						DisplayText = x.DisplayText,
						DisplayDescription = x.DisplayDescription,
						Parameter = x.Parameter,
						Marker = x.Marker,
						IsGroupHeader = x.IsGroupHeader,
						GroupParent = x.GroupParent
					}).ToList();
				flag = true;
			}
		}
		if (flag)
		{
			try
			{
				renderPanel.Invalidate();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private static bool TryMapQuickerWheelPositionToHit(int posKey, ref HitInfo hit)
	{
		hit = new HitInfo
		{
			Index = -1,
			Level = -1
		};
		if (posKey >= 0 && posKey <= 7)
		{
			int index = checked(posKey + 5) % 8;
			hit = new HitInfo
			{
				Index = index,
				Level = 0
			};
			return true;
		}
		if (posKey >= 100 && posKey <= 115)
		{
			int index2 = checked(posKey - 100 + 11) % 16;
			hit = new HitInfo
			{
				Index = index2,
				Level = 1
			};
			return true;
		}
		if (posKey >= 200 && posKey <= 207)
		{
			int index3 = checked(posKey - 200 + 5) % 8;
			hit = new HitInfo
			{
				Index = index3,
				Level = 2
			};
			return true;
		}
		return false;
	}

	private void ApplyParsedQuickerWheelItemToMenuItem(QuickerWheelParsedItem p, MenuItemConfig item)
	{
		if (p == null || item == null)
		{
			return;
		}
		item.Enabled = true;
		item.IsSubMenu = false;
		item.SubMenuName = "";
		item.QuickerMenuItems = null;
		item.IconPath = (p.Icon ?? "").Trim();
		item.DisplayName = p.Title ?? "";
		item.Text = p.ActionName ?? "";
		item.OperationType = "";
		item.Command = "";
		item.Shortcut = "";
		item.TypedText = "";
		item.PasteText = "";
		item.RunPath = "";
		item.RunParams = "";
		switch (p.ActionType)
		{
		case 12:
		{
			item.OperationType = "Quicker动作";
			string text2 = (p.ActionId ?? "").Trim();
			string param = p.ActionParam ?? "";
			if (text2.Length == 0)
			{
				text2 = (p.Id ?? "").Trim();
			}
			if (item.Text.Length == 0)
			{
				item.Text = p.Title ?? "";
			}
			if (text2.Length > 0)
			{
				string text3 = text2;
				string text4 = EncodeQuickerParamNewLinesForImport(param);
				if (text4.Length > 0)
				{
					text3 = text3 + "?" + text4;
				}
				item.Command = "QCAD_ACTION:" + text3;
			}
			if (p.QuickerMenuItems != null && p.QuickerMenuItems.Count > 0)
			{
				item.QuickerMenuItems = (from x in p.QuickerMenuItems
					where x != null
					select new QuickerRightClickMenuItemConfig
					{
						Icon = x.Icon,
						DisplayText = x.DisplayText,
						DisplayDescription = x.DisplayDescription,
						Parameter = x.Parameter,
						Marker = x.Marker,
						IsGroupHeader = x.IsGroupHeader,
						GroupParent = x.GroupParent
					}).ToList();
			}
			break;
		}
		case 1:
			item.OperationType = "发送快捷键";
			item.Shortcut = (p.Data ?? "").Trim();
			if (item.Text.Length == 0)
			{
				item.Text = p.Title ?? "";
			}
			break;
		case 6:
			item.OperationType = "键入文本";
			item.TypedText = p.Data ?? "";
			if (item.Text.Length == 0)
			{
				item.Text = p.Title ?? "";
			}
			break;
		case 3:
			item.OperationType = "粘贴文本";
			item.PasteText = RadialMenuController.NormalizeNewLinesToLf(p.Data ?? "");
			if (item.Text.Length == 0)
			{
				item.Text = p.Title ?? "";
			}
			break;
		case 5:
			item.OperationType = "打开或运行（文件/目录/命令/网址）";
			item.RunPath = (p.Id ?? "").Trim();
			item.RunParams = p.Param ?? "";
			item.Command = item.RunPath;
			if (item.Text.Length == 0)
			{
				string text = (p.ActionName ?? "").Trim();
				if (text.Length == 0)
				{
					text = "打开/运行";
				}
				item.Text = text;
			}
			break;
		default:
			item.OperationType = "Quicker动作";
			if (item.Text.Length == 0)
			{
				item.Text = p.Title ?? "";
			}
			break;
		}
	}

	private static string EncodeQuickerParamNewLinesForImport(string param)
	{
		string text = RadialMenuController.NormalizeNewLinesToLf(param ?? "");
		if (text.Length == 0)
		{
			return "";
		}
		return text.Replace("\n", "%0D%0A");
	}

	private static List<QuickerWheelParsedItem> ParseQuickerCircleMenuConfig(string jsonStr)
	{
		List<QuickerWheelParsedItem> list = new List<QuickerWheelParsedItem>();
		if (string.IsNullOrWhiteSpace(jsonStr))
		{
			return list;
		}
		try
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			Dictionary<string, string> dictionary = javaScriptSerializer.Deserialize<Dictionary<string, string>>(jsonStr);
			if (dictionary == null || dictionary.Count == 0)
			{
				return list;
			}
			int result2;
			List<int> list2 = (from k in dictionary.Keys
				select int.TryParse(k, out result2) ? result2 : int.MaxValue into x
				orderby x
				select x).ToList();
			foreach (int item in list2)
			{
				string text = item.ToString();
				if (!dictionary.ContainsKey(text))
				{
					continue;
				}
				string text2 = dictionary[text];
				if (string.IsNullOrEmpty(text2))
				{
					continue;
				}
				string text3 = text2;
				if (text3.StartsWith("json:", StringComparison.OrdinalIgnoreCase))
				{
					text3 = text3.Substring(5);
				}
				if (string.IsNullOrWhiteSpace(text3))
				{
					continue;
				}
				object obj = null;
				try
				{
					obj = RuntimeHelpers.GetObjectValue(javaScriptSerializer.DeserializeObject(text3));
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					obj = null;
					ProjectData.ClearProjectError();
				}
				if (!(obj is IDictionary<string, object> dictionary2))
				{
					continue;
				}
				string text4 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "Title")));
				string obj2 = TryGetFirstString(dictionary2, new string[4] { "Icon", "IconUrl", "IconURL", "IconText" });
				string text5 = "";
				if (!string.IsNullOrWhiteSpace(obj2))
				{
					text5 = "Field";
				}
				string text6 = NormalizeIconString(obj2);
				string text7 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "Id")));
				if (string.IsNullOrWhiteSpace(text7))
				{
					text7 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "ID")));
				}
				string text8 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "Param")));
				string text9 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "Data")));
				object obj3 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "ActionType")));
				int result = 0;
				if (obj3 == null)
				{
					obj3 = "";
				}
				int.TryParse(((string)obj3).Trim(), out result);
				string text10 = "";
				string actionParam = "";
				if (!string.IsNullOrWhiteSpace(text9))
				{
					string text11 = text9.Replace("\r\n", "\n").Replace("\r", "\n");
					string[] array = text11.Split(new string[1] { "\n" }, StringSplitOptions.None);
					if (array.Length == 1 && text11.Contains("\\n"))
					{
						array = text11.Split(new string[1] { "\\n" }, StringSplitOptions.None);
					}
					if (array.Length >= 1)
					{
						text10 = array[0].Trim();
					}
					if (array.Length >= 2)
					{
						actionParam = array[1].Trim();
					}
				}
				if (result == 5)
				{
					string text12 = (text7 ?? "").Trim();
					string text13 = text8 ?? "";
					if (string.IsNullOrWhiteSpace(text12))
					{
						text12 = TryGetFirstString(dictionary2, new string[10] { "RunPath", "Path", "FilePath", "File", "ExePath", "Exe", "Target", "Url", "URL", "Command" });
					}
					if (string.IsNullOrWhiteSpace(text13))
					{
						text13 = TryGetFirstString(dictionary2, new string[5] { "RunParams", "Args", "Arguments", "Params", "Parameters" });
					}
					if (string.IsNullOrWhiteSpace(text12) && !string.IsNullOrWhiteSpace(text9))
					{
						string text14 = text9.Replace("\r\n", "\n").Replace("\r", "\n");
						List<string> list3 = (from x in text14.Split(new string[1] { "\n" }, StringSplitOptions.None)
							select (x ?? "").Trim() into x
							where x.Length > 0
							select x).ToList();
						if (list3.Count == 1 && text14.Contains("\\n"))
						{
							list3 = (from x in text14.Split(new string[1] { "\\n" }, StringSplitOptions.None)
								select (x ?? "").Trim() into x
								where x.Length > 0
								select x).ToList();
						}
						if (list3.Count >= 1)
						{
							text12 = list3[0].Trim();
						}
						if (list3.Count >= 2 && string.IsNullOrWhiteSpace(text13))
						{
							text13 = list3[1];
						}
					}
					text7 = (text12 ?? "").Trim();
					text8 = text13 ?? "";
				}
				if (string.IsNullOrWhiteSpace(text6) && !string.IsNullOrWhiteSpace(text3))
				{
					Match match = Regex.Match(text3, "\"IconUrl\"\\s*:\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						text6 = NormalizeIconString(match.Groups[1].Value);
						text5 = "Regex:IconUrl";
					}
					else
					{
						Match match2 = Regex.Match(text3, "\"Icon\"\\s*:\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
						if (match2.Success)
						{
							text6 = NormalizeIconString(match2.Groups[1].Value);
							text5 = "Regex:Icon";
						}
						else
						{
							Match match3 = Regex.Match(text3, "\"IconText\"\\s*:\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
							if (match3.Success)
							{
								text6 = NormalizeIconString(match3.Groups[1].Value);
								text5 = "Regex:IconText";
							}
						}
					}
				}
				string text15 = "";
				if (text10.Length > 0)
				{
					QuickerActionMetadata quickerActionMetadata = LookupQuickerMetadataIfLoaded(text10);
					if (quickerActionMetadata != null)
					{
						if (!string.IsNullOrWhiteSpace(quickerActionMetadata.Title))
						{
							text15 = quickerActionMetadata.Title;
						}
						if (string.IsNullOrWhiteSpace(text6) && !string.IsNullOrWhiteSpace(quickerActionMetadata.Icon))
						{
							text6 = NormalizeIconString(quickerActionMetadata.Icon);
							if (text5.Length == 0)
							{
								text5 = "Meta";
							}
						}
					}
				}
				if (string.IsNullOrWhiteSpace(text6) && !string.IsNullOrWhiteSpace(text3))
				{
					Match match4 = Regex.Match(text3, "(https?://[^\"\\s\\\\]+?\\.(png|jpg|jpeg|gif|ico)(\\?[^\"]*)?)", RegexOptions.IgnoreCase);
					if (match4.Success)
					{
						text6 = NormalizeIconString(match4.Groups[1].Value);
						if (LooksLikeIconSpec(text6))
						{
							text5 = "Regex:AnyUrl";
						}
						else
						{
							text6 = "";
						}
					}
				}
				if (string.IsNullOrWhiteSpace(text6))
				{
					string source = "";
					string text16 = TryFindIconDeep(dictionary2, ref source);
					if (!string.IsNullOrWhiteSpace(text16))
					{
						text6 = text16;
						text5 = "Deep:" + source;
					}
				}
				if (result == 12 && text10.Length > 0)
				{
					string title = "";
					string text17 = "";
					string rightClickMenuText = "";
					bool flag = false;
					try
					{
						flag = TryLoadClipboardDemoSubprogramResult(text10, ref title, ref text17, ref rightClickMenuText);
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						flag = false;
						ProjectData.ClearProjectError();
					}
					if (flag)
					{
						if (string.IsNullOrWhiteSpace(text6) && text17.Length > 0)
						{
							text6 = text17;
							text5 = "ClipboardDemoOut";
						}
						if (string.IsNullOrWhiteSpace(text4) && title.Length > 0)
						{
							text4 = title;
						}
					}
				}
				if (string.IsNullOrWhiteSpace(text15))
				{
					text15 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "ActionName")));
				}
				if (string.IsNullOrWhiteSpace(text15))
				{
					text15 = Convert.ToString(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dictionary2, "Name")));
				}
				if (string.IsNullOrWhiteSpace(text15) && result == 5)
				{
					text15 = "打开/运行";
				}
				if (string.IsNullOrWhiteSpace(text15))
				{
					text15 = text4;
				}
				List<QuickerRightClickMenuItemConfig> list4 = null;
				if (result == 12 && text10.Length > 0)
				{
					list4 = TryExtractQuickerMenuItemsFromWheelActionData(dictionary2, text10);
					if (list4 != null && list4.Count > 0)
					{
						AppendQuickerImportLog("Parsed menu. key=" + text + ", actionId=" + text10 + ", count=" + list4.Count);
					}
				}
				if (result == 12 && text10.Length > 0 && (list4 == null || list4.Count == 0))
				{
					QuickerActionMetadata quickerActionMetadata2 = LookupQuickerMetadataIfLoaded(text10);
					if (quickerActionMetadata2 != null && quickerActionMetadata2.QuickerMenuItems != null && quickerActionMetadata2.QuickerMenuItems.Count > 0)
					{
						list4 = quickerActionMetadata2.QuickerMenuItems;
						AppendQuickerImportLog("Menu loaded from meta. key=" + text + ", actionId=" + text10 + ", count=" + list4.Count);
					}
				}
				if (result == 12 && text10.Length > 0 && (list4 == null || list4.Count == 0 || !HasCustomQuickerMenuItems(list4)))
				{
					string title2 = "";
					string text18 = "";
					string rightClickMenuText2 = "";
					bool flag2 = false;
					try
					{
						flag2 = TryLoadClipboardDemoSubprogramResult(text10, ref title2, ref text18, ref rightClickMenuText2);
					}
					catch (Exception projectError3)
					{
						ProjectData.SetProjectError(projectError3);
						flag2 = false;
						ProjectData.ClearProjectError();
					}
					if (flag2 && !string.IsNullOrWhiteSpace(rightClickMenuText2))
					{
						string text19 = rightClickMenuText2;
						try
						{
							text19 = text19.Replace("\r\n", "\n").Replace("\r", "\n");
							if (text19.IndexOf("\\n", StringComparison.Ordinal) >= 0)
							{
								text19 = text19.Replace("\\n", "\n");
							}
						}
						catch (Exception projectError4)
						{
							ProjectData.SetProjectError(projectError4);
							text19 = rightClickMenuText2;
							ProjectData.ClearProjectError();
						}
						List<QuickerRightClickMenuItemConfig> list5 = TryExtractQuickerMenuItemsFromWheelActionData(new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase) { ["RightClickMenu"] = text19 }, text10);
						if (list5 != null && HasCustomQuickerMenuItems(list5))
						{
							list4 = list5;
							AppendQuickerImportLog("Menu loaded from ClipboardDemo out.txt. key=" + text + ", actionId=" + text10 + ", count=" + list5.Count);
							try
							{
								List<QuickerRightClickMenuItemConfig> items = (from x in list5
									where x != null
									select new QuickerRightClickMenuItemConfig
									{
										Icon = x.Icon,
										DisplayText = x.DisplayText,
										DisplayDescription = x.DisplayDescription,
										Parameter = x.Parameter,
										Marker = x.Marker,
										IsGroupHeader = x.IsGroupHeader,
										GroupParent = x.GroupParent
									}).ToList();
								object obj4 = quickerMenuFetchCacheLock;
								ObjectFlowControl.CheckForSyncLockOnValueType(obj4);
								bool lockTaken = false;
								try
								{
									Monitor.Enter(obj4, ref lockTaken);
									quickerMenuFetchCache[text10] = new QuickerMenuFetchCacheEntry
									{
										FetchedAtUtcTicks = DateTime.UtcNow.Ticks,
										Items = items
									};
								}
								finally
								{
									if (lockTaken)
									{
										Monitor.Exit(obj4);
									}
								}
							}
							catch (Exception projectError5)
							{
								ProjectData.SetProjectError(projectError5);
								ProjectData.ClearProjectError();
							}
						}
					}
				}
				if (!string.IsNullOrWhiteSpace(text6))
				{
					string text20 = NormalizeIconString(text6);
					text6 = ((!IsLikelyRenderableIconSpec(text20)) ? "" : text20);
				}
				if (string.IsNullOrWhiteSpace(text6))
				{
					string text21 = text3;
					try
					{
						if (text21.Length > 400)
						{
							text21 = text21.Substring(0, 400);
						}
						text21 = text21.Replace("\r", " ").Replace("\n", " ");
					}
					catch (Exception projectError6)
					{
						ProjectData.SetProjectError(projectError6);
						ProjectData.ClearProjectError();
					}
					AppendQuickerImportLog("NoIcon key=" + text + ", title=\"" + text4 + "\", actionName=\"" + text15 + "\", actionType=" + result + ", actionId=\"" + text10 + "\", snippet=" + text21);
				}
				AppendQuickerImportLog("Parsed item. key=" + text + ", title=\"" + text4 + "\", actionName=\"" + text15 + "\", actionType=" + result + ", actionId=\"" + text10 + "\", icon=\"" + text6 + "\", iconSource=" + text5);
				list.Add(new QuickerWheelParsedItem
				{
					RawPositionKey = text,
					Title = (text4 ?? ""),
					Icon = (text6 ?? ""),
					ActionType = result,
					Data = (text9 ?? ""),
					Id = (text7 ?? ""),
					Param = (text8 ?? ""),
					ActionId = text10,
					ActionParam = actionParam,
					ActionName = (text15 ?? ""),
					QuickerMenuItems = list4
				});
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			AppendQuickerImportLog("ParseQuickerCircleMenuConfig failed: " + ex2.ToString());
			ProjectData.ClearProjectError();
		}
		return list;
	}

	private static List<QuickerRightClickMenuItemConfig> TryExtractQuickerMenuItemsFromWheelActionData(IDictionary<string, object> actionData, string actionId)
	{
		List<QuickerRightClickMenuItemConfig> list = new List<QuickerRightClickMenuItemConfig>();
		if (actionData == null)
		{
			return list;
		}
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return list;
		}
		List<object> list2 = new List<object>();
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "SubActions")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "SubAction")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "RightClickMenu")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "RightMenu")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "Menu")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "MenuItems")));
		list2.Add(RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(actionData, "ParamData")));
		List<QuickerRightClickMenuItemConfig> list3 = new List<QuickerRightClickMenuItemConfig>();
		foreach (object item7 in list2)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(item7);
			try
			{
				list3.AddRange(TryParseQuickerMenuItemsStructured(RuntimeHelpers.GetObjectValue(objectValue), actionId, 0));
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
		list3 = list3.Where([SpecialName] (QuickerRightClickMenuItemConfig x) => x != null && !string.IsNullOrWhiteSpace(x.DisplayText ?? "")).ToList();
		if (list3.Count > 0)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string groupParent = "";
			foreach (QuickerRightClickMenuItemConfig item8 in list3)
			{
				if (item8 == null)
				{
					continue;
				}
				item8.Icon = item8.Icon ?? "";
				item8.DisplayText = item8.DisplayText ?? "";
				item8.DisplayDescription = item8.DisplayDescription ?? "";
				item8.Parameter = item8.Parameter ?? "";
				item8.Marker = item8.Marker ?? "";
				item8.GroupParent = item8.GroupParent ?? "";
				if (item8.IsGroupHeader || string.Equals(item8.Marker, "+", StringComparison.Ordinal))
				{
					item8.IsGroupHeader = true;
					item8.Marker = "+";
					item8.GroupParent = "";
					groupParent = item8.DisplayText;
				}
				else if (string.Equals(item8.Marker, "-", StringComparison.Ordinal))
				{
					if (string.IsNullOrWhiteSpace(item8.GroupParent))
					{
						item8.GroupParent = groupParent;
					}
				}
				else
				{
					groupParent = "";
					item8.GroupParent = "";
				}
				item8.Parameter = NormalizeQuickerParam(item8.Parameter, actionId.Trim());
				string text = (item8.Icon + "|" + item8.DisplayText + "|" + item8.Parameter + "|" + item8.GroupParent + "|" + item8.Marker).Trim();
				if (Operators.CompareString(text, "||", TextCompare: false) != 0 && hashSet.Add(text))
				{
					list.Add(item8);
				}
			}
			string text2 = "quicker:runaction:无限轮盘?sp=编辑动作&targetId=" + actionId.Trim();
			string item = ("编|编辑|" + text2 + "||").Trim();
			if (hashSet.Add(item))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "编",
					DisplayText = "编辑",
					DisplayDescription = "",
					Parameter = text2,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
			string text3 = "quicker:debugaction:" + actionId.Trim();
			string item2 = ("调|调试|" + text3 + "||").Trim();
			if (hashSet.Add(item2))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "调",
					DisplayText = "调试",
					DisplayDescription = "",
					Parameter = text3,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
			return list;
		}
		List<string> list4 = new List<string>();
		foreach (object item9 in list2)
		{
			object objectValue2 = RuntimeHelpers.GetObjectValue(item9);
			if (objectValue2 == null)
			{
				continue;
			}
			if (objectValue2 is string)
			{
				string text4 = (string)objectValue2;
				if (!string.IsNullOrWhiteSpace(text4))
				{
					list4.Add(text4);
				}
				continue;
			}
			List<string> list5 = new List<string>();
			TryCollectStringsFromObjectGraph(RuntimeHelpers.GetObjectValue(objectValue2), list5, 6);
			if (list5.Count > 0)
			{
				list4.AddRange(list5);
			}
		}
		list4 = (from s in list4
			where !string.IsNullOrWhiteSpace(s)
			select s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		if (list4.Count > 0)
		{
			List<QuickerRightClickMenuItemConfig> list6 = new List<QuickerRightClickMenuItemConfig>();
			List<string> list7 = null;
			foreach (string item10 in list4)
			{
				List<string> list8 = (from x in SplitCandidateLines(item10)
					where LooksLikeQuickerMenuLine(x)
					select x).ToList();
				if (list8.Count > 0 && (list7 == null || list8.Count > list7.Count))
				{
					list7 = list8;
				}
			}
			if (list7 == null || list7.Count <= 0)
			{
				foreach (string item11 in list4)
				{
					foreach (string item12 in SplitCandidateLines(item11))
					{
						if (LooksLikeQuickerMenuLine(item12))
						{
							QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig = ParseQuickerMenuLine(item12);
							if (quickerRightClickMenuItemConfig != null)
							{
								quickerRightClickMenuItemConfig.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig.Parameter, actionId.Trim());
								quickerRightClickMenuItemConfig.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig.Marker, "+", StringComparison.Ordinal);
								list6.Add(quickerRightClickMenuItemConfig);
							}
						}
					}
					MatchCollection matchCollection = Regex.Matches(item11, "(?:\\[\\s*[+-]\\s*\\]\\s*)?\\[(?i:(?:fa|url):[^\\]]+)\\][^\\r\\n]+");
					foreach (Match item13 in matchCollection)
					{
						if (item13 == null || !item13.Success)
						{
							continue;
						}
						string value = item13.Value;
						if (LooksLikeQuickerMenuLine(value))
						{
							QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig2 = ParseQuickerMenuLine(value);
							if (quickerRightClickMenuItemConfig2 != null)
							{
								quickerRightClickMenuItemConfig2.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig2.Parameter, actionId.Trim());
								quickerRightClickMenuItemConfig2.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig2.Marker, "+", StringComparison.Ordinal);
								list6.Add(quickerRightClickMenuItemConfig2);
							}
						}
					}
				}
			}
			else
			{
				foreach (string item14 in list7)
				{
					QuickerRightClickMenuItemConfig quickerRightClickMenuItemConfig3 = ParseQuickerMenuLine(item14);
					if (quickerRightClickMenuItemConfig3 != null)
					{
						quickerRightClickMenuItemConfig3.Parameter = NormalizeQuickerParam(quickerRightClickMenuItemConfig3.Parameter, actionId.Trim());
						quickerRightClickMenuItemConfig3.IsGroupHeader = string.Equals(quickerRightClickMenuItemConfig3.Marker, "+", StringComparison.Ordinal);
						list6.Add(quickerRightClickMenuItemConfig3);
					}
				}
			}
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string groupParent2 = "";
			foreach (QuickerRightClickMenuItemConfig item15 in list6)
			{
				if (item15 != null)
				{
					if (item15.IsGroupHeader)
					{
						item15.GroupParent = "";
						groupParent2 = item15.DisplayText ?? "";
					}
					else if (string.Equals(item15.Marker, "-", StringComparison.Ordinal))
					{
						item15.GroupParent = groupParent2;
					}
					else
					{
						item15.GroupParent = "";
						groupParent2 = "";
					}
					string text5 = (item15.Icon + "|" + item15.DisplayText + "|" + item15.Parameter + "|" + item15.GroupParent + "|" + item15.Marker).Trim();
					if (Operators.CompareString(text5, "||", TextCompare: false) != 0 && hashSet2.Add(text5))
					{
						list.Add(item15);
					}
				}
			}
			string text6 = "quicker:runaction:无限轮盘?sp=编辑动作&targetId=" + actionId.Trim();
			string item3 = ("编|编辑|" + text6 + "||").Trim();
			if (hashSet2.Add(item3))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "编",
					DisplayText = "编辑",
					DisplayDescription = "",
					Parameter = text6,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
			string text7 = "quicker:debugaction:" + actionId.Trim();
			string item4 = ("调|调试|" + text7 + "||").Trim();
			if (hashSet2.Add(item4))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "调",
					DisplayText = "调试",
					DisplayDescription = "",
					Parameter = text7,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
		}
		else
		{
			HashSet<string> hashSet3 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string text8 = "quicker:runaction:无限轮盘?sp=编辑动作&targetId=" + actionId.Trim();
			string item5 = ("编|编辑|" + text8 + "||").Trim();
			if (hashSet3.Add(item5))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "编",
					DisplayText = "编辑",
					DisplayDescription = "",
					Parameter = text8,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
			string text9 = "quicker:debugaction:" + actionId.Trim();
			string item6 = ("调|调试|" + text9 + "||").Trim();
			if (hashSet3.Add(item6))
			{
				list.Add(new QuickerRightClickMenuItemConfig
				{
					Icon = "调",
					DisplayText = "调试",
					DisplayDescription = "",
					Parameter = text9,
					Marker = "",
					IsGroupHeader = false,
					GroupParent = ""
				});
			}
		}
		bool flag = false;
		try
		{
			flag = list.Any([SpecialName] (QuickerRightClickMenuItemConfig x) => x != null && !string.Equals(x.DisplayText ?? "", "编辑", StringComparison.OrdinalIgnoreCase) && !string.Equals(x.DisplayText ?? "", "调试", StringComparison.OrdinalIgnoreCase));
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			flag = false;
			ProjectData.ClearProjectError();
		}
		return list;
	}

	private static IDictionary<string, object> GetQuickerActionFullMetadata(string actionId)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(actionId))
			{
				return null;
			}
			if (!Guid.TryParse(actionId.Trim(), out var result))
			{
				return null;
			}
			string[] array = new string[2]
			{
				"quicker:getaction:" + result.ToString(),
				"quicker:actioninfo:" + result.ToString()
			};
			foreach (string payload in array)
			{
				string responseText = "";
				if (!TrySendQuickerIpcPayload(payload, ref responseText) || string.IsNullOrWhiteSpace(responseText))
				{
					continue;
				}
				IDictionary<string, object> dictionary = TryParseJsonDictionary(responseText);
				if (dictionary != null && dictionary.Count > 0)
				{
					return dictionary;
				}
				int num = -1;
				int num2 = -1;
				try
				{
					num = responseText.IndexOf('{');
					num2 = responseText.LastIndexOf('}');
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					num = -1;
					num2 = -1;
					ProjectData.ClearProjectError();
				}
				if (num < 0 || num2 <= num)
				{
					continue;
				}
				string text = "";
				try
				{
					text = responseText.Substring(num, checked(num2 - num + 1));
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					text = "";
					ProjectData.ClearProjectError();
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					dictionary = TryParseJsonDictionary(text);
					if (dictionary != null && dictionary.Count > 0)
					{
						return dictionary;
					}
				}
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		return null;
	}

	private static bool TrySendQuickerIpcPayload(string payload, ref string responseText)
	{
		responseText = "";
		bool result;
		List<byte> list;
		checked
		{
			if (string.IsNullOrWhiteSpace(payload))
			{
				result = false;
			}
			else
			{
				byte[] array = null;
				try
				{
					array = Encoding.UTF8.GetBytes(payload);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					array = null;
					ProjectData.ClearProjectError();
				}
				if (array == null || array.Length == 0)
				{
					result = false;
				}
				else
				{
					list = new List<byte>();
					try
					{
						using NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", "QuickerIPCChannel", PipeDirection.InOut, PipeOptions.None);
						try
						{
							namedPipeClientStream.Connect(400);
						}
						catch (Exception projectError2)
						{
							ProjectData.SetProjectError(projectError2);
							result = false;
							ProjectData.ClearProjectError();
							goto end_IL_005c;
						}
						if (namedPipeClientStream.IsConnected)
						{
							try
							{
								namedPipeClientStream.Write(array, 0, array.Length);
								namedPipeClientStream.Flush();
							}
							catch (Exception projectError3)
							{
								ProjectData.SetProjectError(projectError3);
								result = false;
								ProjectData.ClearProjectError();
								goto end_IL_005c;
							}
							try
							{
								namedPipeClientStream.ReadTimeout = 220;
							}
							catch (Exception projectError4)
							{
								ProjectData.SetProjectError(projectError4);
								ProjectData.ClearProjectError();
							}
							byte[] array2 = new byte[4096];
							int num = 0;
							while (num < 48)
							{
								num++;
								int num2 = 0;
								try
								{
									num2 = namedPipeClientStream.Read(array2, 0, array2.Length);
								}
								catch (Exception projectError5)
								{
									ProjectData.SetProjectError(projectError5);
									ProjectData.ClearProjectError();
									break;
								}
								if (num2 > 0)
								{
									int num3 = num2 - 1;
									for (int i = 0; i <= num3; i++)
									{
										list.Add(array2[i]);
									}
									if (num2 < array2.Length)
									{
										break;
									}
									continue;
								}
								break;
							}
							goto IL_015b;
						}
						result = false;
						end_IL_005c:;
					}
					catch (Exception projectError6)
					{
						ProjectData.SetProjectError(projectError6);
						result = false;
						ProjectData.ClearProjectError();
					}
				}
			}
			goto IL_0220;
		}
		IL_0220:
		return result;
		IL_015b:
		if (list.Count == 0)
		{
			responseText = "";
			result = true;
		}
		else
		{
			byte[] bytes = list.ToArray();
			string text = "";
			try
			{
				text = Encoding.UTF8.GetString(bytes);
			}
			catch (Exception projectError7)
			{
				ProjectData.SetProjectError(projectError7);
				text = "";
				ProjectData.ClearProjectError();
			}
			text = (text ?? "").Replace("\0", "").Trim();
			if (text.Length > 0)
			{
				responseText = text;
				result = true;
			}
			else
			{
				string text2 = "";
				try
				{
					text2 = Encoding.Unicode.GetString(bytes);
				}
				catch (Exception projectError8)
				{
					ProjectData.SetProjectError(projectError8);
					text2 = "";
					ProjectData.ClearProjectError();
				}
				responseText = (text2 ?? "").Replace("\0", "").Trim();
				result = true;
			}
		}
		goto IL_0220;
	}

	private static IDictionary<string, object> TryParseJsonDictionary(string jsonText)
	{
		IDictionary<string, object> result;
		if (string.IsNullOrWhiteSpace(jsonText))
		{
			result = null;
		}
		else
		{
			try
			{
				string text = jsonText.Replace("\0", "").Trim();
				result = ((text.Length != 0) ? (RuntimeHelpers.GetObjectValue(new JavaScriptSerializer
				{
					MaxJsonLength = int.MaxValue
				}.DeserializeObject(text)) as IDictionary<string, object>) : null);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = null;
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	private static List<QuickerRightClickMenuItemConfig> TryParseQuickerMenuItemsStructured(object node, string actionId, int depth)
	{
		List<QuickerRightClickMenuItemConfig> list = new List<QuickerRightClickMenuItemConfig>();
		if (node == null)
		{
			return list;
		}
		if (depth > 8)
		{
			return list;
		}
		if (string.IsNullOrWhiteSpace(actionId))
		{
			return list;
		}
		if (node is string)
		{
			return list;
		}
		checked
		{
			if (node is IDictionary<string, object> dict)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dict, "Items"));
				if (objectValue == null)
				{
					objectValue = RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dict, "Children"));
				}
				if (objectValue == null)
				{
					objectValue = RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dict, "SubActions"));
				}
				if (objectValue == null)
				{
					objectValue = RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dict, "MenuItems"));
				}
				string text = TryGetFirstString(dict, new string[4] { "DisplayText", "Text", "Title", "Name" });
				text = (text ?? "").Trim();
				string text2 = NormalizeIconString(TryGetFirstString(dict, new string[4] { "Icon", "IconUrl", "IconURL", "IconText" }));
				string text3 = TryGetFirstString(dict, new string[3] { "DisplayDescription", "Description", "Desc" });
				text3 = (text3 ?? "").Trim();
				string text4 = TryGetFirstString(dict, new string[1] { "Marker" });
				text4 = (text4 ?? "").Trim();
				string text5 = TryGetFirstString(dict, new string[3] { "GroupParent", "Group", "Parent" });
				text5 = (text5 ?? "").Trim();
				bool result = false;
				try
				{
					object objectValue2 = RuntimeHelpers.GetObjectValue(TryGetDictValueIgnoreCase(dict, "IsGroupHeader"));
					if (objectValue2 != null)
					{
						bool.TryParse(Convert.ToString(RuntimeHelpers.GetObjectValue(objectValue2)), out result);
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					result = false;
					ProjectData.ClearProjectError();
				}
				string text6 = TryGetFirstString(dict, new string[5] { "Parameter", "Param", "Data", "Value", "Command" });
				text6 = (text6 ?? "").Trim();
				if (objectValue != null)
				{
					List<QuickerRightClickMenuItemConfig> list2 = TryParseQuickerMenuItemsStructured(RuntimeHelpers.GetObjectValue(objectValue), actionId, depth + 1);
					if (list2 != null && list2.Count > 0)
					{
						if (text.Length > 0)
						{
							string text7 = text2;
							QuickerRightClickMenuItemConfig item = new QuickerRightClickMenuItemConfig
							{
								Icon = text7,
								DisplayText = text,
								DisplayDescription = text3,
								Parameter = "",
								Marker = "+",
								IsGroupHeader = true,
								GroupParent = ""
							};
							list.Add(item);
							foreach (QuickerRightClickMenuItemConfig item3 in list2)
							{
								if (item3 != null)
								{
									if (string.IsNullOrWhiteSpace(item3.GroupParent))
									{
										item3.GroupParent = text;
									}
									if (string.IsNullOrWhiteSpace(item3.Marker))
									{
										item3.Marker = "-";
									}
									list.Add(item3);
								}
							}
							return list;
						}
						list.AddRange(list2);
						return list;
					}
				}
				if (text.Length > 0)
				{
					QuickerRightClickMenuItemConfig item2 = new QuickerRightClickMenuItemConfig
					{
						Icon = text2,
						DisplayText = text,
						DisplayDescription = text3,
						Parameter = text6,
						Marker = text4,
						IsGroupHeader = (result || string.Equals(text4, "+", StringComparison.Ordinal)),
						GroupParent = text5
					};
					list.Add(item2);
				}
				return list;
			}
			if (node is IEnumerable enumerable)
			{
				int num = 0;
				foreach (object item4 in enumerable)
				{
					object objectValue3 = RuntimeHelpers.GetObjectValue(item4);
					if (num >= 500)
					{
						break;
					}
					num++;
					if (objectValue3 != null)
					{
						list.AddRange(TryParseQuickerMenuItemsStructured(RuntimeHelpers.GetObjectValue(objectValue3), actionId, depth + 1));
					}
				}
			}
			return list;
		}
	}

	private static object TryGetDictValueIgnoreCase(IDictionary<string, object> dict, string key)
	{
		if (dict == null || string.IsNullOrWhiteSpace(key))
		{
			return null;
		}
		foreach (string key2 in dict.Keys)
		{
			if (string.Equals(key2, key, StringComparison.OrdinalIgnoreCase))
			{
				return dict[key2];
			}
		}
		return null;
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__88_002D0()
	{
		if (cursorRestoreTicksLeft <= 0)
		{
			cursorRestoreTimer.Stop();
			return;
		}
		checked
		{
			cursorRestoreTicksLeft--;
			if (EnsureCursorVisible())
			{
				cursorRestoreTimer.Stop();
			}
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D1()
	{
		cboProcess.SelectAll();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D2()
	{
		BeginInvoke((Action)([SpecialName] () =>
		{
			cboProcess.SelectAll();
		}));
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D9()
	{
		ApplyRootFontSizeToAllScopes(Convert.ToInt32(nudRootFontSize.Value));
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D10()
	{
		if (suppressAppearanceChange)
		{
			return;
		}
		sizeConfig.ShowDividers = miShowDividers.Checked;
		suppressAppearanceChange = true;
		try
		{
			if (miShowDividers.Checked)
			{
				miShowInnerDividers.Visible = true;
				miShowInnerDividers.Checked = !sizeConfig.ShowInnerDividers.HasValue || sizeConfig.ShowInnerDividers.Value;
			}
			else
			{
				miShowInnerDividers.Visible = false;
			}
		}
		finally
		{
			suppressAppearanceChange = false;
		}
		renderPanel.Invalidate();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D11()
	{
		if (!suppressAppearanceChange)
		{
			sizeConfig.ShowInnerDividers = miShowInnerDividers.Checked;
			renderPanel.Invalidate();
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D12()
	{
		if (!suppressAppearanceChange)
		{
			sizeConfig.OuterRingGray = miOuterRingGray.Checked;
			renderPanel.Invalidate();
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D13()
	{
		sizeConfig.DragAreaBackgroundImage = "";
		sizeConfig.ShowDividers = true;
		sizeConfig.OuterRingGray = true;
		suppressAppearanceChange = true;
		try
		{
			miShowDividers.Checked = true;
			miShowInnerDividers.Visible = true;
			miShowInnerDividers.Checked = !sizeConfig.ShowInnerDividers.HasValue || sizeConfig.ShowInnerDividers.Value;
			miOuterRingGray.Checked = true;
			miClearSkinImage.Visible = false;
			miSkinOpacity.Visible = false;
		}
		finally
		{
			suppressAppearanceChange = false;
		}
		renderPanel.Invalidate();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D17()
	{
		string text = Interaction.InputBox("请输入皮肤图片URL（http/https）。留空则选择本地图片：", "设置皮肤图片");
		if (!string.IsNullOrWhiteSpace(text))
		{
			string text2 = text.Trim();
			if (!text2.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !text2.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				MessageBox.Show("URL 必须以 http:// 或 https:// 开头。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text3 = RadialMenuController.ResolveSkinSpecToLocalFile(text2);
			if (string.IsNullOrWhiteSpace(text3) || !File.Exists(text3))
			{
				MessageBox.Show("下载图片失败，请检查URL是否可访问。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			sizeConfig.DragAreaBackgroundImage = text2;
			sizeConfig.ShowDividers = false;
			sizeConfig.OuterRingGray = false;
			if (double.IsNaN(sizeConfig.SkinOpacity) || double.IsInfinity(sizeConfig.SkinOpacity))
			{
				sizeConfig.SkinOpacity = 1.0;
			}
			if (sizeConfig.SkinOpacity < 0.0)
			{
				sizeConfig.SkinOpacity = 0.0;
			}
			if (sizeConfig.SkinOpacity > 1.0)
			{
				sizeConfig.SkinOpacity = 1.0;
			}
			suppressAppearanceChange = true;
			try
			{
				miShowDividers.Checked = false;
				miShowInnerDividers.Visible = false;
				miOuterRingGray.Checked = false;
			}
			finally
			{
				suppressAppearanceChange = false;
			}
			miClearSkinImage.Visible = true;
			miSkinOpacity.Visible = true;
			renderPanel.Invalidate();
			return;
		}
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = "选择皮肤图片";
		try
		{
			string directoryName = Path.GetDirectoryName(store.GetActualConfigPath());
			if (!string.IsNullOrEmpty(directoryName) && Directory.Exists(directoryName))
			{
				openFileDialog.InitialDirectory = directoryName;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		openFileDialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.webp|所有文件|*.*";
		openFileDialog.Multiselect = false;
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string fileName = openFileDialog.FileName;
		if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
		{
			return;
		}
		string text4 = "";
		try
		{
			text4 = Path.GetDirectoryName(store.GetActualConfigPath());
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			text4 = "";
			ProjectData.ClearProjectError();
		}
		if (string.IsNullOrWhiteSpace(text4))
		{
			MessageBox.Show("无法确定配置目录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		try
		{
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			ProjectData.ClearProjectError();
		}
		string text5 = "";
		try
		{
			text5 = Path.GetFileName(fileName);
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			text5 = "";
			ProjectData.ClearProjectError();
		}
		if (string.IsNullOrWhiteSpace(text5))
		{
			string text6 = "";
			try
			{
				text6 = Path.GetExtension(fileName);
			}
			catch (Exception projectError5)
			{
				ProjectData.SetProjectError(projectError5);
				text6 = "";
				ProjectData.ClearProjectError();
			}
			if (string.IsNullOrWhiteSpace(text6))
			{
				text6 = ".png";
			}
			text5 = "Skin" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + text6;
		}
		string text7 = Path.Combine(text4, text5);
		try
		{
			string fullPath = Path.GetFullPath(fileName);
			string fullPath2 = Path.GetFullPath(text7);
			if (!string.Equals(fullPath, fullPath2, StringComparison.OrdinalIgnoreCase))
			{
				File.Copy(fullPath, fullPath2, overwrite: true);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			MessageBox.Show("复制图片失败：" + ex2.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			ProjectData.ClearProjectError();
			return;
		}
		sizeConfig.DragAreaBackgroundImage = text7;
		sizeConfig.ShowDividers = false;
		sizeConfig.OuterRingGray = false;
		if (double.IsNaN(sizeConfig.SkinOpacity) || double.IsInfinity(sizeConfig.SkinOpacity))
		{
			sizeConfig.SkinOpacity = 1.0;
		}
		if (sizeConfig.SkinOpacity < 0.0)
		{
			sizeConfig.SkinOpacity = 0.0;
		}
		if (sizeConfig.SkinOpacity > 1.0)
		{
			sizeConfig.SkinOpacity = 1.0;
		}
		suppressAppearanceChange = true;
		try
		{
			miShowDividers.Checked = false;
			miShowInnerDividers.Visible = false;
			miOuterRingGray.Checked = false;
		}
		finally
		{
			suppressAppearanceChange = false;
		}
		miClearSkinImage.Visible = true;
		miSkinOpacity.Visible = true;
		renderPanel.Invalidate();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__91_002D18()
	{
		suppressAppearanceChange = true;
		try
		{
			miShowDividers.Checked = !sizeConfig.ShowDividers.HasValue || sizeConfig.ShowDividers.Value;
			miShowInnerDividers.Checked = !sizeConfig.ShowInnerDividers.HasValue || sizeConfig.ShowInnerDividers.Value;
			miShowInnerDividers.Visible = miShowDividers.Checked;
			miOuterRingGray.Checked = !sizeConfig.OuterRingGray.HasValue || sizeConfig.OuterRingGray.Value;
			bool visible = !string.IsNullOrWhiteSpace((sizeConfig.DragAreaBackgroundImage ?? "").Trim());
			miClearSkinImage.Visible = visible;
			miSkinOpacity.Visible = visible;
		}
		finally
		{
			suppressAppearanceChange = false;
		}
		appearanceMenu.Show(btnAppearance, new Point(0, btnAppearance.Height));
	}
}
