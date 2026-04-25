using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnUtilityText
	{
		public static Vector3 CalculateTextOffset(TextMesh textMesh, float leftAlignOffset, float centerAlignOffset, float rightAlignOffset, float upperAnchorOffset, float middleAnchorOffset, float lowerAnchorOffset, TextAnchor anchor, Vector2 range)
		{
			Vector3 zero = Vector3.zero;
			float num = 0f;
			if (anchor == TextAnchor.LowerLeft || anchor == TextAnchor.MiddleLeft || anchor == TextAnchor.UpperLeft)
			{
				num = -range.x * 0.5f + leftAlignOffset;
			}
			else if (anchor == TextAnchor.LowerCenter || anchor == TextAnchor.MiddleCenter || anchor == TextAnchor.UpperCenter)
			{
				num = centerAlignOffset;
			}
			else if (anchor == TextAnchor.LowerRight || anchor == TextAnchor.MiddleRight || anchor == TextAnchor.UpperRight)
			{
				num = range.x * 0.5f + rightAlignOffset;
			}
			float num2 = 0f;
			if (anchor == TextAnchor.UpperLeft || anchor == TextAnchor.UpperCenter || anchor == TextAnchor.UpperRight)
			{
				num2 = range.y * 0.5f + upperAnchorOffset;
			}
			else if (anchor == TextAnchor.MiddleLeft || anchor == TextAnchor.MiddleCenter || anchor == TextAnchor.MiddleRight)
			{
				num2 = middleAnchorOffset;
			}
			else if (anchor == TextAnchor.LowerLeft || anchor == TextAnchor.LowerCenter || anchor == TextAnchor.LowerRight)
			{
				num2 = -range.y * 0.5f + lowerAnchorOffset;
			}
			return new Vector3(num, num2, 0f);
		}

		public static void CopyTextMeshValue(TextMesh srcTextMesh, TextMesh destTextMesh)
		{
			if (srcTextMesh != null && destTextMesh != null)
			{
				destTextMesh.alignment = srcTextMesh.alignment;
				destTextMesh.anchor = srcTextMesh.anchor;
				destTextMesh.characterSize = srcTextMesh.characterSize;
				destTextMesh.font = srcTextMesh.font;
				destTextMesh.fontSize = srcTextMesh.fontSize;
				destTextMesh.fontStyle = srcTextMesh.fontStyle;
				destTextMesh.lineSpacing = srcTextMesh.lineSpacing;
				destTextMesh.offsetZ = srcTextMesh.offsetZ;
				destTextMesh.richText = srcTextMesh.richText;
				destTextMesh.tabSize = srcTextMesh.tabSize;
				destTextMesh.gameObject.layer = srcTextMesh.gameObject.layer;
			}
		}

		public static string ConvertRichTextToNormal(string srcText)
		{
			srcText = AnUtilityText.RemoveStringFromText(srcText, AnValue.TextSettingPrefix);
			srcText = AnUtilityText.RemoveStringFromText(srcText, AnValue.TextColorPrefix);
			srcText = AnUtilityText.RemoveStringFromText(srcText, AnValue.TextColorSuffix);
			return srcText;
		}

		public static string RemoveStringFromText(string srcText, string prefixString)
		{
			int num = srcText.IndexOf(prefixString);
			if (num < 0)
			{
				return srcText;
			}
			int num2 = srcText.IndexOf(">", num);
			if (num2 < 0)
			{
				return srcText;
			}
			return srcText.Remove(num, num2 - num + 1);
		}

		public static string GetRichTextContent(string srcText, string prefixString)
		{
			int num = srcText.IndexOf(prefixString);
			if (num < 0)
			{
				return null;
			}
			int num2 = srcText.IndexOf(">", num);
			if (num2 < 0)
			{
				return null;
			}
			return srcText.Substring(num, num2 - num + 1);
		}

		public static void GetTextSetting(string srcText, ref float linespace, ref TextAlignment align, ref TextAnchor anchor, ref Vector2 offset, ref Vector2 iconOffset, ref float iconSizeOffset, ref float tabSize, ref bool fit, ref bool wrap)
		{
			string richTextContent = AnUtilityText.GetRichTextContent(srcText, AnValue.TextSettingPrefix);
			if (richTextContent == null)
			{
				return;
			}
			string textValue = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingLineSpace);
			string textValue2 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingAnchor);
			string textValue3 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingAlign);
			string textValue4 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingIconOffsetX);
			string textValue5 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingIconOffsetY);
			string textValue6 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingIconOffsetSize);
			string textValue7 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingOffsetX);
			string textValue8 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingOffsetY);
			string textValue9 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingTab);
			string textValue10 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingFit);
			string textValue11 = AnUtilityText.GetTextValue(richTextContent, AnValue.TextSettingWrap);
			if (textValue != "")
			{
				try
				{
					linespace = float.Parse(textValue);
				}
				catch
				{
				}
			}
			if (textValue2 != "")
			{
				anchor = AnUtilityText.GetTextAnchorFromString(textValue2);
			}
			if (textValue3 != "")
			{
				align = AnUtilityText.GetTextAlignmentFromString(textValue3);
			}
			if (textValue9 != "")
			{
				tabSize = float.Parse(textValue9);
			}
			offset = Vector2.zero;
			if (textValue7 != "")
			{
				try
				{
					offset.x = float.Parse(textValue7);
				}
				catch
				{
				}
			}
			if (textValue8 != "")
			{
				try
				{
					offset.y = float.Parse(textValue8);
				}
				catch
				{
				}
			}
			if (textValue6 != "")
			{
				try
				{
					iconSizeOffset = float.Parse(textValue6);
				}
				catch
				{
				}
			}
			iconOffset = Vector2.zero;
			if (textValue4 != "")
			{
				try
				{
					iconOffset.x = float.Parse(textValue4);
				}
				catch
				{
				}
			}
			if (textValue5 != "")
			{
				try
				{
					iconOffset.y = float.Parse(textValue5);
				}
				catch
				{
				}
			}
			if (textValue10 != "")
			{
				if (textValue10 == "1")
				{
					fit = true;
				}
				else if (textValue10 == "0")
				{
					fit = false;
				}
			}
			if (textValue11 != "")
			{
				if (textValue11 == "1")
				{
					wrap = true;
					return;
				}
				if (textValue11 == "0")
				{
					wrap = false;
				}
			}
		}

		public static string GetTextValue(string srcText, string prefixString)
		{
			int num = srcText.IndexOf(prefixString);
			if (num < 0)
			{
				return "";
			}
			int num2 = srcText.IndexOf(" ", num);
			if (num2 < 0)
			{
				num2 = srcText.IndexOf(">", num);
			}
			if (num2 < 0)
			{
				return "";
			}
			return srcText.Substring(num, num2 - num).Replace(prefixString, "");
		}

		public static TextAlignment GetTextAlignmentFromString(string srcString)
		{
			if (srcString == "r")
			{
				return TextAlignment.Right;
			}
			if (srcString == "c")
			{
				return TextAlignment.Center;
			}
			if (srcString == "l")
			{
				return TextAlignment.Left;
			}
			return TextAlignment.Center;
		}

		public static TextAnchor GetTextAnchorFromString(string srcString)
		{
			if (srcString == "ul")
			{
				return TextAnchor.UpperLeft;
			}
			if (srcString == "uc")
			{
				return TextAnchor.UpperCenter;
			}
			if (srcString == "ur")
			{
				return TextAnchor.UpperRight;
			}
			if (srcString == "ml")
			{
				return TextAnchor.MiddleLeft;
			}
			if (srcString == "mc")
			{
				return TextAnchor.MiddleCenter;
			}
			if (srcString == "mr")
			{
				return TextAnchor.MiddleRight;
			}
			if (srcString == "ll")
			{
				return TextAnchor.LowerLeft;
			}
			if (srcString == "lc")
			{
				return TextAnchor.LowerCenter;
			}
			if (srcString == "lr")
			{
				return TextAnchor.LowerRight;
			}
			return TextAnchor.MiddleCenter;
		}
	}
}
