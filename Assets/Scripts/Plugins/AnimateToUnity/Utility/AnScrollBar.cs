using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnScrollBar : AnUIBase
	{
		public AnScrollBarComponent Component
		{
			get
			{
				return this._component as AnScrollBarComponent;
			}
		}

		public AnMotion BarMotion
		{
			get
			{
				return this._barMotion;
			}
		}

		public AnMotion MoveMotion
		{
			get
			{
				return this._moveMotion;
			}
		}

		public AnButton MoveButton
		{
			get
			{
				return this._moveButton;
			}
		}

		public int StepCount
		{
			get
			{
				return this._stepCount;
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

		public float RangeValue
		{
			get
			{
				return this._rangeValue;
			}
		}

		public float MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		public float MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		public Action ActionValueChangeStart { get; set; }

		public Action ActionValueChangeLoop { get; set; }

		public Action ActionValueChangeEnd { get; set; }

		public AnAction FlActionValueChangeStart { get; protected set; }

		public AnAction FlActionValueChangeLoop { get; protected set; }

		public AnAction FlActionValueChangeEnd { get; protected set; }

		public AnScrollBar()
		{
			this._logTitle = "UI ScrollBar";
		}

		public void SetOtherPath(string barMotionPath, string moveMotionPath, string moveButtonMotionPath)
		{
			AnUtilityString.ReplaceString(barMotionPath, ref this._barMotionPath);
			AnUtilityString.ReplaceString(moveMotionPath, ref this._moveMotionPath);
			AnUtilityString.ReplaceString(moveButtonMotionPath, ref this._moveButtonMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			this._barMotion = null;
			this._moveMotion = null;
			this._moveButton = null;
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
			if (!AnUtilityString.IsEmptyString(this._moveMotionPath))
			{
				this._moveMotion = this._root.Find<AnMotion>(this._barMotion.GameObject, this._moveMotionPath, false);
			}
			if (this._moveMotion == null)
			{
				return false;
			}
			this._moveMotion.SetResetModeType(AnMotion.ResetModeTypes.None);
			this._moveMotion.SetMotionStop();
			this._moveMotion.SetMotionPause(0);
			AnMotion anMotion = null;
			if (!AnUtilityString.IsEmptyString(this._moveButtonMotionPath))
			{
				anMotion = this._root.Find<AnMotion>(this._barMotion.GameObject, this._moveButtonMotionPath, false);
			}
			if (anMotion == null)
			{
				return true;
			}
			AnButtonComponent component = anMotion.ParentObject.GameObject.GetComponent<AnButtonComponent>();
			if (component == null)
			{
				return true;
			}
			if (component.Button == null)
			{
				return true;
			}
			if (!component.Button.Exist)
			{
				return true;
			}
			this._moveButton = component.Button;
			this._moveButton.SetEnableLoopMotionInDownLoop(false);
			this._moveButton.FlActionDownInStart.AddAction(new Action<object>(this._OnDownInMoveButton), 0, -1);
			this._moveButton.FlActionDownInLoop.AddAction(new Action<object>(this._OnDownLoopMoveButton), 0, -1);
			this._moveButton.FlActionDownLoop.AddAction(new Action<object>(this._OnDownLoopMoveButton), 0, -1);
			this._moveButton.FlActionDownOutStart.AddAction(new Action<object>(this._OnDownOutMoveButton), 0, -1);
			this._moveButton.SetParentUI(this);
			this._moveButton.SetEnableSelectInput(false);
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this.FlActionValueChangeStart = base._AddAction();
			this.FlActionValueChangeLoop = base._AddAction();
			this.FlActionValueChangeEnd = base._AddAction();
			this._blendValue = new AnBlendValue(0f, 0f, this._blendTime, AnBlendBase.BlendTypes.Down);
		}

		private void _OnDownInMoveButton(object arg)
		{
			this._executeValueChangeActionFlag = true;
			this._animationFlag = false;
			this._inputFlag = true;
			this._moveButton.SetEnableDownLoopSelection(false);
		}

		private void _OnDownLoopMoveButton(object arg)
		{
			if (this._moveButton.CurrentTouchInput != null)
			{
				AnUtilityVector.GetWorldPositionFromScreen(this._moveButton.CurrentTouchInput.CurrentScreenPosition, this._inputCamera, ref this._worldPositionForTouchInput);
				this._value = this._GetPercentValueFromPosition(this._worldPositionForTouchInput) * (this._maxValue - this._minValue) + this._minValue;
			}
		}

		private void _OnDownOutMoveButton(object arg)
		{
			this._inputFlag = false;
		}

		protected override void _OnActive()
		{
			base._OnActive();
			this._UpdateRangePosition();
			this._UpdateDirection();
		}

		protected override void _Update_DownIn_Init()
		{
			if (base.CurrentTouchInput != null && this._moveButton != null)
			{
				this._moveButton.SetEnableDownLoopSelection(true);
				this._executeValueChangeActionFlag = true;
				this._animationFlag = false;
				this._inputFlag = true;
				AnUtilityVector.GetWorldPositionFromScreen(base.CurrentTouchInput.CurrentScreenPosition, this._inputCamera, ref this._worldPositionForTouchInput);
				this._value = this._GetPercentValueFromPosition(this._worldPositionForTouchInput) * (this._maxValue - this._minValue) + this._minValue;
			}
			base._Update_DownIn_Init();
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
			if (this._minValue > this._maxValue)
			{
				this._minValue = this._maxValue;
			}
			AnUtilityValue.LimitValue(ref this._rangeValue, 0f, this._maxValue - this._minValue);
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
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				this.FixScrollValue(this._blendValue.CurrentValue, out num, out num2, out num3);
				this._barMotion.SetMotionPause((this._barMotion.CurrentLabelTimeLength - this._root.Parameter.OneFrameTime) * num2);
				this._moveMotion.SetMotionPause((this._moveMotion.CurrentLabelTimeLength - this._root.Parameter.OneFrameTime) * num3);
				if (this._blendValue.CurrentBlendValue >= 1f)
				{
					this._animationFlag = false;
					this._value = num;
					return;
				}
			}
			else
			{
				if (this._value == this._prevValue)
				{
					return;
				}
				float num4 = 0f;
				float num5 = 0f;
				this.FixScrollValue(this._value, out this._value, out num4, out num5);
				this._barMotion.SetMotionPause((this._barMotion.CurrentLabelTimeLength - this._root.Parameter.OneFrameTime) * num4);
				this._moveMotion.SetMotionPause((this._moveMotion.CurrentLabelTimeLength - this._root.Parameter.OneFrameTime) * num5);
			}
		}

		private void FixScrollValue(float inputValue, out float outputValue, out float outputValuePercent, out float outputRangePercent)
		{
			if (this._maxValue - this._minValue > 0f)
			{
				outputValuePercent = (inputValue - this._minValue) / (this._maxValue - this._minValue);
				outputRangePercent = this._rangeValue / (this._maxValue - this._minValue);
			}
			else
			{
				outputValuePercent = 0f;
				outputRangePercent = 1f;
			}
			if (this._stepCount > 0)
			{
				float num = 1f / (float)this._stepCount;
				float num2 = outputValuePercent % num;
				if (num2 > num * 0.5f)
				{
					outputValuePercent += num - num2;
				}
				else
				{
					outputValuePercent -= num2;
				}
			}
			AnUtilityValue.LimitValue(ref outputValuePercent, 0f, 1f - outputRangePercent);
			outputValue = (this._maxValue - this._minValue) * outputValuePercent + this._minValue;
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
			if (!this._inputFlag && !this._animationFlag)
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

		private void _UpdateDirection()
		{
			Vector3 vector = this._endPosition - this._startPosition;
			vector.Normalize();
			if (vector.y > 0.5f || vector.y < -0.5f)
			{
				if (this._endPosition.y > this._startPosition.y)
				{
					this._directionType = AnUIDirectionTypes.BottomToTop;
					return;
				}
				this._directionType = AnUIDirectionTypes.TopToButtom;
				return;
			}
			else
			{
				if (this._endPosition.x > this._startPosition.x)
				{
					this._directionType = AnUIDirectionTypes.LeftToRight;
					return;
				}
				this._directionType = AnUIDirectionTypes.RightToLeft;
				return;
			}
		}

		private void _UpdateRangePosition()
		{
			float currentLabelTime = this._barMotion.CurrentLabelTime;
			float currentLabelTime2 = this._moveMotion.CurrentLabelTime;
			this._barMotion.SetMotionPause(0f);
			this._moveMotion.SetMotionPause(0f);
			this._startPosition = this._moveMotion.ParentObject.GameObject.transform.position;
			this._barMotion.SetMotionPause(this._barMotion.CurrentLabelTimeLength);
			this._moveMotion.SetMotionPause(0f);
			this._endPosition = this._moveMotion.ParentObject.GameObject.transform.position;
			this._barMotion.SetMotionPause(currentLabelTime);
			this._moveMotion.SetMotionPause(currentLabelTime2);
		}

		private float _GetPercentValueFromPosition(Vector3 worldPosition)
		{
			float num = 0f;
			float num2 = 0f;
			this._UpdateRangePosition();
			this._UpdateDirection();
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				num2 = this._startPosition.y - this._endPosition.y;
				num = this._startPosition.y - worldPosition.y;
			}
			else if (this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				num2 = this._endPosition.y - this._startPosition.y;
				num = worldPosition.y - this._startPosition.y;
			}
			else if (this._directionType == AnUIDirectionTypes.LeftToRight)
			{
				num2 = this._endPosition.x - this._startPosition.x;
				num = worldPosition.x - this._startPosition.x;
			}
			else if (this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				num2 = this._startPosition.x - this._endPosition.x;
				num = this._startPosition.x - worldPosition.x;
			}
			if (num2 == 0f)
			{
				return 0f;
			}
			if (num < 0f)
			{
				return 0f;
			}
			if (num > num2)
			{
				return 1f;
			}
			return num / num2;
		}

		public override bool _UpdateUI(object arg)
		{
			AnUIInputDirectionTypes anUIInputDirectionTypes = (AnUIInputDirectionTypes)arg;
			if (anUIInputDirectionTypes == AnUIInputDirectionTypes.None)
			{
				return false;
			}
			if (this._swipeDirectionType == AnUIDirectionTypes.LeftToRight)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetValue(this._value + 50f);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetValue(this._value - 50f);
				}
			}
			if (this._swipeDirectionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetValue(this._value + 50f);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetValue(this._value - 50f);
				}
			}
			if (this._swipeDirectionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetValue(this._value + 50f);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetValue(this._value - 50f);
				}
			}
			if (this._swipeDirectionType == AnUIDirectionTypes.TopToButtom)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetValue(this._value + 50f);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetValue(this._value - 50f);
				}
			}
			return true;
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

		public void SetRange(float minValue, float maxValue, float rangeValue)
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._rangeValue = rangeValue;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetStepCount(int stepCount)
		{
			this._stepCount = stepCount;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public override void SetEnable(bool enable, AnUIEnableTypes enableType = AnUIEnableTypes.Normal)
		{
			base.SetEnable(enable, enableType);
			if (this._moveButton != null)
			{
				this._moveButton.SetEnable(enable, enableType);
			}
		}

		public override void SetParentUI(AnUIBase parentInputUI)
		{
			base.SetParentUI(parentInputUI);
			if (this._moveButton != null)
			{
				this._moveButton.SetParentUI(parentInputUI);
			}
		}

		protected string _barMotionPath = "frm_bar";

		protected AnMotion _barMotion;

		protected string _moveMotionPath = "frm_move";

		protected AnMotion _moveMotion;

		protected string _moveButtonMotionPath = "btn_move";

		protected AnButton _moveButton;

		protected Vector3 _startPosition = Vector3.zero;

		protected Vector3 _endPosition = Vector3.zero;

		protected float _value;

		protected float _prevValue = float.MinValue;

		protected float _blendTime = 0.5f;

		protected AnBlendValue _blendValue;

		protected float _minValue;

		protected float _maxValue = 100f;

		protected float _rangeValue = 10f;

		protected int _stepCount;

		protected Vector3 _worldPositionForTouchInput = Vector3.zero;
	}
}
