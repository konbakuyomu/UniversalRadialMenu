using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

internal class StartupToastForm : Form
{
	private readonly Label lbl;

	private Timer closeTimer;

	private int cornerRadius;

	protected override bool ShowWithoutActivation => true;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams obj = base.CreateParams;
			obj.ExStyle = obj.ExStyle | 0x8000000 | 0x80;
			obj.ClassStyle |= 131072;
			return obj;
		}
	}

	public StartupToastForm()
	{
		cornerRadius = 16;
		base.FormBorderStyle = FormBorderStyle.None;
		base.ShowInTaskbar = false;
		base.TopMost = true;
		base.StartPosition = FormStartPosition.Manual;
		BackColor = Color.FromArgb(250, 250, 250);
		base.Opacity = 0.92;
		DoubleBuffered = true;
		lbl = new Label
		{
			Dock = DockStyle.Fill,
			TextAlign = ContentAlignment.MiddleCenter,
			Font = new Font("Microsoft YaHei UI", 14f, FontStyle.Bold),
			ForeColor = Color.FromArgb(35, 35, 35),
			Padding = new Padding(18, 0, 18, 0)
		};
		base.Controls.Add(lbl);
		base.Size = new Size(440, 92);
		UpdateLocation();
	}

	private void UpdateLocation()
	{
		Rectangle workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
		checked
		{
			int num = workingArea.Left + unchecked(checked(workingArea.Width - base.Width) / 2);
			int num2 = workingArea.Top + unchecked(checked(workingArea.Height - base.Height) / 2);
			base.Location = new Point(num, num2);
		}
	}

	public void SetText(string text)
	{
		lbl.Text = text ?? "";
		UpdateLocation();
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		ApplyRoundedRegion();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		Logger.Log("StartupToastForm: OnShown. TopMost=" + base.TopMost + ", Handle=" + base.Handle.ToString("X"));
		ApplyRoundedRegion();
	}

	private void ApplyRoundedRegion()
	{
		try
		{
			if (base.Width > 0 && base.Height > 0)
			{
				int num = Math.Max(0, Math.Min(cornerRadius, Math.Min(base.Width, base.Height) / 2));
				GraphicsPath graphicsPath = new GraphicsPath();
				checked
				{
					int num2 = num * 2;
					graphicsPath.StartFigure();
					graphicsPath.AddArc(0, 0, num2, num2, 180f, 90f);
					graphicsPath.AddArc(base.Width - num2, 0, num2, num2, 270f, 90f);
					graphicsPath.AddArc(base.Width - num2, base.Height - num2, num2, num2, 0f, 90f);
					graphicsPath.AddArc(0, base.Height - num2, num2, num2, 90f, 90f);
					graphicsPath.CloseFigure();
					if (base.Region != null)
					{
						base.Region.Dispose();
					}
					base.Region = new Region(graphicsPath);
					graphicsPath.Dispose();
				}
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		checked
		{
			try
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using Pen pen = new Pen(Color.FromArgb(90, 0, 0, 0), 1f);
				Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				int num = Math.Max(0, Math.Min(cornerRadius, unchecked(Math.Min(base.Width, base.Height) / 2)));
				GraphicsPath graphicsPath = new GraphicsPath();
				int num2 = num * 2;
				graphicsPath.AddArc(rectangle.Left, rectangle.Top, num2, num2, 180f, 90f);
				graphicsPath.AddArc(rectangle.Right - num2, rectangle.Top, num2, num2, 270f, 90f);
				graphicsPath.AddArc(rectangle.Right - num2, rectangle.Bottom - num2, num2, num2, 0f, 90f);
				graphicsPath.AddArc(rectangle.Left, rectangle.Bottom - num2, num2, num2, 90f, 90f);
				graphicsPath.CloseFigure();
				e.Graphics.DrawPath(pen, graphicsPath);
				graphicsPath.Dispose();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			base.OnPaint(e);
		}
	}

	public void AutoClose(int ms)
	{
		if (ms <= 0)
		{
			try
			{
				Close();
				return;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
				return;
			}
		}
		if (closeTimer == null)
		{
			closeTimer = new Timer();
			closeTimer.Tick += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__14_002D0();
			};
		}
		closeTimer.Interval = ms;
		closeTimer.Stop();
		closeTimer.Start();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__14_002D0()
	{
		try
		{
			closeTimer.Stop();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		try
		{
			Close();
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}
}
