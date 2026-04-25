using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnText : AnObjectBase
	{
		public AnTextParameter TextParameter
		{
			get
			{
				return this._textParam;
			}
		}

		public TextMesh MainTextMesh
		{
			get
			{
				return this._mainTextMesh;
			}
		}

		public MeshRenderer MainTextMeshRenderer
		{
			get
			{
				return this._mainTextMeshRenderer;
			}
		}

		public TextMesh ShadowTextMesh
		{
			get
			{
				return this._shadowTextMesh;
			}
		}

		public List<TextMesh> OutlineTextMeshList
		{
			get
			{
				return this._outlineTextMeshList;
			}
		}

		public string Text
		{
			get
			{
				return this._text;
			}
		}

		public string FixText
		{
			get
			{
				return this._fixText;
			}
		}

		public string FixTextWithoutRichText
		{
			get
			{
				return this._fixTextWithoutRichText;
			}
		}

		public bool UseWrap
		{
			get
			{
				return this._useWrap;
			}
		}

		public bool UseFit
		{
			get
			{
				return this._useFit;
			}
		}

		public Vector2 TextRange
		{
			get
			{
				return this._textParam.Size;
			}
		}

		public Vector2 CurrentTextRange
		{
			get
			{
				return this._currentTextRange;
			}
		}

		public bool IsOverRange
		{
			get
			{
				return this._isOverRange;
			}
		}

		public int TextFontSize
		{
			get
			{
				return this._fontSize;
			}
		}

		public float TextLineSpace
		{
			get
			{
				return this._lineSpace;
			}
		}

		public TextAnchor TextAnchor
		{
			get
			{
				return this._textAnchor;
			}
		}

		public TextAlignment TextAlignment
		{
			get
			{
				return this._textAlignment;
			}
		}

		public FontStyle TextFontStyle
		{
			get
			{
				return this._fontStyle;
			}
		}

		public Vector2 TextOffset
		{
			get
			{
				return this._textOffset;
			}
		}

		public Vector2 TextIconOffset
		{
			get
			{
				return this._textIconOffset;
			}
		}

		public float TextIconSizeOffset
		{
			get
			{
				return this._textIconSizeOffset;
			}
		}

		public Color TextColor
		{
			get
			{
				return this._textColor;
			}
		}

		public Color TextShadowColor
		{
			get
			{
				return this._shadowColor;
			}
		}

		public float TextShadowOffset
		{
			get
			{
				return this._shadowOffset;
			}
		}

		public float TextShadowAngle
		{
			get
			{
				return this._shadowAngle;
			}
		}

		public Color TextOutlineColor
		{
			get
			{
				return this._outlineColor;
			}
		}

		public float TextOutlineOffset
		{
			get
			{
				return this._outlineOffset;
			}
		}

		public AnText(GameObject gameObject)
			: base(gameObject)
		{
		}

		public override void _ApplyData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			base._ApplyData(parameter, parentMotion);
			this._textParam = parameter as AnTextParameter;
		}

		public override void _CreateData()
		{
			base._CreateData();
			this._textMeshFlags = 0;
			if (this._textParam.TextMeshType == AnTextMeshTypes.Normal)
			{
				this._textMeshFlags |= 1;
			}
			else if (this._textParam.TextMeshType == AnTextMeshTypes.Shadow)
			{
				this._textMeshFlags |= 1;
				this._textMeshFlags |= 2;
			}
			else if (this._textParam.TextMeshType == AnTextMeshTypes.Outline)
			{
				this._textMeshFlags |= 1;
				this._textMeshFlags |= 4;
			}
			else if (this._textParam.TextMeshType == AnTextMeshTypes.ShadowAndOutline)
			{
				this._textMeshFlags |= 1;
				this._textMeshFlags |= 2;
				this._textMeshFlags |= 4;
			}
			this._text = (string)this._textParam.Text.Clone();
			this._fontSize = this._textParam.FontSize;
			this._lineSpace = this._textParam.LineSpace;
			this._currentLinespace = this._lineSpace;
			this._textAnchor = this._textParam.Anchor;
			this._currentTextAnchor = this._textAnchor;
			this._textAlignment = this._textParam.Alignment;
			this._currentTextAlignment = this._textAlignment;
			this._tabSize = AnValue.DefaultTabSize;
			this._currentTabSize = this._tabSize;
			this._textOffset = Vector2.zero;
			this._currentTextOffset = this._textOffset;
			this._textIconOffset = Vector2.zero;
			this._currentTextIconOffset = this._textIconOffset;
			this._textIconSizeOffset = 0f;
			this._currentTextIconSizeOffset = this._textIconSizeOffset;
			this._fontStyle = this._textParam.FontStyle;
			this._useFit = this._textParam.UseFit;
			this._currentUseFit = this._useFit;
			this._useWrap = this._textParam.UseWrap;
			this._currentUseFit = this._useWrap;
			this._textColor = this._textParam.TextColor;
			this._textColor.a = 1f;
			this._shadowColor = this._textParam.ShadowColor;
			this._shadowColor.a = 1f;
			this._shadowAngle = this._textParam.ShadowAngle;
			this._shadowOffset = this._textParam.ShadowOffset;
			this._outlineColor = this._textParam.OutlineColor;
			this._outlineColor.a = 1f;
			this._outlineOffset = this._textParam.OutlineOffset;
			this._outlineQuality = this._textParam.FixOutlineQuality;
			this._textIconOffset = Vector2.zero;
			this._textOffset = Vector2.zero;
			this._isColorOffsetChangeFlag = false;
			this._prevFixTextColor = Color.magenta;
			this._prevFixShadowColor = Color.magenta;
			this._prevFixOutlineColor = Color.magenta;
			this._root.SortOrderCount += this._root.SortOrderInterval;
			this._sortOrderIndex = this._root.SortOrderCount;
			this._sortOrderIndexForDrawTextLater = 0;
			this._fontMaterialTable = new Hashtable();
			this._fontIconMaterialTable = new Hashtable();
			this._CreateTextMesh();
		}

		private void _CreateTextMesh()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			GameObject childObject = AnUtilityObject.GetChildObject(this._offsetObject, 0);
			if (childObject == null)
			{
				this._textMeshFlags = 0;
				return;
			}
			Font font = AnMonoSingleton<AnRootManager>.Instance._GetFont(this._textParam.FontName, this._textParam.UseCommonFont);
			if (font == null)
			{
				this._textMeshFlags = 0;
				return;
			}
			this._mainTextMesh = this._root.TextMeshTable[childObject] as TextMesh;
			this._mainTextMeshRenderer = this._root.MeshRendererTable[childObject] as MeshRenderer;
			AnUtilityObject.SetMeshRendererDefaultValue(this._mainTextMeshRenderer);
			this._mainTextMesh.gameObject.layer = this._offsetObject.layer;
			this._mainTextMesh.font = font;
			if ((this._textMeshFlags & 2) != 0)
			{
				GameObject childObject2 = AnUtilityObject.GetChildObject(childObject, AnValue.TextShadowName);
				if (childObject2 == null)
				{
					this._textMeshFlags &= -3;
				}
				else
				{
					this._shadowTextMesh = this._parentMotion.Root.TextMeshTable[childObject2] as TextMesh;
					this._shadowTextMeshRenderer = this._root.MeshRendererTable[childObject2] as MeshRenderer;
					this._shadowTextMesh.font = this._mainTextMesh.font;
				}
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				this._outlineTextMeshList = new List<TextMesh>();
				this._outlineTextMeshRendererList = new List<MeshRenderer>();
				for (int i = 0; i < this._outlineQuality; i++)
				{
					GameObject childObject3 = AnUtilityObject.GetChildObject(childObject, AnValue.TextOutlineName + i.ToString());
					if (childObject3 == null)
					{
						this._textMeshFlags &= -5;
						return;
					}
					TextMesh textMesh = this._parentMotion.Root.TextMeshTable[childObject3] as TextMesh;
					MeshRenderer meshRenderer = this._root.MeshRendererTable[childObject3] as MeshRenderer;
					textMesh.font = this._mainTextMesh.font;
					this._outlineTextMeshList.Add(textMesh);
					this._outlineTextMeshRendererList.Add(meshRenderer);
				}
			}
		}

		public override void _FixData()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			base._FixData();
			this._UpdateSortLayer();
			this._UpdateSortOrder();
			this._CheckGradation();
		}

		public override void _UpdateFirst()
		{
			base._UpdateFirst();
			if (this._root._initializeFlag)
			{
				this._UpdateText();
			}
			if (!this._visibleInHierarchy || !this._visibleByAlpha || this._root._initializeFlag)
			{
				this._UpdateEnableRenderer(false);
				this._textVisibleFlag = false;
				return;
			}
			this._UpdateEnableRenderer(true);
			if (!this._textVisibleFlag)
			{
				this._colorChanged = true;
				this._colorOffsetChanged = true;
				this._textVisibleFlag = true;
			}
			this._UpdateTextColor();
			this._UpdateTextColorOffset();
			this._UpdateGradation();
			this._UpdateGradationLater();
		}

		protected override void _UpdateColor()
		{
			this._currentColor = this._baseColor;
			this._currentColorOffset = this._baseColorOffset;
			base._UpdateColor();
		}

		protected override void _UpdateTransform(bool forceUpdate)
		{
			base._UpdateTransform(forceUpdate);
			if (this._scaleChanged)
			{
				this._transform.localScale = this._currentScale;
			}
		}

		private void _UpdateText()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (AnMonoSingleton<AnRootManager>.Instance == null)
			{
				return;
			}
			this._currentLinespace = this._lineSpace;
			this._currentTextAnchor = this._textAnchor;
			this._currentTextAlignment = this._textAlignment;
			this._currentTabSize = this._tabSize;
			this._currentUseFit = this._useFit;
			this._currentUseWrap = this._useWrap;
			this._currentTextOffset = this._textOffset;
			this._currentTextIconOffset = this._textIconOffset;
			this._currentTextIconSizeOffset = this._textIconSizeOffset;
			AnUtilityText.GetTextSetting(this._text, ref this._currentLinespace, ref this._currentTextAlignment, ref this._currentTextAnchor, ref this._currentTextOffset, ref this._currentTextIconOffset, ref this._currentTextIconSizeOffset, ref this._currentTabSize, ref this._currentUseFit, ref this._currentUseWrap);
			this._UpdateIconSizeText();
			this._fixText = AnUtilityText.RemoveStringFromText(this._text, AnValue.TextSettingPrefix);
			this._UpdateTextMesh();
			this._UpdatePosition();
			this._CheckText();
			this._UpdateMaterial();
			this._ApplyTextColorAndTextColorOffset();
		}

		private void _UpdateIconSizeText()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._currentTextIconSizeOffset + this._textParam.FontIconSizeOffset == 0f)
			{
				return;
			}
			this._tempString00 = AnUtilityText.GetRichTextContent(this._text, AnValue.TextIconPrefix);
			if (this._tempString00 == null)
			{
				return;
			}
			this._tempString01 = AnUtilityText.GetTextValue(this._tempString00, AnValue.TextIconSize);
			float num = this._currentTextIconSizeOffset + this._textParam.FontIconSizeOffset;
			if (this._tempString01 != "")
			{
				try
				{
					num += float.Parse(this._tempString01);
				}
				catch
				{
				}
				this._tempString02 = this._tempString00.Replace(AnValue.TextIconSize + this._tempString01, AnValue.TextIconSize + num.ToString());
			}
			else
			{
				num += (float)this._fontSize;
				this._tempString02 = this._tempString00.Replace("/>", AnValue.TextIconSize + num.ToString() + " />");
			}
			this._text = this._text.Replace(this._tempString00, this._tempString02);
			this._tempString00 = null;
			this._tempString01 = null;
			this._tempString02 = null;
		}

		private void _UpdateTextMesh()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			this._mainTextMesh.font = AnMonoSingleton<AnRootManager>.Instance._GetFont(this._textParam.FontName, this._textParam.UseCommonFont);
			this._mainTextMesh.text = this._fixText;
			this._mainTextMesh.characterSize = AnValue.DefaultCharacterSize;
			this._mainTextMesh.lineSpacing = this._currentLinespace + this._textParam.FontLinespaceOffset;
			this._mainTextMesh.anchor = this._currentTextAnchor;
			this._mainTextMesh.alignment = this._currentTextAlignment;
			this._mainTextMesh.richText = true;
			this._mainTextMesh.fontStyle = this._fontStyle;
			this._mainTextMesh.fontSize = this._fontSize + this._textParam.FontSizeOffset;
			this._mainTextMesh.tabSize = this._currentTabSize;
			if (Application.unityVersion.IndexOf("4.6") != 0 && Application.unityVersion.IndexOf("5.") != 0)
			{
				this._mainTextMesh.tabSize = this._mainTextMesh.tabSize * 10f;
			}
			this._UpdateTextStyle();
			this._fixTextWithoutRichText = AnUtilityText.ConvertRichTextToNormal(this._fixText);
			if ((this._textMeshFlags & 2) != 0)
			{
				AnUtilityText.CopyTextMeshValue(this._mainTextMesh, this._shadowTextMesh);
				AnUtilityObject.CopyMeshRendererValue(this._mainTextMeshRenderer, this._shadowTextMeshRenderer);
				this._shadowTextMesh.text = this._fixTextWithoutRichText;
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				for (int i = 0; i < this._outlineTextMeshList.Count; i++)
				{
					AnUtilityText.CopyTextMeshValue(this._mainTextMesh, this._outlineTextMeshList[i]);
					AnUtilityObject.CopyMeshRendererValue(this._mainTextMeshRenderer, this._outlineTextMeshRendererList[i]);
					this._outlineTextMeshList[i].text = this._fixTextWithoutRichText;
				}
			}
		}

		private void _UpdateTextStyle()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			Vector3 localPosition = this._mainTextMesh.gameObject.transform.localPosition;
			Transform parent = this._mainTextMesh.gameObject.transform.parent;
			this._mainTextMesh.gameObject.transform.parent = null;
			this._mainTextMesh.gameObject.transform.localPosition = Vector3.zero;
			this._mainTextMesh.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this._mainTextMesh.gameObject.transform.localScale = Vector3.one;
			this._currentTextRange = this._mainTextMeshRenderer.bounds.size;
			this._CheckTextRange();
			this._CheckAutoReturn();
			this._currentTextRange = this._mainTextMeshRenderer.bounds.size;
			this._mainTextMesh.gameObject.transform.parent = parent;
			this._mainTextMesh.gameObject.transform.localPosition = localPosition;
			this._mainTextMesh.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this._mainTextMesh.gameObject.transform.localScale = Vector3.one;
			this._CheckTextFit();
		}

		private void _CheckAutoReturn()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (!this._currentUseWrap)
			{
				return;
			}
			if (this._fixText.Length < 2)
			{
				return;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			bool flag = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			this._mainTextMesh.text = AnValue.TextEmpty;
			int i = 0;
			while (i < this._fixText.Length)
			{
				char c = this._fixText[i];
				char c2;
				if (i == 0)
				{
					c2 = this._fixText[i];
				}
				else
				{
					c2 = this._fixText[i - 1];
				}
				if (!flag)
				{
					if (!this._GetRichBracketIndex(ref this._fixText, ref i, ref num, ref num2, ref num3))
					{
						goto IL_0119;
					}
					text4 = this._fixText.Substring(i, num - i + 1);
					text5 = this._fixText.Substring(num2, num3 - num2 + 1);
					flag = true;
					text3 = text;
					text6 = text4;
					text7 = text3 + text4;
					i = num;
				}
				else
				{
					if (i < num2)
					{
						goto IL_0119;
					}
					text = text3 + text6 + text5;
					text2 = text;
					text3 = null;
					text7 = null;
					flag = false;
					i = num3;
				}
				IL_0443:
				i++;
				continue;
				IL_0119:
				if (flag)
				{
					text6 += c.ToString();
					text7 += c.ToString();
					string text8 = text7 + text5;
					this._mainTextMesh.text = text8;
				}
				else
				{
					text += c.ToString();
					text2 += c.ToString();
					this._mainTextMesh.text = text2;
				}
				if (this._mainTextMeshRenderer.bounds.size.x >= this._textParam.Size.x)
				{
					bool flag2 = false;
					if (AnMonoSingleton<AnRootManager>.Instance.GlobalData._IsJoinChar(c2) && AnMonoSingleton<AnRootManager>.Instance.GlobalData._IsJoinChar(c))
					{
						int num4 = this._fixText.LastIndexOf(AnValue.TextHalfSpaceChar, i);
						if (num4 < 0)
						{
							num4 = this._fixText.LastIndexOf(AnValue.TextCommaChar, i);
							if (num4 < 0)
							{
								num4 = this._fixText.LastIndexOf(AnValue.TextPeriodChar, i);
							}
						}
						int num5 = this._fixText.IndexOf(AnValue.TextHalfSpaceChar, i);
						if (num5 < 0)
						{
							num5 = this._fixText.IndexOf(AnValue.TextCommaChar, i);
							if (num5 < 0)
							{
								num5 = this._fixText.IndexOf(AnValue.TextPeriodChar, i);
							}
						}
						if (num4 >= 0 && num5 >= 0)
						{
							num4++;
							num5--;
							int num6 = num5 - num4 + 1;
							int num7 = i - 1 - num4 + 1;
							if (num6 >= AnValue.JoinWordMinNum && num6 <= AnValue.JoinWordMaxNum)
							{
								flag2 = true;
								if (flag)
								{
									text6 = text6.Substring(0, text6.Length - num7 - 1) + AnValue.TextReturn;
									text7 = text4;
								}
								else
								{
									text = text.Substring(0, text.Length - num7 - 1) + AnValue.TextReturn;
									text2 = AnValue.TextEmpty;
								}
								i = i - num7 - 1;
							}
						}
					}
					if (!flag2)
					{
						if (AnMonoSingleton<AnRootManager>.Instance.GlobalData._IsBanPrefixChar(c))
						{
							if (flag)
							{
								text6 = text6.Substring(0, text6.Length - 2) + AnValue.TextReturn;
								text7 = text4;
							}
							else
							{
								text = text.Substring(0, text.Length - 2) + AnValue.TextReturn;
								text2 = AnValue.TextEmpty;
							}
							i -= 2;
						}
						else if (AnMonoSingleton<AnRootManager>.Instance.GlobalData._IsBanSuffixChar(c2) && i > 0)
						{
							if (flag)
							{
								text6 = text6.Substring(0, text6.Length - 2) + AnValue.TextReturn;
								text7 = text4;
							}
							else
							{
								text = text.Substring(0, text.Length - 2) + AnValue.TextReturn;
								text2 = AnValue.TextEmpty;
							}
							i -= 2;
						}
						else
						{
							if (flag)
							{
								text6 = text6.Substring(0, text6.Length - 1) + AnValue.TextReturn;
								text7 = text4;
							}
							else
							{
								text = text.Substring(0, text.Length - 1) + AnValue.TextReturn;
								text2 = AnValue.TextEmpty;
							}
							i--;
						}
					}
					this._mainTextMesh.text = AnValue.TextEmpty;
					goto IL_0443;
				}
				goto IL_0443;
			}
			if (text == null)
			{
				text = AnValue.TextEmpty;
			}
			this._mainTextMesh.text = text;
			this._fixText = text;
		}

		private bool _GetRichBracketIndex(ref string target, ref int startBracketStartIndex, ref int startBracketEndIndex, ref int endBracketStartIndex, ref int endBracketEndIndex)
		{
			startBracketEndIndex = -1;
			endBracketStartIndex = -1;
			endBracketEndIndex = -1;
			if (startBracketStartIndex < 0 || startBracketStartIndex >= target.Length)
			{
				return false;
			}
			if (target[startBracketStartIndex] != AnValue.TextRichBracketStartChar)
			{
				return false;
			}
			this._SearchRichBracketEndIndex(ref target, ref startBracketStartIndex, ref startBracketEndIndex);
			if (startBracketEndIndex < 0)
			{
				return false;
			}
			endBracketStartIndex = target.IndexOf(AnValue.TextRichEndBracketStart, startBracketEndIndex);
			if (endBracketStartIndex < 0)
			{
				return false;
			}
			string text = target.Substring(startBracketStartIndex, 2);
			string text2 = AnValue.TextRichEndBracketStart + target.Substring(startBracketStartIndex + 1, 1);
			this._GetRichTextEndIndex(ref target, ref startBracketStartIndex, ref endBracketEndIndex, text, text2);
			return endBracketEndIndex >= 0 && (startBracketStartIndex < startBracketEndIndex && startBracketEndIndex < endBracketStartIndex && endBracketStartIndex < endBracketEndIndex);
		}

		private void _SearchRichBracketEndIndex(ref string target, ref int startIndex, ref int endIndex)
		{
			int num = target.IndexOf(AnValue.TextRichEndBracketStart, startIndex);
			endIndex = -1;
			for (int i = startIndex; i < target.Length; i++)
			{
				int num2 = target.IndexOf(AnValue.TextRichBracketEnd, i);
				if (num2 < 0 || num2 >= num)
				{
					break;
				}
				i = num2;
				endIndex = num2;
			}
		}

		private void _GetRichTextEndIndex(ref string target, ref int startIndex, ref int endIndex, string startFlag, string endFlag)
		{
			if (target.IndexOf(startFlag, startIndex) < 0)
			{
				return;
			}
			if (target.IndexOf(endFlag, startIndex) < 0)
			{
				return;
			}
			endIndex = target.IndexOf(endFlag, startIndex);
			if (endIndex < 0)
			{
				return;
			}
			endIndex = target.IndexOf(AnValue.TextRichBracketEndChar, endIndex);
			int num = endIndex;
		}

		private void _CheckTextRange()
		{
			this._isOverRange = false;
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._currentTextRange.x <= 0f)
			{
				return;
			}
			if (this._textParam.Size.x / this._currentTextRange.x >= 1f)
			{
				return;
			}
			this._isOverRange = true;
		}

		private void _CheckTextFit()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (!this._currentUseFit)
			{
				return;
			}
			if (this._currentTextRange.x <= 0f)
			{
				return;
			}
			float num = this._textParam.Size.x / this._currentTextRange.x;
			if (num < 1f)
			{
				this._mainTextMesh.transform.localScale = new Vector3(num, num, 1f);
				return;
			}
			this._mainTextMesh.transform.localScale = Vector3.one;
		}

		private void _UpdatePosition()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			this._mainTextMesh.transform.localPosition = AnUtilityText.CalculateTextOffset(this._mainTextMesh, this._textParam._FontLeftAlignOffset, this._textParam.FontCenterAlignOffset, this._textParam.FontRightAlignOffset, this._textParam.FontUpperAnchorOffset, this._textParam.FontMiddleAnchorOffset, this._textParam.FontLowerAnchorOffset, this._currentTextAnchor, this._parameter.Size);
			this._mainTextMesh.transform.localPosition += new Vector3(this._currentTextOffset.x, this._currentTextOffset.y, 0f);
			if ((this._textMeshFlags & 2) != 0)
			{
				this._shadowTextMesh.transform.localPosition = new Vector3(Mathf.Cos(-this._shadowAngle * 0.017453292f) * this._shadowOffset, Mathf.Sin(-this._shadowAngle * 0.017453292f) * this._shadowOffset, 0f);
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				for (int i = 0; i < this._outlineTextMeshList.Count; i++)
				{
					Component component = this._outlineTextMeshList[i];
					Vector2 zero = Vector2.zero;
					float num = 360f / (float)this._outlineQuality * (float)i + 360f / (float)this._outlineQuality * 0.5f;
					zero.x = this._outlineOffset * Mathf.Cos(num * 0.017453292f);
					zero.y = this._outlineOffset * Mathf.Sin(num * 0.017453292f);
					component.transform.localPosition = new Vector3(zero.x, zero.y, 0f);
				}
			}
		}

		private void _CheckText()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			this._isNormalText = true;
			this._isTextWithIcons = false;
			this._isTextWithRichColor = false;
			if (this._text.Contains(AnValue.TextIconPrefix) && AnMonoSingleton<AnRootManager>.Instance.GlobalData != null && AnMonoSingleton<AnRootManager>.Instance.GlobalData.FontIconParameterList.Count > 0)
			{
				this._isTextWithIcons = true;
				this._isNormalText = false;
			}
			if (this._text.Contains(AnValue.TextColorPrefix))
			{
				this._isTextWithRichColor = true;
				this._isNormalText = false;
			}
		}

		private void _UpdateTextColor()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (!this._colorChanged)
			{
				return;
			}
			this._currentFixTextColor = this._currentColor * this._textColor;
			if (!AnUtilityColor.IsSameColor(this._currentFixTextColor, this._prevFixTextColor))
			{
				this._mainTextMesh.color = this._currentFixTextColor;
				if (this._isTextWithIcons)
				{
					for (int i = 1; i < this._notSharedMaterialList.Length; i++)
					{
						this._notSharedMaterialList[i].SetColor(AnValue.ShaderParamMultiplyColor, this._currentColor);
					}
				}
				if (this._isTextWithRichColor)
				{
					this._mainTextMesh.color = this._textColor;
					this._notSharedMaterial.SetColor(AnValue.ShaderParamMultiplyColor, this._currentColor);
				}
			}
			this._prevFixTextColor = this._currentFixTextColor;
			if ((this._textMeshFlags & 2) != 0)
			{
				this._currentFixShadowColor = this._currentColor * this._shadowColor;
				if (!AnUtilityColor.IsSameColor(this._currentFixShadowColor, this._prevFixShadowColor))
				{
					this._shadowTextMesh.color = this._currentFixShadowColor;
				}
				this._prevFixShadowColor = this._currentFixShadowColor;
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				this._currentFixOutlineColor = this._currentColor * this._outlineColor;
				if (!AnUtilityColor.IsSameColor(this._currentFixOutlineColor, this._prevFixOutlineColor))
				{
					bool flag = this._currentColor.a < 1f;
					bool flag2 = this._currentFixOutlineColor.a < this._prevFixOutlineColor.a;
					Color currentFixOutlineColor = this._currentFixOutlineColor;
					currentFixOutlineColor.a /= (float)this._outlineTextMeshList.Count;
					for (int j = 0; j < this._outlineTextMeshList.Count; j++)
					{
						if (flag && flag2)
						{
							this._outlineTextMeshList[j].color = currentFixOutlineColor;
						}
						else
						{
							this._outlineTextMeshList[j].color = this._currentFixOutlineColor;
						}
					}
				}
				this._prevFixOutlineColor = this._currentFixOutlineColor;
			}
		}

		private void _UpdateTextColorOffset()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (!this._colorOffsetChanged)
			{
				return;
			}
			if (AnUtilityColor.IsSameColor(this._currentColorOffset, AnValue.ColorZero))
			{
				if (this._isColorOffsetChangeFlag)
				{
					this._isColorOffsetChangeFlag = false;
					this._isMaterialForceUpdate = true;
					this._UpdateMaterial();
					this._ApplyTextColorAndTextColorOffset();
					this._isMaterialForceUpdate = false;
				}
			}
			else if (!this._isColorOffsetChangeFlag)
			{
				this._isColorOffsetChangeFlag = true;
				this._isMaterialForceUpdate = true;
				this._UpdateMaterial();
				this._ApplyTextColorAndTextColorOffset();
				this._isMaterialForceUpdate = false;
			}
			if (this._notSharedMaterial != null)
			{
				this._notSharedMaterial.SetColor(AnValue.ShaderParamColorOffset, this._currentColorOffset);
			}
			if (this._isTextWithIcons && this._notSharedMaterialList != null && this._notSharedMaterialList.Length > 1)
			{
				for (int i = 1; i < this._notSharedMaterialList.Length; i++)
				{
					this._notSharedMaterialList[i].SetColor(AnValue.ShaderParamColorOffset, this._currentColorOffset);
				}
			}
			if (((this._textMeshFlags & 2) != 0 || (this._textMeshFlags & 4) != 0) && this._notSharedSubMaterial != null)
			{
				this._notSharedSubMaterial.SetColor(AnValue.ShaderParamColorOffset, this._currentColorOffset);
			}
		}

		private void _ApplyTextColorAndTextColorOffset()
		{
			this._prevFixTextColor = Color.magenta;
			this._prevFixShadowColor = Color.magenta;
			this._prevFixOutlineColor = Color.magenta;
			this._colorChanged = true;
			this._UpdateTextColor();
			this._colorOffsetChanged = true;
			this._UpdateTextColorOffset();
		}

		private void _UpdateMaterial()
		{
			if (this._IsUpdateMaterial())
			{
				this._CreateMaterial();
			}
			this._SetShaderParameter();
			this._SetMaterialToTextMesh();
		}

		private bool _IsUpdateMaterial()
		{
			if (this._isMaterialForceUpdate)
			{
				return true;
			}
			if (this._sharedMaterial == null)
			{
				return true;
			}
			if (this._sharedSubMaterial == null)
			{
				return true;
			}
			if (this._stencilRef != this._prevStencilRef)
			{
				return true;
			}
			if (this._isGrayscale != this._prevIsGrayscale)
			{
				return true;
			}
			if (!this._isNormalText)
			{
				if (this._notSharedMaterial == null)
				{
					return true;
				}
				if (this._notSharedMaterialList == null)
				{
					return true;
				}
			}
			return false;
		}

		private void _CreateMaterial()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._objectType == AnObjectTypes.StencilAlphaMask || this._objectType == AnObjectTypes.StencilMask)
			{
				AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnShaderTypes.StencilAlphaMask, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedMaterial);
			}
			else if (this._isGrayscale)
			{
				if (this._existGradation)
				{
					AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnShaderTypes.GrayscaleGradation, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedMaterial);
				}
				else
				{
					AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnShaderTypes.Grayscale, this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedMaterial);
				}
			}
			else if (this._existGradation)
			{
				AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType, false, true), this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedMaterial);
			}
			else
			{
				AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType), this._stencilRef, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedMaterial);
			}
			int num = this._stencilRef;
			if (this._objectType == AnObjectTypes.StencilAlphaMask || this._objectType == AnObjectTypes.StencilMask)
			{
				num = this._root.DefaultStencilRefOffset;
			}
			if (this._isGrayscale)
			{
				AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnShaderTypes.Grayscale, num, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedSubMaterial);
			}
			else
			{
				AnMonoSingleton<AnRootManager>.Instance._GetFontMaterial(this._fontMaterialTable, this._textParam.FontName, AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType), num, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, this._textParam.UseCommonFont, ref this._sharedSubMaterial);
			}
			if (this._singleSharedMaterialList == null)
			{
				this._singleSharedMaterialList = new Material[1];
			}
			this._singleSharedMaterialList[0] = this._sharedMaterial;
			this._isMaterialShared = true;
			this._isSubMaterialShared = true;
			if (this._existGradation)
			{
				this._isMaterialShared = false;
			}
			if (!this._isNormalText)
			{
				this._isMaterialShared = false;
			}
			if (!AnUtilityColor.IsSameColor(this._currentColorOffset, AnValue.ColorZero))
			{
				this._isMaterialShared = false;
				this._isSubMaterialShared = false;
			}
			if (!this._isMaterialShared)
			{
				AnMonoSingleton<AnRootManager>.Instance._CloneFontMaterial(this._fontMaterialTable, this._sharedMaterial, this._id, ref this._notSharedMaterial);
				if (this._singleNotSharedMaterialList == null)
				{
					this._singleNotSharedMaterialList = new Material[1];
				}
				this._singleNotSharedMaterialList[0] = this._notSharedMaterial;
				if (this._isTextWithIcons)
				{
					if (this._isGrayscale)
					{
						AnMonoSingleton<AnRootManager>.Instance._CloneTextIconMaterialList(this._fontIconMaterialTable, this._id, AnShaderTypes.Grayscale, this._root.DefaultStencilRefOffset, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, ref this._notSharedMaterialList);
					}
					else
					{
						AnMonoSingleton<AnRootManager>.Instance._CloneTextIconMaterialList(this._fontIconMaterialTable, this._id, AnUtilityMaterial.GetShaderTypeFromBlend(this._blendModeType), this._root.DefaultStencilRefOffset, this._root.DefaultStencilRefOffset, this._stencilCompareFunc, ref this._notSharedMaterialList);
					}
					this._notSharedMaterialList[0] = this._notSharedMaterial;
				}
			}
			if (!this._isSubMaterialShared)
			{
				if (this._sharedMaterial != this._sharedSubMaterial)
				{
					AnMonoSingleton<AnRootManager>.Instance._CloneFontMaterial(this._fontMaterialTable, this._sharedSubMaterial, this._id, ref this._notSharedSubMaterial);
					return;
				}
				this._notSharedSubMaterial = this._notSharedMaterial;
			}
		}

		private void _SetShaderParameter()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._isMaterialShared)
			{
				this._SetShaderValue(this._sharedMaterial, AnTextTargetTypes.NormalText, true);
			}
			else if (this._isTextWithRichColor)
			{
				this._SetShaderValue(this._notSharedMaterial, AnTextTargetTypes.RichText, true);
			}
			else
			{
				this._SetShaderValue(this._notSharedMaterial, AnTextTargetTypes.NormalText, true);
			}
			if (this._isSubMaterialShared)
			{
				if (this._sharedMaterial != this._sharedSubMaterial)
				{
					this._SetShaderValue(this._sharedSubMaterial, AnTextTargetTypes.NormalText, false);
				}
			}
			else
			{
				this._SetShaderValue(this._notSharedSubMaterial, AnTextTargetTypes.NormalText, false);
			}
			if (this._isTextWithIcons && this._notSharedMaterialList.Length > 1)
			{
				for (int i = 1; i < this._notSharedMaterialList.Length; i++)
				{
					this._SetShaderValue(this._notSharedMaterialList[i], AnTextTargetTypes.IconText, false);
				}
			}
		}

		private void _SetShaderValue(Material mat, AnTextTargetTypes target, bool applyGradation)
		{
			mat.SetColor(AnValue.ShaderParamMultiplyColor, Color.white);
			if (applyGradation && this._existGradation)
			{
				Color currentColor = this._gradationInfo._gradationStart._currentColor;
				Color currentColor2 = this._gradationInfo._gradationEnd._currentColor;
				if (this._gradationInfo._enableOverrideGradationAlpha)
				{
					currentColor.a = this._gradationInfo._overrideGradationStartAlpha;
					currentColor2.a = this._gradationInfo._overrideGradationEndAlpha;
				}
				mat.SetColor(AnValue.ShaderParamGradationStartColor, currentColor);
				mat.SetColor(AnValue.ShaderParamGradationEndColor, currentColor2);
				mat.SetVector(AnValue.ShaderParamGradationStartPosition, this._gradationInfo._gradationStart._transform.position);
				mat.SetVector(AnValue.ShaderParamGradationEndPosition, this._gradationInfo._gradationEnd._transform.position);
			}
			if (target == AnTextTargetTypes.RichText)
			{
				mat.SetColor(AnValue.ShaderParamMultiplyColor, this._currentColor);
				return;
			}
			if (target == AnTextTargetTypes.IconText)
			{
				mat.SetColor(AnValue.ShaderParamMultiplyColor, this._currentColor);
				mat.SetVector(AnValue.ShaderParamOffset, this._currentTextIconOffset + this._textParam.FontIconOffset);
			}
		}

		private void _SetMaterialToTextMesh()
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._isMaterialShared)
			{
				this._mainTextMeshRenderer.material = this._sharedMaterial;
				this._mainTextMeshRenderer.materials = this._singleSharedMaterialList;
			}
			else if (this._isTextWithIcons)
			{
				this._mainTextMeshRenderer.material = this._notSharedMaterial;
				this._mainTextMeshRenderer.materials = this._notSharedMaterialList;
			}
			else
			{
				this._mainTextMeshRenderer.material = this._notSharedMaterial;
				this._mainTextMeshRenderer.materials = this._singleNotSharedMaterialList;
			}
			if (this._isSubMaterialShared)
			{
				if ((this._textMeshFlags & 2) != 0)
				{
					this._shadowTextMeshRenderer.material = this._sharedSubMaterial;
				}
				if ((this._textMeshFlags & 4) != 0)
				{
					for (int i = 0; i < this._outlineTextMeshRendererList.Count; i++)
					{
						this._outlineTextMeshRendererList[i].material = this._sharedSubMaterial;
					}
					return;
				}
			}
			else
			{
				if ((this._textMeshFlags & 2) != 0)
				{
					this._shadowTextMeshRenderer.material = this._notSharedSubMaterial;
				}
				if ((this._textMeshFlags & 4) != 0)
				{
					for (int j = 0; j < this._outlineTextMeshRendererList.Count; j++)
					{
						this._outlineTextMeshRendererList[j].material = this._notSharedSubMaterial;
					}
				}
			}
		}

		private void _UpdateEnableRenderer(bool enable)
		{
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._parentMotion._root._initializeFlag)
			{
				enable = false;
			}
			if (enable)
			{
				if (!this._mainTextMeshRenderer.enabled)
				{
					this._mainTextMeshRenderer.enabled = true;
				}
				if ((this._textMeshFlags & 2) != 0 && !this._shadowTextMeshRenderer.enabled)
				{
					this._shadowTextMeshRenderer.enabled = true;
				}
				if ((this._textMeshFlags & 4) != 0)
				{
					for (int i = 0; i < this._outlineTextMeshRendererList.Count; i++)
					{
						if (!this._outlineTextMeshRendererList[i].enabled)
						{
							this._outlineTextMeshRendererList[i].enabled = true;
						}
					}
				}
				return;
			}
			if (this._mainTextMeshRenderer.enabled)
			{
				this._mainTextMeshRenderer.enabled = false;
			}
			if ((this._textMeshFlags & 2) != 0 && this._shadowTextMeshRenderer.enabled)
			{
				this._shadowTextMeshRenderer.enabled = false;
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				for (int j = 0; j < this._outlineTextMeshRendererList.Count; j++)
				{
					if (this._outlineTextMeshRendererList[j].enabled)
					{
						this._outlineTextMeshRendererList[j].enabled = false;
					}
				}
			}
		}

		protected override void _UpdateSortLayer()
		{
			base._UpdateSortLayer();
			if (this._textMeshFlags == 0)
			{
				return;
			}
			this._UpdateSortLayerBase(this._mainTextMeshRenderer);
			if ((this._textMeshFlags & 2) != 0)
			{
				this._UpdateSortLayerBase(this._shadowTextMeshRenderer);
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				for (int i = 0; i < this._outlineTextMeshRendererList.Count; i++)
				{
					this._UpdateSortLayerBase(this._outlineTextMeshRendererList[i]);
				}
			}
		}

		private void _UpdateSortLayerBase(MeshRenderer renderer)
		{
			if (renderer.sortingLayerName != this._sortLayerName)
			{
				renderer.sortingLayerName = this._sortLayerName;
			}
		}

		protected override void _UpdateSortOrder()
		{
			base._UpdateSortOrder();
			if (this._textMeshFlags == 0)
			{
				return;
			}
			if (this._parentMotion.Root.DrawTextLater)
			{
				this._sortOrder += AnMonoSingleton<AnRootManager>.Instance._GetTextSortOderRoundValue() - this._sortOrder % AnMonoSingleton<AnRootManager>.Instance._GetTextSortOderRoundValue();
			}
			this._UpdateSortOrderBase(this._mainTextMeshRenderer, 0);
			if ((this._textMeshFlags & 2) != 0)
			{
				this._UpdateSortOrderBase(this._shadowTextMeshRenderer, 2);
			}
			if ((this._textMeshFlags & 4) != 0)
			{
				for (int i = 0; i < this._outlineTextMeshRendererList.Count; i++)
				{
					this._UpdateSortOrderBase(this._outlineTextMeshRendererList[i], 1);
				}
			}
		}

		private void _UpdateSortOrderBase(Renderer renderer, int offset)
		{
			if (renderer.sortingOrder != this._sortOrder - offset)
			{
				renderer.sortingOrder = this._sortOrder - offset;
			}
		}

		protected override void _UpdateStencilRefBase()
		{
			base._UpdateStencilRefBase();
			this._UpdateText();
		}

		protected override void _SetGrayscaleBase(bool enable)
		{
			base._SetGrayscaleBase(enable);
			this._UpdateText();
		}

		protected virtual void _CheckGradation()
		{
			this._existGradation = false;
			this._gradationInfo = null;
			if (string.IsNullOrEmpty(this._textParam._gradationStartObjectName))
			{
				return;
			}
			if (string.IsNullOrEmpty(this._textParam._gradationEndObjectName))
			{
				return;
			}
			AnObjectBase anObjectBase = this._root.Find<AnObjectBase>(this._parentMotion.GameObject, this._textParam._gradationStartObjectName, false);
			if (anObjectBase == null)
			{
				return;
			}
			AnObjectBase anObjectBase2 = this._root.Find<AnObjectBase>(this._parentMotion.GameObject, this._textParam._gradationEndObjectName, false);
			if (anObjectBase2 == null)
			{
				return;
			}
			this._gradationInfo = new AnText.GradationInfo(this);
			this._gradationInfo._gradationStart = anObjectBase;
			this._gradationInfo._gradationEnd = anObjectBase2;
			this._existGradation = true;
			this._UpdateGradation();
			this._CreateMaterial();
			this._SetShaderParameter();
			this._SetMaterialToTextMesh();
		}

		protected virtual void _UpdateGradation()
		{
			if (!this._existGradation)
			{
				return;
			}
			this._gradationInfo._Update();
			if (!this._gradationChanged)
			{
				return;
			}
			this._SetShaderParameter();
		}

		protected virtual void _UpdateGradationLater()
		{
			if (!this._existGradation)
			{
				return;
			}
			this._gradationInfo._UpdateLater();
		}

		public void SetText(string text)
		{
			if (text == null)
			{
				text = "";
			}
			this._text = text;
			this._UpdateText();
		}

		public void SetTextFontSize(int fontSize)
		{
			this._fontSize = fontSize;
			this._UpdateText();
		}

		public void SetTextLinespace(float lineSpace)
		{
			this._lineSpace = lineSpace;
			this._UpdateText();
		}

		public void SetTextAnchor(TextAnchor anchor)
		{
			this._textAnchor = anchor;
			this._UpdateText();
		}

		public void SetTextAlignment(TextAlignment align)
		{
			this._textAlignment = align;
			this._UpdateText();
		}

		public void SetTextTabSize(float tabSize)
		{
			this._tabSize = tabSize;
			this._UpdateText();
		}

		public void SetTextFontStyle(FontStyle fontStyle)
		{
			this._fontStyle = fontStyle;
			this._UpdateText();
		}

		public void SetTextColor(Color color)
		{
			this._textColor = color;
			this._UpdateText();
		}

		public void SetTextShadow(Color color, float offset, float angle)
		{
			this._shadowColor = color;
			this._shadowOffset = offset;
			this._shadowAngle = angle;
			this._UpdateText();
		}

		public void SetTextOutline(Color color, float offset)
		{
			this._outlineColor = color;
			this._outlineOffset = offset;
			this._UpdateText();
		}

		public void SetTextWrap(bool enable)
		{
			this._useWrap = enable;
			this._UpdateText();
		}

		public void SetTextFit(bool enable)
		{
			this._useFit = enable;
			this._UpdateText();
		}

		public void SetTextOffset(Vector2 offset)
		{
			this._textOffset = offset;
			this._UpdateText();
		}

		public void SetTextIconOffset(Vector2 offset)
		{
			this._textIconOffset = offset;
			this._UpdateText();
		}

		public void SetTextIconSizeOffset(float offset)
		{
			this._textIconSizeOffset = offset;
			this._UpdateText();
		}

		public void SetOverrideGradationAlpha(bool enable, float overrideAlpha)
		{
			this._gradationInfo._enableOverrideGradationAlpha = enable;
			this._gradationInfo._overrideGradationStartAlpha = overrideAlpha;
			this._gradationInfo._overrideGradationEndAlpha = overrideAlpha;
		}

		public void _Destroy()
		{
			if (this._fontMaterialTable != null)
			{
				foreach (object obj in this._fontMaterialTable.Keys)
				{
					string text = (string)obj;
					AnMonoSingleton<AnRootManager>.Instance.RemoveFontMaterial(text);
				}
				this._fontMaterialTable.Clear();
			}
			if (this._fontIconMaterialTable != null)
			{
				foreach (object obj2 in this._fontIconMaterialTable.Keys)
				{
					string text2 = (string)obj2;
					AnMonoSingleton<AnRootManager>.Instance.RemoveFontIconMaterial(text2);
				}
				this._fontIconMaterialTable.Clear();
			}
		}

		private AnTextParameter _textParam;

		private TextMesh _mainTextMesh;

		private TextMesh _shadowTextMesh;

		private List<TextMesh> _outlineTextMeshList;

		private MeshRenderer _mainTextMeshRenderer;

		private MeshRenderer _shadowTextMeshRenderer;

		private List<MeshRenderer> _outlineTextMeshRendererList;

		private int _textMeshFlags;

		private string _text;

		private string _fixText;

		private string _fixTextWithoutRichText;

		private int _fontSize;

		private float _lineSpace;

		private float _currentLinespace;

		private TextAnchor _textAnchor;

		private TextAnchor _currentTextAnchor;

		private TextAlignment _textAlignment;

		private TextAlignment _currentTextAlignment;

		private FontStyle _fontStyle;

		private float _tabSize;

		private float _currentTabSize;

		private Vector2 _textOffset = Vector2.zero;

		private Vector2 _currentTextOffset = Vector2.zero;

		private Vector2 _textIconOffset = Vector2.zero;

		private Vector2 _currentTextIconOffset = Vector2.zero;

		private float _textIconSizeOffset;

		private float _currentTextIconSizeOffset;

		private Vector2 _currentTextRange = Vector2.zero;

		private bool _useFit;

		private bool _currentUseFit;

		private bool _useWrap;

		private bool _currentUseWrap;

		private bool _isOverRange;

		private Color _textColor = Color.white;

		private Color _currentFixTextColor = Color.gray;

		private Color _prevFixTextColor = Color.black;

		private bool _isColorOffsetChangeFlag;

		private Color _shadowColor = Color.white;

		private Color _currentFixShadowColor = Color.gray;

		private Color _prevFixShadowColor = Color.black;

		private float _shadowOffset;

		private float _shadowAngle;

		private Color _outlineColor = Color.white;

		private Color _currentFixOutlineColor = Color.gray;

		private Color _prevFixOutlineColor = Color.black;

		private int _outlineQuality = 1;

		private float _outlineOffset;

		private bool _isMaterialShared;

		private bool _isSubMaterialShared;

		private Material[] _singleSharedMaterialList;

		private Material[] _singleNotSharedMaterialList;

		private Material[] _notSharedMaterialList;

		private Material _sharedMaterial;

		private Material _sharedSubMaterial;

		private Material _notSharedMaterial;

		private Material _notSharedSubMaterial;

		private bool _isNormalText;

		private bool _isTextWithIcons;

		private bool _isTextWithRichColor;

		private bool _isMaterialForceUpdate;

		private Hashtable _fontMaterialTable;

		private Hashtable _fontIconMaterialTable;

		protected AnText.GradationInfo _gradationInfo;

		protected bool _existGradation;

		protected bool _gradationChanged;

		private string _tempString00;

		private string _tempString01;

		private string _tempString02;

		private bool _textVisibleFlag;

		protected class GradationInfo
		{
			public GradationInfo(AnText parent)
			{
				this._parent = parent;
				this._ResetPrevValue();
			}

			public void _ResetPrevValue()
			{
				this._prevGradStartColor = Color.magenta;
				this._prevGradEndColor = Color.magenta;
				this._prevGradStartPosition = Vector3.one * float.MaxValue;
				this._prevGradEndPosition = Vector3.one * float.MaxValue;
			}

			public void _Update()
			{
				this._parent._gradationChanged = false;
				if (this._parent._root._initializeFlag)
				{
					this._parent._gradationChanged = true;
					return;
				}
				if (this._gradationStart._currentColor != this._prevGradStartColor)
				{
					this._parent._gradationChanged = true;
					return;
				}
				if (this._gradationEnd._currentColor != this._prevGradEndColor)
				{
					this._parent._gradationChanged = true;
					return;
				}
				if (this._gradationStart._transform.position != this._prevGradStartPosition)
				{
					this._parent._gradationChanged = true;
					return;
				}
				if (this._gradationEnd._transform.position != this._prevGradEndPosition)
				{
					this._parent._gradationChanged = true;
					return;
				}
			}

			public void _UpdateLater()
			{
				this._prevGradStartColor = this._gradationStart._currentColor;
				this._prevGradEndColor = this._gradationEnd._currentColor;
				this._prevGradStartPosition = this._gradationStart._transform.position;
				this._prevGradEndPosition = this._gradationEnd._transform.position;
			}

			public AnText _parent;

			public AnObjectBase _gradationStart;

			public AnObjectBase _gradationEnd;

			public Vector3 _prevGradStartPosition = Vector3.zero;

			public Vector3 _prevGradEndPosition = Vector3.zero;

			public Color _prevGradStartColor = Color.white;

			public Color _prevGradEndColor = Color.white;

			public bool _enableOverrideGradationAlpha;

			public float _overrideGradationStartAlpha;

			public float _overrideGradationEndAlpha;
		}
	}
}
