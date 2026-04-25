using System;

namespace AnimateToUnity.Utility
{
	public class AnObjectScrollComponent : AnComponentBase
	{
		public AnObjectScroll ObjectScroll
		{
			get
			{
				return this._uiBase as AnObjectScroll;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("ItemRootMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("StartObject", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("EndObject", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("ItemStartObjectPrefix", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("ScrollBarMotion", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.MotionPrefix + text;
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
				text4 = AnValue.ObjectPrefix + text4;
			}
			this.ObjectScroll.SetOtherPath(text, text2, text3, text4);
			if (!AnUtilityString.IsEmptyString(text5))
			{
				text5 = AnValue.MotionPrefix + text5;
			}
			this.ObjectScroll.SetScrollBarPath(text5);
		}
	}
}
