using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnObjectParameterBase
    {
		public string ObjectName
		{
			get
			{
				return this._objectName;
			}
			set
			{
				this._objectName = value;
			}
		}

		public int ObjectIndex
		{
			get
			{
				return this._objectIndex;
			}
			set
			{
				this._objectIndex = value;
			}
		}

		public AnPlaceAnchorTypes PlaceAnchorType
		{
			get
			{
				return this._placeAnchorType;
			}
			set
			{
				this._placeAnchorType = value;
			}
		}

		public AnPlaceAnchorAttachTypes PlaceAnchorAttachType
		{
			get
			{
				return this._placeAnchorAttachType;
			}
			set
			{
				this._placeAnchorAttachType = value;
			}
		}

		public AnPlaceAnchorScaleTypes PlaceAnchorScaleType
		{
			get
			{
				return this._placeAnchorScaleType;
			}
			set
			{
				this._placeAnchorScaleType = value;
			}
		}

		public AnObjectTypes ObjectType
		{
			get
			{
				return this._objectType;
			}
			set
			{
				this._objectType = value;
			}
		}

		public AnBlendModeTypes BlendModeType
		{
			get
			{
				return this._blendModeType;
			}
			set
			{
				this._blendModeType = value;
			}
		}

		public Vector2 TimeRange
		{
			get
			{
				return this._timeRange;
			}
			set
			{
				this._timeRange = value;
			}
		}

		public Vector2 Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		public Vector3 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		public Vector3 PositionOffset
		{
			get
			{
				return this._positionOffset;
			}
			set
			{
				this._positionOffset = value;
			}
		}

		public Vector3 Rotate
		{
			get
			{
				return this._rotate;
			}
			set
			{
				this._rotate = value;
			}
		}

		public Vector3 Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		public Vector2 Shear
		{
			get
			{
				return this._shear;
			}
			set
			{
				this._shear = value;
			}
		}

		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		public Color ColorOffset
		{
			get
			{
				return this._colorOffset;
			}
			set
			{
				this._colorOffset = value;
			}
		}

		public float DepthOffset
		{
			get
			{
				return this._depthOffset;
			}
			set
			{
				this._depthOffset = value;
			}
		}

		public string LayerName
		{
			get
			{
				return this._layerName;
			}
			set
			{
				this._layerName = value;
			}
		}

		public int SortOffset
		{
			get
			{
				return this._sortOffset;
			}
			set
			{
				this._sortOffset = value;
			}
		}

		public string SortLayerName
		{
			get
			{
				return this._sortLayerName;
			}
			set
			{
				this._sortLayerName = value;
			}
		}

		public Vector2 BlurValue
		{
			get
			{
				return this._blurValue;
			}
			set
			{
				this._blurValue = value;
			}
		}

		public int BlurQuality
		{
			get
			{
				return this._blurQuality;
			}
			set
			{
				this._blurQuality = value;
			}
		}

		public int BlurPrecision
		{
			get
			{
				return this._blurPrecision;
			}
			set
			{
				this._blurPrecision = value;
			}
		}

		public AnTimeModeTypes TimeModeType
		{
			get
			{
				return this._timeModeType;
			}
			set
			{
				this._timeModeType = value;
			}
		}

		public List<AnKeyParameter> PositionKeyList
		{
			get
			{
				return this._positionKeyParamList;
			}
			set
			{
				this._positionKeyParamList = value;
			}
		}

		public List<AnKeyParameter> PositionOffsetKeyList
		{
			get
			{
				return this._positionOffsetKeyParamList;
			}
			set
			{
				this._positionOffsetKeyParamList = value;
			}
		}

		public List<AnKeyParameter> RotateKeyList
		{
			get
			{
				return this._rotateKeyParamList;
			}
			set
			{
				this._rotateKeyParamList = value;
			}
		}

		public List<AnKeyParameter> ScaleKeyList
		{
			get
			{
				return this._scaleKeyParamList;
			}
			set
			{
				this._scaleKeyParamList = value;
			}
		}

		public List<AnKeyParameter> ShearKeyList
		{
			get
			{
				return this._shearKeyParamList;
			}
			set
			{
				this._shearKeyParamList = value;
			}
		}

		public List<AnKeyParameter> ColorKeyList
		{
			get
			{
				return this._colorKeyParamList;
			}
			set
			{
				this._colorKeyParamList = value;
			}
		}

		public List<AnKeyParameter> ColorOffsetKeyList
		{
			get
			{
				return this._colorOffsetKeyParamList;
			}
			set
			{
				this._colorOffsetKeyParamList = value;
			}
		}

		public List<AnKeyParameter> BlurKeyList
		{
			get
			{
				return this._blurKeyParamList;
			}
			set
			{
				this._blurKeyParamList = value;
			}
		}

		public List<int> FlagKeyList
		{
			get
			{
				return this._flagKeyList;
			}
			set
			{
				this._flagKeyList = value;
			}
		}

		public List<string> FlagValueList
		{
			get
			{
				return this._flagValueList;
			}
			set
			{
				this._flagValueList = value;
			}
		}

		public AnUIParameter UIParameter
		{
			get
			{
				return this._uiParameter;
			}
			set
			{
				this._uiParameter = value;
			}
		}

		public int StencilRef
		{
			get
			{
				return this._stencilRef;
			}
			set
			{
				this._stencilRef = value;
			}
		}

		public AnStencilCompareFuncTypes StencilCompareFunc
		{
			get
			{
				return this._stencilCompareFunc;
			}
			set
			{
				this._stencilCompareFunc = value;
			}
		}

		public AnCollisionParameter[] CollisionParamList
		{
			get
			{
				return this._collisionParamList;
			}
			set
			{
				this._collisionParamList = value;
			}
		}

		public string GameObjectName
		{
			get
			{
				return this._gameObjectName;
			}
			set
			{
				this._gameObjectName = value;
			}
		}

		public Hashtable FlagTable
		{
			get
			{
				return this._flagTable;
			}
			set
			{
				this._flagTable = value;
			}
		}

		public virtual void _CreateEditorData(AnMotion parentMotion)
		{
			this._targetGameObject = parentMotion._GetChildGameObject(this._gameObjectName);
		}

		public virtual void _Initialize()
		{
			this._CheckTransformAnimation();
			this._CheckColorAnimation();
			this._CheckBlurAnimation();
			this._CreateFlagTable();
		}

		public virtual void _CreateHierarchy(AnRoot root, GameObject parentObject)
		{
			this._targetGameObject = new GameObject();
			this._offsetGameObject = null;
			this._attachGameObject = null;
			this._targetGameObject.transform.parent = parentObject.transform;
			this._targetGameObject.transform.localPosition = this._position;
			this._targetGameObject.transform.localRotation = Quaternion.Euler(this._rotate);
			this._targetGameObject.transform.localScale = this._scale;
			if (this._positionOffset != Vector3.zero || this._positionOffsetKeyParamList.Count != 0)
			{
				this._offsetGameObject = new GameObject(AnValue.ObjectOffsetName);
				this._offsetGameObject.transform.parent = this._targetGameObject.transform;
				this._offsetGameObject.transform.localPosition = this._positionOffset;
				this._offsetGameObject.transform.localRotation = Quaternion.identity;
				this._offsetGameObject.transform.localScale = Vector3.one;
				this._attachGameObject = this._offsetGameObject;
			}
			else
			{
				this._attachGameObject = this._targetGameObject;
			}
			if (this._collisionParamList != null)
			{
				for (int i = 0; i < this._collisionParamList.Length; i++)
				{
					this._collisionParamList[i]._CreateHierarchy(root, this._attachGameObject);
				}
			}
		}

		public virtual void _ApplyData(AnMotion parentMotion)
		{
			this._targetGameObject = parentMotion._GetChildGameObject(this._gameObjectName);
		}

		protected virtual void _CheckTransformAnimation()
		{
			this._transformAnimationFlag = (AnTransformAnimationFlags)0;
			if (this._ExistAnimation(this._positionKeyParamList, 0))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionX;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Position;
			}
			if (this._ExistAnimation(this._positionKeyParamList, 1))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionY;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Position;
			}
			if (this._ExistAnimation(this._positionKeyParamList, 2))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionZ;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Position;
			}
			if (this._ExistAnimation(this._positionOffsetKeyParamList, 0))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffsetX;
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffset;
			}
			if (this._ExistAnimation(this._positionOffsetKeyParamList, 1))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffsetY;
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffset;
			}
			if (this._ExistAnimation(this._positionOffsetKeyParamList, 2))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffsetZ;
				this._transformAnimationFlag |= AnTransformAnimationFlags.PositionOffset;
			}
			if (this._ExistAnimation(this._rotateKeyParamList, 0))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.RotateX;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Rotate;
			}
			if (this._ExistAnimation(this._rotateKeyParamList, 1))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.RotateY;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Rotate;
			}
			if (this._ExistAnimation(this._rotateKeyParamList, 2))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.RotateZ;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Rotate;
			}
			if (this._ExistAnimation(this._scaleKeyParamList, 0))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.ScaleX;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Scale;
			}
			if (this._ExistAnimation(this._scaleKeyParamList, 1))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.ScaleY;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Scale;
			}
			if (this._ExistAnimation(this._scaleKeyParamList, 2))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.ScaleZ;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Scale;
			}
			if (this._ExistAnimation(this._shearKeyParamList, 0))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.ShearX;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Shear;
			}
			if (this._ExistAnimation(this._shearKeyParamList, 1))
			{
				this._transformAnimationFlag |= AnTransformAnimationFlags.ShearY;
				this._transformAnimationFlag |= AnTransformAnimationFlags.Shear;
			}
		}

		protected virtual void _CheckColorAnimation()
		{
			this._colorAnimationFlag = (AnColorAnimationFlags)0;
			if (this._ExistAnimation(this._colorKeyParamList, 0))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorR;
				this._colorAnimationFlag |= AnColorAnimationFlags.Color;
			}
			if (this._ExistAnimation(this._colorKeyParamList, 1))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorG;
				this._colorAnimationFlag |= AnColorAnimationFlags.Color;
			}
			if (this._ExistAnimation(this._colorKeyParamList, 2))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorB;
				this._colorAnimationFlag |= AnColorAnimationFlags.Color;
			}
			if (this._ExistAnimation(this._colorKeyParamList, 3))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorA;
				this._colorAnimationFlag |= AnColorAnimationFlags.Color;
			}
			if (this._ExistAnimation(this._colorOffsetKeyParamList, 0))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffsetR;
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffset;
			}
			if (this._ExistAnimation(this._colorOffsetKeyParamList, 1))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffsetG;
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffset;
			}
			if (this._ExistAnimation(this._colorOffsetKeyParamList, 2))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffsetB;
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffset;
			}
			if (this._ExistAnimation(this._colorOffsetKeyParamList, 3))
			{
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffsetA;
				this._colorAnimationFlag |= AnColorAnimationFlags.ColorOffset;
			}
		}

		protected virtual void _CheckBlurAnimation()
		{
			this._blurAnimationFlag = (AnBlurAnimationFlags)0;
			if (this._ExistAnimation(this._blurKeyParamList, 0))
			{
				this._blurAnimationFlag |= AnBlurAnimationFlags.BlurX;
				this._blurAnimationFlag |= AnBlurAnimationFlags.Blur;
			}
			if (this._ExistAnimation(this._blurKeyParamList, 1))
			{
				this._blurAnimationFlag |= AnBlurAnimationFlags.BlurY;
				this._blurAnimationFlag |= AnBlurAnimationFlags.Blur;
			}
		}

		protected virtual bool _ExistAnimation(List<AnKeyParameter> target, int index)
		{
			return target != null && target.Count != 0 && index < target.Count && index >= 0 && this._ExistAnimation(target[index]);
		}

		protected virtual bool _ExistAnimation(AnKeyParameter target)
		{
			if (target == null)
			{
				return false;
			}
			if (target._keyList == null)
			{
				return false;
			}
			if (target._keyList.Count == 0)
			{
				return false;
			}
			target._keyCount = target._keyList.Count;
			return true;
		}

		protected virtual void _CreateFlagTable()
		{
			this._flagTable = new Hashtable();
			if (this._flagValueList == null || this._flagKeyList == null)
			{
				return;
			}
			if (this._flagKeyList.Count != this._flagValueList.Count)
			{
				return;
			}
			for (int i = 0; i < this._flagKeyList.Count; i++)
			{
				this._flagTable.Add(this._flagKeyList[i], this._flagValueList[i]);
			}
		}

        public string _objectName;

        public int _objectIndex;

        public AnObjectTypes _objectType = AnObjectTypes.Object;

        public AnBlendModeTypes _blendModeType;

        public Vector2 _timeRange = Vector2.zero;

        public Vector2 _size = Vector2.zero;

        public Vector3 _position = Vector3.zero;

        public Vector3 _positionOffset = Vector3.zero;

        public Vector3 _rotate = Vector3.zero;

        public Vector3 _scale = Vector3.one;

        public Vector2 _shear = Vector2.zero;

        public Color _color = Color.white;

        public Color _colorOffset = Color.black;

        public bool _isStabilizeRotation;

        public AnPlaceAnchorTypes _placeAnchorType;

        public AnPlaceAnchorAttachTypes _placeAnchorAttachType;

        public AnPlaceAnchorScaleTypes _placeAnchorScaleType;

        public string _layerName;

        public string _sortLayerName;

        public int _sortOffset;

        public float _depthOffset;

        public int _stencilRef;

        public AnStencilCompareFuncTypes _stencilCompareFunc = AnStencilCompareFuncTypes.None;

        public AnTimeModeTypes _timeModeType;

        public Vector2 _blurValue = Vector2.zero;

        public int _blurQuality;

        public int _blurPrecision = 1;

        public List<AnKeyParameter> _positionKeyParamList;

        public List<AnKeyParameter> _positionOffsetKeyParamList;

        public List<AnKeyParameter> _rotateKeyParamList;

        public List<AnKeyParameter> _scaleKeyParamList;

        public List<AnKeyParameter> _shearKeyParamList;

        public List<AnKeyParameter> _colorKeyParamList;

        public List<AnKeyParameter> _colorOffsetKeyParamList;

        public List<AnKeyParameter> _blurKeyParamList;

        public AnCollisionTypes _collisionType;

        public AnCollisionParameter[] _collisionParamList;

        public List<int> _flagKeyList;

        public List<string> _flagValueList;

        public AnUIParameter _uiParameter;

        protected string _gameObjectName;

        protected GameObject _targetGameObject;

        protected GameObject _offsetGameObject;

        protected GameObject _attachGameObject;

        [NonSerialized]
        public AnTransformAnimationFlags _transformAnimationFlag;

        [NonSerialized]
        public AnColorAnimationFlags _colorAnimationFlag;

        [NonSerialized]
        public AnBlurAnimationFlags _blurAnimationFlag;

        private Hashtable _flagTable;
    }
}
