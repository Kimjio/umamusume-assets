using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnScrollBase : AnUIBase
	{
		public float CurrentScrollPosition
		{
			get
			{
				return this._currentScrollPosition;
			}
		}

		public float ScrollRange
		{
			get
			{
				return this._scrollRange;
			}
		}

		public float MinScrollPosition
		{
			get
			{
				return this._minScrollPosition;
			}
		}

		public float MaxScrollPosition
		{
			get
			{
				return this._maxScrollPosition;
			}
		}

		public AnScrollBase.ScrollModeTypes ScrollModeType
		{
			get
			{
				return this._scrollModeType;
			}
		}

		public int SelectActionId
		{
			get
			{
				return this._selectActionId;
			}
		}

		public bool IsAutoScroll
		{
			get
			{
				return this._isAutoScroll;
			}
		}

		public bool IsAutoScrollAnimation
		{
			get
			{
				return this._isAutoScrollAnimation;
			}
		}

		public AnScrollBase.ScrollStateTypes CurrentScrollState
		{
			get
			{
				return this._currentScrollState;
			}
		}

		public Action ActionScrollStart { get; set; }

		public Action ActionScrollLoop { get; set; }

		public Action ActionScrollOutStart { get; set; }

		public Action ActionScrollOutLoop { get; set; }

		public Action ActionScrollSpringStart { get; set; }

		public Action ActionScrollSpringLoop { get; set; }

		public Action ActionScrollEnd { get; set; }

		public AnAction FlActionScrollStart { get; protected set; }

		public AnAction FlActionScrollLoop { get; protected set; }

		public AnAction FlActionScrollOutStart { get; protected set; }

		public AnAction FlActionScrollOutLoop { get; protected set; }

		public AnAction FlActionScrollSpringStart { get; protected set; }

		public AnAction FlActionScrollSpringLoop { get; protected set; }

		public AnAction FlActionScrollEnd { get; protected set; }

		[Obsolete("Use FlActionScrollStart")]
		public AnAction FlActionScrollInit
		{
			get
			{
				return this.FlActionScrollStart;
			}
			set
			{
				this.FlActionScrollStart = value;
			}
		}

		[Obsolete("Use FlActionWaitInit")]
		public AnAction FlActionWaitInit
		{
			get
			{
				return this.FlActionScrollEnd;
			}
			set
			{
				this.FlActionScrollEnd = value;
			}
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this.FlActionScrollStart = base._AddAction();
			this.FlActionScrollLoop = base._AddAction();
			this.FlActionScrollOutStart = base._AddAction();
			this.FlActionScrollOutLoop = base._AddAction();
			this.FlActionScrollSpringStart = base._AddAction();
			this.FlActionScrollSpringLoop = base._AddAction();
			this.FlActionScrollEnd = base._AddAction();
			this._scrollBlendValue = new AnBlendValue(0f, 0f, 0f, AnBlendBase.BlendTypes.Down);
		}

		protected override void _Reset()
		{
			base._Reset();
			if (this._useTargetScrollPosition)
			{
				this._currentScrollPosition = this._targetScrollPosition;
				this._useTargetScrollPosition = false;
			}
			this._currentScrollState = AnScrollBase.ScrollStateTypes.None;
		}

		protected override void _ResetPrevValue()
		{
			base._ResetPrevValue();
			this._prevScrollPosition = float.MinValue;
		}

		public override void _Release()
		{
			if (!this._exist)
			{
				return;
			}
			this._scrollBlendValue = null;
			base._Release();
		}

		protected override void _Update_Common_Start()
		{
			base._Update_Common_Start();
			switch (this._currentScrollState)
			{
			case AnScrollBase.ScrollStateTypes.Scroll_Init:
				this._currentScrollState = AnScrollBase.ScrollStateTypes.Scroll_Loop;
				return;
			case AnScrollBase.ScrollStateTypes.Scroll_Loop:
			case AnScrollBase.ScrollStateTypes.ScrollOut_Loop:
				break;
			case AnScrollBase.ScrollStateTypes.ScrollOut_Init:
				this._currentScrollState = AnScrollBase.ScrollStateTypes.ScrollOut_Loop;
				return;
			case AnScrollBase.ScrollStateTypes.ScrollSpring_Init:
				this._currentScrollState = AnScrollBase.ScrollStateTypes.ScrollSpring_Loop;
				break;
			default:
				return;
			}
		}

		protected override void _Update(bool update = true)
		{
			base._Update(update);
			switch (this._currentScrollState)
			{
			case AnScrollBase.ScrollStateTypes.Scroll_Loop:
				this._Update_Scroll_Loop();
				return;
			case AnScrollBase.ScrollStateTypes.ScrollOut_Init:
			case AnScrollBase.ScrollStateTypes.ScrollSpring_Init:
				break;
			case AnScrollBase.ScrollStateTypes.ScrollOut_Loop:
				this._Update_ScrollOut_Loop();
				return;
			case AnScrollBase.ScrollStateTypes.ScrollSpring_Loop:
				this._Update_ScrollSpring_Loop();
				break;
			default:
				return;
			}
		}

		protected override void _Update_DownIn_Init()
		{
			base._Update_DownIn_Init();
			this._currentScrollCancelTime = 0f;
		}

		protected override void _Update_DownLoop_Loop()
		{
			base._Update_DownLoop_Loop();
			if (this._isSwiping)
			{
				this._Update_Scroll_Init();
				return;
			}
			if (this._currentScrollCancelTime > this._scrollCancelTime)
			{
				this._Update_Scroll_End();
				return;
			}
			this._currentScrollCancelTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
		}

		protected virtual void _Update_Scroll_Init()
		{
			base._SetLog(AnLogTypes.___________________________SCROLL);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.None;
			this._currentScrollState = AnScrollBase.ScrollStateTypes.Scroll_Init;
			this._ExecuteAction(this.ActionScrollStart, this.FlActionScrollStart);
			base._SetLog(AnLogTypes.ScrollStart);
			this._startWorldPosition = 0f;
			this._currentWorldPosition = 0f;
			this._diffWorldPosition = 0f;
			if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				this._startWorldPosition = AnUtilityVector.GetWorldPositionFromScreen(this._swipeCurrentScreenPosition, this._inputCamera).y;
			}
			else if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				this._startWorldPosition = AnUtilityVector.GetWorldPositionFromScreen(this._swipeCurrentScreenPosition, this._inputCamera).x;
			}
			this._startScrollPosition = this._currentScrollPosition;
			this._blankLength = this._scrollRange * this._blankLengthMultiplyValue;
		}

		protected virtual void _Update_Scroll_Loop()
		{
			this._ExecuteAction(this.ActionScrollLoop, this.FlActionScrollLoop);
			if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				this._currentWorldPosition = AnUtilityVector.GetWorldPositionFromScreen(this._swipeCurrentScreenPosition, this._inputCamera).y;
				if (this._directionType == AnUIDirectionTypes.TopToButtom)
				{
					this._diffWorldPosition = this._currentWorldPosition - this._startWorldPosition;
				}
				else
				{
					this._diffWorldPosition = this._startWorldPosition - this._currentWorldPosition;
				}
			}
			else if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				this._currentWorldPosition = AnUtilityVector.GetWorldPositionFromScreen(this._swipeCurrentScreenPosition, this._inputCamera).x;
				if (this._directionType == AnUIDirectionTypes.LeftToRight)
				{
					this._diffWorldPosition = this._startWorldPosition - this._currentWorldPosition;
				}
				else
				{
					this._diffWorldPosition = this._currentWorldPosition - this._startWorldPosition;
				}
			}
			this._currentScrollPosition = this._diffWorldPosition + this._startScrollPosition;
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				AnUtilityValue.LimitValue(ref this._currentScrollPosition, this._minScrollPosition - this._blankLength, this._maxScrollPosition + this._blankLength);
			}
			if (base._GetInputUpType() != AnInputUpTypes.NotUp)
			{
				this._Update_ScrollOut_Init();
			}
		}

		protected virtual void _Update_ScrollOut_Init()
		{
			this._currentScrollState = AnScrollBase.ScrollStateTypes.ScrollOut_Init;
			this._currentInputType = AnInputTypes.None;
			this._ExecuteAction(this.ActionScrollOutStart, this.FlActionScrollOutStart);
			base._SetLog(AnLogTypes.ScrollOutStart);
			this._tempVector0.x = 0f;
			this._tempVector0.y = 0f;
			this._tempVector0.z = 0f;
			this._tempVector1.x = 0f;
			this._tempVector1.y = 0f;
			this._tempVector1.z = 0f;
			if (base.CurrentTouchInput != null)
			{
				if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
				{
					this._tempVector0.y = base.CurrentTouchInput.AvarageScreenSpeed * base.CurrentTouchInput.AvarageScreenDirection.y;
				}
				else if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
				{
					this._tempVector0.x = -base.CurrentTouchInput.AvarageScreenSpeed * base.CurrentTouchInput.AvarageScreenDirection.x;
				}
			}
			this._tempVector0 = AnUtilityVector.GetWorldPositionFromScreen(this._tempVector0, this._inputCamera);
			this._tempVector1 = AnUtilityVector.GetWorldPositionFromScreen(this._tempVector1, this._inputCamera);
			this._tempVector0 -= this._tempVector1;
			if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				this._outStartSpeed = this._tempVector0.y * AnMonoSingleton<AnRootManager>.Instance._GetScrollSpeedValue();
			}
			else if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				this._outStartSpeed = this._tempVector0.x * AnMonoSingleton<AnRootManager>.Instance._GetScrollSpeedValue();
			}
			float absValue = AnUtilityValue.GetAbsValue(this._outStartSpeed);
			float sign = AnUtilityValue.GetSign(this._outStartSpeed);
			if (absValue < this._outMinSpeed)
			{
				this._outStartAccel = 0f;
				this._outStartSpeed = 0f;
			}
			else
			{
				this._outStartSpeed = AnUtilityValue.GetLimitValue(absValue, 0.1f, float.MaxValue) * sign;
				this._outStartAccel = this._outStartSpeed * AnMonoSingleton<AnRootManager>.Instance._GetScrollAccelValue();
			}
			this._outCurrentSpeed = this._outStartSpeed;
			this._outCurrentAccel = this._outStartAccel;
		}

		protected virtual void _Update_ScrollOut_Loop()
		{
			this._ExecuteAction(this.ActionScrollOutLoop, this.FlActionScrollOutLoop);
			this._outCurrentSpeed -= this._outCurrentAccel;
			this._currentScrollPosition += this._outCurrentSpeed;
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				if (this._currentScrollPosition < -this._blankLength + this._minScrollPosition || this._currentScrollPosition > this._maxScrollPosition + this._blankLength)
				{
					this._Update_ScrollSpring_Init();
					return;
				}
				if (AnUtilityValue.GetAbsValue(this._outCurrentSpeed) < this._outMinSpeed)
				{
					this._Update_ScrollSpring_Init();
					return;
				}
			}
			else if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless && AnUtilityValue.GetAbsValue(this._outCurrentSpeed) < this._outMinSpeed)
			{
				if (this._useTargetScrollPosition)
				{
					this._Update_ScrollSpring_Init();
					return;
				}
				this._Update_Scroll_End();
			}
		}

		protected virtual void _Update_ScrollSpring_Init()
		{
			this._scrollBlendValue.SetStartValue(this._currentScrollPosition);
			this._scrollBlendValue.SetBlendTime(0.5f);
			this._scrollBlendValue.SetBlendType(AnBlendBase.BlendTypes.Down);
			this._scrollBlendValue.SetEndValue(this._currentScrollPosition);
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				if (this._useTargetScrollPosition)
				{
					if (this._currentScrollPosition != this._targetScrollPosition)
					{
						this._scrollBlendValue.SetEndValue(this._targetScrollPosition);
					}
				}
				else if (this._currentScrollPosition >= this._minScrollPosition && this._currentScrollPosition <= this._maxScrollPosition)
				{
					this._Update_Scroll_End();
					return;
				}
				if (this._currentScrollPosition < this._minScrollPosition)
				{
					this._scrollBlendValue.SetEndValue(this._minScrollPosition);
				}
				else if (this._currentScrollPosition > this._maxScrollPosition)
				{
					this._scrollBlendValue.SetEndValue(this._maxScrollPosition);
				}
			}
			else if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless && this._useTargetScrollPosition)
			{
				this._scrollBlendValue.SetEndValue(this._targetScrollPosition);
			}
			this._scrollBlendValue.Reset();
			this._currentScrollState = AnScrollBase.ScrollStateTypes.ScrollSpring_Init;
			this._ExecuteAction(this.ActionScrollSpringStart, this.FlActionScrollSpringStart);
			base._SetLog(AnLogTypes.ScrollSprintStart);
		}

		protected virtual void _Update_ScrollSpring_Loop()
		{
			this._ExecuteAction(this.ActionScrollSpringLoop, this.FlActionScrollSpringLoop);
			this._scrollBlendValue.Update(AnMonoSingleton<AnRootManager>.Instance._currentDeltaTime);
			this._currentScrollPosition = this._scrollBlendValue.CurrentValue;
			if (this._scrollBlendValue.CurrentBlendValue >= 1f)
			{
				this._Update_Scroll_End();
			}
		}

		protected virtual void _Update_Scroll_End()
		{
			this._ExecuteAction(this.ActionScrollEnd, this.FlActionScrollEnd);
			base._SetLog(AnLogTypes.ScrollEnd);
			this._currentScrollState = AnScrollBase.ScrollStateTypes.None;
			this._useTargetScrollPosition = false;
			this._Update_Loop_Init();
		}

		protected override void _UpdateValueChange()
		{
			base._UpdateValueChange();
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				if (this._allScrollLength > this._scrollRange)
				{
					AnUtilityValue.LimitValue(ref this._currentScrollPosition, -this._blankLength + this._minScrollPosition, this._blankLength + this._maxScrollPosition);
					return;
				}
				this._currentScrollPosition = 0f;
			}
		}

		protected override void _UpdatePrevValueChange()
		{
			base._UpdatePrevValueChange();
			this._prevScrollPosition = this._currentScrollPosition;
		}

		public virtual void SetBlankLength(float blankMultiplyValue)
		{
			this._blankLengthMultiplyValue = blankMultiplyValue;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public virtual void SetAutoScroll(bool enable)
		{
			this._isAutoScroll = enable;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public virtual void SetAutoScrollAnimation(bool enable)
		{
			this._isAutoScrollAnimation = enable;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		protected AnScrollBase.ScrollModeTypes _scrollModeType;

		protected AnScrollBase.ScrollStateTypes _currentScrollState;

		protected float _startWorldPosition;

		protected float _currentWorldPosition;

		protected float _diffWorldPosition;

		protected float _startScrollPosition;

		protected float _currentScrollPosition;

		protected float _prevScrollPosition = float.MinValue;

		protected float _allScrollLength;

		protected float _scrollRange;

		protected float _minScrollPosition;

		protected float _maxScrollPosition;

		protected bool _useTargetScrollPosition;

		protected float _targetScrollPosition;

		protected float _outMinSpeed = 0.05f;

		protected float _outStartSpeed;

		protected float _outCurrentSpeed;

		protected float _outStartAccel;

		protected float _outCurrentAccel;

		protected float _blankLength;

		protected float _blankLengthMultiplyValue = 0.5f;

		protected AnBlendValue _scrollBlendValue;

		protected float _scrollCancelTime = 1f;

		protected float _currentScrollCancelTime;

		protected int _selectActionId = 3691215;

		protected bool _isAutoScroll = true;

		protected bool _isAutoScrollAnimation = true;

		protected Vector3 _tempVector0;

		protected Vector3 _tempVector1;

		public enum ScrollStateTypes
		{
			None,
			Scroll_Init,
			Scroll_Loop,
			ScrollOut_Init,
			ScrollOut_Loop,
			ScrollSpring_Init,
			ScrollSpring_Loop
		}

		public enum ScrollModeTypes
		{
			Normal,
			Endless
		}
	}
}
