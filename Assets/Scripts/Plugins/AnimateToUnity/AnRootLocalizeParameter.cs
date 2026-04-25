using System;
using System.Collections.Generic;

namespace AnimateToUnity
{
	[Serializable]
	public class AnRootLocalizeParameter
	{
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

		public List<string> FontNameFromCommonList
		{
			get
			{
				return this._fontNameFromCommonList;
			}
			set
			{
				this._fontNameFromCommonList = value;
			}
		}

		public string _localizeTarget;

		public List<string> _fontNameFromCommonList;
	}
}
