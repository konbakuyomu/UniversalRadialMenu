using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace UniversalRadialMenu;

internal sealed class TextLayoutUtil
{
	private TextLayoutUtil()
	{
	}

	internal static List<string> WrapLines(Graphics g, string text, Font font, float maxWidth)
	{
		List<string> list = new List<string>();
		string[] array = (text ?? "").Replace("\r\n", "\n").Split(new string[1] { "\n" }, StringSplitOptions.None);
		checked
		{
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i] ?? "";
				if (text2.Length == 0)
				{
					list.Add("");
					continue;
				}
				StringBuilder stringBuilder = new StringBuilder();
				int num = text2.Length - 1;
				for (int j = 0; j <= num; j++)
				{
					char c = text2[j];
					if (stringBuilder.Length == 0 && char.IsWhiteSpace(c))
					{
						continue;
					}
					stringBuilder.Append(c);
					if (g.MeasureString(stringBuilder.ToString(), font).Width > maxWidth && stringBuilder.Length > 1)
					{
						stringBuilder.Length--;
						list.Add(stringBuilder.ToString().TrimEnd());
						stringBuilder.Clear();
						if (!char.IsWhiteSpace(c))
						{
							stringBuilder.Append(c);
						}
					}
				}
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString().TrimEnd());
				}
			}
			return list;
		}
	}

	internal static List<string> FitLines(Graphics g, string text, Font baseFont, float maxWidth, float maxHeight, ref Font usedFont, ref float lineHeight, float minFontSize = 6f)
	{
		if (maxWidth < 1f)
		{
			maxWidth = 1f;
		}
		if (maxHeight < 1f)
		{
			maxHeight = 1f;
		}
		Font font = baseFont ?? SystemFonts.DefaultFont;
		string text2 = text ?? "";
		bool num = WrapLines(g, text2, font, maxWidth).Count > 1;
		Font font2 = font;
		checked
		{
			if (num)
			{
				float num2 = Math.Max(minFontSize, font.Size);
				float num3 = ((num2 >= 14f) ? 1f : 0.5f);
				float num4 = num2;
				while (num4 - num3 >= minFontSize)
				{
					num4 -= num3;
					Font font3 = null;
					try
					{
						font3 = new Font(font.FontFamily, num4, font.Style);
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						font3 = null;
						ProjectData.ClearProjectError();
					}
					if (font3 == null)
					{
						break;
					}
					float height = font3.GetHeight(g);
					if (Math.Max(1, (int)Math.Floor(maxHeight / height)) >= 2)
					{
						font2 = font3;
						break;
					}
					try
					{
						font3.Dispose();
					}
					catch (Exception projectError2)
					{
						ProjectData.SetProjectError(projectError2);
						ProjectData.ClearProjectError();
					}
				}
			}
			usedFont = font2;
			lineHeight = font2.GetHeight(g);
			int num5 = Math.Max(1, (int)Math.Floor(maxHeight / lineHeight));
			List<string> list = WrapLines(g, text2, font2, maxWidth);
			if (list.Count > num5)
			{
				list = list.Take(num5).ToList();
			}
			return list;
		}
	}
}
