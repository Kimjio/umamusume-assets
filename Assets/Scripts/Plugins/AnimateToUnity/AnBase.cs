using System;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnBase
    {
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
