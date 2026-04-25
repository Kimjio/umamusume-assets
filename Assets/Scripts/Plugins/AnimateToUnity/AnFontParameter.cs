using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnFontParameter
	{
		public string FontName
		{
			get
			{
				return this._fontName;
			}
		}

		public List<AnFontLocalizeParameter> LocalizeParameterList
		{
			get
			{
				return this._localizeParameterList;
			}
		}

		public void _Initialize()
		{
			if (this._localizeParameterList == null)
			{
				this._localizeParameterList = new List<AnFontLocalizeParameter>();
			}
			for (int i = 0; i < this._localizeParameterList.Count; i++)
			{
				this._localizeParameterList[i]._Initialize();
			}
		}

		public AnFontLocalizeParameter _GetLocalizeParameter(string localizeTarget)
		{
			if (this._localizeParameterList == null)
			{
				return null;
			}
			if (this._localizeParameterList.Count == 0)
			{
				return null;
			}
			if (localizeTarget == null)
			{
				return this._localizeParameterList[0];
			}
			if (localizeTarget == "")
			{
				return this._localizeParameterList[0];
			}
			for (int i = 0; i < this._localizeParameterList.Count; i++)
			{
				AnFontLocalizeParameter anFontLocalizeParameter = this._localizeParameterList[i];
				string[] array = anFontLocalizeParameter.LocalizeTarget.Split(new char[] { ',' });
				if (array.Length != 0)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (localizeTarget.ToLower() == array[j].ToLower())
						{
							return anFontLocalizeParameter;
						}
					}
				}
			}
			return this._localizeParameterList[0];
		}

		[SerializeField]
		private string _fontName;

		[SerializeField]
		private List<AnFontLocalizeParameter> _localizeParameterList;
	}
}
