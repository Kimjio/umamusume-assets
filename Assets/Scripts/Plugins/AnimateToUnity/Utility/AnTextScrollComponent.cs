using System;

namespace AnimateToUnity.Utility
{
	public class AnTextScrollComponent : AnComponentBase
	{
		public AnTextScroll TextScroll
		{
			get
			{
				return this._uiBase as AnTextScroll;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("TextObject", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("StartObject", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("EndObject", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("ScrollBarMotion", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.TextPrefix + text;
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				text2 = AnValue.ObjectPrefix + text2;
			}
			if (!AnUtilityString.IsEmptyString(text3))
			{
				text3 = AnValue.ObjectPrefix + text3;
			}
			if (!AnUtilityString.IsEmptyString(text4))
			{
				text4 = AnValue.MotionPrefix + text4;
			}
			this.TextScroll.SetOtherPath(text, text2, text3);
			this.TextScroll.SetScrollBarPath(text4);
		}
	}
}
