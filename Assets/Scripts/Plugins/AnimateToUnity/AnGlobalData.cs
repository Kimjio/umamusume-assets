using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnGlobalData : ScriptableObject
	{
		public string UnityVersion
		{
			get
			{
				return this._unityVersion;
			}
			set
			{
				this._unityVersion = value;
			}
		}

		public string LocalizeTarget
		{
			get
			{
				return this._localizeTarget;
			}
			set
			{
				this._localizeTarget = value;
			}
		}

		public List<AnFontParameter> FontParameterList
		{
			get
			{
				return this._fontParameterList;
			}
		}

		public List<AnFontIconParameter> FontIconParameterList
		{
			get
			{
				return this._fontIconParameterList;
			}
		}

		public List<AnScreenSizeParameter> ScreenSizeParameterList
		{
			get
			{
				return this._screenSizeParameterList;
			}
		}

		public float BaseScreenWidth
		{
			get
			{
				return this._baseScreenWidth;
			}
		}

		public float ScrollStartPixel
		{
			get
			{
				return this._scrollStartPixel;
			}
		}

		public float ScrollSpeedValue
		{
			get
			{
				return this._scrollSpeedValue;
			}
		}

		public float ScrollAccelValue
		{
			get
			{
				return this._scrollAccelValue;
			}
		}

		public float ScrollIncrementValue
		{
			get
			{
				return this._scrollIncrementValue;
			}
		}

		public float DefaultLongTouchTime
		{
			get
			{
				return this._defaultLongTouchTime;
			}
		}

		public float KeyInputChangeStartDelayTime
		{
			get
			{
				return this._keyInputChangeStartDelayTime;
			}
		}

		public float KeyInputChangeDelayTime
		{
			get
			{
				return this._keyInputChangeDelayTime;
			}
		}

		public float RayInputSubmitDelay
		{
			get
			{
				return this._rayInputSubmitDelay;
			}
		}

		public int TextSortOderRoundValue
		{
			get
			{
				return this._textSortOderRoundValue;
			}
		}

		public int TextOutlineQualityForMinFontSize
		{
			get
			{
				return this._textOutlineQualityForMinFontSize;
			}
		}

		public int TextOutlineQualityMinFontSize
		{
			get
			{
				return this._textOutlineQualityMinFontSize;
			}
		}

		public int TextOutlineQualityForMinOffset
		{
			get
			{
				return this._textOutlineQualityForMinOffset;
			}
		}

		public int TextOutlineQualityMinOffset
		{
			get
			{
				return this._textOutlineQualityMinOffset;
			}
		}

		public int StencilMaskInterval
		{
			get
			{
				return this._stencilMaskInterval;
			}
		}

		public List<AnPlayerSetting> PlayerSettingList
		{
			get
			{
				return this._playerSettingList;
			}
		}

		public void _Initialize()
		{
			for (int i = 0; i < this._fontParameterList.Count; i++)
			{
				this._fontParameterList[i]._Initialize();
			}
			for (int j = 0; j < this._playerSettingList.Count; j++)
			{
				this._playerSettingList[j]._Initialize();
			}
			this._UpdateCharTable();
			this._defaultFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			AnMonoSingleton<AnRootManager>.Instance.SetLocalizeTaget(this._localizeTarget);
		}

		private void _UpdateCharTable()
		{
			if (this._banPrefixCharTable == null)
			{
				this._banPrefixCharTable = new Hashtable();
			}
			if (this._banSuffixCharTable == null)
			{
				this._banSuffixCharTable = new Hashtable();
			}
			if (this._joinCharTable == null)
			{
				this._joinCharTable = new Hashtable();
			}
			this._banPrefixCharTable.Clear();
			this._banSuffixCharTable.Clear();
			this._joinCharTable.Clear();
			for (int i = 0; i < this._banPrefixChar.Length; i++)
			{
				if (!this._banPrefixCharTable.ContainsKey(this._banPrefixChar[i]))
				{
					this._banPrefixCharTable.Add(this._banPrefixChar[i], this._banPrefixChar[i]);
				}
			}
			for (int j = 0; j < this._banSuffixChar.Length; j++)
			{
				if (!this._banSuffixCharTable.ContainsKey(this._banSuffixChar[j]))
				{
					this._banSuffixCharTable.Add(this._banSuffixChar[j], this._banSuffixChar[j]);
				}
			}
			for (int k = 0; k < this._joinChar.Length; k++)
			{
				if (!this._joinCharTable.ContainsKey(this._joinChar[k]))
				{
					this._joinCharTable.Add(this._joinChar[k], this._joinChar[k]);
				}
			}
		}

		public void _UpdateFontTable()
		{
			if (this._fontIconParameterTable == null)
			{
				this._fontIconParameterTable = new Hashtable();
			}
			this._fontIconParameterTable.Clear();
			for (int i = 0; i < this._fontIconParameterList.Count; i++)
			{
				AnFontIconParameter anFontIconParameter = this._fontIconParameterList[i];
				if (!(anFontIconParameter.ColorTexture == null))
				{
					this._fontIconParameterTable.Add(anFontIconParameter.ColorTexture.name, anFontIconParameter);
				}
			}
			for (int j = 0; j < this._fontParameterList.Count; j++)
			{
				this._fontParameterList[j]._Initialize();
			}
			if (this._fontTable == null)
			{
				this._fontTable = new Hashtable();
			}
			if (this._fontLocalizeParamTable == null)
			{
				this._fontLocalizeParamTable = new Hashtable();
			}
			this._fontTable.Clear();
			this._fontLocalizeParamTable.Clear();
			for (int k = 0; k < this._fontParameterList.Count; k++)
			{
				AnFontParameter anFontParameter = this._fontParameterList[k];
				AnFontLocalizeParameter anFontLocalizeParameter = anFontParameter._GetLocalizeParameter(AnMonoSingleton<AnRootManager>.Instance.LocalizeTarget);
				if (anFontLocalizeParameter != null && anFontLocalizeParameter.FontPath != null && !(anFontLocalizeParameter.FontPath == ""))
				{
					string fileName = Path.GetFileName(anFontLocalizeParameter.FontPath);
					Font font = this._GetFontInAddFontTable(fileName, this._fontTable);
					if (font == null)
					{
						font = Resources.Load<Font>(anFontLocalizeParameter.FontPath);
					}
					if (!(font == null))
					{
						this._fontTable.Add(anFontParameter.FontName, font);
						this._fontLocalizeParamTable.Add(anFontParameter.FontName, anFontLocalizeParameter);
					}
				}
			}
			if (this._commonFontTable == null)
			{
				this._commonFontTable = new Hashtable();
			}
			if (this._commonFontLocalizeParamTable == null)
			{
				this._commonFontLocalizeParamTable = new Hashtable();
			}
			this._commonFontTable.Clear();
			this._commonFontLocalizeParamTable.Clear();
			for (int l = 0; l < this._fontParameterList.Count; l++)
			{
				AnFontParameter anFontParameter2 = this._fontParameterList[l];
				for (int m = 0; m < anFontParameter2.LocalizeParameterList.Count; m++)
				{
					AnFontLocalizeParameter anFontLocalizeParameter2 = anFontParameter2.LocalizeParameterList[m];
					if (anFontLocalizeParameter2 != null && anFontLocalizeParameter2.IsCommon && anFontLocalizeParameter2.FontPath != null && !(anFontLocalizeParameter2.FontPath == ""))
					{
						string fileName2 = Path.GetFileName(anFontLocalizeParameter2.FontPath);
						Font font2 = this._GetFontInAddFontTable(fileName2, this._commonFontTable);
						if (font2 == null)
						{
							font2 = Resources.Load<Font>(anFontLocalizeParameter2.FontPath);
						}
						if (!(font2 == null))
						{
							this._commonFontTable.Add(anFontParameter2.FontName, font2);
							this._commonFontLocalizeParamTable.Add(anFontParameter2.FontName, anFontLocalizeParameter2);
						}
					}
				}
			}
		}

		public Font _GetFont(string fontName)
		{
			if (this._fontTable.ContainsKey(fontName))
			{
				return this._fontTable[fontName] as Font;
			}
			if (!this._commonFontTable.ContainsKey(fontName))
			{
				return this._defaultFont;
			}
			return this._commonFontTable[fontName] as Font;
		}

		public Font _GetFontFromCommon(string fontName)
		{
			if (this._commonFontTable.ContainsKey(fontName))
			{
				return this._commonFontTable[fontName] as Font;
			}
			if (!this._fontTable.ContainsKey(fontName))
			{
				return this._defaultFont;
			}
			return this._fontTable[fontName] as Font;
		}

		public void _AddFontToAddFontTable(Font font)
		{
			if (font == null)
			{
				return;
			}
			if (this._addFontTable == null)
			{
				this._addFontTable = new Hashtable();
			}
			string name = font.name;
			if (this._addFontTable.ContainsKey(name))
			{
				this._addFontTable.Remove(name);
			}
			if (this._addFontTable.ContainsKey(name))
			{
				return;
			}
			this._addFontTable.Add(name, font);
			this._UpdateFontTable();
		}

		private Font _GetFontInAddFontTable(string fontName, Hashtable targetTable)
		{
			if (targetTable == null)
			{
				return null;
			}
			if (!targetTable.ContainsKey(fontName))
			{
				return null;
			}
			Font font = targetTable[fontName] as Font;
			if (font == null)
			{
				return null;
			}
			return font;
		}

		public AnFontIconParameter _GetFontIconParameter(string fontIconName)
		{
			if (!this._fontIconParameterTable.ContainsKey(fontIconName))
			{
				return null;
			}
			return this._fontIconParameterTable[fontIconName] as AnFontIconParameter;
		}

		public AnFontLocalizeParameter _GetFontLocalizeParam(string fontName)
		{
			if (this._fontLocalizeParamTable.ContainsKey(fontName))
			{
				return this._fontLocalizeParamTable[fontName] as AnFontLocalizeParameter;
			}
			if (!this._commonFontLocalizeParamTable.ContainsKey(fontName))
			{
				return null;
			}
			return this._commonFontLocalizeParamTable[fontName] as AnFontLocalizeParameter;
		}

		public AnFontLocalizeParameter _GetFontLocalizeParamFromCommon(string fontName)
		{
			if (this._commonFontLocalizeParamTable.ContainsKey(fontName))
			{
				return this._commonFontLocalizeParamTable[fontName] as AnFontLocalizeParameter;
			}
			if (!this._fontLocalizeParamTable.ContainsKey(fontName))
			{
				return null;
			}
			return this._fontLocalizeParamTable[fontName] as AnFontLocalizeParameter;
		}

		public bool _IsBanPrefixChar(char target)
		{
			return this._banPrefixCharTable.ContainsKey(target);
		}

		public bool _IsBanSuffixChar(char target)
		{
			return this._banSuffixCharTable.ContainsKey(target);
		}

		public bool _IsJoinChar(char target)
		{
			return this._joinCharTable.ContainsKey(target);
		}

		public AnPlayerSetting _GetPlayerSetting(int playerIndex)
		{
			if (this._playerSettingList == null)
			{
				return null;
			}
			if (this._playerSettingList.Count == 0)
			{
				return null;
			}
			if (playerIndex >= this._playerSettingList.Count || playerIndex < 0)
			{
				return this._playerSettingList[0];
			}
			return this._playerSettingList[playerIndex];
		}

		public AnScreenSizeParameter _GetScreenSizeParameter(string deviceModel)
		{
			if (this._screenSizeParameterList == null)
			{
				return null;
			}
			if (this._screenSizeParameterList.Count == 0)
			{
				return null;
			}
			this._screenSizeParameterSortedList = new List<AnScreenSizeParameter>();
			for (int i = 0; i < this._screenSizeParameterList.Count; i++)
			{
				this._screenSizeParameterSortedList.Add(this._screenSizeParameterList[i]);
			}
			this._screenSizeParameterSortedList.Sort((AnScreenSizeParameter a, AnScreenSizeParameter b) => b.Priority - a.Priority);
			for (int j = 0; j < this._screenSizeParameterSortedList.Count; j++)
			{
				AnScreenSizeParameter anScreenSizeParameter = this._screenSizeParameterSortedList[j];
				if (anScreenSizeParameter.DeviceModelList != null && anScreenSizeParameter.DeviceModelList.Count != 0)
				{
					for (int k = 0; k < anScreenSizeParameter.DeviceModelList.Count; k++)
					{
						string text = anScreenSizeParameter.DeviceModelList[k];
						if (text != null)
						{
							text = text.ToLower();
							if (Regex.IsMatch(deviceModel, text))
							{
								return anScreenSizeParameter;
							}
						}
					}
				}
			}
			return null;
		}

		[SerializeField]
		private string _unityVersion = "";

		[SerializeField]
		private string _localizeTarget = "";

		[SerializeField]
		private List<AnFontParameter> _fontParameterList;

		[SerializeField]
		private List<AnFontIconParameter> _fontIconParameterList;

		[SerializeField]
		private List<AnScreenSizeParameter> _screenSizeParameterList;

		[NonSerialized]
		private List<AnScreenSizeParameter> _screenSizeParameterSortedList;

		[SerializeField]
		private float _baseScreenWidth = 1920f;

		[SerializeField]
		private int _textSortOderRoundValue = 200;

		[SerializeField]
		private int _textOutlineQualityForMinFontSize = 20;

		[SerializeField]
		private int _textOutlineQualityMinFontSize = 50;

		[SerializeField]
		private int _textOutlineQualityForMinOffset = 16;

		[SerializeField]
		private int _textOutlineQualityMinOffset = 5;

		[SerializeField]
		private int _stencilMaskInterval = 3;

		[SerializeField]
		private float _defaultLongTouchTime = 1f;

		[SerializeField]
		private float _scrollStartPixel = 5f;

		[SerializeField]
		private float _scrollSpeedValue = 0.01f;

		[SerializeField]
		private float _scrollAccelValue = 0.01f;

		[SerializeField]
		private float _scrollIncrementValue = 3f;

		[SerializeField]
		private float _keyInputChangeStartDelayTime = 0.3f;

		[SerializeField]
		private float _keyInputChangeDelayTime = 0.1f;

		[SerializeField]
		private float _rayInputSubmitDelay = 3f;

		[SerializeField]
		private List<AnPlayerSetting> _playerSettingList;

		private string _banPrefixChar = "!%),.:;?]}¢°’”‰′″℃、。々〉》」』】〕ぁぃぅぇぉっゃゅょゎ\u309b\u309cゝゞァィゥェォッャュョヮヵヶ・ーヽヾ！％），．：；？］｝｡｣､･ｧｨｩｪｫｬｭｮｯｰﾞﾟ￠";

		private string _banSuffixChar = "$([\\{£¥‘“〈《「『【〔＄（［｛｢￡￥";

		private string _joinChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

		public Hashtable _fontTable;

		public Hashtable _commonFontTable;

		public Hashtable _fontIconParameterTable;

		private Hashtable _fontLocalizeParamTable;

		private Hashtable _commonFontLocalizeParamTable;

		private Font _defaultFont;

		private Hashtable _addFontTable;

		private Hashtable _banPrefixCharTable;

		private Hashtable _banSuffixCharTable;

		private Hashtable _joinCharTable;
	}
}
