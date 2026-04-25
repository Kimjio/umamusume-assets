using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnObjectBase : AnBase
	{
		public AnObjectParameterBase Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		public AnMotion ParentMotion
		{
			get
			{
				return this._parentMotion;
			}
		}

		public GameObject OffsetObject
		{
			get
			{
				return this._offsetObject;
			}
		}

		public bool ExistOffsetObject
		{
			get
			{
				return this._existOffsetObject;
			}
		}

		public AnBlendModeTypes BlendModeType
		{
			get
			{
				return this._blendModeType;
			}
		}

		public int ObjectIndex
		{
			get
			{
				return this._objectIndex;
			}
		}

		public Vector3 CurrentPosition
		{
			get
			{
				return this._currentPosition;
			}
		}

		public Vector3 CurrentPositionOffset
		{
			get
			{
				return this._currentPositionOffset;
			}
		}

		public Vector3 CurrentRotate
		{
			get
			{
				return this._currentRotate;
			}
		}

		public Vector3 CurrentScale
		{
			get
			{
				return this._currentScale;
			}
		}

		public Vector2 CurrentShear
		{
			get
			{
				return this._currentShear;
			}
		}

		public Color BaseColor
		{
			get
			{
				return this._baseColor;
			}
		}

		public float BaseAlpha
		{
			get
			{
				return this._baseColor.a;
			}
		}

		public Color BaseColorOffset
		{
			get
			{
				return this._baseColorOffset;
			}
		}

		public float BaseAlphaOffset
		{
			get
			{
				return this._baseColorOffset.a;
			}
		}

		public Collider Collider
		{
			get
			{
				return this._collider;
			}
			set
			{
				this._collider = value;
			}
		}

		public Collider2D Collider2D
		{
			get
			{
				return this._collider2D;
			}
		}

		public Collider SubCollider
		{
			get
			{
				return this._subCollider;
			}
		}

		public int ExistSubCollider
		{
			get
			{
				return this._existSubCollider;
			}
		}

		public AnObjectBase(GameObject gameObject)
		{
			this._gameObject = gameObject;
			this._transform = gameObject.transform;
			this._id = this._gameObject.GetInstanceID().ToString();
		}

		public virtual void _CreateEditorData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			this._root = parentMotion.Root;
			this._parameter = parameter;
			this._parentMotion = parentMotion;
			this._offsetObject = this._gameObject;
			this._offsetTransform = this._transform;
			if (this._transform.childCount != 0 && this._transform.GetChild(0).name == AnValue.ObjectOffsetName)
			{
				this._offsetObject = this._gameObject.transform.GetChild(0).gameObject;
				this._offsetTransform = this._offsetObject.transform;
			}
		}

		public virtual void _ApplyData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			this._root = parentMotion.Root;
			this._parameter = parameter;
			this._parentMotion = parentMotion;
			this._parentMotion.ObjectList.Add(this);
			this._existOffsetObject = false;
			this._offsetObject = this._gameObject;
			this._offsetTransform = this._transform;
			if (this._transform.childCount != 0 && this._transform.GetChild(0).name == AnValue.ObjectOffsetName)
			{
				this._existOffsetObject = true;
				this._offsetObject = this._gameObject.transform.GetChild(0).gameObject;
				this._offsetTransform = this._offsetObject.transform;
			}
		}

		public override void _CreateData()
		{
			base._CreateData();
			this._visible = true;
			this._positionKeyIndex = new int[3];
			this._positionOffsetKeyIndex = new int[3];
			this._rotateKeyIndex = new int[3];
			this._scaleKeyIndex = new int[3];
			this._shearKeyIndex = new int[2];
			this._blurValueKeyIndex = new int[2];
			this._currentShear = this._parameter.Shear;
			this._objectType = this._parameter.ObjectType;
			this._blendModeType = this._parameter.BlendModeType;
			this._multiplyColor = new Color(1f, 1f, 1f, 1f);
			this._colorOffset = new Color(0f, 0f, 0f, 0f);
			this._baseColor = this._parameter._color;
			this._baseColorOffset = this._parameter._colorOffset;
			this._colorKeyIndex = new int[4];
			this._colorOffsetKeyIndex = new int[4];
			this._stencilRef = 0;
			this._blurQuality = this._parameter.BlurQuality;
			this._blurPrecision = this._parameter.BlurPrecision;
			this._blurValue = this._parameter.BlurValue;
			if (this._parameter.StencilRef == 0)
			{
				this._localStencilRefOffset = 0;
			}
			else
			{
				if (!this._parentMotion.ExistStencilRefCountUp)
				{
					if (this._root.StencilRefCount == -1)
					{
						this._root.StencilRefCount = 0;
					}
					else
					{
						this._root.StencilRefCount += this._root.StencilRefInterval;
					}
					this._parentMotion.ExistStencilRefCountUp = true;
				}
				this._localStencilRefOffset = this._parameter.StencilRef + this._root.StencilRefCount;
			}
			if (this._localStencilRefOffset > 0)
			{
				this._stencilRef = this._root.DefaultStencilRefOffset + this._localStencilRefOffset;
			}
			else
			{
				this._stencilRef = this._root.DefaultStencilRefOffset;
			}
			if (this._parameter.StencilCompareFunc != AnStencilCompareFuncTypes.None)
			{
				this._localStencilCompareFunc = this._parameter.StencilCompareFunc;
			}
			if (this._localStencilCompareFunc != AnStencilCompareFuncTypes.None)
			{
				this._stencilCompareFunc = this._localStencilCompareFunc;
			}
			else
			{
				this._stencilCompareFunc = this._root.DefaultStencilCompareFunc;
			}
			this._layerName = this._parentMotion.Root.Parameter.LayerName;
			this._layerIndex = this._parentMotion.Root.Parameter.LayerIndex;
			if (this._parameter.LayerName != "")
			{
				this._layerName = this._parameter.LayerName;
				this._layerIndex = AnUtilityObject.GetLayerIndex(this._layerName);
			}
			this._gameObject.layer = this._layerIndex;
			this._offsetObject.layer = this._layerIndex;
			this._sortOffset = 0;
			this._localSortOffset = 0;
			if (this._parameter._sortOffset != 0)
			{
				this._localSortOffset = this._parameter._sortOffset;
			}
			this._sortLayerName = this._parentMotion.Root.Parameter.SortLayerName;
			if (this._parameter.SortLayerName != "")
			{
				this._sortLayerName = this._parameter.SortLayerName;
			}
			if (this._parameter.ObjectType == AnObjectTypes.Mask)
			{
				this._objectType = AnObjectTypes.Mask;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.AlphaMask)
			{
				this._objectType = AnObjectTypes.AlphaMask;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.StencilMask)
			{
				this._objectType = AnObjectTypes.StencilMask;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.StencilAlphaMask)
			{
				this._objectType = AnObjectTypes.StencilAlphaMask;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.Opaque)
			{
				this._objectType = AnObjectTypes.Opaque;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.ObjectMask)
			{
				this._objectType = AnObjectTypes.ObjectMask;
			}
			else if (this._parameter.ObjectType == AnObjectTypes.ObjectAlphaMask)
			{
				this._objectType = AnObjectTypes.ObjectAlphaMask;
			}
			this._isGrayscale = false;
			this._timeModeType = this._parameter.TimeModeType;
			this._CheckParentMotion();
			this._CheckCollision();
			this._ResetPrevValue();
		}

		private void _CheckCollision()
		{
			this._existCollider = 0;
			this._existSubCollider = 0;
			this._enableCollider = true;
			this._colliderThrough = false;
			this._collider = null;
			this._collider2D = null;
			this._subCollider = null;
			if (this._parameter.CollisionParamList.Length == 0)
			{
				return;
			}
			this._colliderThrough = this._parameter.CollisionParamList[0].Through;
			this._collider = this._parentMotion.Root.ColliderTable[this._offsetObject] as Collider;
			if (this._collider != null)
			{
				this._existCollider = 1;
				return;
			}
			this._collider2D = this._parentMotion.Root.ColliderTable[this._offsetObject] as Collider2D;
			if (this._collider2D != null)
			{
				this._existCollider = 2;
				return;
			}
		}

		private void _CheckParentMotion()
		{
			if (!this._parentMotion.ExistParentObject)
			{
				return;
			}
			if (this._parentMotion.ParentObject.BlendModeType != AnBlendModeTypes.Normal)
			{
				this._blendModeType = this._parentMotion.ParentObject.BlendModeType;
			}
			if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.Mask)
			{
				this._objectType = AnObjectTypes.Mask;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.AlphaMask)
			{
				this._objectType = AnObjectTypes.AlphaMask;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.StencilMask)
			{
				this._objectType = AnObjectTypes.StencilMask;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.StencilAlphaMask)
			{
				this._objectType = AnObjectTypes.StencilAlphaMask;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.Opaque)
			{
				this._objectType = AnObjectTypes.Opaque;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.ObjectMask)
			{
				this._objectType = AnObjectTypes.ObjectMask;
			}
			else if (this._parentMotion.ParentObject.ObjectType == AnObjectTypes.ObjectAlphaMask)
			{
				this._objectType = AnObjectTypes.ObjectAlphaMask;
			}
			if (this._parentMotion.ParentObject.LayerName != this._parentMotion.Root.Parameter.LayerName)
			{
				this._layerName = this._parentMotion.ParentObject.LayerName;
				this._layerIndex = this._parentMotion.ParentObject.LayerIndex;
				this._gameObject.layer = this._layerIndex;
				this._offsetObject.layer = this._layerIndex;
			}
			if (this._parentMotion.ParentObject.LocalSortOffset != 0)
			{
				this._localSortOffset = this._parentMotion.ParentObject.LocalSortOffset;
			}
			if (this._parentMotion.ParentObject.LocalStencilRefOffset != 0)
			{
				this._localStencilRefOffset = this._parentMotion.ParentObject.LocalStencilRefOffset;
			}
			if (this._parentMotion.ParentObject.LocalStencilCompareFunc != AnStencilCompareFuncTypes.None)
			{
				this._localStencilCompareFunc = this._parentMotion.ParentObject.LocalStencilCompareFunc;
			}
			if (this._parentMotion.ParentObject.SortLayerName != this._parentMotion.Root.Parameter.SortLayerName)
			{
				this._sortLayerName = this._parentMotion.ParentObject.SortLayerName;
			}
			if (this._parentMotion.ParentObject.TimeModeType == AnTimeModeTypes.Sync)
			{
				if (this._parameter.TimeModeType != AnTimeModeTypes.Normal)
				{
					this._timeModeType = AnTimeModeTypes.Sync;
				}
			}
			else if (this._parentMotion.ParentObject.TimeModeType == AnTimeModeTypes.Normal)
			{
				this._timeModeType = AnTimeModeTypes.Normal;
			}
			if (this._parentMotion.ParentObject.BlurQuality != 0)
			{
				this._blurQuality = this._parentMotion.ParentObject.BlurQuality;
			}
			if (this._parentMotion.ParentObject.BlurPrecision != 0)
			{
				this._blurPrecision = this._parentMotion.ParentObject.BlurPrecision;
			}
			if (this._parentMotion.ParentObject.BlurValue != Vector2.zero)
			{
				this._blurValue = this._parentMotion.ParentObject.BlurValue;
			}
		}

		protected override void _ResetPrevValue()
		{
			base._ResetPrevValue();
			this._prevPosition = AnValue.Vector3Max;
			this._prevPositionOffset = AnValue.Vector3Max;
			this._prevRotate = AnValue.Vector3Max;
			this._prevScale = AnValue.Vector3Max;
			this._prevShear = AnValue.Vector2Max;
		}

		public override void _ResetTime()
		{
			base._ResetTime();
			if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Position) != (AnTransformAnimationFlags)0)
			{
				for (int i = 0; i < this._positionKeyIndex.Length; i++)
				{
					this._positionKeyIndex[i] = 0;
				}
			}
			if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionOffset) != (AnTransformAnimationFlags)0)
			{
				for (int j = 0; j < this._positionOffsetKeyIndex.Length; j++)
				{
					this._positionOffsetKeyIndex[j] = 0;
				}
			}
			if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Rotate) != (AnTransformAnimationFlags)0)
			{
				for (int k = 0; k < this._rotateKeyIndex.Length; k++)
				{
					this._rotateKeyIndex[k] = 0;
				}
			}
			if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Scale) != (AnTransformAnimationFlags)0)
			{
				for (int l = 0; l < this._scaleKeyIndex.Length; l++)
				{
					this._scaleKeyIndex[l] = 0;
				}
			}
			if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Shear) != (AnTransformAnimationFlags)0)
			{
				for (int m = 0; m < this._shearKeyIndex.Length; m++)
				{
					this._shearKeyIndex[m] = 0;
				}
			}
			if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.Color) != (AnColorAnimationFlags)0)
			{
				for (int n = 0; n < this._colorKeyIndex.Length; n++)
				{
					this._colorKeyIndex[n] = 0;
				}
			}
			if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffset) != (AnColorAnimationFlags)0)
			{
				for (int num = 0; num < this._colorOffsetKeyIndex.Length; num++)
				{
					this._colorOffsetKeyIndex[num] = 0;
				}
			}
			if ((this._parameter._blurAnimationFlag & AnBlurAnimationFlags.Blur) != (AnBlurAnimationFlags)0)
			{
				for (int num2 = 0; num2 < this._blurValueKeyIndex.Length; num2++)
				{
					this._blurValueKeyIndex[num2] = 0;
				}
			}
			this._ResetPrevValue();
		}

		public override void _FixData()
		{
			base._FixData();
			this._UpdateSortOrder();
			this._UpdateSortLayer();
			this._UpdateStencilRef(false);
			this._UpdateBasePlaceOffset();
			this._SetDepth();
			this._UpdateShear();
			this._CheckUI();
		}

		public override void _FinalizeData()
		{
			base._FinalizeData();
			this._CreateUI();
		}

		private void _UpdateBasePlaceOffset()
		{
			this._basePlaceOffset = Vector3.zero;
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.Default)
			{
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopLeft)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseLeftPosition.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseRightPosition.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopCenter)
			{
				this._basePlaceOffset.x = this._transform.position.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseRightPosition.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopRight)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseRightPosition.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseRightPosition.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleLeft)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseLeftPosition.x;
				this._basePlaceOffset.y = this._transform.position.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleCenter)
			{
				this._basePlaceOffset.x = this._transform.position.x;
				this._basePlaceOffset.y = this._transform.position.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleRight)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseRightPosition.x;
				this._basePlaceOffset.y = this._transform.position.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomLeft)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseLeftPosition.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseLeftPosition.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomCenter)
			{
				this._basePlaceOffset.x = this._transform.position.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseLeftPosition.y;
				return;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomRight)
			{
				this._basePlaceOffset.x = this._transform.position.x - this._root._screenBaseRightPosition.x;
				this._basePlaceOffset.y = this._transform.position.y - this._root._screenBaseLeftPosition.y;
			}
		}

		private void _UpdatePlaceOffset()
		{
			this._placeOffset = Vector3.zero;
			this._placeScale = Vector3.one;
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.Default)
			{
				return;
			}
			AnBase._tempVector3_0 = Vector3.zero;
			AnBase._tempVector3_1 = Vector3.zero;
			AnBase._tempVector3_2 = Vector3.zero;
			AnBase._tempVector3_3 = Vector3.zero;
			AnBase._tempVector3_4 = Vector3.one;
			if (this._root._fitScreen)
			{
				if (this._parameter._placeAnchorAttachType == AnPlaceAnchorAttachTypes.Default)
				{
					AnBase._tempVector3_1.x = this._root._screenLeftPosition.x;
					AnBase._tempVector3_1.y = this._root._screenLeftPosition.y;
					AnBase._tempVector3_2.x = this._root._screenRightPosition.x;
					AnBase._tempVector3_2.y = this._root._screenRightPosition.y;
					AnBase._tempVector3_3.x = this._root._screenOffset.x * 0.5f;
					AnBase._tempVector3_3.y = this._root._screenOffset.y * 0.5f;
					if (this._parameter._placeAnchorScaleType == AnPlaceAnchorScaleTypes.Default)
					{
						AnBase._tempVector3_4.x = this._root._screenScale;
						AnBase._tempVector3_4.y = this._root._screenScale;
					}
				}
				else if (this._parameter._placeAnchorAttachType == AnPlaceAnchorAttachTypes.Edge)
				{
					AnBase._tempVector3_1.x = this._root._screenEdgeLeftPosition.x;
					AnBase._tempVector3_1.y = this._root._screenEdgeLeftPosition.y;
					AnBase._tempVector3_2.x = this._root._screenEdgeRightPosition.x;
					AnBase._tempVector3_2.y = this._root._screenEdgeRightPosition.y;
				}
				else if (this._parameter._placeAnchorAttachType == AnPlaceAnchorAttachTypes.Margin)
				{
					AnBase._tempVector3_1.x = this._root._screenMarginLeftPosition.x;
					AnBase._tempVector3_1.y = this._root._screenMarginLeftPosition.y;
					AnBase._tempVector3_2.x = this._root._screenMarginRightPosition.x;
					AnBase._tempVector3_2.y = this._root._screenMarginRightPosition.y;
					AnBase._tempVector3_3.x = this._root._screenMarginOffset.x * 0.5f;
					AnBase._tempVector3_3.y = this._root._screenMarginOffset.y * 0.5f;
					if (this._parameter._placeAnchorScaleType == AnPlaceAnchorScaleTypes.Default)
					{
						AnBase._tempVector3_4.x = this._root._screenScale;
						AnBase._tempVector3_4.y = this._root._screenScale;
					}
				}
			}
			else
			{
				AnBase._tempVector3_1.x = this._root._screenBaseLeftPosition.x;
				AnBase._tempVector3_1.y = this._root._screenBaseLeftPosition.y;
				AnBase._tempVector3_2.x = this._root._screenBaseRightPosition.x;
				AnBase._tempVector3_2.y = this._root._screenBaseRightPosition.y;
			}
			if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopLeft)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_1.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_2.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopCenter)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_3.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_2.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.TopRight)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_2.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_2.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleLeft)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_1.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_3.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleCenter)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_3.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_3.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.MiddleRight)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_2.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_3.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomLeft)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_1.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_1.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomCenter)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_3.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_1.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			else if (this._parameter.PlaceAnchorType == AnPlaceAnchorTypes.BottomRight)
			{
				AnBase._tempVector3_0.x = AnBase._tempVector3_2.x + this._basePlaceOffset.x * AnBase._tempVector3_4.x;
				AnBase._tempVector3_0.y = AnBase._tempVector3_1.y + this._basePlaceOffset.y * AnBase._tempVector3_4.y;
			}
			this._transform.position = AnBase._tempVector3_0;
			this._placeOffset.x = this._transform.localPosition.x - this._parameter._position.x;
			this._placeOffset.y = this._transform.localPosition.y - this._parameter._position.y;
			this._placeScale.x = AnBase._tempVector3_4.x;
			this._placeScale.y = AnBase._tempVector3_4.y;
			this._UpdateTransform(true);
		}

		private void _SetDepth()
		{
			this._localDepthOffset = this._parameter._depthOffset;
			this._fixLocalDepthOffset = 0f;
			if (this._localDepthOffset != 0f)
			{
				float z = this._transform.localPosition.z;
				this._transform.position = new Vector3(this._transform.position.x, this._transform.position.y, this._transform.position.z + this._localDepthOffset);
				this._fixLocalDepthOffset = this._transform.localPosition.z - z;
			}
		}

		private void _CheckUI()
		{
			if (this._parameter.UIParameter == null)
			{
				return;
			}
			if (this._parameter.UIParameter.UIType == AnUITypes.None)
			{
				return;
			}
			this._root.FinalizeTargetDataList.Add(this);
		}

		private void _CreateUI()
		{
			if (this._parameter.UIParameter == null)
			{
				return;
			}
			this._parameter.UIParameter._CreateData(this);
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
			if (!this._visibleInHierarchy || !this._visibleByAlpha)
			{
				this._UpdateEnableCollision(false);
				return;
			}
			this._UpdateEnableCollision(true);
			this._UpdateTransform(false);
			this._UpdateBlurValue();
		}

		public override void _UpdateSecond()
		{
			base._UpdateSecond();
			this._prevPosition = this._currentPosition;
			this._prevPositionOffset = this._currentPositionOffset;
			this._prevRotate = this._currentRotate;
			this._prevScale = this._currentScale;
			this._prevShear = this._currentShear;
		}

		protected virtual void _UpdateVisible()
		{
			if (this._parentMotion._objectTime < this._parameter._timeRange.x)
			{
				this._isInTimeRange = false;
				return;
			}
			if (this._parentMotion._objectTime >= this._parameter._timeRange.y)
			{
				this._isInTimeRange = false;
				return;
			}
			if (!this._isInTimeRange && !this._root._initializeFlag)
			{
				this._isResetTime = true;
				this._ResetTime();
				this._isResetTime = false;
				this._isInTimeRange = true;
			}
			if (!this._parentMotion._visibleInHierarchy)
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
		}

		protected virtual void _UpdateColor()
		{
			this._colorChanged = false;
			this._colorOffsetChanged = false;
			if (this._parameter._colorAnimationFlag != (AnColorAnimationFlags)0)
			{
				if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.Color) != (AnColorAnimationFlags)0)
				{
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorR) != (AnColorAnimationFlags)0)
					{
						this._currentColor.r = this._parameter._colorKeyParamList[0]._GetValue(this._baseColor.r, this._parentMotion, ref this._colorKeyIndex[0]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorG) != (AnColorAnimationFlags)0)
					{
						this._currentColor.g = this._parameter._colorKeyParamList[1]._GetValue(this._baseColor.g, this._parentMotion, ref this._colorKeyIndex[1]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorB) != (AnColorAnimationFlags)0)
					{
						this._currentColor.b = this._parameter._colorKeyParamList[2]._GetValue(this._baseColor.b, this._parentMotion, ref this._colorKeyIndex[2]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorA) != (AnColorAnimationFlags)0)
					{
						this._currentColor.a = this._parameter._colorKeyParamList[3]._GetValue(this._baseColor.a, this._parentMotion, ref this._colorKeyIndex[3]);
					}
				}
				if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffset) != (AnColorAnimationFlags)0)
				{
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffsetR) != (AnColorAnimationFlags)0)
					{
						this._currentColorOffset.r = this._parameter._colorOffsetKeyParamList[0]._GetValue(this._baseColorOffset.r, this._parentMotion, ref this._colorOffsetKeyIndex[0]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffsetG) != (AnColorAnimationFlags)0)
					{
						this._currentColorOffset.g = this._parameter._colorOffsetKeyParamList[1]._GetValue(this._baseColorOffset.g, this._parentMotion, ref this._colorOffsetKeyIndex[1]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffsetB) != (AnColorAnimationFlags)0)
					{
						this._currentColorOffset.b = this._parameter._colorOffsetKeyParamList[2]._GetValue(this._baseColorOffset.b, this._parentMotion, ref this._colorOffsetKeyIndex[2]);
					}
					if ((this._parameter._colorAnimationFlag & AnColorAnimationFlags.ColorOffsetA) != (AnColorAnimationFlags)0)
					{
						this._currentColorOffset.a = this._parameter._colorOffsetKeyParamList[3]._GetValue(this._baseColorOffset.a, this._parentMotion, ref this._colorOffsetKeyIndex[3]);
					}
				}
			}
			AnUtilityColor.MultiplyColor(ref this._currentColor, this._multiplyColor);
			AnUtilityColor.AddColor(ref this._currentColorOffset, this._colorOffset);
			AnUtilityColor.MultiplyColor(ref this._currentColor, this._parentMotion._currentColor);
			AnUtilityColor.MultiplyColor(ref this._currentColorOffset, this._parentMotion._currentColor);
			AnUtilityColor.AddColor(ref this._currentColorOffset, this._parentMotion._currentColorOffset);
			if (this._currentColor.a + this._currentColorOffset.a <= AnValue.MinAlphaValue)
			{
				return;
			}
			if (!AnUtilityColor.IsSameColor(this._currentColor, this._prevColor))
			{
				this._colorChanged = true;
			}
			if (!AnUtilityColor.IsSameColor(this._currentColorOffset, this._prevColorOffset))
			{
				this._colorOffsetChanged = true;
			}
			this._visibleByAlpha = true;
		}

		protected virtual void _UpdateTransform(bool forceUpdate)
		{
			this._currentPosition = this._parameter._position;
			this._currentPosition.x = this._currentPosition.x + this._placeOffset.x;
			this._currentPosition.y = this._currentPosition.y + this._placeOffset.y;
			this._currentPosition.z = this._currentPosition.z + (this._placeOffset.z + this._localDepthOffset);
			this._currentPositionOffset = this._parameter._positionOffset;
			this._currentRotate = this._parameter._rotate;
			this._currentScale = this._parameter._scale;
			this._currentScale.x = this._currentScale.x * this._placeScale.x;
			this._currentScale.y = this._currentScale.y * this._placeScale.y;
			this._currentScale.z = this._currentScale.z * this._placeScale.z;
			this._currentShear = this._parameter._shear;
			if (forceUpdate)
			{
				this._positionChanged = true;
				this._positionOffsetChanged = true;
				this._rotateChanged = true;
				this._scaleChanged = true;
				this._shearChanged = true;
			}
			else
			{
				this._positionChanged = false;
				this._positionOffsetChanged = false;
				this._rotateChanged = false;
				this._scaleChanged = false;
				this._shearChanged = false;
			}
			if (this._parameter._transformAnimationFlag != (AnTransformAnimationFlags)0)
			{
				if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Position) != (AnTransformAnimationFlags)0)
				{
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionX) != (AnTransformAnimationFlags)0)
					{
						this._currentPosition.x = this._parameter._positionKeyParamList[0]._GetValue(this._parameter._position.x, this._parentMotion, ref this._positionKeyIndex[0]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionY) != (AnTransformAnimationFlags)0)
					{
						this._currentPosition.y = this._parameter._positionKeyParamList[1]._GetValue(this._parameter._position.y, this._parentMotion, ref this._positionKeyIndex[1]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionZ) != (AnTransformAnimationFlags)0)
					{
						this._currentPosition.z = this._parameter._positionKeyParamList[2]._GetValue(this._parameter._position.z, this._parentMotion, ref this._positionKeyIndex[2]);
					}
					if (!AnUtilityVector.IsSameVector(this._currentPosition, this._prevPosition))
					{
						this._transform.localPosition = new Vector3(this._currentPosition.x, this._currentPosition.y, this._currentPosition.z + this._fixLocalDepthOffset) + this._placeOffset;
						this._positionChanged = true;
					}
				}
				if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionOffset) != (AnTransformAnimationFlags)0)
				{
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionOffsetX) != (AnTransformAnimationFlags)0)
					{
						this._currentPositionOffset.x = this._parameter._positionOffsetKeyParamList[0]._GetValue(this._parameter._positionOffset.x, this._parentMotion, ref this._positionOffsetKeyIndex[0]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionOffsetY) != (AnTransformAnimationFlags)0)
					{
						this._currentPositionOffset.y = this._parameter._positionOffsetKeyParamList[1]._GetValue(this._parameter._positionOffset.y, this._parentMotion, ref this._positionOffsetKeyIndex[1]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.PositionOffsetZ) != (AnTransformAnimationFlags)0)
					{
						this._currentPositionOffset.z = this._parameter._positionOffsetKeyParamList[2]._GetValue(this._parameter._positionOffset.z, this._parentMotion, ref this._positionOffsetKeyIndex[2]);
					}
					if (!AnUtilityVector.IsSameVector(this._currentPositionOffset, this._prevPositionOffset))
					{
						this._offsetTransform.localPosition = new Vector3(this._currentPositionOffset.x, this._currentPositionOffset.y, this._currentPositionOffset.z);
						this._positionOffsetChanged = true;
					}
				}
				if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Rotate) != (AnTransformAnimationFlags)0)
				{
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.RotateX) != (AnTransformAnimationFlags)0)
					{
						this._currentRotate.x = this._parameter._rotateKeyParamList[0]._GetValue(this._parameter._rotate.x, this._parentMotion, ref this._rotateKeyIndex[0]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.RotateY) != (AnTransformAnimationFlags)0)
					{
						this._currentRotate.y = this._parameter._rotateKeyParamList[1]._GetValue(this._parameter._rotate.y, this._parentMotion, ref this._rotateKeyIndex[1]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.RotateZ) != (AnTransformAnimationFlags)0)
					{
						this._currentRotate.z = this._parameter._rotateKeyParamList[2]._GetValue(this._parameter._rotate.z, this._parentMotion, ref this._rotateKeyIndex[2]);
					}
					if (!AnUtilityVector.IsSameVector(this._currentRotate, this._prevRotate))
					{
						this._transform.localRotation = Quaternion.Euler(this._currentRotate);
						this._rotateChanged = true;
					}
				}
				if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Scale) != (AnTransformAnimationFlags)0)
				{
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.ScaleX) != (AnTransformAnimationFlags)0)
					{
						this._currentScale.x = this._parameter._scaleKeyParamList[0]._GetValue(this._parameter._scale.x, this._parentMotion, ref this._scaleKeyIndex[0]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.ScaleY) != (AnTransformAnimationFlags)0)
					{
						this._currentScale.y = this._parameter._scaleKeyParamList[1]._GetValue(this._parameter._scale.y, this._parentMotion, ref this._scaleKeyIndex[1]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.ScaleZ) != (AnTransformAnimationFlags)0)
					{
						this._currentScale.z = this._parameter._scaleKeyParamList[2]._GetValue(this._parameter._scale.z, this._parentMotion, ref this._scaleKeyIndex[2]);
					}
					if (!AnUtilityVector.IsSameVector(this._currentScale, this._prevScale))
					{
						this._scaleChanged = true;
					}
				}
				if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.Shear) != (AnTransformAnimationFlags)0)
				{
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.ShearX) != (AnTransformAnimationFlags)0)
					{
						this._currentShear.x = this._parameter._shearKeyParamList[0]._GetValue(this._parameter._shear.x, this._parentMotion, ref this._shearKeyIndex[0]);
					}
					if ((this._parameter._transformAnimationFlag & AnTransformAnimationFlags.ShearY) != (AnTransformAnimationFlags)0)
					{
						this._currentShear.y = this._parameter._shearKeyParamList[1]._GetValue(this._parameter._shear.y, this._parentMotion, ref this._shearKeyIndex[1]);
					}
					if (!AnUtilityVector.IsSameVector(this._currentShear, this._prevShear))
					{
						this._UpdateShear();
						this._shearChanged = true;
					}
				}
			}
		}

		protected void _UpdateShear()
		{
			this._currentShearCosSin.x = Mathf.Cos(0.017453292f * this._currentShear.x);
			this._currentShearCosSin.y = Mathf.Sin(0.017453292f * this._currentShear.x);
			this._currentShearCosSin.z = Mathf.Cos(0.017453292f * this._currentShear.y);
			this._currentShearCosSin.w = Mathf.Sin(0.017453292f * this._currentShear.y);
		}

		protected virtual void _UpdateBlurValue()
		{
			this._blurChanged = false;
			if (this._parentMotion.CurrentBlurQuality > 0)
			{
				if (this._blurQuality <= 0)
				{
					this._currentBlurQuality = this._parentMotion.CurrentBlurQuality;
				}
				else
				{
					this._currentBlurQuality = this._blurQuality;
				}
			}
			else
			{
				this._currentBlurQuality = this._blurQuality;
			}
			if (this._currentBlurQuality <= 0)
			{
				return;
			}
			if (this._parentMotion.CurrentBlurPrecision > 0)
			{
				if (this._blurPrecision <= 0)
				{
					this._currentBlurPrecision = this._parentMotion.CurrentBlurPrecision;
				}
				else
				{
					this._currentBlurPrecision = this._blurPrecision;
				}
			}
			else
			{
				this._currentBlurPrecision = this._blurPrecision;
			}
			this._currentBlurValue = this._blurValue;
			if (this._parameter._blurAnimationFlag != (AnBlurAnimationFlags)0 && (this._parameter._blurAnimationFlag & AnBlurAnimationFlags.Blur) != (AnBlurAnimationFlags)0)
			{
				if ((this._parameter._blurAnimationFlag & AnBlurAnimationFlags.BlurX) != (AnBlurAnimationFlags)0)
				{
					this._currentBlurValue.x = this._parameter._blurKeyParamList[0]._GetValue(this._blurValue.x, this._parentMotion, ref this._blurValueKeyIndex[0]);
				}
				if ((this._parameter._blurAnimationFlag & AnBlurAnimationFlags.BlurX) != (AnBlurAnimationFlags)0)
				{
					this._currentBlurValue.y = this._parameter._blurKeyParamList[1]._GetValue(this._blurValue.y, this._parentMotion, ref this._blurValueKeyIndex[1]);
				}
			}
			if (this._parentMotion.CurrentBlurValue.x != 0f || this._parentMotion.CurrentBlurValue.y != 0f)
			{
				this._currentBlurValue += this._parentMotion.CurrentBlurValue;
			}
			if (!AnUtilityVector.IsSameVector(this._currentBlurValue, this._prevBlurValue))
			{
				this._blurChanged = true;
			}
		}

		protected virtual void _UpdateEnableCollision(bool enable)
		{
			if (this._existCollider == 0)
			{
				return;
			}
			if (this._existCollider == 1)
			{
				if (enable)
				{
					if (!this._collider.enabled && this._enableCollider)
					{
						this._collider.enabled = true;
					}
					return;
				}
				if (this._collider.enabled)
				{
					this._collider.enabled = false;
					return;
				}
			}
			else if (this._existCollider == 2)
			{
				if (enable)
				{
					if (!this._collider2D.enabled && this._enableCollider)
					{
						this._collider2D.enabled = true;
					}
					return;
				}
				if (this._collider2D.enabled)
				{
					this._collider2D.enabled = false;
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
			this._sortLayerName = this._parentMotion.Root.Parameter.SortLayerName;
		}

		public override void SetSortOffset(int sortOffset)
		{
			base.SetSortOffset(sortOffset);
			this._UpdateSortOrder();
		}

		public override void SetSortLayer(string sortLayerName)
		{
			base.SetSortLayer(sortLayerName);
			this._UpdateSortLayer();
		}

		public override void SetColliderThrough(bool through, bool affectChildren)
		{
			base.SetColliderThrough(through, affectChildren);
		}

		public override void SetColliderThicknessOffset(float thicknessOffset, bool affectChildren)
		{
			base.SetColliderThicknessOffset(thicknessOffset, affectChildren);
			this._UpdateColliderThickness(false);
		}

		public override void _UpdateColliderThickness(bool affectChildren)
		{
			base._UpdateColliderThickness(affectChildren);
			if (this._existCollider != 1)
			{
				int existCollider = this._existCollider;
				return;
			}
			BoxCollider boxCollider = this._collider as BoxCollider;
			if (boxCollider == null)
			{
				return;
			}
			boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, this._root.DefaultColliderThickness + this._colliderThicknessOffset);
		}

		public override void SetEnableCollider(bool enable, bool affectChildren)
		{
			base.SetEnableCollider(enable, affectChildren);
			if (this._existCollider != 1)
			{
				if (this._existCollider == 2)
				{
					if (this._visibleInHierarchy)
					{
						this._collider2D.enabled = enable;
						return;
					}
					this._collider2D.enabled = false;
				}
				return;
			}
			if (this._visibleInHierarchy)
			{
				this._collider.enabled = enable;
				return;
			}
			this._collider.enabled = false;
		}

		public override void SetSubCollider(Collider subCollider, bool affectChildren)
		{
			base.SetSubCollider(subCollider, affectChildren);
			this._existSubCollider = 0;
			if (this._existCollider != 1)
			{
				return;
			}
			if (this._collider == null)
			{
				return;
			}
			if (subCollider == null)
			{
				return;
			}
			this._existSubCollider = 1;
			this._subCollider = subCollider;
		}

		public string GetFlagValue(int flagNo)
		{
			if (!this._parameter.FlagTable.Contains(flagNo))
			{
				return "";
			}
			return this._parameter.FlagTable[flagNo] as string;
		}

		public virtual void SetBaseColor(Color value)
		{
			this._baseColor = new Color(value.r, value.g, value.b, this._baseColor.a);
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetBaseAlpha(float alpha)
		{
			this._baseColor.a = alpha;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetBaseColorOffset(Color value)
		{
			this._baseColorOffset = new Color(value.r, value.g, value.b, this._baseColorOffset.a);
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public virtual void SetBaseAlphaOffset(float value)
		{
			this._baseColorOffset.a = value;
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public override void SetBlurQuality(int blurQuality, int blurPrecision, bool affectChildren)
		{
			base.SetBlurQuality(blurQuality, blurPrecision, affectChildren);
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public override void SetBlurValue(Vector2 blurValue, bool affectChildren)
		{
			base.SetBlurValue(blurValue, affectChildren);
			if (AnUtilityObject.CheckParentVisibleInHierarchy(this))
			{
				this._UpdateForce();
			}
		}

		public override void _UpdateScreenSize()
		{
			base._UpdateScreenSize();
			this._UpdatePlaceOffset();
		}

		public AnMotion _parentMotion;

		public AnObjectParameterBase _parameter;

		protected int _objectIndex;

		protected GameObject _offsetObject;

		protected Transform _offsetTransform;

		protected bool _existOffsetObject;

		protected AnBlendModeTypes _blendModeType;

		protected int _existCollider;

		protected Collider _collider;

		protected Collider2D _collider2D;

		protected int _existSubCollider;

		protected Collider _subCollider;

		protected Vector3 _basePlaceOffset = Vector3.zero;

		protected Vector3 _placeOffset = Vector3.zero;

		protected Vector3 _placeScale = Vector3.one;

		protected Vector3 _currentPosition = Vector3.zero;

		protected Vector3 _currentPositionOffset = Vector3.zero;

		protected Vector3 _currentRotate = Vector3.zero;

		protected Vector3 _currentScale = Vector3.zero;

		protected Vector2 _currentShear = Vector3.zero;

		protected Vector3 _prevPosition = AnValue.Vector3Max;

		protected Vector3 _prevPositionOffset = AnValue.Vector3Max;

		protected Vector3 _prevRotate = AnValue.Vector3Max;

		protected Vector3 _prevScale = AnValue.Vector3Max;

		protected Vector2 _prevShear = AnValue.Vector2Max;

		protected Vector4 _currentShearCosSin = Vector4.one;

		protected bool _positionChanged;

		protected bool _positionOffsetChanged;

		protected bool _rotateChanged;

		protected bool _scaleChanged;

		protected bool _shearChanged;

		protected int[] _positionKeyIndex;

		protected int[] _positionOffsetKeyIndex;

		protected int[] _rotateKeyIndex;

		protected int[] _scaleKeyIndex;

		protected int[] _shearKeyIndex;

		protected float _localDepthOffset;

		protected float _fixLocalDepthOffset;

		protected Color _baseColor = Color.white;

		protected Color _baseColorOffset = AnValue.ColorZero;

		protected bool _colorChanged;

		protected bool _colorOffsetChanged;

		protected int[] _colorKeyIndex;

		protected int[] _colorOffsetKeyIndex;

		protected bool _blurChanged;

		protected int[] _blurValueKeyIndex;
	}
}
