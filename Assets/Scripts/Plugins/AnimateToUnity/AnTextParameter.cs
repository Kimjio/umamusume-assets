using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnTextParameter : AnObjectParameterBase
	{
		public AnTextMeshTypes TextMeshType
		{
			get
			{
				return this._textMeshType;
			}
			set
			{
				this._textMeshType = value;
			}
		}

		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		public string FontName
		{
			get
			{
				return this._fontName;
			}
			set
			{
				this._fontName = value;
			}
		}

		public int FontSize
		{
			get
			{
				return this._fontSize;
			}
			set
			{
				this._fontSize = value;
			}
		}

		public float LineSpace
		{
			get
			{
				return this._lineSpace;
			}
			set
			{
				this._lineSpace = value;
			}
		}

		public TextAnchor Anchor
		{
			get
			{
				return this._anchor;
			}
			set
			{
				this._anchor = value;
			}
		}

		public FontStyle FontStyle
		{
			get
			{
				return this._fontStyle;
			}
			set
			{
				this._fontStyle = value;
			}
		}

		public TextAlignment Alignment
		{
			get
			{
				return this._alignment;
			}
			set
			{
				this._alignment = value;
			}
		}

		public bool UseWrap
		{
			get
			{
				return this._useWrap;
			}
			set
			{
				this._useWrap = value;
			}
		}

		public bool UseFit
		{
			get
			{
				return this._useFit;
			}
			set
			{
				this._useFit = value;
			}
		}

		public Color TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				this._textColor = value;
			}
		}

		public Color ShadowColor
		{
			get
			{
				return this._shadowColor;
			}
			set
			{
				this._shadowColor = value;
			}
		}

		public float ShadowOffset
		{
			get
			{
				return this._shadowOffset;
			}
			set
			{
				this._shadowOffset = value;
			}
		}

		public float ShadowAngle
		{
			get
			{
				return this._shadowAngle;
			}
			set
			{
				this._shadowAngle = value;
			}
		}

		public Color OutlineColor
		{
			get
			{
				return this._outlineColor;
			}
			set
			{
				this._outlineColor = value;
			}
		}

		public int OutlineQuality
		{
			get
			{
				return this._outlineQuality;
			}
			set
			{
				this._outlineQuality = value;
			}
		}

		public int FixOutlineQuality
		{
			get
			{
				return this._fixOutlineQuality;
			}
			set
			{
				this._fixOutlineQuality = value;
			}
		}

		public float OutlineOffset
		{
			get
			{
				return this._outlineOffset;
			}
			set
			{
				this._outlineOffset = value;
			}
		}

		public int FontSizeOffset
		{
			get
			{
				return this._fontSizeOffset;
			}
			set
			{
				this._fontSizeOffset = value;
			}
		}

		public float FontUpperAnchorOffset
		{
			get
			{
				return this._fontUpperAnchorOffset;
			}
			set
			{
				this._fontUpperAnchorOffset = value;
			}
		}

		public float FontMiddleAnchorOffset
		{
			get
			{
				return this._fontMiddleAnchorOffset;
			}
			set
			{
				this._fontMiddleAnchorOffset = value;
			}
		}

		public float FontLowerAnchorOffset
		{
			get
			{
				return this._fontLowerAnchorOffset;
			}
			set
			{
				this._fontLowerAnchorOffset = value;
			}
		}

		public float _FontLeftAlignOffset
		{
			get
			{
				return this._fontLeftAlignOffset;
			}
		}

		public float FontCenterAlignOffset
		{
			get
			{
				return this._fontCenterAlignOffset;
			}
		}

		public float FontRightAlignOffset
		{
			get
			{
				return this._fontRightAlignOffset;
			}
		}

		public Vector2 FontIconOffset
		{
			get
			{
				return this._fontIconOffset;
			}
			set
			{
				this._fontIconOffset = value;
			}
		}

		public float FontIconSizeOffset
		{
			get
			{
				return this._fontIconSizeOffset;
			}
			set
			{
				this._fontIconSizeOffset = value;
			}
		}

		public float FontLinespaceOffset
		{
			get
			{
				return this._fontLinespaceOffset;
			}
			set
			{
				this._fontLinespaceOffset = value;
			}
		}

		public bool UseCommonFont
		{
			get
			{
				return this._useCommonFont;
			}
		}

		public string GradationStartObjectName
		{
			get
			{
				return this._gradationStartObjectName;
			}
			set
			{
				this._gradationStartObjectName = value;
			}
		}

		public string GradationEndObjectName
		{
			get
			{
				return this._gradationEndObjectName;
			}
			set
			{
				this._gradationEndObjectName = value;
			}
		}

		public override void _Initialize()
		{
			base._Initialize();
			this._gameObjectName = AnValue.TextPrefix + this._objectName;
		}

		public override void _CreateHierarchy(AnRoot root, GameObject parentObject)
		{
			base._CreateHierarchy(root, parentObject);
			if (this._targetGameObject == null)
			{
				return;
			}
			this._targetGameObject.name = AnValue.TextPrefix + base.ObjectName;
			if (!Application.isPlaying)
			{
				return;
			}
			if (!AnMonoSingleton<AnRootManager>.Instance.ExistGlobalData)
			{
				return;
			}
			this._useCommonFont = root.Parameter._UseCommonFont(this._fontName);
			AnFontLocalizeParameter anFontLocalizeParameter = AnMonoSingleton<AnRootManager>.Instance._GetFontLocalizeParam(this._fontName, this._useCommonFont);
			int num = AnMonoSingleton<AnRootManager>.Instance._GetTextOutlineQualityMinFontSize(anFontLocalizeParameter);
			int num2 = AnMonoSingleton<AnRootManager>.Instance._GetTextOutlineQualityForMinFontSize(anFontLocalizeParameter);
			float num3 = AnMonoSingleton<AnRootManager>.Instance._GetTextOutlineQualityMinOffset(anFontLocalizeParameter);
			int num4 = AnMonoSingleton<AnRootManager>.Instance._GetTextOutlineQualityForMinOffset(anFontLocalizeParameter);
			this._fixOutlineQuality = this._outlineQuality;
			if (this._outlineQuality >= 10)
			{
				this._fixOutlineQuality = 12;
			}
			else if (this._outlineQuality >= 5)
			{
				this._fixOutlineQuality = 8;
			}
			else
			{
				this._fixOutlineQuality = 6;
			}
			if (this._fontSize >= num)
			{
				this._fixOutlineQuality = num2;
			}
			else if (this._outlineOffset >= num3)
			{
				this._fixOutlineQuality = num4;
			}
			TextMesh textMesh = new GameObject
			{
				name = AnValue.TextMainName,
				transform = 
				{
					parent = this._attachGameObject.transform,
					localPosition = Vector3.zero,
					localRotation = Quaternion.identity,
					localScale = Vector3.one
				}
			}.AddComponent<TextMesh>();
			if (this.ShadowOffset != 0f)
			{
				new GameObject
				{
					name = AnValue.TextShadowName,
					transform = 
					{
						parent = textMesh.gameObject.transform,
						localPosition = Vector3.zero,
						localRotation = Quaternion.identity,
						localScale = Vector3.one
					}
				}.AddComponent<TextMesh>();
			}
			if (this._outlineOffset > 0f && this._fixOutlineQuality > 2)
			{
				for (int i = 0; i < this._fixOutlineQuality; i++)
				{
					new GameObject
					{
						name = AnValue.TextOutlineName + i.ToString(),
						transform = 
						{
							parent = textMesh.gameObject.transform,
							localPosition = Vector3.zero,
							localRotation = Quaternion.identity,
							localScale = Vector3.one
						}
					}.AddComponent<TextMesh>();
				}
			}
		}

		public override void _ApplyData(AnMotion parentMotion)
		{
			base._ApplyData(parentMotion);
			if (this._targetGameObject == null)
			{
				return;
			}
			if (!AnMonoSingleton<AnRootManager>.Instance.ExistGlobalData)
			{
				return;
			}
			AnFontLocalizeParameter anFontLocalizeParameter = AnMonoSingleton<AnRootManager>.Instance._GetFontLocalizeParam(this._fontName, this._useCommonFont);
			if (anFontLocalizeParameter != null)
			{
				AnFontSizeParameter anFontSizeParameter = anFontLocalizeParameter._GetFontSizeParameter(this._fontSize);
				if (anFontSizeParameter != null)
				{
					this._fontSizeOffset = anFontSizeParameter.SizeOffset;
					this._fontUpperAnchorOffset = anFontSizeParameter.UpperAnchorOffset;
					this._fontMiddleAnchorOffset = anFontSizeParameter.MiddleAnchorOffset;
					this._fontLowerAnchorOffset = anFontSizeParameter.LowerAnchorOffset;
					this._fontLeftAlignOffset = anFontSizeParameter.LeftAlignOffset;
					this._fontCenterAlignOffset = anFontSizeParameter.CenterAlignOffset;
					this._fontRightAlignOffset = anFontSizeParameter.RightAlignOffset;
					this._fontLinespaceOffset = anFontSizeParameter.LineSpaceOffset;
					this._fontIconOffset = anFontSizeParameter.IconOffset;
					this._fontIconSizeOffset = anFontSizeParameter.IconSizeOffset;
				}
			}
			AnText anText = new AnText(this._targetGameObject);
			anText._ApplyData(this, parentMotion);
			parentMotion.Root.ObjectList.Add(anText);
			parentMotion.Root.DataTable.Add(this._targetGameObject, anText);
			parentMotion.Root.DataList.Add(anText);
		}

		public AnTextMeshTypes _textMeshType;

		public string _text;

		public string _fontName;

		public int _fontSize;

		public float _lineSpace;

		public TextAnchor _anchor;

		public FontStyle _fontStyle;

		public TextAlignment _alignment;

		public bool _useWrap;

		public bool _useFit;

		public Color32 _textColor = Color.white;

		public Color32 _shadowColor = Color.black;

		public float _shadowOffset;

		public float _shadowAngle;

		public Color32 _outlineColor = Color.gray;

		public int _outlineQuality;

		public float _outlineOffset;

		public string _gradationStartObjectName;

		public string _gradationEndObjectName;

		private int _fixOutlineQuality;

		private float _fontUpperAnchorOffset;

		private float _fontMiddleAnchorOffset;

		private float _fontLowerAnchorOffset;

		private float _fontLeftAlignOffset;

		private float _fontCenterAlignOffset;

		private float _fontRightAlignOffset;

		private float _fontLinespaceOffset;

		private int _fontSizeOffset;

		private Vector2 _fontIconOffset = Vector2.zero;

		private float _fontIconSizeOffset;

		private bool _useCommonFont;
	}
}
