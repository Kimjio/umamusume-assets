using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnUpDownArrow : AnUIBase
	{
		public AnSlideBarComponent Component
		{
			get
			{
				return this._component as AnSlideBarComponent;
			}
		}

		public AnButton UpButton
		{
			get
			{
				return this._upButton;
			}
		}

		public AnButton DownButton
		{
			get
			{
				return this._downButton;
			}
		}

		public int Value
		{
			get
			{
				return this._value;
			}
		}

		public int MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		public int MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		public bool EnableLoopValue
		{
			get
			{
				return this._enableLoopValue;
			}
		}

		public Action ActionValueChangeUp { get; set; }

		public Action ActionValueChangeDown { get; set; }

		public Action ActionValueChangeStart { get; set; }

		public AnAction FlActionValueChangeUp { get; protected set; }

		public AnAction FlActionValueChangeDown { get; protected set; }

		public AnAction FlActionValueChangeStart { get; protected set; }

		public AnUpDownArrow()
		{
			this._logTitle = "UI UpDownArrow";
			this._enableDownInputForKeyInput = false;
		}

		public void SetOtherPath(string upButtonMotionPath, string downButtonMotionPath)
		{
			AnUtilityString.ReplaceString(upButtonMotionPath, ref this._upButtonMotionPath);
			AnUtilityString.ReplaceString(downButtonMotionPath, ref this._downButtonMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			this._upButton = null;
			this._downButton = null;
			if (AnUtilityString.IsEmptyString(this._upButtonMotionPath))
			{
				return false;
			}
			AnMotion anMotion = this._root.Find<AnMotion>(this._rootObject, this._upButtonMotionPath, false);
			if (anMotion == null)
			{
				return false;
			}
			AnButtonComponent component = anMotion.ParentObject.GameObject.GetComponent<AnButtonComponent>();
			if (component == null)
			{
				return false;
			}
			if (component.Button == null)
			{
				return false;
			}
			if (!component.Button.Exist)
			{
				return false;
			}
			this._upButton = component.Button;
			this._upButton.FlActionDownInStart.AddAction(new Action<object>(this._OnArrowButtonDown), 0, -1);
			this._upButton.FlActionDownInLoop.AddAction(new Action<object>(this._OnArrowButtonDownLoop), 0, -1);
			this._upButton.FlActionDownLoop.AddAction(new Action<object>(this._OnArrowButtonDownLoop), 0, -1);
			this._upButton.FlActionDownOutStart.AddAction(new Action<object>(this._OnArrowButtonDownOut), 0, -1);
			this._upButton.SetParentUI(this);
			if (AnUtilityString.IsEmptyString(this._downButtonMotionPath))
			{
				return false;
			}
			AnMotion anMotion2 = this._root.Find<AnMotion>(this._rootObject, this._downButtonMotionPath, false);
			if (anMotion2 == null)
			{
				return false;
			}
			AnButtonComponent component2 = anMotion2.ParentObject.GameObject.GetComponent<AnButtonComponent>();
			if (component2 == null)
			{
				return false;
			}
			if (component2.Button == null)
			{
				return false;
			}
			if (!component2.Button.Exist)
			{
				return false;
			}
			this._downButton = component2.Button;
			this._downButton.FlActionDownInStart.AddAction(new Action<object>(this._OnArrowButtonDown), 10, -1);
			this._downButton.FlActionDownInLoop.AddAction(new Action<object>(this._OnArrowButtonDownLoop), 10, -1);
			this._downButton.FlActionDownLoop.AddAction(new Action<object>(this._OnArrowButtonDownLoop), 10, -1);
			this._downButton.FlActionDownOutStart.AddAction(new Action<object>(this._OnArrowButtonDownOut), 10, -1);
			this._downButton.SetParentUI(this);
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this.FlActionValueChangeUp = base._AddAction();
			this.FlActionValueChangeDown = base._AddAction();
		}

		private void _OnArrowButtonDown(object arg)
		{
			int num = (int)arg;
			if (num == 0)
			{
				this._isValueChangeUp = true;
			}
			else if (num == 10)
			{
				this._isValueChangeUp = false;
			}
			if (this._valueChangeByDownInStart)
			{
				if (this._isValueChangeUp)
				{
					this._value++;
				}
				else
				{
					this._value--;
				}
				this._initializeValueChangeFlag = true;
				this._executeValueChangeActionFlag = true;
			}
		}

		private void _OnArrowButtonDownLoop(object arg)
		{
		}

		private void _OnArrowButtonDownOut(object arg)
		{
			int num = (int)arg;
			if (num == 0)
			{
				this._isValueChangeUp = true;
			}
			else if (num == 10)
			{
				this._isValueChangeUp = false;
			}
			if (!this._valueChangeByDownInStart)
			{
				if (this._isValueChangeUp)
				{
					this._value++;
				}
				else
				{
					this._value--;
				}
				this._initializeValueChangeFlag = true;
				this._executeValueChangeActionFlag = true;
			}
		}

		protected override void _Update_Loop_Init()
		{
			this._valueChangeByDownInStart = false;
			this._UpdateDirection();
			base._Update_Loop_Init();
		}

		protected override void _UpdateValueChange()
		{
			if (this._value == this._prevValue)
			{
				return;
			}
			if (this._minValue > this._maxValue)
			{
				this._minValue = this._maxValue;
			}
			if (this._minValue == this._maxValue)
			{
				this._value = this._minValue;
				if (this._downButton.Enable)
				{
					this._downButton.SetEnable(false, AnUIEnableTypes.WithDisableLabel);
				}
				if (this._upButton.Enable)
				{
					this._upButton.SetEnable(false, AnUIEnableTypes.WithDisableLabel);
				}
				return;
			}
			if (!this._enableLoopValue)
			{
				AnUtilityValue.LimitValue(ref this._value, this._minValue, this._maxValue);
				if (this._value <= this._minValue)
				{
					if (!this._upButton.Enable)
					{
						this._upButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
					}
					if (this._downButton.Enable)
					{
						this._downButton.SetEnable(false, AnUIEnableTypes.WithDisableLabel);
						return;
					}
				}
				else if (this._value >= this._maxValue)
				{
					if (this._upButton.Enable)
					{
						this._upButton.SetEnable(false, AnUIEnableTypes.WithDisableLabel);
					}
					if (!this._downButton.Enable)
					{
						this._downButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
						return;
					}
				}
				else
				{
					if (!this._upButton.Enable)
					{
						this._upButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
					}
					if (!this._downButton.Enable)
					{
						this._downButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
					}
				}
				return;
			}
			if (this._value > this._maxValue)
			{
				this._value = this._minValue;
			}
			else if (this._value < this._minValue)
			{
				this._value = this._maxValue;
			}
			if (!this._upButton.Enable)
			{
				this._upButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
			}
			if (!this._downButton.Enable)
			{
				this._downButton.SetEnable(true, AnUIEnableTypes.WithDisableLabel);
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
			this._prevValue = int.MinValue;
		}

		protected override void _UpdateValueChangeStart()
		{
			base._UpdateValueChangeStart();
			if (this._value > this._prevValue)
			{
				this._ExecuteAction(this.ActionValueChangeUp, this.FlActionValueChangeUp);
				base._SetLog(AnLogTypes.ValueChangeUp);
			}
			else
			{
				this._ExecuteAction(this.ActionValueChangeDown, this.FlActionValueChangeDown);
				base._SetLog(AnLogTypes.ValueChangeDown);
			}
			this._ExecuteAction(this.ActionValueChangeStart, this.FlActionValueChangeStart);
			base._SetLog(AnLogTypes.ValueChangeStart);
		}

		protected override void _UpdateValueChangeLoop()
		{
			base._UpdateValueChangeLoop();
			this._currentValueCnageState = AnCommonStateTypes.End;
		}

		private void _UpdateDirection()
		{
			Vector3 localPosition = this._upButton.Motion.ParentObject.Transform.localPosition;
			Vector3 localPosition2 = this._downButton.Motion.ParentObject.Transform.localPosition;
			Vector3 vector = localPosition - localPosition2;
			vector.Normalize();
			if (vector.y > 0.5f || vector.y < -0.5f)
			{
				if (localPosition.y > localPosition2.y)
				{
					this._directionType = AnUIDirectionTypes.BottomToTop;
					return;
				}
				this._directionType = AnUIDirectionTypes.TopToButtom;
				return;
			}
			else
			{
				if (localPosition.x > localPosition2.x)
				{
					this._directionType = AnUIDirectionTypes.LeftToRight;
					return;
				}
				this._directionType = AnUIDirectionTypes.RightToLeft;
				return;
			}
		}

		public override bool _UpdateUI(object arg)
		{
			if (!this._exist)
			{
				return false;
			}
			AnUIInputDirectionTypes anUIInputDirectionTypes = (AnUIInputDirectionTypes)arg;
			if (anUIInputDirectionTypes == AnUIInputDirectionTypes.None)
			{
				return false;
			}
			if (this._directionType == AnUIDirectionTypes.LeftToRight)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				this._valueChangeByDownInStart = true;
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this._upButton.SetDownInToDownOut();
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this._downButton.SetDownInToDownOut();
				}
				return true;
			}
			else if (this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				this._valueChangeByDownInStart = true;
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this._upButton.SetDownInToDownOut();
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this._downButton.SetDownInToDownOut();
				}
				return true;
			}
			else if (this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				this._valueChangeByDownInStart = true;
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this._upButton.SetDownInToDownOut();
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this._downButton.SetDownInToDownOut();
				}
				return true;
			}
			else
			{
				if (this._directionType != AnUIDirectionTypes.TopToButtom)
				{
					return false;
				}
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				this._valueChangeByDownInStart = true;
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this._upButton.SetDownInToDownOut();
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this._downButton.SetDownInToDownOut();
				}
				return true;
			}
		}

		public void SetValue(int value)
		{
			this.SetValue(value, false);
		}

		public void SetValue(int value, bool executeAction)
		{
			this._value = value;
			this._initializeValueChangeFlag = true;
			this._executeValueChangeActionFlag = executeAction;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetRange(int minValue, int maxValue)
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetEnableLoopValue(bool enable)
		{
			this._enableLoopValue = enable;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		protected string _upButtonMotionPath = "";

		protected AnButton _upButton;

		protected string _downButtonMotionPath = "";

		protected AnButton _downButton;

		protected int _value = 5;

		protected int _prevValue = int.MinValue;

		protected int _minValue;

		protected int _maxValue = 10;

		protected bool _enableLoopValue;

		protected bool _isValueChangeUp;

		protected bool _valueChangeByDownInStart;
	}
}
