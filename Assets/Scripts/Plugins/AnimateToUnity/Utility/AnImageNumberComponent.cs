using System;

namespace AnimateToUnity.Utility
{
	public class AnImageNumberComponent : AnComponentBase
	{
		public AnImageNumber ImageNumber
		{
			get
			{
				return this._uiBase as AnImageNumber;
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("ObjectPrefix", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("Value", 1);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("Align", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("Time", 1);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("Digit", 1);
			string text6 = this._objectBase.Parameter.UIParameter._GetParameterValue("InDelayTime", 1);
			string text7 = this._objectBase.Parameter.UIParameter._GetParameterValue("OutStartTime", 1);
			string text8 = this._objectBase.Parameter.UIParameter._GetParameterValue("OutDelayTime", 1);
			string text9 = this._objectBase.Parameter.UIParameter._GetParameterValue("FromLower", 2);
			string text10 = this._objectBase.Parameter.UIParameter._GetParameterValue("StartLabel", 0);
			string text11 = this._objectBase.Parameter.UIParameter._GetParameterValue("PlusMinus", 2);
			if (!AnUtilityString.IsEmptyString(text))
			{
				this.ImageNumber.SetOtherPath(AnValue.ObjectPrefix + text);
			}
			if (!AnUtilityString.IsEmptyString(text3))
			{
				if (text3 == "Left")
				{
					this.ImageNumber.SetAlignType(AnUIAlignTypes.Left);
				}
				else if (text3 == "Center")
				{
					this.ImageNumber.SetAlignType(AnUIAlignTypes.Center);
				}
				else if (text3 == "Right")
				{
					this.ImageNumber.SetAlignType(AnUIAlignTypes.Right);
				}
				else
				{
					this.ImageNumber.SetAlignType(AnUIAlignTypes.Center);
				}
			}
			if (!AnUtilityString.IsEmptyString(text4))
			{
				this.ImageNumber.SetBlendTime(float.Parse(text4));
			}
			if (!AnUtilityString.IsEmptyString(text5))
			{
				this.ImageNumber.SetDigit(int.Parse(text5));
			}
			if (!AnUtilityString.IsEmptyString(text6))
			{
				this.ImageNumber.SetInDelayTime(float.Parse(text6));
			}
			if (!AnUtilityString.IsEmptyString(text7))
			{
				this.ImageNumber.SetOutStartTime(float.Parse(text7));
			}
			if (!AnUtilityString.IsEmptyString(text8))
			{
				this.ImageNumber.SetOutDelayTime(float.Parse(text8));
			}
			if (!AnUtilityString.IsEmptyString(text10))
			{
				if (text10 == "In")
				{
					this.ImageNumber.SetStartLabelType(AnImageNumber.StartLabelTypes.In);
				}
				else if (text10 == "Loop")
				{
					this.ImageNumber.SetStartLabelType(AnImageNumber.StartLabelTypes.Loop);
				}
				else if (text10 == "Out")
				{
					this.ImageNumber.SetStartLabelType(AnImageNumber.StartLabelTypes.Out);
				}
			}
			if (!AnUtilityString.IsEmptyString(text9))
			{
				this.ImageNumber.SetAnimationFromLower(bool.Parse(text9));
			}
			if (!AnUtilityString.IsEmptyString(text11))
			{
				this.ImageNumber.SetPlusMinusVisibility(bool.Parse(text11));
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				this.ImageNumber.SetValue(float.Parse(text2), false);
				return;
			}
			this.ImageNumber.SetValue(0, false);
		}
	}
}
