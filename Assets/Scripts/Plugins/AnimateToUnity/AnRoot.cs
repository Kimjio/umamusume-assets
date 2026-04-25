using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimateToUnity.Utility;

namespace AnimateToUnity
{
    public class AnRoot : AnMonoBehaviour
    {
		public AnRootParameter Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
			}
		}

		public AnMeshParameterGroup MeshParameterGroup
		{
			get
			{
				return this._meshParameterGroup;
			}
			set
			{
				this._meshParameterGroup = value;
			}
		}

		public GameObject TopObject
		{
			get
			{
				return this._topObject;
			}
		}

		public AnMotion RootMotion
		{
			get
			{
				return this._rootMotion;
			}
			set
			{
				this._rootMotion = value;
			}
		}

		public List<AnBase> DataList
		{
			get
			{
				return this._dataList;
			}
		}

		public List<AnMotion> MotionList
		{
			get
			{
				return this._motionList;
			}
		}

		public List<AnObjectBase> ObjectList
		{
			get
			{
				return this._objectList;
			}
		}

		public List<AnBase> FinalizeTargetDataList
		{
			get
			{
				return this._finalizeTargetDataList;
			}
		}

		public int SortOrderCount
		{
			get
			{
				return this._sortOrderCount;
			}
			set
			{
				this._sortOrderCount = value;
			}
		}

		public int SortOrderCountForDrawTextLater
		{
			get
			{
				return this._sortOderCountForDrawTextLater;
			}
			set
			{
				this._sortOderCountForDrawTextLater = value;
			}
		}

		public int SortOrderInterval
		{
			get
			{
				return this._sortOrderInterval;
			}
		}

		public int DefaultSortOffset
		{
			get
			{
				return this._defaultSortOffset;
			}
		}

		public int DefaultStencilRefOffset
		{
			get
			{
				return this._defaultStencilRefOffset;
			}
		}

		public AnStencilCompareFuncTypes DefaultStencilCompareFunc
		{
			get
			{
				return this._defaultStencilCompareFunc;
			}
		}

		public int StencilRefInterval
		{
			get
			{
				return this._stencilRefInterval;
			}
		}

		public int StencilRefCount
		{
			get
			{
				return this._stencilRefCount;
			}
			set
			{
				this._stencilRefCount = value;
			}
		}

		public float DefaultDepthOffset
		{
			get
			{
				return this._defaultDepthOffset;
			}
		}

		public float DefaultScaleOffset
		{
			get
			{
				return this._defaultScaleOffset;
			}
		}

		public float DefaultScaleValue
		{
			get
			{
				return this._defaultScaleValue;
			}
		}

		public float DefaultColliderThickness
		{
			get
			{
				return this._defaultColliderThickness;
			}
		}

		public bool DrawTextLater
		{
			get
			{
				return this._drawTextLater;
			}
		}

		public Hashtable DataTable
		{
			get
			{
				return this._dataTable;
			}
		}

		public Hashtable MeshRendererTable
		{
			get
			{
				return this._meshRendererTable;
			}
		}

		public Hashtable MeshFilterTable
		{
			get
			{
				return this._meshFilterTable;
			}
		}

		public Hashtable ColliderTable
		{
			get
			{
				return this._colliderTable;
			}
		}

		public Hashtable TextMeshTable
		{
			get
			{
				return this._textMeshTable;
			}
		}

		public float DeltaTime
		{
			get
			{
				return this._deltaTime;
			}
		}

		public float CustomDeltaTime
		{
			get
			{
				return this._customDeltaTime;
			}
		}

		public bool UseCustomDeltaTime
		{
			get
			{
				return this._useCustomDeltaTime;
			}
		}

		public float SyncTime
		{
			get
			{
				return this._syncTime;
			}
		}

		public float CustomSyncTime
		{
			get
			{
				return this._customSyncTime;
			}
		}

		public bool UseCustomSyncTime
		{
			get
			{
				return this._useCustomSyncTime;
			}
		}

		public int UpdateInterval
		{
			get
			{
				return this._updateInterval;
			}
		}

		public int UpdateGroup
		{
			get
			{
				return this._updateGroup;
			}
		}

		public Vector2 BaseScreenSize
		{
			get
			{
				return this._baseScreenSize;
			}
		}

		public AnScreenCastTypes ScreenCastType
		{
			get
			{
				return this._screenCastType;
			}
		}

		public float BaseCameraSize
		{
			get
			{
				return this._baseCameraSize;
			}
		}

		public bool FitScreen
		{
			get
			{
				return this._fitScreen;
			}
		}

		public Vector2 ScreenReferenceSize
		{
			get
			{
				return this._screenReferenceSize;
			}
		}

		public Vector2 ScreenBaseRightPosition
		{
			get
			{
				return this._screenBaseRightPosition;
			}
		}

		public Vector2 ScreenBaseLeftPosition
		{
			get
			{
				return this._screenBaseLeftPosition;
			}
		}

		public Vector2 ScreenReferenceFixSize
		{
			get
			{
				return this._screenReferenceFixSize;
			}
		}

		public Vector2 ScreenEdgeRightPosition
		{
			get
			{
				return this._screenEdgeRightPosition;
			}
		}

		public Vector2 ScreenEdgeLeftPosition
		{
			get
			{
				return this._screenEdgeLeftPosition;
			}
		}

		public Vector2 ScreenMarginRightPosition
		{
			get
			{
				return this._screenMarginRightPosition;
			}
		}

		public Vector2 ScreenMarginLeftPosition
		{
			get
			{
				return this._screenMarginLeftPosition;
			}
		}

		public Vector2 ScreenRightPosition
		{
			get
			{
				return this._screenRightPosition;
			}
		}

		public Vector2 ScreenLeftPosition
		{
			get
			{
				return this._screenLeftPosition;
			}
		}

		public Vector2 VirtualScreenSize
		{
			get
			{
				return this._screenVirtualSize;
			}
		}

		private void Awake()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this._Initialize();
			this._Initialize_PostProcess();
		}

		private void OnDestroy()
		{
			if (!Application.isPlaying || this._isDestroy)
			{
				return;
			}
			this._Destroy();
		}

		public void Release()
		{
			this.OnDestroy();
		}

		private void _Initialize()
		{
			this._BootManager();
			this._meshInfoParameterGroupMaterialTable = new Hashtable();
			this._transform = base.transform;
			this._topObject = base.gameObject.transform.parent.gameObject;
			this._topObjectTransform = this._topObject.transform;
			this._ResetTransform();
			AnMonoSingleton<AnRootManager>.Instance._AddRoot(this);
			this._parameter = AnMonoSingleton<AnRootManager>.Instance._GetRootParameter(this._parameter);
			this._meshParameterGroup._Initialize();
			this._CreateHierarchy();
			this._CreateMeshRendererTable();
			this._CreateMeshFilterTable();
			this._CreateColliderTable();
			this._CreateTextMeshTable();
			this._visible = true;
			this._visibleInHierarchy = false;
			this._initializeFlag = true;
			this._isDestroy = false;
			this._updateInterval = 0;
			this._defaultSortOffset = this._parameter._sortOffset;
			this._drawTextLater = this._parameter._drawTextLater;
			this._sortOrderCount = 0;
			this._sortOderCountForDrawTextLater = 5;
			this._defaultStencilRefOffset = this._parameter._stencilRefOffset;
			this._defaultStencilCompareFunc = this._parameter._stencilCompareFunc;
			this._stencilRefInterval = AnMonoSingleton<AnRootManager>.Instance._GetStencilMaskInterval();
			this._stencilRefCount = -1;
			this._deltaTime = 0f;
			this._useCustomDeltaTime = false;
			this._defaultColliderThickness = this._parameter._colliderThickness;
			this._defaultDepthOffset = this._parameter._depthOffset;
			this._defaultScaleOffset = this._parameter._scaleOffset;
			this._defaultScaleValue = this._parameter._scaleValue;
			this._screenCastType = this._parameter._screenCastType;
			this._baseCameraSize = this._parameter._baseCameraSize;
			this._fitScreen = this._parameter._fitScreen;
			this._baseScreenSize = this._parameter._baseScreenUsingSize;
			this._screenReferenceSize = this._parameter._screenReferenceSize;
			this._UpdateScreenSize();
			this._ApplyData();
			this._currentCreateDataIndex = 0;
			while (this._CreateData())
			{
			}
			this._FixData();
		}

		private void _UpdateInitialize()
		{
		}

		private void _Initialize_PostProcess()
		{
		}

		private void _BootManager()
		{
			AnMonoSingleton<AnRootManager>.Instance._Boot();
		}

		private void _CreateHierarchy()
		{
			if (base.gameObject.transform.childCount != 0)
			{
				GameObject[] array = new GameObject[base.gameObject.transform.childCount];
				for (int i = 0; i < base.gameObject.transform.childCount; i++)
				{
					array[i] = base.gameObject.transform.GetChild(i).gameObject;
				}
				for (int j = 0; j < array.Length; j++)
				{
					global::UnityEngine.Object.DestroyImmediate(array[j], true);
				}
			}
			if (base.gameObject.transform.childCount == 0)
			{
				AnMotionParameter anMotionParameter = this._parameter.MotionParameterGroup._GetMotionParameter(this._parameter.RootMotionID);
				if (anMotionParameter == null)
				{
					return;
				}
				anMotionParameter._CreateHierarchy(this, null);
			}
			AnUtilityObject.SetLayer(base.transform.parent.gameObject, this._parameter.LayerIndex);
		}

		private void _CreateMeshRendererTable()
		{
			this._meshRendererTable = new Hashtable();
			foreach (MeshRenderer meshRenderer in base.GetComponentsInChildren<MeshRenderer>())
			{
				this._meshRendererTable.Add(meshRenderer.gameObject, meshRenderer);
			}
		}

		private void _CreateMeshFilterTable()
		{
			this._meshFilterTable = new Hashtable();
			foreach (MeshFilter meshFilter in base.GetComponentsInChildren<MeshFilter>())
			{
				this._meshFilterTable.Add(meshFilter.gameObject, meshFilter);
			}
		}

		private void _CreateColliderTable()
		{
			this._colliderTable = new Hashtable();
			foreach (Collider collider in base.GetComponentsInChildren<Collider>())
			{
				this._colliderTable.Add(collider.gameObject, collider);
			}
			foreach (Collider2D collider2D in base.GetComponentsInChildren<Collider2D>())
			{
				this._colliderTable.Add(collider2D.gameObject, collider2D);
			}
		}

		private void _CreateTextMeshTable()
		{
			this._textMeshTable = new Hashtable();
			TextMesh[] componentsInChildren = base.GetComponentsInChildren<TextMesh>();
			this._sortOrderInterval = 1;
			foreach (TextMesh textMesh in componentsInChildren)
			{
				if (textMesh.name.Contains(AnValue.TextShadowName))
				{
					if (this._sortOrderCount < 2)
					{
						this._sortOrderInterval = 2;
					}
				}
				else if (textMesh.name.Contains(AnValue.TextOutlineName) && this._sortOrderCount < 3)
				{
					this._sortOrderInterval = 3;
				}
				this._textMeshTable.Add(textMesh.gameObject, textMesh);
			}
		}

		private void _UpdateScreenSizeAll()
		{
			this._ResetTransform();
			this._UpdateScreenSize();
			if (Application.isPlaying)
			{
				this._rootMotion._UpdateScreenSize();
			}
			this._RevertTransform();
		}

		private void _ResetTransform()
		{
			if (this._topObjectTransform == null)
			{
				if (this._topObject == null)
				{
					this._topObject = base.gameObject.transform.parent.gameObject;
				}
				this._topObjectTransform = this._topObject.transform;
			}
			this._topObjectCurrentLocalPosition = this._topObjectTransform.localPosition;
			this._topObjectCurrentLocalRotate = this._topObjectTransform.localRotation;
			this._topObjectCurrentLocalScale = this._topObjectTransform.localScale;
			if (this._topObjectTransform.parent != null)
			{
				this._parentObjectTransform = this._topObject.transform.parent;
				this._parentObject = this._parentObjectTransform.gameObject;
				this._topObjectTransform.SetParent(null);
			}
			else
			{
				this._parentObject = null;
				this._parentObjectTransform = null;
			}
			this._topObjectTransform.localPosition = Vector3.zero;
			this._topObjectTransform.localRotation = Quaternion.identity;
			this._topObjectTransform.localScale = Vector3.one;
			this._transform.localPosition = Vector3.zero;
			this._transform.localRotation = Quaternion.identity;
			this._transform.localScale = Vector3.one;
		}

		private void _UpdateScreenSize()
		{
			this._screenBaseAspect = this._baseScreenSize.x / this._baseScreenSize.y;
			this._screenReferenceFixSize.x = this._baseScreenSize.x;
			this._screenReferenceFixSize.y = this._baseScreenSize.y;
			if (this._screenReferenceSize.x > 0f && this._screenReferenceSize.y > 0f)
			{
				this._screenReferenceFixSize.x = this._screenReferenceSize.x;
				this._screenReferenceFixSize.y = this._screenReferenceSize.y;
			}
			this._screenReferenceAspect = this._screenReferenceFixSize.x / this._screenReferenceFixSize.y;
			if (Application.isPlaying)
			{
				if (this._fitScreen)
				{
					this._screenEdgeAspect = AnMonoSingleton<AnRootManager>.Instance._currentScreenAspect;
				}
				else
				{
					this._screenEdgeAspect = this._screenBaseAspect;
				}
			}
			else if (this._fitScreen)
			{
				this._screenEdgeAspect = (float)Screen.width / (float)Screen.height;
			}
			else
			{
				this._screenEdgeAspect = this._screenBaseAspect;
			}
			this._screenBaseRightPosition.x = this._baseScreenSize.x * 0.5f;
			this._screenBaseRightPosition.y = this._baseScreenSize.y * 0.5f;
			this._screenBaseLeftPosition = this._screenBaseRightPosition * -1f;
			if (this._screenEdgeAspect > this._screenBaseAspect)
			{
				this._screenEdgeRightPosition.x = this._baseScreenSize.y * 0.5f * this._screenEdgeAspect;
				this._screenEdgeRightPosition.y = this._baseScreenSize.y * 0.5f;
			}
			else
			{
				this._screenEdgeRightPosition.x = this._baseScreenSize.x * 0.5f;
				this._screenEdgeRightPosition.y = this._baseScreenSize.x * 0.5f / this._screenEdgeAspect;
			}
			this._screenEdgeLeftPosition = this._screenEdgeRightPosition * -1f;
			this._screenEdgeSize.x = this._screenEdgeRightPosition.x - this._screenEdgeLeftPosition.x;
			this._screenEdgeSize.y = this._screenEdgeRightPosition.y - this._screenEdgeLeftPosition.y;
			this._screenMarginRightPosition = this._screenEdgeRightPosition;
			this._screenMarginLeftPosition = this._screenEdgeLeftPosition;
			if (Application.isPlaying && this._fitScreen && !this._ignoreMargin)
			{
				this._screenMarginRightPosition.x = this._screenMarginRightPosition.x - this._screenEdgeSize.x * AnMonoSingleton<AnRootManager>.Instance._screenRightMarginPercent;
				this._screenMarginRightPosition.y = this._screenMarginRightPosition.y - this._screenEdgeSize.y * AnMonoSingleton<AnRootManager>.Instance._screenTopMarginPercent;
				this._screenMarginLeftPosition.x = this._screenMarginLeftPosition.x + this._screenEdgeSize.x * AnMonoSingleton<AnRootManager>.Instance._screenLeftMarginPercent;
				this._screenMarginLeftPosition.y = this._screenMarginLeftPosition.y + this._screenEdgeSize.y * AnMonoSingleton<AnRootManager>.Instance._screenBottomMarginPercent;
			}
			this._screenMarginSize.x = this._screenMarginRightPosition.x - this._screenMarginLeftPosition.x;
			this._screenMarginSize.y = this._screenMarginRightPosition.y - this._screenMarginLeftPosition.y;
			this._screenMarginOffset.x = this._screenMarginRightPosition.x + this._screenMarginLeftPosition.x;
			this._screenMarginOffset.y = this._screenMarginRightPosition.y + this._screenMarginLeftPosition.y;
			this._screenMarginAspect = this._screenMarginSize.x / this._screenMarginSize.y;
			if (this._screenMarginAspect > this._screenBaseAspect)
			{
				this._screenScale = this._screenMarginSize.y / this._screenEdgeSize.y;
			}
			else
			{
				this._screenScale = this._screenMarginSize.x / this._screenEdgeSize.x;
			}
			this._screenRightPosition = this._screenMarginRightPosition;
			this._screenLeftPosition = this._screenMarginLeftPosition;
			if (Application.isPlaying && this._fitScreen)
			{
				if (this._screenMarginAspect > AnMonoSingleton<AnRootManager>.Instance._screenMaxWideAspect)
				{
					this._screenRightPosition.x = this._screenMarginSize.x * 0.5f * AnMonoSingleton<AnRootManager>.Instance._screenMaxWideAspect / this._screenMarginAspect + this._screenMarginOffset.x;
					this._screenLeftPosition.x = -this._screenMarginSize.x * 0.5f * AnMonoSingleton<AnRootManager>.Instance._screenMaxWideAspect / this._screenMarginAspect + this._screenMarginOffset.x;
				}
				else if (this._screenMarginAspect < AnMonoSingleton<AnRootManager>.Instance._screenMaxNarrowAspect)
				{
					this._screenRightPosition.y = this._screenMarginSize.y * 0.5f * this._screenMarginAspect / AnMonoSingleton<AnRootManager>.Instance._screenMaxNarrowAspect + this._screenMarginOffset.y;
					this._screenLeftPosition.y = -this._screenMarginSize.y * 0.5f * this._screenMarginAspect / AnMonoSingleton<AnRootManager>.Instance._screenMaxNarrowAspect + this._screenMarginOffset.y;
				}
			}
			this._screenSize.x = this._screenRightPosition.x - this._screenLeftPosition.x;
			this._screenSize.y = this._screenRightPosition.y - this._screenLeftPosition.y;
			this._screenOffset.x = this._screenRightPosition.x + this._screenLeftPosition.x;
			this._screenOffset.y = this._screenRightPosition.y + this._screenLeftPosition.y;
			if (this._screenCastType == AnScreenCastTypes.ExpandBaseCanvas)
			{
				if (this._screenEdgeAspect > this._screenReferenceAspect)
				{
					this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
					this._screenVirtualSize.y = this._screenReferenceFixSize.y;
					return;
				}
				this._screenVirtualSize.x = this._screenReferenceFixSize.x;
				this._screenVirtualSize.y = this._screenReferenceFixSize.x / this._screenEdgeAspect;
				return;
			}
			else if (this._screenCastType == AnScreenCastTypes.ShrinkBaseCanvas)
			{
				if (this._screenEdgeAspect > this._screenReferenceAspect)
				{
					this._screenVirtualSize.x = this._screenReferenceFixSize.x;
					this._screenVirtualSize.y = this._screenReferenceFixSize.x / this._screenEdgeAspect;
					return;
				}
				this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
				this._screenVirtualSize.y = this._screenReferenceFixSize.y;
				return;
			}
			else
			{
				if (this._screenCastType == AnScreenCastTypes.WidthBaseCanvas)
				{
					this._screenVirtualSize.x = this._screenReferenceFixSize.x;
					this._screenVirtualSize.y = this._screenReferenceFixSize.x / this._screenEdgeAspect;
					return;
				}
				if (this._screenCastType == AnScreenCastTypes.HeightBaseCanvas)
				{
					this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
					this._screenVirtualSize.y = this._screenReferenceFixSize.y;
					return;
				}
				if (this._screenCastType == AnScreenCastTypes.ConstantPixelCanvas)
				{
					if (Application.isPlaying)
					{
						if (this._fitScreen)
						{
							this._screenVirtualSize.x = AnMonoSingleton<AnRootManager>.Instance._screenWidth;
							this._screenVirtualSize.y = AnMonoSingleton<AnRootManager>.Instance._screenHeight;
							return;
						}
						this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
						this._screenVirtualSize.y = this._screenReferenceFixSize.y;
						return;
					}
					else
					{
						if (this._fitScreen)
						{
							this._screenVirtualSize.x = (float)Screen.width;
							this._screenVirtualSize.y = (float)Screen.height;
							return;
						}
						this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
						this._screenVirtualSize.y = this._screenReferenceFixSize.y;
						return;
					}
				}
				else
				{
					if (this._screenCastType != AnScreenCastTypes.OthographicCamera)
					{
						this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
						this._screenVirtualSize.y = this._screenReferenceFixSize.y;
						return;
					}
					this._screenVirtualSize.x = this._screenReferenceFixSize.y * this._screenEdgeAspect;
					this._screenVirtualSize.y = this._screenReferenceFixSize.y;
					if (this._baseCameraSize != 0f)
					{
						this._screenVirtualSize.x = this._screenVirtualSize.x * (this._baseCameraSize / (this._baseScreenSize.y * 0.5f));
						this._screenVirtualSize.y = this._screenVirtualSize.y * (this._baseCameraSize / (this._baseScreenSize.y * 0.5f));
						return;
					}
					this._screenVirtualSize.x = this._screenVirtualSize.x * (1f / (this._baseScreenSize.y * 0.5f));
					this._screenVirtualSize.y = this._screenVirtualSize.y * (1f / (this._baseScreenSize.y * 0.5f));
					return;
				}
			}
		}

		private void _RevertTransform()
		{
			if (this._parentObject != null)
			{
				this._topObjectTransform.SetParent(this._parentObjectTransform, false);
			}
			this._topObjectTransform.position = Vector3.zero;
			this._topObjectTransform.rotation = Quaternion.identity;
			this._transform.position = Vector3.zero;
			this._transform.rotation = Quaternion.identity;
			this._tempVector3_0 = Vector3.zero;
			this._tempVector3_0.z = this._defaultDepthOffset;
			this._transform.position = this._tempVector3_0;
			this._tempVector3_0.z = this._transform.localPosition.z;
			this._topObjectTransform.localPosition = this._topObjectCurrentLocalPosition;
			this._topObjectTransform.localRotation = this._topObjectCurrentLocalRotate;
			this._topObjectTransform.localScale = this._topObjectCurrentLocalScale;
			this._transform.localPosition = this._tempVector3_0;
			this._transform.localRotation = Quaternion.identity;
			this._tempVector3_0.x = this._screenVirtualSize.x / this._screenEdgeSize.x;
			this._tempVector3_0.y = this._screenVirtualSize.y / this._screenEdgeSize.y;
			this._tempVector3_0.z = 1f;
			this._tempVector3_0.x = this._tempVector3_0.x + this._defaultScaleOffset;
			this._tempVector3_0.y = this._tempVector3_0.y + this._defaultScaleOffset;
			if (this._defaultScaleValue > 0f)
			{
				this._tempVector3_0.x = this._tempVector3_0.x * this._defaultScaleValue;
				this._tempVector3_0.y = this._tempVector3_0.y * this._defaultScaleValue;
			}
			this._transform.localScale = this._tempVector3_0;
		}

		private void _ApplyData()
		{
			this._motionList = new List<AnMotion>();
			this._objectList = new List<AnObjectBase>();
			this._finalizeTargetDataList = new List<AnBase>();
			this._dataTable = new Hashtable();
			this._dataList = new List<AnBase>();
			AnMotionParameter anMotionParameter = this._parameter.MotionParameterGroup._GetMotionParameter(this._parameter.RootMotionID);
			if (anMotionParameter == null)
			{
				return;
			}
			anMotionParameter._ApplyData(null, this);
		}

		private bool _CreateData()
		{
			if (this._currentCreateDataIndex > 100000)
			{
				return false;
			}
			if (this._currentCreateDataIndex > this._dataList.Count - 1)
			{
				return false;
			}
			if (this._currentCreateDataIndex < 0)
			{
				return false;
			}
			this._dataList[this._currentCreateDataIndex]._CreateData();
			this._currentCreateDataIndex++;
			return true;
		}

		private void _FixData()
		{
			foreach (AnMotion anMotion in this._motionList)
			{
				anMotion._FixData();
			}
			this._deltaTime = this._parameter._oneFrameTime;
			this._initializeFlag = true;
			this._UpdateRoot(true);
			this._initializeFlag = false;
			this._rootMotion.SetMotionReset();
			this._ResetUpdateInterval();
			this._finalizeTargetDataList.Reverse();
			foreach (AnBase anBase in this._finalizeTargetDataList)
			{
				anBase._FinalizeData();
			}
			AnMonoSingleton<AnRootManager>.Instance.UIManager.CollisionManager._AddRoot(this);
			this.SetDefaultColliderThickness(this._parameter.ColliderThickness);
			this.SetDefaultStencilRefOffset(this._parameter.StencilRefOffset);
			this.SetDefaultStencilCompareFunc(this._parameter.StencilCompareFunc);
			if (this._stencilRefCount == -1)
			{
				this._stencilRefCount = 0;
			}
			this._rootMotion._UpdateScreenSize();
			this._RevertTransform();
		}

		public void _UpdateRoot(bool updateTime)
		{
			this._UpdateStart();
			this._UpdateVisible();
			if (this._initializeFlag)
			{
				this._visibleInHierarchy = true;
			}
			this._UpdateChildren();
			this._UpdateEnd();
		}

		private void _UpdateStart()
		{
			this._UpdateScreenSizeChangeFlag();
			if (this._screenSizeChangeFlag)
			{
				this._UpdateScreenSizeAll();
			}
		}

		private void _UpdateVisible()
		{
			if (!this._visible)
			{
				this._visibleInHierarchy = false;
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				this._visibleInHierarchy = false;
				return;
			}
			this._visibleInHierarchy = true;
		}

		private void _UpdateChildren()
		{
			if (this._currentUpdateState != 0)
			{
				return;
			}
			this._rootMotion._Update();
		}

		private void _UpdateUpdateInterval()
		{
			if (this._updateInterval == 0)
			{
				this._currentUpdateState = 0;
				this._deltaTime = 0f;
				return;
			}
			if (this._currentUpdateState == 0)
			{
				this._deltaTime = 0f;
			}
			this._currentUpdateState = (Time.frameCount + this._updateGroup) % (this._updateInterval + 1);
		}

		private void _ResetUpdateInterval()
		{
			this._currentUpdateState = this._updateGroup % (this._updateInterval + 1);
			this._deltaTime = 0f;
		}

		private void _UpdateEnd()
		{
			this._UpdateUpdateInterval();
			this._UpdateTime();
		}

		private void _UpdateTime()
		{
			if (this._useCustomDeltaTime)
			{
				if (this._customDeltaTime < 0f)
				{
					this._deltaTime += AnMonoSingleton<AnRootManager>.Instance._currentDeltaTime;
				}
				else
				{
					this._deltaTime += this._customDeltaTime;
				}
			}
			else
			{
				this._deltaTime += AnMonoSingleton<AnRootManager>.Instance._currentDeltaTime;
			}
			if (!this._useCustomSyncTime)
			{
				this._syncTime = Time.time;
				return;
			}
			if (this._customSyncTime < 0f)
			{
				this._syncTime = AnMonoSingleton<AnRootManager>.Instance._currentTime;
				return;
			}
			this._syncTime = this._customSyncTime;
		}

		private void _UpdateScreenSizeChangeFlag()
		{
			this._screenSizeChangeFlag = false;
			if (this._initializeFlag)
			{
				return;
			}
			if (AnMonoSingleton<AnRootManager>.Instance._screenSizeChangeFlag)
			{
				this._screenSizeChangeFlag = true;
				return;
			}
		}

		private void _Destroy()
		{
			if (this._meshParameterGroup != null)
			{
				this._meshParameterGroup._Destroy();
			}
			if (AnMonoSingleton<AnRootManager>.HasInstance())
			{
				if (this._meshInfoParameterGroupMaterialTable != null)
				{
					foreach (object obj in this._meshInfoParameterGroupMaterialTable.Keys)
					{
						AnMonoSingleton<AnRootManager>.Instance.RemoveSharedMaterial((string)obj);
					}
				}
				foreach (AnBase anBase in this._dataList)
				{
					if (anBase is AnText)
					{
						(anBase as AnText)._Destroy();
					}
				}
				AnMonoSingleton<AnRootManager>.Instance._RemoveRoot(this);
			}
			Hashtable meshInfoParameterGroupMaterialTable = this._meshInfoParameterGroupMaterialTable;
			if (meshInfoParameterGroupMaterialTable != null)
			{
				meshInfoParameterGroupMaterialTable.Clear();
			}
			this._isDestroy = true;
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (this._isDestroy)
			{
				return;
			}
			this._UpdateRoot(false);
		}

		public void SetCustomDeltaTime(bool enable, float deltaTime = -3.4028235E+38f)
		{
			this._useCustomDeltaTime = enable;
			this._customDeltaTime = deltaTime;
		}

		public void SetCustomSyncTime(bool enable, float syncTime = -3.4028235E+38f)
		{
			this._useCustomSyncTime = enable;
			this._customSyncTime = syncTime;
		}

		public void SetUpdateInterval(int interval)
		{
			this._updateInterval = Mathf.Max(0, interval);
			this._updateGroup = AnMonoSingleton<AnRootManager>.Instance._GetUpdateGroup(interval);
			this._ResetUpdateInterval();
		}

		public void SetBaseScreenSize(Vector2 baseScreenSize)
		{
			this._baseScreenSize = baseScreenSize;
			this._UpdateScreenSizeAll();
		}

		public void SetBaseCameraSize(float baseCameraSize)
		{
			this._baseCameraSize = baseCameraSize;
			this._UpdateScreenSizeAll();
		}

		public void SetFitScreen(bool fitScreen)
		{
			this._fitScreen = fitScreen;
			this._UpdateScreenSizeAll();
		}

		public void SetScreenCastType(AnScreenCastTypes screenCastType)
		{
			this._screenCastType = screenCastType;
			this._UpdateScreenSizeAll();
		}

		public void SetScreenReferenceSize(Vector2 screenReferenceSize)
		{
			this._screenReferenceSize = screenReferenceSize;
			this._UpdateScreenSizeAll();
		}

		public void SetDefaultDepthOffset(float defaultDepthOffset)
		{
			this._defaultDepthOffset = defaultDepthOffset;
			this._ResetTransform();
			this._RevertTransform();
		}

		public void SetDefaultScaleOffset(float defalutScaleOffset)
		{
			this._defaultScaleOffset = defalutScaleOffset;
			this._ResetTransform();
			this._RevertTransform();
		}

		public void SetDefaultScaleValue(float scaleValue)
		{
			this._defaultScaleValue = scaleValue;
			this._ResetTransform();
			this._RevertTransform();
		}

		public void SetDefaultSortOffset(int defaultSortOffset)
		{
			this._defaultSortOffset = defaultSortOffset;
			this._rootMotion.SetSortOffset(0);
		}

		public void SetDefaultStencilRefOffset(int defaultStencilRefOffset)
		{
			this._defaultStencilRefOffset = defaultStencilRefOffset;
			this._rootMotion._UpdateStencilRef(true);
		}

		public void SetDefaultStencilCompareFunc(AnStencilCompareFuncTypes stencilCompareFunc)
		{
			this._defaultStencilCompareFunc = stencilCompareFunc;
			this._rootMotion._UpdateStencilCompareFunc(true);
		}

		public void SetDefaultColliderThickness(float defaultColliderThickness)
		{
			this._defaultColliderThickness = defaultColliderThickness;
			this._rootMotion._UpdateColliderThickness(true);
		}

		public void SetDrawTextLater(bool drawTextLater)
		{
			this._drawTextLater = drawTextLater;
			this._rootMotion.SetSortOffset(0);
		}

		public T FindComponent<T>(GameObject rootObject, string path, bool fullMatch = false) where T : Component
		{
			return AnUtilityObject.FindComponent<T>(rootObject, path, fullMatch);
		}

		public T Find<T>(GameObject rootObject, string path, bool fullMatch = false) where T : AnBase
		{
			return AnUtilityObject.FindInstance<T>(this, rootObject, path, fullMatch);
		}

		public List<T> Find<T>(GameObject rootObject) where T : AnBase
		{
			return AnUtilityObject.FindInstancesInChildren<T>(this, rootObject);
		}

		public T FindUI<T>(GameObject rootObject, string path, bool fullMatch = false) where T : AnUIBase
		{
			return AnUtilityObject.FindUIInstance<T>(this, rootObject, path, fullMatch);
		}

		public T FindAndInitUI<T>(GameObject rootObject, string path, bool fullMatch = false) where T : AnUIBase, new()
		{
			return AnUtilityObject.FindAndInitUIInstance<T>(this, rootObject, path, fullMatch);
		}

		public bool _GetMeshParameterGroupMaterial(string textureName, AnShaderTypes shaderType, int stencilRef, int baseStencilRef, AnStencilCompareFuncTypes stencilCompareFunc, bool useCustomMesh, ref Material material)
		{
			if (this.MeshParameterGroup._GetMaterial(textureName, shaderType, stencilRef, baseStencilRef, stencilCompareFunc, useCustomMesh, ref material))
			{
				if (this._meshInfoParameterGroupMaterialTable != null && !this._meshInfoParameterGroupMaterialTable.ContainsKey(material.name))
				{
					this._meshInfoParameterGroupMaterialTable.Add(material.name, material);
					AnMonoSingleton<AnRootManager>.Instance.AddSharedMaterial(material.name, material);
				}
				return true;
			}
			return false;
		}

		public bool _CloneMeshParameterGroupMaterial(Material baseMaterial, string id, ref Material material)
		{
			return this.MeshParameterGroup._CloneMaterial(baseMaterial, id, ref material);
		}

        public AnRootParameter _parameter;

        public AnMeshParameterGroup _meshParameterGroup;

        private AnMotion _rootMotion;

        private List<AnBase> _dataList;

        private List<AnMotion> _motionList;

        private List<AnObjectBase> _objectList;

        private List<AnBase> _finalizeTargetDataList;

        private int _currentCreateDataIndex;

        private Hashtable _dataTable;

        private Hashtable _meshRendererTable;

        private Hashtable _meshFilterTable;

        private Hashtable _colliderTable;

        private Hashtable _textMeshTable;

        private Transform _transform;

        private GameObject _topObject;

        private Transform _topObjectTransform;

        private Vector3 _topObjectCurrentLocalPosition = Vector3.zero;

        private Quaternion _topObjectCurrentLocalRotate = Quaternion.identity;

        private Vector3 _topObjectCurrentLocalScale = Vector3.one;

        private GameObject _parentObject;

        private Transform _parentObjectTransform;

        [NonSerialized]
        public bool _initializeFlag;

        private bool _isDestroy;

        private bool _selectedFlag;

        private int _sortOrderInterval;

        private int _sortOrderCount;

        private int _sortOderCountForDrawTextLater;

        private int _defaultSortOffset;

        private int _defaultStencilRefOffset;

        private AnStencilCompareFuncTypes _defaultStencilCompareFunc;

        private int _stencilRefInterval;

        private int _stencilRefCount;

        private float _defaultDepthOffset;

        private float _defaultScaleOffset;

        private float _defaultScaleValue;

        private float _defaultColliderThickness = 1f;

        private bool _drawTextLater;

        [NonSerialized]
        public Vector2 _screenBaseRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenBaseLeftPosition = Vector3.zero;

        private float _screenBaseAspect;

        private Vector2 _screenReferenceSize = Vector2.zero;

        private Vector2 _screenReferenceFixSize = Vector2.zero;

        private float _screenReferenceAspect;

        [NonSerialized]
        public Vector2 _screenEdgeRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenEdgeLeftPosition = Vector3.zero;

        private Vector2 _screenEdgeSize = Vector3.zero;

        private float _screenEdgeAspect;

        [NonSerialized]
        public Vector2 _screenMarginRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenMarginLeftPosition = Vector3.zero;

        private Vector2 _screenMarginSize = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenMarginOffset = Vector3.zero;

        private float _screenMarginAspect;

        public bool _ignoreMargin;

        [NonSerialized]
        public Vector2 _screenRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenLeftPosition = Vector3.zero;

        private Vector2 _screenSize = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenOffset = Vector3.zero;

        [NonSerialized]
        public float _screenScale = 1f;

        private Vector2 _screenVirtualSize = Vector2.zero;

        private Vector2 _baseScreenSize = Vector2.zero;

        private AnScreenCastTypes _screenCastType;

        private float _baseCameraSize;

        [NonSerialized]
        public bool _fitScreen;

        private bool _screenSizeChangeFlag;

        [NonSerialized]
        public float _deltaTime;

        [NonSerialized]
        public float _syncTime;

        private bool _useCustomSyncTime;

        private float _customSyncTime;

        private bool _useCustomDeltaTime;

        private float _customDeltaTime;

        private int _updateInterval;

        private int _updateGroup;

        private int _currentUpdateState;

        private Hashtable _meshInfoParameterGroupMaterialTable;

        [NonSerialized]
        private Vector3 _tempVector3_0 = Vector3.zero;

        [SerializeField]
        private bool _isWriteAlphaChannel;
    }
}
