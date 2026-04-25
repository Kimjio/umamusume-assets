using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnFontLocalizeParameter
	{
		public string LocalizeTarget
		{
			get
			{
				return this._localizeTarget;
			}
		}

		public string FontPath
		{
			get
			{
				return this._fontPath;
			}
		}

		public List<AnFontSizeParameter> FontSizeParameterList
		{
			get
			{
				return this._fontSizeParameterList;
			}
		}

		public bool IsCommon
		{
			get
			{
				return this._isCommon;
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

		public float TextOutlineQualityMinOffset
		{
			get
			{
				return this._textOutlineQualityMinOffset;
			}
		}

		public void _Initialize()
		{
			if (this._fontSizeParameterList == null)
			{
				this._fontSizeParameterList = new List<AnFontSizeParameter>();
			}
			if (this._sortedFontSizeParameterList == null)
			{
				this._sortedFontSizeParameterList = new List<AnFontSizeParameter>();
			}
			this._sortedFontSizeParameterList.Clear();
			for (int i = 0; i < this._fontSizeParameterList.Count; i++)
			{
				this._sortedFontSizeParameterList.Add(this._fontSizeParameterList[i]);
			}
			this._sortedFontSizeParameterList.Sort(new Comparison<AnFontSizeParameter>(this._CompareFuncForFontSizeParameter));
		}

		public AnFontSizeParameter _GetFontSizeParameter(int fontSize)
		{
			if (this._sortedFontSizeParameterList == null)
			{
				return null;
			}
			if (this._sortedFontSizeParameterList.Count == 0)
			{
				return null;
			}
			if (this._sortedFontSizeParameterList.Count == 1)
			{
				return this._sortedFontSizeParameterList[0];
			}
			for (int i = 0; i < this._sortedFontSizeParameterList.Count - 1; i++)
			{
				AnFontSizeParameter anFontSizeParameter = this._sortedFontSizeParameterList[i];
				AnFontSizeParameter anFontSizeParameter2 = this._sortedFontSizeParameterList[i + 1];
				if (fontSize >= anFontSizeParameter.FontSize && fontSize < anFontSizeParameter2.FontSize)
				{
					return anFontSizeParameter;
				}
			}
			return this._sortedFontSizeParameterList[this._sortedFontSizeParameterList.Count - 1];
		}

		private int _CompareFuncForFontSizeParameter(AnFontSizeParameter first, AnFontSizeParameter second)
		{
			if (first == null)
			{
				if (second == null)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (second == null)
				{
					return -1;
				}
				if (first.FontSize < second.FontSize)
				{
					return -1;
				}
				if (first.FontSize > second.FontSize)
				{
					return 1;
				}
				return 0;
			}
		}

		[SerializeField]
		private string _localizeTarget;

		[SerializeField]
		private string _fontPath;

		[SerializeField]
		private bool _isCommon;

		[SerializeField]
		private int _textOutlineQualityForMinFontSize;

		[SerializeField]
		private int _textOutlineQualityMinFontSize;

		[SerializeField]
		private int _textOutlineQualityForMinOffset;

		[SerializeField]
		private float _textOutlineQualityMinOffset;

		[SerializeField]
		private List<AnFontSizeParameter> _fontSizeParameterList;

		private List<AnFontSizeParameter> _sortedFontSizeParameterList;
	}
}
