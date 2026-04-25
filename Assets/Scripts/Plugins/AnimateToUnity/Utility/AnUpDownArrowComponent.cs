using System;

namespace AnimateToUnity.Utility
{
	public class AnUpDownArrowComponent : AnComponentBase
	{
		public AnUpDownArrow UpDownArrow
		{
			get
			{
				return this._uiBase as AnUpDownArrow;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("UpButtonMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("DownButtonMotion", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("Value", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("Min", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("Max", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.MotionPrefix + text;
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				text2 = AnValue.MotionPrefix + text2;
			}
			this.UpDownArrow.SetOtherPath(text, text2);
			if (AnUtilityString.IsEmptyString(text4))
			{
				text4 = this.UpDownArrow.MinValue.ToString();
			}
			if (AnUtilityString.IsEmptyString(text5))
			{
				text5 = this.UpDownArrow.MaxValue.ToString();
			}
			this.UpDownArrow.SetRange(int.Parse(text4), int.Parse(text5));
			if (!AnUtilityString.IsEmptyString(text3))
			{
				this.UpDownArrow.SetValue(int.Parse(text3));
			}
		}
	}
}
