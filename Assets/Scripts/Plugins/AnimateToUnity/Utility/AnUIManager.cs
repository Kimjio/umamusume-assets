using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnUIManager
	{
		public AnCameraManager CameraManager
		{
			get
			{
				return this._cameraManager;
			}
		}

		public AnCollisionManager CollisionManager
		{
			get
			{
				return this._collisionManager;
			}
		}

		public AnUIBaseManager UIBaseManager
		{
			get
			{
				return this._uiBaseManager;
			}
		}

		public List<List<AnUIBase>> CurrentInputUIBaseGroupList
		{
			get
			{
				return this._currentInputUIBaseGroupList;
			}
		}

		public List<List<AnUIBase>> PrevInputUIBaseGroupList
		{
			get
			{
				return this._prevInputUIBaseGroupList;
			}
		}

		public List<AnUIBase> CurrentOverInputUIBaseList
		{
			get
			{
				return this._currentOverInputUIBaseList;
			}
		}

		public List<AnUIBase> PrevOverInutUIBaseList
		{
			get
			{
				return this._prevOverInutUIBaseList;
			}
		}

		public void _Initilaize()
		{
			this._exist = false;
			this._tempStringList0 = new List<string>();
			this._mouseInputList = new List<AnMouseInput>();
			this._touchInputList = new List<AnTouchInput>();
			this._keyInputList = new List<AnKeyInput>();
			this._rayInputList = new List<AnRayInput>();
			this._overrideInputAxisActionList = new List<Func<object, Vector2>>();
			this._overrideInputAxisActionValueList = new List<object>();
			this._overrideInputSubmitDownActionList = new List<Func<object, bool>>();
			this._overrideInputSubmitDownActionValueList = new List<object>();
			this._overrideInputSubmitUpActionList = new List<Func<object, bool>>();
			this._overrideInputSubmitUpActionValueList = new List<object>();
			this._overrideInputCancelDownActionList = new List<Func<object, bool>>();
			this._overrideInputCancelDownActionValueList = new List<object>();
			this._overrideInputCancelUpActionList = new List<Func<object, bool>>();
			this._overrideInputCancelUpActionValueList = new List<object>();
			this._overrideInputMouseActionList = new List<Func<object, Vector2>>();
			this._overrideInputMouseActionValueList = new List<object>();
			this._overrideInputRayActionList = new List<Func<object, Ray>>();
			this._overrideInputRayActionValueList = new List<object>();
			this._currentInputUIBaseGroupList = new List<List<AnUIBase>>();
			this._prevInputUIBaseGroupList = new List<List<AnUIBase>>();
			this._currentOverInputUIBaseList = new List<AnUIBase>();
			this._prevOverInutUIBaseList = new List<AnUIBase>();
			for (int i = 0; i < this._maxInputCount; i++)
			{
				AnMouseInput anMouseInput = new AnMouseInput(this, i);
				this._mouseInputList.Add(anMouseInput);
				AnTouchInput anTouchInput = new AnTouchInput(this, i);
				this._touchInputList.Add(anTouchInput);
				AnKeyInput anKeyInput = new AnKeyInput(this, i);
				this._keyInputList.Add(anKeyInput);
				AnRayInput anRayInput = new AnRayInput(this, i);
				this._rayInputList.Add(anRayInput);
				this._overrideInputAxisActionList.Add(null);
				this._overrideInputAxisActionValueList.Add(null);
				this._overrideInputSubmitDownActionList.Add(null);
				this._overrideInputSubmitDownActionValueList.Add(null);
				this._overrideInputSubmitUpActionList.Add(null);
				this._overrideInputSubmitUpActionValueList.Add(null);
				this._overrideInputCancelDownActionList.Add(null);
				this._overrideInputCancelDownActionValueList.Add(null);
				this._overrideInputCancelUpActionList.Add(null);
				this._overrideInputCancelUpActionValueList.Add(null);
				this._overrideInputMouseActionList.Add(null);
				this._overrideInputMouseActionValueList.Add(null);
				this._overrideInputRayActionList.Add(null);
				this._overrideInputRayActionValueList.Add(null);
				this._currentOverInputUIBaseList.Add(null);
				this._prevOverInutUIBaseList.Add(null);
				this._currentInputUIBaseGroupList.Add(new List<AnUIBase>());
				this._prevInputUIBaseGroupList.Add(new List<AnUIBase>());
				for (int j = 0; j < this._maxUICount; j++)
				{
					this._currentInputUIBaseGroupList[i].Add(null);
					this._prevInputUIBaseGroupList[i].Add(null);
				}
			}
			this.EnableMouseInput(true, 0);
			this.EnableTouchInput(true, 0);
			this._cameraManager = new AnCameraManager();
			this._cameraManager._Initialize();
			this._collisionManager = new AnCollisionManager();
			this._collisionManager._Initialize();
			this._uiBaseManager = new AnUIBaseManager();
			this._uiBaseManager._Initialize();
			this._exist = true;
		}

		public void _OptimizeAll()
		{
			this._cameraManager._OptimizeAll();
			this._collisionManager._OptimizeAll();
			this._uiBaseManager._OptimizeAll();
		}

		private void SetEnableInput(bool enable)
		{
			for (int i = 0; i < this._touchInputList.Count; i++)
			{
				this._touchInputList[i]._SetEnable(enable);
			}
		}

		public void _Update()
		{
			if (!this._exist)
			{
				return;
			}
			this._cameraManager._Update();
			this._uiBaseManager._UpdateFirst();
			for (int i = 0; i < this._maxInputCount; i++)
			{
				this._mouseInputList[i]._Update();
				this._touchInputList[i]._Update();
				this._keyInputList[i]._Update();
				this._rayInputList[i]._Update();
			}
			this._uiBaseManager._UpdateSecond();
			for (int j = 0; j < this._maxInputCount; j++)
			{
				this._UpdateInputUIGroup(j, false);
				this._UpdateOverInputUI(j, false);
				this._prevOverInutUIBaseList[j] = this._currentOverInputUIBaseList[j];
			}
		}

		public AnTouchInput _GetTouchInput(Collider collision)
		{
			for (int i = 0; i < this._touchInputList.Count; i++)
			{
				if (this._touchInputList[i]._GetDown(collision) == AnInputDownTypes.DownInRange)
				{
					return this._touchInputList[i];
				}
			}
			return null;
		}

		public AnTouchInput _GetTouchInputDownLoop(Collider collision)
		{
			for (int i = 0; i < this._touchInputList.Count; i++)
			{
				if (this._touchInputList[i]._GetDownLoop(collision) == AnInputDownLoopTypes.DownLoopInRange)
				{
					return this._touchInputList[i];
				}
			}
			return null;
		}

		public AnKeyInput _GetKeyInput(AnUIBase inputUI)
		{
			for (int i = 0; i < this._keyInputList.Count; i++)
			{
				if (this._keyInputList[i]._GetDown(inputUI) == AnInputDownTypes.DownInRange)
				{
					return this._keyInputList[i];
				}
			}
			return null;
		}

		public AnKeyInput _GetRayInput(AnUIBase inputUI)
		{
			for (int i = 0; i < this._rayInputList.Count; i++)
			{
				if (this._rayInputList[i]._GetDown(inputUI) == AnInputDownTypes.DownInRange)
				{
					return this._keyInputList[i];
				}
			}
			return null;
		}

		private void _UpdateInputUIGroup(int inputIndex, bool startSelectLoop)
		{
			for (int i = 0; i < this._currentInputUIBaseGroupList[inputIndex].Count; i++)
			{
				this._UpdateInputUI(inputIndex, i, startSelectLoop);
				this._prevInputUIBaseGroupList[inputIndex][i] = this._currentInputUIBaseGroupList[inputIndex][i];
			}
		}

		private void _UpdateInputUI(int inputIndex, int targetIndex, bool startLoop)
		{
			AnUIBase anUIBase = this._currentInputUIBaseGroupList[inputIndex][targetIndex];
			AnUIBase anUIBase2 = this._prevInputUIBaseGroupList[inputIndex][targetIndex];
			if (anUIBase == anUIBase2)
			{
				if (anUIBase == null)
				{
					return;
				}
				if (!anUIBase._IsLoopState())
				{
					return;
				}
				anUIBase.SetSelectLoop();
			}
			if (anUIBase != null)
			{
				if (anUIBase._IsDownState())
				{
					return;
				}
				if (anUIBase._IsSelectState())
				{
					return;
				}
				if (startLoop)
				{
					anUIBase.SetSelectLoop();
					return;
				}
				anUIBase.SetSelectIn();
			}
		}

		public void SetCurrentInputUI(AnUIBase targetUI, int inputIndex)
		{
			this._SetInputUIBase(targetUI, inputIndex, false, false, false, false, true, 0);
		}

		public void SetCurrentInputUI(AnUIBase targetUI, int inputIndex, bool startLoop)
		{
			this._SetInputUIBase(targetUI, inputIndex, startLoop, false, false, false, true, 0);
		}

		public void SetCurrentInputUI(AnUIBase targetUI, int inputIndex, bool currentStartLoop, bool prevStartLoop)
		{
			this._SetInputUIBase(targetUI, inputIndex, currentStartLoop, prevStartLoop, false, false, true, 0);
		}

		public void AddInputUI(AnUIBase targetUI, int inputIndex)
		{
			this._SetInputUIBase(targetUI, inputIndex, false, false, true, false, false, 0);
		}

		public void AddInputUI(AnUIBase targetUI, int inputIndex, bool startLoop)
		{
			this._SetInputUIBase(targetUI, inputIndex, startLoop, false, true, false, false, 0);
		}

		public void RemoveInputUI(AnUIBase targetUI, int inputIndex)
		{
			this._SetInputUIBase(targetUI, inputIndex, false, false, false, true, false, 0);
		}

		public void RemoveInputUI(AnUIBase targetUI, int inputIndex, bool startLoop)
		{
			this._SetInputUIBase(targetUI, inputIndex, false, startLoop, false, true, false, 0);
		}

		private void _SetInputUIBase(AnUIBase targetUI, int inputIndex, bool currentStartLoop, bool prevStartLoop, bool add, bool remove, bool replace, int replaceIndex)
		{
			if (remove)
			{
				int num = this._GetInputUIIndex(targetUI, inputIndex, true);
				if (num < 0)
				{
					return;
				}
				if (num == 0)
				{
					if (prevStartLoop)
					{
						this._currentInputUIBaseGroupList[inputIndex][0].SetLoopIn();
					}
					else
					{
						this._currentInputUIBaseGroupList[inputIndex][0].SetSelectOut();
					}
					this._currentInputUIBaseGroupList[inputIndex][0] = null;
					this._prevInputUIBaseGroupList[inputIndex][0] = null;
				}
				num = this._GetInputUIIndex(targetUI, inputIndex, true);
				if (num <= 0)
				{
					return;
				}
				if (prevStartLoop)
				{
					this._currentInputUIBaseGroupList[inputIndex][num].SetLoopIn();
				}
				else
				{
					this._currentInputUIBaseGroupList[inputIndex][num].SetSelectOut();
				}
				this._currentInputUIBaseGroupList[inputIndex][num] = null;
				this._prevInputUIBaseGroupList[inputIndex][num] = null;
			}
			else if (add)
			{
				if (this._GetInputUIIndex(targetUI, inputIndex, true) > 0)
				{
					return;
				}
				int num2 = this._GetEmptyInputUIIndex(inputIndex);
				if (num2 <= 0)
				{
					return;
				}
				this._currentInputUIBaseGroupList[inputIndex][num2] = targetUI;
				this._prevInputUIBaseGroupList[inputIndex][num2] = null;
				if (this._currentInputUIBaseGroupList[inputIndex][num2].ParentUI != null)
				{
					this._currentInputUIBaseGroupList[inputIndex][num2] = this._currentInputUIBaseGroupList[inputIndex][num2].ParentUI;
				}
			}
			else if (replace)
			{
				if (this._currentInputUIBaseGroupList[inputIndex][replaceIndex] == targetUI)
				{
					return;
				}
				if (this._currentInputUIBaseGroupList[inputIndex][replaceIndex] != null && this._GetInputUIIndex(this._currentInputUIBaseGroupList[inputIndex][replaceIndex], inputIndex, false) <= 0)
				{
					if (prevStartLoop)
					{
						this._currentInputUIBaseGroupList[inputIndex][replaceIndex].SetLoopIn();
					}
					else
					{
						this._currentInputUIBaseGroupList[inputIndex][replaceIndex].SetSelectOut();
					}
				}
				this._currentInputUIBaseGroupList[inputIndex][replaceIndex] = targetUI;
				this._prevInputUIBaseGroupList[inputIndex][replaceIndex] = null;
				if (this._currentInputUIBaseGroupList[inputIndex][replaceIndex] == null)
				{
					return;
				}
				if (this._currentInputUIBaseGroupList[inputIndex][replaceIndex].ParentUI != null)
				{
					this._currentInputUIBaseGroupList[inputIndex][replaceIndex] = this._currentInputUIBaseGroupList[inputIndex][replaceIndex].ParentUI;
				}
			}
			this._UpdateInputUIGroup(inputIndex, currentStartLoop);
		}

		private int _GetInputUIIndex(AnUIBase targetUI, int inputIndex, bool includeZeroIndex)
		{
			if (targetUI == null)
			{
				return -1;
			}
			int num = 1;
			if (includeZeroIndex)
			{
				num = 0;
			}
			for (int i = num; i < this._currentInputUIBaseGroupList[inputIndex].Count; i++)
			{
				if (this._currentInputUIBaseGroupList[inputIndex][i] != null && this._currentInputUIBaseGroupList[inputIndex][i] == targetUI)
				{
					return i;
				}
			}
			return -1;
		}

		private int _GetEmptyInputUIIndex(int inputIndex)
		{
			for (int i = 0; i < this._currentInputUIBaseGroupList[inputIndex].Count; i++)
			{
				if (this._currentInputUIBaseGroupList[inputIndex][i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		private void _UpdateOverInputUI(int inputIndex, bool startOverLoop)
		{
			if (this._currentOverInputUIBaseList[inputIndex] != this._prevOverInutUIBaseList[inputIndex])
			{
				if (this._currentOverInputUIBaseList[inputIndex] != null)
				{
					if (this._currentOverInputUIBaseList[inputIndex]._IsDownState())
					{
						return;
					}
					if (this._currentOverInputUIBaseList[inputIndex]._IsOverState())
					{
						return;
					}
					if (startOverLoop)
					{
						this._currentOverInputUIBaseList[inputIndex].SetOverLoop();
						return;
					}
					this._currentOverInputUIBaseList[inputIndex].SetOverIn();
				}
				return;
			}
			if (this._currentOverInputUIBaseList[inputIndex] == null)
			{
				return;
			}
			if (!this._currentOverInputUIBaseList[inputIndex]._IsLoopState() && !this._currentOverInputUIBaseList[inputIndex]._IsSelectState())
			{
				return;
			}
			this._currentOverInputUIBaseList[inputIndex].SetOverIn();
		}

		public void SetOverInputUI(AnUIBase targetInputUI, int inputIndex)
		{
			this._SetOverInputUIBase(targetInputUI, inputIndex, false);
		}

		public void SetOverInputUI(AnUIBase targetInputUI, int inputIndex, bool startOverLoop)
		{
			this._SetOverInputUIBase(targetInputUI, inputIndex, startOverLoop);
		}

		private void _SetOverInputUIBase(AnUIBase targetInputUI, int inputIndex, bool startOverLoop)
		{
			if (this._currentOverInputUIBaseList[inputIndex] == targetInputUI)
			{
				return;
			}
			if (this._currentOverInputUIBaseList[inputIndex] != null)
			{
				this._currentOverInputUIBaseList[inputIndex].SetOverOut();
			}
			this._currentOverInputUIBaseList[inputIndex] = targetInputUI;
			this._prevOverInutUIBaseList[inputIndex] = null;
			if (this._currentOverInputUIBaseList[inputIndex] == null)
			{
				return;
			}
			if (this._currentOverInputUIBaseList[inputIndex].ParentUI != null)
			{
				this._currentOverInputUIBaseList[inputIndex] = this._currentOverInputUIBaseList[inputIndex].ParentUI;
			}
			this._UpdateOverInputUI(inputIndex, startOverLoop);
		}

		public void EnableTouchInput(bool enable, int inputIndex)
		{
			this._touchInputList[inputIndex]._SetEnable(enable);
		}

		public void EnableKeyInput(bool enable, int inputIndex)
		{
			this._keyInputList[inputIndex]._SetEnable(enable);
			this._rayInputList[inputIndex]._SetEnable(false);
		}

		public void EnableRayInput(bool enable, int inputIndex)
		{
			this._rayInputList[inputIndex]._SetEnable(enable);
			this._keyInputList[inputIndex]._SetEnable(false);
		}

		public void EnableMouseInput(bool enable, int inputIndex)
		{
			this._mouseInputList[inputIndex]._SetEnable(enable);
		}

		public void SetOverrideInputAxis(Func<object, Vector2> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputAxisActionList[inputIndex] = overrideFunc;
			this._overrideInputAxisActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputSubmitDown(Func<object, bool> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputSubmitDownActionList[inputIndex] = overrideFunc;
			this._overrideInputSubmitDownActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputSubmitUp(Func<object, bool> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputSubmitUpActionList[inputIndex] = overrideFunc;
			this._overrideInputSubmitUpActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputCancelDown(Func<object, bool> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputCancelDownActionList[inputIndex] = overrideFunc;
			this._overrideInputCancelDownActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputCancelUp(Func<object, bool> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputCancelUpActionList[inputIndex] = overrideFunc;
			this._overrideInputCancelUpActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputMouse(Func<object, Vector2> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputMouseActionList[inputIndex] = overrideFunc;
			this._overrideInputMouseActionValueList[inputIndex] = value;
		}

		public void SetOverrideInputRay(Func<object, Ray> overrideFunc, object value, int inputIndex)
		{
			this._overrideInputRayActionList[inputIndex] = overrideFunc;
			this._overrideInputRayActionValueList[inputIndex] = value;
		}

		public Vector3 _GetMousePosition(int inputIndex)
		{
			if (this._overrideInputMouseActionList[inputIndex] != null)
			{
				return this._overrideInputMouseActionList[inputIndex](this._overrideInputMouseActionValueList[inputIndex]);
			}
			if (!Input.mousePresent)
			{
				return Vector3.zero;
			}
			return Input.mousePosition * AnMonoSingleton<AnRootManager>.Instance.ScreenRate;
		}

		public Vector2 _GetAxis(int inputIndex)
		{
			if (this._overrideInputAxisActionList[inputIndex] != null)
			{
				return this._overrideInputAxisActionList[inputIndex](this._overrideInputAxisActionValueList[inputIndex]);
			}
			this._tempAxisVector.x = 0f;
			this._tempAxisVector.y = 0f;
			this._tempStringList0 = AnMonoSingleton<AnRootManager>.Instance._GetHorizontalAxisNameList(inputIndex);
			for (int i = 0; i < this._tempStringList0.Count; i++)
			{
				if (Input.GetAxis(this._tempStringList0[i]) != 0f)
				{
					this._tempAxisVector.x = Input.GetAxis(this._tempStringList0[i]);
				}
			}
			this._tempStringList1 = AnMonoSingleton<AnRootManager>.Instance._GetVerticalAxisNameList(inputIndex);
			for (int j = 0; j < this._tempStringList1.Count; j++)
			{
				if (Input.GetAxis(this._tempStringList1[j]) != 0f)
				{
					this._tempAxisVector.y = Input.GetAxis(this._tempStringList1[j]);
				}
			}
			if (this._tempAxisVector.x == 0f && this._tempAxisVector.y == 0f)
			{
				this._keyInputList[inputIndex].Repeater.End();
			}
			else
			{
				this._keyInputList[inputIndex].Repeater.Start();
			}
			this._keyInputList[inputIndex].Repeater._Update();
			if (!this._keyInputList[inputIndex].Repeater.GetRepeat())
			{
				this._tempAxisVector.x = 0f;
				this._tempAxisVector.y = 0f;
			}
			return this._tempAxisVector;
		}

		public bool _GetSubmitButtonDown(int inputIndex)
		{
			if (this._overrideInputSubmitDownActionList[inputIndex] != null)
			{
				return this._overrideInputSubmitDownActionList[inputIndex](this._overrideInputSubmitDownActionValueList[inputIndex]);
			}
			this._tempStringList0 = AnMonoSingleton<AnRootManager>.Instance._GetSubmitButtonNameList(inputIndex);
			for (int i = 0; i < this._tempStringList0.Count; i++)
			{
				if (Input.GetButtonDown(this._tempStringList0[i]))
				{
					return true;
				}
			}
			return false;
		}

		public bool _GetSubmitButtonUp(int inputIndex)
		{
			if (this._overrideInputSubmitUpActionList[inputIndex] != null)
			{
				return this._overrideInputSubmitUpActionList[inputIndex](this._overrideInputSubmitUpActionValueList[inputIndex]);
			}
			this._tempStringList0 = AnMonoSingleton<AnRootManager>.Instance._GetSubmitButtonNameList(inputIndex);
			for (int i = 0; i < this._tempStringList0.Count; i++)
			{
				if (Input.GetButtonUp(this._tempStringList0[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool _GetCancelButtonDown(int inputIndex)
		{
			if (this._overrideInputCancelDownActionList[inputIndex] != null)
			{
				return this._overrideInputCancelDownActionList[inputIndex](this._overrideInputCancelDownActionValueList[inputIndex]);
			}
			this._tempStringList0 = AnMonoSingleton<AnRootManager>.Instance._GetCancelButtonNameList(inputIndex);
			for (int i = 0; i < this._tempStringList0.Count; i++)
			{
				if (Input.GetButtonDown(this._tempStringList0[i]))
				{
					return true;
				}
			}
			return false;
		}

		public bool _GetCancelButtonUp(int inputIndex)
		{
			if (this._overrideInputCancelUpActionList[inputIndex] != null)
			{
				return this._overrideInputCancelUpActionList[inputIndex](this._overrideInputCancelUpActionValueList[inputIndex]);
			}
			this._tempStringList0 = AnMonoSingleton<AnRootManager>.Instance._GetCancelButtonNameList(inputIndex);
			for (int i = 0; i < this._tempStringList0.Count; i++)
			{
				if (Input.GetButtonUp(this._tempStringList0[i]))
				{
					return true;
				}
			}
			return false;
		}

		public Ray _GetRay(int inputIndex)
		{
			if (this._overrideInputRayActionList[inputIndex] != null)
			{
				return this._overrideInputRayActionList[inputIndex](this._overrideInputRayActionValueList[inputIndex]);
			}
			this._tempRay.direction = Vector3.zero;
			this._tempRay.origin = Vector3.zero;
			if (Camera.main == null)
			{
				return this._tempRay;
			}
			if (!Camera.main.isActiveAndEnabled)
			{
				return this._tempRay;
			}
			this._tempRay.origin = Camera.main.transform.position;
			this._tempRay.direction = Camera.main.transform.forward.normalized;
			return this._tempRay;
		}

		private int _maxInputCount = 10;

		private int _maxUICount = 20;

		private List<AnMouseInput> _mouseInputList;

		private List<AnTouchInput> _touchInputList;

		private List<AnKeyInput> _keyInputList;

		private List<AnRayInput> _rayInputList;

		private List<List<AnUIBase>> _currentInputUIBaseGroupList;

		private List<List<AnUIBase>> _prevInputUIBaseGroupList;

		private List<AnUIBase> _currentOverInputUIBaseList;

		private List<AnUIBase> _prevOverInutUIBaseList;

		private AnCameraManager _cameraManager;

		private AnCollisionManager _collisionManager;

		private AnUIBaseManager _uiBaseManager;

		private bool _exist;

		private List<string> _tempStringList0;

		private List<string> _tempStringList1;

		private List<Func<object, Vector2>> _overrideInputAxisActionList;

		private List<object> _overrideInputAxisActionValueList;

		private List<Func<object, bool>> _overrideInputSubmitDownActionList;

		private List<object> _overrideInputSubmitDownActionValueList;

		private List<Func<object, bool>> _overrideInputSubmitUpActionList;

		private List<object> _overrideInputSubmitUpActionValueList;

		private List<Func<object, bool>> _overrideInputCancelDownActionList;

		private List<object> _overrideInputCancelDownActionValueList;

		private List<Func<object, bool>> _overrideInputCancelUpActionList;

		private List<object> _overrideInputCancelUpActionValueList;

		private List<Func<object, Vector2>> _overrideInputMouseActionList;

		private List<object> _overrideInputMouseActionValueList;

		private List<Func<object, Ray>> _overrideInputRayActionList;

		private List<object> _overrideInputRayActionValueList;

		private Vector2 _tempAxisVector = Vector2.zero;

		private Ray _tempRay;
	}
}
