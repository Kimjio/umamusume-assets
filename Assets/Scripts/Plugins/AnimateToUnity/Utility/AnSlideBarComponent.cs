using System;

namespace AnimateToUnity.Utility
{
	public class AnSlideBarComponent : AnComponentBase
	{
		public AnSlideBar SlideBar
		{
			get
			{
				return this._uiBase as AnSlideBar;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("BarMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("ButtonMotion", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("Value", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("Min", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("Max", 0);
			string text6 = this._objectBase.Parameter.UIParameter._GetParameterValue("Step", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.MotionPrefix + text;
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				text2 = AnValue.MotionPrefix + text2;
			}
			this.SlideBar.SetOtherPath(text, text2);
			if (AnUtilityString.IsEmptyString(text4))
			{
				text4 = this.SlideBar.MinValue.ToString();
			}
			if (AnUtilityString.IsEmptyString(text5))
			{
				text5 = this.SlideBar.MaxValue.ToString();
			}
			this.SlideBar.SetRange(float.Parse(text4), float.Parse(text5));
			if (!AnUtilityString.IsEmptyString(text3))
			{
				this.SlideBar.SetValue(float.Parse(text3));
			}
			if (!AnUtilityString.IsEmptyString(text6))
			{
				this.SlideBar.SetStepCount(int.Parse(text6));
			}
		}
	}
}
