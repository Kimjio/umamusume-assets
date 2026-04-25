using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnMotion : AnBase
	{
		public AnMotionParameter Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		public AnObject ParentObject
		{
			get
			{
				return this._parentObject;
			}
		}

		public bool ExistParentObject
		{
			get
			{
				return this._existParentObject;
			}
		}

		public string CurrentLabelName
		{
			get
			{
				return this._currentLabelName;
			}
			set
			{
				this._currentLabelName = value;
			}
		}

		public int CurrentLabelIndex
		{
			get
			{
				return this._currentLabelIndex;
			}
			set
			{
				this._currentLabelIndex = value;
			}
		}

		public Vector2 CurrentLabelTimeRange
		{
			get
			{
				return this._currentLabelTimeRange;
			}
			set
			{
				this._currentLabelTimeRange = value;
			}
		}

		public float CurrentLabelTimeLength
		{
			get
			{
				return this._currentLabelTimeRange.y - this._currentLabelTimeRange.x;
			}
		}

		public float CurrentLabelTime
		{
			get
			{
				return AnUtilityValue.GetLimitValue(this._currentTime - this._currentLabelTimeRange.x, 0f, this._currentLabelTimeRange.y - this._currentLabelTimeRange.x);
			}
		}

		public int CurrentLabelFrame
		{
			get
			{
				return Mathf.FloorToInt(this.CurrentLabelTime * this._root.Parameter.BaseFrameRate);
			}
		}

		public float CurrentLabelNormalizeTime
		{
			get
			{
				return (this._currentTime - this._currentLabelTimeRange.x) / (this._currentLabelTimeRange.y - this._currentLabelTimeRange.x);
			}
		}

		public string NextLabelName
		{
			get
			{
				return this._nextLabelName;
			}
			set
			{
				this._nextLabelName = value;
			}
		}

		public int NextLabelIndex
		{
			get
			{
				return this._nextLabelIndex;
			}
			set
			{
				this._nextLabelIndex = value;
			}
		}

		public Action LabelActionStart
		{
			get
			{
				return this._labelActionStart;
			}
			set
			{
				this._labelActionStart = value;
			}
		}

		public Action LabelActionLoop
		{
			get
			{
				return this._labelActionLoop;
			}
			set
			{
				this._labelActionLoop = value;
			}
		}

		public Action LabelActionEnd
		{
			get
			{
				return this._labelActionEnd;
			}
			set
			{
				this._labelActionEnd = value;
			}
		}

		public bool ExistLabelActionStart
		{
			get
			{
				return this._existLabelActionStart;
			}
			set
			{
				this._existLabelActionStart = value;
			}
		}

		public bool ExistLabelActionLoop
		{
			get
			{
				return this._existLabelActionLoop;
			}
			set
			{
				this._existLabelActionLoop = value;
			}
		}

		public bool ExistLabelActionEnd
		{
			get
			{
				return this._existLabelActionEnd;
			}
			set
			{
				this._existLabelActionEnd = value;
			}
		}

		public AnMotion.ResetModeTypes ResetModeType
		{
			get
			{
				return this._resetModeType;
			}
		}

		public AnMotion.StateTypes CurrentState
		{
			get
			{
				return this._currentStateType;
			}
		}

		public float CurrentTime
		{
			get
			{
				return this._currentTime;
			}
			set
			{
				this._currentTime = value;
			}
		}

		public float PrevTime
		{
			get
			{
				return this._prevTime;
			}
		}

		public float ObjectTime
		{
			get
			{
				return this._objectTime;
			}
		}

		public float ObjectTimeWithoutLastFrame
		{
			get
			{
				return this._objectTimeWithoutLastFrame;
			}
		}

		public float FixObjectTime
		{
			get
			{
				return this._fixObjectTime;
			}
		}

		public float MotionSpeed
		{
			get
			{
				return this._motionSpeed;
			}
		}

		public bool ExistStencilRefCountUp
		{
			get
			{
				return this._existStencilRefCountUp;
			}
			set
			{
				this._existStencilRefCountUp = value;
			}
		}

		public List<AnObjectBase> ObjectList
		{
			get
			{
				return this._objectList;
			}
		}

		public AnMotion(GameObject gameObject)
		{
			this._gameObject = gameObject;
			this._transform = gameObject.transform;
			this._id = this._gameObject.GetInstanceID().ToString();
		}

		public void _CreateEditorData(AnMotionParameter parameter, AnObject parentObject, AnRoot root)
		{
			this._root = root;
			this._parameter = parameter;
			if (this._parentObject != null)
			{
				this._parentObject = parentObject;
				this._parentObject.ChildMotion = this;
			}
		}

		public void _ApplyData(AnMotionParameter parameter, AnObject parentObject, AnRoot root)
		{
			this._root = root;
			this._parameter = parameter;
			this._objectList = new List<AnObjectBase>();
			this._root.MotionList.Add(this);
			this._existParentObject = false;
			if (parentObject != null)
			{
				this._parentObject = parentObject;
				this._parentObject.ChildMotion = this;
				this._existParentObject = true;
			}
		}

		public override void _CreateData()
		{
			base._CreateData();
			this._visible = true;
			this._updateLowerFlag = true;
			this._motionSpeed = 1f;
			this._currentTime = 0f;
			this._prevTime = float.MaxValue;
			this._currentStateType = AnMotion.StateTypes.Playing;
			this._resetModeType = AnMotion.ResetModeTypes.ResetAll;
			if (this._existParentObject && this._parentObject.ObjectParameter.MotionResetModeType != AnMotion.ResetModeTypes.ResetAll)
			{
				this._resetModeType = this._parentObject.ObjectParameter.MotionResetModeType;
			}
			this._currentLabelName = this._parameter.LabelParamList[0].Name;
			this._currentLabelTimeRange = this._parameter.LabelParamList[0].TimeRange;
			this._nextLabelName = this._parameter.LabelParamList[0].NextLabel;
			this._layerName = this._root.Parameter.LayerName;
			this._layerIndex = this._root.Parameter.LayerIndex;
			if (this._existParentObject && this._parentObject.LayerName != "")
			{
				this._layerName = this._parentObject.LayerName;
				this._layerIndex = this._parentObject.LayerIndex;
			}
			this._gameObject.layer = this._layerIndex;
			this._sortOrderIndex = this._root.SortOrderCount;
			this._sortOrderIndexForDrawTextLater = this._root.SortOrderCountForDrawTextLater;
			this._sortOffset = 0;
			this._localSortOffset = 0;
			this._sortLayerName = this._root._parameter._sortLayerName;
			if (this._existParentObject)
			{
				this._localSortOffset = this._parentObject.LocalSortOffset;
				this._sortLayerName = this._parentObject._sortLayerName;
			}
			this._isGrayscale = false;
			if (this._existParentObject)
			{
				this._isGrayscale = this._parentObject.IsGrayscale;
			}
			this._existStencilRefCountUp = false;
			if (this._existParentObject)
			{
				this._localStencilRefOffset = this._parentObject.LocalStencilRefOffset;
			}
			this._timeModeType = AnTimeModeTypes.Normal;
			if (this._existParentObject)
			{
				this._timeModeType = this._parentObject.TimeModeType;
			}
			this._currentBlurValue = Vector2.zero;
			if (this._existParentObject)
			{
				this._currentBlurValue = this._parentObject.CurrentBlurValue;
			}
			this._currentBlurQuality = 0;
			if (this._existParentObject)
			{
				this._currentBlurQuality = this._parentObject.CurrentBlurPrecision;
			}
			this._multiplyColor = new Color(1f, 1f, 1f, 1f);
			this._colorOffset = new Color(0f, 0f, 0f, 0f);
		}

		public override void _FixData()
		{
			base._FixData();
			this._CheckLowerObjects();
			this._UpdateSortOrder();
			this._UpdateSortLayer();
			this._UpdateStencilRef(false);
			this._parameter._CreateAllObjectControlInfoList(this);
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._FixData();
			}
			this._updateLowerFlag = true;
		}

		public GameObject _GetChildGameObject(string gameObjectName)
		{
			if (this._childGameObjectTable == null)
			{
				this._CreateChildGameObjectTable();
			}
			if (!this._childGameObjectTable.ContainsKey(gameObjectName))
			{
				return null;
			}
			return this._childGameObjectTable[gameObjectName] as GameObject;
		}

		private void _CreateChildGameObjectTable()
		{
			this._childGameObjectTable = new Hashtable();
			foreach (object obj in this._transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform == this._transform))
				{
					this._childGameObjectTable.Add(transform.name, transform.gameObject);
				}
			}
		}

		public override void _UpdateFirst()
		{
			base._UpdateFirst();
			this._UpdateVisible();
			if (this._root._initializeFlag)
			{
				this._visibleInHierarchy = true;
			}
			if (this._visibleInHierarchy)
			{
				this._UpdateColor();
			}
			if (this._root._initializeFlag)
			{
				this._visibleByAlpha = true;
			}
			if (!this._visibleInHierarchy)
			{
				this._UpdateLowerObjects(false);
			}
			if (this._updateFlag)
			{
				this._UpdateState();
			}
			this._UpdateTime();
			this._UpdateBlurValue();
			this._UpdateChildren();
			this._UpdateObjectControlInfoList();
		}

		public override void _UpdateSecond()
		{
			base._UpdateSecond();
			this._prevTime = this._currentTime;
		}

		private void _UpdateVisible()
		{
			if (this._existParentObject && !this._parentObject._visibleInHierarchy)
			{
				return;
			}
			if (!this._root._visibleInHierarchy)
			{
				return;
			}
			if (!this._visible)
			{
				return;
			}
			if (!this._gameObject.activeInHierarchy)
			{
				return;
			}
			this._visibleInHierarchy = true;
			this._updateLowerFlag = true;
		}

		private void _UpdateColor()
		{
			if (this._existParentObject)
			{
				this._currentColor = this._multiplyColor * this._parentObject._currentColor;
				this._currentColorOffset = this._colorOffset + this.ParentObject._currentColorOffset;
			}
			else
			{
				this._currentColor = this._multiplyColor;
				this._currentColorOffset = this._colorOffset;
			}
			if (this._currentColor.a + this._currentColorOffset.a <= AnValue.MinAlphaValue)
			{
				return;
			}
			this._visibleByAlpha = true;
		}

		private void _UpdateState()
		{
			AnMotion.StateTypes currentStateType = this._currentStateType;
			if (currentStateType != AnMotion.StateTypes.Playing)
			{
				return;
			}
			this._parameter._UpdateMotionTime(this);
			if (this._timeModeType != AnTimeModeTypes.Sync || this._currentLabelIndex != this._nextLabelIndex)
			{
				this._currentTime += this._root._deltaTime * this._motionSpeed;
			}
		}

		private void _UpdateTime()
		{
			if (this._currentTime == this._prevTime)
			{
				return;
			}
			this._restCurrentTime = this._currentTime % AnMonoSingleton<AnRootManager>.Instance._currentOneFrameTime;
			if (this._restCurrentTime < 0.001f && this._restCurrentTime > 0f)
			{
				this._currentTime -= this._restCurrentTime;
			}
			else if (this._restCurrentTime > AnMonoSingleton<AnRootManager>.Instance._currentOneFrameTime - 0.001f && this._restCurrentTime < AnMonoSingleton<AnRootManager>.Instance._currentOneFrameTime)
			{
				this._currentTime += AnMonoSingleton<AnRootManager>.Instance._currentOneFrameTime - this._restCurrentTime;
			}
			AnUtilityValue.LimitValue(ref this._currentTime, this._currentLabelTimeRange.x, this._currentLabelTimeRange.y);
			this._objectTime = this._currentTime;
			AnUtilityValue.LimitValue(ref this._objectTime, this._currentLabelTimeRange.x + AnValue.ObjectTimeAddValue, this._currentLabelTimeRange.y - AnValue.ObjectTimeAddValue);
			this._objectTimeWithoutLastFrame = this._currentTime;
			if (this._root != null && this._root._parameter != null)
			{
				AnUtilityValue.LimitValue(ref this._objectTimeWithoutLastFrame, this._currentLabelTimeRange.x + AnValue.ObjectTimeAddValue, this._currentLabelTimeRange.y - this._root._parameter._oneFrameTime);
			}
			this._fixObjectTime = this._objectTimeWithoutLastFrame;
			if (this._nextLabelIndex != -1 && this._root != null && this._root._parameter != null && this._nextLabelIndex > this._currentLabelIndex && this._currentLabelTimeRange.y - this._currentLabelTimeRange.x >= this._root._parameter._oneFrameTime + AnValue.ObjectTimeAddValue)
			{
				this._fixObjectTime = this._objectTime;
			}
		}

		private void _UpdateObjectControlInfoList()
		{
			for (int i = 0; i < this._currentObjectControlInfoList.Count; i++)
			{
				AnObjectControlInfo anObjectControlInfo = this._currentObjectControlInfoList[i];
				if (this._objectTime >= anObjectControlInfo._startTime && !anObjectControlInfo._isActive)
				{
					anObjectControlInfo._targetObject.ChildMotion._SetMotionPlayBase(anObjectControlInfo._fixTargetTime, anObjectControlInfo._targetIsStop, false);
					anObjectControlInfo._isActive = true;
				}
			}
		}

		private void _UpdateBlurValue()
		{
			if (this._existParentObject)
			{
				this._currentBlurQuality = this._parentObject.CurrentBlurQuality;
				this._currentBlurPrecision = this._parentObject.CurrentBlurPrecision;
				this._currentBlurValue = this._parentObject.CurrentBlurValue;
			}
		}

		private void _UpdateChildren()
		{
			if (this._updateFlag)
			{
				for (int i = 0; i < this._objectList.Count; i++)
				{
					this._objectList[i]._Update();
				}
				return;
			}
			for (int j = 0; j < this._objectList.Count; j++)
			{
				this._objectList[j]._UpdateForce();
			}
		}

		public override void _ResetTime()
		{
			base._ResetTime();
			if (this._resetModeType == AnMotion.ResetModeTypes.None)
			{
				return;
			}
			if (!this._isResetTime && this._existParentObject)
			{
				if (this._parentObject._isResetTime)
				{
					this._isResetTime = true;
				}
				else if (this._parentObject._parentMotion._currentTime <= this._parentObject._parameter._timeRange.x + AnValue.ObjectTimeAddValue)
				{
					this._isResetTime = true;
				}
				else if (this._parentObject._parentMotion._currentTime >= this._parentObject._parameter._timeRange.y - AnValue.ObjectTimeAddValue)
				{
					this._isResetTime = true;
				}
			}
			if (this._isResetTime)
			{
				float num;
				if (this._resetModeType == AnMotion.ResetModeTypes.ResetLabel)
				{
					num = this._currentLabelTimeRange.x;
				}
				else
				{
					num = -1E-05f;
				}
				this._parameter._SetCurrentLabel(this, num);
				this._prevTime = float.MaxValue;
				this._UpdateTime();
			}
			this._currentStateType = AnMotion.StateTypes.Playing;
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._isResetTime = this._isResetTime;
				this._objectList[i]._ResetTime();
				this._objectList[i]._isResetTime = false;
			}
			this._isResetTime = false;
		}

		public void SetResetModeType(AnMotion.ResetModeTypes resetModeType)
		{
			this._resetModeType = resetModeType;
		}

		public void SetMotionPlay()
		{
			this._currentStateType = AnMotion.StateTypes.Playing;
		}

		public void SetMotionPlay(string labelName)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			this._SetMotionPlayBase(this._currentLabelTimeRange.x, false, true);
		}

		public void SetMotionPlay(string labelName, float timeOffset)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			float num = this._currentLabelTimeRange.x + timeOffset;
			this._SetMotionPlayBase(num, false, true);
		}

		public void SetMotionPlay(string labelName, int frameOffset)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			float num = this._currentLabelTimeRange.x + ((float)frameOffset + 0.001f) / this._root.Parameter.BaseFrameRate;
			this._SetMotionPlayBase(num, false, true);
		}

		public void SetMotionPlay(float time)
		{
			this._SetMotionPlayBase(time, false, true);
		}

		public void SetMotionPlay(int frame)
		{
			float num = ((float)frame + 0.001f) / this._root.Parameter.BaseFrameRate;
			this._SetMotionPlayBase(num, false, true);
		}

		public void SetMotionPause()
		{
			this._currentStateType = AnMotion.StateTypes.Pause;
		}

		public void SetMotionPause(string labelName)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			this._SetMotionPlayBase(this._currentLabelTimeRange.x, true, true);
		}

		public void SetMotionPause(string labelName, float timeOffset)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			float num = this._currentLabelTimeRange.x + timeOffset;
			this._SetMotionPlayBase(num, true, true);
		}

		public void SetMotionPause(string labelName, int frameOffset)
		{
			this._parameter._SetCurrentLabel(this, labelName);
			float num = this._currentLabelTimeRange.x + ((float)frameOffset + 0.001f) / this._root.Parameter.BaseFrameRate;
			this._SetMotionPlayBase(num, true, true);
		}

		public void SetMotionPause(float time)
		{
			this._SetMotionPlayBase(time, true, true);
		}

		public void SetMotionPause(int frame)
		{
			float num = ((float)frame + 0.002f) / this._root.Parameter.BaseFrameRate;
			this._SetMotionPlayBase(num, true, true);
		}

		public void SetMotionReset()
		{
			float num = this._currentTime;
			if (this._resetModeType == AnMotion.ResetModeTypes.None)
			{
				return;
			}
			if (this._resetModeType == AnMotion.ResetModeTypes.ResetLabel)
			{
				num = this._currentLabelTimeRange.x;
			}
			else if (this._resetModeType == AnMotion.ResetModeTypes.ResetAll)
			{
				num = -1E-05f;
			}
			this._SetMotionPlayBase(num, false, true);
		}

		public void SetMotionStop()
		{
			this._SetMotionPlayBase(0f, true, true);
		}

		private void _SetMotionPlayBase(float time, bool pause, bool resetByStartLabel)
		{
			this._currentStateType = AnMotion.StateTypes.Playing;
			if (pause)
			{
				this._currentStateType = AnMotion.StateTypes.Pause;
			}
			this._parameter._SetCurrentLabel(this, time);
			this._prevTime = float.MaxValue;
			this._UpdateTime();
			this._isResetTime = false;
			if (resetByStartLabel && time <= this._parameter._labelParamList[0]._timeRange.x)
			{
				this._isResetTime = true;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._isResetTime = this._isResetTime;
				this._objectList[i]._ResetTime();
				this._objectList[i]._isResetTime = false;
			}
			this._isResetTime = false;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		private void _CheckLowerObjects()
		{
			this._meshRenderList = this._gameObject.GetComponentsInChildren<MeshRenderer>(true);
			this._colliderList = this._gameObject.GetComponentsInChildren<Collider>(true);
			this._collider2DList = this._gameObject.GetComponentsInChildren<Collider2D>(true);
			this._tempTransformList = this._gameObject.GetComponentsInChildren<Transform>(true);
			if (this._tempChildBaseList == null)
			{
				this._tempChildBaseList = new List<AnBase>();
			}
			this._tempChildBaseList.Clear();
			for (int i = 0; i < this._tempTransformList.Length; i++)
			{
				Transform transform = this._tempTransformList[i];
				if (!(transform == this._transform))
				{
					AnBase anBase = this._root.DataTable[transform.gameObject] as AnBase;
					if (anBase != null)
					{
						this._tempChildBaseList.Add(anBase);
					}
				}
			}
			this._childBaseList = this._tempChildBaseList.ToArray();
			this._tempChildBaseList = null;
			this._tempTransformList = null;
		}

		private void _UpdateLowerObjects(bool visible)
		{
			if (!this._updateLowerFlag)
			{
				return;
			}
			this._UpdateChildVisible(visible);
			this._UpdateEnableRenderer(visible);
			this._UpdateEnableCollider(visible);
			this._updateLowerFlag = false;
		}

		private void _UpdateChildVisible(bool visible)
		{
			for (int i = 0; i < this._childBaseList.Length; i++)
			{
				this._childBaseList[i]._visibleInHierarchy = visible;
			}
		}

		private void _UpdateEnableRenderer(bool enable)
		{
			for (int i = 0; i < this._meshRenderList.Length; i++)
			{
				this._meshRenderList[i].enabled = enable;
			}
		}

		private void _UpdateEnableCollider(bool enable)
		{
			for (int i = 0; i < this._colliderList.Length; i++)
			{
				this._colliderList[i].enabled = enable;
				if (!this._visible)
				{
					this._colliderList[i].enabled = false;
				}
			}
			for (int j = 0; j < this._collider2DList.Length; j++)
			{
				this._collider2DList[j].enabled = enable;
				if (!this._visible)
				{
					this._collider2DList[j].enabled = false;
				}
			}
		}

		protected override void _UpdateSortOrder()
		{
			base._UpdateSortOrder();
			if (!this._root.DrawTextLater)
			{
				this._sortOrder = this._root.SortOrderCount - this._sortOrderIndex + this._sortOffset + this._root.DefaultSortOffset + this._localSortOffset;
				return;
			}
			this._sortOrder = this._root.SortOrderCountForDrawTextLater - this._sortOrderIndexForDrawTextLater + this._sortOffset + this._root.DefaultSortOffset + this._localSortOffset;
		}

		protected override void _UpdateSortLayer()
		{
			base._UpdateSortLayer();
			if (this._sortLayerName != "")
			{
				return;
			}
			this._sortLayerName = this._root.Parameter.SortLayerName;
		}

		public override void _UpdateStencilRef(bool affectChildren)
		{
			base._UpdateStencilRef(affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._UpdateStencilRef(affectChildren);
			}
		}

		public override void _UpdateStencilCompareFunc(bool affectChildren)
		{
			base._UpdateStencilCompareFunc(affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._UpdateStencilCompareFunc(affectChildren);
			}
		}

		protected override void _SetGrayscaleBase(bool enable)
		{
			base._SetGrayscaleBase(enable);
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetGrayscale(enable);
			}
		}

		public void SetAction(string labelName, Action action, AnMotionActionTypes actionType)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				if (!anLabelParameter.ActionStartTable.ContainsKey(this))
				{
					anLabelParameter.ActionStartTable.Add(this, action);
				}
				else
				{
					anLabelParameter.ActionStartTable[this] = action;
				}
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				if (!anLabelParameter.ActionLoopTable.ContainsKey(this))
				{
					anLabelParameter.ActionLoopTable.Add(this, action);
				}
				else
				{
					anLabelParameter.ActionLoopTable[this] = action;
				}
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				if (!anLabelParameter.ActionEndTable.ContainsKey(this))
				{
					anLabelParameter.ActionEndTable.Add(this, action);
				}
				else
				{
					anLabelParameter.ActionEndTable[this] = action;
				}
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		public void AddAction(string labelName, AnMotionActionTypes actionType, Action<object> action, object value, int id = -1)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				this._AddActionBase(anLabelParameter.FlActionStartTable, action, value, id);
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				this._AddActionBase(anLabelParameter.FlActionLoopTable, action, value, id);
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				this._AddActionBase(anLabelParameter.FlActionEndTable, action, value, id);
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		public void RemoveAction(string labelName, AnMotionActionTypes actionType)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				if (anLabelParameter.ActionStartTable.ContainsKey(this))
				{
					anLabelParameter.ActionStartTable.Remove(this);
				}
				this._RemoveActionBase(anLabelParameter.FlActionStartTable, -1, -1);
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				if (anLabelParameter.ActionLoopTable.ContainsKey(this))
				{
					anLabelParameter.ActionLoopTable.Remove(this);
				}
				this._RemoveActionBase(anLabelParameter.FlActionLoopTable, -1, -1);
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				if (anLabelParameter.ActionEndTable.ContainsKey(this))
				{
					anLabelParameter.ActionEndTable.Remove(this);
				}
				this._RemoveActionBase(anLabelParameter.FlActionEndTable, -1, -1);
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		public void RemoveAllAction()
		{
			for (int i = 0; i < this._parameter.LabelParamList.Length; i++)
			{
				AnLabelParameter anLabelParameter = this._parameter.LabelParamList[i];
				this.RemoveAction(anLabelParameter.Name, AnMotionActionTypes.Start);
				this.RemoveAction(anLabelParameter.Name, AnMotionActionTypes.Loop);
				this.RemoveAction(anLabelParameter.Name, AnMotionActionTypes.End);
			}
		}

		public void RemoveOnlyAdditionalAction(string labelName, AnMotionActionTypes actionType)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				this._RemoveActionBase(anLabelParameter.FlActionStartTable, -1, -1);
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				this._RemoveActionBase(anLabelParameter.FlActionLoopTable, -1, -1);
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				this._RemoveActionBase(anLabelParameter.FlActionEndTable, -1, -1);
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		public void RemoveOnlyAdditionalAction()
		{
			for (int i = 0; i < this._parameter.LabelParamList.Length; i++)
			{
				AnLabelParameter anLabelParameter = this._parameter.LabelParamList[i];
				this.RemoveOnlyAdditionalAction(anLabelParameter.Name, AnMotionActionTypes.Start);
				this.RemoveOnlyAdditionalAction(anLabelParameter.Name, AnMotionActionTypes.Loop);
				this.RemoveOnlyAdditionalAction(anLabelParameter.Name, AnMotionActionTypes.End);
			}
		}

		public void RemoveActionFromID(string labelName, AnMotionActionTypes actionType, int id)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				this._RemoveActionBase(anLabelParameter.FlActionStartTable, id, -1);
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				this._RemoveActionBase(anLabelParameter.FlActionLoopTable, id, -1);
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				this._RemoveActionBase(anLabelParameter.FlActionEndTable, id, -1);
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		public void RemoveActionFromIndex(string labelName, AnMotionActionTypes actionType, int index)
		{
			AnLabelParameter anLabelParameter = this._parameter._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			if (actionType == AnMotionActionTypes.Start)
			{
				this._RemoveActionBase(anLabelParameter.FlActionStartTable, -1, index);
			}
			else if (actionType == AnMotionActionTypes.Loop)
			{
				this._RemoveActionBase(anLabelParameter.FlActionLoopTable, -1, index);
			}
			else if (actionType == AnMotionActionTypes.End)
			{
				this._RemoveActionBase(anLabelParameter.FlActionEndTable, -1, index);
			}
			this._parameter._SetMotionAction(this, labelName);
		}

		private void _AddActionBase(Hashtable targetTable, Action<object> action, object value, int id = -1)
		{
			if (targetTable == null)
			{
				return;
			}
			if (!targetTable.ContainsKey(this))
			{
				AnAction anAction = new AnAction();
				targetTable.Add(this, anAction);
			}
			AnAction anAction2 = targetTable[this] as AnAction;
			if (anAction2 == null)
			{
				return;
			}
			anAction2.AddAction(action, value, id);
		}

		private void _RemoveActionBase(Hashtable targetTable, int id = -1, int index = -1)
		{
			if (targetTable == null)
			{
				return;
			}
			if (!targetTable.ContainsKey(this))
			{
				return;
			}
			AnAction anAction = targetTable[this] as AnAction;
			if (anAction == null)
			{
				return;
			}
			if (id >= 0 && index < 0)
			{
				anAction.RemoveActionFromID(id, true);
			}
			else if (id < 0 && index >= 0)
			{
				anAction.RemoveActionFromIndex(index, true);
			}
			else
			{
				anAction.RemoveAllAction();
			}
			targetTable[this] = null;
			targetTable.Remove(this);
		}

		public override void SetSortOffset(int sortOffset)
		{
			base.SetSortOffset(sortOffset);
			this._UpdateSortOrder();
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetSortOffset(sortOffset);
			}
		}

		public override void SetSortLayer(string sortLayerName)
		{
			base.SetSortLayer(sortLayerName);
			this._UpdateSortLayer();
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetSortLayer(sortLayerName);
			}
		}

		public override void SetTimeModeType(AnTimeModeTypes timeModeType, bool children)
		{
			base.SetTimeModeType(timeModeType, children);
			if (!children)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetTimeModeType(timeModeType, children);
			}
		}

		public override void SetMotionSpeed(float speed, bool children)
		{
			base.SetMotionSpeed(speed, children);
			if (!children)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetMotionSpeed(speed, children);
			}
		}

		public override void SetColliderThrough(bool through, bool affectChildren)
		{
			base.SetColliderThrough(through, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetColliderThrough(through, affectChildren);
			}
		}

		public override void SetColliderThicknessOffset(float thicknessOffset, bool affectChildren)
		{
			base.SetColliderThicknessOffset(thicknessOffset, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetColliderThicknessOffset(thicknessOffset, affectChildren);
			}
		}

		public override void _UpdateColliderThickness(bool affectChildren)
		{
			base._UpdateColliderThickness(affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._UpdateColliderThickness(affectChildren);
			}
		}

		public override void SetEnableCollider(bool enable, bool affectChildren)
		{
			base.SetEnableCollider(enable, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetEnableCollider(enable, affectChildren);
			}
		}

		public override void SetSubCollider(Collider subCollider, bool affectChildren)
		{
			base.SetSubCollider(subCollider, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetSubCollider(subCollider, affectChildren);
			}
		}

		public override void SetBlurQuality(int blurRadius, int blurQuality, bool affectChildren)
		{
			base.SetBlurQuality(blurRadius, blurQuality, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetBlurQuality(blurRadius, blurQuality, affectChildren);
			}
		}

		public override void SetBlurValue(Vector2 blurValue, bool affectChildren)
		{
			base.SetBlurValue(blurValue, affectChildren);
			if (!affectChildren)
			{
				return;
			}
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i].SetBlurValue(blurValue, affectChildren);
			}
		}

		public override void _UpdateScreenSize()
		{
			base._UpdateScreenSize();
			for (int i = 0; i < this._objectList.Count; i++)
			{
				this._objectList[i]._UpdateScreenSize();
			}
		}

		private Hashtable _childGameObjectTable;

		private AnMotionParameter _parameter;

		private List<AnObjectBase> _objectList;

		private AnObject _parentObject;

		private bool _existParentObject;

		private AnMotion.ResetModeTypes _resetModeType;

		private AnMotion.StateTypes _currentStateType = AnMotion.StateTypes.Pause;

		public string _currentLabelName = "";

		public int _currentLabelIndex;

		public Vector2 _currentLabelTimeRange = Vector2.zero;

		public string _nextLabelName = "";

		public int _nextLabelIndex;

		public List<List<AnObjectControlInfo>> _allObjectControlInfoList;

		public List<AnObjectControlInfo> _currentObjectControlInfoList;

		public Action _labelActionStart;

		public Action _labelActionLoop;

		public Action _labelActionEnd;

		public bool _existLabelActionStart;

		public bool _existLabelActionLoop;

		public bool _existLabelActionEnd;

		public AnAction _labelFlActionStart;

		public AnAction _labelFlActionLoop;

		public AnAction _labelFlActionEnd;

		public bool _existLabelFlActionStart;

		public bool _existLabelFlActionLoop;

		public bool _existLabelFlActionEnd;

		public float _currentTime;

		public float _prevTime = -1f;

		public float _objectTime;

		public float _objectTimeWithoutLastFrame;

		public float _fixObjectTime;

		private float _restCurrentTime;

		private bool _updateLowerFlag;

		private MeshRenderer[] _meshRenderList;

		private Collider[] _colliderList;

		private Collider2D[] _collider2DList;

		private Transform[] _tempTransformList;

		private List<AnBase> _tempChildBaseList;

		private AnBase[] _childBaseList;

		private bool _existStencilRefCountUp;

		public enum ResetModeTypes
		{
			ResetAll,
			None,
			ResetLabel
		}

		public enum StateTypes
		{
			Playing,
			Pause
		}
	}
}
