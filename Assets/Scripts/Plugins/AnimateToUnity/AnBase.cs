using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnBase
	{
		public AnRoot Root
		{
			get
			{
				return this._root;
			}
		}

		public GameObject GameObject
		{
			get
			{
				return this._gameObject;
			}
		}

		public Transform Transform
		{
			get
			{
				return this._transform;
			}
		}

		public string ID
		{
			get
			{
				return this._id;
			}
		}

		public bool Visible
		{
			get
			{
				return this._visible;
			}
		}

		public bool VisibleInHierarchy
		{
			get
			{
				return this._visibleInHierarchy;
			}
		}

		public AnObjectTypes ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		public bool IsGrayscale
		{
			get
			{
				return this._isGrayscale;
			}
		}

		public string LayerName
		{
			get
			{
				return this._layerName;
			}
		}

		public int LayerIndex
		{
			get
			{
				return this._layerIndex;
			}
		}

		public int SortOffset
		{
			get
			{
				return this._sortOffset;
			}
		}

		public int LocalSortOffset
		{
			get
			{
				return this._localSortOffset;
			}
		}

		public string SortLayerName
		{
			get
			{
				return this._sortLayerName;
			}
		}

		public int SortOrderIndex
		{
			get
			{
				return this._sortOrderIndex;
			}
		}

		public int SortOrder
		{
			get
			{
				return this._sortOrder;
			}
		}

		public int StencilRef
		{
			get
			{
				return this._stencilRef;
			}
		}

		public int LocalStencilRefOffset
		{
			get
			{
				return this._localStencilRefOffset;
			}
		}

		public AnStencilCompareFuncTypes LocalStencilCompareFunc
		{
			get
			{
				return this._localStencilCompareFunc;
			}
		}

		public AnTimeModeTypes TimeModeType
		{
			get
			{
				return this._timeModeType;
			}
		}

		public Color MultiplyColor
		{
			get
			{
				return this._multiplyColor;
			}
		}

		public float MultiplyAlpha
		{
			get
			{
				return this._multiplyColor.a;
			}
		}

		public Color ColorOffset
		{
			get
			{
				return this._colorOffset;
			}
		}

		public float AlphaOffset
		{
			get
			{
				return this._colorOffset.a;
			}
		}

		public Color CurrentColor
		{
			get
			{
				return this._currentColor;
			}
		}

		public float CurrentAlpha
		{
			get
			{
				return this._currentColor.a;
			}
		}

		public Color CurrentColorOffset
		{
			get
			{
				return this._currentColorOffset;
			}
		}

		public float CurrentAlphaOffset
		{
			get
			{
				return this._currentColorOffset.a;
			}
		}

		public bool EnableCollider
		{
			get
			{
				return this._enableCollider;
			}
		}

		public bool ColliderThrough
		{
			get
			{
				return this._colliderThrough;
			}
		}

		public Vector2 CurrentBlurValue
		{
			get
			{
				return this._currentBlurValue;
			}
		}

		public int CurrentBlurQuality
		{
			get
			{
				return this._currentBlurQuality;
			}
		}

		public int CurrentBlurPrecision
		{
			get
			{
				return this._currentBlurPrecision;
			}
		}

		public Vector2 BlurValue
		{
			get
			{
				return this._blurValue;
			}
		}

		public int BlurQuality
		{
			get
			{
				return this._blurQuality;
			}
		}

		public int BlurPrecision
		{
			get
			{
				return this._blurPrecision;
			}
		}

		public Action OnAfterUpdateFirst
		{
			get
			{
				return this._onAfterUpdateFirst;
			}
			set
			{
				this._onAfterUpdateFirst = value;
			}
		}

		public virtual void _CreateData()
		{
		}

		public virtual void _FixData()
		{
		}

		public virtual void _FinalizeData()
		{
		}

		protected virtual void _ResetPrevValue()
		{
			this._prevColor = Color.magenta;
			this._prevColorOffset = Color.magenta;
			this._prevBlurQuality = int.MaxValue;
			this._prevBlurPrecision = int.MaxValue;
			this._prevBlurValue = AnValue.Vector2Max;
		}

		public virtual void _Update()
		{
			this._updateFlag = true;
			this._UpdateFirst();
			Action onAfterUpdateFirst = this._onAfterUpdateFirst;
			if (onAfterUpdateFirst != null)
			{
				onAfterUpdateFirst();
			}
			if (!this._visibleInHierarchy)
			{
				return;
			}
			this._UpdateSecond();
		}

		public virtual void _UpdateForce()
		{
			this._updateFlag = false;
			this._UpdateFirst();
			if (!this._visibleInHierarchy || !this._visibleByAlpha)
			{
				return;
			}
			this._UpdateSecond();
		}

		public virtual void _UpdateFirst()
		{
			this._visibleInHierarchy = false;
			this._visibleByAlpha = false;
		}

		public virtual void _UpdateSecond()
		{
			this._prevColor = this._currentColor;
			this._prevColorOffset = this._currentColorOffset;
			this._prevBlurQuality = this._currentBlurQuality;
			this._prevBlurPrecision = this._currentBlurPrecision;
			this._prevBlurValue = this._currentBlurValue;
			this._prevStencilRef = this._stencilRef;
		}

		public virtual void _ResetTime()
		{
		}

		public virtual void SetVisible(bool visible)
		{
			this._visible = visible;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetMultiplyColor(Color value)
		{
			this._multiplyColor.r = value.r;
			this._multiplyColor.g = value.g;
			this._multiplyColor.b = value.b;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetMultiplyAlpha(float alpha)
		{
			this._multiplyColor.a = alpha;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetColorOffset(Color value)
		{
			this._colorOffset.r = value.r;
			this._colorOffset.g = value.g;
			this._colorOffset.b = value.b;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetAlphaOffset(float value)
		{
			this._colorOffset.a = value;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetGrayscale(bool enable)
		{
			this._isGrayscale = enable;
			this._SetGrayscaleBase(enable);
			this._prevIsGrayscale = this._isGrayscale;
		}

		protected virtual void _SetGrayscaleBase(bool enable)
		{
		}

		public virtual void SetLayer(string layerName)
		{
			this._layerName = layerName;
			this._layerIndex = LayerMask.NameToLayer(layerName);
		}

		public virtual void SetLayer(int layerIndex)
		{
			this._layerIndex = layerIndex;
			this._layerName = LayerMask.LayerToName(layerIndex);
		}

		public virtual void SetSortOffset(int sortOffset)
		{
			this._sortOffset = sortOffset;
		}

		public virtual void SetSortLayer(string sortLayerName)
		{
			this._sortLayerName = sortLayerName;
		}

		public virtual void SetTimeModeType(AnTimeModeTypes timeModeType, bool affectChildren)
		{
			this._timeModeType = timeModeType;
		}

		public virtual void SetMotionSpeed(float speed, bool affectChildren)
		{
			this._motionSpeed = speed;
		}

		public virtual void SetColliderThrough(bool through, bool affectChildren)
		{
			this._colliderThrough = through;
		}

		public virtual void SetColliderThicknessOffset(float thicknessOffset, bool affectChildren)
		{
			this._colliderThicknessOffset = thicknessOffset;
		}

		public virtual void _UpdateColliderThickness(bool affectChildren)
		{
		}

		public virtual void SetEnableCollider(bool enable, bool affectChildren)
		{
			this._enableCollider = enable;
		}

		public virtual void SetSubCollider(Collider subCollider, bool affectChildren)
		{
		}

		public virtual void SetBlurQuality(int blurQuality, int blurPrecision, bool affectChildren)
		{
			this._blurQuality = blurQuality;
			this._blurPrecision = blurPrecision;
		}

		public virtual void SetBlurValue(Vector2 blurValue, bool affectChildren)
		{
			this._blurValue = blurValue;
		}

		protected virtual void _UpdateSortOrder()
		{
		}

		protected virtual void _UpdateSortLayer()
		{
		}

		public virtual void _UpdateStencilRef(bool affectChildren)
		{
			if (this._localStencilRefOffset > 0)
			{
				this._stencilRef = this._root.DefaultStencilRefOffset + this._localStencilRefOffset;
			}
			else
			{
				this._stencilRef = this._root.DefaultStencilRefOffset;
			}
			if (this._stencilRef == this._prevStencilRef)
			{
				return;
			}
			this._UpdateStencilRefBase();
			this._prevStencilRef = this._stencilRef;
		}

		protected virtual void _UpdateStencilRefBase()
		{
		}

		public virtual void _UpdateStencilCompareFunc(bool affectChildren)
		{
			if (this._localStencilCompareFunc != AnStencilCompareFuncTypes.None)
			{
				this._stencilCompareFunc = this._localStencilCompareFunc;
			}
			else
			{
				this._stencilCompareFunc = this._root.DefaultStencilCompareFunc;
			}
			if (this._stencilCompareFunc == this._prevStencilCompareFunc)
			{
				return;
			}
			this._UpdateStencilCompareFuncBase();
			this._prevStencilCompareFunc = this._stencilCompareFunc;
		}

		protected virtual void _UpdateStencilCompareFuncBase()
		{
		}

		public virtual void _UpdateScreenSize()
		{
		}

		public AnRoot _root;

		protected string _id;

		protected GameObject _gameObject;

		public Transform _transform;

		protected bool _visible;

		public bool _visibleInHierarchy;

		protected bool _isInTimeRange;

		public bool _isResetTime;

		protected AnObjectTypes _objectType = AnObjectTypes.Object;

		protected bool _updateFlag;

		protected Color _multiplyColor = Color.white;

		protected Color _colorOffset = AnValue.ColorZero;

		public Color _currentColor = Color.black;

		public Color _currentColorOffset = AnValue.ColorZero;

		protected Color _prevColor = Color.magenta;

		protected Color _prevColorOffset = Color.magenta;

		public bool _visibleByAlpha;

		protected Vector2 _blurValue = Vector2.zero;

		protected Vector2 _currentBlurValue = Vector2.zero;

		protected Vector2 _prevBlurValue = AnValue.Vector2Max;

		protected int _blurPrecision;

		protected int _currentBlurPrecision;

		protected int _prevBlurPrecision = int.MaxValue;

		protected int _blurQuality;

		protected int _currentBlurQuality;

		protected int _prevBlurQuality = int.MaxValue;

		protected bool _isGrayscale;

		protected bool _prevIsGrayscale;

		protected string _layerName = AnValue.TextEmpty;

		protected int _layerIndex;

		public int _sortOffset;

		public int _localSortOffset;

		public string _sortLayerName;

		protected int _sortOrderIndex;

		protected int _sortOrder;

		protected int _sortOrderIndexForDrawTextLater;

		public float _motionSpeed;

		protected bool _enableCollider = true;

		protected bool _colliderThrough;

		protected float _colliderThicknessOffset;

		protected int _stencilRef;

		protected int _prevStencilRef = int.MaxValue;

		protected int _localStencilRefOffset;

		protected AnStencilCompareFuncTypes _stencilCompareFunc;

		protected AnStencilCompareFuncTypes _prevStencilCompareFunc = AnStencilCompareFuncTypes.None;

		protected AnStencilCompareFuncTypes _localStencilCompareFunc = AnStencilCompareFuncTypes.None;

		public AnTimeModeTypes _timeModeType;

		private Action _onAfterUpdateFirst;

		protected static Vector3 _tempVector3_0 = Vector3.zero;

		protected static Vector3 _tempVector3_1 = Vector3.zero;

		protected static Vector3 _tempVector3_2 = Vector3.zero;

		protected static Vector3 _tempVector3_3 = Vector3.zero;

		protected static Vector3 _tempVector3_4 = Vector3.zero;

		protected static Vector4 _tempVector4_0 = Vector3.zero;
	}
}
