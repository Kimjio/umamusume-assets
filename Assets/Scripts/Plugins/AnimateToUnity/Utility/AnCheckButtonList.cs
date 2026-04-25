using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnCheckButtonList : AnUIBase
	{
		public AnCheckButtonListComponent Component
		{
			get
			{
				return this._component as AnCheckButtonListComponent;
			}
		}

		public int CurrentIndex
		{
			get
			{
				return this._currentIndex;
			}
		}

		public int Count
		{
			get
			{
				return this._currentCount;
			}
		}

		public int MaxCount
		{
			get
			{
				return this._maxCount;
			}
		}

		public List<AnCheckButton> CheckButtonList
		{
			get
			{
				return this._checkButtonList;
			}
		}

		public Action ActionValueChangeStart { get; set; }

		public Action ActionValueChangeLoop { get; set; }

		public Action ActionValueChangeEnd { get; set; }

		public AnAction FlActionValueChangeStart { get; protected set; }

		public AnAction FlActionValueChangeLoop { get; protected set; }

		public AnAction FlActionValueChangeEnd { get; protected set; }

		public AnCheckButtonList()
		{
			this._logTitle = "UI CheckButtonList";
		}

		public void SetOtherPath(string checkButtonObjectPrefixName)
		{
			AnUtilityString.ReplaceString(checkButtonObjectPrefixName, ref this._checkButtonObjectPrefixName);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			if (AnUtilityString.IsEmptyString(this._checkButtonObjectPrefixName))
			{
				return false;
			}
			if (this._checkButtonList == null)
			{
				this._checkButtonList = new List<AnCheckButton>();
			}
			this._checkButtonList.Clear();
			for (int i = 0; i < this._maxCount; i++)
			{
				string text = this._checkButtonObjectPrefixName + AnUtilityString.GetNumberString(i, 2);
				AnObject anObject = this._motion.Root.Find<AnObject>(this._motion.GameObject, text, false);
				if (anObject == null || anObject.ChildMotion == null)
				{
					break;
				}
				AnCheckButtonComponent component = anObject.GameObject.GetComponent<AnCheckButtonComponent>();
				if (component == null || component.CheckButton == null || !component.CheckButton.Exist)
				{
					break;
				}
				component.CheckButton.FlActionCheckStart.AddAction(new Action<object>(this._OnCheckStart), i, -1);
				component.CheckButton.FlActionCheckLoop.AddAction(new Action<object>(this._OnCheckLoop), i, -1);
				component.CheckButton.FlActionChecked.AddAction(new Action<object>(this._OnCheckEnd), i, -1);
				component.CheckButton.SetParentUI(this);
				this._checkButtonList.Add(component.CheckButton);
			}
			if (this._checkButtonList.Count == 0)
			{
				return false;
			}
			this._maxCount = this._checkButtonList.Count;
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this.FlActionValueChangeStart = base._AddAction();
			this.FlActionValueChangeLoop = base._AddAction();
			this.FlActionValueChangeEnd = base._AddAction();
		}

		private void _OnCheckStart(object arg)
		{
			int num = (int)arg;
			this._currentIndex = num;
			this._inputFlag = true;
			this._animationFlag = true;
			this._executeValueChangeActionFlag = true;
		}

		private void _OnCheckLoop(object arg)
		{
			int num = (int)arg;
			this._currentIndex = num;
		}

		private void _OnCheckEnd(object arg)
		{
			int num = (int)arg;
			this._currentIndex = num;
			this._checkButtonList[this._currentIndex].SetEnable(false);
			this._inputFlag = false;
			this._animationFlag = false;
		}

		public override void _Release()
		{
			if (!this._exist)
			{
				return;
			}
			if (AnMonoSingleton<AnRootManager>.Instance != null)
			{
				for (int i = 0; i < this._checkButtonList.Count; i++)
				{
					if (this._checkButtonList[i] != null)
					{
						this._checkButtonList[i]._Release();
					}
				}
			}
			this._checkButtonList.Clear();
			this._checkButtonList = null;
			base._Release();
		}

		protected override void _UpdateValueChange()
		{
			base._UpdateValueChange();
			AnUtilityValue.LimitValue(ref this._currentCount, 0, this._checkButtonList.Count);
			AnUtilityValue.LimitValue(ref this._currentIndex, 0, this._currentCount - 1);
			this._UpdateDirectionAndObjectSpace();
			this._UpdateCheckButtonPosition();
			this._UpdateCheckButtonState();
		}

		private void _UpdateDirectionAndObjectSpace()
		{
			this._objectSpace = 0f;
			this._directionType = AnUIDirectionTypes.LeftToRight;
			if (this._checkButtonList.Count <= 1)
			{
				return;
			}
			this._startPosition = this._checkButtonList[0].Motion.GameObject.transform.position;
			this._endPosition = this._checkButtonList[1].Motion.GameObject.transform.position;
			Vector3 vector = this._startPosition - this._endPosition;
			vector.Normalize();
			if (vector.y > 0.5f || vector.y < -0.5f)
			{
				if (this._endPosition.y > this._startPosition.y)
				{
					this._directionType = AnUIDirectionTypes.BottomToTop;
				}
				else
				{
					this._directionType = AnUIDirectionTypes.TopToButtom;
				}
				this._objectSpace = AnUtilityValue.GetAbsValue(this._endPosition.y - this._startPosition.y);
				return;
			}
			if (this._endPosition.x > this._startPosition.x)
			{
				this._directionType = AnUIDirectionTypes.LeftToRight;
			}
			else
			{
				this._directionType = AnUIDirectionTypes.RightToLeft;
			}
			this._objectSpace = AnUtilityValue.GetAbsValue(this._endPosition.x - this._startPosition.x);
		}

		private void _UpdateCheckButtonPosition()
		{
			if (this._currentCount == this._prevCount)
			{
				return;
			}
			for (int i = 0; i < this._checkButtonList.Count; i++)
			{
				if (i >= this._currentCount)
				{
					this._checkButtonList[i].Motion.SetVisible(false);
				}
				else
				{
					float num = ((float)(this._currentCount - 1) * 0.5f - (float)i) * this._objectSpace;
					this._checkButtonList[i].Motion.SetVisible(true);
					Vector3 vector = new Vector3(0f, 0f, 0f);
					if (this._directionType == AnUIDirectionTypes.LeftToRight)
					{
						vector.x = -num;
					}
					else if (this._directionType == AnUIDirectionTypes.RightToLeft)
					{
						vector.x = num;
					}
					else if (this._directionType == AnUIDirectionTypes.TopToButtom)
					{
						vector.x = -num;
					}
					else if (this._directionType == AnUIDirectionTypes.BottomToTop)
					{
						vector.y = num;
					}
					this._checkButtonList[i].Motion.GameObject.transform.position = this._motion.GameObject.transform.position + vector;
				}
			}
		}

		private void _UpdateCheckButtonState()
		{
			if (this._currentIndex == this._prevIndex)
			{
				return;
			}
			for (int i = 0; i < this._checkButtonList.Count; i++)
			{
				if (i < this._currentCount)
				{
					if (this._currentIndex == i)
					{
						this._checkButtonList[i].SetCheck(true, this._animationFlag, this._inputFlag);
						if (!this._inputFlag)
						{
							this._checkButtonList[i].SetEnable(false);
						}
					}
					else
					{
						if (this._checkButtonList[i].GetCheck())
						{
							this._checkButtonList[i].SetCheck(false, false, false);
						}
						if (!this._checkButtonList[i].Enable)
						{
							this._checkButtonList[i].SetEnable(true);
						}
					}
				}
			}
			this._animationFlag = false;
		}

		protected override void _UpdatePrevValueChange()
		{
			base._UpdatePrevValueChange();
			this._prevIndex = this._currentIndex;
			this._prevCount = this._currentCount;
		}

		protected override void _ResetPrevValue()
		{
			base._ResetPrevValue();
			this._prevIndex = int.MinValue;
			this._prevCount = int.MinValue;
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

		public override bool _UpdateUI(object arg)
		{
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
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetIndex(this._currentIndex + 1, true, true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetIndex(this._currentIndex - 1, true, true);
				}
			}
			if (this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetIndex(this._currentIndex - 1, true, true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetIndex(this._currentIndex + 1, true, true);
				}
			}
			if (this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetIndex(this._currentIndex + 1, true, true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetIndex(this._currentIndex - 1, true, true);
				}
			}
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetIndex(this._currentIndex - 1, true, true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetIndex(this._currentIndex + 1, true, true);
				}
			}
			return true;
		}

		public void SetIndex(int index)
		{
			this.SetIndex(index, false, false);
		}

		public void SetIndex(int index, bool animation)
		{
			this.SetIndex(index, animation, false);
		}

		public void SetIndex(int index, bool animation, bool executeAction)
		{
			this._currentIndex = index;
			this._animationFlag = animation;
			this._executeValueChangeActionFlag = executeAction;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetCount(int count)
		{
			this._currentCount = count;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public override void SetEnable(bool enable, AnUIEnableTypes enableType = AnUIEnableTypes.Normal)
		{
			if (!this._exist)
			{
				return;
			}
			base.SetEnable(enable, enableType);
			for (int i = 0; i < this._checkButtonList.Count; i++)
			{
				if (this._checkButtonList[i] != null)
				{
					this._checkButtonList[i].SetEnable(enable, enableType);
				}
			}
		}

		public override void SetParentUI(AnUIBase parentInputUI)
		{
			base.SetParentUI(parentInputUI);
			for (int i = 0; i < this._checkButtonList.Count; i++)
			{
				this._checkButtonList[i].SetParentUI(this);
			}
		}

		protected string _checkButtonObjectPrefixName = "";

		protected List<AnCheckButton> _checkButtonList;

		protected float _objectSpace;

		protected Vector3 _startPosition = Vector3.zero;

		protected Vector3 _endPosition = Vector3.zero;

		protected int _currentCount = 5;

		protected int _prevCount = int.MinValue;

		protected int _maxCount = 100;

		protected int _currentIndex;

		protected int _prevIndex = int.MinValue;

		protected AnCommonStateTypes _valueChangeState;
	}
}
