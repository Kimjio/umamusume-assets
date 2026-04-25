using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnUIBase
	{
		public AnComponentBase ComponentBase
		{
			get
			{
				return this._component;
			}
			set
			{
				this._component = value;
			}
		}

		public AnRoot Root
		{
			get
			{
				return this._root;
			}
		}

		public GameObject RootObject
		{
			get
			{
				return this._rootObject;
			}
		}

		public AnMotion Motion
		{
			get
			{
				return this._motion;
			}
		}

		public AnObjectBase HitAreaObject
		{
			get
			{
				return this._hitAreaObject;
			}
		}

		public Collider Collider
		{
			get
			{
				if (this._hitAreaObject == null)
				{
					return null;
				}
				return this._hitAreaObject.Collider;
			}
		}

		[Obsolete("Use Collider")]
		public Collider Collision
		{
			get
			{
				return this.Collider;
			}
		}

		public bool Exist
		{
			get
			{
				return this._exist;
			}
		}

		public bool Enable
		{
			get
			{
				return this._enable;
			}
		}

		public AnUIEnableTypes EnableType
		{
			get
			{
				return this._enableType;
			}
		}

		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		public bool ParentEnable
		{
			get
			{
				return this._parentEnable;
			}
		}

		public AnUIBase.FlUIBaseStateTypes CurrentBaseState
		{
			get
			{
				return this._currentBaseState;
			}
		}

		public AnInputTypes CurrentInputType
		{
			get
			{
				return this._currentInputType;
			}
		}

		public List<string> CustomSubmitButtonNameList
		{
			get
			{
				return this._customSubmitButtonNameList;
			}
		}

		public List<string> CustomCancelButtonNameList
		{
			get
			{
				return this._customCancelButtonNameList;
			}
		}

		public float CustomSubmitDelayTimeForRayInput
		{
			get
			{
				return this._customSubmitDelayTimeForRayInput;
			}
		}

		public bool EnableSubmitDelayTimeForRayInput
		{
			get
			{
				return this._enableSubmitDelayTimeForRayInput;
			}
		}

		public bool EnableDownLoopForTouchInput
		{
			get
			{
				return this._enableDownLoopForTouchInput;
			}
		}

		public bool EnableDownLoopForKeyInput
		{
			get
			{
				return this._enableDownLoopForKeyInput;
			}
		}

		public bool EnableDownLoopForRayInput
		{
			get
			{
				return this._enableDownLoopForRayInput;
			}
		}

		public AnUIBase ParentUI
		{
			get
			{
				return this._parentUI;
			}
		}

		public bool EnableSelectInputForTouchInput
		{
			get
			{
				return this._enableSelectInputForTouchInput;
			}
		}

		public bool EnableSelectInputForKeyInput
		{
			get
			{
				return this._enableSelectInputForKeyInput;
			}
		}

		public bool EnableSelectInputForRayInput
		{
			get
			{
				return this._enableSelectInputForRayInput;
			}
		}

		public bool EnableDownInputForTouchInput
		{
			get
			{
				return this._enableDownInputForTouchInput;
			}
		}

		public bool EnableDownInputForKeyInput
		{
			get
			{
				return this._enableDownInputForKeyInput;
			}
		}

		public bool EnableDownInputForRayInput
		{
			get
			{
				return this._enableDownInputForRayInput;
			}
		}

		public bool EnableOverInputForMouseInput
		{
			get
			{
				return this._enableOverInputForMouse;
			}
		}

		public bool EnableContinuousInputForTouchInput
		{
			get
			{
				return this._enableContinuousInputForTouchInput;
			}
		}

		public bool EnableContinuousInputForKeyInput
		{
			get
			{
				return this._enableContinuousInputForKeyInput;
			}
		}

		public bool EnableContinuousInputForRayInput
		{
			get
			{
				return this._enableContinuousInputForRayInput;
			}
		}

		public AnTouchInput CurrentTouchInput
		{
			get
			{
				return this._currentInput as AnTouchInput;
			}
		}

		public AnKeyInput CurrentKeyInput
		{
			get
			{
				return this._currentInput as AnKeyInput;
			}
		}

		public AnRayInput CurrentRayInput
		{
			get
			{
				return this._currentInput as AnRayInput;
			}
		}

		public bool IsSwiping
		{
			get
			{
				return this._isSwiping;
			}
		}

		public AnUIDirectionTypes SwipeDirectionType
		{
			get
			{
				return this._swipeDirectionType;
			}
		}

		public AnUIDirectionTypes DirectionType
		{
			get
			{
				return this._directionType;
			}
		}

		public bool EnableDownLoopSelection
		{
			get
			{
				return this._enableDownLoopSelection;
			}
		}

		public Action ActionCommonStart { get; set; }

		public Action ActionCommonEnd { get; set; }

		public Action ActionLoop { get; set; }

		public Action ActionDownInStart { get; set; }

		public Action ActionDownInLoop { get; set; }

		public Action ActionDownInEnd { get; set; }

		public Action ActionDownLoop { get; set; }

		public Action ActionDownLoopOn { get; set; }

		public Action ActionDownLoopOff { get; set; }

		public Action ActionLongDownLoopStart { get; set; }

		public Action ActionLongDownLoop { get; set; }

		public Action ActionDownOutStart { get; set; }

		public Action ActionDownOutStartOn { get; set; }

		public Action ActionDownOutStartOff { get; set; }

		public Action ActionDownOutLoop { get; set; }

		public Action ActionDownOutEnd { get; set; }

		public Action ActionDownOutEndOn { get; set; }

		public Action ActionDownOutEndOff { get; set; }

		public Action ActionSelectInStart { get; set; }

		public Action ActionSelectInLoop { get; set; }

		public Action ActionSelectInEnd { get; set; }

		public Action ActionSelectLoop { get; set; }

		public Action ActionSelectOutStart { get; set; }

		public Action ActionSelectOutLoop { get; set; }

		public Action ActionSelectOutEnd { get; set; }

		public Action ActionOverInStart { get; set; }

		public Action ActionOverInLoop { get; set; }

		public Action ActionOverInEnd { get; set; }

		public Action ActionOverLoop { get; set; }

		public Action ActionOverOutStart { get; set; }

		public Action ActionOverOutLoop { get; set; }

		public Action ActionOverOutEnd { get; set; }

		public Action ActionSwipeStart { get; set; }

		public Action ActionSwipeLoop { get; set; }

		public AnAction FlActionCommonStart { get; protected set; }

		public AnAction FlActionCommonEnd { get; protected set; }

		public AnAction FlActionLoop { get; protected set; }

		public AnAction FlActionDownInStart { get; protected set; }

		public AnAction FlActionDownInLoop { get; protected set; }

		public AnAction FlActionDownInEnd { get; protected set; }

		public AnAction FlActionDownLoop { get; protected set; }

		public AnAction FlActionDownLoopOn { get; protected set; }

		public AnAction FlActionDownLoopOff { get; protected set; }

		public AnAction FlActionLongDownLoopStart { get; protected set; }

		public AnAction FlActionLongDownLoop { get; protected set; }

		public AnAction FlActionDownOutStart { get; protected set; }

		public AnAction FlActionDownOutStartOn { get; protected set; }

		public AnAction FlActionDownOutStartOff { get; protected set; }

		public AnAction FlActionDownOutLoop { get; protected set; }

		public AnAction FlActionDownOutEnd { get; protected set; }

		public AnAction FlActionDownOutEndOn { get; protected set; }

		public AnAction FlActionDownOutEndOff { get; protected set; }

		public AnAction FlActionSelectInStart { get; protected set; }

		public AnAction FlActionSelectInLoop { get; protected set; }

		public AnAction FlActionSelectInEnd { get; protected set; }

		public AnAction FlActionSelectLoop { get; protected set; }

		public AnAction FlActionSelectOutStart { get; protected set; }

		public AnAction FlActionSelectOutLoop { get; protected set; }

		public AnAction FlActionSelectOutEnd { get; protected set; }

		public AnAction FlActionOverInStart { get; protected set; }

		public AnAction FlActionOverInLoop { get; protected set; }

		public AnAction FlActionOverInEnd { get; protected set; }

		public AnAction FlActionOverLoop { get; protected set; }

		public AnAction FlActionOverOutStart { get; protected set; }

		public AnAction FlActionOverOutLoop { get; protected set; }

		public AnAction FlActionOverOutEnd { get; protected set; }

		public AnAction FlActionSwipeStart { get; protected set; }

		public AnAction FlActionSwipeLoop { get; protected set; }

		public AnUIBase()
		{
			this._actionList = new List<AnAction>();
			this._uiLabelNameTable = new Hashtable();
			this._customSubmitButtonNameList = new List<string>();
			this._customCancelButtonNameList = new List<string>();
			this._nextInputUIList = new List<AnUIBase>();
			this._nextInputUIExistList = new List<bool>();
		}

		public virtual void SetBasePath(AnRoot root, GameObject rootObject, string motionPath)
		{
			this._root = root;
			this._rootObject = rootObject;
			AnUtilityString.ReplaceString(motionPath, ref this._motionPath);
		}

		public virtual void SetHitAreaObject(string hitAreaObjectPath)
		{
			AnUtilityString.ReplaceString(hitAreaObjectPath, ref this._hitAreaObjectPath);
		}

		public virtual void Initialize()
		{
			this._InitializeLogColor();
			this._exist = false;
			if (!this._InitializeData())
			{
				return;
			}
			if (!this._InitializeThisData())
			{
				return;
			}
			this._InitializeThisData_PostProcess();
			AnMonoSingleton<AnRootManager>.Instance.UIManager.UIBaseManager._AddObject(this);
			this._exist = true;
			this._UpdateForce();
			this._firstUpdateFlag = false;
		}

		protected virtual bool _InitializeData()
		{
			if (this._root == null)
			{
				return false;
			}
			if (this._rootObject == null)
			{
				return false;
			}
			if (AnUtilityString.IsEmptyString(this._motionPath))
			{
				return false;
			}
			this._motion = this._root.Find<AnMotion>(this._rootObject, this._motionPath, false);
			if (this._motion == null)
			{
				return false;
			}
			this._motionObject = this._motion.GameObject;
			this._motion.SetResetModeType(AnMotion.ResetModeTypes.None);
			this._hitAreaObject = this._root.Find<AnObjectBase>(this._rootObject, this._hitAreaObjectPath, false);
			this._InitializeInput();
			this._InitializeActionList();
			this._InitializeUILabelNameTable();
			return true;
		}

		protected virtual bool _InitializeThisData()
		{
			return true;
		}

		protected virtual void _InitializeThisData_PostProcess()
		{
		}

		protected virtual void _InitializeInput()
		{
			this._customSubmitButtonNameList.Clear();
			this._customCancelButtonNameList.Clear();
			this._nextInputUIList.Clear();
			this._nextInputUIExistList.Clear();
			for (int i = 0; i < 8; i++)
			{
				this._nextInputUIList.Add(null);
				this._nextInputUIExistList.Add(false);
			}
		}

		protected virtual void _InitializeActionList()
		{
			this.FlActionCommonStart = this._AddAction();
			this.FlActionCommonEnd = this._AddAction();
			this.FlActionLoop = this._AddAction();
			this.FlActionDownInStart = this._AddAction();
			this.FlActionDownInLoop = this._AddAction();
			this.FlActionDownInEnd = this._AddAction();
			this.FlActionDownLoop = this._AddAction();
			this.FlActionDownLoopOn = this._AddAction();
			this.FlActionDownLoopOff = this._AddAction();
			this.FlActionLongDownLoopStart = this._AddAction();
			this.FlActionLongDownLoop = this._AddAction();
			this.FlActionDownOutStart = this._AddAction();
			this.FlActionDownOutStartOn = this._AddAction();
			this.FlActionDownOutStartOff = this._AddAction();
			this.FlActionDownOutLoop = this._AddAction();
			this.FlActionDownOutEnd = this._AddAction();
			this.FlActionDownOutEndOn = this._AddAction();
			this.FlActionDownOutEndOff = this._AddAction();
			this.FlActionSelectInStart = this._AddAction();
			this.FlActionSelectInLoop = this._AddAction();
			this.FlActionSelectInEnd = this._AddAction();
			this.FlActionSelectLoop = this._AddAction();
			this.FlActionSelectOutStart = this._AddAction();
			this.FlActionSelectOutLoop = this._AddAction();
			this.FlActionSelectOutEnd = this._AddAction();
			this.FlActionOverInStart = this._AddAction();
			this.FlActionOverInLoop = this._AddAction();
			this.FlActionOverInEnd = this._AddAction();
			this.FlActionOverLoop = this._AddAction();
			this.FlActionOverOutStart = this._AddAction();
			this.FlActionOverOutLoop = this._AddAction();
			this.FlActionOverOutEnd = this._AddAction();
			this.FlActionSwipeStart = this._AddAction();
			this.FlActionSwipeLoop = this._AddAction();
		}

		protected virtual void _InitializeLogColor()
		{
		}

		protected virtual void _InitializeUILabelNameTable()
		{
			if (this._uiLabelNameTable == null)
			{
				this._uiLabelNameTable = new Hashtable();
			}
			this._uiLabelNameTable.Clear();
			this._uiLabelNameTable.Add(this._labelLoop, this._labelLoop);
			this._uiLabelNameTable.Add(this._labelDisable, this._labelDisable);
			this._uiLabelNameTable.Add(this._labelDownIn, this._labelDownIn);
			this._uiLabelNameTable.Add(this._labelDownLoop, this._labelDownLoop);
			this._uiLabelNameTable.Add(this._labelDownOut, this._labelDownOut);
			this._uiLabelNameTable.Add(this._labelDownOutOn, this._labelDownOutOn);
			this._uiLabelNameTable.Add(this._labelSelectIn, this._labelSelectIn);
			this._uiLabelNameTable.Add(this._labelSelectLoop, this._labelSelectLoop);
			this._uiLabelNameTable.Add(this._labelSelectOut, this._labelSelectOut);
			this._uiLabelNameTable.Add(this._labelOverIn, this._labelOverIn);
			this._uiLabelNameTable.Add(this._labelOverLoop, this._labelOverLoop);
			this._uiLabelNameTable.Add(this._labelOverOut, this._labelOverOut);
		}

		protected virtual bool _PlayMotion(string labelName, bool force)
		{
			return this._PlayMotionBase(labelName, null, null, force);
		}

		protected virtual bool _PlayMotionBase(string labelName, string secondLabelName, string thirdLabelName, bool force)
		{
			if (!this._exist)
			{
				return false;
			}
			if (!force && !this._IsUILabelName())
			{
				return false;
			}
			string text = labelName;
			if (!this._motion.Parameter._ExistLabel(labelName))
			{
				if (secondLabelName == null)
				{
					return false;
				}
				if (!this._motion.Parameter._ExistLabel(secondLabelName))
				{
					if (thirdLabelName == null)
					{
						return false;
					}
					if (!this._motion.Parameter._ExistLabel(thirdLabelName))
					{
						return false;
					}
					text = thirdLabelName;
				}
				else
				{
					text = secondLabelName;
				}
			}
			if (this._motion.CurrentLabelName == text)
			{
				return false;
			}
			this._motion.SetMotionPlay(text);
			return true;
		}

		protected virtual bool _IsUILabelName()
		{
			return this._uiLabelNameTable != null && this._uiLabelNameTable.ContainsKey(this._motion.CurrentLabelName);
		}

		protected virtual void _ResetMotion(bool force)
		{
			if (!this._exist)
			{
				return;
			}
			if (this._motion.CurrentLabelName == this._labelDownIn || this._motion.CurrentLabelName == this._labelDownLoop)
			{
				this._motion.SetMotionPlay(this._labelDownOut);
				return;
			}
			if (this._motion.CurrentLabelName == this._labelSelectIn || this._motion.CurrentLabelName == this._labelSelectLoop)
			{
				this._motion.SetMotionPlay(this._labelSelectOut);
				return;
			}
			if (this._motion.CurrentLabelName == this._labelOverIn || this._motion.CurrentLabelName == this._labelOverLoop)
			{
				this._motion.SetMotionPlay(this._labelOverOut);
				return;
			}
			this._PlayMotion(this._labelLoop, force);
		}

		protected virtual void _OnActive()
		{
			this._Reset();
		}

		protected virtual void _OnDeactive()
		{
			this._Reset();
		}

		protected virtual void _Reset()
		{
			this._ResetPrevValue();
			this._Update_Loop_Init();
		}

		protected virtual void _ResetPrevValue()
		{
		}

		public virtual void _Release()
		{
			if (!this._exist)
			{
				return;
			}
			this._ReleaseAction();
			if (AnMonoSingleton<AnRootManager>.Instance != null)
			{
				AnMonoSingleton<AnRootManager>.Instance.UIManager.UIBaseManager._RemoveObject(this);
			}
			this._exist = false;
		}

		public virtual void _UpdateInitialize()
		{
			if (!this._exist)
			{
				return;
			}
			this._updateFlag = true;
			this._currentUpdateDepth = 0;
		}

		public virtual void _UpdateFirst()
		{
			if (!this._exist)
			{
				return;
			}
			this._ForceUpdateStart();
			if (this._isActive != this._prevIsActive)
			{
				if (this._isActive)
				{
					this._OnActive();
				}
				else
				{
					this._OnDeactive();
				}
			}
			if (this._firstUpdateFlag)
			{
				this._OnActive();
			}
			this._UpdateEnable();
			this._UpdateCollider();
		}

		public virtual void _UpdateSecond()
		{
			if (!this._exist)
			{
				return;
			}
			if (this._isActive)
			{
				this._Update(this._updateFlag);
			}
			this._ForceUpdateEnd();
		}

		public virtual void _UpdateForce()
		{
			if (!this._exist)
			{
				return;
			}
			this._updateFlag = false;
			this._currentUpdateDepth++;
			this._ResetPrevValue();
			this._UpdateFirst();
			this._UpdateSecond();
		}

		protected virtual void _ForceUpdateStart()
		{
			this._isActive = true;
			if (!this._exist)
			{
				this._isActive = false;
				return;
			}
			if (this._motion == null)
			{
				this._isActive = false;
				return;
			}
			if (this._motion.GameObject == null)
			{
				this._isActive = false;
				return;
			}
			if (!this._motion.GameObject.activeInHierarchy)
			{
				this._isActive = false;
				return;
			}
			if (!this._motion._visibleInHierarchy)
			{
				this._isActive = false;
				return;
			}
			if (!this._motion._visibleByAlpha)
			{
				this._isActive = false;
				return;
			}
			if (!this._enable)
			{
				this._isActive = false;
				return;
			}
			if (!this._parentEnable)
			{
				this._isActive = false;
				return;
			}
		}

		protected virtual void _ForceUpdateEnd()
		{
			this._prevIsActive = this._isActive;
		}

		protected virtual void _Update_Common_Start()
		{
			this._ExecuteAction(this.ActionCommonStart, this.FlActionCommonStart);
			if (this._IsInputDown())
			{
				this._Update_DownIn_Init();
			}
			else if (this._IsInputDownLoop())
			{
				this._Update_DownIn_Init();
			}
			else if (this._IsInputOver())
			{
				this._Update_OverIn_Init();
			}
			else if (this._IsInputSelect())
			{
				this._Update_SelectIn_Init();
			}
			switch (this._currentBaseState)
			{
			case AnUIBase.FlUIBaseStateTypes.Loop_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.Loop_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.Loop_Loop:
			case AnUIBase.FlUIBaseStateTypes.DownIn_Loop:
			case AnUIBase.FlUIBaseStateTypes.DownLoop_Loop:
			case AnUIBase.FlUIBaseStateTypes.DownOut_Loop:
			case AnUIBase.FlUIBaseStateTypes.SelectIn_Loop:
			case AnUIBase.FlUIBaseStateTypes.SelectLoop_Loop:
			case AnUIBase.FlUIBaseStateTypes.SelectOut_Loop:
			case AnUIBase.FlUIBaseStateTypes.OverIn_Loop:
			case AnUIBase.FlUIBaseStateTypes.OverLoop_Loop:
				break;
			case AnUIBase.FlUIBaseStateTypes.DownIn_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownIn_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.DownLoop_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownLoop_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.DownOut_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownOut_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.SelectIn_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectIn_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.SelectLoop_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectLoop_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.SelectOut_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectOut_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.OverIn_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverIn_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.OverLoop_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverLoop_Loop;
				return;
			case AnUIBase.FlUIBaseStateTypes.OverOut_Init:
				this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverOut_Loop;
				break;
			default:
				return;
			}
		}

		protected virtual void _Update(bool update = true)
		{
			this._Update_Common_Start();
			switch (this._currentBaseState)
			{
			case AnUIBase.FlUIBaseStateTypes.Loop_Loop:
				this._Update_Loop_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.DownIn_Loop:
				this._Update_DownIn_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.DownLoop_Loop:
				this._Update_DownLoop_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.DownOut_Loop:
				this._Update_DownOut_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.SelectIn_Loop:
				this._Update_SelectIn_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.SelectLoop_Loop:
				this._Update_SelectLoop_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.SelectOut_Loop:
				this._Update_SelectOut_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.OverIn_Loop:
				this._Update_OverIn_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.OverLoop_Loop:
				this._Update_OverLoop_Loop();
				break;
			case AnUIBase.FlUIBaseStateTypes.OverOut_Loop:
				this._Update_OverOut_Loop();
				break;
			}
			this._Update_Common_End();
		}

		protected virtual void _Update_Common_End()
		{
			this._CheckInitializeValueChange();
			this._CheckUpdateValueChange();
			this._CheckValueChangeState();
			this._CheckUpdatePrevValueChange();
			if (this._isSwiping)
			{
				this._UpdateSwipeValue();
				this._ExecuteAction(this.ActionSwipeLoop, this.FlActionSwipeLoop);
			}
			this._ExecuteAction(this.ActionCommonEnd, this.FlActionCommonEnd);
		}

		protected virtual void _Update_Loop_Init()
		{
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.Loop_Init;
			this._currentInput = null;
			this._currentInputType = AnInputTypes.None;
			this._isDownInToDownOut = false;
			this._forceDownIn = false;
			this._forceDownLoop = false;
			this._isLongDownLoopStart = false;
			this._currentLongDownLoopTime = 0f;
			this._isSwiping = false;
			this._isSwipeStart = false;
			this._ResetMotion(false);
		}

		protected virtual void _Update_Loop_Loop()
		{
			this._ExecuteAction(this.ActionLoop, this.FlActionLoop);
			this._PlayMotion(this._labelLoop, false);
		}

		protected virtual void _Update_DownIn_Init()
		{
			this._SetLog(AnLogTypes.___________________________DOWN);
			this._SetLog(AnLogTypes.DownInStart);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownIn_Init;
			this._ExecuteAction(this.ActionDownInStart, this.FlActionDownInStart);
			this._PlayMotion(this._labelDownIn, true);
			this._isSwipeStart = true;
			this._isSwiping = false;
		}

		protected virtual void _Update_DownIn_Loop()
		{
			this._ExecuteAction(this.ActionDownInLoop, this.FlActionDownInLoop);
			if (!this._isSwiping && this._IsInputSwipeStart(this._swipeDirectionType) && this._isSwipeStart)
			{
				this._ExecuteAction(this.ActionSwipeStart, this.FlActionSwipeStart);
				this._isSwipeStart = false;
				this._isSwiping = true;
			}
			this._inputUpType = this._GetInputUpType();
			if (this._forceDownLoop)
			{
				this._Update_DownLoop_Init();
				return;
			}
			if (this._inputUpType != AnInputUpTypes.NotUp)
			{
				this._Update_DownOut_Init();
				return;
			}
			if (!this._motion.Parameter._ExistLabel(this._labelDownIn))
			{
				this._Update_DownLoop_Init();
				return;
			}
			if (this._motion.CurrentLabelName != this._labelDownIn)
			{
				this._Update_DownLoop_Init();
				return;
			}
		}

		protected virtual void _Update_DownLoop_Init()
		{
			this._SetLog(AnLogTypes.DownInEnd);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownLoop_Init;
			this._downLoopOnFlag = true;
			this._isLongDownLoopStart = true;
			this._ExecuteAction(this.ActionDownInEnd, this.FlActionDownInEnd);
			this._PlayMotion(this._labelDownLoop, true);
		}

		protected virtual void _Update_DownLoop_Loop()
		{
			this._ExecuteAction(this.ActionDownLoop, this.FlActionDownLoop);
			if (!this._isSwiping && this._IsInputSwipeStart(this._swipeDirectionType) && this._isSwipeStart)
			{
				this._ExecuteAction(this.ActionSwipeStart, this.FlActionSwipeStart);
				this._isSwipeStart = false;
				this._isSwiping = true;
			}
			AnInputDownLoopTypes anInputDownLoopTypes = this._GetInputDownLoopType();
			if (anInputDownLoopTypes == AnInputDownLoopTypes.DownLoopInRange)
			{
				if (this._downLoopOnFlag)
				{
					this._ExecuteAction(this.ActionDownLoopOn, this.FlActionDownLoopOn);
					this._downLoopOnFlag = false;
				}
				if (this._motion.CurrentLabelName != this._labelDownIn && this._motion.CurrentLabelName != this._labelDownLoop)
				{
					this._PlayMotion(this._labelDownIn, true);
				}
				if (this._currentLongDownLoopTime > this._longDownLoopTime)
				{
					if (this._isLongDownLoopStart)
					{
						this._ExecuteAction(this.ActionLongDownLoopStart, this.FlActionLongDownLoopStart);
						this._isLongDownLoopStart = false;
					}
					this._ExecuteAction(this.ActionLongDownLoop, this.FlActionLongDownLoop);
				}
				this._currentLongDownLoopTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
			}
			else if (anInputDownLoopTypes == AnInputDownLoopTypes.DownLoopOutRange)
			{
				if (!this._downLoopOnFlag)
				{
					this._ExecuteAction(this.ActionDownLoopOff, this.FlActionDownLoopOff);
					this._downLoopOnFlag = true;
				}
				if (this._currentLongDownLoopTime <= this._longDownLoopTime)
				{
					this._isLongDownLoopStart = true;
					this._currentLongDownLoopTime = 0f;
				}
				if (this._isLoopMotionInDownLoop && this._motion.CurrentLabelName != this._labelDownOut && this._motion.CurrentLabelName != this._labelLoop)
				{
					this._PlayMotion(this._labelDownOut, true);
				}
			}
			this._inputUpType = this._GetInputUpType();
			if (this._isDownInToDownOut)
			{
				this._inputUpType = AnInputUpTypes.UpInRange;
			}
			if (this._inputUpType != AnInputUpTypes.NotUp)
			{
				this._Update_DownOut_Init();
			}
		}

		protected virtual void _Update_DownOut_Init()
		{
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.DownOut_Init;
			this._currentInputType = AnInputTypes.None;
			this._isSwiping = false;
			if (this._inputUpType == AnInputUpTypes.UpInRange)
			{
				this._ExecuteAction(this.ActionDownOutStartOn, this.FlActionDownOutStartOn);
				this._SetLog(AnLogTypes.DownOutStartOn);
			}
			else
			{
				this._ExecuteAction(this.ActionDownOutStartOff, this.FlActionDownOutStartOff);
				this._SetLog(AnLogTypes.DownOutStartOff);
			}
			this._ExecuteAction(this.ActionDownOutStart, this.FlActionDownOutStart);
			this._SetLog(AnLogTypes.DownOutStart);
			if (this._motion.CurrentLabelName != this._labelLoop)
			{
				if (this._inputUpType == AnInputUpTypes.UpInRange)
				{
					if (this._motion.Parameter._ExistLabel(this._labelDownOutOn))
					{
						this._PlayMotion(this._labelDownOutOn, true);
					}
					else
					{
						this._PlayMotion(this._labelDownOut, true);
					}
				}
				else
				{
					this._PlayMotion(this._labelDownOut, true);
				}
			}
			if (this._enableSelectInputForTouchInput && this.CurrentTouchInput != null)
			{
				AnMonoSingleton<AnRootManager>.Instance.UIManager.SetCurrentInputUI(this, this.CurrentTouchInput.InputIndex);
			}
		}

		protected virtual void _Update_DownOut_Loop()
		{
			this._ExecuteAction(this.ActionDownOutLoop, this.FlActionDownOutLoop);
			if (this._motion.Parameter._ExistLabel(this._labelDownOutOn))
			{
				if (this._motion.CurrentLabelName != this._labelDownOutOn)
				{
					this._Update_DownOut_End();
					return;
				}
			}
			else
			{
				if (!this._motion.Parameter._ExistLabel(this._labelDownOut))
				{
					this._Update_DownOut_End();
					return;
				}
				if (this._motion.CurrentLabelName != this._labelDownOut)
				{
					this._Update_DownOut_End();
					return;
				}
			}
		}

		protected virtual void _Update_DownOut_End()
		{
			if (this._inputUpType == AnInputUpTypes.UpInRange)
			{
				this._ExecuteAction(this.ActionDownOutEndOn, this.FlActionDownOutEndOn);
				this._SetLog(AnLogTypes.DownOutEndOn);
			}
			else
			{
				this._ExecuteAction(this.ActionDownOutEndOff, this.FlActionDownOutEndOff);
				this._SetLog(AnLogTypes.DownOutEndOff);
			}
			this._ExecuteAction(this.ActionDownOutEnd, this.FlActionDownOutEnd);
			this._SetLog(AnLogTypes.DownOutEnd);
			this._Update_Loop_Init();
		}

		protected virtual void _Update_SelectIn_Init()
		{
			this._SetLog(AnLogTypes._________________________SELECT);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectIn_Init;
			this._ExecuteAction(this.ActionSelectInStart, this.FlActionSelectInStart);
			this._SetLog(AnLogTypes.SelectInStart);
			this._PlayMotion(this._labelSelectIn, true);
		}

		protected virtual void _Update_SelectIn_Loop()
		{
			this._ExecuteAction(this.ActionSelectInLoop, this.FlActionSelectInLoop);
			if (this._forceSelectLoop)
			{
				this._Update_SelectLoop_Init();
				return;
			}
			if (this._motion.Parameter._ExistLabel(this._labelSelectIn))
			{
				if (this._motion.CurrentLabelName != this._labelSelectIn)
				{
					this._Update_SelectLoop_Init();
					return;
				}
			}
			else
			{
				this._Update_SelectLoop_Init();
			}
		}

		protected virtual void _Update_SelectLoop_Init()
		{
			this._ExecuteAction(this.ActionSelectInEnd, this.FlActionSelectInEnd);
			this._SetLog(AnLogTypes.SelectInEnd);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectLoop_Init;
			this._PlayMotion(this._labelSelectLoop, true);
		}

		protected virtual void _Update_SelectLoop_Loop()
		{
			this._ExecuteAction(this.ActionSelectLoop, this.FlActionSelectLoop);
			this._PlayMotion(this._labelSelectLoop, true);
		}

		protected virtual void _Update_SelectOut_Init()
		{
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.SelectOut_Init;
			this._ExecuteAction(this.ActionSelectOutStart, this.FlActionSelectOutStart);
			this._SetLog(AnLogTypes.SelectOutStart);
			this._PlayMotion(this._labelSelectOut, true);
		}

		protected virtual void _Update_SelectOut_Loop()
		{
			this._ExecuteAction(this.ActionSelectOutLoop, this.FlActionSelectOutLoop);
			if (this._motion.Parameter._ExistLabel(this._labelSelectOut))
			{
				if (this._motion.CurrentLabelName != this._labelSelectOut)
				{
					this._Update_SelectOut_End();
					return;
				}
			}
			else
			{
				this._Update_SelectOut_End();
			}
		}

		protected virtual void _Update_SelectOut_End()
		{
			this._ExecuteAction(this.ActionSelectOutEnd, this.FlActionSelectOutEnd);
			this._SetLog(AnLogTypes.SelectOutEnd);
			this._Update_Loop_Init();
		}

		protected virtual void _Update_OverIn_Init()
		{
			this._SetLog(AnLogTypes.___________________________OVER);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverIn_Init;
			this._ExecuteAction(this.ActionOverInStart, this.FlActionOverInStart);
			this._SetLog(AnLogTypes.OverInStart);
			this._PlayMotion(this._labelOverIn, true);
		}

		protected virtual void _Update_OverIn_Loop()
		{
			this._ExecuteAction(this.ActionOverInLoop, this.FlActionOverInLoop);
			if (this._forceOverLoop)
			{
				this._Update_OverLoop_Init();
				return;
			}
			if (this._motion.Parameter._ExistLabel(this._labelOverIn))
			{
				if (this._motion.CurrentLabelName != this._labelOverIn)
				{
					this._Update_OverLoop_Init();
					return;
				}
			}
			else
			{
				this._Update_OverLoop_Init();
			}
		}

		protected virtual void _Update_OverLoop_Init()
		{
			this._ExecuteAction(this.ActionOverInEnd, this.FlActionOverInEnd);
			this._SetLog(AnLogTypes.OverInEnd);
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverLoop_Init;
			this._PlayMotion(this._labelOverLoop, true);
		}

		protected virtual void _Update_OverLoop_Loop()
		{
			this._ExecuteAction(this.ActionOverLoop, this.FlActionOverLoop);
			this._PlayMotion(this._labelOverLoop, true);
		}

		protected virtual void _Update_OverOut_Init()
		{
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.OverOut_Init;
			this._ExecuteAction(this.ActionOverOutStart, this.FlActionOverOutStart);
			this._SetLog(AnLogTypes.OverOutStart);
			this._PlayMotion(this._labelOverOut, true);
		}

		protected virtual void _Update_OverOut_Loop()
		{
			this._ExecuteAction(this.ActionOverOutLoop, this.FlActionOverOutLoop);
			if (this._motion.Parameter._ExistLabel(this._labelOverOut))
			{
				if (this._motion.CurrentLabelName != this._labelOverOut)
				{
					this._Update_OverOut_End();
					return;
				}
			}
			else
			{
				this._Update_OverOut_End();
			}
		}

		protected virtual void _Update_OverOut_End()
		{
			this._ExecuteAction(this.ActionOverOutEnd, this.FlActionOverOutEnd);
			this._SetLog(AnLogTypes.OverOutEnd);
			this._Update_Loop_Init();
		}

		protected virtual void _CheckInitializeValueChange()
		{
			if (!this._initializeValueChangeFlag)
			{
				return;
			}
			this._InitializeValueChange();
			this._initializeValueChangeFlag = false;
		}

		protected virtual void _InitializeValueChange()
		{
		}

		protected virtual void _CheckUpdateValueChange()
		{
			this._UpdateValueChange();
		}

		protected virtual void _UpdateValueChange()
		{
		}

		protected virtual void _CheckUpdatePrevValueChange()
		{
			this._UpdatePrevValueChange();
		}

		protected virtual void _UpdatePrevValueChange()
		{
		}

		protected virtual void _CheckValueChangeState()
		{
			if (this._firstUpdateFlag)
			{
				this._executeValueChangeActionFlag = false;
				this._currentValueCnageState = AnCommonStateTypes.None;
				return;
			}
			if (this._executeValueChangeActionFlag)
			{
				this._currentValueCnageState = AnCommonStateTypes.Start;
				this._executeValueChangeActionFlag = false;
			}
			this._UpdateValueChangeState();
		}

		protected virtual void _UpdateValueChangeState()
		{
			AnCommonStateTypes currentValueCnageState = this._currentValueCnageState;
			if (currentValueCnageState != AnCommonStateTypes.Start)
			{
				if (currentValueCnageState == AnCommonStateTypes.End)
				{
					this._UpdateValueChangeEnd();
				}
			}
			else
			{
				this._UpdateValueChangeStart();
			}
			if (this._currentValueCnageState == AnCommonStateTypes.Loop)
			{
				this._UpdateValueChangeLoop();
			}
		}

		protected virtual void _UpdateValueChangeStart()
		{
			this._currentValueCnageState = AnCommonStateTypes.Loop;
		}

		protected virtual void _UpdateValueChangeLoop()
		{
		}

		protected virtual void _UpdateValueChangeEnd()
		{
			this._currentValueCnageState = AnCommonStateTypes.None;
		}

		protected void _UpdateCollider()
		{
			this._isHiAreaActive = false;
			if (!this._exist)
			{
				return;
			}
			if (this._hitAreaObject == null)
			{
				return;
			}
			if (this._hitAreaObject.Collider == null)
			{
				return;
			}
			if (this._isActive)
			{
				if (!this._hitAreaObject.Collider.enabled)
				{
					this._hitAreaObject.Collider.enabled = true;
				}
				this._isHiAreaActive = true;
				return;
			}
			if (this._hitAreaObject.Collider.enabled)
			{
				this._hitAreaObject.Collider.enabled = false;
			}
		}

		public virtual void SetEnableDownLoopSelection(bool enable)
		{
			this._enableDownLoopSelection = enable;
		}

		public virtual void SetLongDownLoopTime(bool useDefault, float longDownLoopTime)
		{
			if (useDefault)
			{
				this._longDownLoopTime = AnMonoSingleton<AnRootManager>.Instance._GetDefaultLongTouchTime();
				return;
			}
			this._longDownLoopTime = longDownLoopTime;
		}

		public virtual void SetEnableLoopMotionInDownLoop(bool enable)
		{
			this._isLoopMotionInDownLoop = enable;
		}

		public virtual bool _IsLoopState()
		{
			return this._isActive && (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.Loop_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.Loop_Loop);
		}

		public virtual bool _IsDownState()
		{
			return this._isActive && (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownIn_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownIn_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownLoop_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownLoop_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownOut_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownOut_Loop);
		}

		public virtual bool _IsSelectState()
		{
			return this._isActive && (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectIn_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectIn_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectLoop_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectLoop_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectOut_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectOut_Loop);
		}

		public virtual bool _IsOverState()
		{
			return this._isActive && (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverIn_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverIn_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverLoop_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverLoop_Loop || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverOut_Init || this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverOut_Loop);
		}

		protected bool _IsInputDown()
		{
			if (!this._isActive)
			{
				return false;
			}
			if (!this._isHiAreaActive)
			{
				return false;
			}
			if (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownIn_Init)
			{
				return false;
			}
			if (this._forceDownIn)
			{
				this._currentInputType = AnInputTypes.None;
			}
			if (this._currentInputType != AnInputTypes.None)
			{
				return false;
			}
			AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager._GetTargetCamera(this._motion.GameObject, ref this._inputCamera);
			if (this._forceDownIn)
			{
				this._currentInputType = AnInputTypes.Force;
				this._forceDownIn = false;
				return true;
			}
			if (this._enableDownInputForTouchInput)
			{
				this._currentInput = AnMonoSingleton<AnRootManager>.Instance.UIManager._GetTouchInput(this._hitAreaObject.Collider);
				if (this._currentInput != null)
				{
					this._currentInputType = AnInputTypes.Touch;
					if (this._IsDownState() && !this._enableContinuousInputForTouchInput)
					{
						return false;
					}
					if (!this._enableDownLoopForTouchInput)
					{
						this._isDownInToDownOut = true;
					}
					return true;
				}
			}
			if (this._enableDownInputForKeyInput)
			{
				this._currentInput = AnMonoSingleton<AnRootManager>.Instance.UIManager._GetKeyInput(this);
				if (this._currentInput != null)
				{
					this._currentInputType = AnInputTypes.Key;
					if (this._IsDownState() && !this._enableContinuousInputForKeyInput)
					{
						return false;
					}
					if (!this._enableDownLoopForKeyInput)
					{
						this._isDownInToDownOut = true;
					}
					return true;
				}
			}
			if (this._enableDownInputForRayInput)
			{
				this._currentInput = AnMonoSingleton<AnRootManager>.Instance.UIManager._GetRayInput(this);
				if (this._currentInput != null)
				{
					this._currentInputType = AnInputTypes.Ray;
					if (this._IsDownState() && !this._enableContinuousInputForRayInput)
					{
						return false;
					}
					if (!this._enableDownLoopForRayInput)
					{
						this._isDownInToDownOut = true;
					}
					return true;
				}
			}
			return false;
		}

		protected bool _IsInputDownLoop()
		{
			if (!this._isActive)
			{
				return false;
			}
			if (!this._enableDownLoopSelection)
			{
				return false;
			}
			if (!this._isHiAreaActive)
			{
				return false;
			}
			if (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.DownIn_Init)
			{
				return false;
			}
			if (this._currentInputType != AnInputTypes.None)
			{
				return false;
			}
			AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager._GetTargetCamera(this._motion.GameObject, ref this._inputCamera);
			if (this._enableDownInputForTouchInput)
			{
				this._currentInput = AnMonoSingleton<AnRootManager>.Instance.UIManager._GetTouchInputDownLoop(this._hitAreaObject.Collider);
				if (this._currentInput != null)
				{
					this._currentInputType = AnInputTypes.Touch;
					if (!this._enableDownLoopForTouchInput)
					{
						this._isDownInToDownOut = true;
					}
					return true;
				}
			}
			return false;
		}

		protected AnInputDownLoopTypes _GetInputDownLoopType()
		{
			if (!this._isActive)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this._currentInput == null)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this.CurrentTouchInput != null)
			{
				return this.CurrentTouchInput._GetDownLoop(this._hitAreaObject.Collider);
			}
			return AnInputDownLoopTypes.NotDownLoop;
		}

		protected AnInputUpTypes _GetInputUpType()
		{
			if (!this._isActive)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this._currentInput == null)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this.CurrentTouchInput != null)
			{
				return this.CurrentTouchInput._GetUp(this._hitAreaObject.Collider);
			}
			if (this.CurrentKeyInput != null)
			{
				return this.CurrentKeyInput._GetUp(this);
			}
			if (this.CurrentRayInput != null)
			{
				return this.CurrentRayInput._GetUp(this);
			}
			return AnInputUpTypes.NotUp;
		}

		protected bool _IsInputSwipeStart(AnUIDirectionTypes directionType)
		{
			this._swipeStartFixScreenPosition.x = 0f;
			this._swipeStartFixScreenPosition.y = 0f;
			this._swipeStartFixScreenPosition.z = 0f;
			this._swipeCurrentFixScreenPosition.x = 0f;
			this._swipeCurrentFixScreenPosition.y = 0f;
			this._swipeCurrentFixScreenPosition.z = 0f;
			this._swipeVector.x = 0f;
			this._swipeVector.y = 0f;
			this._swipeVector.z = 0f;
			if (!this._isActive)
			{
				return false;
			}
			if (this._currentInput == null)
			{
				return false;
			}
			if (this.CurrentTouchInput == null)
			{
				return false;
			}
			if (this.CurrentTouchInput._GetSwipeStart(null, directionType))
			{
				this._swipeStartFixScreenPosition = this.CurrentTouchInput.StartFixScreenPosition;
				this._swipeStartScreenPosition = this.CurrentTouchInput.StartScreenPosition;
				this._UpdateSwipeValue();
				return true;
			}
			return false;
		}

		protected void _UpdateSwipeValue()
		{
			if (!this._isActive)
			{
				return;
			}
			if (this.CurrentTouchInput != null)
			{
				this._swipeCurrentFixScreenPosition = this.CurrentTouchInput.CurrentFixScreenPosition;
				this._swipeCurrentScreenPosition = this.CurrentTouchInput.CurrentScreenPosition;
				this._swipeVector = this.CurrentTouchInput.FixScreenVectorFromStart;
			}
		}

		protected bool _IsInputSelect()
		{
			if (!this._isActive)
			{
				return false;
			}
			if (!this._isHiAreaActive)
			{
				return false;
			}
			if (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.SelectIn_Init)
			{
				return false;
			}
			if (this._forceSelectIn)
			{
				this._forceSelectIn = false;
				return true;
			}
			return false;
		}

		protected bool _IsInputOver()
		{
			if (!this._isActive)
			{
				return false;
			}
			if (!this._isHiAreaActive)
			{
				return false;
			}
			if (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.OverIn_Init)
			{
				return false;
			}
			if (this._forceOverIn)
			{
				this._forceOverIn = false;
				return true;
			}
			return false;
		}

		public virtual void SetLoopIn()
		{
			this._UpdateForce();
			this._Update_Loop_Init();
		}

		public virtual void SetDownIn()
		{
			this._forceDownIn = true;
			this._UpdateForce();
			this._forceDownIn = false;
		}

		public virtual void SetDownLoop()
		{
			this._forceDownIn = true;
			this._forceDownLoop = true;
			this._UpdateForce();
			this._forceDownIn = false;
			this._forceDownLoop = false;
		}

		public virtual void SetDownInToDownOut()
		{
			this._isDownInToDownOut = true;
			this._forceDownIn = true;
			this._UpdateForce();
			this._forceDownIn = false;
		}

		public virtual void SetDownOutInRange()
		{
			this._UpdateForce();
			if (!this._IsDownState())
			{
				return;
			}
			this._inputUpType = AnInputUpTypes.UpInRange;
			this._Update_DownOut_Init();
		}

		public virtual void SetDownOutOutRange()
		{
			this._UpdateForce();
			if (!this._IsDownState())
			{
				return;
			}
			this._inputUpType = AnInputUpTypes.UpOutRange;
			this._Update_DownOut_Init();
		}

		public virtual void SetSelectIn()
		{
			this._forceSelectIn = true;
			this._UpdateForce();
			this._forceSelectIn = false;
		}

		public virtual void SetSelectLoop()
		{
			this._forceSelectIn = true;
			this._forceSelectLoop = true;
			this._UpdateForce();
			this._forceSelectIn = false;
			this._forceSelectLoop = false;
		}

		public virtual void SetSelectOut()
		{
			this._UpdateForce();
			if (!this._IsSelectState())
			{
				return;
			}
			this._Update_SelectOut_Init();
		}

		public virtual void SetOverIn()
		{
			this._forceOverIn = true;
			this._UpdateForce();
			this._forceOverIn = false;
		}

		public virtual void SetOverLoop()
		{
			this._forceOverIn = true;
			this._forceOverLoop = true;
			this._UpdateForce();
			this._forceOverIn = false;
			this._forceOverLoop = false;
		}

		public virtual void SetOverOut()
		{
			this._UpdateForce();
			if (!this._IsOverState())
			{
				return;
			}
			this._Update_OverOut_Init();
		}

		public virtual AnUIBase GetNextInputUI(AnUIInputDirectionTypes inputDirectionType)
		{
			if (inputDirectionType == AnUIInputDirectionTypes.None)
			{
				return null;
			}
			if (!this._nextInputUIExistList[(int)inputDirectionType])
			{
				return null;
			}
			return this._nextInputUIList[(int)inputDirectionType];
		}

		public virtual bool ExistNextInputUI(AnUIInputDirectionTypes inputDirectionType)
		{
			return inputDirectionType != AnUIInputDirectionTypes.None && this._nextInputUIExistList[(int)inputDirectionType];
		}

		public virtual void SetNextInputUI(AnUIBase targetInputUI, AnUIInputDirectionTypes inputDirectionType)
		{
			if (inputDirectionType == AnUIInputDirectionTypes.None)
			{
				return;
			}
			this._nextInputUIExistList[(int)inputDirectionType] = true;
			this._nextInputUIList[(int)inputDirectionType] = targetInputUI;
		}

		public virtual void SetNextInputUI(AnUIBase targetInputUI)
		{
			for (int i = 0; i < this._nextInputUIList.Count; i++)
			{
				this._nextInputUIExistList[i] = true;
				this._nextInputUIList[i] = targetInputUI;
			}
		}

		public virtual void RemoveNextInputUI(AnUIInputDirectionTypes inputDirectionType)
		{
			if (inputDirectionType == AnUIInputDirectionTypes.None)
			{
				return;
			}
			this._nextInputUIExistList[(int)inputDirectionType] = false;
			this._nextInputUIList[(int)inputDirectionType] = null;
		}

		public virtual void ClearNextInputUI()
		{
			for (int i = 0; i < this._nextInputUIList.Count; i++)
			{
				this._nextInputUIExistList[i] = false;
				this._nextInputUIList[i] = null;
			}
		}

		public virtual void SetEnableSubmitDelayTimeForRayInput(bool enable)
		{
			this._enableSubmitDelayTimeForRayInput = enable;
		}

		public virtual void SetCustomSubmitDelayTimeForRayInput(float delayTime)
		{
			this._customSubmitDelayTimeForRayInput = delayTime;
		}

		public virtual void SetEnableDownLoop(bool enable)
		{
			this.SetEnableDownLoop(enable, AnInputTypes.Touch);
			this.SetEnableDownLoop(enable, AnInputTypes.Key);
			this.SetEnableDownLoop(enable, AnInputTypes.Ray);
		}

		public virtual void SetEnableDownLoop(bool enable, AnInputTypes inputType)
		{
			if (inputType == AnInputTypes.Touch)
			{
				this._enableDownLoopForTouchInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Key)
			{
				this._enableDownLoopForKeyInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Ray)
			{
				this._enableDownLoopForRayInput = enable;
			}
		}

		public virtual void SetEnableOverInput(bool enable)
		{
			this.SetEnableOverInput(enable, AnInputTypes.Mouse);
		}

		public virtual void SetEnableOverInput(bool enable, AnInputTypes inputType)
		{
			if (inputType == AnInputTypes.Mouse)
			{
				this._enableOverInputForMouse = enable;
			}
		}

		public virtual void SetEnableSelectInput(bool enable)
		{
			this.SetEnableSelectInput(enable, AnInputTypes.Touch);
			this.SetEnableSelectInput(enable, AnInputTypes.Key);
			this.SetEnableSelectInput(enable, AnInputTypes.Ray);
		}

		public virtual void SetEnableSelectInput(bool enable, AnInputTypes inputType)
		{
			if (inputType == AnInputTypes.Touch)
			{
				this._enableSelectInputForTouchInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Key)
			{
				this._enableSelectInputForKeyInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Ray)
			{
				this._enableSelectInputForRayInput = enable;
			}
		}

		public virtual void SetEnableDownInput(bool enable)
		{
			this.SetEnableDownInput(enable, AnInputTypes.Touch);
			this.SetEnableDownInput(enable, AnInputTypes.Key);
			this.SetEnableDownInput(enable, AnInputTypes.Ray);
		}

		public virtual void SetEnableDownInput(bool enable, AnInputTypes inputType)
		{
			if (inputType == AnInputTypes.Touch)
			{
				this._enableDownInputForTouchInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Key)
			{
				this._enableDownInputForKeyInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Ray)
			{
				this._enableDownInputForRayInput = enable;
			}
		}

		public virtual void SetEnableContinuousInput(bool enable)
		{
			this.SetEnableDownInput(enable, AnInputTypes.Touch);
			this.SetEnableDownInput(enable, AnInputTypes.Key);
			this.SetEnableDownInput(enable, AnInputTypes.Ray);
		}

		public virtual void SetEnableContinuousInput(bool enable, AnInputTypes inputType)
		{
			if (inputType == AnInputTypes.Touch)
			{
				this._enableContinuousInputForTouchInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Key)
			{
				this._enableContinuousInputForKeyInput = enable;
				return;
			}
			if (inputType == AnInputTypes.Ray)
			{
				this._enableContinuousInputForRayInput = enable;
			}
		}

		public virtual void SetCustomSubmitButtonName(string buttonName)
		{
			if (this._customSubmitButtonNameList == null)
			{
				this._customSubmitButtonNameList = new List<string>();
			}
			this._customSubmitButtonNameList.Clear();
			if (buttonName == null || buttonName == "")
			{
				return;
			}
			string[] array = buttonName.Split(new char[] { ',' });
			this._customSubmitButtonNameList.AddRange(array);
		}

		public virtual void SetCustomCancelButtonName(string buttonName)
		{
			if (this._customCancelButtonNameList == null)
			{
				this._customCancelButtonNameList = new List<string>();
			}
			this._customCancelButtonNameList.Clear();
			if (buttonName == null || buttonName == "")
			{
				return;
			}
			string[] array = buttonName.Split(new char[] { ',' });
			this._customCancelButtonNameList.AddRange(array);
		}

		public virtual void SetParentUI(AnUIBase parentUI)
		{
			this._parentUI = parentUI;
		}

		public virtual void SetEnable(bool enable)
		{
			this.SetEnable(enable, this._enableType);
		}

		[Obsolete("Use Set Enable")]
		public virtual void SetEnableWithGrayscale(bool enable)
		{
			this.SetEnable(enable, AnUIEnableTypes.WithGrayscale);
		}

		[Obsolete("Use Set Enable")]
		public virtual void SetEnableWithDisableLabel(bool enable)
		{
			this.SetEnable(enable, AnUIEnableTypes.WithDisableLabel);
		}

		public virtual void SetEnable(bool enable, AnUIEnableTypes enableType)
		{
			this._enable = enable;
			this._enableType = enableType;
			this._UpdateForce();
		}

		protected virtual void _UpdateEnable()
		{
			if (!this._exist)
			{
				return;
			}
			if (this._motion == null)
			{
				return;
			}
			if (this._enableType == AnUIEnableTypes.Normal)
			{
				if (!this._enable)
				{
					this._ResetMotion(false);
					return;
				}
			}
			else if (this._enableType == AnUIEnableTypes.WithGrayscale)
			{
				if (this._enable)
				{
					if (this._motion.IsGrayscale)
					{
						this._motion.SetGrayscale(false);
						return;
					}
				}
				else if (!this._motion.IsGrayscale)
				{
					this._motion.SetGrayscale(true);
					return;
				}
			}
			else if (this._enableType == AnUIEnableTypes.WithDisableLabel)
			{
				if (this._enable)
				{
					if (this._motion.Parameter._ExistLabel(this._labelDisable) && this._motion.CurrentLabelName == this._labelDisable)
					{
						this._PlayMotion(this._labelLoop, true);
						return;
					}
				}
				else if (this._motion.Parameter._ExistLabel(this._labelDisable) && this._motion.CurrentLabelName != this._labelDisable)
				{
					this._PlayMotion(this._labelDisable, true);
				}
			}
		}

		public virtual void SetParentEnable(bool enable)
		{
			this._parentEnable = enable;
			this._UpdateForce();
		}

		public virtual void ResetUpdateDepth()
		{
			this._currentUpdateDepth = 0;
		}

		public virtual void RemoveAllAction()
		{
			if (!this._exist)
			{
				return;
			}
			if (this._actionList == null)
			{
				return;
			}
			for (int i = 0; i < this._actionList.Count; i++)
			{
				this._actionList[i].RemoveAllAction();
			}
		}

		protected virtual void _ExecuteAction(Action action, AnAction flAction)
		{
			if (!this._exist)
			{
				return;
			}
			if (!this._isActive)
			{
				return;
			}
			if (this._currentUpdateDepth > this._maxUpdateDepth)
			{
				return;
			}
			if (action == null && flAction == null)
			{
				return;
			}
			if (action == null && flAction.ActionList.Count == 0)
			{
				return;
			}
			if (action != null)
			{
				action();
			}
			if (flAction != null)
			{
				flAction._ExecuteAction();
			}
		}

		protected AnAction _AddAction()
		{
			if (this._actionList == null)
			{
				this._actionList = new List<AnAction>();
			}
			AnAction anAction = new AnAction();
			this._actionList.Add(anAction);
			return anAction;
		}

		protected void _ReleaseAction()
		{
			if (!this._exist)
			{
				return;
			}
			if (this._actionList == null)
			{
				return;
			}
			for (int i = 0; i < this._actionList.Count; i++)
			{
				this._actionList[i]._Release();
				this._actionList[i] = null;
			}
			this._actionList = null;
		}

		public virtual bool _UpdateUI(object arg)
		{
			return false;
		}

		protected void _SetLog(AnLogTypes logType)
		{
		}

		protected void _SetErrorLog(AnLogTypes logType)
		{
		}

		protected string _logTitle = "";

		protected Color _logColor = Color.white;

		protected bool _exist;

		protected bool _firstUpdateFlag = true;

		protected bool _updateFlag;

		protected int _currentUpdateDepth;

		protected int _maxUpdateDepth = 3;

		protected bool _enable = true;

		protected AnUIEnableTypes _enableType;

		protected bool _parentEnable = true;

		protected bool _isActive;

		protected bool _prevIsActive = true;

		protected AnUIBase.FlUIBaseStateTypes _currentBaseState = AnUIBase.FlUIBaseStateTypes.Loop_Init;

		protected AnComponentBase _component;

		protected AnRoot _root;

		protected GameObject _rootObject;

		protected string _motionPath = "MOT_";

		protected AnMotion _motion;

		protected GameObject _motionObject;

		protected string _hitAreaObjectPath = "OBJ_hit";

		protected AnObjectBase _hitAreaObject;

		protected bool _isHiAreaActive;

		protected Camera _inputCamera;

		protected AnInputTypes _currentInputType;

		protected AnInputBase _currentInput;

		protected AnUIBase _parentUI;

		protected AnInputUpTypes _inputUpType;

		protected bool _forceDownIn;

		protected bool _forceDownLoop;

		protected bool _forceSelectIn;

		protected bool _forceSelectLoop;

		protected bool _forceOverIn;

		protected bool _forceOverLoop;

		protected bool _enableOverInputForMouse = true;

		protected bool _enableSelectInputForTouchInput = true;

		protected bool _enableSelectInputForKeyInput = true;

		protected bool _enableSelectInputForRayInput = true;

		protected bool _enableDownInputForTouchInput = true;

		protected bool _enableDownInputForKeyInput = true;

		protected bool _enableDownInputForRayInput = true;

		protected bool _enableDownLoopForTouchInput = true;

		protected bool _enableDownLoopForKeyInput;

		protected bool _enableDownLoopForRayInput;

		protected bool _enableContinuousInputForTouchInput = true;

		protected bool _enableContinuousInputForKeyInput = true;

		protected bool _enableContinuousInputForRayInput = true;

		protected List<AnUIBase> _nextInputUIList;

		protected List<bool> _nextInputUIExistList;

		protected bool _enableSubmitDelayTimeForRayInput;

		protected float _customSubmitDelayTimeForRayInput = AnMonoSingleton<AnRootManager>.Instance._GetRayInputSubmitDelay();

		protected List<string> _customSubmitButtonNameList;

		protected List<string> _customCancelButtonNameList;

		protected bool _enableDownLoopSelection;

		protected bool _isLoopMotionInDownLoop = true;

		protected bool _downLoopOnFlag;

		protected bool _isLongDownLoopStart;

		protected float _currentLongDownLoopTime;

		protected float _longDownLoopTime = AnMonoSingleton<AnRootManager>.Instance._GetDefaultLongTouchTime();

		protected bool _isDownInToDownOut;

		protected Hashtable _uiLabelNameTable;

		protected string _labelLoop = "Loop";

		protected string _labelDisable = "Disable";

		protected string _labelDownIn = "DownIn";

		protected string _labelDownLoop = "DownLoop";

		protected string _labelDownOut = "DownOut";

		protected string _labelDownOutOn = "DownOutOn";

		protected string _labelSelectIn = "SelectIn";

		protected string _labelSelectLoop = "SelectLoop";

		protected string _labelSelectOut = "SelectOut";

		protected string _labelOverIn = "OverIn";

		protected string _labelOverLoop = "OverLoop";

		protected string _labelOverOut = "OverOut";

		protected bool _isSwipeStart;

		protected bool _isSwiping;

		protected AnUIDirectionTypes _swipeDirectionType;

		protected Vector3 _swipeVector = Vector3.zero;

		protected Vector3 _swipeStartFixScreenPosition = Vector3.zero;

		protected Vector3 _swipeCurrentFixScreenPosition = Vector3.zero;

		protected Vector3 _swipeStartScreenPosition = Vector3.zero;

		protected Vector3 _swipeCurrentScreenPosition = Vector3.zero;

		protected List<AnAction> _actionList;

		protected bool _initializeValueChangeFlag;

		protected bool _executeValueChangeActionFlag;

		protected AnCommonStateTypes _currentValueCnageState;

		protected bool _animationFlag;

		protected bool _inputFlag;

		protected AnUIDirectionTypes _directionType;

		public enum FlUIBaseStateTypes
		{
			None,
			Loop_Init,
			Loop_Loop,
			DownIn_Init,
			DownIn_Loop,
			DownLoop_Init,
			DownLoop_Loop,
			DownOut_Init,
			DownOut_Loop,
			SelectIn_Init,
			SelectIn_Loop,
			SelectLoop_Init,
			SelectLoop_Loop,
			SelectOut_Init,
			SelectOut_Loop,
			OverIn_Init,
			OverIn_Loop,
			OverLoop_Init,
			OverLoop_Loop,
			OverOut_Init,
			OverOut_Loop
		}
	}
}
