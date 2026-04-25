using System;

namespace AnimateToUnity.Utility
{
	public class AnProgressBarComponent : AnComponentBase
	{
		public AnProgressBar ProgressBar
		{
			get
			{
				return this._uiBase as AnProgressBar;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("BarMotion", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("Value", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("Min", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("Max", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("Time", 1);
			if (!AnUtilityString.IsEmptyString(text))
			{
				this.ProgressBar.SetOtherPath(AnValue.MotionPrefix + text);
			}
			if (AnUtilityString.IsEmptyString(text3))
			{
				text3 = this.ProgressBar.MinValue.ToString();
			}
			if (AnUtilityString.IsEmptyString(text4))
			{
				text4 = this.ProgressBar.MaxValue.ToString();
			}
			this.ProgressBar.SetRange(float.Parse(text3), float.Parse(text4));
			if (!AnUtilityString.IsEmptyString(text2))
			{
				this.ProgressBar.SetValue(float.Parse(text2), false);
			}
			if (!AnUtilityString.IsEmptyString(text5))
			{
				this.ProgressBar.SetBlendTime(float.Parse(text5));
			}
		}
	}
}
