using System;

namespace AnimateToUnity.Utility
{
	public class AnScrollBarComponent : AnComponentBase
	{
		public AnScrollBar ScrollBar
		{
			get
			{
				return this._uiBase as AnScrollBar;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("BarMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("MoveMotion", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("ButtonMotion", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("Value", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("Min", 0);
			string text6 = this._objectBase.Parameter.UIParameter._GetParameterValue("Max", 0);
			string text7 = this._objectBase.Parameter.UIParameter._GetParameterValue("Range", 0);
			string text8 = this._objectBase.Parameter.UIParameter._GetParameterValue("Step", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.MotionPrefix + text;
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				text2 = AnValue.MotionPrefix + text2;
			}
			if (!AnUtilityString.IsEmptyString(text3))
			{
				text3 = AnValue.MotionPrefix + text3;
			}
			this.ScrollBar.SetOtherPath(text, text2, text3);
			if (AnUtilityString.IsEmptyString(text7))
			{
				text7 = this.ScrollBar.RangeValue.ToString();
			}
			if (AnUtilityString.IsEmptyString(text5))
			{
				text5 = this.ScrollBar.MinValue.ToString();
			}
			if (AnUtilityString.IsEmptyString(text6))
			{
				text6 = this.ScrollBar.MaxValue.ToString();
			}
			this.ScrollBar.SetRange(float.Parse(text5), float.Parse(text6), float.Parse(text7));
			if (!AnUtilityString.IsEmptyString(text4))
			{
				this.ScrollBar.SetValue(float.Parse(text4));
			}
			if (!AnUtilityString.IsEmptyString(text8))
			{
				this.ScrollBar.SetStepCount(int.Parse(text8));
			}
		}
	}
}
