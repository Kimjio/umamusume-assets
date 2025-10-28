using System;
using UnityEngine;

namespace AnimateToUnity
{
    public class AnObjectBase : AnBase
    {
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
