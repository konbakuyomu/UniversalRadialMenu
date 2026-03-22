using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

internal class EditItemForm : Form
{
	private class ShortcutCaptureForm : Form
	{
		private Label lblValue;

		private Button btnOk;

		private string captured;

		public string CapturedShortcut => captured;

		public ShortcutCaptureForm()
		{
			captured = "";
			Text = "捕获快捷键";
			base.Size = new Size(420, 190);
			base.StartPosition = FormStartPosition.CenterParent;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.KeyPreview = true;
			Label label = new Label
			{
				Text = "请触发快捷键组合（按下非修饰键时捕获）",
				Location = new Point(16, 16),
				AutoSize = true
			};
			lblValue = new Label
			{
				Text = "",
				Location = new Point(16, 52),
				AutoSize = true,
				Font = new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Bold)
			};
			btnOk = new Button
			{
				Text = "确定",
				Location = new Point(230, 110),
				Size = new Size(80, 28),
				BackColor = Color.White,
				Enabled = false
			};
			Button button = new Button
			{
				Text = "取消",
				Location = new Point(320, 110),
				Size = new Size(80, 28),
				BackColor = Color.White,
				DialogResult = DialogResult.Cancel
			};
			btnOk.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__5_002D0();
			};
			base.Controls.AddRange(new Control[4] { label, lblValue, btnOk, button });
			base.AcceptButton = btnOk;
			base.CancelButton = button;
			base.KeyDown += OnKeyDownCapture;
		}

		private void OnKeyDownCapture(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.ControlKey || keyCode == Keys.ShiftKey || keyCode == Keys.Menu || keyCode == Keys.LControlKey || keyCode == Keys.RControlKey || keyCode == Keys.LShiftKey || keyCode == Keys.RShiftKey || keyCode == Keys.LMenu || keyCode == Keys.RMenu)
			{
				e.SuppressKeyPress = true;
				return;
			}
			List<string> list = new List<string>();
			if ((e.Modifiers & Keys.Control) == Keys.Control)
			{
				list.Add("Ctrl");
			}
			if ((e.Modifiers & Keys.Alt) == Keys.Alt)
			{
				list.Add("Alt");
			}
			if ((e.Modifiers & Keys.Shift) == Keys.Shift)
			{
				list.Add("Shift");
			}
			string keyName = GetKeyName(keyCode);
			if (!string.IsNullOrWhiteSpace(keyName))
			{
				list.Add(keyName);
			}
			string text = string.Join("+", list);
			lblValue.Text = text;
			captured = text;
			btnOk.Enabled = true;
			e.SuppressKeyPress = true;
		}

		private static string GetKeyName(Keys k)
		{
			if (k >= Keys.D0 && k <= Keys.D9)
			{
				return Strings.ChrW((int)checked(48 + (k - 48))).ToString();
			}
			if (k >= Keys.A && k <= Keys.Z)
			{
				return Strings.ChrW((int)checked(65 + (k - 65))).ToString();
			}
			return new KeysConverter().ConvertToString(k);
		}

		[SpecialName]
		[CompilerGenerated]
		private void _Lambda_0024__5_002D0()
		{
			base.DialogResult = DialogResult.OK;
		}
	}

	private class KeySelectionForm : Form
	{
		[CompilerGenerated]
		internal sealed class _Closure_0024__12_002D0
		{
			public ListBox _0024VB_0024Local_lst;

			public KeySelectionForm _0024VB_0024Me;

			[SpecialName]
			internal void _Lambda_0024__R4(object a0, EventArgs a1)
			{
				_Lambda_0024__0();
			}

			[SpecialName]
			internal void _Lambda_0024__0()
			{
				string text = _0024VB_0024Local_lst.SelectedItem as string;
				_0024VB_0024Me.selectedKey = text ?? "";
				_0024VB_0024Me.UpdatePreview();
			}
		}

		private CheckBox chkCtrl;

		private CheckBox chkAlt;

		private CheckBox chkShift;

		private TabControl tab;

		private Label lblPreview;

		private Button btnOk;

		private string selectedKey;

		public string SelectedShortcut { get; set; }

		public KeySelectionForm()
		{
			selectedKey = "";
			Text = "选择按键";
			base.Size = new Size(560, 520);
			base.StartPosition = FormStartPosition.CenterParent;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			chkCtrl = new CheckBox
			{
				Text = "Ctrl",
				Location = new Point(16, 14),
				AutoSize = true
			};
			chkAlt = new CheckBox
			{
				Text = "Alt",
				Location = new Point(86, 14),
				AutoSize = true
			};
			chkShift = new CheckBox
			{
				Text = "Shift",
				Location = new Point(146, 14),
				AutoSize = true
			};
			tab = new TabControl
			{
				Location = new Point(16, 44),
				Size = new Size(512, 360)
			};
			tab.TabPages.Add(CreateTab("数字", BuildDigits()));
			tab.TabPages.Add(CreateTab("字符", BuildChars()));
			tab.TabPages.Add(CreateTab("方向", BuildDirections()));
			tab.TabPages.Add(CreateTab("功能键", BuildFunctionKeysAll()));
			tab.TabPages.Add(CreateTab("F键", BuildFKeysAll()));
			tab.TabPages.Add(CreateTab("数字键盘", BuildNumpadAll()));
			tab.TabPages.Add(CreateTab("媒体", BuildMedia()));
			tab.TabPages.Add(CreateTab("特殊", BuildSpecial()));
			tab.TabPages.Add(CreateTab("鼠标", BuildMouse()));
			Label label = new Label
			{
				Text = "预览：",
				Location = new Point(16, 414),
				AutoSize = true
			};
			lblPreview = new Label
			{
				Text = "",
				Location = new Point(64, 412),
				AutoSize = true,
				Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold)
			};
			btnOk = new Button
			{
				Text = "确定",
				Location = new Point(356, 452),
				Size = new Size(80, 28),
				BackColor = Color.White,
				Enabled = false,
				DialogResult = DialogResult.OK
			};
			Button button = new Button
			{
				Text = "取消",
				Location = new Point(448, 452),
				Size = new Size(80, 28),
				BackColor = Color.White,
				DialogResult = DialogResult.Cancel
			};
			base.Controls.AddRange(new Control[8] { chkCtrl, chkAlt, chkShift, tab, label, lblPreview, btnOk, button });
			base.AcceptButton = btnOk;
			base.CancelButton = button;
			chkCtrl.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				UpdatePreview();
			};
			chkAlt.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				UpdatePreview();
			};
			chkShift.CheckedChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				UpdatePreview();
			};
			UpdatePreview();
		}

		private TabPage CreateTab(string title, List<string> items)
		{
			_Closure_0024__12_002D0 CS_0024_003C_003E8__locals7 = new _Closure_0024__12_002D0();
			CS_0024_003C_003E8__locals7._0024VB_0024Me = this;
			TabPage tabPage = new TabPage(title);
			CS_0024_003C_003E8__locals7._0024VB_0024Local_lst = new ListBox
			{
				Dock = DockStyle.Fill,
				IntegralHeight = false
			};
			CS_0024_003C_003E8__locals7._0024VB_0024Local_lst.Items.AddRange(items.Cast<object>().ToArray());
			CS_0024_003C_003E8__locals7._0024VB_0024Local_lst.SelectedIndexChanged += [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals7._Lambda_0024__0();
			};
			CS_0024_003C_003E8__locals7._0024VB_0024Local_lst.DoubleClick += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__12_002D1();
			};
			tabPage.Controls.Add(CS_0024_003C_003E8__locals7._0024VB_0024Local_lst);
			return tabPage;
		}

		private void UpdatePreview()
		{
			List<string> list = new List<string>();
			if (chkCtrl.Checked)
			{
				list.Add("Ctrl");
			}
			if (chkAlt.Checked)
			{
				list.Add("Alt");
			}
			if (chkShift.Checked)
			{
				list.Add("Shift");
			}
			if (!string.IsNullOrWhiteSpace(selectedKey))
			{
				list.Add(selectedKey);
			}
			string text = (SelectedShortcut = string.Join("+", list));
			lblPreview.Text = text;
			btnOk.Enabled = !string.IsNullOrWhiteSpace(selectedKey);
		}

		private static List<string> BuildDigits()
		{
			List<string> list = new List<string>();
			int num = 0;
			do
			{
				list.Add(num.ToString());
				num = checked(num + 1);
			}
			while (num <= 9);
			return list;
		}

		private static List<string> BuildChars()
		{
			List<string> list = new List<string>();
			int num = 0;
			checked
			{
				do
				{
					list.Add(Strings.ChrW(65 + num).ToString());
					num++;
				}
				while (num <= 25);
				list.AddRange(new string[11]
				{
					"`", "-", "=", "[", "]", "\\", ";", "'", ",", ".",
					"/"
				});
				return list;
			}
		}

		private static List<string> BuildDirections()
		{
			return new List<string> { "Up", "Down", "Left", "Right", "Home", "End", "PgUp", "PgDn" };
		}

		private static List<string> BuildFunctionKeysAll()
		{
			return new List<string>
			{
				"Enter", "Tab", "Esc", "Space", "Backspace", "Break", "Delete", "Insert", "CapsLock", "NumLock",
				"ScrollLock", "PrintScreen", "Pause", "Apps", "Help"
			};
		}

		private static List<string> BuildFKeysAll()
		{
			List<string> list = new List<string>();
			int num = 1;
			do
			{
				list.Add("F" + num);
				num = checked(num + 1);
			}
			while (num <= 24);
			return list;
		}

		private static List<string> BuildNumpadAll()
		{
			List<string> list = new List<string>();
			int num = 0;
			do
			{
				list.Add("NumPad" + num);
				num = checked(num + 1);
			}
			while (num <= 9);
			list.AddRange(new string[5] { "Add", "Subtract", "Multiply", "Divide", "Decimal" });
			return list;
		}

		private static List<string> BuildMedia()
		{
			return new List<string> { "VOLUME_MUTE", "VOLUME_DOWN", "VOLUME_UP", "MEDIA_PREV_TRACK", "MEDIA_NEXT_TRACK", "MEDIA_STOP", "MEDIA_PLAY_PAUSE" };
		}

		private static List<string> BuildSpecial()
		{
			return new List<string>
			{
				"ACCEPT", "ATTN", "CANCEL", "CLEAR", "CRSEL", "EREOF", "EXSEL", "EXECUTE", "HELP", "NONAME",
				"PA1", "PACKET", "PLAY", "PROCESSKEY", "SELECT", "SLEEP", "ZOOM", "BROWSER_BACK", "BROWSER_FORWARD", "BROWSER_REFRESH",
				"BROWSER_STOP", "BROWSER_SEARCH", "BROWSER_FAVORITES", "BROWSER_HOME", "LAUNCH_MAIL", "LAUNCH_MEDIA_SELECT", "LAUNCH_APP1", "LAUNCH_APP2"
			};
		}

		private static List<string> BuildMouse()
		{
			return new List<string> { "LBUTTON", "RBUTTON", "MBUTTON", "XBUTTON1", "XBUTTON2" };
		}

		[SpecialName]
		[CompilerGenerated]
		private void _Lambda_0024__12_002D1()
		{
			if (btnOk.Enabled)
			{
				base.DialogResult = DialogResult.OK;
			}
		}
	}

	private readonly MenuItemConfig item;

	private TextBox txtText;

	private TextBox txtDisplayName;

	private PictureBox picIcon;

	private CheckBox chkEnabled;

	private ComboBox cboOperationType;

	private double dpiScaleValue;

	private Panel pnlQuickerAction;

	private TextBox txtParam;

	private string quickerActionId;

	private Panel pnlSubMenu;

	private TextBox txtSubMenu;

	private Panel pnlShortcut;

	private TextBox txtShortcut;

	private Button btnCaptureShortcut;

	private Button btnSelectShortcut;

	private Panel pnlTypedText;

	private TextBox txtTypedText;

	private Panel pnlPasteText;

	private TextBox txtPasteText;

	private Panel pnlRun;

	private TextBox txtRunPath;

	private TextBox txtRunParams;

	private TextBox txtIconPath;

	private CheckBox chkUseFirstChar;

	private CheckBox chkOnlyShowIcon;

	private Button btnOk;

	private Button btnCancel;

	private Image previewIcon;

	private bool suppressPreviewUpdate;

	public EditItemForm(MenuItemConfig item)
	{
		dpiScaleValue = 1.0;
		quickerActionId = "";
		suppressPreviewUpdate = false;
		this.item = item;
		Text = "编辑按钮";
		try
		{
			base.Icon = RadialMenuController.CreateTitleIcon("编辑");
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		dpiScaleValue = GetDpiScale();
		Size size = new Size(600, 580);
		Rectangle workingArea;
		try
		{
			workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			workingArea = Screen.PrimaryScreen.WorkingArea;
			ProjectData.ClearProjectError();
		}
		checked
		{
			int val = (int)Math.Round((double)size.Width * dpiScaleValue);
			int val2 = (int)Math.Round((double)size.Height * dpiScaleValue);
			int val3 = Math.Max(360, (int)Math.Floor((double)workingArea.Width * 0.95));
			int val4 = Math.Max(280, (int)Math.Floor((double)workingArea.Height * 0.95));
			val = Math.Max(360, Math.Min(val, val3));
			val2 = Math.Max(280, Math.Min(val2, val4));
			base.Size = new Size(val, val2);
			int num = Math.Max(360, Math.Min((int)Math.Round((double)size.Width * dpiScaleValue), Math.Max(360, workingArea.Width - 80)));
			int num2 = Math.Max(280, Math.Min((int)Math.Round((double)size.Height * dpiScaleValue), Math.Max(280, workingArea.Height - 80)));
			MinimumSize = new Size(num, num2);
			base.StartPosition = FormStartPosition.CenterParent;
			AutoScroll = true;
			base.AutoScaleMode = AutoScaleMode.Dpi;
			InitializeUI();
			LoadValues();
			base.Shown += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__30_002D0();
			};
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

	private int ScaleI(int v)
	{
		return Math.Max(1, checked((int)Math.Round((double)v * dpiScaleValue)));
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		try
		{
			if (picIcon != null)
			{
				picIcon.Image = null;
			}
			if (previewIcon != null)
			{
				previewIcon.Dispose();
				previewIcon = null;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		base.OnFormClosing(e);
	}

	private void InitializeUI()
	{
		int num = ScaleI(16);
		int num2 = ScaleI(96);
		checked
		{
			int num3 = num + num2 + ScaleI(8);
			int num4 = ScaleI(360);
			int num5 = ScaleI(14);
			picIcon = new PictureBox
			{
				Location = new Point(num, num5),
				Size = new Size(ScaleI(48), ScaleI(48)),
				SizeMode = PictureBoxSizeMode.Zoom,
				BorderStyle = BorderStyle.FixedSingle
			};
			int num6 = picIcon.Right + ScaleI(8);
			int num7 = ScaleI(60);
			int num8 = num6 + num7 + ScaleI(8);
			int num9 = ScaleI(260);
			Label label = new Label
			{
				Text = "图\u3000标：",
				Location = new Point(num6, num5 + ScaleI(2)),
				Size = new Size(num7, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtIconPath = new TextBox
			{
				Location = new Point(num8, num5),
				Width = num9
			};
			txtIconPath.TextChanged += UpdateIconPreview;
			chkOnlyShowIcon = new CheckBox
			{
				Text = "仅显示图标",
				Location = new Point(num8 + num9 + ScaleI(10), num5 + ScaleI(2)),
				AutoSize = true
			};
			int num10 = num5 + ScaleI(26);
			Label label2 = new Label
			{
				Text = "显示名：",
				Location = new Point(num6, num10 + ScaleI(2)),
				Size = new Size(num7, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtDisplayName = new TextBox
			{
				Location = new Point(num8, num10),
				Width = num9
			};
			chkUseFirstChar = new CheckBox
			{
				Text = "使用首字符",
				Location = new Point(num8 + num9 + ScaleI(10), num10 + ScaleI(2)),
				AutoSize = true
			};
			chkUseFirstChar.CheckedChanged += UpdateIconPreview;
			txtDisplayName.TextChanged += UpdateIconPreview;
			int num11 = num5 + ScaleI(62);
			Label label3 = new Label
			{
				Text = "动作名：",
				Location = new Point(num, num11 + ScaleI(2)),
				Size = new Size(num2, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtText = new TextBox
			{
				Location = new Point(num3, num11),
				Width = num4
			};
			txtText.TextChanged += UpdateIconPreview;
			num11 += ScaleI(34);
			Label label4 = new Label
			{
				Text = "操作类型：",
				Location = new Point(num, num11 + ScaleI(2)),
				Size = new Size(num2, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			cboOperationType = new ComboBox
			{
				Location = new Point(num3, num11),
				Width = num4,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			cboOperationType.Items.AddRange(new object[8] { "Quicker动作", "子菜单", "发送快捷键", "键入文本", "粘贴文本", "打开或运行（文件/目录/命令/网址）", "设置", "退出" });
			cboOperationType.SelectedIndexChanged += OnOperationTypeChanged;
			num11 += ScaleI(34);
			Panel panel = new Panel
			{
				Location = new Point(ScaleI(16), num11),
				Size = new Size(ScaleI(464), ScaleI(210))
			};
			pnlQuickerAction = BuildQuickerActionPanel();
			pnlSubMenu = BuildSubMenuPanel();
			pnlShortcut = BuildShortcutPanel();
			pnlTypedText = BuildTypedTextPanel();
			pnlPasteText = BuildPasteTextPanel();
			pnlRun = BuildRunPanel();
			panel.Controls.AddRange(new Control[6] { pnlQuickerAction, pnlSubMenu, pnlShortcut, pnlTypedText, pnlPasteText, pnlRun });
			num11 += panel.Height + ScaleI(10);
			chkEnabled = new CheckBox
			{
				Text = "启用",
				Location = new Point(num3, num11),
				AutoSize = true
			};
			num11 += ScaleI(40);
			btnOk = new Button
			{
				Text = "确定",
				Size = new Size(ScaleI(80), ScaleI(28)),
				BackColor = Color.White
			};
			btnCancel = new Button
			{
				Text = "取消",
				Size = new Size(ScaleI(80), ScaleI(28)),
				BackColor = Color.White
			};
			int num12 = ScaleI(16);
			int num13 = ScaleI(12);
			btnCancel.Location = new Point(Math.Max(0, base.ClientSize.Width - num12 - btnCancel.Width), num11);
			btnOk.Location = new Point(Math.Max(0, btnCancel.Left - num13 - btnOk.Width), num11);
			btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOk.Click += SaveAndClose;
			btnCancel.Click += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__34_002D0();
			};
			base.Controls.AddRange(new Control[15]
			{
				picIcon, label, txtIconPath, chkOnlyShowIcon, label2, txtDisplayName, chkUseFirstChar, label3, txtText, label4,
				cboOperationType, panel, chkEnabled, btnOk, btnCancel
			});
			base.AcceptButton = btnOk;
			base.CancelButton = btnCancel;
		}
	}

	private void UpdateIconPreview(object sender, EventArgs e)
	{
		if (suppressPreviewUpdate)
		{
			return;
		}
		string text = txtIconPath.Text.Trim();
		bool flag = chkUseFirstChar.Checked;
		string text2 = ((!string.IsNullOrWhiteSpace(txtDisplayName.Text)) ? txtDisplayName.Text : txtText.Text);
		try
		{
			if (picIcon != null)
			{
				picIcon.Image = null;
			}
			if (previewIcon != null)
			{
				previewIcon.Dispose();
				previewIcon = null;
			}
			if (flag)
			{
				Image image = RadialMenuController.CreateLetterIcon(text2);
				if (image != null)
				{
					previewIcon = new Bitmap(image);
				}
				else
				{
					previewIcon = null;
				}
			}
			else if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("fa:", StringComparison.OrdinalIgnoreCase))
				{
					previewIcon = null;
				}
				else
				{
					previewIcon = RadialMenuController.LoadIcon(text);
				}
			}
			else
			{
				previewIcon = null;
			}
			picIcon.Image = previewIcon;
			txtIconPath.Enabled = !flag;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private Panel BuildQuickerActionPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			Label label = new Label
			{
				Text = "参数：",
				Location = new Point(0, ScaleI(2)),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtParam = new TextBox
			{
				Location = new Point(num4, ScaleI(2)),
				Width = Math.Max(10, num - num4),
				Height = Math.Max(10, num2 - ScaleI(4)),
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				AcceptsReturn = true,
				AcceptsTab = true
			};
			txtParam.KeyDown += OnMultiTextBoxKeyDown;
			panel.Controls.AddRange(new Control[2] { label, txtParam });
			return panel;
		}
	}

	private Panel BuildSubMenuPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			Label label = new Label
			{
				Text = "子菜单名：",
				Location = new Point(0, ScaleI(2)),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtSubMenu = new TextBox
			{
				Location = new Point(num4, ScaleI(2)),
				Width = Math.Max(10, num - num4)
			};
			panel.Controls.AddRange(new Control[2] { label, txtSubMenu });
			return panel;
		}
	}

	private Panel BuildShortcutPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			int num5 = ScaleI(60);
			int num6 = ScaleI(26);
			int num7 = ScaleI(26);
			int num8 = ScaleI(10);
			int num9 = ScaleI(4);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			Label label = new Label
			{
				Text = "发送快捷键：",
				Location = new Point(0, ScaleI(2)),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			btnSelectShortcut = new Button
			{
				Text = "▼",
				Location = new Point(Math.Max(num4, num - num6), 0),
				Size = new Size(num6, num7),
				BackColor = Color.White
			};
			btnCaptureShortcut = new Button
			{
				Text = "捕获...",
				Location = new Point(Math.Max(num4, btnSelectShortcut.Left - num9 - num5), 0),
				Size = new Size(num5, num7),
				BackColor = Color.White
			};
			txtShortcut = new TextBox
			{
				Location = new Point(num4, ScaleI(2)),
				Width = Math.Max(10, btnCaptureShortcut.Left - num8 - num4)
			};
			btnCaptureShortcut.Click += CaptureShortcut;
			btnSelectShortcut.Click += SelectShortcutFromList;
			panel.Controls.AddRange(new Control[4] { label, txtShortcut, btnCaptureShortcut, btnSelectShortcut });
			return panel;
		}
	}

	private Panel BuildTypedTextPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			Label label = new Label
			{
				Text = "键入文本：",
				Location = new Point(0, ScaleI(2)),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtTypedText = new TextBox
			{
				Location = new Point(num4, ScaleI(2)),
				Width = Math.Max(10, num - num4),
				Height = Math.Max(10, num2 - ScaleI(4)),
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				AcceptsReturn = true,
				AcceptsTab = true
			};
			txtTypedText.KeyDown += OnMultiTextBoxKeyDown;
			panel.Controls.AddRange(new Control[2] { label, txtTypedText });
			return panel;
		}
	}

	private Panel BuildPasteTextPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			Label label = new Label
			{
				Text = "粘贴文本：",
				Location = new Point(0, ScaleI(2)),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtPasteText = new TextBox
			{
				Location = new Point(num4, ScaleI(2)),
				Width = Math.Max(10, num - num4),
				Height = Math.Max(10, num2 - ScaleI(4)),
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				AcceptsReturn = true,
				AcceptsTab = true
			};
			txtPasteText.KeyDown += OnMultiTextBoxKeyDown;
			panel.Controls.AddRange(new Control[2] { label, txtPasteText });
			return panel;
		}
	}

	private void OnMultiTextBoxKeyDown(object sender, KeyEventArgs e)
	{
		if (e != null && e.Control && e.KeyCode == Keys.Return)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			SaveAndClose(btnOk, EventArgs.Empty);
		}
	}

	private Panel BuildRunPanel()
	{
		int num = ScaleI(464);
		int num2 = ScaleI(210);
		int num3 = ScaleI(96);
		checked
		{
			int num4 = num3 + ScaleI(8);
			Panel panel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(num, num2),
				Visible = false
			};
			int num5 = ScaleI(2);
			int num6 = ScaleI(34);
			Label label = new Label
			{
				Text = "路径/网址/命令：",
				Location = new Point(0, num5),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtRunPath = new TextBox
			{
				Location = new Point(num4, num5),
				Width = Math.Max(10, num - num4)
			};
			Label label2 = new Label
			{
				Text = "参数：",
				Location = new Point(0, num6),
				Size = new Size(num3, ScaleI(22)),
				TextAlign = ContentAlignment.MiddleRight
			};
			txtRunParams = new TextBox
			{
				Location = new Point(num4, num6),
				Width = Math.Max(10, num - num4),
				Height = Math.Max(10, num2 - num6),
				Multiline = true,
				ScrollBars = ScrollBars.Vertical,
				AcceptsReturn = true,
				AcceptsTab = true
			};
			txtRunParams.KeyDown += OnMultiTextBoxKeyDown;
			panel.Controls.AddRange(new Control[4] { label, txtRunPath, label2, txtRunParams });
			return panel;
		}
	}

	private void LoadValues()
	{
		suppressPreviewUpdate = true;
		try
		{
			txtText.Text = item.Text;
			txtDisplayName.Text = item.DisplayName;
			chkEnabled.Checked = item.Enabled;
			txtIconPath.Text = item.IconPath;
			chkUseFirstChar.Checked = item.UseFirstCharIcon;
			chkOnlyShowIcon.Checked = item.OnlyShowIcon;
			txtSubMenu.Text = item.SubMenuName;
			txtShortcut.Text = item.Shortcut;
			txtTypedText.Text = item.TypedText;
			txtPasteText.Text = RadialMenuController.NormalizeNewLinesToCrLf(item.PasteText);
			txtRunPath.Text = (string.IsNullOrWhiteSpace((item.RunPath ?? "").Trim()) ? (item.Command ?? "").Trim() : item.RunPath);
			txtRunParams.Text = item.RunParams;
		}
		finally
		{
			suppressPreviewUpdate = false;
		}
		string actionId = "";
		string param = "";
		ParseQuickerActionSpec(item.Command, ref actionId, ref param);
		if (string.IsNullOrWhiteSpace((item.IconPath ?? "").Trim()) && !string.IsNullOrWhiteSpace(actionId))
		{
			string quickerIconSpec = RadialMenuSettingsForm.GetQuickerIconSpec(actionId);
			if (!string.IsNullOrWhiteSpace(quickerIconSpec))
			{
				item.IconPath = quickerIconSpec;
				txtIconPath.Text = quickerIconSpec;
			}
		}
		txtParam.Text = param;
		quickerActionId = actionId;
		txtParam.Enabled = true;
		txtParam.ReadOnly = false;
		string text = (item.OperationType ?? "").Trim();
		if (string.Equals(item.Command, "_SETTINGS", StringComparison.OrdinalIgnoreCase))
		{
			text = "设置";
		}
		else if (string.Equals(item.Command, "_EXIT", StringComparison.OrdinalIgnoreCase))
		{
			text = "退出";
		}
		else if (item.IsSubMenu)
		{
			text = "子菜单";
		}
		else if (string.IsNullOrWhiteSpace(text))
		{
			text = "Quicker动作";
		}
		if (cboOperationType.Items.Contains(text))
		{
			cboOperationType.SelectedItem = text;
		}
		else
		{
			cboOperationType.SelectedIndex = 0;
		}
		OnOperationTypeChanged(null, EventArgs.Empty);
		UpdateIconPreview(null, EventArgs.Empty);
	}

	private void OnOperationTypeChanged(object sender, EventArgs e)
	{
		string a = (cboOperationType.SelectedItem as string) ?? "";
		pnlQuickerAction.Visible = string.Equals(a, "Quicker动作", StringComparison.OrdinalIgnoreCase);
		pnlSubMenu.Visible = string.Equals(a, "子菜单", StringComparison.OrdinalIgnoreCase);
		pnlShortcut.Visible = string.Equals(a, "发送快捷键", StringComparison.OrdinalIgnoreCase);
		pnlTypedText.Visible = string.Equals(a, "键入文本", StringComparison.OrdinalIgnoreCase);
		pnlPasteText.Visible = string.Equals(a, "粘贴文本", StringComparison.OrdinalIgnoreCase);
		pnlRun.Visible = string.Equals(a, "打开或运行（文件/目录/命令/网址）", StringComparison.OrdinalIgnoreCase);
		if (pnlQuickerAction.Visible)
		{
			txtParam.Enabled = true;
			txtParam.ReadOnly = false;
		}
	}

	private void CaptureShortcut(object sender, EventArgs e)
	{
		using ShortcutCaptureForm shortcutCaptureForm = new ShortcutCaptureForm();
		if (shortcutCaptureForm.ShowDialog(this) == DialogResult.OK)
		{
			txtShortcut.Text = shortcutCaptureForm.CapturedShortcut;
		}
	}

	private void SelectShortcutFromList(object sender, EventArgs e)
	{
		using KeySelectionForm keySelectionForm = new KeySelectionForm();
		if (keySelectionForm.ShowDialog(this) == DialogResult.OK)
		{
			string text = (keySelectionForm.SelectedShortcut ?? "").Trim();
			if (text.Length > 0)
			{
				txtShortcut.Text = text;
			}
		}
	}

	private void SaveAndClose(object sender, EventArgs e)
	{
		item.Text = txtText.Text;
		item.DisplayName = txtDisplayName.Text;
		item.Enabled = chkEnabled.Checked;
		item.IconPath = txtIconPath.Text.Trim();
		item.UseFirstCharIcon = chkUseFirstChar.Checked;
		item.OnlyShowIcon = chkOnlyShowIcon != null && chkOnlyShowIcon.Checked;
		string text = (cboOperationType.SelectedItem as string) ?? "";
		item.OperationType = text;
		switch (text)
		{
		case "Quicker动作":
			item.IsSubMenu = false;
			item.SubMenuName = "";
			if (!string.IsNullOrWhiteSpace(quickerActionId))
			{
				string text2 = quickerActionId.Trim();
				string text3 = EncodeQuickerParamNewLines(txtParam.Text ?? "");
				if (text3.Length > 0)
				{
					text2 = text2 + "?" + text3;
				}
				item.Command = "QCAD_ACTION:" + text2;
			}
			break;
		case "设置":
			item.Text = "设置";
			item.DisplayName = "";
			item.Enabled = true;
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.Command = "_SETTINGS";
			item.ColorArgb = ColorTranslator.FromHtml("#5b87ff").ToArgb();
			break;
		case "退出":
			item.Text = "退出";
			item.DisplayName = "";
			item.Enabled = true;
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.Command = "_EXIT";
			item.ColorArgb = ColorTranslator.FromHtml("#666666").ToArgb();
			break;
		case "子菜单":
			item.IsSubMenu = true;
			item.SubMenuName = txtSubMenu.Text.Trim();
			item.Command = "";
			break;
		case "发送快捷键":
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.Shortcut = (txtShortcut.Text ?? "").Trim();
			item.Command = "";
			break;
		case "键入文本":
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.TypedText = txtTypedText.Text ?? "";
			item.Command = "";
			break;
		case "粘贴文本":
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.PasteText = RadialMenuController.NormalizeNewLinesToLf(txtPasteText.Text);
			item.Command = "";
			break;
		case "打开或运行（文件/目录/命令/网址）":
			item.IsSubMenu = false;
			item.SubMenuName = "";
			item.RunPath = (txtRunPath.Text ?? "").Trim();
			item.RunParams = txtRunParams.Text ?? "";
			item.Command = item.RunPath;
			break;
		default:
			item.IsSubMenu = false;
			item.SubMenuName = "";
			break;
		}
		base.DialogResult = DialogResult.OK;
	}

	private static string EncodeQuickerParamNewLines(string param)
	{
		string text = RadialMenuController.NormalizeNewLinesToLf(param);
		if (text.Length == 0)
		{
			return "";
		}
		return text.Replace("\n", "%0D%0A");
	}

	private static string DecodeQuickerParamNewLines(string param)
	{
		return RadialMenuController.DecodeQuickerPercentNewLines(param);
	}

	private static int HexNibble(char c)
	{
		checked
		{
			if (c >= '0' && c <= '9')
			{
				return c - 48;
			}
			char c2 = char.ToUpperInvariant(c);
			if (c2 >= 'A' && c2 <= 'F')
			{
				return 10 + (c2 - 65);
			}
			return -1;
		}
	}

	private static void ParseQuickerActionSpec(string cmd, ref string actionId, ref string param)
	{
		actionId = "";
		param = "";
		if (string.IsNullOrWhiteSpace(cmd) || !cmd.StartsWith("QCAD_ACTION:", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		string text = cmd.Substring("QCAD_ACTION:".Length).Trim();
		if (text.Length != 0)
		{
			int num = text.IndexOf('?');
			if (num < 0)
			{
				actionId = text;
				return;
			}
			actionId = text.Substring(0, num);
			param = RadialMenuController.NormalizeNewLinesToCrLf(DecodeQuickerParamNewLines(text.Substring(checked(num + 1))));
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__30_002D0()
	{
		if (txtDisplayName != null)
		{
			BeginInvoke((Action)([SpecialName] () =>
			{
				txtDisplayName.Focus();
				txtDisplayName.SelectAll();
			}));
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__34_002D0()
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
