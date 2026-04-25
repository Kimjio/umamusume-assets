using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnImageNumber : AnUIBase
	{
		public AnImageNumberComponent Component
		{
			get
			{
				return this._component as AnImageNumberComponent;
			}
		}

		public int Value
		{
			get
			{
				return this._value;
			}
		}

		public int CurrentValue
		{
			get
			{
				return this._blendValue.CurrentValue;
			}
		}

		public int Digit
		{
			get
			{
				return this._digit;
			}
		}

		public int MaxDigit
		{
			get
			{
				return this._maxDigit;
			}
		}

		public float BlendTime
		{
			get
			{
				return this._blendTime;
			}
		}

		public AnUIAlignTypes AlignType
		{
			get
			{
				return this._alignType;
			}
		}

		public AnBlendIntValue BlendValue
		{
			get
			{
				return this._blendValue;
			}
		}

		public float InDelayTime
		{
			get
			{
				return this._inDelayTime;
			}
		}

		public float OutStartTime
		{
			get
			{
				return this._outStartTime;
			}
		}

		public float OutDelayTime
		{
			get
			{
				return this._outDelayTime;
			}
		}

		public bool AnimationFromLower
		{
			get
			{
				return this._animationFromLower;
			}
		}

		public AnImageNumber.ImageNumberTypes ImageNumberType
		{
			get
			{
				return this._imageNumberType;
			}
		}

		public AnImageNumber.StartLabelTypes StartLabelType
		{
			get
			{
				return this._startLabelType;
			}
		}

		public bool PlusMinusVisibility
		{
			get
			{
				return this._plusMinusVisibility;
			}
		}

		public List<AnMotion> NumberMotionList
		{
			get
			{
				return this._numberMotionList;
			}
		}

		public Action ActionValueChangeStart { get; set; }

		public Action ActionValueChangeLoop { get; set; }

		public Action ActionValueChangeEnd { get; set; }

		public AnAction FlActionValueChangeStart { get; protected set; }

		public AnAction FlActionValueChangeLoop { get; protected set; }

		public AnAction FlActionValueChangeEnd { get; protected set; }

		public AnImageNumber()
		{
			this._logTitle = "UI ImageNumber";
		}

		public void SetOtherPath(string numberObjectPrefixName)
		{
			AnUtilityString.ReplaceString(numberObjectPrefixName, ref this._numberObjectPrefixName);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			if (AnUtilityString.IsEmptyString(this._numberObjectPrefixName))
			{
				return false;
			}
			if (this._numberMotionList == null)
			{
				this._numberMotionList = new List<AnMotion>();
			}
			if (this._objectListForSpace == null)
			{
				this._objectListForSpace = new List<GameObject>();
			}
			if (this._customFrameFlagList == null)
			{
				this._customFrameFlagList = new List<bool>();
			}
			this._numberMotionList.Clear();
			this._objectListForSpace.Clear();
			this._customFrameFlagList.Clear();
			for (int i = 0; i < 20; i++)
			{
				string text = this._numberObjectPrefixName + i.ToString("D2");
				Transform transform = this._root.FindComponent<Transform>(this._motion.GameObject, text, false);
				if (transform == null)
				{
					break;
				}
				AnMotion anMotion = this._root.Find<AnMotion>(transform.gameObject, "MOT_", false);
				if (anMotion == null)
				{
					break;
				}
				anMotion.SetResetModeType(AnMotion.ResetModeTypes.None);
				anMotion.SetMotionPause(0);
				this._numberMotionList.Add(anMotion);
				this._objectListForSpace.Add(transform.gameObject);
				this._customFrameFlagList.Add(false);
			}
			if (this._numberMotionList.Count == 0)
			{
				return false;
			}
			this._maxDigit = this._maxDigit;
			this._maxDigit = this._objectListForSpace.Count;
			this._CheckImageNumberType();
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this._blendValue = null;
			this._blendValue = new AnBlendIntValue(0, 0, 0.25f, AnBlendBase.BlendTypes.Linear);
			this.FlActionValueChangeStart = base._AddAction();
			this.FlActionValueChangeLoop = base._AddAction();
			this.FlActionValueChangeEnd = base._AddAction();
		}

		private void _CheckImageNumberType()
		{
			this._imageNumberType = AnImageNumber.ImageNumberTypes.FixNumber;
			AnMotion anMotion = this._numberMotionList[0];
			if (anMotion.ObjectList.Count == 0)
			{
				return;
			}
			if (!anMotion.Parameter._ExistLabel(this._imageNumberLabelIn))
			{
				return;
			}
			if (!anMotion.Parameter._ExistLabel(this._imageNumberLabelLoop))
			{
				return;
			}
			if (!anMotion.Parameter._ExistLabel(this._imageNumberLabelOut))
			{
				return;
			}
			this._imageNumberType = AnImageNumber.ImageNumberTypes.AnimationNumber;
			for (int i = 0; i < this._numberMotionList.Count; i++)
			{
				AnMotion childMotion = (this._numberMotionList[i].ObjectList[0] as AnObject).ChildMotion;
				childMotion.SetResetModeType(AnMotion.ResetModeTypes.None);
				childMotion.SetMotionPause(0);
			}
		}

		public override void _Release()
		{
			if (!this._exist)
			{
				return;
			}
			this._numberMotionList = null;
			this._objectListForSpace = null;
			this._customFrameFlagList = null;
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
			if (this._imageNumberType == AnImageNumber.ImageNumberTypes.AnimationNumber)
			{
				for (int i = 0; i < this._numberMotionList.Count; i++)
				{
					AnMotion anMotion = this._numberMotionList[i];
					anMotion.SetVisible(true);
					if (this._startLabelType == AnImageNumber.StartLabelTypes.In)
					{
						anMotion.SetMotionPause(this._imageNumberLabelIn);
					}
					else if (this._startLabelType == AnImageNumber.StartLabelTypes.Loop)
					{
						anMotion.SetMotionPlay(this._imageNumberLabelLoop);
					}
					else if (this._startLabelType == AnImageNumber.StartLabelTypes.Out)
					{
						anMotion.SetMotionPause(this._imageNumberLabelOut);
					}
					anMotion.SetVisible(false);
				}
				this._currentTime = 0f;
			}
			this._prevValue = int.MinValue;
		}

		protected override void _UpdateValueChange()
		{
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
				this._UpdateNumberMotionList((float)this._blendValue.CurrentValue);
				if (this._blendValue.CurrentBlendValue >= 1f)
				{
					this._animationFlag = false;
				}
			}
			else if (this._value != this._prevValue)
			{
				this._UpdateNumberMotionList((float)this._value);
			}
			this._UpdateAlign();
			this._UpdateAnimation();
		}

		private void _UpdateNumberMotionList(float value)
		{
			float absValue = AnUtilityValue.GetAbsValue(value);
			this._currentDigit = AnUtilityValue.GetDigit(absValue);
			AnUtilityValue.LimitValue(ref this._digit, 0, this._maxDigit);
			int num = 1;
			for (int i = 0; i < this._numberMotionList.Count; i++)
			{
				AnMotion anMotion = this._numberMotionList[i];
				AnMotion anMotion2 = anMotion;
				if (this._imageNumberType == AnImageNumber.ImageNumberTypes.AnimationNumber)
				{
					anMotion2 = (this._numberMotionList[i].ObjectList[0] as AnObject).ChildMotion;
				}
				if (this._customFrameFlagList[i])
				{
					num *= 10;
				}
				else
				{
					anMotion.SetVisible(false);
					anMotion2.SetMotionPause(0);
					if (this._digit == 0)
					{
						if (this._currentDigit > this._maxDigit)
						{
							anMotion2.SetMotionPause(9);
							anMotion.SetVisible(true);
						}
						else if (i < this._currentDigit)
						{
							int num2 = (int)absValue / num % 10;
							anMotion2.SetMotionPause(num2);
							anMotion.SetVisible(true);
						}
						if (this._plusMinusVisibility && i == this._currentDigit && value != 0f)
						{
							anMotion.SetVisible(true);
							if (value < 0f)
							{
								anMotion2.SetMotionPause(11);
							}
							else
							{
								anMotion2.SetMotionPause(10);
							}
						}
					}
					else
					{
						if (this._currentDigit > this._digit)
						{
							if (i < this._digit)
							{
								anMotion2.SetMotionPause(9);
								anMotion.SetVisible(true);
							}
						}
						else if (i < this._digit)
						{
							if (i < this._currentDigit)
							{
								int num3 = (int)absValue / num % 10;
								anMotion2.SetMotionPause(num3);
							}
							anMotion.SetVisible(true);
						}
						if (this._plusMinusVisibility && i == this._digit && value != 0f)
						{
							anMotion.SetVisible(true);
							if (value < 0f)
							{
								anMotion2.SetMotionPause(11);
							}
							else
							{
								anMotion2.SetMotionPause(10);
							}
						}
					}
					num *= 10;
				}
			}
		}

		private void _UpdateAnimation()
		{
			if (this._imageNumberType != AnImageNumber.ImageNumberTypes.AnimationNumber)
			{
				return;
			}
			int num = 0;
			int num2 = this._currentDigit;
			if (this._digit > 0)
			{
				num2 = this._digit;
			}
			if (this._plusMinusVisibility)
			{
				num2++;
			}
			for (int i = num; i < num2; i++)
			{
				int num3 = i;
				if (!this._animationFromLower)
				{
					num3 = num2 - 1 - i;
				}
				if (num3 < this._numberMotionList.Count)
				{
					AnMotion anMotion = this._numberMotionList[num3];
					if (anMotion.CurrentLabelName == this._imageNumberLabelIn)
					{
						if (this._inDelayTime >= 0f)
						{
							if (anMotion._currentTime <= 0.0001f && this._currentTime > this._inDelayTime * (float)i)
							{
								anMotion.SetMotionPlay(this._imageNumberLabelIn);
							}
						}
						else
						{
							anMotion.SetMotionPlay(this._imageNumberLabelLoop);
						}
					}
					else if (anMotion.CurrentLabelName == this._imageNumberLabelLoop && this._outDelayTime >= 0f && this._outStartTime >= 0f && this._currentTime >= this._outStartTime && this._currentTime - this._outStartTime > this._outDelayTime * (float)i)
					{
						anMotion.SetMotionPlay(this._imageNumberLabelOut);
					}
				}
			}
		}

		private void _UpdateAlign()
		{
			this._UpdateImageSpace();
			float num = (float)this._currentDigit;
			if (this._digit > 0)
			{
				num = (float)this._digit;
			}
			if (this._plusMinusVisibility && this._value != 0)
			{
				num += 1f;
			}
			if (num > (float)this._maxDigit)
			{
				num = (float)this._maxDigit;
			}
			float num2 = 0f;
			if ((float)this._maxDigit - num > 0f)
			{
				if (this._alignType == AnUIAlignTypes.Right)
				{
					num2 = 0f;
				}
				else if (this._alignType == AnUIAlignTypes.Center)
				{
					num2 = this._imageSpace * ((float)this._maxDigit - num) * 0.5f;
				}
				else if (this._alignType == AnUIAlignTypes.Left)
				{
					num2 = this._imageSpace * ((float)this._maxDigit - num);
				}
			}
			for (int i = 0; i < this._numberMotionList.Count; i++)
			{
				this._numberMotionList[i].GameObject.transform.localPosition = new Vector3(num2, 0f, 0f);
			}
		}

		private void _UpdateImageSpace()
		{
			this._imageSpace *= 1f;
			this._imageSpace = 0f;
			if (this._objectListForSpace.Count > 1)
			{
				this._imageSpace = this._objectListForSpace[1].transform.localPosition.x - this._objectListForSpace[0].transform.localPosition.x;
			}
		}

		protected override void _UpdatePrevValueChange()
		{
			base._UpdatePrevValueChange();
			this._currentTime += AnMonoSingleton<AnRootManager>.Instance._currentDeltaTime;
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

		public void SetValue(int value)
		{
			this.SetValue(value, false, false);
		}

		public void SetValue(float value, bool animation)
		{
			this.SetValue((int)value, animation);
		}

		public void SetValue(int value, bool animation)
		{
			this.SetValue(value, animation, true);
		}

		public void SetValue(int value, bool animation, bool executeAction)
		{
			this._value = value;
			this._animationFlag = animation;
			this._initializeValueChangeFlag = true;
			this._executeValueChangeActionFlag = executeAction;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetInDelayTime(float value)
		{
			this._inDelayTime = value;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetOutStartTime(float value)
		{
			this._outStartTime = value;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetOutDelayTime(float value)
		{
			this._outDelayTime = value;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetAnimationFromLower(bool fromLower)
		{
			this._animationFromLower = fromLower;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetBlendTime(float time)
		{
			this._blendTime = time;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetAlignType(AnUIAlignTypes alignType)
		{
			this._alignType = alignType;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetDigit(int digit)
		{
			this._digit = digit;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetStartLabelType(AnImageNumber.StartLabelTypes startLabelType)
		{
			this._startLabelType = startLabelType;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetPlusMinusVisibility(bool visibility)
		{
			this._plusMinusVisibility = visibility;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetCustomFrame(params int[] targetDigitList)
		{
			if (!this._exist)
			{
				return;
			}
			this.SetDisableCustomFrame();
			if (targetDigitList.Length == 0)
			{
				return;
			}
			if (targetDigitList.Length % 2 != 0)
			{
				return;
			}
			for (int i = 0; i < targetDigitList.Length; i += 2)
			{
				if (targetDigitList[i] < this._customFrameFlagList.Count)
				{
					this._customFrameFlagList[targetDigitList[i]] = true;
					AnMotion anMotion = this._numberMotionList[targetDigitList[i]];
					AnMotion anMotion2 = anMotion;
					if (this._imageNumberType == AnImageNumber.ImageNumberTypes.AnimationNumber)
					{
						anMotion2 = (anMotion.ObjectList[0] as AnObject).ChildMotion;
					}
					anMotion2.SetMotionPause(targetDigitList[i + 1]);
				}
			}
			this._prevValue = int.MaxValue;
		}

		public void SetDisableCustomFrame()
		{
			if (!this._exist)
			{
				return;
			}
			for (int i = 0; i < this._customFrameFlagList.Count; i++)
			{
				this._customFrameFlagList[i] = false;
			}
			this._prevValue = int.MaxValue;
		}

		protected AnImageNumber.ImageNumberTypes _imageNumberType;

		protected string _numberObjectPrefixName = "OBJ_typ_num00_";

		protected List<AnMotion> _numberMotionList;

		protected List<GameObject> _objectListForSpace;

		protected int _value;

		protected float _blendTime;

		protected int _digit;

		protected int _maxDigit;

		protected int _currentDigit;

		protected float _imageSpace;

		protected AnImageNumber.StartLabelTypes _startLabelType;

		protected bool _animationFromLower;

		protected float _inDelayTime = 0.05f;

		protected float _outStartTime = 1f;

		protected float _outDelayTime = 0.05f;

		protected float _currentTime;

		protected bool _plusMinusVisibility;

		protected AnUIAlignTypes _alignType;

		protected AnBlendIntValue _blendValue;

		protected int _prevValue = int.MinValue;

		protected List<bool> _customFrameFlagList;

		protected string _imageNumberLabelIn = "In";

		protected string _imageNumberLabelLoop = "Loop";

		protected string _imageNumberLabelOut = "Out";

		public enum ImageNumberTypes
		{
			FixNumber,
			AnimationNumber
		}

		public enum StartLabelTypes
		{
			In,
			Loop,
			Out
		}
	}
}
