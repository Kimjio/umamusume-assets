using System;

namespace AnimateToUnity.Utility
{
	public class AnCheckButtonListComponent : AnComponentBase
	{
		public AnCheckButtonList CheckButtonList
		{
			get
			{
				return this._uiBase as AnCheckButtonList;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("ObjectPrefix", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("Count", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("Index", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				this.CheckButtonList.SetOtherPath(AnValue.ObjectPrefix + text);
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				this.CheckButtonList.SetCount(int.Parse(text2));
			}
			if (!AnUtilityString.IsEmptyString(text3))
			{
				this.CheckButtonList.SetIndex(int.Parse(text3));
			}
		}
	}
}
