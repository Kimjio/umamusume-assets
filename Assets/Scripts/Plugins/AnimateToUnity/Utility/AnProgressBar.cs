using System;

namespace AnimateToUnity.Utility
{
	public class AnProgressBar : AnUIBase
	{
		public AnProgressBarComponent Component
		{
			get
			{
				return this._component as AnProgressBarComponent;
			}
		}

		public AnMotion BarMotion
		{
			get
			{
				return this._barMotion;
			}
		}

		public float Value
		{
			get
			{
				return this._value;
			}
		}

		public float CurrentValue
		{
			get
			{
				return this._blendValue.CurrentValue;
			}
		}

		public float MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		public float MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		public float BlendTime
		{
			get
			{
				return this._blendTime;
			}
		}

		public AnBlendValue BlendValue
		{
			get
			{
				return this._blendValue;
			}
		}

		public Action ActionValueChangeStart { get; set; }

		public Action ActionValueChangeLoop { get; set; }

		public Action ActionValueChangeEnd { get; set; }

		public AnAction FlActionValueChangeStart { get; protected set; }

		public AnAction FlActionValueChangeLoop { get; protected set; }

		public AnAction FlActionValueChangeEnd { get; protected set; }

		public AnProgressBar()
		{
			this._logTitle = "UI ProgressBar";
		}

		public void SetOtherPath(string barMotionPath)
		{
			AnUtilityString.ReplaceString(barMotionPath, ref this._barMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			this._barMotion = null;
			if (!AnUtilityString.IsEmptyString(this._barMotionPath))
			{
				this._barMotion = this._root.Find<AnMotion>(this._motion.GameObject, this._barMotionPath, false);
			}
			if (this._barMotion == null)
			{
				this._barMotion = this._motion;
			}
			this._barMotion.SetResetModeType(AnMotion.ResetModeTypes.None);
			this._barMotion.SetMotionStop();
			this._barMotion.SetMotionPause(0);
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this._blendValue = new AnBlendValue(0f, 0f, 0.5f, AnBlendBase.BlendTypes.Down);
			this.FlActionValueChangeStart = base._AddAction();
			this.FlActionValueChangeLoop = base._AddAction();
			this.FlActionValueChangeEnd = base._AddAction();
		}

		public override void _Release()
		{
			if (!this._exist)
			{
				return;
			}
			this._blendValue = null;
			base._Release();
		}

		protected override void _InitializeValueChange()
		{
			base._InitializeValueChange();
			if (this._animationFlag)
			{
				this._blendValue.SetStartValue(this._blendValue.CurrentValue);
			}
			else
			{
				this._blendValue.SetStartValue(this._value);
			}
			this._blendValue.SetEndValue(this._value);
			this._blendValue.SetBlendTime(this._blendTime);
			this._blendValue.Reset();
			this._ResetPrevValue();
		}

		protected override void _UpdateValueChange()
		{
			base._UpdateValueChange();
			if (this._minValue >= this._maxValue)
			{
				this._minValue = this._maxValue;
			}
			AnUtilityValue.LimitValue(ref this._value, this._minValue, this._maxValue);
			if (this._animationFlag)
			{
				if (this._updateFlag)
				{
					this._blendValue.Update(AnMonoSingleton<AnRootManager>.Instance._currentDeltaTime);
				}
				else
				{
					this._blendValue.Update(0f);
				}
				if (this._minValue != this._maxValue)
				{
					this._barMotion.SetMotionPause((this._blendValue.CurrentValue - this._minValue) / (this._maxValue - this._minValue) * this._barMotion.CurrentLabelTimeLength);
				}
				else
				{
					this._barMotion.SetMotionPause(this._barMotion.CurrentLabelTimeLength);
				}
				if (this._blendValue.CurrentBlendValue >= 1f)
				{
					this._value = this._blendValue.CurrentValue;
					this._animationFlag = false;
					return;
				}
			}
			else
			{
				if (this._value == this._prevValue)
				{
					return;
				}
				if (this._minValue != this._maxValue)
				{
					this._barMotion.SetMotionPause((this._value - this._minValue) / (this._maxValue - this._minValue) * this._barMotion.CurrentLabelTimeLength);
					return;
				}
				this._barMotion.SetMotionPause(this._barMotion.CurrentLabelTimeLength);
			}
		}

		protected override void _UpdatePrevValueChange()
		{
			base._UpdatePrevValueChange();
			this._prevValue = this._value;
		}

		protected override void _ResetPrevValue()
		{
			base._ResetPrevValue();
			this._prevValue = -2.1474836E+09f;
		}

		protected override void _UpdateValueChangeStart()
		{
			base._UpdateValueChangeStart();
			this._ExecuteAction(this.ActionValueChangeStart, this.FlActionValueChangeStart);
			base._SetLog(AnLogTypes.ValueChangeStart);
		}

		protected override void _UpdateValueChangeLoop()
		{
			base._UpdateValueChangeLoop();
			this._ExecuteAction(this.ActionValueChangeLoop, this.FlActionValueChangeLoop);
			if (!this._animationFlag)
			{
				this._currentValueCnageState = AnCommonStateTypes.End;
			}
		}

		protected override void _UpdateValueChangeEnd()
		{
			base._UpdateValueChangeEnd();
			this._ExecuteAction(this.ActionValueChangeEnd, this.FlActionValueChangeEnd);
			base._SetLog(AnLogTypes.ValueChangeEnd);
		}

		public void SetValue(float value)
		{
			this.SetValue(value, false, false);
		}

		public void SetValue(float value, bool animation)
		{
			this.SetValue(value, animation, true);
		}

		public void SetValue(float value, bool animation, bool executeAction)
		{
			this._value = value;
			this._animationFlag = animation;
			this._initializeValueChangeFlag = true;
			this._executeValueChangeActionFlag = executeAction;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetRange(float minValue, float maxValue)
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetBlendTime(float time)
		{
			this._blendTime = time;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		protected string _barMotionPath = "";

		protected AnMotion _barMotion;

		protected float _value;

		protected float _minValue;

		protected float _maxValue = 100f;

		protected float _blendTime = 0.5f;

		protected AnBlendValue _blendValue;

		protected float _prevValue = -2.1474836E+09f;
	}
}
